using System;
using System.Collections.Generic;
using System.Text;

using Ted.Limit.ExtLoader.Test;

namespace Ted.Limit.Common.Test
{
    class LogManagerTest : ITestRun
    {

        private void Log()
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "\\log.cfg";
            LogManager LogMgr = LogManager.CreateInstance();
            LogMgr.Start(path);
            LogMgr.Log("Test...just a test", LogLevel.Debug, "Test");
            LogMgr.Log(new ArgumentNullException(), LogLevel.Debug, "AnOtherTest");
        }

        #region ITestRun 成员

        void ITestRun.Run()
        {
            Log();
        }

        #endregion
    }
}
