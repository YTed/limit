using System;
using System.Collections.Generic;
using System.Text;
using Ted.Limit.Core;

namespace Ted.Limit.Ext.Map.Analyst
{
    class AnalystMake : IMake
    {
        public AnalystMake()
        {
            m_toolbars = new IToolbar[] {
                new AnalystToolbar()
            };
            m_dckWins = new IDockableWindow[] { };
            m_tabs = new ITab[] { };
            m_consoles = new IConsole[] { };

            List<IItem> itmLst = new List<IItem>();
            for (int i = 0; i < m_toolbars.Length; i++)
            {
                IGroup[] tmpGrp = m_toolbars[i].Groups;
                for (int j = 0; j < tmpGrp.Length; j++)
                {
                    itmLst.AddRange(tmpGrp[j].Items);
                }
            }
            m_items = itmLst.ToArray();
        }

        #region IMake 成员

        string m_key = "Analyst";

        IToolbar[] m_toolbars;

        IDockableWindow[] m_dckWins;

        ITab[] m_tabs;

        IItem[] m_items;

        IConsole[] m_consoles;

        public string Key
        {
            get { return m_key; }
        }

        public IToolbar[] Toolbars
        {
            get { return m_toolbars; }
        }

        public IDockableWindow[] DockableWindows
        {
            get { return m_dckWins; }
        }

        public ITab[] Tabs
        {
            get { return m_tabs; }
        }

        public IItem[] Items
        {
            get { return m_items; }
        }

        public IConsole[] Consoles
        {
            get { return m_consoles; }
        }

        #endregion
    }

    class AnalystToolbar : IToolbar
    {
        public AnalystToolbar()
        {
            m_groups = new IGroup[]{
                new BufferGroup()
            };
        }

        #region IToolbar 成员

        private IGroup[] m_groups;

        public string Name
        {
            get { return "Analyst"; }
        }

        public string Key
        {
            get { return typeof(AnalystToolbar).FullName; }
        }

        public IGroup[] Groups
        {
            get { return m_groups; }
        }

        #endregion
    }

    class BufferGroup : BaseGroup
    {
        public BufferGroup()
        {
            m_key = typeof(BufferGroup).FullName;
            m_items = new IItem[] { new BufferCommand() };
        }
    }
}
