using System;
using System.Threading;
using System.Threading.Tasks;

using MassTransit;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using TestTransit.Shared;

namespace TestCommandPublisher
{
    public class MessageSender : IHostedService, IDisposable
    {

        private readonly IBus _bus;

        private readonly ILogger<MessageSender> _logger;

        private Timer _timer;

        private int _counter = 0;

        private bool _isTimerActive = true;

        public MessageSender(IBus bus, ILogger<MessageSender> logger)
        {
            _bus = bus;
            _logger = logger;
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
            var message = $"Query Fired-{_counter}";
            _bus.Send<ICommand>(new { Id = NewId.NextGuid(), Name = message }).Wait();
            _logger.LogInformation($"Sent Message \"{message}\"");
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}