using System;

namespace TestTransit.Shared
{
    public interface IQueryCommand
    {
        Guid Id { get; }

        string Name { get; }
    }
}