using System.Threading.Tasks;
using BlazorTransitionableRoute;
using BlazingPay.Abstractions.Contracts;
using BlazingPay.Abstractions.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace BlazingPay.UI.Services
{
    public static class UIServiceExtensions
    {
        public static IServiceCollection AddUIServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<StackService>();
            serviceCollection.AddSingleton<UIStateService>();
            serviceCollection.AddScoped<UIPreferencesAccessor>();
            serviceCollection.AddScoped<InstanceRepository>();
            serviceCollection.AddScoped<ThemeSwitcher>();
            serviceCollection.AddSingleton<IUIStateService>(provider => provider.GetService<UIStateService>());
            serviceCollection.AddScoped<IRouteTransitionInvoker, DefaultRouteTransitionInvoker>();

            return serviceCollection;
        }

        public static async Task NavigateBack(this NavigationManager navigationManager, IJSRuntime runtime,
            string route)
        {
            await runtime.InvokeVoidAsync("dispatchEventWrapper", "back");
            navigationManager.NavigateTo(route);
        }
    }
}