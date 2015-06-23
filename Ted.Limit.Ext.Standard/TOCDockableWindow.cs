using System;
using System.Collections.Generic;
using System.Text;
using Ted.Limit.Core;

namespace Ted.Limit.Ext.Map.Standard
{
    class TOCDockableWindow : IDockableWindow
    {
        public TOCDockableWindow()
        {
            m_name = "Table Of Content";
            m_key = typeof(TOCDockableWindow).FullName;
        }

        #region IDockableWindow 成员

        private string m_name;

        private string m_key;

        private System.Windows.Forms.Control m_content;

        string IDockableWindow.Name
        {
            get { return m_name; }
        }

        string IDockableWindow.Key
        {
            get { return m_key; }
        }

        System.Windows.Forms.Control IDockableWindow.Content
        {
            get
            {
                if (m_content == null)
                {
                    m_content = new TOCContent(m_app.TOCBuddy);
                }
                return m_content;
            }
        }

        #endregion

        #region IActive 成员

        private IApplication m_app;

        void IActive.OnCreate(object hook)
        {
            m_app = hook as IApplication;
        }

        void IActive.OnClick()
        {

        }

        void IActive.Deactive()
        {

        }

        #endregion

        #region IMakable 成员

        IMake IMakable.Make
        {
            get { return StandardMake.GetInstance(); }
        }

        #endregion
    }
}
