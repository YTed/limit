using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Ted.Limit.Core;
using System.IO;
using Ted.Limit.Common;

namespace Ted.Limit.Ext.Map.Measure
{
    class MeasureLength : BaseTool
    {
        private string m_cursorFile;
        private Image m_cursorImg;

        public MeasureLength()
        {
            m_category = MeasureCommon.c_CATEGORY;
            m_key = typeof(MeasureLength).FullName;
            m_largeImgFile = "measure-line.gif";
            m_name = "Measure Length";
            m_smallImgFile = m_largeImgFile;
            m_tooltip = "measure length..";
            m_cursorFile = "measureCursor.gif";
        }

        private Image Cursor
        {
            get
            {
                if (m_cursorImg == null)
                {
                    m_cursorFile = m_app.ResourcePath(m_key) + "\\" + m_cursorFile;
                    if (!string.IsNullOrEmpty(m_cursorFile) && File.Exists(m_cursorFile))
                    {
                        m_cursorImg = Image.FromFile(m_cursorFile);
                    }
                }
                return m_cursorImg;
            }
        }

        public override void OnClick()
        {
            m_app.MapControl.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerParentWindow;
            m_app.MapControl.MouseIcon = Global.ConvertImage(Cursor);
            m_app.ShowDockableWindow(MeasureView.GetInstance());
        }

        public override void Deactive()
        {
            m_app.MapControl.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerDefault;
            m_app.HideDockableWindow(MeasureView.GetInstance());
        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            

        }
    }

    class MeasureArea : BaseTool
    {

    }
}
