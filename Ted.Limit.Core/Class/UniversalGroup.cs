using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.Core
{
    public class UniversalGroup : BaseGroup
    {        
        private List<IItem> m_itemLst;
        public UniversalGroup(string key)
        {
            m_key = key;
            m_itemLst = new List<IItem>();
        }

        public void AddItem(IItem item)
        {
            m_itemLst.Add(item);
        }

        public override IItem[] Items
        {
            get
            {
                return m_itemLst.ToArray();
            }
        }
    }
}
