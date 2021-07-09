using System;

namespace BlazingPay.Abstractions.Contracts
{
    public interface ICounterState
    {
        int CurrentCount { get; }
        void IncrementCount();
        event Action StateChanged;
    }
}