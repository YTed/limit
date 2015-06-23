namespace Ted.Limit.Ext.Map.Analyst
{
    partial class BufferWizard
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
            this.gbInput = new System.Windows.Forms.GroupBox();
            this.inputBtn = new System.Windows.Forms.Button();
            this.txtInputFile = new System.Windows.Forms.TextBox();
            this.rbFromFile = new System.Windows.Forms.RadioButton();
            this.cmbInputLyr = new System.Windows.Forms.ComboBox();
            this.rbFromLry = new System.Windows.Forms.RadioButton();
            this.gbDist = new System.Windows.Forms.GroupBox();
            this.cmbDistField = new System.Windows.Forms.ComboBox();
            this.txtDist = new System.Windows.Forms.TextBox();
            this.rbDistField = new System.Windows.Forms.RadioButton();
            this.rbDistFix = new System.Windows.Forms.RadioButton();
            this.gbEndType = new System.Windows.Forms.GroupBox();
            this.rbEndTypeFlat = new System.Windows.Forms.RadioButton();
            this.rbEndTypeRound = new System.Windows.Forms.RadioButton();
            this.gbLine = new System.Windows.Forms.GroupBox();
            this.rbLineRight = new System.Windows.Forms.RadioButton();
            this.rbLineLeft = new System.Windows.Forms.RadioButton();
            this.rbLineAll = new System.Windows.Forms.RadioButton();
            this.gbOutput = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.outputBtn = new System.Windows.Forms.Button();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.gbInput.SuspendLayout();
            this.gbDist.SuspendLayout();
            this.gbEndType.SuspendLayout();
            this.gbLine.SuspendLayout();
            this.gbOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbInput
            // 
            this.gbInput.Controls.Add(this.inputBtn);
            this.gbInput.Controls.Add(this.txtInputFile);
            this.gbInput.Controls.Add(this.rbFromFile);
            this.gbInput.Controls.Add(this.cmbInputLyr);
            this.gbInput.Controls.Add(this.rbFromLry);
            this.gbInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbInput.Location = new System.Drawing.Point(0, 0);
            this.gbInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbInput.Name = "gbInput";
            this.gbInput.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbInput.Size = new System.Drawing.Size(424, 81);
            this.gbInput.TabIndex = 0;
            this.gbInput.TabStop = false;
            this.gbInput.Text = "Input Feature";
            // 
            // inputBtn
            // 
            this.inputBtn.Enabled = false;
            this.inputBtn.Location = new System.Drawing.Point(371, 41);
            this.inputBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.inputBtn.Name = "inputBtn";
            this.inputBtn.Size = new System.Drawing.Size(33, 22);
            this.inputBtn.TabIndex = 4;
            this.inputBtn.Text = "...";
            this.inputBtn.UseVisualStyleBackColor = true;
            this.inputBtn.Click += new System.EventHandler(this.inputBtn_Click);
            // 
            // txtInputFile
            // 
            this.txtInputFile.Enabled = false;
            this.txtInputFile.Location = new System.Drawing.Point(98, 42);
            this.txtInputFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.Size = new System.Drawing.Size(267, 21);
            this.txtInputFile.TabIndex = 3;
            // 
            // rbFromFile
            // 
            this.rbFromFile.AutoSize = true;
            this.rbFromFile.Location = new System.Drawing.Point(5, 43);
            this.rbFromFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbFromFile.Name = "rbFromFile";
            this.rbFromFile.Size = new System.Drawing.Size(77, 16);
            this.rbFromFile.TabIndex = 2;
            this.rbFromFile.Text = "From File";
            this.rbFromFile.UseVisualStyleBackColor = true;
            // 
            // cmbInputLyr
            // 
            this.cmbInputLyr.FormattingEnabled = true;
            this.cmbInputLyr.Location = new System.Drawing.Point(98, 18);
            this.cmbInputLyr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbInputLyr.Name = "cmbInputLyr";
            this.cmbInputLyr.Size = new System.Drawing.Size(306, 20);
            this.cmbInputLyr.TabIndex = 1;
            this.cmbInputLyr.SelectedIndexChanged += new System.EventHandler(this.cmbInputLyr_SelectedIndexChanged);
            // 
            // rbFromLry
            // 
            this.rbFromLry.AutoSize = true;
            this.rbFromLry.Checked = true;
            this.rbFromLry.Location = new System.Drawing.Point(5, 19);
            this.rbFromLry.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbFromLry.Name = "rbFromLry";
            this.rbFromLry.Size = new System.Drawing.Size(83, 16);
            this.rbFromLry.TabIndex = 0;
            this.rbFromLry.TabStop = true;
            this.rbFromLry.Text = "From Layer";
            this.rbFromLry.UseVisualStyleBackColor = true;
            // 
            // gbDist
            // 
            this.gbDist.Controls.Add(this.cmbDistField);
            this.gbDist.Controls.Add(this.txtDist);
            this.gbDist.Controls.Add(this.rbDistField);
            this.gbDist.Controls.Add(this.rbDistFix);
            this.gbDist.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDist.Location = new System.Drawing.Point(0, 140);
            this.gbDist.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDist.Name = "gbDist";
            this.gbDist.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbDist.Size = new System.Drawing.Size(424, 81);
            this.gbDist.TabIndex = 1;
            this.gbDist.TabStop = false;
            this.gbDist.Text = "Buffer Distance";
            // 
            // cmbDistField
            // 
            this.cmbDistField.Enabled = false;
            this.cmbDistField.FormattingEnabled = true;
            this.cmbDistField.Location = new System.Drawing.Point(98, 40);
            this.cmbDistField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbDistField.Name = "cmbDistField";
            this.cmbDistField.Size = new System.Drawing.Size(306, 20);
            this.cmbDistField.TabIndex = 3;
            // 
            // txtDist
            // 
            this.txtDist.Location = new System.Drawing.Point(190, 15);
            this.txtDist.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDist.Name = "txtDist";
            this.txtDist.Size = new System.Drawing.Size(214, 21);
            this.txtDist.TabIndex = 2;
            // 
            // rbDistField
            // 
            this.rbDistField.AutoSize = true;
            this.rbDistField.Location = new System.Drawing.Point(5, 41);
            this.rbDistField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbDistField.Name = "rbDistField";
            this.rbDistField.Size = new System.Drawing.Size(53, 16);
            this.rbDistField.TabIndex = 1;
            this.rbDistField.Text = "Field";
            this.rbDistField.UseVisualStyleBackColor = true;
            // 
            // rbDistFix
            // 
            this.rbDistFix.AutoSize = true;
            this.rbDistFix.Checked = true;
            this.rbDistFix.Location = new System.Drawing.Point(5, 16);
            this.rbDistFix.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbDistFix.Name = "rbDistFix";
            this.rbDistFix.Size = new System.Drawing.Size(179, 16);
            this.rbDistFix.TabIndex = 0;
            this.rbDistFix.TabStop = true;
            this.rbDistFix.Text = "Fix Distance(use map unit)";
            this.rbDistFix.UseVisualStyleBackColor = true;
            // 
            // gbEndType
            // 
            this.gbEndType.Controls.Add(this.rbEndTypeFlat);
            this.gbEndType.Controls.Add(this.rbEndTypeRound);
            this.gbEndType.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbEndType.Location = new System.Drawing.Point(0, 221);
            this.gbEndType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbEndType.Name = "gbEndType";
            this.gbEndType.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbEndType.Size = new System.Drawing.Size(424, 53);
            this.gbEndType.TabIndex = 2;
            this.gbEndType.TabStop = false;
            this.gbEndType.Text = "End Type [Optional]";
            // 
            // rbEndTypeFlat
            // 
            this.rbEndTypeFlat.AutoSize = true;
            this.rbEndTypeFlat.Location = new System.Drawing.Point(76, 18);
            this.rbEndTypeFlat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbEndTypeFlat.Name = "rbEndTypeFlat";
            this.rbEndTypeFlat.Size = new System.Drawing.Size(47, 16);
            this.rbEndTypeFlat.TabIndex = 1;
            this.rbEndTypeFlat.TabStop = true;
            this.rbEndTypeFlat.Text = "FLAT";
            this.rbEndTypeFlat.UseVisualStyleBackColor = true;
            // 
            // rbEndTypeRound
            // 
            this.rbEndTypeRound.AutoSize = true;
            this.rbEndTypeRound.Location = new System.Drawing.Point(5, 18);
            this.rbEndTypeRound.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbEndTypeRound.Name = "rbEndTypeRound";
            this.rbEndTypeRound.Size = new System.Drawing.Size(53, 16);
            this.rbEndTypeRound.TabIndex = 0;
            this.rbEndTypeRound.TabStop = true;
            this.rbEndTypeRound.Text = "ROUND";
            this.rbEndTypeRound.UseVisualStyleBackColor = true;
            // 
            // gbLine
            // 
            this.gbLine.Controls.Add(this.rbLineRight);
            this.gbLine.Controls.Add(this.rbLineLeft);
            this.gbLine.Controls.Add(this.rbLineAll);
            this.gbLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbLine.Location = new System.Drawing.Point(0, 274);
            this.gbLine.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbLine.Name = "gbLine";
            this.gbLine.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbLine.Size = new System.Drawing.Size(424, 55);
            this.gbLine.TabIndex = 3;
            this.gbLine.TabStop = false;
            this.gbLine.Text = "Line Side [Optional]";
            // 
            // rbLineRight
            // 
            this.rbLineRight.AutoSize = true;
            this.rbLineRight.Location = new System.Drawing.Point(105, 18);
            this.rbLineRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbLineRight.Name = "rbLineRight";
            this.rbLineRight.Size = new System.Drawing.Size(53, 16);
            this.rbLineRight.TabIndex = 2;
            this.rbLineRight.TabStop = true;
            this.rbLineRight.Text = "RIGHT";
            this.rbLineRight.UseVisualStyleBackColor = true;
            // 
            // rbLineLeft
            // 
            this.rbLineLeft.AutoSize = true;
            this.rbLineLeft.Location = new System.Drawing.Point(52, 18);
            this.rbLineLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbLineLeft.Name = "rbLineLeft";
            this.rbLineLeft.Size = new System.Drawing.Size(47, 16);
            this.rbLineLeft.TabIndex = 1;
            this.rbLineLeft.TabStop = true;
            this.rbLineLeft.Text = "LEFT";
            this.rbLineLeft.UseVisualStyleBackColor = true;
            // 
            // rbLineAll
            // 
            this.rbLineAll.AutoSize = true;
            this.rbLineAll.Location = new System.Drawing.Point(5, 18);
            this.rbLineAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbLineAll.Name = "rbLineAll";
            this.rbLineAll.Size = new System.Drawing.Size(47, 16);
            this.rbLineAll.TabIndex = 0;
            this.rbLineAll.TabStop = true;
            this.rbLineAll.Text = "FULL";
            this.rbLineAll.UseVisualStyleBackColor = true;
            // 
            // gbOutput
            // 
            this.gbOutput.Controls.Add(this.label1);
            this.gbOutput.Controls.Add(this.outputBtn);
            this.gbOutput.Controls.Add(this.txtOutputFile);
            this.gbOutput.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbOutput.Location = new System.Drawing.Point(0, 81);
            this.gbOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbOutput.Name = "gbOutput";
            this.gbOutput.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbOutput.Size = new System.Drawing.Size(424, 59);
            this.gbOutput.TabIndex = 4;
            this.gbOutput.TabStop = false;
            this.gbOutput.Text = "Output Feature";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Output File:";
            // 
            // outputBtn
            // 
            this.outputBtn.Location = new System.Drawing.Point(371, 16);
            this.outputBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.outputBtn.Name = "outputBtn";
            this.outputBtn.Size = new System.Drawing.Size(33, 22);
            this.outputBtn.TabIndex = 1;
            this.outputBtn.Text = "...";
            this.outputBtn.UseVisualStyleBackColor = true;
            this.outputBtn.Click += new System.EventHandler(this.outputBtn_Click);
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(98, 18);
            this.txtOutputFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(267, 21);
            this.txtOutputFile.TabIndex = 0;
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(340, 345);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(64, 22);
            this.cancelBtn.TabIndex = 5;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // okBtn
            // 
            this.okBtn.Location = new System.Drawing.Point(270, 345);
            this.okBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(64, 22);
            this.okBtn.TabIndex = 6;
            this.okBtn.Text = "Ok";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // BufferWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.gbLine);
            this.Controls.Add(this.gbEndType);
            this.Controls.Add(this.gbDist);
            this.Controls.Add(this.gbOutput);
            this.Controls.Add(this.gbInput);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "BufferWizard";
            this.Size = new System.Drawing.Size(424, 373);
            this.gbInput.ResumeLayout(false);
            this.gbInput.PerformLayout();
            this.gbDist.ResumeLayout(false);
            this.gbDist.PerformLayout();
            this.gbEndType.ResumeLayout(false);
            this.gbEndType.PerformLayout();
            this.gbLine.ResumeLayout(false);
            this.gbLine.PerformLayout();
            this.gbOutput.ResumeLayout(false);
            this.gbOutput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbInput;
        private System.Windows.Forms.Button inputBtn;
        private System.Windows.Forms.TextBox txtInputFile;
        private System.Windows.Forms.RadioButton rbFromFile;
        private System.Windows.Forms.ComboBox cmbInputLyr;
        private System.Windows.Forms.RadioButton rbFromLry;
        private System.Windows.Forms.GroupBox gbDist;
        private System.Windows.Forms.TextBox txtDist;
        private System.Windows.Forms.RadioButton rbDistField;
        private System.Windows.Forms.RadioButton rbDistFix;
        private System.Windows.Forms.ComboBox cmbDistField;
        private System.Windows.Forms.GroupBox gbEndType;
        private System.Windows.Forms.RadioButton rbEndTypeFlat;
        private System.Windows.Forms.RadioButton rbEndTypeRound;
        private System.Windows.Forms.GroupBox gbLine;
        private System.Windows.Forms.RadioButton rbLineRight;
        private System.Windows.Forms.RadioButton rbLineLeft;
        private System.Windows.Forms.RadioButton rbLineAll;
        private System.Windows.Forms.GroupBox gbOutput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button outputBtn;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button okBtn;
    }
}
