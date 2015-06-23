using System;
using System.Collections.Generic;
using System.Text;
using Ted.Limit.Core;

namespace Ted.Limit.Ext.Map.Standard
{
    class TOCViewCommand : BaseCommand
    {
        public TOCViewCommand()
        {
            m_category = StandardCommon.c_CATEGORY;
            m_key = typeof(TOCViewCommand).FullName;
            m_largeImgFile = "TOC.gif";
            m_name = "TOC";
            m_smallImgFile = "TOC.gif";
            m_tooltip = "TOC control toggle";
        }

        private IDockableWindow m_toc;

        public override void OnClick()
        {
            if (m_app != null)
            {
                if (m_toc == null)
                {
                    m_toc = new TOCDockableWindow();
                    m_toc.OnCreate(m_app);
                }
                m_app.AddDockableWindow(m_toc);
            }
        }
    }

    class ViewGroup : BaseGroup
    {
        public ViewGroup()
        {
            m_items = new IItem[]{
                new TOCViewCommand()
            };
            m_key = typeof(ViewGroup).FullName;
        }
    }
}
