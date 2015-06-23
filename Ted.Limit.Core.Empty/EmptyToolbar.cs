using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ted.Limit.Core.Empty
{
    public class EmptyToolbar : IToolbar , IMakable
    {
        public EmptyToolbar()
        {
            m_key = "Ted.Limit.Core.Empty.EmptyToolbar";
            m_name = "Empty";
            m_groups = new IGroup[] { new EmptyGroup() };
        }

        #region IToolbar 成员

        private string m_key;

        private IGroup[] m_groups;

        private string m_name;

        string IToolbar.Key
        {
            get { return m_key; }
        }

        IGroup[] IToolbar.Groups
        {
            get { return m_groups; }
        }

        string IToolbar.Name
        {
            get { return m_name; }
        }

        #endregion

        #region IMakable 成员

        private IMake m_make;

        IMake IMakable.Make
        {
            get { return m_make; }
        }

        public IMake Make
        {
            set
            {
                m_make = value;
            }
        }

        #endregion
    }
}
