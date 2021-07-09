using System;
using System.Threading.Tasks;

namespace BlazingPay.Abstractions.Contracts
{
    public interface IWebsiteLauncher
    {
        Task Launch(Uri uri);
    }
}