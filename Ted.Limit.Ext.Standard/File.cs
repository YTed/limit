using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;

using Ted.Limit.Core;

namespace Ted.Limit.Ext.Map.Standard
{
    class NewFileCmd : BaseCommand
    {
        public NewFileCmd()
        {
            m_category = StandardCommon.c_CATEGORY;
            m_key = typeof(NewFileCmd).FullName;
            m_largeImgFile = "new.gif";
            m_name = "New";
            m_smallImgFile = "new.gif";
            m_tooltip = "Create a new File ...";
        }

        public override void OnClick()
        {
            IMapControlDefault mapCtrl = m_app.MapControl;
            DialogResult saveDr = MessageBox.Show("是否保存文件?", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (saveDr != DialogResult.Cancel)
            {
                if (saveDr == DialogResult.Yes)
                {
                    IActive iAct = StandardCommon.GetCommand(ItemType.Save) as IActive;
                    if (iAct != null)
                    {
                        iAct.OnClick();
                    }
                }
                IMapDocument mapDoc = null;
                try
                {
                    mapDoc = new MapDocumentClass();
                    mapDoc.New("untitled.mxd");
                    mapCtrl.Map = mapDoc.get_Map(0);
                }
                finally
                {
                    if (mapDoc != null)
                    {
                        mapDoc.Close();
                    }
                }
            }
        }
    }

    class OpenFileCmd : BaseCommand
    {
        public OpenFileCmd()
        {
            m_category = StandardCommon.c_CATEGORY;
            m_key = typeof(OpenFileCmd).FullName;
            m_largeImgFile = "open.gif";
            m_name = "Open";
            m_smallImgFile = "open.gif";
            m_tooltip = "Open File ...";
        }

        private ESRI.ArcGIS.SystemUI.ICommand m_openDocCmd;

        public override void OnClick()
        {
            if (m_openDocCmd == null)
            {
                m_openDocCmd = new ControlsOpenDocCommandClass();
                m_openDocCmd.OnCreate(m_app.MapControl);
            }
            m_openDocCmd.OnClick();
        }
    }

    class SaveFileCmd : BaseCommand
    {
        public SaveFileCmd()
        {
            m_category = StandardCommon.c_CATEGORY;
            m_key = typeof(SaveFileCmd).FullName;
            m_largeImgFile = "save.gif";
            m_name = "Save";
            m_smallImgFile = "save.gif";
            m_tooltip = "Save ...";
        }

        public override void OnClick()
        {
            IMapControlDefault mapCtrl = m_app.MapControl;
            if (!string.IsNullOrEmpty(mapCtrl.DocumentFilename))
            {
                try
                {
                    StandardCommon.SaveMxdFile(mapCtrl, mapCtrl.DocumentFilename, false, false);
                }
                catch (Exception exp)
                {
                    m_app.Message(string.Format("保存文件失败:{0}!", exp.Message));
                }
            }
            else
            {
                IActive iAct = StandardCommon.GetCommand(ItemType.SaveAs) as IActive;
                if (iAct != null)
                {
                    iAct.OnClick();
                }
            }
        }
    }

    class SaveFileAsCmd : BaseCommand
    {
        public SaveFileAsCmd() 
        {
            m_category = StandardCommon.c_CATEGORY;
            m_key = typeof(SaveFileAsCmd).FullName;
            m_largeImgFile = "save_as.gif";
            m_name = "SaveAs";
            m_smallImgFile = "save_as.gif";
            m_tooltip = "Save As ...";
        }

        private ESRI.ArcGIS.SystemUI.ICommand m_saveAsCmd;

        public override void OnClick()
        {
            if (m_saveAsCmd == null)
            {
                m_saveAsCmd = new ControlsSaveAsDocCommandClass();
                m_saveAsCmd.OnCreate(m_app.MapControl);
            }
            m_saveAsCmd.OnClick();
        }
    }

    class AddDataCmd : BaseCommand
    {
        public AddDataCmd()
        {
            m_category = StandardCommon.c_CATEGORY;
            m_key = typeof(AddDataCmd).FullName;
            m_largeImgFile = "add_data.gif";
            m_name = "Add";
            m_smallImgFile = "add_data.gif";
            m_tooltip = "Add Data...";
        }

        private ESRI.ArcGIS.SystemUI.ICommand m_addData;

        public override void OnClick()
        {
            if (m_addData == null)
            {
                m_addData = new ControlsAddDataCommandClass();
                m_addData.OnCreate(m_app.MapControl);
            }
            m_addData.OnClick();
        }
    }

    class FileGroup : BaseGroup
    {
        public FileGroup()
        {
            m_key = typeof(FileGroup).FullName;
            m_items = new IItem[]{
                StandardCommon.GetCommand(ItemType.New),
                StandardCommon.GetCommand(ItemType.Open),
                StandardCommon.GetCommand(ItemType.Save),
                StandardCommon.GetCommand(ItemType.SaveAs),
                StandardCommon.GetCommand(ItemType.AddData)
            };
        }
    }
}
