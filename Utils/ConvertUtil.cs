using RuanMou.Projects.Cores.Middleware;
using System;
using System.Collections.Generic;

namespace RuanMou.Projects.Cores.Utils
{
    /// <summary>
    /// 转换工具类
    /// </summary>
    public class ConvertUtil
    {

        /// <summary>
        /// 中台结果转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        static public T MiddleResultToObject<T>(MiddleResult middleResult) where T : new()
        {
            return DicToObject<T>(middleResult.resultDic);
        }

        /// <summary>
        /// 中台结果转换成集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        static public IList<T> MiddleResultToList<T>(MiddleResult middleResult) where T : new()
        {
            return ListToObject<T>(middleResult.resultList);
        }


        /// <summary>
        /// List集合转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        static public IList<T> ListToObject<T>(IList<IDictionary<string, object>> resultList) where T : new()
        {
            // 1、创建集合对象
            IList<T> lists = new List<T>();

            foreach (var list in resultList)
            {
                // 2、转换成泛型对象
                lists.Add(DicToObject<T>(list));
            }

            return lists;
        }

        /// <summary>
        /// List集合字典对象转换成集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        static public dynamic ListToObject(IList<IDictionary<string, object>> resultList, Type type)
        {
            // 1、创建List泛型类型并指定泛型
            Type listType = typeof(List<>).MakeGenericType(type);
            dynamic value = Activator.CreateInstance(listType);
            foreach (var list in resultList)
            {
                // 2、转换成泛型对象
                value.Add(DicToObject(list, type));
            }
            return value;
        }

        /// <summary>
        /// 字典转换成对象
        /// </summary>
        /// <typeparam name="Type"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        static public dynamic DicToObject(IDictionary<string, object> dic, Type type)
        {
            var entity = Activator.CreateInstance(type);
            var fields = type.GetProperties();
            string val = string.Empty;
            object obj = null;

            foreach (var field in fields)
            {
                if (!dic.ContainsKey(field.Name))
                    continue;
                val = Convert.ToString(dic[field.Name]);

                object defaultVal;
                if (field.PropertyType.Name.Equals("String"))
                    defaultVal = "";
                else if (field.PropertyType.Name.Equals("Boolean"))
                {
                    defaultVal = false;
                    val = (val.Equals("1") || val.Equals("on")).ToString();
                }
                else if (field.PropertyType.Name.Equals("Decimal"))
                    defaultVal = 0M;
                else
                    defaultVal = 0;

                if (!field.PropertyType.IsGenericType)
                    obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, field.PropertyType);
                else
                {
                    Type genericTypeDefinition = field.PropertyType.GetGenericTypeDefinition();
                    if (genericTypeDefinition == typeof(Nullable<>))
                        obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, Nullable.GetUnderlyingType(field.PropertyType));
                }

                field.SetValue(entity, obj, null);
            }

            return entity;
        }

        /// <summary>
        /// 字典转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        static public T DicToObject<T>(IDictionary<string, object> dic) where T : new()
        {
            Type myType = typeof(T);
            T entity = new T();
            var fields = myType.GetProperties();
            string val = string.Empty;
            object obj = null;

            foreach (var field in fields)
            {
                if (!dic.ContainsKey(field.Name))
                    continue;
                val = Convert.ToString(dic[field.Name]);

                object defaultVal;
                if (field.PropertyType.Name.Equals("String"))
                    defaultVal = "";
                else if (field.PropertyType.Name.Equals("Boolean"))
                {
                    defaultVal = false;
                    val = (val.Equals("1") || val.Equals("on")).ToString();
                }
                else if (field.PropertyType.Name.Equals("Decimal"))
                    defaultVal = 0M;
                else
                    defaultVal = 0;

                if (!field.PropertyType.IsGenericType)
                    obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, field.PropertyType);
                else
                {
                    Type genericTypeDefinition = field.PropertyType.GetGenericTypeDefinition();
                    if (genericTypeDefinition == typeof(Nullable<>))
                        obj = string.IsNullOrEmpty(val) ? defaultVal : Convert.ChangeType(val, Nullable.GetUnderlyingType(field.PropertyType));
                }

                field.SetValue(entity, obj, null);
            }

            return entity;
        }

    }
}
