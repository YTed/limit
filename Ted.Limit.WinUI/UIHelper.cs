using System;
using System.Collections.Generic;
using System.Text;

using Infragistics.Win.UltraWinToolbars;

using Ted.Limit.Core;

namespace Ted.Limit.WinUI
{
    public class UIHelper
    {
        private static string s_KeySeperator = "#";

        private static object s_application;

        public static object Application
        {
            get
            {
                return s_application;
            }
            set
            {
                s_application = value;
            }
        }

        public static string CreateKeyForTool(string moduleKey, string toolKey)
        {
            return moduleKey + s_KeySeperator + toolKey;
        }

        public static string[] SpiltKeysOfTool(string uiToolKey)
        {
            if (string.IsNullOrEmpty(uiToolKey))
            {
                throw new ArgumentNullException();
            }
            if (!uiToolKey.Contains(s_KeySeperator))
            {
                throw new ArgumentException("传入的工具标识为非法标识!");
            }
            return uiToolKey.Split(s_KeySeperator.ToCharArray());
        }

        private static ToolBase[] CreateGroup(
            IGroup iGrp, 
            UltraToolbarsManager uiTbrMgr, 
            string moduleKey
            )
        {
            IItem[] grpItm = iGrp.Items;
            int grpItmCount = grpItm.Length;
            ToolBase[] presentTools = new ToolBase[grpItmCount];

            for (int i = 0; i < grpItmCount; i++)
            {
                string tmpToolKey = CreateKeyForTool(moduleKey, grpItm[i].Key);
                presentTools[i] = UIToolFactory.PresentTool(grpItm[i], tmpToolKey, i == 0);
            }
            return presentTools;
        }

        /// <summary>
        /// 制造工具条.
        /// </summary>
        /// <param name="iTbr">工具条模型</param>
        /// <param name="uiTbrMgr">工具条管理器</param>
        public static void CreateToolbar(IToolbar iTbr, UltraToolbarsManager uiTbrMgr, string moduleKey)
        {
            if (iTbr == null)
            {
                throw new UIExtException("要加载的工具条为空!");
            }
            string tbrKey = UIHelper.CreateKeyForTool(moduleKey, iTbr.Key);
            if (uiTbrMgr.Toolbars.Exists(tbrKey))
            {
                throw new UIExtException("系统已加载具有与指定的工具条的关键字相同的工具条!");
            }
            bool addin = false;
            UltraToolbar newTbr = null;
            try
            {
                ((System.ComponentModel.ISupportInitialize)uiTbrMgr).BeginInit();
                newTbr = new UltraToolbar(tbrKey);
                newTbr.Text = iTbr.Name; 
                newTbr.DockedColumn = 30;
                newTbr.DockedRow = 1;
                newTbr.DockedPosition = DockedPosition.Floating;
                newTbr.FloatingSize = new System.Drawing.Size(1000, 30);

                uiTbrMgr.Toolbars.AddRange(new UltraToolbar[] { newTbr });
                addin = true;

                IGroup[] iGrp = iTbr.Groups;
                int grpCount = iGrp.Length;
                for (int i = 0; i < grpCount; i++)
                {
                    ToolBase[] nonInhTools = CreateGroup(iGrp[i], uiTbrMgr, moduleKey);
                    newTbr.NonInheritedTools.AddRange(nonInhTools);
                }
            }
            catch (Exception exp)
            {
                if (addin)
                {
                    uiTbrMgr.Toolbars.Remove(newTbr);
                }
                throw exp is UIExtException ? exp :
                    new UIExtException(
                        string.Format("加载工具条[{0}]时发生不可控异常!", iTbr.Name),
                        exp);
            }
            finally
            {
                ((System.ComponentModel.ISupportInitialize)uiTbrMgr).EndInit();
            }
        }

        /// <summary>
        /// 制造工具页
        /// </summary>
        /// <param name="iTb"></param>
        /// <param name="uiTbrMgr"></param>
        /// <param name="moduleKey"></param>
        public static void CreateTabs(ITab iTb, UltraToolbarsManager uiTbrMgr, string moduleKey)
        {
            if (iTb == null)
            {
                throw new UIExtException("要加载的工具页为空!");
            }
            string tbKey = CreateKeyForTool(moduleKey,iTb.Key);
            if (uiTbrMgr.Ribbon.Tabs.Exists(tbKey))
            {
                throw new UIExtException("系统已加载具有与指定的工具页的关键字相同的工具页!");
            }
            bool addin = false;
            RibbonTab tab = null;
            try
            {
                ((System.ComponentModel.ISupportInitialize)uiTbrMgr).BeginInit();
                tab = new RibbonTab(tbKey);
                tab.Caption = iTb.Name;

                uiTbrMgr.Ribbon.NonInheritedRibbonTabs.AddRange(new RibbonTab[] { tab });
                addin = true;

                IGroup[] iGrp = iTb.Groups;
                int grpCount = iGrp.Length;
                RibbonGroup[] pGrp = new RibbonGroup[grpCount];
                for (int i = 0; i < grpCount; i++)
                {
                    ToolBase[] tmpToolbaseArr =CreateGroup(iGrp[i], uiTbrMgr, moduleKey);
                    string grpKey = UIHelper.CreateKeyForTool(moduleKey, iGrp[i].Key);
                    pGrp[i] = new RibbonGroup(grpKey,"");
                    tab.Groups.Add(pGrp[i]);
                    pGrp[i].Tools.AddRange(tmpToolbaseArr);
                }
            }
            catch (Exception exp)
            {
                FormExtension.UIExceptionRaised(delegate
                {
                    if (addin)
                    {
                        uiTbrMgr.Ribbon.Tabs.Remove(tab);
                    }
                }, Common.Global.LOG_UI, exp, Ted.Limit.Common.LogLevel.Warn);
            }
            finally
            {
                ((System.ComponentModel.ISupportInitialize)uiTbrMgr).EndInit();
            }
        }

        public static void CreateTools(IItem[] items, UltraToolbarsManager uiTbrMgr, string moduleKey)
        {
            int itemCount = items.Length;
            ToolBase[] tools = new ToolBase[itemCount];
            for (int i = 0; i < itemCount; i++)
            {
                string toolkey = CreateKeyForTool(moduleKey, items[i].Key);
                IActive iAct = items[i] as IActive;
                if (iAct != null)
                {
                    iAct.OnCreate(s_application);
                }
                tools[i] = UIToolFactory.CreateTool(items[i], toolkey);
            }
            uiTbrMgr.Tools.AddRange(tools);
        }
    }
}
