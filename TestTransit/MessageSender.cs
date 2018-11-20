using System;
using System.Threading;
using System.Threading.Tasks;

using MassTransit;

using Microsoft.Extensions.Hosting;

using TestTransit.Shared;

namespace TestTransit
{
    public class MessageSender : IHostedService, IDisposable
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private Timer _timer;

        private int _counter = 0;

        public MessageSender(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
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
            _publishEndpoint.Publish<IPublishMyMessage>(new { Id = NewId.NextGuid(), Name = $"Test Message-{_counter}" }).Wait();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}