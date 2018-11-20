using System;
using System.Threading.Tasks;

using MassTransit;

using Microsoft.Extensions.Logging;

using TestTransit.Shared;

namespace TestTransitConsume2
{
    public class PublishMyMessageConsumer : IConsumer<IPublishMyMessage>
    {
        private readonly ILogger<PublishMyMessageConsumer> _logger;

        public PublishMyMessageConsumer(ILogger<PublishMyMessageConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IPublishMyMessage> context)
        {
            _logger.LogInformation(context.Message.Name);
            await Console.Out.WriteLineAsync(context.Message.Name);
        }
    }
}
