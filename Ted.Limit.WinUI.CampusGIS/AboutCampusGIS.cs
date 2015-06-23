using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace Ted.Limit.WinUI.CampusGIS
{
    partial class AboutCampusGIS : Form
    {
        public AboutCampusGIS()
        {
            InitializeComponent();

            //  初始化 AboutBox 以显示程序集信息中包含的产品信息。
            //  也可以通过以下方法更改应用程序的程序集信息设置:
            //  - 项目->属性->应用程序->程序集信息
            //  - AssemblyInfo.cs
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
        }

        #region 程序集属性访问器

        public string AssemblyTitle
        {
            get
            {
                return "SCAU CampusGIS";
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                return
@"说明：
    此软件及其源代码是开放的。你可以从svn://svnhost.cn/ctin/trunk/DotNet/Ted/Limit免费获得本软件的源代码（但不包括它所使用的组件库）。
    版权所无，翻版不究。
";
            }
        }

        public string AssemblyProduct
        {
            get
            {
                return "SCAU CampusGIS";
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                return "Copyleft@ 2009";
            }
        }

        public string AssemblyCompany
        {
            get
            {

                return "Wrote by Ted";
            }
        }
        #endregion

        private void AboutCampusGIS_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
