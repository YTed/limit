using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Ted.Limit.Core
{
    public class BaseLabel : ILabel
    {
        #region ILabel 成员

        protected string m_text;

        public virtual string Text
        {
            get { return m_text; }
        }

        #endregion

        #region IItem 成员

        protected bool m_enabled;

        protected bool m_checked;

        protected string m_key;

        protected string m_name;

        protected string m_category;

        protected string m_tooltip;

        public virtual System.Drawing.Bitmap SmallImage
        {
            get 
            {
                return null;
            }
        }

        public virtual System.Drawing.Bitmap LargeImage
        {
            get 
            {
                return null;
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
            get { return m_key; }
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
                m_tooltip = value;
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
