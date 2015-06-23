using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using stdole;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Geodatabase;

namespace Ted.Limit.Common
{
    public class Global
    {
        public static string
            LOG_UI = "uiLog",
            LOG_EXT = "extLog",
            LOG_PROGRAME = "pgLog";

        #region AE common libs

        /// <summary>
        /// 地图单位的字符串表示.
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static string esriUnitToString(ESRI.ArcGIS.esriSystem.esriUnits unit)
        {
            return unit.ToString().Substring(4);
        }

        /// <summary>
        /// 设备像素转换为地图距离
        /// </summary>
        /// <param name="mapCtrl"></param>
        /// <returns></returns>
        public static double ConvertDistance(IMapControlDefault mapCtrl)
        {
            IPoint pnt1 = mapCtrl.ToMapPoint(0, 0);
            IPoint pnt2 = mapCtrl.ToMapPoint(0, 1);
            return Math.Abs(pnt1.Y - pnt2.Y);
        }

        /// <summary>
        /// 把.Net图形转换为COM的IPictureDisp图像对象
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static IPictureDisp ConvertImage(Image img)
        {
            if (img == null)
            {
                throw new ArgumentNullException();
            }
            return ESRI.ArcGIS.ADF.COMSupport.OLE.GetIPictureDispFromBitmap(new Bitmap(img)) as IPictureDisp;
        }

        /**
         * @pre pRow != null
         *      rowFormatter != null
         * @post returns a string array
         *      if any pRow.get_value(i) makes rowFormatter returns true
         *          the array contains such row record that was formatted by rowFormatter.
         */
        /// <summary>
        /// 返回IRow记录的格式化数据
        /// </summary>
        /// <param name="pRow"></param>
        /// <param name="rowFormatter"></param>
        /// <returns></returns>
        public static string[] ExactData(IRow pRow, 
            EsriRowDataFormatter rowFormatter)
        {
            if (pRow == null || rowFormatter == null)
            {
                throw new ArgumentNullException();
            }
            int count = pRow.Fields.FieldCount;
            int addedCounter = 0;
            string[] fmtData = new string[count];
            for (int i = 0; i < count; i++)
            {
                string tmpData = string.Empty;
                if (rowFormatter(pRow.get_Value(i),pRow.Fields.get_Field(i), out tmpData))
                {
                    fmtData[addedCounter++] = tmpData;
                }
            }
            Array.Resize<string>(ref fmtData, addedCounter);
            return fmtData;
        }

        /**
         * @pre pRow != null
         *      rowFormatter != null
         *      fieldFormatter != null
         * @post returns a 2-D string array 
         *      strArr.length = 2;
         *      if any i makes fieldFormatter(pRow.Fields.get_Field(i)) and rowFormatter(pRow.get_Value(i)) returns true
         *          strArr[0] contains field fomatted name and strArr[1] contains row formatted data.
         */
        /// <summary>
        /// 返回IRow所属ITable包含的IFields的格式化名称和该记录的格式化数据
        /// </summary>
        /// <param name="pRow"></param>
        /// <param name="fieldFormatter"></param>
        /// <param name="rowFormatter"></param>
        /// <returns></returns>
        public static string[][] ExactData(IRow pRow, 
            EsriFieldNameFormatter fieldFormatter, 
            EsriRowDataFormatter rowFormatter)
        {
            if (pRow == null || fieldFormatter == null || rowFormatter == null)
            {
                throw new ArgumentNullException();
            }
            string[][] fmtData = new string[2][];
            int fCount = pRow.Fields.FieldCount,
                addedCount = 0;
            fmtData[0] = new string[fCount];
            fmtData[1] = new string[fCount];
            for (int i = 0; i < fCount; i++)
            {
                string tmpFName = string.Empty,
                    tmpRData = string.Empty;
                if (fieldFormatter(pRow.Fields.get_Field(i).Name, out tmpFName) &&
                    rowFormatter(pRow.get_Value(i),pRow.Fields.get_Field(i), out tmpRData))
                {
                    fmtData[0][addedCount] = tmpFName;
                    fmtData[1][addedCount] = tmpRData;
                    addedCount++;
                }
            }
            Array.Resize<string>(ref fmtData[0], addedCount);
            Array.Resize<string>(ref fmtData[1], addedCount);
            return fmtData;
        }

        /**
         * @pre all params != null
         * @post strArr[0] refers to formatted field names,
         *      and the rest refers to the formatted records witch field exists in strArr[0]
         *      any exception caused when formatted row data leads to "undefined" result.
         */
        /// <summary>
        /// 返回由游标指定的一堆格式化行记录.
        /// </summary>
        /// <param name="pCursor"></param>
        /// <param name="fieldFormatter"></param>
        /// <param name="rowFormatter"></param>
        /// <returns></returns>
        public static string[][] ExactData(ICursor pCursor,
            EsriFieldNameFormatter fieldFormatter,
            EsriRowDataFormatter rowFormatter)
        {
            if (pCursor == null ||
                fieldFormatter == null ||
                rowFormatter == null)
            {
                throw new ArgumentNullException();
            }
            List<string[]> fmtData = new List<string[]>();
            //格式化字段名,收集合法格式化的字段序号
            IFields pFields = pCursor.Fields;
            int count = pCursor.Fields.FieldCount;
            int addedCount = 0;
            int[] idxes = new int[count];
            string[] fldNames = new string[count];
            for (int i = 0; i < count; i++)
            {
                string tmpFldName = string.Empty;
                if (fieldFormatter(pFields.get_Field(i).Name, out tmpFldName))
                {
                    fldNames[addedCount++] = tmpFldName;
                }
            }
            Array.Resize<string>(ref fldNames, addedCount);
            fmtData.Add(fldNames);
            //根据合法格式化字段序号,格式化记录对应字段的值.
            //格式化失败则赋值为数据对象的字符串形式
            //出现任何异常赋值为"未定义"
            IRow pRow = pCursor.NextRow();
            while (pRow != null)
            {
                string[] rowDataArr = new string[addedCount];
                for (int i = 0; i < addedCount; i++)
                {
                    string tmpRowData = string.Empty;
                    try
                    {
                        object data = pRow.get_Value(idxes[i]);
                        if (!rowFormatter(data, 
                            pFields.get_Field(idxes[i]), 
                            out tmpRowData))
                        {
                            tmpRowData = data.ToString();
                        }
                    }
                    catch
                    {
                        tmpRowData = "未定义";
                    }
                    rowDataArr[i] = tmpRowData;
                }
                fmtData.Add(rowDataArr);
                pRow = pCursor.NextRow();
            }
            return fmtData.ToArray();
        }

        #endregion
    }

    /// <summary>
    /// 字段名格式化代理
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="fmtName"></param>
    /// <returns></returns>
    public delegate bool EsriFieldNameFormatter(string fieldName, out string fmtName);

    /// <summary>
    /// 行记录格式化代理
    /// </summary>
    /// <param name="rowData"></param>
    /// <param name="pField"></param>
    /// <param name="fmtData"></param>
    /// <returns></returns>
    public delegate bool EsriRowDataFormatter(object rowData, IField pField, out string fmtData);
}
