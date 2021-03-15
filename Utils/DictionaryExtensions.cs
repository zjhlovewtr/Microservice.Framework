using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace RuanMou.Projects.Cores.Utils
{
    /// <summary>
    /// 字典扩展
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 对象转换成字典
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IDictionary<string, object> AddObject(this IDictionary<string, object> dics, object value)
        {
            // 1、获取反射类型
            Type type = value.GetType();

            // 2、获取所有反射属性
            PropertyInfo[] propertyInfos = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // 3、遍历PropertyInfo
            foreach (PropertyInfo info in propertyInfos)
            {
                dics.Add(info.Name, Convert.ToString(info.GetValue(value)));
            }

            return dics;
        }


        /// <summary>
        /// 字典转换成字典
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IDictionary<string, object> AddDictionary(this IDictionary<string, object> dics, IDictionary dic)
        {
            foreach (var key in dic.Keys)
            {
                dics.Add(Convert.ToString(key), dic[key]);
            }
            return dics;
        }
    }
}
