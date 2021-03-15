namespace RuanMou.Projects.Cores.Middleware.Urls
{
    /// <summary>
    /// 动态中台url
    /// </summary>
    public interface IDynamicMiddleUrl
    {
        /// <summary>
        ///  获取中台Url
        /// </summary>
        /// <param name="urlShcme">中台url</param>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        public string GetMiddleUrl(string urlShcme, string serviceName);
    }
}
