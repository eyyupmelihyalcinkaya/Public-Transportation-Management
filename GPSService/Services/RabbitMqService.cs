using GPSService.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GPSService.Services
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ILogger<RabbitMqService> _logger;
        private readonly Dictionary<string, AsyncEventingBasicConsumer> _consumers;
        private readonly Dictionary<string, string> _consumerTags;

        public RabbitMqService(IConfiguration configuration, ILogger<RabbitMqService> logger)
        {
            _logger = logger;
            _consumers = new Dictionary<string, AsyncEventingBasicConsumer>();
            _consumerTags = new Dictionary<string, string>();

            var factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMQ:HostName"] ?? "localhost",
                UserName = configuration["RabbitMQ:UserName"] ?? "guest",
                Password = configuration["RabbitMQ:Password"] ?? "guest",
                Port = configuration.GetValue<int>("RabbitMQ:Port", 5672),
                DispatchConsumersAsync = true
            };

            try
            {
                _connection = factory.CreateConnection("GPSService");
                _channel = _connection.CreateModel();

                _logger.LogInformation("RabbitMQ bağlantısı kuruldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RabbitMQ bağlantısı kurulamadı");
                throw;
            }
        }

        public async Task PublishAsync<T>(string queueName, T message)
        {
            try
            {
                // Queue'yu declare et
                _channel.QueueDeclare(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var json = JsonSerializer.Serialize(message, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                var body = Encoding.UTF8.GetBytes(json);
                var properties = _channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                properties.MessageId = Guid.NewGuid().ToString();

                _channel.BasicPublish(
                    exchange: "",
                    routingKey: queueName,
                    basicProperties: properties,
                    body: body);

                _logger.LogDebug("Mesaj {QueueName} kuyruğuna gönderildi: {MessageId}",
                    queueName, properties.MessageId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mesaj {QueueName} kuyruğuna gönderilemedi", queueName);
                throw;
            }
        }

        public async Task StartConsumingAsync(string queueName, Func<string, Task> messageHandler)
        {
            try
            {
                // Queue'yu declare et
                _channel.QueueDeclare(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new AsyncEventingBasicConsumer(_channel);

                consumer.Received += async (model, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        _logger.LogDebug("Mesaj alındı {QueueName}: {MessageId}",
                            queueName, ea.BasicProperties?.MessageId);

                        await messageHandler(message);

                        _channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Mesaj işlenirken hata {QueueName}", queueName);

                        _channel.BasicNack(ea.DeliveryTag, false, false);
                    }
                };

                var consumerTag = _channel.BasicConsume(
                    queue: queueName,
                    autoAck: false,
                    consumer: consumer);

                _consumers[queueName] = consumer;
                _consumerTags[queueName] = consumerTag;

                _logger.LogInformation("Consumer {QueueName} kuyruğu için başlatıldı", queueName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Consumer {QueueName} için başlatılamadı", queueName);
                throw;
            }
        }

        public async Task StopConsumingAsync(string queueName)
        {
            try
            {
                if (_consumerTags.TryGetValue(queueName, out var consumerTag))
                {
                    _channel.BasicCancel(consumerTag);
                    _consumerTags.Remove(queueName);
                    _consumers.Remove(queueName);

                    _logger.LogInformation("Consumer {QueueName} kuyruğu için durduruldu", queueName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Consumer {QueueName} durdurulurken hata", queueName);
            }
        }

        public void Dispose()
        {
            try
            {
                foreach (var queueName in _consumerTags.Keys.ToList())
                {
                    StopConsumingAsync(queueName).Wait(TimeSpan.FromSeconds(5));
                }

                _channel?.Close();
                _connection?.Close();

                _logger.LogInformation("RabbitMQ bağlantısı kapatıldı");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RabbitMQ bağlantısı kapatılırken hata");
            }
        }
    }
}
