using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.Core
{
    public class BaseToolbar : IToolbar
    {
        protected BaseToolbar()
        {

        }

        #region IToolbar 成员

        protected string m_name;

        protected string m_key;

        protected IGroup[] m_groups;

        public virtual string Name
        {
            get { return m_name; }
        }

        public virtual string Key
        {
            get { return m_key; }
        }

        public virtual IGroup[] Groups
        {
            get { return m_groups; }
        }

        #endregion
    }
}
