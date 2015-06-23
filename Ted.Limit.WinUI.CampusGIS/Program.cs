using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ted.Limit.Common;

namespace Ted.Limit.WinUI.CampusGIS
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                FormExtension.StartLogging();
                FormExtension.RegistUIWorkers();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new CampusGIS());
            }
            //至此被捕获的异常都是不可恢复的,系统无条件退出.
            catch (Exception exp)
            {
                LogManager.CreateInstance().Log(exp, LogLevel.Fatal, Global.LOG_PROGRAME);
                Application.Exit();
            }
        }
    }
}