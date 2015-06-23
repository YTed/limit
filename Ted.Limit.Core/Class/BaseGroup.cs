using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.Core
{
    public class BaseGroup : IGroup
    {
        protected BaseGroup()
        {

        }

        #region IGroup 成员

        protected string m_key;

        protected IItem[] m_items;

        public virtual string Key
        {
            get { return m_key; }
        }

        public virtual IItem[] Items
        {
            get 
            {
                return m_items;
            }
        }

        #endregion
    }
}
