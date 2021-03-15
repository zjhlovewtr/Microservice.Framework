using System.Collections.Generic;

namespace RuanMou.Projects.Cores.Middleware
{
    /// <summary>
    /// 中台服务(服务交互)
    /// </summary>
    public interface IDynamicMiddleService
    {
        public IList<IDictionary<string, object>> GetList(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> middleParam);
        public IDictionary<string, object> Get(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> middleParam);
        public dynamic GetDynamic(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> middleParam);
        public IList<T> GetList<T>(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
            where T : new();

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public T Get<T>(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
            where T : new();

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public void Post(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> middleParam);

        /// <summary>
        /// Post请求，动态参数
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public dynamic PostDynamic(string urlShcme, string serviceName, string serviceLink, dynamic middleParam);

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParams">中台集合参数</param>
        /// <returns></returns>
        public void Post(string urlShcme, string serviceName, string serviceLink, IList<IDictionary<string, object>> middleParams);


        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public void Delete(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> middleParam);

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public dynamic DeleteDynamic(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> middleParam);

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public void Put(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> middleParam);

        /// <summary>
        /// Put请求，动态参数
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParam">中台参数</param>
        /// <returns></returns>
        public dynamic PutDynamic(string urlShcme, string serviceName, string serviceLink, dynamic middleParam);

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="middleUrl">中台链接</param>
        /// <param name="middleParams">中台参数</param>
        /// <returns></returns>
        public void Put(string urlShcme, string serviceName, string serviceLink, IList<IDictionary<string, object>> middleParams);

    }
}
