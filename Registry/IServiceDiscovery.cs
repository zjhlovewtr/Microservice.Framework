using System.Collections.Generic;
using System.Threading.Tasks;

namespace RuanMou.Projects.Cores.Registry
{
    /// <summary>
    /// 服务发现
    /// </summary>
    public interface IServiceDiscovery
    {
        /// <summary>
        /// 服务发现
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        List<ServiceNode> Discovery(string serviceName);
    }
}
