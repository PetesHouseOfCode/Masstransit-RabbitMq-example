using System.Threading;
using System.Threading.Tasks;

using MassTransit;

using Microsoft.Extensions.Hosting;

using TestTransit.Shared;

namespace TestTransitConsume2
{
    public class MessageConsumerService : IHostedService
    {
        private readonly IRequestClient<IPublishMyMessage> _requestClient;

        public MessageConsumerService(IRequestClient<IPublishMyMessage> requestClient)
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