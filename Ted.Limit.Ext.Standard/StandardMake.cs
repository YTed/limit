using System;
using System.Collections.Generic;
using System.Text;

using Ted.Limit.Core;

namespace Ted.Limit.Ext.Map.Standard
{
    class StandardToolbar : BaseToolbar
    {
        public StandardToolbar()
        {
            m_key = typeof(StandardToolbar).FullName;
            m_name = "Standard";
            m_groups = new IGroup[]{
                new FileGroup(),
                new NavigatorGroup(),
                new ViewGroup()
            };
        }
    }

    public class StandardMake : IMake
    {
        public StandardMake()
        {
            m_toolbars = new IToolbar[]{
                new StandardToolbar()
            };
            int itmCount = 0;
            foreach (IToolbar tbr in m_toolbars)
            {
                foreach (IGroup grp in tbr.Groups)
                {
                    itmCount += grp.Items.Length;
                }
            }
            m_items = new IItem[itmCount];
            int offset = 0;
            foreach (IToolbar tbr in m_toolbars)
            {
                foreach (IGroup grp in tbr.Groups)
                {
                    Array.Copy(grp.Items,0, m_items, offset, grp.Items.Length);
                    offset += grp.Items.Length;
                }
            }

            s_instance = this;
        }

        private static IMake s_instance;

        public static IMake GetInstance()
        {
            return s_instance;
        }

        #region IMake 成员

        private string m_key = "Standard";

        private IItem[] m_items;

        string IMake.Key
        {
            get { return m_key; }
        }

        private IToolbar[] m_toolbars;

        IToolbar[] IMake.Toolbars
        {
            get { return m_toolbars; }
        }

        private IDockableWindow[] m_windows;

        IDockableWindow[] IMake.DockableWindows
        {
            get 
            {
                if (m_windows == null)
                {
                    m_windows = new IDockableWindow[]{
                        new TOCDockableWindow()
                    };
                }
                return m_windows;
            }
        }

        ITab[] IMake.Tabs
        {
            get { return new ITab[] { }; }
        }

        IItem[] IMake.Items
        {
            get
            {
                return m_items;
            }
        }

        IConsole[] IMake.Consoles
        {
            get { return new IConsole[] { }; }
        }

        #endregion
    }
}
