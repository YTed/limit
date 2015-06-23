using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.Core
{
    /// <summary>
    /// 提示选择列表更新.
    /// </summary>
    public delegate void SelectorNotice();

    /// <summary>
    /// 选择
    /// </summary>
    public interface ISelector : IItem , IActive
    {
        /// <summary>
        /// 列表选择项改变回调事件
        /// </summary>
        /// <param name="idx">新选择项的索引</param>
        void ChangeSelection(int idx);

        /// <summary>
        /// 设置改变列表项的代理
        /// </summary>
        /// <param name="reset">代理</param>
        SelectorNotice Notice { set;}

        /// <summary>
        /// 获取列表项的值列表.
        /// </summary>
        object[] ValueList { get;}
    }
}
