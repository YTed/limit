namespace Ted.Limit.WinUI
{
    partial class MainFrame
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrame));
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel1 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel2 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel3 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel4 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinStatusBar.UltraStatusPanel ultraStatusPanel5 = new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel();
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool1 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Limit#WinUI.Menu.Tools.Ext");
            Infragistics.Win.UltraWinToolbars.ButtonTool buttonTool2 = new Infragistics.Win.UltraWinToolbars.ButtonTool("Limit#WinUI.Menu.Tools.Ext");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            this.MainFrame_Fill_Panel = new System.Windows.Forms.Panel();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.ui_status = new Infragistics.Win.UltraWinStatusBar.UltraStatusBar();
            this.ui_dckMgr = new Infragistics.Win.UltraWinDock.UltraDockManager(this.components);
            this._MainFrameUnpinnedTabAreaLeft = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._MainFrameUnpinnedTabAreaRight = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._MainFrameUnpinnedTabAreaTop = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._MainFrameUnpinnedTabAreaBottom = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
            this._MainFrameAutoHideControl = new Infragistics.Win.UltraWinDock.AutoHideControl();
            this.ui_tbrMgr = new Infragistics.Win.UltraWinToolbars.UltraToolbarsManager(this.components);
            this._MainFrame_Toolbars_Dock_Area_Left = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._MainFrame_Toolbars_Dock_Area_Right = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._MainFrame_Toolbars_Dock_Area_Top = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this._MainFrame_Toolbars_Dock_Area_Bottom = new Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea();
            this.MainFrame_Fill_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ui_dckMgr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ui_tbrMgr)).BeginInit();
            this.SuspendLayout();
            // 
            // MainFrame_Fill_Panel
            // 
            this.MainFrame_Fill_Panel.Controls.Add(this.axLicenseControl1);
            this.MainFrame_Fill_Panel.Controls.Add(this.ui_status);
            this.MainFrame_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.MainFrame_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainFrame_Fill_Panel.Location = new System.Drawing.Point(4, 174);
            this.MainFrame_Fill_Panel.Name = "MainFrame_Fill_Panel";
            this.MainFrame_Fill_Panel.Size = new System.Drawing.Size(873, 292);
            this.MainFrame_Fill_Panel.TabIndex = 0;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(381, 104);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 1;
            // 
            // ui_status
            // 
            this.ui_status.Location = new System.Drawing.Point(0, 269);
            this.ui_status.Name = "ui_status";
            ultraStatusPanel1.Key = "ACTION";
            ultraStatusPanel1.Width = 200;
            ultraStatusPanel2.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded4Thick;
            ultraStatusPanel2.Key = "PROGRESS";
            appearance1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            appearance1.BackColor2 = System.Drawing.Color.Silver;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.BackwardDiagonal;
            appearance1.ForeColor = System.Drawing.Color.Navy;
            appearance1.ForeColorDisabled = System.Drawing.Color.Black;
            appearance1.ForegroundAlpha = Infragistics.Win.Alpha.Opaque;
            ultraStatusPanel2.ProgressBarInfo.FillAppearance = appearance1;
            ultraStatusPanel2.Style = Infragistics.Win.UltraWinStatusBar.PanelStyle.Progress;
            ultraStatusPanel2.Visible = false;
            ultraStatusPanel3.Key = "CROODINATE";
            ultraStatusPanel3.MinWidth = 250;
            ultraStatusPanel3.Text = "(1354687.1649843 , 3165498.134698)";
            ultraStatusPanel3.Width = 300;
            ultraStatusPanel4.Key = "MAP_SCALE";
            ultraStatusPanel4.MinWidth = 150;
            ultraStatusPanel4.Text = "[Scale : 545364]";
            ultraStatusPanel4.Width = 200;
            ultraStatusPanel5.Key = "MAP_UNIT";
            ultraStatusPanel5.MinWidth = 150;
            ultraStatusPanel5.Text = "[Unit : Unknown]";
            ultraStatusPanel5.Width = 200;
            this.ui_status.Panels.AddRange(new Infragistics.Win.UltraWinStatusBar.UltraStatusPanel[] {
            ultraStatusPanel1,
            ultraStatusPanel2,
            ultraStatusPanel3,
            ultraStatusPanel4,
            ultraStatusPanel5});
            this.ui_status.Size = new System.Drawing.Size(873, 23);
            this.ui_status.TabIndex = 0;
            this.ui_status.ViewStyle = Infragistics.Win.UltraWinStatusBar.ViewStyle.Office2007;
            // 
            // ui_dckMgr
            // 
            this.ui_dckMgr.CaptionStyle = Infragistics.Win.UltraWinDock.CaptionStyle.Office2007;
            this.ui_dckMgr.HostControl = this;
            this.ui_dckMgr.UnpinnedTabStyle = Infragistics.Win.UltraWinTabs.TabStyle.Office2007Ribbon;
            this.ui_dckMgr.WindowStyle = Infragistics.Win.UltraWinDock.WindowStyle.Office2007;
            // 
            // _MainFrameUnpinnedTabAreaLeft
            // 
            this._MainFrameUnpinnedTabAreaLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this._MainFrameUnpinnedTabAreaLeft.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._MainFrameUnpinnedTabAreaLeft.Location = new System.Drawing.Point(4, 174);
            this._MainFrameUnpinnedTabAreaLeft.Name = "_MainFrameUnpinnedTabAreaLeft";
            this._MainFrameUnpinnedTabAreaLeft.Owner = this.ui_dckMgr;
            this._MainFrameUnpinnedTabAreaLeft.Size = new System.Drawing.Size(0, 292);
            this._MainFrameUnpinnedTabAreaLeft.TabIndex = 5;
            // 
            // _MainFrameUnpinnedTabAreaRight
            // 
            this._MainFrameUnpinnedTabAreaRight.Dock = System.Windows.Forms.DockStyle.Right;
            this._MainFrameUnpinnedTabAreaRight.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._MainFrameUnpinnedTabAreaRight.Location = new System.Drawing.Point(877, 174);
            this._MainFrameUnpinnedTabAreaRight.Name = "_MainFrameUnpinnedTabAreaRight";
            this._MainFrameUnpinnedTabAreaRight.Owner = this.ui_dckMgr;
            this._MainFrameUnpinnedTabAreaRight.Size = new System.Drawing.Size(0, 292);
            this._MainFrameUnpinnedTabAreaRight.TabIndex = 6;
            // 
            // _MainFrameUnpinnedTabAreaTop
            // 
            this._MainFrameUnpinnedTabAreaTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._MainFrameUnpinnedTabAreaTop.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._MainFrameUnpinnedTabAreaTop.Location = new System.Drawing.Point(4, 174);
            this._MainFrameUnpinnedTabAreaTop.Name = "_MainFrameUnpinnedTabAreaTop";
            this._MainFrameUnpinnedTabAreaTop.Owner = this.ui_dckMgr;
            this._MainFrameUnpinnedTabAreaTop.Size = new System.Drawing.Size(873, 0);
            this._MainFrameUnpinnedTabAreaTop.TabIndex = 7;
            // 
            // _MainFrameUnpinnedTabAreaBottom
            // 
            this._MainFrameUnpinnedTabAreaBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._MainFrameUnpinnedTabAreaBottom.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._MainFrameUnpinnedTabAreaBottom.Location = new System.Drawing.Point(4, 466);
            this._MainFrameUnpinnedTabAreaBottom.Name = "_MainFrameUnpinnedTabAreaBottom";
            this._MainFrameUnpinnedTabAreaBottom.Owner = this.ui_dckMgr;
            this._MainFrameUnpinnedTabAreaBottom.Size = new System.Drawing.Size(873, 0);
            this._MainFrameUnpinnedTabAreaBottom.TabIndex = 8;
            // 
            // _MainFrameAutoHideControl
            // 
            this._MainFrameAutoHideControl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._MainFrameAutoHideControl.Location = new System.Drawing.Point(25, 149);
            this._MainFrameAutoHideControl.Name = "_MainFrameAutoHideControl";
            this._MainFrameAutoHideControl.Owner = this.ui_dckMgr;
            this._MainFrameAutoHideControl.Size = new System.Drawing.Size(100, 317);
            this._MainFrameAutoHideControl.TabIndex = 9;
            // 
            // ui_tbrMgr
            // 
            this.ui_tbrMgr.DesignerFlags = 1;
            this.ui_tbrMgr.DockWithinContainer = this;
            this.ui_tbrMgr.DockWithinContainerBaseType = typeof(System.Windows.Forms.Form);
            this.ui_tbrMgr.Office2007UICompatibility = false;
            this.ui_tbrMgr.Ribbon.QuickAccessToolbar.NonInheritedTools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool1});
            this.ui_tbrMgr.Ribbon.Visible = true;
            this.ui_tbrMgr.ShowFullMenusDelay = 500;
            appearance2.Image = global::Ted.Limit.WinUI.Properties.Resources.Ext;
            buttonTool2.SharedProps.AppearancesLarge.Appearance = appearance2;
            appearance3.Image = global::Ted.Limit.WinUI.Properties.Resources.Ext;
            buttonTool2.SharedProps.AppearancesSmall.Appearance = appearance3;
            buttonTool2.SharedProps.Caption = "Ext";
            buttonTool2.SharedProps.Category = "Limit";
            this.ui_tbrMgr.Tools.AddRange(new Infragistics.Win.UltraWinToolbars.ToolBase[] {
            buttonTool2});
            this.ui_tbrMgr.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.ui_tbrMgr_ToolClick);
            // 
            // _MainFrame_Toolbars_Dock_Area_Left
            // 
            this._MainFrame_Toolbars_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._MainFrame_Toolbars_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._MainFrame_Toolbars_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Left;
            this._MainFrame_Toolbars_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._MainFrame_Toolbars_Dock_Area_Left.InitialResizeAreaExtent = 4;
            this._MainFrame_Toolbars_Dock_Area_Left.Location = new System.Drawing.Point(0, 174);
            this._MainFrame_Toolbars_Dock_Area_Left.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._MainFrame_Toolbars_Dock_Area_Left.Name = "_MainFrame_Toolbars_Dock_Area_Left";
            this._MainFrame_Toolbars_Dock_Area_Left.Size = new System.Drawing.Size(4, 292);
            this._MainFrame_Toolbars_Dock_Area_Left.ToolbarsManager = this.ui_tbrMgr;
            // 
            // _MainFrame_Toolbars_Dock_Area_Right
            // 
            this._MainFrame_Toolbars_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._MainFrame_Toolbars_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._MainFrame_Toolbars_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Right;
            this._MainFrame_Toolbars_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._MainFrame_Toolbars_Dock_Area_Right.InitialResizeAreaExtent = 4;
            this._MainFrame_Toolbars_Dock_Area_Right.Location = new System.Drawing.Point(877, 174);
            this._MainFrame_Toolbars_Dock_Area_Right.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._MainFrame_Toolbars_Dock_Area_Right.Name = "_MainFrame_Toolbars_Dock_Area_Right";
            this._MainFrame_Toolbars_Dock_Area_Right.Size = new System.Drawing.Size(4, 292);
            this._MainFrame_Toolbars_Dock_Area_Right.ToolbarsManager = this.ui_tbrMgr;
            // 
            // _MainFrame_Toolbars_Dock_Area_Top
            // 
            this._MainFrame_Toolbars_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._MainFrame_Toolbars_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._MainFrame_Toolbars_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Top;
            this._MainFrame_Toolbars_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._MainFrame_Toolbars_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._MainFrame_Toolbars_Dock_Area_Top.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._MainFrame_Toolbars_Dock_Area_Top.Name = "_MainFrame_Toolbars_Dock_Area_Top";
            this._MainFrame_Toolbars_Dock_Area_Top.Size = new System.Drawing.Size(881, 174);
            this._MainFrame_Toolbars_Dock_Area_Top.ToolbarsManager = this.ui_tbrMgr;
            // 
            // _MainFrame_Toolbars_Dock_Area_Bottom
            // 
            this._MainFrame_Toolbars_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._MainFrame_Toolbars_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._MainFrame_Toolbars_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinToolbars.DockedPosition.Bottom;
            this._MainFrame_Toolbars_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._MainFrame_Toolbars_Dock_Area_Bottom.InitialResizeAreaExtent = 4;
            this._MainFrame_Toolbars_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 466);
            this._MainFrame_Toolbars_Dock_Area_Bottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this._MainFrame_Toolbars_Dock_Area_Bottom.Name = "_MainFrame_Toolbars_Dock_Area_Bottom";
            this._MainFrame_Toolbars_Dock_Area_Bottom.Size = new System.Drawing.Size(881, 4);
            this._MainFrame_Toolbars_Dock_Area_Bottom.ToolbarsManager = this.ui_tbrMgr;
            // 
            // MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 470);
            this.Controls.Add(this._MainFrameAutoHideControl);
            this.Controls.Add(this.MainFrame_Fill_Panel);
            this.Controls.Add(this._MainFrameUnpinnedTabAreaTop);
            this.Controls.Add(this._MainFrameUnpinnedTabAreaBottom);
            this.Controls.Add(this._MainFrameUnpinnedTabAreaRight);
            this.Controls.Add(this._MainFrameUnpinnedTabAreaLeft);
            this.Controls.Add(this._MainFrame_Toolbars_Dock_Area_Left);
            this.Controls.Add(this._MainFrame_Toolbars_Dock_Area_Right);
            this.Controls.Add(this._MainFrame_Toolbars_Dock_Area_Top);
            this.Controls.Add(this._MainFrame_Toolbars_Dock_Area_Bottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainFrame";
            this.Text = "MainFrame";
            this.Load += new System.EventHandler(this.MainFrame_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFrame_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrame_FormClosing);
            this.MainFrame_Fill_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ui_dckMgr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ui_tbrMgr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainFrame_Fill_Panel;
        private Infragistics.Win.UltraWinStatusBar.UltraStatusBar ui_status;
        private Infragistics.Win.UltraWinDock.AutoHideControl _MainFrameAutoHideControl;
        private Infragistics.Win.UltraWinDock.UltraDockManager ui_dckMgr;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _MainFrameUnpinnedTabAreaLeft;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _MainFrameUnpinnedTabAreaTop;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _MainFrameUnpinnedTabAreaBottom;
        private Infragistics.Win.UltraWinDock.UnpinnedTabArea _MainFrameUnpinnedTabAreaRight;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _MainFrame_Toolbars_Dock_Area_Left;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsManager ui_tbrMgr;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _MainFrame_Toolbars_Dock_Area_Right;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _MainFrame_Toolbars_Dock_Area_Top;
        private Infragistics.Win.UltraWinToolbars.UltraToolbarsDockArea _MainFrame_Toolbars_Dock_Area_Bottom;
    }
}

