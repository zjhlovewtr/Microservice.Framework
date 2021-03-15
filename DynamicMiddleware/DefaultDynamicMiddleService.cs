using RuanMou.Projects.Commons.Exceptions;
using RuanMou.Projects.Cores.Middleware.transports;
using RuanMou.Projects.Cores.Middleware.Urls;
using RuanMou.Projects.Cores.Utils;
using System.Collections.Generic;

namespace RuanMou.Projects.Cores.Middleware
{
    /// <summary>
    /// 动态中台服务，从注册中心动态获取服务
    /// </summary>
    public class DefaultDynamicMiddleService : IDynamicMiddleService
    {
        private readonly IMiddleService middleService; // 中台组件
        private readonly IDynamicMiddleUrl middleUrl; // 动态url组件

        public DefaultDynamicMiddleService(IMiddleService middleService, IDynamicMiddleUrl middleUrl)
        {
            this.middleService = middleService;
            this.middleUrl = middleUrl;
        }

        public void Delete(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
        {
            // 1、获取中台url  https://localhost:5001   servicenode localhost:5001
            string url = middleUrl.GetMiddleUrl(urlShcme, serviceName);

            // 2、请求
            MiddleResult middleResult = middleService.Delete(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);
        }

        public IList<T> GetList<T>(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
            where T : new()
        {
            // 1、获取中台url
            string url = middleUrl.GetMiddleUrl(urlShcme, serviceName);

            // 2、请求
            MiddleResult middleResult = middleService.Get(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);

            // 4、结果进行转换对象
            return ConvertUtil.MiddleResultToList<T>(middleResult);
        }

        public T Get<T>(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
            where T : new()
        {
            // 1、获取中台url
            string url = middleUrl.GetMiddleUrl(urlShcme, serviceName);

            // 2、请求
            MiddleResult middleResult = middleService.Get(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);

            // 4、结果进行转换对象
            return ConvertUtil.MiddleResultToObject<T>(middleResult);
        }

        public void Post(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
        {
            // 1、获取中台url
            string url = middleUrl.GetMiddleUrl(urlShcme, serviceName);

            // 2、请求
            MiddleResult middleResult = middleService.Post(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);
        }

        public dynamic PostDynamic(string urlShcme, string serviceName, string serviceLink, dynamic middleParam)
        {
            // 1、获取中台url
            string url = middleUrl.GetMiddleUrl(urlShcme, serviceName);

            // 2、请求
            MiddleResult middleResult = middleService.PostDynamic(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);

            return middleResult.Result;
        }

        public void Post(string urlShcme, string serviceName, string serviceLink, IList<IDictionary<string, object>> middleParams)
        {
            // 1、获取中台url
            string url = middleUrl.GetMiddleUrl(urlShcme, serviceName);

            // 2、请求
            MiddleResult middleResult = middleService.Post(url + serviceLink, middleParams);

            // 3、判断是否成功
            IsSuccess(middleResult);
        }

        public void Put(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
        {
            // 1、获取中台url
            string url = middleUrl.GetMiddleUrl(urlShcme, serviceName);

            // 2、请求
            MiddleResult middleResult = middleService.Put(url + serviceLink, middleParam);
            // 3、判断是否成功
            IsSuccess(middleResult);
        }

        public dynamic PutDynamic(string urlShcme, string serviceName, string serviceLink, dynamic middleParam)
        {
            // 1、获取中台url
            string url = middleUrl.GetMiddleUrl(urlShcme, serviceName);

            // 2、请求
            MiddleResult middleResult = middleService.PutDynamic(url + serviceLink, middleParam);
            // 3、判断是否成功
            IsSuccess(middleResult);

            return middleResult.Result;
        }

        public void Put(string urlShcme, string serviceName, string serviceLink, IList<IDictionary<string, object>> middleParams)
        {
            // 1、获取中台url
            string url = middleUrl.GetMiddleUrl(urlShcme, serviceName);

            // 2、请求
            MiddleResult middleResult = middleService.Put(url + serviceLink, middleParams);
            // 3、判断是否成功
            IsSuccess(middleResult);
        }

        private void IsSuccess(MiddleResult middleResult)
        {
            if (!middleResult.ErrorNo.Equals("0"))
            {
                throw new FrameException(middleResult.ErrorNo, middleResult.ErrorInfo);
            }
        }

        public IList<IDictionary<string, object>> GetList(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
        {
            // 1、获取中台url
            string url = middleUrl.GetMiddleUrl(urlShcme, serviceName);

            // 2、请求
            MiddleResult middleResult = middleService.Get(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);

            return middleResult.resultList;
        }

        public IDictionary<string, object> Get(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
        {
            // 1、获取中台url
            string url = middleUrl.GetMiddleUrl(urlShcme, serviceName);

            // 2、请求
            MiddleResult middleResult = middleService.Get(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);

            return middleResult.resultDic;
        }

        /// <summary>
        /// 返回动态结果
        /// </summary>
        /// <param name="urlShcme"></param>
        /// <param name="serviceName"></param>
        /// <param name="serviceLink"></param>
        /// <param name="middleParam"></param>
        /// <returns></returns>
        public dynamic GetDynamic(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
        {
            // 1、获取中台url
            string url = middleUrl.GetMiddleUrl(urlShcme, serviceName);

            // 2、请求
            MiddleResult middleResult = middleService.Get(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);

            return middleResult.Result;
        }

        public dynamic DeleteDynamic(string urlShcme, string serviceName, string serviceLink, IDictionary<string, object> middleParam)
        {
            // 1、获取中台url
            string url = middleUrl.GetMiddleUrl(urlShcme, serviceName);

            // 2、请求
            MiddleResult middleResult = middleService.Delete(url + serviceLink, middleParam);

            // 3、判断是否成功
            IsSuccess(middleResult);

            return middleResult.Result;
        }
    }
}
