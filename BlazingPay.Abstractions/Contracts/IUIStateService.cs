using Microsoft.AspNetCore.Http;

namespace BlazingPay.Abstractions.Contracts
{
    public interface IUIStateService
    {
        string Title { get; set; }
        string EntryData { get; set; }
        IFormCollection Form { get; set; }
    }
}