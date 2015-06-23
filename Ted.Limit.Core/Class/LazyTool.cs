using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.Core
{
    public class LazyTool : BaseTool
    {
        protected ESRI.ArcGIS.SystemUI.ICommand m_realCmd;
        protected ESRI.ArcGIS.SystemUI.ITool m_realTool;

        public LazyTool(string key, string name,
            ESRI.ArcGIS.SystemUI.ICommand realCmd,
            string icon, string tooltip, string category)
            : base()
        {
            m_key = key;
            m_name = name;
            m_smallImgFile = icon;
            m_largeImgFile = icon;
            m_realCmd = realCmd;
            m_realTool = realCmd as ESRI.ArcGIS.SystemUI.ITool;
            m_tooltip = tooltip;
            m_category = category;
        }

        public override void OnClick()
        {
            base.OnClick();
            m_realCmd.OnClick();
        }

        public override void OnMouseDown(int button, int shift, int x, int y)
        {
            base.OnMouseDown(button, shift, x, y);
            m_realTool.OnMouseDown(button, shift, x, y);
        }

        public override void OnMouseMove(int button, int shift, int x, int y)
        {
            base.OnMouseMove(button, shift, x, y);
            m_realTool.OnMouseDown(button, shift, x, y);
        }

        public override void OnMouseUp(int button, int shift, int x, int y)
        {
            base.OnMouseUp(button, shift, x, y);
            m_realTool.OnMouseUp(button, shift, x, y);
        }

        public override void OnCreate(object hook)
        {
            base.OnCreate(hook);
            m_realCmd.OnCreate(hook);
        }

        public override void Deactive()
        {
            base.Deactive();
            m_realTool.Deactivate();
        }

        public override void OnDblClick()
        {
            base.OnDblClick();
            m_realTool.OnDblClick();
        }

        public override void OnKeyDown(int keycode, int shift)
        {
            base.OnKeyDown(keycode, shift);
            m_realTool.OnKeyDown(keycode, shift);
        }

        public override void OnKeyUp(int keycode, int shift)
        {
            base.OnKeyUp(keycode, shift);
            m_realTool.OnKeyUp(keycode, shift);
        }
    }
}
