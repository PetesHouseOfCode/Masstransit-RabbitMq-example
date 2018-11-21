using System;
using System.Threading;
using System.Threading.Tasks;

using MassTransit;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using TestTransit.Shared;

namespace TestQueryCommandPublisher
{
    public class MessageSender : IHostedService, IDisposable
    {
        private readonly IRequestClient<IQueryCommand, IQueryCommandResult> _requestClient;

        private readonly ILogger<MessageSender> _logger;

        private Timer _timer;

        private int _counter = 0;

        public MessageSender(IRequestClient<IQueryCommand, IQueryCommandResult> requestClient, ILogger<MessageSender> logger)
        {
            _requestClient = requestClient;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            _timer = new Timer(Trigger, null, 1000, 200);
        }

        public async Task StopAsync(CancellationToken cancellationToken = default)
        {

        }

        public void Trigger(object state)
        {
            _counter++;
            var message = _requestClient.Request(new { Id = NewId.NextGuid(), Name = $"Query Fired-{_counter}" }).Result;
            _logger.LogInformation($"Received Result: {message.Name}");
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}