using internshipproject1.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace internshipProject1.WebAPI.Controllers
{
    public class ConsumerController : ControllerBase
    {
        
            private readonly IRabbitMqConsumerService _consumerService;

            public ConsumerController(IRabbitMqConsumerService consumerService)
            {
                _consumerService = consumerService;

                _consumerService.MessageReceived += OnMessageReceived;
                _consumerService.ErrorOccurred += OnErrorOccurred;
            }

            private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
            {
                Console.WriteLine($"Queue: {e.QueueName}, Message: {e.Message}");
            }

            private void OnErrorOccurred(object sender, ConsumerErrorEventArgs e)
            {
                Console.WriteLine($"Error in queue {e.QueueName}: {e.Exception.Message}");
            }

            [HttpPost("start-consumer")]
            public async Task<IActionResult> StartConsumer(string queueName,CancellationToken cancellationToken)
            {
                await _consumerService.StartAsync(cancellationToken);
                await _consumerService.StartConsumingAsync(queueName,cancellationToken);
                return Ok($"Queue '{queueName}' dinlemeye başlandı");
            }
    }
}
