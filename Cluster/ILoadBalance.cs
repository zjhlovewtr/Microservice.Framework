using RuanMou.Projects.Cores.Registry;
using System.Collections.Generic;

namespace RuanMou.Projects.Cores.Cluster
{
    /// <summary>
    /// 服务负载均衡
    /// </summary>
    public interface ILoadBalance
    {
        /// <summary>
        /// 服务选择
        /// </summary>
        /// <param name="serviceUrls"></param>
        /// <returns></returns>
        ServiceNode Select(IList<ServiceNode> serviceUrls);
    }
}
