using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace Ted.Limit.Core.Empty
{
    public class EmptyCommand : ICommand , IMakable
    {
        public EmptyCommand()
        {
            m_category = "Empty";
            m_checked = false;
            m_enabled = false;
            m_key = "Ted.Limit.Core.Empty.EmptyCommand";
            m_name = "EmptyCmd";
            m_tooltip = "Empty...";
            m_MsgDry = new MessageDelivery(EmptyDelivery.Dry);
        }

        private MessageDelivery m_MsgDry;

        private MessageDelivery MsgDelivery
        {
            get
            {
                return m_MsgDry;
            }
            set
            {
                m_MsgDry = value;
            }
        }

        private IDockableWindow m_dckWindow = new EmptyDockableWindow();

        #region IItem 成员

        private string m_category;

        private string m_key;

        private string m_name;

        private string m_tooltip;

        private bool m_checked;

        private bool m_enabled;

        private string m_smallImgFile;

        private string m_largeImgFile;

        private Image m_smallImg;

        private Image m_largeImg;

        System.Drawing.Bitmap IItem.SmallImage
        {
            get
            {
                if (m_smallImg == null)
                {
                    string rerxPath = m_app.ResourcePath(m_key);
                    rerxPath += "\\" + m_smallImgFile;
                    if (File.Exists(rerxPath))
                    {
                        m_smallImg = Image.FromFile(rerxPath);
                    }
                    else
                    {
                        m_smallImg = new Bitmap(15, 15);
                    }
                }
                return new Bitmap(m_smallImg);
            }
        }

        System.Drawing.Bitmap IItem.LargeImage
        {
            get 
            {
                if (m_largeImg == null)
                {
                    string rerxPath = m_app.ResourcePath(m_key);
                    rerxPath += "\\" + m_largeImgFile;
                    if (File.Exists(rerxPath))
                    {
                        m_largeImg = Image.FromFile(rerxPath);
                    }
                    else
                    {
                        m_largeImg = new Bitmap(30, 30);
                    }
                }
                return new Bitmap(m_largeImg);
            }
        }

        string IItem.Category
        {
            get
            { 
                return m_category; 
            }
        }

        string IItem.Key
        {
            get
            { 
                return m_key;
            }
        }

        string IItem.Name
        {
            get
            {
                return m_name;
            }
        }

        bool IItem.Checked
        {
            get
            {
                return m_checked;
            }
            set
            {
                m_checked = value;
            }
        }

        bool IItem.Enabled
        {
            get
            {
                return m_enabled;
            }
            set
            {
                m_enabled = value;
            }
        }

        string IItem.Tooltip
        {
            get
            {
                return m_tooltip;
            }
        }

        #endregion

        #region IActive 成员

        private IApplication m_app;

        void IActive.OnCreate(object hook)
        {
            m_app = hook as IApplication;
            if (m_app == null)
            {
                m_enabled = false;
            }
            else
            {
                m_enabled = true;
            }
            m_MsgDry(m_name + " On Create\n");
        }

        void IActive.OnClick()
        {
            m_MsgDry(m_name + " On Click\n");
            m_app.AddDockableWindow(m_dckWindow);
        }

        void IActive.Deactive()
        {
            m_MsgDry(m_name + " On Deactive\n");
        }

        #endregion

        #region IMakable 成员

        private IMake m_make;

        IMake IMakable.Make
        {
            get { return m_make; }
        }

        public IMake Make
        {
            set
            {
                m_make = value;
            }
        }

        #endregion

        #region IItem 成员

        event PorpertyChanged IItem.OnPorpertyChanged
        {
            add {  }
            remove {  }
        }

        #endregion
    }

    class EmptyDockableWindow : IDockableWindow
    {

        #region IDockableWindow 成员

        string IDockableWindow.Name
        {
            get { return "Empty Window"; }
        }

        string IDockableWindow.Key
        {
            get { return typeof(EmptyDockableWindow).FullName; }
        }

        private System.Windows.Forms.Control m_content = new DckWindowContent();

        System.Windows.Forms.Control IDockableWindow.Content
        {
            get 
            {
                return m_content;
            }
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

        public IMake Make
        {
            get 
            {
                return EmptyMake.GetInstance();
            }
        }

        #endregion
    }
}
