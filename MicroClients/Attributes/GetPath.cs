using System;

namespace RuanMou.Projects.Cores.Proxy.Attributes
{
    /// <summary>
    /// Get方法请求
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class GetPath : Attribute
    {
        // 请求路径
        public string Path { get; } // path/
        public GetPath(string Path)
        {
            this.Path = Path;
        }
    }
}
