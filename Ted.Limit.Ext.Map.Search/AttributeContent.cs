using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using Infragistics.Win.UltraWinTree;

using Ted.Limit.Common;

namespace Ted.Limit.Ext.Map.Search
{
    public partial class AttributeContent : UserControl
    {
        public AttributeContent()
        {
            InitializeComponent();

            CreateEmptyDataSource();
            m_AttrGrid.DataSource = m_GridDataSource;
        }

        private DataTable m_GridDataSource;

        /// <summary>
        /// 
        /// </summary>
        private void CreateEmptyDataSource()
        {
            m_GridDataSource = new DataTable();
            m_GridDataSource.Columns.Add("字段", typeof(string));
            m_GridDataSource.Columns.Add("值", typeof(string));
        }

        /**
         * @pre pFeat != null
         * @post LyrTree.Nodes.IndexOf(pFeat.Class.AliasName) != -1
         *         pFeat contained in LyrTree.Nodes[pFeat.Class.AliasName].Nodes as one of the tag.
         */
        private void AddLayerNode(IFeature pFeat)
        {
            IObjectClass iObjCls = pFeat.Class;
            string key = iObjCls.AliasName;
            int idx = m_LyrTree.Nodes.IndexOf(key);
            UltraTreeNode node = null;
            if (idx == -1)
            {
                node = m_LyrTree.Nodes.Add(key);
                node.Text = iObjCls.AliasName;
            }
            else
            {
                node = m_LyrTree.Nodes[idx];
            }
            string subNodeTxt = string.Empty;
            if (pFeat.HasOID)
            {
                subNodeTxt = pFeat.OID.ToString();
            }
            else
            {
                subNodeTxt = pFeat.get_Value(0).ToString();
            }
            UltraTreeNode subNode = node.Nodes.Add();
            subNode.Text = subNodeTxt;
            subNode.Tag = pFeat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumFeat"></param>
        public void DisplaySelection(IEnumFeature enumFeat)
        {
            if(enumFeat == null)
            {
                throw new ArgumentNullException();
            }
            ClearData();
            enumFeat.Reset();
            IFeature pFeat = enumFeat.Next();
            while (pFeat != null)
            {
                AddLayerNode(pFeat);
                pFeat = enumFeat.Next();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearData()
        {
            m_LyrTree.Nodes.Clear();
            m_GridDataSource.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_LyrTree_AfterSelect(object sender, SelectEventArgs e)
        {
            if (e.NewSelections.Count != 0)
            {
                UltraTreeNode node = e.NewSelections.All[0] as UltraTreeNode;
                IFeature pFeat = node.Tag as IFeature;
                FillGridData(pFeat);
            }
        }

        /**
         * @pre pFeat != null
         * @post m_GridDataSource is filled with pFeat's data.
         *      and calls m_AttrGrid.DataBind()
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pFeat"></param>
        private void FillGridData(IFeature pFeat)
        {
            if (pFeat != null)
            {
                m_GridDataSource.Clear();
                
                string[][] pFeatData = Global.ExactData(
                    pFeat.Table.GetRow(pFeat.OID), 
                    SearchCommon.CommonFieldFormatter, 
                    SearchCommon.CommonRowFormatter);
                int fldCount = pFeat.Fields.FieldCount;
                for (int i = 0; i < fldCount; i++)
                {
                    m_GridDataSource.Rows.Add(new object[] { pFeatData[0][i], pFeatData[1][i] });
                }
                m_AttrGrid.DataBind();
            }
        }

    }
}
