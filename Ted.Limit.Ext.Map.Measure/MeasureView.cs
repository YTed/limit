using System;
using System.Collections.Generic;
using System.Text;

using Ted.Limit.Core;

namespace Ted.Limit.Ext.Map.Measure
{
    class MeasureView : IDockableWindow
    {
        private static MeasureView s_instance;

        public MeasureView()
        {
            m_name = "Measure";
            m_key = typeof(MeasureView).FullName;
            m_content = new MeasureContent();
            s_instance = this;
        }

        public static MeasureView GetInstance()
        {
            if (s_instance == null)
            {
                s_instance = new MeasureView();
            }
            return s_instance;
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
            get { return m_content; }
        }

        #endregion

        #region IActive 成员

        void IActive.OnCreate(object hook)
        {

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
            get { return null; }
        }

        #endregion
    }
}
