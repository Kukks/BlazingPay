using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BlazingPay.Abstractions.Contracts
{
    public interface IUIStateService
    {
        string Title { get; set; }
        bool AnyStackState { get; }
        Task InvokeStackState();
        Task PushStackState(Func<Task> act);
        Task ClearStack();
        string EntryData { get; set; }
        IFormCollection Form { get; set; }
    }
}