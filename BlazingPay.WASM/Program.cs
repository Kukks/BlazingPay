using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazor.Extensions;
using BlazingPay.Abstractions.Contracts;
using BlazingPay.Abstractions.Services;
using BlazingPay.UI;
using BlazingPay.UI.Services;
using BlazingPay.WASM.Services;
using BlazingPay.WebCommon;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingPay.WASM
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddUIServices();
            builder.Services.AddSingleton<ICounterState, CounterState>();
            builder.Services.AddSingleton<ILocalContentFetcher, HttpClientLocalContentFetcher>();
            builder.Services.AddSingleton<IWebsiteLauncher, JsInteropWebsiteLauncher>();
            builder.Services.AddSingleton<IWeatherForecastFetcher, WeatherForecastFetcher>();
            builder.Services.AddSingleton<IConfigProvider, JsInteropConfigProvider>();
            builder.Services.AddSingleton<ISecureConfigProvider, PasswordEncryptedJsInteropSecureConfigProvider>();
            builder.Services.AddScoped<INotificationManager, WebNotificationManager>();

            builder.Services.AddSingleton(
                sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

            builder.Services.AddNotifications();
            await builder.Build().RunAsync();
        }
    }
}