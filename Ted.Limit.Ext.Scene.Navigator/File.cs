using System;
using System.Collections.Generic;
using System.Text;
using Ted.Limit.Core;

namespace Ted.Limit.Ext.Scene.File
{
    public class NewFileCommand : BaseCommand3D
    {
        public NewFileCommand()
        {
            m_category = "Scene File";
            m_key = typeof(NewFileCommand).FullName;
            m_name = "Scene New File";
            m_tooltip = "New File";
        }

        public override void OnClick()
        {
            base.OnClick();
            
        }
    }
}
