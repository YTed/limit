﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace Ted.Limit.Core
{
    public abstract class BaseCommand : ICommand
    {
        protected BaseCommand()
        {

        }

        #region IItem 成员

        protected bool m_enabled;

        protected bool m_checked;

        protected string m_key;

        protected string m_name;

        protected string m_category;

        protected string m_tooltip;

        protected string m_smallImgFile;

        protected string m_largeImgFile;

        protected Image m_largeImg;

        protected Image m_smallImg;

        public virtual System.Drawing.Bitmap SmallImage
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
            set
            {
                m_smallImg = value;
                if (OnPorpertyChanged != null)
                {
                    OnPorpertyChanged(this);
                }
            }
        }

        public virtual System.Drawing.Bitmap LargeImage
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
            set
            {
                m_largeImg = value;
                if (OnPorpertyChanged != null)
                {
                    OnPorpertyChanged(this);
                }
            }
        }

        public virtual string Category
        {
            get
            {
                return m_category;
            }
            set
            {
                m_category = value;
                if (OnPorpertyChanged != null)
                {
                    OnPorpertyChanged(this);
                }
            }
        }

        public virtual string Key
        {
            get
            {
                return m_key;
            }
        }

        public virtual string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
                if (OnPorpertyChanged != null)
                {
                    OnPorpertyChanged(this);
                }
            }
        }

        public virtual bool Checked
        {
            get
            {
                return m_checked;
            }
            set
            {
                m_checked = value;
                if (OnPorpertyChanged != null)
                {
                    OnPorpertyChanged(this);
                }
            }
        }

        public virtual bool Enabled
        {
            get
            {
                return m_enabled;
            }
            set
            {
                m_enabled = value;
                if (OnPorpertyChanged != null)
                {
                    OnPorpertyChanged(this);
                }
            }
        }

        public virtual string Tooltip
        {
            get
            {
                return m_tooltip;
            }
            set
            {
                if (OnPorpertyChanged != null)
                {
                    OnPorpertyChanged(this);
                }
            }
        }

        public event PorpertyChanged OnPorpertyChanged;

        #endregion

        #region IActive 成员

        protected IApplication m_app;

        public virtual void OnCreate(object hook)
        {
            m_app = hook as IApplication;
            m_enabled = m_app != null;
        }

        public virtual void OnClick()
        {

        }

        public virtual void Deactive()
        {

        }

        #endregion
    }
}
