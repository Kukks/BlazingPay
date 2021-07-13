using BlazorTransitionableRoute;
using BlazingPay.Abstractions.Contracts;
using BlazingPay.Abstractions.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingPay.UI.Services
{
    public static class UIServiceExtensions
    {
        public static IServiceCollection AddUIServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<UIStateService>();
            serviceCollection.AddScoped(provider => provider.GetService<IConfigProvider>().Get<UIPreferences>("UIPreferences").Result);
            serviceCollection.AddScoped<InstanceRepository>();
            
            serviceCollection.AddSingleton<IUIStateService>(provider => provider.GetService<UIStateService>());
            serviceCollection.AddScoped<IRouteTransitionInvoker, DefaultRouteTransitionInvoker>();

            return serviceCollection;
        }
    }
}