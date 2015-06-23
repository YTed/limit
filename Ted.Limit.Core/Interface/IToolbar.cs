using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.Core
{
    /// <summary>
    /// Toolbar分组.IToolbar上显示工具的小图标
    /// </summary>
    public interface IToolbar
    {
        /// <summary>
        /// Get the name of the toolbar.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// the unqiue identify in system.
        /// </summary>
        string Key { get;}

        /// <summary>
        /// Get the groups in the toolbar.
        /// </summary>
        IGroup[] Groups { get;}
    }
}
