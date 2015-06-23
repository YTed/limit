using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Ted.Limit.Core;
using Ted.Limit.Common;

namespace Ted.Limit.ExtLoader
{
    public class ExtManager : IExtManager
    {
        private IShareUnit m_su;

        private ICommandPool m_cmdPool;

        public ExtManager()
        {
            m_su = new ShareUnit();
            m_cmdPool = new CommandPool();
        }

        #region IExtManager 成员

        void IExtManager.LoadShareUnit(string suPath)
        {
            string suText = File.ReadAllText(suPath);
            string loadResult = m_su.Load(suText);
            Common.LogManager.CreateInstance().Log(
                loadResult, 
                Ted.Limit.Common.LogLevel.Debug, 
                Common.Global.LOG_EXT);
        }

        void IExtManager.SaveShareUnit(string suPath)
        {
            string suText = m_su.TextFormat;
            string suParentPath = suPath.Substring(0, suPath.LastIndexOf("\\"));
            if (!Directory.Exists(suParentPath))
            {
                Directory.CreateDirectory(suParentPath);
                FileAttributes dirAttr = File.GetAttributes(suParentPath);
                dirAttr = dirAttr & ~FileAttributes.ReadOnly;
                File.SetAttributes(suParentPath, dirAttr);
            }
            File.WriteAllText(suPath, suText);
            FileAttributes fileAttr = File.GetAttributes(suPath);
            fileAttr = fileAttr & ~FileAttributes.ReadOnly;
            File.SetAttributes(suPath, fileAttr);
        }

        Ted.Limit.Core.IMake IExtManager.LoadExtDef(string path, ref string key)
        {
            IMake make = m_su.Add(path, out key);
            m_cmdPool.AddExtModule(key);
            IItem[] items = make.Items;
            foreach (IItem item in items)
            {
                item.OnPorpertyChanged += m_pcHandler;
                m_cmdPool.AddExtension(item.Key, item, key);
            }
            return make;
        }

        void IExtManager.LoadModule(string key)
        {
            IMake make = m_su.LoadModule(key);
            if (m_cmdPool.ExistModule(key))
            {
                return;
            }
            m_cmdPool.AddExtModule(key);
            IItem[] items = make.Items;
            int count = items.Length;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    items[i].OnPorpertyChanged += m_pcHandler;
                    IActive iAct = items[i] as IActive;
                    if (iAct != null)
                    {
                        iAct.OnCreate(m_app);
                    }
                    m_cmdPool.AddExtension(items[i].Key, items[i], key);
                }
                catch (Exception exp)
                {
                    LogManager.CreateInstance().Log(exp, LogLevel.Warn, Global.LOG_EXT);
                }
            }
            IConsole[] consoles = make.Consoles;
            count = consoles.Length;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    m_cmdPool.AddExtension(consoles[i].Command, consoles[i], key);
                }
                catch (Exception exp)
                {
                    LogManager.CreateInstance().Log(exp, LogLevel.Warn, Global.LOG_EXT);
                }
            }
            IDockableWindow[] windows = make.DockableWindows;
            count = windows.Length;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    windows[i].OnCreate(m_app);
                    m_cmdPool.AddExtension(windows[i].Key, windows[i], key);
                }
                catch (Exception exp)
                {
                    LogManager.CreateInstance().Log(exp, LogLevel.Warn, Global.LOG_EXT);
                }
            }
        }

        object IExtManager.GetTool(string key)
        {
            return m_cmdPool.GetExtension(key);
        }

        string IExtManager.GetResource(string key)
        {
            string moduleKey = m_cmdPool.GetModuleKey(key);
            return m_su.GetResourcePath(moduleKey);
        }

        IMake[] IExtManager.AllModules()
        {
            return m_su.AllModules();
        }

        private object m_app;

        private PorpertyChanged m_pcHandler;

        object IExtManager.Application
        {
            get
            {
                return m_app;
            }
            set
            {
                m_app = value;
                IApplication iApp = value as IApplication;
                if (iApp != null)
                {
                    m_pcHandler = iApp.PCHandler;
                }
            }
        }

        string IExtManager.GetModuleKey(string key)
        {
            return m_cmdPool.GetModuleKey(key);
        }

        void IExtManager.RemoveModule(string key)
        {
            m_cmdPool.RemoveExtModule(key);
            m_su.Remove(key);
        }

        #endregion
    }
}
