using Consul;
using Microsoft.Extensions.Options;
using RuanMou.Projects.Commons.Exceptions;
using RuanMou.Projects.Cores.Registry.Options;
using System;
using System.Collections.Generic;
using System.Net;

namespace RuanMou.Projects.Cores.Registry
{
    /// <summary>
    /// consul服务发现实现
    /// </summary>
    public class ConsulServiceDiscovery : IServiceDiscovery
    {
        private readonly ServiceDiscoveryOptions serviceDiscoveryOptions;
        public ConsulServiceDiscovery(IOptions<ServiceDiscoveryOptions> options)
        {
            this.serviceDiscoveryOptions = options.Value;
        }

        public List<ServiceNode> Discovery(string serviceName)
        {
            // 1.2、从远程服务器取
            CatalogService[] queryResult = RemoteDiscovery(serviceName);

            var list = new List<ServiceNode>();
            foreach (var service in queryResult)
            {
                list.Add(new ServiceNode { Url = service.ServiceAddress + ":" + service.ServicePort });
            }

            return list;
        }

        private CatalogService[] RemoteDiscovery(string serviceName)
        {
            // 1、创建consul客户端连接
            var consulClient = new ConsulClient(configuration =>
            {
                //1.1 建立客户端和服务端连接
                configuration.Address = new Uri(serviceDiscoveryOptions.DiscoveryAddress);
            });

            // 2、consul查询服务,根据具体的服务名称查询
            var queryResult = consulClient.Catalog.Service(serviceName).Result;
            // 3、判断请求是否失败
            if (!queryResult.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new FrameException($"consul连接失败:{queryResult.StatusCode}");
            }

            return queryResult.Response;
        }
    }
}
