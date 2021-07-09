using System;
using System.Threading.Tasks;
using BlazingPay.Abstractions.Contracts;
using Microsoft.JSInterop;

namespace BlazingPay.WebCommon
{
    public class JsInteropWebsiteLauncher : IWebsiteLauncher
    {
        private readonly IJSRuntime _jsRuntime;

        public JsInteropWebsiteLauncher(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        public async Task Launch(Uri uri)
        {
            await _jsRuntime.InvokeAsync<object>("open", uri.ToString(), "_blank");
        }
    }
}