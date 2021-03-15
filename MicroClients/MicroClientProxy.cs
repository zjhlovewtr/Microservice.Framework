using Castle.DynamicProxy;
using Newtonsoft.Json;
using RuanMou.Projects.Commons.Exceptions;
using RuanMou.Projects.Cores.Middleware;
using RuanMou.Projects.Cores.Proxy.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace RuanMou.Projects.Cores.Proxy
{
    /// <summary>
    /// 微服务客户端代理
    /// </summary>
    public class MicroClientProxy : IInterceptor
    {
        private readonly IDynamicMiddleService middleService;

        public MicroClientProxy(IDynamicMiddleService middleService)
        {
            this.middleService = middleService;
        }

        /// <summary>
        /// 客户端代理执行
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            // 1、获取接口方法
            MethodInfo methodInfo = invocation.Method;

            // 2、获取方法上特性
            IEnumerable<Attribute> attributes = methodInfo.GetCustomAttributes();

            // 3、遍历
            foreach (var attribute in attributes)
            {
                // 1、获取Url
                Type type = invocation.Method.DeclaringType;
                MicroClient microClient = type.GetCustomAttribute<MicroClient>();
                if (microClient == null)
                {
                    throw new FrameException($"MicroClient 特性不能为空");
                }

                // 2、转换成动态参数
                ProxyMethodParameter proxyMethodParameter = new ProxyMethodParameter(methodInfo.GetParameters(), invocation.Arguments);
                dynamic paramPairs = ArgumentsConvert(proxyMethodParameter);

                if (attribute is GetPath getPath)
                {
                    // 3、路径变量替换
                    string path = PathParse(getPath.Path, paramPairs);

                    // 4、Get请求
                    dynamic result = middleService.GetDynamic(microClient.UrlShcme, microClient.ServiceName, path, paramPairs);

                    // 5、获取返回值类型
                    Type returnType = methodInfo.ReturnType;

                    // 8、赋值给返回值
                    invocation.ReturnValue = ResultConvert(result, returnType);

                }
                else if (attribute is PostPath postPath)
                {
                    // 3、路径变量替换
                    string path = PathParse(postPath.Path, paramPairs);

                    // 4、执行
                    dynamic result = middleService.PostDynamic(microClient.UrlShcme, microClient.ServiceName, path, paramPairs);

                    // 5、获取返回值类型
                    Type returnType = methodInfo.ReturnType;

                    // 8、赋值给返回值
                    invocation.ReturnValue = ResultConvert(result, returnType);
                }
                else if (attribute is PutPath putPath)
                {
                    // 3、路径变量替换
                    string path = PathParse(putPath.Path, paramPairs);

                    // 4、执行
                    dynamic result = middleService.PutDynamic(microClient.UrlShcme, microClient.ServiceName, path, paramPairs);

                    // 5、获取返回值类型
                    Type returnType = methodInfo.ReturnType;

                    // 8、赋值给返回值
                    invocation.ReturnValue = ResultConvert(result, returnType);
                }
                else if (attribute is DeletePath deletePath)
                {
                    // 3、路径变量替换
                    string path = PathParse(deletePath.Path, paramPairs);

                    // 4、执行
                    dynamic result = middleService.DeleteDynamic(microClient.UrlShcme, microClient.ServiceName, path, paramPairs);

                    // 5、获取返回值类型
                    Type returnType = methodInfo.ReturnType;

                    // 8、赋值给返回值
                    invocation.ReturnValue = ResultConvert(result, returnType);
                }
                else
                {
                    throw new FrameException($"方法特性不存在");
                }

            }
        }


        /// <summary>
        /// 结果转换
        /// </summary>
        /// <param name="result"></param>
        /// <param name="convertType"></param>
        /// <returns></returns>
        private dynamic ResultConvert(dynamic result, Type convertType)
        {
            // 1、判断是否为void
            if (convertType == typeof(void))
            {
                return null;
            }
            // 6、先序列化json
            string resultJson = JsonConvert.SerializeObject(result);

            // 7、再反序列成需要的对象
            dynamic returnResult = JsonConvert.DeserializeObject(resultJson, convertType);

            return returnResult;
        }

        /// <summary>
        /// 参数转换为动态类型
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        private dynamic ArgumentsConvert(ProxyMethodParameter proxyMethodParameter)
        {
            // 动态参数
            dynamic dynamicParams = new Dictionary<string, object>();

            // 多个参数情况包装成字典
            IDictionary<string, object> paramPairs = new Dictionary<string, object>();
            foreach (var parameterInfo in proxyMethodParameter.parameterInfos)
            {
                object parameterValue = proxyMethodParameter.Arguments[parameterInfo.Position];
                Type parameterType = parameterInfo.ParameterType;

                // 1、是否一个参数
                if (proxyMethodParameter.Arguments.Length == 1)
                {
                    // 1.1 如果是值类型
                    if (parameterType.IsValueType)
                    {
                        PathVariable pathVariable = parameterInfo.GetCustomAttribute<PathVariable>();
                        if (pathVariable != null)
                        {
                            // 1.1.2 设置路径变量名称
                            paramPairs.Add(pathVariable.Name, proxyMethodParameter.Arguments[parameterInfo.Position]);
                        }
                        else
                        {
                            paramPairs.Add(parameterInfo.Name, proxyMethodParameter.Arguments[parameterInfo.Position]);
                        }

                        // 1.1.3 设置为动态返回
                        dynamicParams = paramPairs;
                    }
                    else
                    {
                        // 1.2 如果是引用类型，直接动态返回
                        dynamicParams = parameterValue;
                    }

                }
                else
                {
                    // 2、判断是否有两个两个以上(全部用字典组装起来)
                    PathVariable pathVariable = parameterInfo.GetCustomAttribute<PathVariable>();
                    if (pathVariable != null)
                    {
                        // 2.1 设置路径变量名称
                        paramPairs.Add(pathVariable.Name, proxyMethodParameter.Arguments[parameterInfo.Position]);
                    }
                    else
                    {
                        paramPairs.Add(parameterInfo.Name, proxyMethodParameter.Arguments[parameterInfo.Position]);
                    }
                    // 1.1.3 设置为动态返回
                    dynamicParams = paramPairs;
                }
            }

            return dynamicParams;
        }

        /// <summary>
        /// 路径变量解析
        /// </summary>
        /// <param name="path"></param>
        /// <param name="paramPairs"></param>
        /// <returns></returns>
        private string PathParse(string path, dynamic paramPairs)
        {
            // 1、判断为字典进行路径解析
            if (paramPairs is IDictionary<string, object>)
            {
                // 1、路径前缀
                string PathPrefi = "{";
                // 2、路径后缀
                string PathSuffix = "}";

                foreach (var key in paramPairs.Keys)
                {
                    string pathvariable = PathPrefi + key + PathSuffix;
                    if (path.Contains(pathvariable))
                    {
                        path = path.Replace(pathvariable, Convert.ToString(paramPairs[key]));
                    }
                };
            }
            return path;
        }


    }
    class ProxyMethodParameter
    {
        // 参数类型
        public ParameterInfo[] parameterInfos { get; }
        // 参数值
        public object[] Arguments { get; }

        public ProxyMethodParameter(ParameterInfo[] parameterInfos, object[] arguments)
        {
            this.parameterInfos = parameterInfos;
            this.Arguments = arguments;
        }
    }
}
