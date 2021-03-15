using Microsoft.Extensions.DependencyInjection;
using RuanMou.Projects.Cores.Registry.Options;
using System;

namespace RuanMou.Projects.Cores.Registry.Extentions
{
    /// <summary>
    ///  服务发现IOC容器扩展
    /// </summary>
    public static class ServiceDiscoveryServiceCollectionExtensions
    {
        // consul服务发现
        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services)
        {
            // 1、注册consul服务发现
            AddServiceDiscovery(services, options => { });
            return services;
        }

        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services,
                                                      Action<ServiceDiscoveryOptions> options)
        {
            // 2、注册到IOC容器
            services.Configure<ServiceDiscoveryOptions>(options);

            // 3、注册consul服务发现
            services.AddSingleton<IServiceDiscovery, ConsulServiceDiscovery>();
            return services;
        }

    }
}
