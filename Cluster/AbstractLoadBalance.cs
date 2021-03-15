﻿using RuanMou.Projects.Cores.Registry;
using System;
using System.Collections.Generic;

namespace RuanMou.Projects.Cores.Cluster
{
    /// <summary>
    /// 负载均衡抽象实现
    /// </summary>
    public abstract class AbstractLoadBalance : ILoadBalance
    {
        static int CalculateWarmupWeight(int uptime, int warmup, int weight)
        {
            int ww = (int)((float)uptime / ((float)warmup / (float)weight));
            return ww < 1 ? 1 : (ww > weight ? weight : ww);
        }

        public ServiceNode Select(IList<ServiceNode> serviceUrls)
        {
            if (serviceUrls == null || serviceUrls.Count == 0)
                return null;
            if (serviceUrls.Count == 1)
                return serviceUrls[0];
            return DoSelect(serviceUrls);
        }

        /// <summary>
        /// 子类去实现
        /// </summary>
        /// <param name="serviceUrls"></param>
        /// <returns></returns>
        public abstract ServiceNode DoSelect(IList<ServiceNode> serviceUrls);

        /// <summary>
        /// 获取权重
        /// </summary>
        /// <returns></returns>
        protected int GetWeight()
        {
            int weight = 100;
            if (weight > 0)
            {
                long timestamp = 0L;
                if (timestamp > 0L)
                {
                    int uptime = (int)(DateTime.Now.ToFileTimeUtc() - timestamp);
                    int warmup = 10 * 60 * 1000;
                    if (uptime > 0 && uptime < warmup)
                    {
                        weight = CalculateWarmupWeight(uptime, warmup, weight);
                    }
                }
            }
            return weight;
        }
    }
}
