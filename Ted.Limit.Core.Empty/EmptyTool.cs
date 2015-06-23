using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Ted.Limit.Core;
using System.IO;

namespace Ted.Limit.Core.Empty
{
    public class EmptyTool : ITool , IMakable
    {
        public EmptyTool()
        {
            m_category = "Empty";
            m_checked = false;
            m_enabled = true;
            m_key = "Ted.Limit.Core.Empty.EmptyTool";
            m_smallImgFile = "empty.gif";
            m_largeImgFile = "empty.gif";
            m_name = "EmptyTool";
            m_tooltip = "Empty...";
            m_msgDry = new MessageDelivery(EmptyDelivery.Dry);
        }

        private MessageDelivery m_msgDry;

        public MessageDelivery MessagDelivery
        {
            set
            {
                m_msgDry = value;
            }
            get
            {
                return m_msgDry;
            }
        }

        public void DeliverMouseActionMessage(string addition, int button, int shift, int x, int y)
        {
            MessagDelivery(m_name + " On Mouse Action : " + addition + string.Format("@({2},{3}) , with Button[{0}] Shift[{1}]\n", button, shift, x, y));
        }

        public void DeliverKeyActionMessage(string addition, int key, int shift)
        {
            MessagDelivery(m_name + " On Key Action : " + addition + string.Format("with key[{0:N}] shift[{1:N}]\n"));
        }

        #region ITool 成员

        void ITool.OnMouseDown(int button, int shift, int x, int y)
        {
            DeliverMouseActionMessage("Mouse Down", button, shift, x, y);
        }

        void ITool.OnMouseUp(int button, int shift, int x, int y)
        {
            DeliverMouseActionMessage("Mouse Up", button, shift, x, y);
        }

        void ITool.OnMouseMove(int button, int shift, int x, int y)
        {
            DeliverMouseActionMessage("Mouse Move", button, shift, x, y);
        }

        void ITool.OnDblClick()
        {
            MessagDelivery("On Double Click\n");
        }

        void ITool.OnKeyDown(int keycode, int shift)
        {
            DeliverKeyActionMessage("Key Down", keycode, shift);
        }

        void ITool.OnKeyUp(int keycode, int shift)
        {
            DeliverKeyActionMessage("Key Up", keycode, shift);
        }

        #endregion

        #region IItem 成员

        private bool m_enabled;

        private bool m_checked;

        private string m_key;

        private string m_name;

        private string m_category;

        private string m_tooltip;

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
            m_msgDry(m_name + " On Create\n");
        }

        void IActive.OnClick()
        {
            MessagDelivery(m_name + " On Click\n");
        }

        void IActive.Deactive()
        {
            MessagDelivery(m_name + " On Deactive.\n");
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
}
