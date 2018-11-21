using System.Threading.Tasks;

using MassTransit;

using Microsoft.Extensions.Logging;

using TestTransit.Shared;

namespace TestEventConsumer1
{
    public class EventFiredConsumer : IConsumer<IEventFired>
    {
        private readonly ILogger<EventFiredConsumer> _logger;

        public EventFiredConsumer(ILogger<EventFiredConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<IEventFired> context)
        {
            _logger.LogInformation(context.Message.Name);
            return Task.FromResult(context.Message);
        }
    }
}