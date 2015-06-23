using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Ted.Limit.Core.Empty
{
    class EmptyMake : IMake
    {
        public EmptyMake()
        {
            m_key = "Empty";
            IToolbar tbr = new EmptyToolbar();
            m_toolbars = new IToolbar[] { tbr };
            m_tabs = new ITab[] { new EmptyTab() };
            m_dckWindows = new IDockableWindow[] { new EmptyDockableWindow() };
            m_consoles = new IConsole[] { };
            m_items = tbr.Groups[0].Items;
            s_instance = this;
        }

        private static IMake s_instance;

        public static IMake GetInstance()
        {
            return s_instance;
        }

        #region IMake 成员

        private string m_key;

        private IToolbar[] m_toolbars;

        private IDockableWindow[] m_dckWindows;

        private ITab[] m_tabs;

        private IItem[] m_items;

        private IConsole[] m_consoles;

        //---------------------------------------------------

        string IMake.Key
        {
            get { return m_key; }
        }

        IToolbar[] IMake.Toolbars
        {
            get { return m_toolbars; }
        }

        IDockableWindow[] IMake.DockableWindows
        {
            get { return m_dckWindows; }
        }

        ITab[] IMake.Tabs
        {
            get { return m_tabs; }
        }

        IItem[] IMake.Items
        {
            get { return m_items; }
        }

        IConsole[] IMake.Consoles
        {
            get { return m_consoles; }
        }

        #endregion

        private static string m_logFile = "tool.log";

        private static StringBuilder m_builder = new StringBuilder();

        private static void ToolLog(string msg)
        {
            m_builder.Append(msg);
        }

        private static void FlushLog()
        {
            string dir = Directory.GetCurrentDirectory();
            File.AppendAllText(dir + "\\" + m_logFile, m_builder.ToString());
        }
    }
}
