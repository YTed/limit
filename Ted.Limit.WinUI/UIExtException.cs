using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.WinUI
{
    /// <summary>
    /// UIExtException 是本地UI加载扩展模块时可能抛出的异常,
    /// 在安装扩展模块时,如果此异常被抛出,则异常是UI可控的,
    /// 日志等级为Warn.
    /// </summary>
    public class UIExtException : Exception
    {
        public UIExtException()
            : base()
        {

        }

        public UIExtException(string msg)
            : base(msg)
        {

        }

        public UIExtException(string msg, Exception cause)
            : base(msg, cause)
        {

        }

        public UIExtException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
    }
}
