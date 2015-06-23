using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.ExtLoader
{
    class CommandPool : ICommandPool
    {
        /// <summary>
        /// 模块键-工具键表
        /// </summary>
        private Dictionary<string, List<string>> m_ModulePool;

        /// <summary>
        /// 工具键-工具
        /// </summary>
        private Dictionary<string, object> m_CommandPool;

        /// <summary>
        /// 工具键-模块键
        /// </summary>
        private Dictionary<string, string> m_moduleCommandPool;

        public CommandPool()
        {
            m_ModulePool = new Dictionary<string, List<string>>();
            m_CommandPool = new Dictionary<string, object>();
            m_moduleCommandPool = new Dictionary<string, string>();
        }

        #region ICommandPool 成员

        void ICommandPool.AddExtModule(string key)
        {
            if (m_ModulePool.ContainsKey(key))
            {
                throw new ExtExistException();
            }
            List<string> cmdKeyLst = new List<string>();
            m_ModulePool.Add(key, cmdKeyLst);
        }

        void ICommandPool.RemoveExtModule(string key)
        {
            if (m_ModulePool.ContainsKey(key))
            {
                List<string> cmdKeyLst = m_ModulePool[key];
                foreach (string cmdkey in cmdKeyLst)
                {
                    m_CommandPool.Remove(cmdkey);
                    m_moduleCommandPool.Remove(cmdkey);
                }
                m_ModulePool.Remove(key);
            }
        }

        bool ICommandPool.ExistModule(string key)
        {
            return m_ModulePool.ContainsKey(key);
        }

        void ICommandPool.AddExtension(string key, object item, string moduleKey)
        {
            if (m_CommandPool.ContainsKey(key))
            {
                throw new ExtExistException();
            }
            if (!m_ModulePool.ContainsKey(moduleKey))
            {
                throw new ExtLoadingException();
            }
            List<string> cmdKeyLst = m_ModulePool[moduleKey];
            cmdKeyLst.Add(key);
            m_CommandPool.Add(key, item);
            m_moduleCommandPool.Add(key, moduleKey);
        }

        void ICommandPool.AddExtRange(Dictionary<string, object> items, string moduleKey)
        {
            if (!m_ModulePool.ContainsKey(moduleKey))
            {
                throw new ExtLoadingException();
            }
            List<string> cmdKeyLst = m_ModulePool[moduleKey];
            foreach (KeyValuePair<string, object> pair in items)
            {
                m_CommandPool.Add(pair.Key, pair.Value);
                cmdKeyLst.Add(pair.Key);
                m_moduleCommandPool.Add(pair.Key, moduleKey);
            }
        }

        object ICommandPool.GetExtension(string key)
        {
            if (!m_CommandPool.ContainsKey(key))
            {
                throw new ExtNotFoundException();
            }
            return m_CommandPool[key];
        }

        void ICommandPool.RemoveExtension(string key)
        {
            if (m_CommandPool.ContainsKey(key))
            {
                m_CommandPool.Remove(key);
            }
        }

        bool ICommandPool.ExistExtension(string key)
        {
            return m_CommandPool.ContainsKey(key);
        }

        string ICommandPool.GetModuleKey(string toolKey)
        {
            if (!m_moduleCommandPool.ContainsKey(toolKey))
            {
                throw new ExtNotFoundException();
            }
            return m_moduleCommandPool[toolKey];
        }

        #endregion
    }
}
