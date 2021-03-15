using System;

namespace RuanMou.Projects.Cores.Registry.Options
{
    /// <summary>
    /// 节点注册选项
    /// </summary>
    public class ServiceRegistryOptions
    {
        public ServiceRegistryOptions()
        {
            this.ServiceId = Guid.NewGuid().ToString();
            this.RegistryAddress = "http://localhost:8500";
            this.HealthCheckAddress = "/HealthCheck";
        }

        // 服务ID
        public string ServiceId { get; set; }

        // 服务名称
        public string ServiceName { get; set; }

        // 服务地址http://localhost:5001
        public string ServiceAddress { get; set; }

        // 服务标签(版本)
        public string[] ServiceTags { set; get; }

        /*// 服务地址(可以选填 === 默认加载启动路径(localhost))
        public string ServiceAddress { set; get; }

        // 服务端口号(可以选填 === 默认加载启动路径端口)
        public int ServicePort { set; get; }

        // Https 或者 http
        public string ServiceScheme { get; set; }*/

        // 服务注册地址
        public string RegistryAddress { get; set; }

        // 服务健康检查地址
        public string HealthCheckAddress { get; set; }
    }
}
