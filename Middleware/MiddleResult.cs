using Newtonsoft.Json;
using System.Collections.Generic;

namespace RuanMou.Projects.Cores.Middleware
{
    /// <summary>
    /// 中台结果
    /// </summary>
    public class MiddleResult
    {
        public const string SUCCESS = "0";
        public string ErrorNo { set; get; } // 是否成功状态
        public string ErrorInfo { set; get; } // 失败信息
        public IDictionary<string, object> resultDic { set; get; }// 用于非结果集返回
        public IList<IDictionary<string, object>> resultList { set; get; }// 用于结果集返回

        public dynamic Result { set; get; }// 返回动态结果(通用)

        public MiddleResult()
        {
            resultDic = new Dictionary<string, object>();
            resultList = new List<IDictionary<string, object>>();
        }

        public MiddleResult(string jsonStr)
        {
            MiddleResult result = JsonConvert.DeserializeObject<MiddleResult>(jsonStr);
        }
        /// <summary>
        /// 中台结果串转换成为MiddleResult
        /// </summary>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static MiddleResult JsonToMiddleResult(string jsonStr)
        {
            MiddleResult result = JsonConvert.DeserializeObject<MiddleResult>(jsonStr);
            return result;
        }

        public MiddleResult(string errorNo, string errorInfo)
        {
            this.ErrorNo = errorNo;
            this.ErrorInfo = errorInfo;
            resultDic = new Dictionary<string, object>();
            resultList = new List<IDictionary<string, object>>();
        }

        public MiddleResult(string errorNo, string erroInfo, IDictionary<string, object> resultDic, IList<IDictionary<string, object>> resultList) : this(errorNo, erroInfo)
        {
            this.resultDic = resultDic;
            this.resultList = resultList;
            this.resultDic = resultDic;
            this.resultList = resultList;
        }
    }
}
