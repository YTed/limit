using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.Ext.Map.Search
{
    class AttributeViewDockableWindow : Ted.Limit.Core.IDockableWindow
    {
        private static AttributeViewDockableWindow s_instance;

        public AttributeViewDockableWindow()
        {
            s_instance = this;
        }

        public static Ted.Limit.Core.IDockableWindow GetInstance()
        {
            if (s_instance == null)
            {
                s_instance = new AttributeViewDockableWindow();
            }
            return s_instance;
        }

        #region IDockableWindow 成员

        string Ted.Limit.Core.IDockableWindow.Name
        {
            get { return "查看属性"; }
        }

        string Ted.Limit.Core.IDockableWindow.Key
        {
            get { return typeof(AttributeViewDockableWindow).FullName; }
        }

        private AttributeContent m_content;

        System.Windows.Forms.Control Ted.Limit.Core.IDockableWindow.Content
        {
            get
            {
                if (m_content == null)
                {
                    m_content = new AttributeContent();
                }
                return m_content;
            }
        }

        #endregion

        #region IActive 成员

        void Ted.Limit.Core.IActive.OnCreate(object hook)
        {

        }

        void Ted.Limit.Core.IActive.OnClick()
        {

        }

        void Ted.Limit.Core.IActive.Deactive()
        {

        }

        #endregion

        #region IMakable 成员

        Ted.Limit.Core.IMake Ted.Limit.Core.IMakable.Make
        {
            get { return SearchMake.GetInstance(); }
        }

        #endregion
    }
}
