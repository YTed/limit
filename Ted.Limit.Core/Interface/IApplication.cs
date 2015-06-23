using System;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;

namespace Ted.Limit.Core
{
    public interface IApplication
    {
        #region ArcEngine components

        EventHandler OnSelectionChanged { set;}

        ITOCBuddy TOCBuddy { get;}

        IToolbarBuddy ToolbarBuddy { get;}

        IMapControlDefault MapControl { get;}

        IMap FocusMap { get;}

        IActiveView ActiveView { get;}

        #endregion

        #region Application path

        string StartupPath { get;}

        string ResourcePath(string toolKey);

        #endregion

        #region status setting

        void SetStatus(string status);

        void SetStatus(string status, int progress);

        #endregion

        #region Dockable Window

        /// <summary>
        /// 向宿主程序添加指定的可停靠窗口
        /// </summary>
        /// <param name="dckWin"></param>
        void AddDockableWindow(IDockableWindow dckWin);

        /// <summary>
        /// 显示宿主程序中指定的可停靠窗口
        /// </summary>
        /// <param name="dckWin"></param>
        void ShowDockableWindow(IDockableWindow dckWin);

        /// <summary>
        /// 隐藏宿主程序中指定的可停靠窗口
        /// </summary>
        /// <param name="dckWin"></param>
        void HideDockableWindow(IDockableWindow dckWin);

        #endregion

        #region inner form

        void ShowInnerForm(IInnerForm child);

        void CloseInnerForm(IInnerForm child);

        void HideInnerForm(IInnerForm child);

        #endregion

        #region message & handler

        void Message(string msg);

        PorpertyChanged PCHandler { get;}

        #endregion
    }
}
