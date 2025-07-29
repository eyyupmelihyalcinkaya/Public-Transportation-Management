using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using internshipproject1.Application.Interfaces.Services;
using DnsClient.Internal;
using Microsoft.Extensions.Logging;

namespace internshipProject1.Infrastructure.Data.Services
{
    public class RabbitMqConsumerBackgroundService : BackgroundService
    {
        private readonly IRabbitMqConsumerService _consumerService;
        private readonly ILogger<RabbitMqConsumerBackgroundService> _logger;
    
        public RabbitMqConsumerBackgroundService(IRabbitMqConsumerService consumerService, ILogger<RabbitMqConsumerBackgroundService> logger)
        {
            _consumerService = consumerService;
            _logger = logger;

            _consumerService.MessageReceived += (s, e) =>
            {
                _logger.LogInformation($"Queue: {e.QueueName} , Message: {e.Message}, Received At: {e.ReceivedAt}");
            };
            _consumerService.ErrorOccurred += (s, e) =>
            {
                _logger.LogError(e.Exception, $"Error in queue {e.QueueName} at {e.OccurredAt}: {e.Exception.Message}");
            };
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
                _logger.LogInformation("RabbitMqConsumerBackgroundService başlatılıyor...");
                await _consumerService.StartAsync(cancellationToken);
                await _consumerService.StartConsumingAsync("boarding-events", cancellationToken);
                _logger.LogInformation("RabbitMqConsumerBackgroundService çalışıyor.");
                await Task.Delay(Timeout.Infinite, cancellationToken);
            }
            catch (OperationCanceledException)
            { 
                _logger.LogInformation("RabbitMqConsumerBackgroundService is stopping due to cancellation.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RabbitMQ consumer background service başlatılırken bir hata oluştu.");
            }
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RabbitMQ consumer background service durduruluyor...");
            await _consumerService.StopAsync(cancellationToken);
            _logger.LogInformation("RabbitMQ consumer background service durduruldu.");
            await base.StopAsync(cancellationToken);
        }
    }
}
