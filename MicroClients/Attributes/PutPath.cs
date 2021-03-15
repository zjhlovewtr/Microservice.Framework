using System;

namespace RuanMou.Projects.Cores.Proxy.Attributes
{
    /// <summary>
    /// Put请求特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PutPath : Attribute
    {
        // 请求路径
        public string Path { get; }
        public PutPath(string Path)
        {
            this.Path = Path;
        }
    }
}
