using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Ted.Limit.Common
{
    /**
     * 其实我只是要一个简单的日志记录,log4net太强大了...
     */
    /// <summary>
    /// 日志记录器.
    /// </summary>
    class Logger
    {
        private const string CLRF = "\r\n";

        private LogLevel m_level;

        public LogLevel Level
        {
            get { return m_level; }
            set { m_level = value; }
        }

        private string m_logFile;

        public string LogFile
        {
            get { return m_logFile; }
            set { m_logFile = value; }
        }

        protected bool ShouldLog(LogLevel level)
        {
            return (m_level != LogLevel.None) && ((m_level & level) == level);
        }

        public void Log(Exception exp, LogLevel level)
        {
            if (ShouldLog(level))
            {
                Log(exp.Message + CLRF + "\t\t" +
                    exp.StackTrace, 
                    level
                );
            }
        }

        public void Log(string msg, LogLevel level)
        {
            if (ShouldLog(level))
            {
                string logMsg = CLRF +
                    DateTime.Now.ToString() + "\t\t-" + 
                    LogManager.TransLevel(level) + CLRF + 
                    msg + CLRF;
                File.AppendAllText(LogFile, logMsg);
            }
        }
    }
}
