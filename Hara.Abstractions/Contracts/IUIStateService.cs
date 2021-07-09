using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hara.Abstractions.Contracts
{
    public interface IUIStateService
    {
        string Title { get; set; }
        bool AnyStackState { get; }
        Task InvokeStackState();
        Task PushStackState(Func<Task> act);
        Task ClearStack();
    }
}