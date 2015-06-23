using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ted.Limit.Core.Empty
{
    public class EmptyGroup : IGroup , IMakable
    {
        public EmptyGroup()
        {
            m_key = "Ted.Limit.Core.Empty.EmptyGroup";
            m_items = new IItem[] { 
                new EmptyTool(),
                new EmptyCommand()
            };
        }

        #region IGroup 成员

        private string m_key;

        private IItem[] m_items;

        string IGroup.Key
        {
            get { return m_key; }
        }

        IItem[] IGroup.Items
        {
            get { return m_items; }
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
