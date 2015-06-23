using System;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Controls;

using Ted.Limit.Core;
using Ted.Limit.Ext.Scene.Search;
using Ted.Limit.Ext.Scene.File;
using Ted.Limit.Ext.Scene.Contour;
using Ted.Limit.Ext.Scene.ViewShed;

namespace Ted.Limit.Ext.Scene.Navigator
{
    public class NavigatorMake : IMake
    {
        public NavigatorMake()
        {
            m_key = Common.c_CATEGROTY;
            m_toolbars = new IToolbar[] { };
            m_tabs = new ITab[] { 
                new BaseTab(),
                new AnaylstTab()
            };
            m_dckWindows = new IDockableWindow[] { };
            m_consoles = new IConsole[] { };
            List<IItem> itemLst = new List<IItem>();
            foreach (IToolbar tbr in m_toolbars)
            {
                foreach (IGroup grp in tbr.Groups)
                {
                    itemLst.AddRange(grp.Items);
                }
            }
            foreach (ITab tab in m_tabs)
            {
                foreach (IGroup grp in tab.Groups)
                {
                    itemLst.AddRange(grp.Items);
                }
            }
            m_items = itemLst.ToArray();
        }

        #region IMake 成员

        private string m_key;

        private IToolbar[] m_toolbars;

        private IDockableWindow[] m_dckWindows;

        private ITab[] m_tabs;

        private IItem[] m_items;

        private IConsole[] m_consoles;

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
            get { return m_dckWindows; }
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

    class NavigatorTab : BaseTab
    {
        public NavigatorTab()
        {
            string keyPrefix = typeof(NavigatorTab).FullName;

            UniversalGroup group = new UniversalGroup(keyPrefix + ".Group");

            string[] navNames = {
                "Navigate",
                "Fly",
                "Zoom In/Out",
                "Narrow Field Of View",
                "Expand Field Of View",
                "Zoom To Target",
                "Zoom In",
                "Zoom Out",
                "Pan",
                "Full Extent"
            };
            string[] navImage = {

            };
            ESRI.ArcGIS.SystemUI.ICommand[] realCommands =
                new ESRI.ArcGIS.SystemUI.ICommand[]{
                    new ControlsSceneNavigateToolClass(),
                    new ControlsSceneFlyToolClass(),
                    new ControlsSceneZoomInOutToolClass(),
                    new ControlsSceneNarrowFOVCommandClass(),
                    new ControlsSceneExpandFOVCommandClass(),
                    new ControlsSceneTargetZoomToolClass(),
                    new ControlsSceneZoomInToolClass(),
                    new ControlsSceneZoomOutToolClass(),
                    new ControlsScenePanToolClass(),
                    new ControlsSceneFullExtentCommandClass()
                };

            for (int i = 0; i < navNames.Length; i++)
            {
                group.AddItem(
                    new LazyTool(
                        string.Format(
                            "{0}.{1}", 
                            keyPrefix, 
                            navNames[i]),
                    navNames[i], 
                    realCommands[i], 
                    navImage[Math.Min(i, navImage.Length)],
                    navNames[i], 
                    "Scene Navigator"));
            }

            m_groups = new IGroup[] { group };
            m_key = typeof(BaseTab).FullName;
            m_name = "Scene Navigator";
        }
    }

    class AnaylstTab : BaseTab
    {
        public AnaylstTab()
        {
            m_key = typeof(AnaylstTab).FullName;

            UniversalGroup group = new UniversalGroup(m_key + ".Group");

            group.AddItem(new ContourTool());
            group.AddItem(new ViewShedCommand());
            group.AddItem(new SceneLocator());

            m_groups = new IGroup[] { group };
            m_name = "Scene Analyst";
        }
    }
}
