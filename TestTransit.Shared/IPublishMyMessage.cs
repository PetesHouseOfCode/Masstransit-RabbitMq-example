using System;

namespace TestTransit.Shared
{

    public interface IPublishMyMessage
    {
        Guid Id { get; }
        string Name { get; }
    }
}