using System.Collections.Generic;
using System.Text;

namespace RuanMou.Projects.Cores.Utils
{
    /// <summary>
    /// http参数工具类
    /// </summary>
    public class HttpParamUtil
    {
        /// <summary>
        /// 字典转换成为url参数?userid=1&useid=2
        /// </summary>
        /// <param name="middleParam"></param>
        /// <returns></returns>
        public static string DicToHttpUrlParam(IDictionary<string, object> middleParam)
        {
            if (middleParam.Count != 0)
            {
                // 1、拼接
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("?");
                foreach (var keyValue in middleParam)
                {
                    stringBuilder.Append(keyValue.Key);
                    stringBuilder.Append("=");
                    stringBuilder.Append(keyValue.Value);
                    stringBuilder.Append("&");
                }

                // 2、截取去掉最后一个&
                string urlParam = stringBuilder.ToString();
                urlParam = urlParam.Substring(0, urlParam.Length - 1);

                return urlParam;
            }
            return "";
        }
    }
}
