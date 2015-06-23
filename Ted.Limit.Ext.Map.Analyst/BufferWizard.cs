using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Geoprocessor;

using Ted.Limit.Common;

namespace Ted.Limit.Ext.Map.Analyst
{
    partial class BufferWizard : UserControl
    {
        IMapControlDefault m_mapCtrl;

        private Buffer m_buffer;

        private BufferInnerForm m_innerForm;

        public BufferWizard()
        {
            InitializeComponent();

            m_FeatLyrLst = new List<IFeatureLayer>();
            m_openDlg = new OpenFileDialog();
            m_openDlg.Filter = "Shape File (*.shp) | *.shp";
            m_openDlg.Multiselect = false;
            m_outDlg = new SaveFileDialog();
            m_outDlg.Filter = m_openDlg.Filter;
            BindUILogic();

            m_buffer = Buffer.GetInstance();
        }

        internal void SetInnerForm(BufferInnerForm innerForm)
        {
            m_innerForm = innerForm;
        }

        private void BindUILogic()
        {
            LogicRelative lrFromFile = new LogicRelative(rbFromFile);
            lrFromFile.AddRelative(txtInputFile);
            lrFromFile.AddRelative(inputBtn);

            LogicRelative lrFromLayer = new LogicRelative(rbFromLry);
            lrFromLayer.AddRelative(cmbInputLyr);

            LogicRelative lrDistFix = new LogicRelative(rbDistFix);
            lrDistFix.AddRelative(txtDist);

            LogicRelative lrDistField = new LogicRelative(rbDistField);
            lrDistField.AddRelative(cmbDistField);
        }

        private class LogicRelative
        {
            private RadioButton m_rdBtn;

            private List<Control> m_ctrlLst;

            private EventHandler m_handler;

            public LogicRelative(RadioButton target)
            {
                m_rdBtn = target;
                m_ctrlLst = new List<Control>();
                Bind();
            }

            public void AddRelative(Control ctrl)
            {
                m_ctrlLst.Add(ctrl);
            }

            private void handle(object sender, EventArgs e)
            {
                foreach (Control c in m_ctrlLst)
                {
                    c.Enabled = m_rdBtn.Checked;
                }
            }

            public void Bind()
            {
                if (m_handler == null)
                {
                    m_handler = new EventHandler(handle);
                    m_rdBtn.CheckedChanged += m_handler;
                }
            }
        }

        public IMapControlDefault MapControl
        {
            get
            {
                return m_mapCtrl;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                m_mapCtrl = value;
                FillLyrCbx();
            }
        }

        private List<IFeatureLayer> m_FeatLyrLst;

        private void FillLyrCbx()
        {
            cmbInputLyr.Items.Clear();
            m_FeatLyrLst.Clear();
            int count = m_mapCtrl.LayerCount;
            for (int i = 0; i < count; i++)
            {
                IFeatureLayer pFeatLyr = m_mapCtrl.get_Layer(i) as IFeatureLayer;
                if (pFeatLyr != null && pFeatLyr.FeatureClass != null)
                {
                    cmbInputLyr.Items.Add(pFeatLyr.Name);
                    m_FeatLyrLst.Add(pFeatLyr);
                }
            }
        }

        private void FillFieldCbx(IFields pFields)
        {
            cmbDistField.Items.Clear();
            cmbDistField.Text = "";
            int count = pFields.FieldCount;
            for (int i = 0; i < count; i++)
            {
                IField pTmpField = pFields.get_Field(i);
                if (pTmpField.Type == esriFieldType.esriFieldTypeDouble ||
                    pTmpField.Type == esriFieldType.esriFieldTypeInteger ||
                    pTmpField.Type == esriFieldType.esriFieldTypeSingle ||
                    pTmpField.Type == esriFieldType.esriFieldTypeSmallInteger)
                {
                    cmbDistField.Items.Add(pTmpField.Name);
                }
            }
        }

        private void cmbInputLyr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDistField.Enabled)
            {
                IFeatureLayer pSelectedLyr = GetLyr();
                if (pSelectedLyr != null)
                {
                    IFields pFields = pSelectedLyr.FeatureClass.Fields;
                    FillFieldCbx(pFields);
                }
            }
        }

        private IFeatureLayer GetLyr()
        {
            int idx = cmbInputLyr.SelectedIndex;
            if(idx != -1)
            {
                return m_FeatLyrLst[idx];
            }
            return null;
        }

        private OpenFileDialog m_openDlg;

        private void inputBtn_Click(object sender, EventArgs e)
        {
            DialogResult dlgRsl = m_openDlg.ShowDialog(this);
            if (dlgRsl == DialogResult.OK)
            {
                txtInputFile.Text = m_openDlg.FileName;
                string fileFolder = DirectoryHelper.GetParentPath(m_openDlg.FileName);
                string filename = DirectoryHelper.GetFileName(m_openDlg.FileName);

                IWorkspaceFactory wkspFct = new ShapefileWorkspaceFactoryClass();
                IFeatureWorkspace pFeatWksp = wkspFct.OpenFromFile(fileFolder, this.Handle.ToInt32()) as IFeatureWorkspace;
                IFeatureClass pFeatCls = pFeatWksp.OpenFeatureClass(filename);
                if (pFeatCls != null)
                {
                    IFields pFields = pFeatCls.Fields;
                    FillFieldCbx(pFields);
                }
            }
        }

        private SaveFileDialog m_outDlg;

        private void outputBtn_Click(object sender, EventArgs e)
        {
            DialogResult dlgRsl = m_outDlg.ShowDialog(this);
            if (dlgRsl == DialogResult.OK)
            {
                txtOutputFile.Text = m_outDlg.FileName;
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            //缓冲区选项设置
            string inputFeature = GetInputFeature();
            string outputFeature = GetOutputFeature();
            string distance = GetDistance();
            m_buffer.ConstructBufferProcess(
                inputFeature,outputFeature, distance, 
                m_lineSide, m_endType);
            //缓冲分析
            string[] messages = m_buffer.Run();
            //分析信息显示
            this.SuspendLayout();

            TextBox msgBox = new TextBox();
            msgBox.Multiline = true;
            msgBox.ReadOnly = true;
            msgBox.Dock = DockStyle.Fill;
            foreach (string msg in messages)
            {
                msgBox.Text = msgBox.Text + msg + "\r\n";
            }
            this.Controls.Clear();
            this.Controls.Add(msgBox);

            this.ResumeLayout();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            m_innerForm.Close();
        }

        #region buffer option

        private string m_endType = string.Empty, 
            m_lineSide = string.Empty;

        private void SelectEndType(object sender, EventArgs e)
        {
            m_endType = ((RadioButton)sender).Text;
        }

        private void SelectLineSide(object sender, EventArgs e)
        {
            m_lineSide = ((RadioButton)sender).Text;
        }

        private string GetInputFeature()
        {
            string inputFeature = string.Empty;
            if (rbFromFile.Checked)
            {
                inputFeature = txtInputFile.Text;
            }
            else
            {
                IFeatureLayer pFeatLyr = GetLyr();
                if (pFeatLyr != null)
                {
                    string datasetName = string.Empty;
                    string path = string.Empty;
                    IDataset pDataset = pFeatLyr.FeatureClass.FeatureDataset as IDataset;
                    IWorkspace pWksp = null;
                    if (pDataset == null)
                    {
                        pDataset = pFeatLyr.FeatureClass as IDataset;
                        datasetName = pDataset.BrowseName + ".shp";
                    }
                    else
                    {
                        datasetName = pDataset.BrowseName + "\\" + pFeatLyr.FeatureClass.AliasName;
                    }
                    pWksp = pDataset.Workspace;
                    path = pWksp.PathName;
                    inputFeature = path + "\\" + datasetName;
                }
            }
            return inputFeature; 
        }

        private string GetOutputFeature()
        {
            return txtOutputFile.Text;
        }

        private string GetDistance()
        {
            string dist = string.Empty;
            if (rbDistFix.Checked)
            {
                dist = txtDist.Text + " " + Global.esriUnitToString(m_mapCtrl.MapUnits);
            }
            else
            {
                dist = cmbDistField.SelectedItem.ToString();
            }
            return dist;
        }

        #endregion
    }


    class Buffer
    {
        private static Buffer s_instance;

        public static Buffer GetInstance()
        {
            if (s_instance == null)
            {
                s_instance = new Buffer();
            }
            return s_instance;
        }

        private Buffer()
        {
            m_geoprocessor = new Geoprocessor();
        }

        private Geoprocessor m_geoprocessor;

        private ESRI.ArcGIS.AnalysisTools.Buffer m_process;

        public string[] Run()
        {
            m_geoprocessor.Execute(m_process, null);
        
            List<string> msgLst = new List<string>();
            int count = m_geoprocessor.MessageCount;
            for (int i = 0; i < count; i++)
            {
                msgLst.Add(m_geoprocessor.GetMessage(i));
            }
            return msgLst.ToArray();
        }

        public void ConstructBufferProcess(
            string inFeature,
            string outFeature,
            string distance,
            string line_side,
            string line_end_type)
        {
            if (m_process == null)
            {
                m_process = new ESRI.ArcGIS.AnalysisTools.Buffer();
            }
            m_process.in_features = inFeature;
            m_process.out_feature_class = outFeature;
            m_process.buffer_distance_or_field = distance;
            m_process.line_side = line_side;
            m_process.line_end_type = line_end_type;
        }
    }

}
