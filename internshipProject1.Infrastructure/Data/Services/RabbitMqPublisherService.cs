using internshipproject1.Application.DTOs;
using internshipproject1.Application.Interfaces.Services;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace internshipProject1.Infrastructure.Services
{
    public class RabbitMqPublisherService :IRabbitMqPublisherService, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName = "payment-events";

        public RabbitMqPublisherService(string hostname = "localhost")
        {
            var factory = new ConnectionFactory()
            {
                HostName = hostname,
                DispatchConsumersAsync = true
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public void PublishAsync<T>(string queueName, T message)
        {
            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);
            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: properties, body: body);
        }
        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }

        
    }
}