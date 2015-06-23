using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.Core
{
    /// <summary>
    /// 制造接口定义
    /// </summary>
    public interface IMake
    {
        /// <summary>
        /// 模块在系统中的唯一标识
        /// </summary>
        string Key { get;}

        /// <summary>
        /// 创建工具条
        /// </summary>
        /// <returns></returns>
        IToolbar[] Toolbars { get;}

        /// <summary>
        /// 获取浮动窗口
        /// </summary>
        IDockableWindow[] DockableWindows { get;}

        /// <summary>
        /// 创建Tab
        /// </summary>
        /// <returns></returns>
        ITab[] Tabs { get;}

        /// <summary>
        /// 获取全部图形工具
        /// </summary>
        IItem[] Items { get;}

        /// <summary>
        /// 获取全部控制台工具
        /// </summary>
        IConsole[] Consoles { get;}
    }

    public interface IMakable
    {
        /// <summary>
        /// 获取组件的制造者.
        /// </summary>
        IMake Make { get;}
    }
}
