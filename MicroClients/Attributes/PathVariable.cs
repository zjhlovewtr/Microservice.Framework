using System;

namespace RuanMou.Projects.Cores.Proxy.Attributes
{
    /// <summary>
    /// 路径变量特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class PathVariable : Attribute
    {
        public string Name { get; }

        public PathVariable(string name)
        {
            Name = name;
        }
    }
}
