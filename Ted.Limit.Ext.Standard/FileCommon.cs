using System;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

using Ted.Limit.Core;

namespace Ted.Limit.Ext.Standard
{
    class FileCommon
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

        private static ICommand[] s_cmds = new ICommand[]{
            new NewFileCmd(),
            new OpenFileCmd(),
            new SaveFileCmd(),
            new SaveFileAsCmd(),
            new AddDataCmd()
        };

        public static ICommand GetCommand(CmdType type)
        {
            return s_cmds[(int)type];
        }
    }

    enum CmdType
    {
        New = 0,
        Open = 1,
        Save = 2,
        SaveAs = 3,
        AddData = 4
    }
}
