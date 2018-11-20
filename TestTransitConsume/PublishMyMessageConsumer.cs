using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using TestTransit.Shared;

namespace TestTransitConsume
{
    public class PublishMyMessageConsumer : IConsumer<IPublishMyMessage>
    {
        private readonly ILogger<PublishMyMessageConsumer> _logger;

        public PublishMyMessageConsumer(ILogger<PublishMyMessageConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<IPublishMyMessage> context)
        {
            _logger.LogInformation(context.Message.Name);
            return Task.FromResult(context.Message);
        }
    }
}