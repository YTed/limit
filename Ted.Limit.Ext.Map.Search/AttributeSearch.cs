using System;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

using Ted.Limit.Core;
using ESRI.ArcGIS.Geometry;
using Ted.Limit.Common;
using ESRI.ArcGIS.Geodatabase;

namespace Ted.Limit.Ext.Map.Search
{
    class AttributeSearch : BaseTool
    {
        private ISelectionEnvironment m_env;

        public AttributeSearch()
        {
            m_category = SearchCommon.c_CATEGORY;
            m_key = typeof(AttributeSearch).FullName;
            m_largeImgFile = "attr_srch.gif";
            m_name = "Attribute Search";
            m_smallImgFile = m_largeImgFile;
            m_tooltip = "search feature by attribute ..";

            m_env = new SelectionEnvironmentClass();
            m_env.AreaSearchDistance = 1;
            m_env.AreaSelectionMethod = ESRI.ArcGIS.Geodatabase.esriSpatialRelEnum.esriSpatialRelIntersects;
            m_env.LinearSearchDistance = 1;
            m_env.LinearSelectionMethod = ESRI.ArcGIS.Geodatabase.esriSpatialRelEnum.esriSpatialRelCrosses;
            m_env.PointSearchDistance = 1;
            m_env.PointSelectionMethod = ESRI.ArcGIS.Geodatabase.esriSpatialRelEnum.esriSpatialRelContains;
            m_env.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
            m_env.SearchTolerance = 4;
        }

        public override void OnClick()
        {
            IMapControlDefault mapCtrl = m_app.MapControl;
            mapCtrl.CurrentTool = null;
            mapCtrl.MousePointer = esriControlsMousePointer.esriPointerIdentify;

            m_app.ShowDockableWindow(AttributeViewDockableWindow.GetInstance());
        }

        public override void Deactive()
        {
            IMapControlDefault mapCtrl = m_app.MapControl;
            mapCtrl.MousePointer = esriControlsMousePointer.esriPointerDefault;
        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            if (button == 1)
            {
                IMapControlDefault mapCtrl = m_app.MapControl;
                IPoint hitPnt = mapCtrl.ToMapPoint(x, y);
                IGeometry srchGeom = ((ITopologicalOperator)hitPnt).Buffer(m_env.SearchTolerance * Global.ConvertDistance(mapCtrl));
                mapCtrl.Map.SelectByShape(srchGeom, m_env, false);
                IEnumFeature enumFeat = mapCtrl.Map.FeatureSelection as IEnumFeature;
                mapCtrl.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, mapCtrl.Extent);

                IDockableWindow dckWin = AttributeViewDockableWindow.GetInstance();
                m_app.ShowDockableWindow(dckWin);
                AttributeContent ac = dckWin.Content as AttributeContent;
                ac.ClearData();
                ac.DisplaySelection(enumFeat);
            }
        }
    }

    class FeatureQuery : BaseCommand
    {

    }
}
