using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.Core
{
    /// <summary>
    /// Tab分组.Tab分组上显示工具的大图标.
    /// </summary>
    public interface ITab
    {
        /// <summary>
        /// 系统唯一标识
        /// </summary>
        string Key { get;}

        /// <summary>
        /// 显示名称
        /// </summary>
        string Name { get;}

        /// <summary>
        /// 工具组.
        /// </summary>
        IGroup[] Groups { get;}
    }
}
