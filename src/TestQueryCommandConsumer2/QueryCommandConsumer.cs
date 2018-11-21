using System;
using System.Threading.Tasks;

using MassTransit;

using Microsoft.Extensions.Logging;

using TestTransit.Shared;

namespace TestQueryCommandConsumer2
{
    public class QueryCommandConsumer : IConsumer<IQueryCommand>
    {
        private readonly ILogger<QueryCommandConsumer> _logger;

        public QueryCommandConsumer(ILogger<QueryCommandConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IQueryCommand> context)
        {
            _logger.LogInformation(context.Message.Name);
            await context.RespondAsync<IQueryCommandResult>(new { Id = context.Message.Id, Name = context.Message.Name + ": From 2" });
        }
    }
}
