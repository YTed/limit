using System;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Analyst3D;

namespace Ted.Limit.Ext.Scene.Navigator
{
    class Common
    {
        public const string c_CATEGROTY = "Scene Navigator";

        public static IPoint LocatePoint(ISceneGraph sceneGph, int x, int y)
        {
            ISceneViewer sceneVw = sceneGph.ActiveViewer;
            IPoint hitPnt;
            object pOwner, pObj;
            sceneGph.Locate(sceneVw, x, y, esriScenePickMode.esriScenePickAll, true, out hitPnt, out pOwner, out pObj);
            return hitPnt;
        }
    }
}
