using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.Core
{
    /// <summary>
    /// 内部窗体.
    /// </summary>
    public interface IInnerForm
    {
        #region form properties

        /// <summary>
        /// 窗体标题
        /// </summary>
        string Title { get;}

        #endregion

        #region user's control

        /// <summary>
        /// 置于窗体内部的控件
        /// </summary>
        System.Windows.Forms.Control Content { get;}

        #endregion

        #region event handles

        /// <summary>
        /// 实体窗体Load事件发生时执行此函数
        /// </summary>
        void OnLoad();

        /// <summary>
        /// 实体窗体Closing事件发生时执行此函数
        /// </summary>
        /// <param name="cancel">是否要关闭实体窗体</param>
        void OnClosing(ref bool cancel);

        /// <summary>
        /// 实体窗体关闭时执行此函数
        /// </summary>
        /// <param name="reason">关闭的原因</param>
        void OnClosed(System.Windows.Forms.CloseReason reason);

        #endregion
    }
}
