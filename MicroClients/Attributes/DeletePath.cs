using System;

namespace RuanMou.Projects.Cores.Proxy.Attributes
{
    /// <summary>
    /// Delete请求特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DeletePath : Attribute
    {
        // 请求路径
        public string Path { get; }
        public DeletePath(string Path)
        {
            this.Path = Path;
        }
    }
}
