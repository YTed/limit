namespace Ted.Limit.Ext.Map.Standard
{
    partial class TOCContent
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TOCContent));
            this.TOC = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            ((System.ComponentModel.ISupportInitialize)(this.TOC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // TOC
            // 
            this.TOC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TOC.Location = new System.Drawing.Point(0, 0);
            this.TOC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TOC.Name = "TOC";
            this.TOC.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("TOC.OcxState")));
            this.TOC.Size = new System.Drawing.Size(391, 789);
            this.TOC.TabIndex = 0;
            this.TOC.OnMouseUp += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseUpEventHandler(this.TOC_OnMouseUp);
            this.TOC.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.TOC_OnMouseDown);
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(211, 189);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 1;
            // 
            // TOCContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axLicenseControl1);
            this.Controls.Add(this.TOC);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TOCContent";
            this.Size = new System.Drawing.Size(391, 789);
            ((System.ComponentModel.ISupportInitialize)(this.TOC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxTOCControl TOC;
        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
    }
}
