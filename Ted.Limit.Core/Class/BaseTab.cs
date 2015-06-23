using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.Core
{
    public class BaseTab : ITab
    {
        protected BaseTab()
        {

        }

        #region ITab 成员

        protected string m_key;

        protected string m_name;

        protected IGroup[] m_groups;

        public virtual string Key
        {
            get { return m_key; }
        }

        public virtual string Name
        {
            get { return m_name; }
        }

        public virtual IGroup[] Groups
        {
            get { return m_groups; }
        }

        #endregion
    }
}
