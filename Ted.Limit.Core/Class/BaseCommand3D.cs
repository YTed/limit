using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.Core
{
    public class BaseCommand3D : BaseCommand
    {
        protected IApplication3D m_app3d;

        public override void OnCreate(object hook)
        {
            base.OnCreate(hook);
            m_app3d = hook as IApplication3D;
            m_enabled = hook != null;
        }
    }
}
