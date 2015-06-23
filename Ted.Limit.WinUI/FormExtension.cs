using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Ted.Limit.Common;

namespace Ted.Limit.WinUI
{
    /**
     * 关于此类，请参见http://www.cnblogs.com/fangfan4060/archive/2009/07/16/1525158.html
     */
    public class FormExtension
    {
        /// <summary>
        /// 安全的异步更新窗体方法.
        /// </summary>
        /// <param name="form">要更新的控件所属窗体</param>
        /// <param name="code">更新代码代理</param>
        public static void UIThread(
            System.Windows.Forms.Form form, 
            System.Windows.Forms.MethodInvoker code)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(code);
            }
            else
            {
                code.Invoke();
            }
        }

        /// <summary>
        /// 多线程中,UI中异常触发时的安全异步提示/日志/处理方法
        /// </summary>
        /// <param name="form">发生异常时用户正在操作的窗体</param>
        /// <param name="code">异常处理代码(提示/修复)</param>
        /// <param name="logKey">log关键字</param>
        /// <param name="exp">log异常</param>
        /// <param name="lvl">log等级</param>
        public static void UIExceptionRaisedAsyn(
            System.Windows.Forms.Form form,             //发生异常的窗体
            System.Windows.Forms.MethodInvoker code,    //用户提示
            string logKey,                              //log关键字
            Exception exp,                              //log异常
            Ted.Limit.Common.LogLevel lvl               //log等级
            )
        {
            UIThread(form, code);
            Ted.Limit.Common.LogManager.CreateInstance().Log(exp, lvl, logKey);
        }

        /// <summary>
        /// 单线程时触发异常的处理方法.
        /// </summary>
        /// <param name="code">异常处理代码</param>
        /// <param name="logKey">log关键字</param>
        /// <param name="exp">log异常</param>
        /// <param name="lvl">log等级</param>
        public static void UIExceptionRaised(
            System.Windows.Forms.MethodInvoker code,    //用户提示
            string logKey,                              //log关键字
            Exception exp,                              //log异常
            Ted.Limit.Common.LogLevel lvl               //log等级
            )
        {
            code.Invoke();
            Ted.Limit.Common.LogManager.CreateInstance().Log(exp, lvl, logKey);
        }

        private static EventHandler 
            SetChildrenTopMostTrue = new EventHandler(ActiveChildren),
            SetChildrenTopMostFalse = new EventHandler(DeactiveChildren);

        public static void BindParentChild(Form parent, Form child)
        {
            if (parent != null && child != null)
            {
                if (!s_formParent.ContainsKey(parent))
                {
                    s_formParent.Add(parent, new List<Form>());
                    parent.Activated += SetChildrenTopMostTrue;
                    parent.Deactivate += SetChildrenTopMostFalse;
                }
                List<Form> formChild = s_formParent[parent];
                if (!formChild.Contains(child))
                {
                    formChild.Add(child);
                }
            }
        }

        public static void UnbindParentChild(Form parent, Form child)
        {
            List<Form> children = null;
            if (s_formParent.TryGetValue(parent, out children))
            {
                if (children.Contains(child))
                {
                    children.Remove(child);
                }
            }
        }

        private static Dictionary<Form, List<Form>> s_formParent = new Dictionary<Form, List<Form>>();

        private static void ActiveChildren(object sender, EventArgs e)
        {
            SetChildrenTopMost(sender, true);
        }

        private static void DeactiveChildren(object sender, EventArgs e)
        {
            SetChildrenTopMost(sender, false);
        }

        private static void SetChildrenTopMost(object sender, bool topmost)
        {
            Form parent = sender as Form;
            List<Form> children = null;
            if (s_formParent.TryGetValue(parent,out children))
            {
                Form[] childArr = children.ToArray();
                foreach (Form child in childArr)
                {
                    if (child.IsDisposed)
                    {
                        children.Remove(child);
                    }
                    else if (!child.IsDisposed
                        && child.Visible
                        && (child.TopMost ^ topmost)
                        )
                    {
                        child.TopMost = topmost;
                    }
                }
            }
        }

        public static void StartLogging()
        {
            try
            {
                string logCfg = DirectoryHelper.GetConfigFile(ConfiguraionFile.Log);
                LogManager.CreateInstance().Start(logCfg);
            }
            catch (Exception exp)
            {
                // ignore the exception
            }
        }

        public static void RegistUIWorkers()
        {
            try
            {
                WinUI.UIToolRegistrer.Config(DirectoryHelper.GetConfigFile(ConfiguraionFile.UIFactory));
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
