using internshipproject1.Application.DTOs;
using internshipproject1.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace internshipProject1.Infrastructure.Data.Services
{
    public class RabbitMqConsumerService : IRabbitMqConsumerService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly ConcurrentDictionary<string, AsyncEventingBasicConsumer> _consumers
            = new ConcurrentDictionary<string, AsyncEventingBasicConsumer>();
        private readonly ConcurrentDictionary<string, string> _consumerTags
            = new ConcurrentDictionary<string, string>();
        private readonly ILogger<RabbitMqConsumerService> _logger;
        private bool _isRunning;
        private bool _disposed;
        private readonly object _lockObject = new object();

        public bool IsRunning => _isRunning && !_disposed;

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<ConsumerErrorEventArgs> ErrorOccurred;

        public RabbitMqConsumerService(IServiceScopeFactory scopeFactory,ILogger<RabbitMqConsumerService> logger, string hostname = "localhost")
        {
            _logger = logger;
            _scopeFactory = scopeFactory;

            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = hostname,
                    DispatchConsumersAsync = true,
                    AutomaticRecoveryEnabled = true,
                    NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
                };
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _logger.LogInformation("RabbitMQ bağlantısı başarıyla kuruldu");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RabbitMQ bağlantısı kurulurken hata oluştu");
                throw;
            }

     
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (_lockObject)
            {
                if (!_disposed && disposing)
                {
                    try
                    {
                        _isRunning = false;

                        // Tüm consumer'ları durdur
                        foreach (var queueName in _consumers.Keys)
                        {
                            try
                            {
                                if (_consumerTags.TryGetValue(queueName, out var consumerTag) &&
                                    !string.IsNullOrEmpty(consumerTag))
                                {
                                    _channel?.BasicCancel(consumerTag);
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogWarning(ex, "Consumer durdurulurken hata oluştu");
                            }
                        }
                        _consumers.Clear();
                        _consumerTags.Clear();

                        _channel?.Close();
                        _connection?.Close();
                        _channel?.Dispose();
                        _connection?.Dispose();

                        _logger.LogInformation("RabbitMQ bağlantısı güvenli bir şekilde kapatıldı");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "RabbitMQ bağlantısı kapatılırken hata oluştu");
                    }

                    _disposed = true;
                }
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            lock (_lockObject)
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(RabbitMqConsumerService));
                }

                _isRunning = true;
                _logger.LogInformation("RabbitMQ Consumer servisi başlatıldı");
            }
            await Task.CompletedTask;
        }

        public async Task StartConsumingAsync(string queueName, CancellationToken cancellationToken = default)
        {
            lock (_lockObject)
            {
                if (_disposed)
                {
                    throw new ObjectDisposedException(nameof(RabbitMqConsumerService));
                }

                if (!_isRunning)
                {
                    throw new InvalidOperationException("Consumer servisi başlatılmamış");
                }

                if (_consumers.ContainsKey(queueName))
                {
                    _logger.LogWarning($"Queue '{queueName}' zaten dinleniyor");
                    return;
                }
            }

            try
            {
                // Queue'yu declare et
                _channel.QueueDeclare(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var consumer = new AsyncEventingBasicConsumer(_channel);

                consumer.Received += async (model, ea) =>
                {
                    // Dispose kontrolü
                    if (_disposed)
                    {
                        return;
                    }

                    try
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                            Converters = { new JsonStringEnumConverter() }
                        };

                        var message = Encoding.UTF8.GetString(ea.Body.Span);
                        var dto = JsonSerializer.Deserialize<PaymentEventDTO>(message);
                        _logger.LogInformation($"Queue '{queueName}''den mesaj alındı: {message}");
                        if (dto != null) 
                        {
                            using (var scope = _scopeFactory.CreateScope())
                            { 
                                var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
                                await paymentService.ProcessPaymentAsync(dto, cancellationToken);
                            }
                            // Event'i tetikle
                            MessageReceived?.Invoke(this, new MessageReceivedEventArgs
                            {
                                QueueName = queueName,
                                Message = message,
                                ReceivedAt = DateTime.UtcNow,
                                DeliveryTag = ea.DeliveryTag
                            });

                        }
                        
                        // Mesajı acknowledge et (dispose kontrolü ile)
                        if (!_disposed && _channel != null && _channel.IsOpen)
                        {
                            _channel.BasicAck(ea.DeliveryTag, multiple: false);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Mesaj işlenirken hata oluştu. Queue: {queueName}");

                        // Hata event'ini tetikle
                        ErrorOccurred?.Invoke(this, new ConsumerErrorEventArgs
                        {
                            QueueName = queueName,
                            Exception = ex,
                            OccurredAt = DateTime.UtcNow
                        });

                        // Mesajı reject et (dispose kontrolü ile)
                        if (!_disposed && _channel != null && _channel.IsOpen)
                        {
                            _channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: false);
                        }
                    }
                };

                consumer.Shutdown += async (model, ea) =>
                {
                    _logger.LogWarning($"Consumer shutdown oldu. Queue: {queueName}, Reason: {ea.Initiator}");
                    await Task.CompletedTask;
                };

                var consumerTag = _channel.BasicConsume(
                    queue: queueName,
                    autoAck: false,
                    consumer: consumer
                );

                _consumers.TryAdd(queueName, consumer);
                _consumerTags.TryAdd(queueName, consumerTag);

                _logger.LogInformation($"Queue '{queueName}' dinlemeye başlandı. Consumer Tag: {consumerTag}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Queue '{queueName}' dinlemeye başlanırken hata oluştu");
                throw;
            }

            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            lock (_lockObject)
            {
                if (_disposed)
                {
                    return;
                }

                _isRunning = false;

                foreach (var queueName in _consumers.Keys)
                {
                    try
                    {
                        if (_consumerTags.TryGetValue(queueName, out var consumerTag) &&
                            !string.IsNullOrEmpty(consumerTag) &&
                            _channel != null &&
                            _channel.IsOpen)
                        {
                            _channel.BasicCancel(consumerTag);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Consumer durdurulurken hata oluştu");
                    }
                }
                _consumers.Clear();
                _consumerTags.Clear();

                _logger.LogInformation("RabbitMQ Consumer servisi durduruldu");
            }
            await Task.CompletedTask;
        }

        public async Task StopConsumingAsync(string queueName)
        {
            if (_consumers.TryRemove(queueName, out var consumer))
            {
                try
                {
                    if (_consumerTags.TryRemove(queueName, out var consumerTag) &&
                        !string.IsNullOrEmpty(consumerTag) &&
                        _channel != null &&
                        _channel.IsOpen)
                    {
                        _channel.BasicCancel(consumerTag);
                        _logger.LogInformation($"Queue '{queueName}' dinleme durduruldu");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Queue '{queueName}' dinleme durdurulurken hata oluştu");
                }
            }
            else
            {
                _logger.LogWarning($"Queue '{queueName}' dinlenmiyor");
            }

            await Task.CompletedTask;
        }
    }
}