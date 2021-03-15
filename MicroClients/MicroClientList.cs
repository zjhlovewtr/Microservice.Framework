using System;
using System.Collections.Generic;

namespace RuanMou.Projects.Cores.Proxy
{
    /// <summary>
    /// 微服务客户端实例集合
    /// </summary>
    public class MicroClientList
    {
        public readonly MicroClientProxyFactory microClientProxyFactory;

        public MicroClientList(MicroClientProxyFactory microClientProxyFactory)
        {
            this.microClientProxyFactory = microClientProxyFactory;
        }

        /// <summary>
        /// 获取所有客户端实例
        /// </summary>
        /// <param name="assmalyName"></param>
        /// <returns></returns>
        public IDictionary<Type, object> GetClients(string assmalyName)
        {
            // 1、加载所有MicroClient特性类型
            IList<Type> types = AssemblyUtil.GetMicroClientTypesByAssembly(assmalyName);
            
            // 2、创建所有类型实例
            IDictionary<Type, object> keyValuePairs = new Dictionary<Type, object>();
            foreach (var type in types)
            {
                object value = microClientProxyFactory.CreateMicroClientProxy(type);
                keyValuePairs.Add(type, value);
            }
            return keyValuePairs;
        }
    }
}
