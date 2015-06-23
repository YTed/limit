using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.Core
{
    /// <summary>
    /// 可浮动窗口
    /// </summary>
    public interface IDockableWindow : IActive , IMakable
    {
        /// <summary>
        /// 用于显示的名称
        /// </summary>
        string Name { get;}

        /// <summary>
        /// 组件在系统的唯一标识
        /// </summary>
        string Key { get;}

        /// <summary>
        /// 显示在可浮动窗口上的控件.
        /// </summary>
        System.Windows.Forms.Control Content { get;}
    }
}
