using System;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Controls;

using Ted.Limit.Core;
using Ted.Limit.ExtLoader;
using Ted.Limit.Common;
using Ted.Limit.WinUI;
using System.Windows.Forms;

namespace Ted.Limit.WinUI.CampusGIS
{
    class CampusGIS : Ted.Limit.WinUI.MainFrame , Ted.Limit.Core.IApplication , Ted.Limit.Core.IApplicationAsyn
    {
        private ESRI.ArcGIS.Controls.AxMapControl m_axMapCtrl;

        public CampusGIS()
            : base()
        {
            eh_onSelectionChanged = new List<EventHandler>();
            m_axMapCtrl = new AxMapControl();
            base.AddMainContent(m_axMapCtrl);
            base.ExtensionManager = new ExtManager();
            base.ExtensionManager.Application = this;
            UIHelper.Application = this;

            AddMapEvents();

            this.OnNewToolActive += new NewToolActive(CampusGIS_OnNewToolActive);
            this.OnCurrentToolExit += new CurrentToolExit(CampusGIS_OnCurrentToolExit);
            this.FormClosed += new FormClosedEventHandler(Shutdown);
        }

        /// <summary>
        /// 因为我们是使用插件的方式调用AE的COM组件的,当系统关闭时,调入内存的COM
        /// 组件并没有被显式地释放.需要使用此方法,在系统关闭时释放所有的COM对象.
        /// 如果没有此方法,关闭系统时会提示某某指令调用的某某内存,其值不能为"Read"
        /// 云云的错误.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Shutdown(object sender, FormClosedEventArgs e)
        {
            ESRI.ArcGIS.ADF.COMSupport.AOUninitialize.Shutdown();
        }

        #region map event

        private void AddMapEvents()
        {
            m_axMapCtrl.OnMouseDown += new IMapControlEvents2_Ax_OnMouseDownEventHandler(MapCtrl_OnMouseDown);
            m_axMapCtrl.OnMouseUp += new IMapControlEvents2_Ax_OnMouseUpEventHandler(MapCtrl_OnMouseUp);
            m_axMapCtrl.OnMouseMove += new IMapControlEvents2_Ax_OnMouseMoveEventHandler(MapCtrl_OnMouseMove);
            m_axMapCtrl.OnDoubleClick += new IMapControlEvents2_Ax_OnDoubleClickEventHandler(MapCtrl_OnDoubleClick);
            m_axMapCtrl.OnKeyDown += new IMapControlEvents2_Ax_OnKeyDownEventHandler(MapCtrl_OnKeyDown);
            m_axMapCtrl.OnKeyUp += new IMapControlEvents2_Ax_OnKeyUpEventHandler(MapCtrl_OnKeyUp);

            m_axMapCtrl.OnMouseMove += new IMapControlEvents2_Ax_OnMouseMoveEventHandler(UpdateCoord_OnMouseMove);
            m_axMapCtrl.OnMouseMove += new IMapControlEvents2_Ax_OnMouseMoveEventHandler(UpdateScale_OnMouseMove);
            m_axMapCtrl.OnMapReplaced += new IMapControlEvents2_Ax_OnMapReplacedEventHandler(UpdateMapUnit_OnMapReplaced);
        }

        void UpdateScale_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            base.SetMapScale(m_axMapCtrl.MapScale);
        }

        void UpdateMapUnit_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            base.SetMapUnit(Global.esriUnitToString(m_axMapCtrl.MapUnits));
        }

        void UpdateCoord_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            base.SetCoordinate(e.mapX, e.mapY);
        }

        void MapCtrl_OnKeyUp(object sender, IMapControlEvents2_OnKeyUpEvent e)
        {
            if (OnMapKeyUp != null)
            {
                OnMapKeyUp(e.keyCode, e.shift);
            }
        }

        void MapCtrl_OnKeyDown(object sender, IMapControlEvents2_OnKeyDownEvent e)
        {
            if (OnMapKeyDown!= null)
            {
                OnMapKeyDown(e.keyCode, e.shift);
            }
        }

        void MapCtrl_OnDoubleClick(object sender, IMapControlEvents2_OnDoubleClickEvent e)
        {
            if (OnMapDblClick != null)
            {
                OnMapDblClick();
            }
        }

        void MapCtrl_OnMouseMove(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {
            if (OnMapMouseMove != null)
            {
                OnMapMouseMove(e.button, e.shift, e.x, e.y);
            }
        }

        void MapCtrl_OnMouseUp(object sender, IMapControlEvents2_OnMouseUpEvent e)
        {
            if (OnMapMouseUp != null)
            {
                OnMapMouseUp(e.button, e.shift, e.x, e.y);
            }
        }

        void MapCtrl_OnMouseDown(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            if (OnMapMouseDown != null)
            {
                OnMapMouseDown(e.button, e.shift, e.x, e.y);
            }
        }

        #endregion

        #region mousedown , up ,move etc...

        private event MainFrame.ToolMouseAction OnMapMouseDown;

        private event MainFrame.ToolMouseAction OnMapMouseUp;

        private event MainFrame.ToolMouseAction OnMapMouseMove;

        private event MainFrame.ToolKeyAction OnMapKeyDown;

        private event MainFrame.ToolKeyAction OnMapKeyUp;

        private event MainFrame.DblClick OnMapDblClick;

        protected override void AddMouseDown(MainFrame.ToolMouseAction mouseDown)
        {
            OnMapMouseDown += mouseDown;
        }

        protected override void AddMouseMove(MainFrame.ToolMouseAction mouseMove)
        {
            OnMapMouseMove += mouseMove;
        }

        protected override void AddMouseUp(MainFrame.ToolMouseAction mouseUp)
        {
            OnMapMouseUp += mouseUp;
        }

        protected override void AddDbClick(MainFrame.DblClick dblClick)
        {
            OnMapDblClick += dblClick;
        }

        protected override void AddKeyDown(MainFrame.ToolKeyAction keyDown)
        {
            OnMapKeyDown += keyDown;
        }

        protected override void AddKeyUp(MainFrame.ToolKeyAction keyUp)
        {
            OnMapKeyUp += keyUp;
        }

        protected override void RemoveMouseDown(MainFrame.ToolMouseAction mouseDown)
        {
            OnMapMouseDown -= mouseDown;
        }

        protected override void RemoveMouseMove(MainFrame.ToolMouseAction mouseMove)
        {
            OnMapMouseMove -= mouseMove;
        }

        protected override void RemoveMouseUp(MainFrame.ToolMouseAction mouseUp)
        {
            OnMapMouseUp -= mouseUp;
        }

        protected override void RemoveDbClick(MainFrame.DblClick dblClick)
        {
            OnMapDblClick -= dblClick;
        }

        protected override void RemoveKeyDown(MainFrame.ToolKeyAction keyDown)
        {
            OnMapKeyDown -= keyDown;
        }

        protected override void RemoveKeyUp(MainFrame.ToolKeyAction keyUp)
        {
            OnMapKeyUp -= keyUp;
        }

        #endregion

        #region IApplication 成员

        private IMapControlDefault iApp_MapCtrl;

        IMapControlDefault Ted.Limit.Core.IApplication.MapControl
        {
            get
            {
                if (iApp_MapCtrl == null)
                {
                    iApp_MapCtrl = m_axMapCtrl.Object as IMapControlDefault;
                }
                return iApp_MapCtrl;
            }
        }

        ESRI.ArcGIS.Carto.IMap Ted.Limit.Core.IApplication.FocusMap
        {
            get 
            {
                if (iApp_MapCtrl == null)
                {
                    return ((IApplication)this).MapControl.Map;
                }
                return iApp_MapCtrl.Map;
            }
        }

        ESRI.ArcGIS.Carto.IActiveView Ted.Limit.Core.IApplication.ActiveView
        {
            get
            {
                if (iApp_MapCtrl == null)
                {
                    return ((IApplication)this).MapControl.ActiveView;
                }
                return iApp_MapCtrl.ActiveView;
            }
        }

        string Ted.Limit.Core.IApplication.StartupPath
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath;
            }
        }

        string Ted.Limit.Core.IApplication.ResourcePath(string toolKey)
        {
            IExtManager ExtMgr = ExtensionManager;
            return ExtMgr.GetResource(toolKey);
        }

        void Ted.Limit.Core.IApplication.SetStatus(string status)
        {
            ActionStatus = status;
        }

        void Ted.Limit.Core.IApplication.SetStatus(string status, int progress)
        {
            ActionStatus = status;
            Progress = progress;
        }

        void Ted.Limit.Core.IApplication.AddDockableWindow(Ted.Limit.Core.IDockableWindow dckWin)
        {
            base.AddDockableWindow(dckWin);
        }

        void IApplication.ShowDockableWindow(IDockableWindow dckWin)
        {
            base.ShowDockableWindow(dckWin);
        }

        void IApplication.HideDockableWindow(IDockableWindow dckWin)
        {
            base.HideDockableWindow(dckWin);
        }

        void IApplication.Message(string msg)
        {

        }

        ITOCBuddy IApplication.TOCBuddy
        {
            get { return m_axMapCtrl.Object as ITOCBuddy; }
        }

        IToolbarBuddy IApplication.ToolbarBuddy
        {
            get { return m_axMapCtrl.Object as IToolbarBuddy; }
        }

        /// <summary>
        /// 获得属性改变事件委托
        /// </summary>
        PorpertyChanged IApplication.PCHandler
        {
            get { return new PorpertyChanged(base.OnToolPorpertyChanged); }
        }

        /// <summary>
        /// 取消工具时清除事件
        /// </summary>
        private void ClearExitingToolEvents()
        {
            //平衡方法:IApplication.OnSelectionChanged
            foreach (EventHandler handler in eh_onSelectionChanged)
            {
                m_axMapCtrl.OnSelectionChanged -= handler;
            }
            eh_onSelectionChanged.Clear();
        }

        private List<EventHandler> eh_onSelectionChanged;

        /// <summary>
        /// 添加地图选择事件控制
        /// </summary>
        EventHandler IApplication.OnSelectionChanged
        {
            set
            {
                if (value != null)
                {
                    eh_onSelectionChanged.Add(value);
                    m_axMapCtrl.OnSelectionChanged += value;
                }
            }
        }

        void IApplication.ShowInnerForm(IInnerForm child)
        {
            base.ShowInnerForm(child);
        }

        void IApplication.CloseInnerForm(IInnerForm child)
        {
            base.CloseInnerForm(child);
        }

        void IApplication.HideInnerForm(IInnerForm child)
        {
            base.HideInnerForm(child);
        }

        #endregion

        #region IApplicationAsyn 成员

        void IApplicationAsyn.SetStatusAsyn(string status)
        {
            FormExtension.UIThread(this, delegate
            {
                ((IApplication)this).SetStatus(status);
            });
        }

        void IApplicationAsyn.SetStatusAsyn(string status, int progress)
        {
            FormExtension.UIThread(this, delegate
            {
                ((IApplication)this).SetStatus(status, progress);
            });
        }

        void IApplicationAsyn.AddDockableWindow(IDockableWindow dckWin)
        {
            FormExtension.UIThread(this, delegate
            {
                ((IApplication)this).AddDockableWindow(dckWin);
            });
        }

        #endregion

        #region main frame event : OnNewToolActive , OnCurrentToolExit

        void CampusGIS_OnNewToolActive(IItem newTool)
        {

        }

        void CampusGIS_OnCurrentToolExit(IItem crrTool)
        {
            ClearExitingToolEvents();
        }

        #endregion
    }
}
