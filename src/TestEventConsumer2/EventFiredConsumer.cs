using System;
using System.Threading.Tasks;

using MassTransit;

using Microsoft.Extensions.Logging;

using TestTransit.Shared;

namespace TestEventConsume2
{
    public class EventFiredConsumer : IConsumer<IEventFired>
    {
        private readonly ILogger<EventFiredConsumer> _logger;

        public EventFiredConsumer(ILogger<EventFiredConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IEventFired> context)
        {
            _logger.LogInformation(context.Message.Name);
            await Console.Out.WriteLineAsync(context.Message.Name);
        }
    }
}
