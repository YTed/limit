using System;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;

using Ted.Limit.Core;

namespace Ted.Limit.Ext.Map.Standard
{
    class StandardCommon
    {
        public const string c_CATEGORY = "Standard";

        public static void SaveMxdFile(IMapControlDefault mapCtrl, string fileDest, bool useRltPath, bool createThumbnail)
        {
            IMapDocument doc = new MapDocumentClass();
            try
            {
                if (doc.get_IsReadOnly(fileDest))
                {
                    return;
                }
                doc.Open(fileDest, string.Empty);
                doc.ReplaceContents(mapCtrl.Map as IMxdContents);
                doc.SaveAs(fileDest, useRltPath, createThumbnail);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                doc.Close();
            }
        }

        public static void FixZoom(IMapControlDefault mapCtrlDft, bool zoomIn)
        {
            int factor = zoomIn ? -1 : 1;
            IEnvelope pEnv = mapCtrlDft.Extent;
            double halfX = (pEnv.XMax - pEnv.XMin) / 4;
            double halfY = (pEnv.YMax - pEnv.YMin) / 4;
            double halfZ = (pEnv.ZMax - pEnv.ZMin) / 4;
            double halfM = (pEnv.MMax - pEnv.MMin) / 4;
            if (!(double.IsNaN(halfX) && double.IsNaN(halfY)))
            {
                pEnv.Expand(factor * halfX, factor * halfY, false);
            }
            if (!double.IsNaN(halfZ))
            {
                pEnv.ExpandM(factor * halfM, false);
            }
            if (!double.IsNaN(halfM))
            {
                pEnv.ExpandZ(factor * halfZ, false);
            }
            mapCtrlDft.Extent = pEnv;
        }

        public static IItem GetCommand(ItemType type)
        {
            return s_cmds[(int)type];
        }

        private static IItem[] s_cmds = new IItem[]{
            new NewFileCmd(),
            new OpenFileCmd(),
            new SaveFileCmd(),
            new SaveFileAsCmd(),
            new AddDataCmd(),
            new ZoomIn(),
            new ZoomOut(),
            new FixZoomIn(),
            new FixZoomOut(),
            new Pan(),
            new FullExtent(),
            new SelectFeature(),
            new ClearSelection()
        };
    }

    enum ItemType
    {
        New = 0,
        Open = 1,
        Save = 2,
        SaveAs = 3,
        AddData = 4,
        ZoomIn = 5,
        ZoomOut = 6,
        FixZoomIn = 7,
        FixZoomOut = 8,
        Pan = 9,
        FullExtent = 10,
        SelectFeature = 11,
        ClearSelection = 12
    }
}
