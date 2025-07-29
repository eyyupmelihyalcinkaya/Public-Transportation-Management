using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internshipproject1.Application.Interfaces.Services
{
    public interface IRabbitMqPublisherService : IDisposable
    {
        void PublishAsync<T>(string queueName, T message);

    }
}
