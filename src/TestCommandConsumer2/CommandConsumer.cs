using System.Threading.Tasks;

using MassTransit;

using Microsoft.Extensions.Logging;

using TestTransit.Shared;

namespace TestCommandConsumer2
{
    public class CommandConsumer : IConsumer<ICommand>
    {
        private readonly ILogger<CommandConsumer> _logger;

        public CommandConsumer(ILogger<CommandConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ICommand> context)
        {
            _logger.LogInformation(context.Message.Name);
        }
    }
}
