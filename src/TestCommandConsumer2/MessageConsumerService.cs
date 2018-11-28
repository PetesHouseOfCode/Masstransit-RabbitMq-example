using System.Threading;
using System.Threading.Tasks;

using MassTransit;

using Microsoft.Extensions.Hosting;

using TestTransit.Shared;

namespace TestCommandConsumer2
{
    public class MessageConsumerService : IHostedService
    {
        private readonly IRequestClient<IEventFired> _requestClient;

        public MessageConsumerService(IRequestClient<IEventFired> requestClient)
        {
            _requestClient = requestClient;
        }

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {

        }

        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
        }
    }
}