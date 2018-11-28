using System;
using System.Threading;
using System.Threading.Tasks;

using MassTransit;

using Microsoft.Extensions.Hosting;

using TestTransit.Shared;

namespace TestEventPublisher
{
    public class MessageSender : IHostedService, IDisposable
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private Timer _timer;

        private int _counter = 0;

        private bool _isTimerActive = true;

        public MessageSender(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            _isTimerActive = true;
            _timer = new Timer(Trigger, null, 1000, 200);
        }

        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            _isTimerActive = false;
            _timer.Change(TimeSpan.Zero, TimeSpan.Zero);
        }

        public void Trigger(object state)
        {
            if (!_isTimerActive)
                return;

            _counter++;
            _publishEndpoint.Publish<IEventFired>(new { Id = NewId.NextGuid(), Name = $"Event Fired-{_counter}" }).Wait();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}