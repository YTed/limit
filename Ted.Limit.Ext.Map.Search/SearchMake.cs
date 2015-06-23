using System;
using System.Collections.Generic;
using System.Text;
using Ted.Limit.Core;

namespace Ted.Limit.Ext.Map.Search
{
    class SearchMake : IMake
    {
        private static IMake s_instance;

        public SearchMake()
        {
            m_key = "Search";
            m_consoles = new IConsole[] { };
            m_dckWindows = new IDockableWindow[] { AttributeViewDockableWindow.GetInstance() };
            m_tabs = new ITab[] { };
            m_toolbars = new IToolbar[] { new SearchToolbar() };
            List<IItem> itmLst = new List<IItem>();
            foreach (IToolbar iTbr in m_toolbars)
            {
                foreach (IGroup iGrp in iTbr.Groups)
                {
                    itmLst.AddRange(iGrp.Items);
                }
            }
            m_items = itmLst.ToArray();

            s_instance = this;
        }

        public static IMake GetInstance()
        {
            return s_instance;
        }

        #region IMake 成员

        private string m_key;

        string Ted.Limit.Core.IMake.Key
        {
            get { return m_key; }
        }

        private IToolbar[] m_toolbars;

        Ted.Limit.Core.IToolbar[] Ted.Limit.Core.IMake.Toolbars
        {
            get { return m_toolbars; }
        }

        private IDockableWindow[] m_dckWindows;

        Ted.Limit.Core.IDockableWindow[] Ted.Limit.Core.IMake.DockableWindows
        {
            get { return m_dckWindows; }
        }

        private ITab[] m_tabs;

        Ted.Limit.Core.ITab[] Ted.Limit.Core.IMake.Tabs
        {
            get { return m_tabs; }
        }

        private IItem[] m_items;

        Ted.Limit.Core.IItem[] Ted.Limit.Core.IMake.Items
        {
            get { return m_items; }
        }

        private IConsole[] m_consoles;

        Ted.Limit.Core.IConsole[] Ted.Limit.Core.IMake.Consoles
        {
            get { return m_consoles; }
        }

        #endregion
    }

    class SearchGroup : BaseGroup
    {
        public SearchGroup()
        {
            m_key = typeof(SearchGroup).FullName;
            m_items = new IItem[] { 
                new AttributeSearch()
            };
        }
    }

    class SearchToolbar : IToolbar
    {
        public SearchToolbar()
        {
            m_name = "Search";
            m_key = typeof(SearchToolbar).FullName;
            m_groups = new IGroup[] { 
                new SearchGroup() 
            };
        }

        #region IToolbar 成员

        private string m_name;

        private string m_key;

        private IGroup[] m_groups;

        string IToolbar.Name
        {
            get { return m_name; }
        }

        string IToolbar.Key
        {
            get { return m_key; }
        }

        IGroup[] IToolbar.Groups
        {
            get { return m_groups; }
        }

        #endregion
    }
}
