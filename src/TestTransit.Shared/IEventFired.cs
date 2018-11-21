using System;

namespace TestTransit.Shared
{

    public interface IEventFired
    {
        Guid Id { get; }
        string Name { get; }
    }
}