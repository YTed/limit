using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.Core
{
    /// <summary>
    /// 提供对应用程序窗体的安全的异步属性访问/方法调用
    /// </summary>
    public interface IApplicationAsyn
    {
        void SetStatusAsyn(string status);

        void SetStatusAsyn(string status, int progress);

        void AddDockableWindow(IDockableWindow dckWin);
    }
}
