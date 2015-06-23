using System;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

namespace Ted.Limit.Ext.Map.Standard
{
    class TOCContextMenu
    {
        private static IToolbarMenu s_FeatureToolbarMenu;

        private static IToolbarMenu CreatureFeatureMenu(object hook)
        {
            IToolbarMenu iTbrMenu = new ToolbarMenuClass();

            ESRI.ArcGIS.SystemUI.ICommand removeLyr = new EARemoveLayerCmd();
            removeLyr.OnCreate(hook);

            iTbrMenu.AddItem(removeLyr, 0, -1, false, ESRI.ArcGIS.SystemUI.esriCommandStyles.esriCommandStyleIconOnly);
            return iTbrMenu;
        }

        private static IToolbarMenu s_RasterToolbarMenu;

        private static IToolbarMenu CreateRasterMenu(object hook)
        {
            return null;
        }

        public static IToolbarMenu CreateLayerMenu(ILayer layer, object hook)
        {
            IToolbarMenu iTbrMenu = null;
            if (layer is IFeatureLayer)
            {
                if (s_FeatureToolbarMenu == null)
                {
                    s_FeatureToolbarMenu = CreatureFeatureMenu(hook);
                }
                iTbrMenu = s_FeatureToolbarMenu;
            }
            else if (layer is IRasterLayer)
            {
                if (s_RasterToolbarMenu == null)
                {
                    s_RasterToolbarMenu = CreateRasterMenu(hook);
                }
                iTbrMenu = s_RasterToolbarMenu;
            }

            if (iTbrMenu != null)
            {
                int count = iTbrMenu.Count;
                for (int i = 0; i < count; i++)
                {
                    ITOCContextCommand item = iTbrMenu.GetItem(i) as ITOCContextCommand;
                    if (item != null)
                    {
                        item.Target = layer;
                    }
                }
            }

            return iTbrMenu;
        }
    }

    interface ITOCContextCommand
    {
        ILayer Target { get;set;}
    }
}
