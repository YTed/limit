using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.Core
{
    public class LazyCommand : BaseCommand
    {
        private ESRI.ArcGIS.SystemUI.ICommand m_realCmd;

        public LazyCommand(string key, string name,
            ESRI.ArcGIS.SystemUI.ICommand realCmd,
            string icon, string tooltip, string category)
        {
            m_key = key;
            m_name = name;
            m_realCmd = realCmd;
            m_smallImgFile = icon;
            m_largeImgFile = icon;
            m_tooltip = tooltip;
            m_category = category;
        }

        public override void OnCreate(object hook)
        {
            base.OnCreate(hook);
            m_realCmd.OnClick();
        }

        public override void OnClick()
        {
            base.OnClick();
            m_realCmd.OnClick();
        }
    }
}
