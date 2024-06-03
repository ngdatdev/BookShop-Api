using System.Linq;
using System.Reflection;
using BookShop.Application.Shared.Features;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Application.Shared.ServiceConfigs
{
    public static class MediatorConfig
    {
        public static IServiceCollection ConfigMediatorHandlers(this IServiceCollection services)
        {
            Assembly assembly = typeof(DependencyInjection).Assembly;
            var handlerTypes = assembly
                .GetTypes()
                .Where(t =>
                    !t.IsAbstract
                    && t.GetInterfaces()
                        .Any(i =>
                            i.IsGenericType
                            && i.GetGenericTypeDefinition() == typeof(IFeatureHandler<,>)
                        )
                );

            foreach (var handlerType in handlerTypes)
            {
                var handlerInterfaces = handlerType
                    .GetInterfaces()
                    .Where(i =>
                        i.IsGenericType
                        && i.GetGenericTypeDefinition() == typeof(IFeatureHandler<,>)
                    );

                foreach (var handlerInterface in handlerInterfaces)
                {
                    services.AddTransient(handlerInterface, handlerType);
                }
            }

            return services;
        }
    }
}
