namespace RuanMou.Projects.Cores.Cluster.Options
{
    /// <summary>
    /// 负载均衡选项
    /// </summary>
    public class LoadBalanceOptions
    {
        public LoadBalanceOptions()
        {
            this.Type = "Random";
        }

        /// <summary>
        /// 负载均衡类型
        /// </summary>
        public string Type { set; get; }
    }
}
