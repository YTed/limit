using System;
using Ted.Limit.Common;

namespace Ted.Limit.Core.Empty
{
    public delegate void MessageDelivery(string message);

    class EmptyDelivery
    {
        internal static void Dry(string msg)
        {
            LogManager.CreateInstance().Log(msg, LogLevel.Debug, Global.LOG_EXT);
        }
    }
}