using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using PaymentService.Events;
namespace PaymentService.Services
{
    public class RabbitMqService
    {
        //TODO: RabbitMq implemente edilecek

        private readonly string _hostname = "localhost";
        private readonly string _queueName = "boarding-events";
        public void Publish(BoardingCompletedEvent evt)
        {
            /*
            var factory = new ConnectionFactory() { HostName = _hostname };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue:_queueName,durable:false,exclusive:false,autoDelete:false , arguments:null);
            var json = JsonSerializer.Serialize(evt);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange:"",routingKey: _queueName,basicProperties:null,body : body);
        */
        }
    }
}
