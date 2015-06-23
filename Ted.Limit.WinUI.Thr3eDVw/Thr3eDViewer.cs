using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Controls;

using Ted.Limit.Common;
using Ted.Limit.Core;
using Ted.Limit.ExtLoader;
using Ted.Limit.WinUI;

namespace Ted.Limit.WinUI.Thr3eDVw
{
    public partial class Thr3eDViewer : MainFrame , IApplication , IApplicationAsyn , IApplication3D
    {
        private ESRI.ArcGIS.SceneControl.AxSceneControl m_sceneCtrl;

        public Thr3eDViewer()
            : base()
        {
            m_sceneCtrl = new ESRI.ArcGIS.SceneControl.AxSceneControl();
            base.AddMainContent(m_sceneCtrl);
            IExtManager extMgr = new ExtManager();
            extMgr.Application = this;
            base.ExtensionManager = extMgr;
            UIHelper.Application = this;
        }

        #region scene event

        private void AddSceneEvent()
        {
            m_sceneCtrl.OnMouseDown += new ESRI.ArcGIS.SceneControl.ISceneControlEvents_OnMouseDownEventHandler(SceneCtrl_OnMouseDown);
            m_sceneCtrl.OnMouseMove += new ESRI.ArcGIS.SceneControl.ISceneControlEvents_OnMouseMoveEventHandler(SceneCtrl_OnMouseMove);
            m_sceneCtrl.OnMouseUp += new ESRI.ArcGIS.SceneControl.ISceneControlEvents_OnMouseUpEventHandler(SceneCtrl_OnMouseUp);
            m_sceneCtrl.OnKeyDown += new ESRI.ArcGIS.SceneControl.ISceneControlEvents_OnKeyDownEventHandler(SceneCtrl_OnKeyDown);
            m_sceneCtrl.OnKeyUp += new ESRI.ArcGIS.SceneControl.ISceneControlEvents_OnKeyUpEventHandler(SceneCtrl_OnKeyUp);
            m_sceneCtrl.OnDoubleClick += new ESRI.ArcGIS.SceneControl.ISceneControlEvents_OnDoubleClickEventHandler(SceneCtrl_DblClick);
        }

        void SceneCtrl_DblClick(object sender, ESRI.ArcGIS.SceneControl.ISceneControlEvents_OnDoubleClickEvent e)
        {
            if (OnSceneDblClick != null)
            {
                OnSceneDblClick();
            }
        }

        void SceneCtrl_OnKeyUp(object sender, ESRI.ArcGIS.SceneControl.ISceneControlEvents_OnKeyUpEvent e)
        {
            if (OnSceneKeyUp != null)
            {
                OnSceneKeyUp(e.keyCode, e.shift);
            }
        }

        void SceneCtrl_OnKeyDown(object sender, ESRI.ArcGIS.SceneControl.ISceneControlEvents_OnKeyDownEvent e)
        {
            if (OnSceneKeyDown != null)
            {
                OnSceneKeyDown(e.keyCode, e.shift);
            }
        }

        void SceneCtrl_OnMouseUp(object sender, ESRI.ArcGIS.SceneControl.ISceneControlEvents_OnMouseUpEvent e)
        {
            if (OnSceneMouseUp != null)
            {
                OnSceneMouseUp(e.button, e.shift, e.x, e.y);
            }
        }

        void SceneCtrl_OnMouseMove(object sender, ESRI.ArcGIS.SceneControl.ISceneControlEvents_OnMouseMoveEvent e)
        {
            if (OnSceneMouseMove != null)
            {
                OnSceneMouseMove(e.button, e.shift, e.x, e.y);
            }
        }

        void SceneCtrl_OnMouseDown(object sender, ESRI.ArcGIS.SceneControl.ISceneControlEvents_OnMouseDownEvent e)
        {
            if (OnSceneMouseDown != null)
            {
                OnSceneMouseDown(e.button, e.shift, e.x, e.y);
            }
        }

        private event MainFrame.ToolMouseAction
            OnSceneMouseDown,
            OnSceneMouseMove,
            OnSceneMouseUp;

        private event MainFrame.ToolKeyAction
            OnSceneKeyDown,
            OnSceneKeyUp;

        private event MainFrame.DblClick OnSceneDblClick;

        protected override void AddMouseDown(MainFrame.ToolMouseAction mouseDown)
        {
            OnSceneMouseDown += mouseDown;
        }

        protected override void AddMouseMove(MainFrame.ToolMouseAction mouseMove)
        {
            OnSceneMouseMove += mouseMove;
        }

        protected override void AddMouseUp(MainFrame.ToolMouseAction mouseUp)
        {
            OnSceneMouseUp += mouseUp;
        }

        protected override void AddDbClick(MainFrame.DblClick dblClick)
        {
            OnSceneDblClick += dblClick;
        }

        protected override void AddKeyDown(MainFrame.ToolKeyAction keyDown)
        {
            OnSceneKeyDown += keyDown;
        }

        protected override void AddKeyUp(MainFrame.ToolKeyAction keyUp)
        {
            OnSceneKeyUp += keyUp;
        }

        protected override void RemoveMouseDown(MainFrame.ToolMouseAction mouseDown)
        {
            OnSceneMouseDown -= mouseDown;
        }

        protected override void RemoveMouseMove(MainFrame.ToolMouseAction mouseMove)
        {
            OnSceneMouseMove -= mouseMove;
        }

        protected override void RemoveMouseUp(MainFrame.ToolMouseAction mouseUp)
        {
            OnSceneMouseUp -= mouseUp;
        }

        protected override void RemoveDbClick(MainFrame.DblClick dblClick)
        {
            OnSceneDblClick -= dblClick;
        }

        protected override void RemoveKeyDown(MainFrame.ToolKeyAction keyDown)
        {
            OnSceneKeyDown -= keyDown;
        }

        protected override void RemoveKeyUp(MainFrame.ToolKeyAction keyUp)
        {
            OnSceneKeyUp -= keyUp;
        }

        #endregion //scene event

        #region IApplication 成员

        EventHandler IApplication.OnSelectionChanged
        {
            set { throw new Exception("The method or operation is not implemented."); }
        }

        ESRI.ArcGIS.Controls.ITOCBuddy IApplication.TOCBuddy
        {
            get { return m_sceneCtrl.Object as ITOCBuddy; }
        }

        ESRI.ArcGIS.Controls.IToolbarBuddy IApplication.ToolbarBuddy
        {
            get { return m_sceneCtrl.Object as IToolbarBuddy; }
        }

        ESRI.ArcGIS.Controls.IMapControlDefault IApplication.MapControl
        {
            get { return null; }
        }

        ESRI.ArcGIS.Carto.IMap IApplication.FocusMap
        {
            get { return null; }
        }

        ESRI.ArcGIS.Carto.IActiveView IApplication.ActiveView
        {
            get { return null; }
        }

        string IApplication.StartupPath
        {
            get { return Application.StartupPath; }
        }

        string IApplication.ResourcePath(string toolKey)
        {
            return ExtensionManager.GetResource(toolKey);
        }

        void IApplication.SetStatus(string status)
        {
            ActionStatus = status;
        }

        void IApplication.SetStatus(string status, int progress)
        {
            ActionStatus = status;
            Progress = progress;
        }

        void IApplication.AddDockableWindow(IDockableWindow dckWin)
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

        void IApplication.Message(string msg)
        {

        }

        private PorpertyChanged m_pcHandler;

        PorpertyChanged IApplication.PCHandler
        {
            get
            {
                if (m_pcHandler == null)
                {
                    m_pcHandler = new PorpertyChanged(base.OnToolPorpertyChanged);
                }
                return m_pcHandler;
            }
        }

        #endregion

        #region IApplicationAsyn 成员

        void IApplicationAsyn.SetStatusAsyn(string status)
        {
            FormExtension.UIThread(this, delegate
            {
                ActionStatus = status;
            });
        }

        void IApplicationAsyn.SetStatusAsyn(string status, int progress)
        {
            FormExtension.UIThread(this, delegate
            {
                ActionStatus = status;
                Progress = progress;
            });
        }

        void IApplicationAsyn.AddDockableWindow(IDockableWindow dckWin)
        {
            FormExtension.UIThread(this, delegate
            {
                base.AddDockableWindow(dckWin);
            });
        }

        #endregion

        #region IApplication3D 成员

        private ISceneControlDefault m_sceneCtrlDefault;

        IScene IApplication3D.Scene
        {
            get
            {
                return ((IApplication3D)this).SceneControl.Scene;
            }
        }

        ESRI.ArcGIS.Controls.ISceneControlDefault IApplication3D.SceneControl
        {
            get
            {
                if (m_sceneCtrlDefault == null)
                {
                    m_sceneCtrlDefault = m_sceneCtrl.Object as ISceneControlDefault;
                }
                return m_sceneCtrlDefault;
            }
        }

        #endregion
    }
}