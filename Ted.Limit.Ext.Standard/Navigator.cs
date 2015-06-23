using System;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;

using Ted.Limit.Core;

namespace Ted.Limit.Ext.Map.Standard
{
    class Pan : BaseTool
    {
        public Pan()
        {
            m_category = StandardCommon.c_CATEGORY;
            m_checked = false;
            m_key = typeof(Pan).FullName;
            m_largeImgFile = "pan.gif";
            m_smallImgFile = "pan.gif";
            m_name = "Pan";
            m_tooltip = "pan";
        }

        private ESRI.ArcGIS.SystemUI.ICommand m_pan;

        public override void OnClick()
        {
            if (m_pan == null)
            {
                m_pan = new ControlsMapPanToolClass();
                m_pan.OnCreate(m_app.ToolbarBuddy);
            }
            m_app.MapControl.CurrentTool = m_pan as ESRI.ArcGIS.SystemUI.ITool;
        }
    }

    class FullExtent : BaseCommand
    {
        public FullExtent()
        {
            m_category = StandardCommon.c_CATEGORY;
            m_key = typeof(FullExtent).FullName;
            m_largeImgFile = "globe.gif";
            m_smallImgFile = "globe.gif";
            m_name = "Full Extent";
            m_tooltip = "full extent...";
        }

        public override void OnClick()
        {
            IMapControlDefault mapCtrlDft = m_app.MapControl;
            mapCtrlDft.Extent = mapCtrlDft.FullExtent;
        }
    }

    class FixZoomIn : BaseCommand
    {
        public FixZoomIn()
        {
            m_category = StandardCommon.c_CATEGORY;
            m_key = typeof(FixZoomIn).FullName;
            m_largeImgFile = "fix_zoom_in.gif";
            m_smallImgFile = "fix_zoom_in.gif";
            m_name = "Fix Zoom In";
            m_tooltip = "fix zoom in...";
        }

        public override void OnClick()
        {
            IMapControlDefault mapCtrlDft = m_app.MapControl;
            StandardCommon.FixZoom(mapCtrlDft, true);
        }
    }

    class FixZoomOut : BaseCommand
    {
        public FixZoomOut()
        {
            m_category = StandardCommon.c_CATEGORY;
            m_key = typeof(FixZoomOut).FullName;
            m_largeImgFile = "fix_zoom_out.gif";
            m_smallImgFile = "fix_zoom_out.gif";
            m_name = "Fix Zoom Out";
            m_tooltip = "fix zoom out...";
        }

        public override void OnClick()
        {
            IMapControlDefault mapCtrlDft = m_app.MapControl;
            StandardCommon.FixZoom(mapCtrlDft, false);
        }
    }

    class ZoomIn : BaseTool
    {
        public ZoomIn()
        {
            m_category = StandardCommon.c_CATEGORY;
            m_key = typeof(ZoomIn).FullName;
            m_largeImgFile = "zoom_in.gif";
            m_smallImgFile = "zoom_in.gif";
            m_name = "Zoom In";
            m_tooltip = "zoom in...";
        }

        public override void OnClick()
        {
            IMapControlDefault mapCtrlDft = m_app.MapControl;
            mapCtrlDft.MousePointer = esriControlsMousePointer.esriPointerZoomIn;
        }

        public override void Deactive()
        {
            IMapControlDefault mapCtrlDft = m_app.MapControl;
            mapCtrlDft.MousePointer = esriControlsMousePointer.esriPointerDefault;
        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            if (button == 1)
            {
                IMapControlDefault mapCtrlDft = m_app.MapControl;
                IEnvelope pEnv = mapCtrlDft.TrackRectangle();
                mapCtrlDft.Extent = pEnv;
            }
        }
    }

    class ZoomOut : BaseTool
    {
        public ZoomOut()
        {
            m_category = StandardCommon.c_CATEGORY;
            m_key = typeof(ZoomOut).FullName;
            m_largeImgFile = "zoom_out.gif";
            m_smallImgFile = "zoom_out.gif";
            m_name = "Zoom Out";
            m_tooltip = "zoom out...";
        }

        public override void OnClick()
        {
            IMapControlDefault mapCtrlDft = m_app.MapControl;
            mapCtrlDft.MousePointer = esriControlsMousePointer.esriPointerZoomOut;
        }

        public override void Deactive()
        {
            IMapControlDefault mapCtrlDft = m_app.MapControl;
            mapCtrlDft.MousePointer = esriControlsMousePointer.esriPointerDefault;
        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            if (button == 1)
            {
                IMapControlDefault mapCtrlDft = m_app.MapControl;
                IEnvelope pCrrEnt = mapCtrlDft.Extent;
                IEnvelope pEnv = mapCtrlDft.TrackRectangle();
                //以此矩形为新的显示范围,则为求一矩形,
                //使得它的某边长度L与对应的现有矩形L'及所画矩形L''有如下关系:
                //              (L'^2) / L'' = L
                pCrrEnt.Width = pCrrEnt.Width * pCrrEnt.Width / pEnv.Width;
                pCrrEnt.Height = pCrrEnt.Height * pCrrEnt.Height / pEnv.Height;
                pCrrEnt.Depth = pCrrEnt.Depth * pCrrEnt.Depth / pEnv.Depth;
                mapCtrlDft.Extent = pCrrEnt;
            }
        }
    }

    class NavigatorGroup : BaseGroup
    {
        public NavigatorGroup()
        {
            m_key = typeof(NavigatorGroup).FullName;
            m_items = new IItem[]{
                StandardCommon.GetCommand(ItemType.ZoomIn),
                StandardCommon.GetCommand(ItemType.ZoomOut),
                StandardCommon.GetCommand(ItemType.FixZoomIn),
                StandardCommon.GetCommand(ItemType.FixZoomOut),
                StandardCommon.GetCommand(ItemType.Pan),
                StandardCommon.GetCommand(ItemType.FullExtent),
                StandardCommon.GetCommand(ItemType.SelectFeature),
                StandardCommon.GetCommand(ItemType.ClearSelection)
            };
        }
    }
}
