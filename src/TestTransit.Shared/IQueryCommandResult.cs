using System;

namespace TestTransit.Shared
{
    public interface IQueryCommandResult
    {
        Guid Id { get; }
        string Name { get; }
    }
}