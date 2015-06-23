using System;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Geodatabase;

using Ted.Limit.Common;

namespace Ted.Limit.Ext.Map.Search
{
    class SearchCommon
    {
        public const string c_CATEGORY = "Search";

        public static EsriRowDataFormatter CommonRowFormatter
        {
            get
            {
                return s_rowFmt;
            }
        }

        public static EsriFieldNameFormatter CommonFieldFormatter
        {
            get
            {
                return s_fldFmt;
            }
        }

        private static EsriRowDataFormatter s_rowFmt = new EsriRowDataFormatter(RowDataFormatter);

        private static EsriFieldNameFormatter s_fldFmt = new EsriFieldNameFormatter(FieldNameFormatter);

        private static bool RowDataFormatter(object data, IField pField, out string fmtData)
        {
            if (pField == null)
            {
                throw new ArgumentNullException();
            }
            if (data == null)
            {
                fmtData = "null";
                return true;
            }
            fmtData = string.Empty;
            switch (pField.Type)
            {
                case esriFieldType.esriFieldTypeBlob:
                    fmtData = "二进制数据";
                    break;
                case esriFieldType.esriFieldTypeGeometry:
                    fmtData = "几何对象";
                    break;
                case esriFieldType.esriFieldTypeRaster:
                    fmtData = "栅格数据";
                    break;
            }
            if (string.IsNullOrEmpty(fmtData))
            {
                fmtData = data.ToString();
            }
            return true;
        }

        private static bool FieldNameFormatter(string fldName, out string fmtName)
        {
            if (string.IsNullOrEmpty(fldName))
            {
                throw new ArgumentNullException();
            }
            fmtName = fldName;
            return true;
        }
    }
}
