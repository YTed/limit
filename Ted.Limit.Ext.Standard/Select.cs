using System;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using Ted.Limit.Common;

namespace Ted.Limit.Ext.Map.Standard
{
    class SelectFeature : Ted.Limit.Core.BaseTool
    {
        private ISelectionEnvironment m_selEnv;

        public SelectFeature()
        {
            m_category = StandardCommon.c_CATEGORY;
            m_key = typeof(SelectFeature).FullName;
            m_largeImgFile = "select_feature.png";
            m_name = "Select Feature";
            m_smallImgFile = m_largeImgFile;
            m_tooltip = "select feature..";

            m_selEnv = new SelectionEnvironmentClass();
            m_selEnv.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
            m_selEnv.AreaSelectionMethod = ESRI.ArcGIS.Geodatabase.esriSpatialRelEnum.esriSpatialRelIntersects;
            m_selEnv.LinearSelectionMethod = ESRI.ArcGIS.Geodatabase.esriSpatialRelEnum.esriSpatialRelCrosses;
            m_selEnv.PointSelectionMethod = ESRI.ArcGIS.Geodatabase.esriSpatialRelEnum.esriSpatialRelContains;
            m_selEnv.AreaSearchDistance = 10;
            m_selEnv.LinearSearchDistance = 10;
            m_selEnv.PointSearchDistance = 10;
            m_selEnv.SearchTolerance = 4;
        }

        public override void OnClick()
        {
            IMapControlDefault mapCtrl = m_app.MapControl;
            mapCtrl.CurrentTool = null;
            mapCtrl.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerCrosshair;
        }

        public override void Deactive()
        {
            m_app.MapControl.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            IMapControlDefault mapCtrl = m_app.MapControl;
            IMap map = mapCtrl.Map;
            IPoint hitPnt = mapCtrl.ToMapPoint(x, y);
            IGeometry pSrchGeom = ((ITopologicalOperator)hitPnt).Buffer(Global.ConvertDistance(mapCtrl) * m_selEnv.SearchTolerance);
            map.SelectByShape(pSrchGeom, m_selEnv, false);
            mapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, m_app.MapControl.Extent);
        }
    }

    class ClearSelection : Ted.Limit.Core.BaseCommand
    {
        public ClearSelection()
        {
            m_category = StandardCommon.c_CATEGORY;
            m_key = typeof(ClearSelection).FullName;
            m_largeImgFile = "clear_selection.png";
            m_name = "Clear Selection";
            m_smallImgFile = m_largeImgFile;
            m_tooltip = "clear selection..";
        }

        public override void OnClick()
        {
            IMapControlDefault mapCtrl = m_app.MapControl;
            IMap map = mapCtrl.Map;
            map.ClearSelection();
            mapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, mapCtrl.Extent);
        }
    }
}
