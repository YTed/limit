using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Ted.Limit.ExtLoader;
using Ted.Limit.Core;
using Ted.Limit.Common;
using System.Threading;

namespace Ted.Limit.WinUI
{
    public partial class ExtView : Form
    {
        private IExtManager m_ExtMgr;

        private MainFrame m_mainFrame;

        public ExtView(IExtManager ExtMgr, MainFrame frame)
        {
            InitializeComponent();

            m_ExtMgr = ExtMgr;
            m_mainFrame = frame;
        }

        public new void Show()
        {
            newModuleTxt.Text = string.Empty;
            FetchModules();

            base.Show();
        }

        private void FetchModules()
        {
            moduleLstView.Clear();
            toolLstView.Clear();
            IMake[] makers = m_ExtMgr.AllModules();
            int count = makers.Length;
            ListViewItem[] items = new ListViewItem[count];
            for (int i = 0; i < count; i++)
            {
                items[i] = new ListViewItem(makers[i].Key);
            }
            moduleLstView.Items.AddRange(items);
        }

        private void ExtView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void removeModuleBtn_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in moduleLstView.SelectedItems)
            {
                try
                {
                    m_mainFrame.RemoveExtension(item.Text);
                }
                catch (Exception exp)
                {
                    StartErrorThread(string.Format("移除模块{0}过程中出现异常!", item.Text), exp, LogLevel.Warn);
                }
            }
            FetchModules();
        }

        private OpenFileDialog m_openFileDlg;

        private void loadModuleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string key = string.Empty;
                IMake make = m_ExtMgr.LoadExtDef(newModuleTxt.Text, ref key);
                m_mainFrame.AddExtension(make);
            }
            catch (ExtNotFoundException extNFExp)
            {
                StartErrorThread("未能找到指定的扩展模块!", extNFExp, LogLevel.Warn);
            }
            catch (ExtDefFormatException extFmtExp)
            {
                StartErrorThread("扩展定义文件格式错误!", extFmtExp, LogLevel.Warn);
            }
            catch (ExtLoadingException extLoadExp)
            {
                StartErrorThread("扩展模块加载错误!", extLoadExp, LogLevel.Warn);
            }
            catch (UIExtException uiExtExp)
            {
                StartErrorThread("扩展模块加载错误!", uiExtExp, LogLevel.Warn);
            }
            catch (Exception exp)
            {
                StartErrorThread("加载扩展模块时发生未知错误!", exp, LogLevel.Warn);
            }
            FetchModules();
        }

        private void StartErrorThread(string msg , Exception exp , LogLevel logLvl)
        {
            FormExtension.UIExceptionRaised(delegate{errorLbl.Text = msg;},
                Ted.Limit.Common.Global.LOG_UI, exp, logLvl);
            new Thread(new ThreadStart(FadeOut)).Start();
        }

        private void FadeOut()
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                FormExtension.UIThread(this, delegate
                {
                    errorLbl.Text = "";
                });
            }
            catch (Exception exp)
            {
                FormExtension.UIExceptionRaisedAsyn(this,
                    delegate { },
                    Ted.Limit.Common.Global.LOG_UI,
                    exp, LogLevel.Error);
            }
        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            if (m_openFileDlg == null)
            {
                m_openFileDlg = new OpenFileDialog();
                m_openFileDlg.Filter = "ExtDef 文件|*.ted";
                m_openFileDlg.Multiselect = false;
            }
            DialogResult dr = m_openFileDlg.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                newModuleTxt.Text = m_openFileDlg.FileName;
            }
        }

        private void moduleLstView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string moduleKey = moduleLstView.SelectedItems[0].Text;
                IMake[] make = m_ExtMgr.AllModules();
                foreach (IMake selected in make)
                {
                    if ((moduleLstView.SelectedItems.Count > 0) && (moduleKey.Equals(selected.Key)))
                    {
                        toolLstView.Items.Clear();
                        IItem[] items = selected.Items;
                        foreach (IItem item in items)
                        {
                            ListViewItem lvItm = toolLstView.Items.Add(new ListViewItem(item.Name));
                        }
                        break;
                    }
                }
            }
            catch (Exception exp)
            {
                FormExtension.UIExceptionRaised(delegate { }, Global.LOG_UI, exp, LogLevel.Warn);
            }
        }
    }
}