using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Ted.Limit.Core;

namespace Ted.Limit.ExtLoader
{
    public class ExtManager : IExtManager
    {
        private IShareUnit m_su;

        private ICommandPool m_cmdPool;

        public ExtManager()
        {
            m_su = new ShareUnit();

        }

        #region IExtManager 成员

        void IExtManager.LoadShareUnit(string suPath)
        {
            string suText = File.ReadAllText(suPath);
            m_su.Load(suText);
        }

        void IExtManager.SaveShareUnit(string suPath)
        {
            string suText = m_su.TextFormat;
            File.WriteAllText(suPath, suText);
        }

        Ted.Limit.Core.IMake IExtManager.LoadExtDef(string path, ref string key)
        {
            return m_su.Add(path, out key);
        }

        void IExtManager.LoadModule(string key)
        {
            IMake make = m_su.LoadModule(key);
            IItem[] items = make.Items;
            int count = items.Length;
            for (int i = 0; i < count; i++)
            {
                m_cmdPool.AddExtension(items[i].Key, items[i], key);
            }
            IConsole[] consoles = make.Consoles;
            for (int i = 0; i < count; i++)
            {
                m_cmdPool.AddExtension(consoles[i].Command, consoles[i], key);
            }
        }

        object IExtManager.GetTool(string key)
        {
            return m_cmdPool.GetExtension(key);
        }

        #endregion
    }
}
