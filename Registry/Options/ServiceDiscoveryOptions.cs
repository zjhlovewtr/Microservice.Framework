namespace RuanMou.Projects.Cores.Registry.Options
{
    /// <summary>
    /// 服务发现选项
    /// </summary>
    public class ServiceDiscoveryOptions
    {
        public ServiceDiscoveryOptions()
        {
            // 默认地址
            this.DiscoveryAddress = "http://localhost:8500";
        }

        /// <summary>
        /// 服务发现地址
        /// </summary>
        public string DiscoveryAddress { set; get; }
    }
}
