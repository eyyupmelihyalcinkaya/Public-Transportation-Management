using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Interfaces.Services
{
    public interface IRabbitMqConsumerService : IDisposable
    {
        Task StartAsync(CancellationToken cancellationToken );
        Task StopAsync(CancellationToken cancellationToken );
        Task StartConsumingAsync (string queueName, CancellationToken cancellationToken);
        Task StopConsumingAsync(string queueName);
        bool IsRunning { get; }
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        event EventHandler<ConsumerErrorEventArgs> ErrorOccurred;
    }
    public class MessageReceivedEventArgs : EventArgs
    { 
        public string QueueName { get; set; }
        public string Message { get; set; }
        public DateTime ReceivedAt { get; set; }
        public ulong DeliveryTag { get; set; }
    }
    public class  ConsumerErrorEventArgs : EventArgs
    {
        public string QueueName { get; set; }
        public Exception Exception { get; set; }
        public DateTime OccurredAt { get; set; }
    }

}
