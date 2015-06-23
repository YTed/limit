using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Drawing;

using Infragistics.Win.UltraWinToolbars;

using Ted.Limit.Core;
using Ted.Limit.Common;

namespace Ted.Limit.WinUI
{
    class UIToolFactory
    {
        public static List<IUIToolWorker> s_workers = new List<IUIToolWorker>();

        public static void Registry(IUIToolWorker worker)
        {
            if (worker == null)
            {
                throw new ArgumentNullException("注册到UI工厂的UI工人为不能为空值!");
            }
            s_workers.Add(worker);
        }

        public static void Unregistry(IUIToolWorker worker)
        {
            if (worker != null)
            {
                s_workers.Remove(worker);
            }
        }

        public static ToolBase CreateTool(IItem item, string key)
        {
            foreach (IUIToolWorker worker in s_workers)
            {
                if (worker.CanProduce(item))
                {
                    return worker.ProduceActualTool(item, key);
                }
            }
            throw new ArgumentException("该工具未定义!");
        }

        public static ToolBase PresentTool(IItem item, string key, bool beginGroup)
        {
            foreach (IUIToolWorker worker in s_workers)
            {
                if (worker.CanProduce(item))
                {
                    return worker.ProducePresentTool(item, key, beginGroup);
                }
            }
            throw new ArgumentException("该工具未定义!");
        }

        public static void FillShareProperties(Infragistics.Win.UltraWinToolbars.SharedProps sp, IItem itm)
        {
            try
            {
                sp.Caption = itm.Name;
                sp.Category = itm.Category;
                sp.ToolTipText = itm.Tooltip;
                sp.Enabled = itm.Enabled;
                Bitmap img = itm.SmallImage;
                if (img != null)
                {
                    if (sp.AppearancesSmall.Appearance.Image == null || !(sp.AppearancesSmall.Appearance.Image.Equals(img)))
                    {
                        sp.AppearancesSmall.Appearance = new Infragistics.Win.Appearance();
                        sp.AppearancesSmall.Appearance.Image = itm.SmallImage;
                    }
                }
                img = itm.LargeImage;
                if (img != null)
                {
                    if (sp.AppearancesLarge.Appearance.Image == null || !(sp.AppearancesLarge.Appearance.Image.Equals(img)))
                    {
                        sp.AppearancesLarge.Appearance = new Infragistics.Win.Appearance();
                        sp.AppearancesLarge.Appearance.Image = itm.LargeImage;
                    }
                }
            }
            catch (Exception exp)
            {
                LogManager.CreateInstance().Log(exp, LogLevel.Warn, Global.LOG_UI);
            }
        }

        public static void FillInstanceProps(InstanceProps instProps, bool isFirstInGroup)
        {
            instProps.PreferredSizeOnRibbon = RibbonToolSize.Large;
            instProps.MinimumSizeOnRibbon = RibbonToolSize.Large;
            instProps.IsFirstInGroup = isFirstInGroup;
        }
    }

    interface IUIToolWorker
    {
        bool CanProduce(IItem item);

        ToolBase ProduceActualTool(IItem item, string key);

        ToolBase ProducePresentTool(IItem item, string key, bool beginGroup);
    }

    public class UIToolRegistrer
    {
        /// <summary>
        /// 配置UI工厂,配置文件格式:
        /// ClassName@AssemblyPath\Assembly.dll
        /// 如果文件存在,则忽略但记录加载异常.
        /// </summary>
        /// <param name="workerCfg"></param>
        public static void Config(string workerCfg)
        {
            if (string.IsNullOrEmpty(workerCfg))
            {
                throw new ArgumentNullException("传递到UI工具注册器的UI工厂配置文件路径为空!");
            }
            if (!File.Exists(workerCfg))
            {
                throw new ArgumentException("找不到传递到UI工具注册器的UI工厂配置文件!");
            }
            try
            {
                string seperator = "@";
                string[] uiWkCfgTxt = File.ReadAllLines(workerCfg);
                Dictionary<string, Assembly> asmDict = new Dictionary<string, Assembly>();
                foreach (string wkCfg in uiWkCfgTxt)
                {
                    try
                    {
                        string tmpWkCfg = wkCfg.Trim();
                        if (wkCfg.Contains(seperator))
                        {
                            string[] kvPair = tmpWkCfg.Split(seperator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            Assembly asm = null;
                            if (kvPair.Length == 2)
                            {
                                kvPair[1] = kvPair[1].Trim();
                                if (!asmDict.ContainsKey(kvPair[1]))
                                {
                                    asm = Assembly.LoadFile(kvPair[1]);
                                    asmDict.Add(kvPair[1], asm);
                                }
                                else
                                {
                                    asm = asmDict[kvPair[1]];
                                }
                            }
                            else
                            {
                                asm = typeof(UIHelper).Assembly;
                            }
                            IUIToolWorker worker = asm.CreateInstance(kvPair[0].Trim()) as IUIToolWorker;
                            UIToolFactory.Registry(worker);
                        }
                    }
                    catch (Exception exp)
                    {
                        // catch and log and ignore the exception
                        Ted.Limit.Common.LogManager.CreateInstance().Log(
                            exp, 
                            Ted.Limit.Common.LogLevel.Warn, 
                            Ted.Limit.Common.Global.LOG_PROGRAME);
                    }
                }
            }
            catch (Exception exp)
            {
                throw new Exception("加载UI工厂配置文件过程中发生未知异常!", exp);
            }
        }
    }
}
