using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.ExtLoader
{
    class CommandPool : ICommandPool
    {
        private Dictionary<string, List<string>> m_ModulePool;

        private Dictionary<string, object> m_CommandPool;

        public CommandPool()
        {
            m_ModulePool = new Dictionary<string, List<string>>();
            m_CommandPool = new Dictionary<string, object>();
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
            List<string> cmdKeyLst = m_ModulePool[key];
            cmdKeyLst.Add(key);
            m_CommandPool.Add(key, item);
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

        #endregion
    }
}
