using System;
using System.Collections.Generic;
using System.Text;

using Ted.Limit.Core;
using ESRI.ArcGIS.Controls;

namespace Ted.Limit.Ext.Map.Analyst
{
    delegate void Close(IInnerForm inner);

    class BufferCommand : BaseCommand
    {

        public BufferCommand()
        {
            m_category = Common.c_CATEGORY;
            m_key = typeof(BufferCommand).FullName;
            m_largeImgFile = "buffer.gif";
            m_name = "Buffer";
            m_smallImgFile = m_largeImgFile;
            m_tooltip = "buffer ..";
        }

        public override void OnClick()
        {
            BufferInnerForm buffInnerForm = new BufferInnerForm(m_app.MapControl);
            buffInnerForm.SetCloseHandle(new Close(m_app.CloseInnerForm));
            m_app.ShowInnerForm(buffInnerForm);
        }
    }

    class BufferInnerForm : IInnerForm
    {
        public BufferInnerForm(IMapControlDefault mapCtrl)
        {
            BufferWizard wizard = new BufferWizard();
            wizard.SetInnerForm(this);
            wizard.MapControl = mapCtrl;
            this.m_content = wizard;
        }

        private Close m_close;

        public void SetCloseHandle(Close close)
        {
            m_close = close;
        }

        public void Close()
        {
            if (m_close != null)
            {
                m_close(this);
            }
        }

        #region IInnerForm 成员

        public string Title
        {
            get { return "Buffer Wizard"; }
        }

        private System.Windows.Forms.Control m_content;

        public System.Windows.Forms.Control Content
        {
            get { return m_content; }
        }

        public void OnLoad()
        {

        }

        public void OnClosing(ref bool cancel)
        {

        }

        public void OnClosed(System.Windows.Forms.CloseReason reason)
        {

        }

        #endregion
    }
}
