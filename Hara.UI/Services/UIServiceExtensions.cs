using BlazorTransitionableRoute;
using Hara.Abstractions.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Hara.UI.Services
{
    public static class UIServiceExtensions
    {
        public static IServiceCollection AddUIServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<UIStateService>();
            serviceCollection.AddSingleton<IUIStateService>(provider => provider.GetService<UIStateService>());
            serviceCollection.AddScoped<IRouteTransitionInvoker, DefaultRouteTransitionInvoker>();

            return serviceCollection;
        }
    }
}