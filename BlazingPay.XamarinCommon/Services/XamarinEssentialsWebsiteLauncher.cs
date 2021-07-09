using System;
using System.Threading.Tasks;
using BlazingPay.Abstractions;
using BlazingPay.Abstractions.Contracts;
using Xamarin.Essentials;

namespace BlazingPay.XamarinCommon.Services
{
    public class XamarinEssentialsWebsiteLauncher : IWebsiteLauncher
    {
        public async Task Launch(Uri uri)
        {
            await Browser.OpenAsync(uri);
        }
    }
}