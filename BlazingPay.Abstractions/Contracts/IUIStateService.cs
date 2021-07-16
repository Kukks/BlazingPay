using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace BlazingPay.Abstractions.Contracts
{
    public interface IUIStateService
    {
        string Title { get; set; }
        string EntryData { get; set; }
        Dictionary<string, StringValues> Form { get; set; }
    }
}