using Microsoft.Extensions.DependencyInjection;
using RuanMou.Projects.Cores.Cluster.Options;
using System;

namespace RuanMou.Projects.Cores.Cluster.Extentions
{
    /// <summary>
    /// 负载均衡ServiceCollection扩展
    /// </summary>
   public static class LoadBalanceServiceCollectionExtensions
    {
        /// <summary>
        /// 注册负载均衡
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddLoadBalance(this IServiceCollection services)
        {
            AddLoadBalance(services, options => { });
            return services;
        }

        /// <summary>
        /// 注册负载均衡
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddLoadBalance(this IServiceCollection services, Action<LoadBalanceOptions> options)
        {

            services.Configure<LoadBalanceOptions>(options);
            // 1、注册到IOC容器
            services.AddSingleton<ILoadBalance, RandomLoadBalance>();

            return services;
        }
    }
}
