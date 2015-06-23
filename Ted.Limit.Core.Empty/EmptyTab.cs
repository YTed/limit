using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.Core.Empty
{
    class EmptyTab : ITab
    {
        public EmptyTab()
        {
            m_key = "Ted.Limit.Core.Empty.EmptyTab";
            m_name = "Empty";
            m_groups = new IGroup[] { new EmptyGroup() };
        }

        #region ITab 成员

        private string m_key;

        private string m_name;

        private IGroup[] m_groups;

        string ITab.Key
        {
            get
            {
                return m_key;
            }
        }

        string ITab.Name
        {
            get
            {
                return m_name;
            }
        }

        IGroup[] ITab.Groups
        {
            get
            {
                return m_groups;
            }
        }

        #endregion
    }
}
