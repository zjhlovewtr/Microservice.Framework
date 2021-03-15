using Newtonsoft.Json;
using RuanMou.Projects.Commons.Exceptions;
using RuanMou.Projects.Cores.Middleware.transports;
using RuanMou.Projects.Cores.Utils;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace RuanMou.Projects.Cores.Middleware.support
{
    /// <summary>
    /// Http中台请求结果
    /// </summary>
    public class HttpMiddleService : IMiddleService
    {
        private IHttpClientFactory httpClientFactory;
        private const string HttpConst = "mrico";

        public HttpMiddleService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public MiddleResult Delete(string middleUrl, IDictionary<string, object> middleParam)
        {
            // 1、获取httpClient
            HttpClient httpClient = httpClientFactory.CreateClient(HttpConst);

            // 2、Delete请求
            HttpResponseMessage httpResponseMessage = httpClient.DeleteAsync(middleUrl).Result;

            return GetMiddleResult(httpResponseMessage);
        }


        public MiddleResult Get(string middleUrl, IDictionary<string, object> middleParam)
        {
            // 1、获取httpClient
            HttpClient httpClient = httpClientFactory.CreateClient(HttpConst);

            // 2、参数转换为url方式
            string urlParam = HttpParamUtil.DicToHttpUrlParam(middleParam);

            // 3、Get请求
            HttpResponseMessage httpResponseMessage = httpClient.GetAsync(middleUrl + urlParam).Result;

            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult Post(string middleUrl, IDictionary<string, object> middleParam)
        {
            // 1、获取httpClient
            HttpClient httpClient = httpClientFactory.CreateClient(HttpConst);

            // 2、转换成json参数
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(middleParam), Encoding.UTF8, "application/json");

            // 3、Post请求
            HttpResponseMessage httpResponseMessage = httpClient.PostAsync(middleUrl, httpContent).Result;

            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult Post(string middleUrl, IList<IDictionary<string, object>> middleParams)
        {
            // 1、获取httpClient
            HttpClient httpClient = httpClientFactory.CreateClient(HttpConst);

            // 2、转换成json参数
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(middleParams), Encoding.UTF8, "application/json");

            // 3、Post请求
            HttpResponseMessage httpResponseMessage = httpClient.PostAsync(middleUrl, httpContent).Result;

            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult PostDynamic(string middleUrl, dynamic middleParam)
        {
            // 1、获取httpClient
            HttpClient httpClient = httpClientFactory.CreateClient(HttpConst);

            // 2、转换成json参数
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(middleParam), Encoding.UTF8, "application/json");

            // 3、Post请求
            HttpResponseMessage httpResponseMessage = httpClient.PostAsync(middleUrl, httpContent).Result;

            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult Put(string middleUrl, IDictionary<string, object> middleParam)
        {
            // 1、获取httpClient
            HttpClient httpClient = httpClientFactory.CreateClient(HttpConst);

            // 2、转换成json参数
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(middleParam), Encoding.UTF8, "application/json");
            // 3、Put请求
            HttpResponseMessage httpResponseMessage = httpClient.PutAsync(middleUrl, httpContent).Result;

            return GetMiddleResult(httpResponseMessage);
        }

        public MiddleResult Put(string middleUrl, IList<IDictionary<string, object>> middleParams)
        {
            // 1、获取httpClient
            HttpClient httpClient = httpClientFactory.CreateClient(HttpConst);

            // 2、转换成json参数
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(middleParams), Encoding.UTF8, "application/json");

            // 3、Put请求
            HttpResponseMessage httpResponseMessage = httpClient.PutAsync(middleUrl, httpContent).Result;

            return GetMiddleResult(httpResponseMessage);
        }


        public MiddleResult PutDynamic(string middleUrl, dynamic middleParam)
        {
            // 1、获取httpClient
            HttpClient httpClient = httpClientFactory.CreateClient(HttpConst);

            // 2、转换成json参数
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(middleParam), Encoding.UTF8, "application/json");
            // 3、Put请求
            HttpResponseMessage httpResponseMessage = httpClient.PutAsync(middleUrl, httpContent).Result;

            return GetMiddleResult(httpResponseMessage);
        }

        private MiddleResult GetMiddleResult(HttpResponseMessage httpResponseMessage)
        {
            // 3、将HttpResponseMessage转换成MiddleResult
            if (httpResponseMessage.StatusCode.Equals(HttpStatusCode.OK)
                || httpResponseMessage.StatusCode.Equals(HttpStatusCode.Created) ||
                httpResponseMessage.StatusCode.Equals(HttpStatusCode.Accepted))
            {
                string httpJsonString = httpResponseMessage.Content.ReadAsStringAsync().Result;

                // 3.1 创建MiddleResult
                return MiddleResult.JsonToMiddleResult(httpJsonString);
            }
            else
            {
                throw new FrameException($"{HttpConst}服务调用错误:{httpResponseMessage.Content.ReadAsStringAsync().ToString()}");
            }
        }
    }
}
