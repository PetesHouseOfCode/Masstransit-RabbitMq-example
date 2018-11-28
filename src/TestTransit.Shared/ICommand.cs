using System;

namespace TestTransit.Shared
{
    public interface ICommand
    {
        Guid Id { get; }

        string Name { get; }
    }
}