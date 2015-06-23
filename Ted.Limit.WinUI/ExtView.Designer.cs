namespace Ted.Limit.WinUI
{
    partial class ExtView
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
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo5 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("existing modules in system", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo4 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("select a module to remove", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo3 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("open a file dialog selecting a module to add", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo2 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("tools in selected module", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            Infragistics.Win.UltraWinToolTip.UltraToolTipInfo ultraToolTipInfo1 = new Infragistics.Win.UltraWinToolTip.UltraToolTipInfo("module to add", Infragistics.Win.ToolTipImage.Default, null, Infragistics.Win.DefaultableBoolean.Default);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtView));
            this.moduleLstView = new System.Windows.Forms.ListView();
            this.removeModuleBtn = new System.Windows.Forms.Button();
            this.loadModuleBtn = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.toolLstView = new System.Windows.Forms.ListView();
            this.newModuleTxt = new System.Windows.Forms.TextBox();
            this.ui_tooltipMgr = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(this.components);
            this.openBtn = new System.Windows.Forms.Button();
            this.errorLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // moduleLstView
            // 
            this.moduleLstView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.moduleLstView.Location = new System.Drawing.Point(10, 8);
            this.moduleLstView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.moduleLstView.MultiSelect = false;
            this.moduleLstView.Name = "moduleLstView";
            this.moduleLstView.Size = new System.Drawing.Size(153, 178);
            this.moduleLstView.TabIndex = 0;
            ultraToolTipInfo5.ToolTipText = "existing modules in system";
            this.ui_tooltipMgr.SetUltraToolTip(this.moduleLstView, ultraToolTipInfo5);
            this.moduleLstView.UseCompatibleStateImageBehavior = false;
            this.moduleLstView.View = System.Windows.Forms.View.List;
            this.moduleLstView.SelectedIndexChanged += new System.EventHandler(this.moduleLstView_SelectedIndexChanged);
            // 
            // removeModuleBtn
            // 
            this.removeModuleBtn.Location = new System.Drawing.Point(10, 190);
            this.removeModuleBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.removeModuleBtn.Name = "removeModuleBtn";
            this.removeModuleBtn.Size = new System.Drawing.Size(64, 22);
            this.removeModuleBtn.TabIndex = 1;
            this.removeModuleBtn.Text = "Remove";
            ultraToolTipInfo4.ToolTipText = "select a module to remove";
            this.ui_tooltipMgr.SetUltraToolTip(this.removeModuleBtn, ultraToolTipInfo4);
            this.removeModuleBtn.UseVisualStyleBackColor = true;
            this.removeModuleBtn.Click += new System.EventHandler(this.removeModuleBtn_Click);
            // 
            // loadModuleBtn
            // 
            this.loadModuleBtn.Location = new System.Drawing.Point(261, 215);
            this.loadModuleBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loadModuleBtn.Name = "loadModuleBtn";
            this.loadModuleBtn.Size = new System.Drawing.Size(64, 22);
            this.loadModuleBtn.TabIndex = 2;
            this.loadModuleBtn.Text = "Load";
            ultraToolTipInfo3.ToolTipText = "open a file dialog selecting a module to add";
            this.ui_tooltipMgr.SetUltraToolTip(this.loadModuleBtn, ultraToolTipInfo3);
            this.loadModuleBtn.UseVisualStyleBackColor = true;
            this.loadModuleBtn.Click += new System.EventHandler(this.loadModuleBtn_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.Location = new System.Drawing.Point(261, 241);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(64, 22);
            this.closeBtn.TabIndex = 4;
            this.closeBtn.Text = "Close";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // toolLstView
            // 
            this.toolLstView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.toolLstView.Location = new System.Drawing.Point(173, 8);
            this.toolLstView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.toolLstView.Name = "toolLstView";
            this.toolLstView.Size = new System.Drawing.Size(153, 204);
            this.toolLstView.TabIndex = 5;
            ultraToolTipInfo2.ToolTipText = "tools in selected module";
            this.ui_tooltipMgr.SetUltraToolTip(this.toolLstView, ultraToolTipInfo2);
            this.toolLstView.UseCompatibleStateImageBehavior = false;
            this.toolLstView.View = System.Windows.Forms.View.List;
            // 
            // newModuleTxt
            // 
            this.newModuleTxt.Location = new System.Drawing.Point(8, 216);
            this.newModuleTxt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.newModuleTxt.Name = "newModuleTxt";
            this.newModuleTxt.Size = new System.Drawing.Size(207, 21);
            this.newModuleTxt.TabIndex = 6;
            ultraToolTipInfo1.ToolTipText = "module to add";
            this.ui_tooltipMgr.SetUltraToolTip(this.newModuleTxt, ultraToolTipInfo1);
            // 
            // ui_tooltipMgr
            // 
            this.ui_tooltipMgr.ContainingControl = this;
            // 
            // openBtn
            // 
            this.openBtn.Location = new System.Drawing.Point(221, 214);
            this.openBtn.Name = "openBtn";
            this.openBtn.Size = new System.Drawing.Size(34, 23);
            this.openBtn.TabIndex = 7;
            this.openBtn.Text = "...";
            this.openBtn.UseVisualStyleBackColor = true;
            this.openBtn.Click += new System.EventHandler(this.openBtn_Click);
            // 
            // errorLbl
            // 
            this.errorLbl.AutoSize = true;
            this.errorLbl.ForeColor = System.Drawing.Color.Red;
            this.errorLbl.Location = new System.Drawing.Point(6, 246);
            this.errorLbl.Name = "errorLbl";
            this.errorLbl.Size = new System.Drawing.Size(0, 12);
            this.errorLbl.TabIndex = 8;
            // 
            // ExtView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 268);
            this.Controls.Add(this.errorLbl);
            this.Controls.Add(this.openBtn);
            this.Controls.Add(this.newModuleTxt);
            this.Controls.Add(this.toolLstView);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.loadModuleBtn);
            this.Controls.Add(this.removeModuleBtn);
            this.Controls.Add(this.moduleLstView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ExtView";
            this.ShowInTaskbar = false;
            this.Text = "ExtView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExtView_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView moduleLstView;
        private System.Windows.Forms.Button removeModuleBtn;
        private System.Windows.Forms.Button loadModuleBtn;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.ListView toolLstView;
        private System.Windows.Forms.TextBox newModuleTxt;
        private Infragistics.Win.UltraWinToolTip.UltraToolTipManager ui_tooltipMgr;
        private System.Windows.Forms.Button openBtn;
        private System.Windows.Forms.Label errorLbl;
    }
}