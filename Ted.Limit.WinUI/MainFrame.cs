using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using stdole;

using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinDock;
using Infragistics.Win.UltraWinToolbars;

using Ted.Limit.Common;
using Ted.Limit.Core;
using Ted.Limit.ExtLoader;

namespace Ted.Limit.WinUI
{
    public abstract partial class MainFrame : Form
    {
        private AppJudge m_judge;

        public MainFrame()
        {
            InitializeComponent();
            m_crrTool = new CurrentTool(this);
            m_dckWinPool = new Dictionary<string, Infragistics.Win.UltraWinDock.DockableWindow>();
            m_judge = new AppJudge();
            m_innerFormDict = new Dictionary<IInnerForm, Form>();
        }

        #region form event

        /// <summary>
        /// 保存设置
        /// </summary>
        private void SaveConfig()
        {
                ui_tbrMgr.SaveAsXml(DirectoryHelper.GetConfigFile(ConfiguraionFile.ToolbarManager), true);
                ui_dckMgr.SaveAsXML(DirectoryHelper.GetConfigFile(ConfiguraionFile.DockableManager));
                ext_mgr.SaveShareUnit(DirectoryHelper.GetConfigFile(ConfiguraionFile.Extension));
        }

        /// <summary>
        /// 加载设置
        /// </summary>
        private void LoadConfig()
        {
            m_judge.AppStatus = CurrentAppStatus.Loading;
            string cfgPath = DirectoryHelper.GetApplicationConfigPath();
            if (Directory.Exists(cfgPath))
            {
                string extCfg = DirectoryHelper.GetConfigFile(ConfiguraionFile.Extension);
                if (File.Exists(extCfg))
                {
                    ext_mgr.LoadShareUnit(extCfg);
                }
                string tbrCfg = DirectoryHelper.GetConfigFile(ConfiguraionFile.ToolbarManager);
                if (File.Exists(tbrCfg))
                {
                    ui_tbrMgr.LoadFromXml(tbrCfg);
                }
                string dckCfg = DirectoryHelper.GetConfigFile(ConfiguraionFile.DockableManager);
                if (File.Exists(dckCfg))
                {
                    ui_dckMgr.LoadFromXML(dckCfg);
                    RecoverDockedPanes();
                }
            }
            else
            {
                Directory.CreateDirectory(cfgPath);
            }

            m_judge.AppStatus = CurrentAppStatus.Normal;
        }

        /// <summary>
        /// 恢复可停靠窗体的内容板
        /// </summary>
        private void RecoverDockedPanes()
        {
            foreach (DockAreaPane dap in ui_dckMgr.DockAreas)
            {
                foreach (DockableControlPane dcp in dap.Panes)
                {
                    string[] mtKeys = UIHelper.SpiltKeysOfTool(dcp.Key);
                    ext_mgr.LoadModule(mtKeys[0]);
                    IDockableWindow iDckWin = ext_mgr.GetTool(mtKeys[1]) as IDockableWindow;
                    dcp.Control = iDckWin.Content;
                }
            }
        }

        /// <summary>
        /// 撤销所有工具按钮的Checked状态
        /// </summary>
        private void UncheckAllStateButton()
        {
            foreach (ToolBase tb in ui_tbrMgr.Tools)
            {
                StateButtonTool sbt = tb as StateButtonTool;
                if (sbt != null && sbt.Checked)
                {
                    sbt.Checked = false;
                }
            }
        }

        /// <summary>
        /// 窗体Load事件,加载工具栏配置,浮动窗口配置,扩展配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrame_Load(object sender, EventArgs e)
        {
            try
            {
                m_judge.AppStatus = CurrentAppStatus.Loading;
                LoadConfig();
            }
            catch (Exception exp)
            {
                FormExtension.UIExceptionRaised(
                    delegate { },
                    Global.LOG_UI,
                    exp,
                    LogLevel.Error);
                throw exp;
            }
        }

        /**
         * 因为Infragistics只能对它自己的库进行序列化,所以需要用一个Infragistics的
         * 组件替换可停靠窗体里包含的组件,以便反序列化时可以正确恢复可停靠窗体.
         * 恢复时是根据Key进行恢复的.
         * 撤销所有工具的Checked状态,是为了在恢复时保持视图与模型的一致性,其实这个
         * 也可以通过保存当前工具的关键字来实现,只是现在的做法比较简单,所以就采用它.
         */
        /// <summary>
        /// 关闭时保存设置.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                m_judge.AppStatus = CurrentAppStatus.Closing;
                foreach (DockAreaPane dap in ui_dckMgr.DockAreas)
                {
                    foreach (DockableControlPane dcp in dap.Panes)
                    {
                        dcp.Control = new Infragistics.Win.Misc.UltraLabel();
                    }
                }
                UncheckAllStateButton();
                SaveConfig();
            }
            catch (Exception exp)
            {
                FormExtension.UIExceptionRaised(
                    delegate { },
                    Global.LOG_UI,
                    exp,
                    LogLevel.Error);
            }
        }

        private void MainFrame_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_judge.AppStatus = CurrentAppStatus.Closed;
        }

        #endregion  //form event

        #region status bar actions

        private const string c_ACTION = "ACTION",
            c_PROGRESS = "PROGRESS",
            c_COORDINATE = "CROODINATE",
            c_MAP_SCALE = "MAP_SCALE",
            c_MAP_UNIT = "MAP_UNIT";

        protected string ActionStatus
        {
            get
            {
                return ui_status.Panels["ACTION"].Text;
            }
            set
            {
                ui_status.Panels[c_ACTION].Text = value;
            }
        }

        protected bool ProgressVisible
        {
            get
            {
                return ui_status.Panels[c_PROGRESS].Visible;
            }
            set
            {
                ui_status.Panels[c_PROGRESS].Visible = value;
            }
        }

        protected int Progress
        {
            get
            {
                return ui_status.Panels[c_PROGRESS].ProgressBarInfo.Value;
            }
            set
            {
                ui_status.Panels[c_PROGRESS].ProgressBarInfo.Value = value;
            }
        }

        protected void SetCoordinate(double x, double y)
        {
            FormExtension.UIThread(this, delegate {
                foreach (Infragistics.Win.UltraWinStatusBar.UltraStatusPanel panel in ui_status.Panels)
                {
                    if (panel.Key.Equals(c_COORDINATE))
                    {
                        panel.Text = string.Format("({0} , {1})", x, y);
                        break;
                    }
                }
            });
        }

        protected void SetMapUnit(string unit)
        {
            ui_status.Panels[c_MAP_UNIT].Text = string.Format("[Unit : {0}]", unit);
        }

        protected void SetMapScale(double scale)
        {
            ui_status.Panels[c_MAP_SCALE].Text = string.Format("[Scale : {0}]", scale);
        }

        #endregion  //status bar actions

        #region extensions

        #region active new , exit current

        protected delegate void NewToolActive(IItem newTool);

        protected delegate void CurrentToolExit(IItem crrTool);

        protected event NewToolActive OnNewToolActive;

        protected event CurrentToolExit OnCurrentToolExit;

        #endregion

        #region extension manager

        private IExtManager ext_mgr;

        private static string s_ExtToolKey = "Limit#WinUI.Menu.Tools.Ext";

        public IExtManager ExtensionManager
        {
            get
            {
                return ext_mgr;
            }
            set
            {
                ext_mgr = value;
            }
        }

        private CurrentTool m_crrTool;

        #endregion  //extension manager

        #region MouseDown , MouseMove , MouseUp , DblClick , KeyDown , KeyUp

        /**
         * typically , these methods are add the passing by delegate to handle
         * the main control , like MapControl , SceneControl , etc .
         */

        protected delegate void ToolMouseAction(int button, int shift, int x, int y);

        protected delegate void DblClick();

        protected delegate void ToolKeyAction(int keyCode, int shift);

        protected abstract void AddMouseDown(ToolMouseAction mouseDown);

        protected abstract void AddMouseMove(ToolMouseAction mouseMove);

        protected abstract void AddMouseUp(ToolMouseAction mouseUp);

        protected abstract void AddDbClick(DblClick dblClick);

        protected abstract void AddKeyDown(ToolKeyAction keyDown);

        protected abstract void AddKeyUp(ToolKeyAction keyUp);

        protected abstract void RemoveMouseDown(ToolMouseAction mouseDown);

        protected abstract void RemoveMouseMove(ToolMouseAction mouseMove);

        protected abstract void RemoveMouseUp(ToolMouseAction mouseUp);

        protected abstract void RemoveDbClick(DblClick dblClick);

        protected abstract void RemoveKeyDown(ToolKeyAction keyDown);

        protected abstract void RemoveKeyUp(ToolKeyAction keyUp);

        #endregion //MouseDown , etc...

        #region execute extensions

        /// <summary>
        /// 执行工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ui_tbrMgr_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs e)
        {
            if (m_judge.ShouldExcuteTool)
            {
                m_judge.AppStatus = CurrentAppStatus.Excuting;

                Cursor tmpCsr = this.Cursor;
                this.Cursor = Cursors.WaitCursor;

                Infragistics.Win.UltraWinToolbars.ToolBase tool = e.Tool;
                string toolkey = tool.Key;
                try
                {
                    if (toolkey.Equals(s_ExtToolKey, StringComparison.InvariantCulture))
                    {
                        Ext();
                    }
                    else
                    {
                        try
                        {
                            object ext = FetchTool(toolkey);
                            ExecuteTool(ext, tool);
                        }
                        catch (Exception exp)
                        {
                            FormExtension.UIExceptionRaised(delegate
                            {
                                MessageBox.Show("执行工具的过程中遇到一个异常:" + exp.Message +
                                    "\n详细异常信息请参看\\bin\\log\\ext.log文件.");
                            }, Global.LOG_EXT, exp, LogLevel.Error);
                        }
                    }
                }
                catch (Exception exp)
                {
                    FormExtension.UIExceptionRaised(delegate { },
                        Global.LOG_UI, exp, LogLevel.Error);
                }
                finally
                {
                    this.Cursor = tmpCsr;
                    m_judge.AppStatus = CurrentAppStatus.Normal;
                }
            }
        }

        /// <summary>
        /// 选择框工具改变事件--尚未测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ui_tbrMgr_ToolValueChanged(object sender, Infragistics.Win.UltraWinToolbars.ToolEventArgs e)
        {
            Cursor tmpCsr = this.Cursor;
            this.Cursor = Cursors.WaitCursor;

            Infragistics.Win.UltraWinToolbars.ToolBase toolbase = e.Tool;
            string tooltag = toolbase.Tag.ToString();
            try
            {
                ISelector pSel = FetchTool(tooltag) as ISelector;
                Infragistics.Win.UltraWinToolbars.ComboBoxTool cobTool = toolbase as Infragistics.Win.UltraWinToolbars.ComboBoxTool;
                object selectedValue = cobTool.Value;
                int idx = cobTool.ValueList.ValueListItems.IndexOf(selectedValue);
                pSel.ChangeSelection(idx);
            }
            catch (Exception exp)
            {
                MessageBox.Show("in method ui_tbrMgr_ToolValueChanged :\n" + exp.Message);
            }
            finally
            {
                this.Cursor = tmpCsr;
            }
        }

        /// <summary>
        /// 取得扩展工具
        /// </summary>
        /// <param name="toolTag"></param>
        /// <returns></returns>
        private object FetchTool(string toolTag)
        {
            string[] keys = UIHelper.SpiltKeysOfTool(toolTag);
            if (keys.Length > 2)
            {
                throw new UIExtException();
            }
            string moduleKey = keys[0];
            string toolKey = keys[1];
            ext_mgr.LoadModule(moduleKey);
            object ext = ext_mgr.GetTool(toolKey);
            return ext;
        }

        /// <summary>
        /// 执行扩展工具
        /// </summary>
        /// <param name="ext"></param>
        /// <param name="toolbase"></param>
        private void ExecuteTool(object ext, Infragistics.Win.UltraWinToolbars.ToolBase toolbase)
        {
            ITool iTool = ext as ITool;
            m_previousTool = toolbase.Key;
            if (iTool != null)
            {
                m_crrTool.UpdateCurrentTool(
                    iTool, 
                    toolbase as Infragistics.Win.UltraWinToolbars.StateButtonTool
                );
                return;
            }
            ICommand iCmd = ext as ICommand;
            if (iCmd != null)
            {
                iCmd.OnClick();
                iCmd.Deactive();
                return;
            }
        }

        /// <summary>
        /// 工具属性改变事件
        /// </summary>
        /// <param name="item"></param>
        protected void OnToolPorpertyChanged(IItem item)
        {
            string itemKey = item.Key;
            string moduleKey = ext_mgr.GetModuleKey(itemKey);
            string uiToolKey = UIHelper.CreateKeyForTool(moduleKey, itemKey);
            ToolBase uiTools = ui_tbrMgr.Tools.GetItem(ui_tbrMgr.Tools.IndexOf(uiToolKey)) as ToolBase;
            UIToolFactory.FillShareProperties(uiTools.SharedProps, item);
            if (item is ITool)
            {
                ((StateButtonTool)uiTools).Checked = item.Checked;
            }
        }

        #endregion  //execute extensions

        #region tools->ext 
        
        private string m_previousTool;

        private static ExtView m_extView;

        //逻辑工具图形化
        internal bool AddExtension(IMake iMake)
        {
            bool totalSucc = true;
            if (iMake == null)
            {
                throw new ArgumentNullException("指定加载的模块为空!");
            }
            try
            {
                string moduleKey = iMake.Key;
                IItem[] items = iMake.Items;
                //添加工具
                UIHelper.CreateTools(items, ui_tbrMgr, moduleKey);
                //添加工具栏
                IToolbar[] tbr = iMake.Toolbars;
                int count = tbr.Length;
                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        UIHelper.CreateToolbar(tbr[i], ui_tbrMgr, moduleKey);
                    }
                    catch (UIExtException innerUiExtExp)
                    {
                        LogManager.CreateInstance().Log(innerUiExtExp, LogLevel.Warn, Global.LOG_UI);
                        totalSucc = false;
                    }
                }
                //添加工具页
                ITab[] tabs = iMake.Tabs;
                count = tabs.Length;
                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        UIHelper.CreateTabs(tabs[i], ui_tbrMgr, moduleKey);
                    }
                    catch (UIExtException innerUiExtExp)
                    {
                        LogManager.CreateInstance().Log(innerUiExtExp, LogLevel.Warn, Global.LOG_UI);
                        totalSucc = false;
                    }
                }
                this.Update();
                SaveConfig();
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return totalSucc;
        }

        /**
         * 移除工具  涉及:
         * 1.工具管理器-
         *      移除所有以指定模块键为键前缀的工具,工具栏,工具页.
         * 2.可停靠窗口管理器-
         *      移除所有以指定模块键为键前缀的可停靠窗口.
         * 3.工具池-
         *      移除工具池中该模块所有工具.
         * 4.配置文件-
         *      保存配置文件.
         */
        internal void RemoveExtension(string moduleKey)
        {
            RemoveTool(moduleKey);
            RemoveDockableWindow(moduleKey);
            ext_mgr.RemoveModule(moduleKey);
            SaveConfig();
        }

        private void RemoveTool(string moduleKey)
        {
            //移除工具栏
            List<UltraToolbar> tbrRemoveLst = new List<UltraToolbar>();
            foreach (UltraToolbar tbr in ui_tbrMgr.Toolbars)
            {
                string[] keys = UIHelper.SpiltKeysOfTool(tbr.Key);
                if (keys!=null && 
                    !string.IsNullOrEmpty(keys[0]) && 
                    keys[0].Equals(moduleKey, StringComparison.CurrentCultureIgnoreCase))
                {
                    tbrRemoveLst.Add(tbr);
                }
            }
            foreach (UltraToolbar tbr in tbrRemoveLst)
            {
                ui_tbrMgr.Toolbars.Remove(tbr);
            }
            //移除工具页
            List<RibbonTab> tabRemoveLst = new List<RibbonTab>();
            foreach (RibbonTab rt in ui_tbrMgr.Ribbon.Tabs)
            {
                string[] keys = UIHelper.SpiltKeysOfTool(rt.Key);
                if (keys != null &&
                    !string.IsNullOrEmpty(keys[0]) &&
                    keys[0].Equals(moduleKey, StringComparison.CurrentCultureIgnoreCase))
                {
                    tabRemoveLst.Add(rt);
                }
            }
            foreach (RibbonTab rt in tabRemoveLst)
            {
                ui_tbrMgr.Ribbon.Tabs.Remove(rt);
            }
            //移除工具
            List<ToolBase> toolRemoveLst = new List<ToolBase>();
            foreach (ToolBase tool in ui_tbrMgr.Tools)
            {
                string[] keys = UIHelper.SpiltKeysOfTool(tool.Key);
                if (keys != null &&
                    !string.IsNullOrEmpty(keys[0]) &&
                    keys[0].Equals(moduleKey, StringComparison.CurrentCultureIgnoreCase))
                {
                    toolRemoveLst.Add(tool);
                }
            }
            foreach (ToolBase tool in toolRemoveLst)
            {
                ui_tbrMgr.Tools.Remove(tool);
            }
        }

        private void RemoveDockableWindow(string moduleKey)
        {
            ui_dckMgr.SuspendLayout();
            List<DockableControlPane> paneRemoveLst = new List<DockableControlPane>();
            foreach (DockAreaPane area in ui_dckMgr.DockAreas)
            {
                foreach (DockableControlPane pane in area.Panes)
                {
                    string[] keys = UIHelper.SpiltKeysOfTool(pane.Key);
                    if (keys != null &&
                        !string.IsNullOrEmpty(keys[0]) &&
                        keys[0].Equals(moduleKey, StringComparison.CurrentCultureIgnoreCase))
                    {
                        paneRemoveLst.Add(pane);
                    }
                }
                foreach (DockableControlPane pane in paneRemoveLst)
                {
                    area.Panes.Remove(pane);
                }
            }
            ui_dckMgr.ResumeLayout();
        }

        protected virtual void Ext()
        {
            if (m_extView == null)
            {
                m_extView = new ExtView(ext_mgr, this);
                FormExtension.BindParentChild(this, m_extView);
            }
            m_extView.Show();
        }

        #endregion

        #endregion //extensions

        #region DockableWindow

        private Dictionary<string, Infragistics.Win.UltraWinDock.DockableWindow> m_dckWinPool;

        protected DockablePaneBase GetDockablePane(IDockableWindow iDckWinTool)
        {
            if (iDckWinTool == null)
            {
                throw new ArgumentNullException();
            }
            string dckWinKey = UIHelper.CreateKeyForTool(iDckWinTool.Make.Key, iDckWinTool.Key);
            return ui_dckMgr.PaneFromKey(dckWinKey);
        }

        protected void AddDockableWindowAsyn(IDockableWindow iDckWinTool)
        {
            FormExtension.UIThread(this, delegate { AddDockableWindowAsyn(iDckWinTool); });
        }

        protected void AddDockableWindow(IDockableWindow iDckWinTool)
        {
            string dckWinKey = UIHelper.CreateKeyForTool(iDckWinTool.Make.Key, iDckWinTool.Key);
            DockablePaneBase pane = GetDockablePane(iDckWinTool);
            if (pane == null)
            {
                pane = new DockableControlPane(dckWinKey, iDckWinTool.Name, iDckWinTool.Content);
                Infragistics.Win.UltraWinDock.WindowDockingArea dckArea = new Infragistics.Win.UltraWinDock.WindowDockingArea();
                DockAreaPane dap = new DockAreaPane(DockedLocation.DockedLeft);
                dap.ChildPaneStyle = ChildPaneStyle.TabGroup;
                dckArea.SuspendLayout();
                dap.Panes.Add(pane);
                ui_dckMgr.DockAreas.Add(dap);
                dckArea.ResumeLayout();
            }
            else
            {
                if (pane.IsVisible)
                {
                    pane.Close();
                }
                else
                {
                    pane.Show();
                }
            }
            SaveConfig();
        }

        protected void ShowDockableWindow(IDockableWindow iDckWinTool)
        {
            DockablePaneBase pane = GetDockablePane(iDckWinTool);
            if (pane == null)
            {
                AddDockableWindow(iDckWinTool);
            }
            else
            {
                pane.Show();
            }
        }

        protected void HideDockableWindow(IDockableWindow iDckWinTool)
        {
            DockablePaneBase pane = GetDockablePane(iDckWinTool);
            if (pane != null)
            {
                pane.Close();
            }
        }

        protected Infragistics.Win.UltraWinDock.DockableWindow GetDockableWindow(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException();
            }
            if (m_dckWinPool.ContainsKey(key))
            {
                return m_dckWinPool[key];
            }
            throw new UIExtException();
        }

        protected Infragistics.Win.UltraWinDock.DockableWindow[] DockableWindows
        {
            get
            {
                Dictionary<string, Infragistics.Win.UltraWinDock.DockableWindow>.ValueCollection valColl
                    = m_dckWinPool.Values;
                Infragistics.Win.UltraWinDock.DockableWindow[] dckWinArr 
                    = new Infragistics.Win.UltraWinDock.DockableWindow[valColl.Count];
                return dckWinArr;
            }
        }

        #endregion

        #region inner form

        private Dictionary<IInnerForm, Form> m_innerFormDict;

        protected void ShowInnerForm(IInnerForm child)
        {
            if (child != null)
            {
                Form childView = null;
                if (m_innerFormDict.ContainsKey(child))
                {
                    childView = m_innerFormDict[child];
                }
                else
                {
                    childView = new Form();
                    childView.Text = child.Title;
                    InnerFormEvent inFormEvent = new InnerFormEvent(child);
                    childView.Load += new EventHandler(inFormEvent.OnLoad);
                    childView.FormClosing += new FormClosingEventHandler(inFormEvent.OnClosing);
                    childView.FormClosed += new FormClosedEventHandler(inFormEvent.OnClosed);
                    Control content = child.Content;
                    if (content != null)
                    {
                        childView.SuspendLayout();
                        childView.Width = content.Width + 6;
                        childView.Height = content.Height + 38;
                        content.Dock = DockStyle.Fill;
                        childView.Controls.Add(content);
                        childView.ResumeLayout();
                    }
                    FormExtension.BindParentChild(this, childView);
                    m_innerFormDict.Add(child, childView);
                }
                if ((!childView.IsDisposed) && (!childView.Visible))
                {
                    childView.Show();
                }
            }
        }

        protected void CloseInnerForm(IInnerForm child)
        {
            if (child != null)
            {
                Form childView = null;
                if (m_innerFormDict.TryGetValue(child, out childView))
                {
                    if (!childView.IsDisposed)
                    {
                        childView.Close();
                    }
                    m_innerFormDict.Remove(child);
                }
            }
        }

        protected void HideInnerForm(IInnerForm child)
        {
            if (child != null)
            {
                Form childView = null;
                if (m_innerFormDict.TryGetValue(child, out childView))
                {
                    if (!childView.IsDisposed)
                    {
                        childView.Hide();
                    }
                }
            }
        }

        private class InnerFormEvent
        {
            private IInnerForm m_model;

            public InnerFormEvent(IInnerForm model)
            {
                if (model == null)
                {
                    throw new ArgumentNullException();
                }
                m_model = model;
            }

            public void OnLoad(object sender, EventArgs e)
            {
                m_model.OnLoad();
            }

            public void OnClosing(object sender, FormClosingEventArgs e)
            {
                bool cancel = false;
                m_model.OnClosing(ref cancel);
                e.Cancel = cancel;
            }

            public void OnClosed(object sender, FormClosedEventArgs e)
            {
                m_model.OnClosed(e.CloseReason);
            }
        }

        #endregion

        #region main content

        protected void AddMainContent(Control mainCtrl)
        {
            this.SuspendLayout();
            MainFrame_Fill_Panel.SuspendLayout();
            mainCtrl.Dock = DockStyle.Fill;
            MainFrame_Fill_Panel.Controls.Add(mainCtrl);
            MainFrame_Fill_Panel.ResumeLayout();
            this.ResumeLayout();
        }

        #endregion

        #region AppJudge , used as application flow control

        /**
         * AppJudge是用于控制应用程序的控制流的.
         * AppJudge内置一些应用程序可能的状态,诸如加载,关闭,执行等,
         * 通过对当前状态的判断,应用程序可以决定是否应该执行某些操作,
         * 主要用于扩展工具的执行.
         */
        private class AppJudge
        {
            private CurrentAppStatus m_status = CurrentAppStatus.Normal;

            /// <summary>
            /// 根据当前应用程序的状态,判断是否应该执行工具
            /// </summary>
            public bool ShouldExcuteTool
            {
                get
                {
                    return AppStatus == CurrentAppStatus.Normal;
                }
            }

            /// <summary>
            /// 设置应用程序状态
            /// </summary>
            public CurrentAppStatus AppStatus
            {
                get
                {
                    return m_status;
                }
                set
                {
                    m_status = value;
                }
            }
        }

        /// <summary>
        /// 应用程序状态
        /// </summary>
        private enum CurrentAppStatus
        {
            /// <summary>
            /// 正常状态,等待用户操作.
            /// </summary>
            Normal,
            /// <summary>
            /// 关闭中
            /// </summary>
            Closing,
            /// <summary>
            /// 已关闭
            /// </summary>
            Closed,
            /// <summary>
            /// 正在执行工具
            /// </summary>
            Excuting,
            /// <summary>
            /// 正在加载应用程序
            /// </summary>
            Loading
        }

        #endregion  //AppJudge

        #region CurrentTool , used as tool excuting control

        private class CurrentTool
        {
            private ITool m_ext;

            #region handlers

            private ToolMouseAction m_mouseDown;

            private ToolMouseAction m_mouseMove;

            private ToolMouseAction m_mouseUp;

            private DblClick m_dblClick;

            private ToolKeyAction m_keyDown;

            private ToolKeyAction m_keyUp;

            #endregion

            private MainFrame m_app;

            public CurrentTool(MainFrame frm)
            {
                m_app = frm;
            }

            public void UpdateCurrentTool(ITool ext, Infragistics.Win.UltraWinToolbars.StateButtonTool tool)
            {
                if (!ext.Checked && (ext.Checked == tool.Checked))
                {
                    m_ext = ext;
                }
                CurrentExit();
                if (ext != m_ext)
                {
                    m_ext = ext;
                    NewActive();
                }
                else
                {
                    m_ext = null;
                }
            }

            private void CurrentExit()
            {
                if (m_ext != null && m_ext.Checked)
                {
                    m_ext.Deactive();
                    m_ext.Checked = false;
                    RemoveOldHandler();
                    if (m_app.OnCurrentToolExit != null)
                    {
                        m_app.OnCurrentToolExit(m_ext);
                    }
                }
            }

            private void NewActive()
            {
                if (m_ext != null)
                {
                    m_ext.OnClick();
                    m_ext.Checked = true;
                    AddNewHandler();
                    if (m_app.OnNewToolActive != null)
                    {
                        m_app.OnNewToolActive(m_ext);
                    }
                }
            }

            private void RemoveOldHandler()
            {
                if (m_ext != null)
                {
                    m_app.RemoveMouseDown(m_mouseDown);
                    m_app.RemoveMouseMove(m_mouseMove);
                    m_app.RemoveMouseUp(m_mouseUp);
                    m_app.RemoveDbClick(m_dblClick);
                    m_app.RemoveKeyDown(m_keyDown);
                    m_app.RemoveKeyUp(m_keyUp);
                }
            }

            private void AddNewHandler()
            {
                if (m_ext != null)
                {
                    m_mouseDown = new ToolMouseAction(m_ext.OnMouseDown);
                    m_app.AddMouseDown(m_mouseDown);
                    m_mouseMove = new ToolMouseAction(m_ext.OnMouseMove);
                    m_app.AddMouseMove(m_mouseMove);
                    m_mouseUp = new ToolMouseAction(m_ext.OnMouseUp);
                    m_app.AddMouseUp(m_mouseUp);
                    m_dblClick = new DblClick(m_ext.OnDblClick);
                    m_app.AddDbClick(m_dblClick);

                    m_keyDown = new ToolKeyAction(m_ext.OnKeyDown);
                    m_app.AddKeyDown(m_keyDown);
                    m_keyUp = new ToolKeyAction(m_ext.OnKeyUp);
                    m_app.AddKeyUp(m_keyUp);
                }
            }
        }

        #endregion  //CurrentTool
    }
}
