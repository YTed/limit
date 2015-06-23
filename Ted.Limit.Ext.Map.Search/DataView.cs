using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Ted.Limit.Common;

using ESRI.ArcGIS.Geodatabase;

namespace Ted.Limit.Ext.Map.Search
{
    public partial class DataView : UserControl
    {
        private DataTable m_dataSource;

        public DataView()
        {
            InitializeComponent();
        }

        /**
         * @pre cursor != null
         * @post fills the data of the row in the cursor to data grid.
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cursor"></param>
        public void DisplayData(ICursor cursor)
        {
            if (cursor == null)
            {
                throw new ArgumentNullException();
            }
            IFields pFields = cursor.Fields;
            CreateDataSource(pFields);
            FillDataSource(cursor);
            m_dataView.DataSource = m_dataSource;
            m_dataView.DataBind();
        }

        /**
         * @pre pFields != null
         * @post creates a DataTable that has the structure the pFields has
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pFields"></param>
        private void CreateDataSource(IFields pFields)
        {
            if (pFields == null)
            {
                throw new ArgumentNullException();
            }
            m_dataSource = new DataTable();
            int fCount = pFields.FieldCount;
            for (int i = 0; i < fCount; i++)
            {
                m_dataSource.Columns.Add(pFields.get_Field(i).Name, typeof(string));
            }
        }

        /**
         * @pre only be called by DisplayData
         * @post fills m_dataSource with the data pCursor contains.
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pCursor"></param>
        private void FillDataSource(ICursor pCursor)
        {
            IRow pRow = pCursor.NextRow();
            while (pRow != null)
            {
                string[] fmtData = Global.ExactData(pRow, SearchCommon.CommonRowFormatter);
                m_dataSource.Rows.Add(fmtData);
                pRow = pCursor.NextRow();
            }
        }
    }
}
