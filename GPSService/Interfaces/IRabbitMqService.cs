using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSService.Interfaces
{
    public interface IRabbitMqService : IDisposable
    {
        Task PublishAsync<T>(string queueName, T message);
        Task StartConsumingAsync(string queueName, Func<string, Task> messageHandler);
        Task StopConsumingAsync(string queueName);
    }
}
