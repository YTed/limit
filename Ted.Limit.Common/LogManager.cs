using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Ted.Limit.Common
{
    public class LogManager
    {
        private Dictionary<string, Logger> m_loggerPool;

        private Logger m_logMe;

        private LogManager()
        {
            m_loggerPool = new Dictionary<string, Logger>();

            // logger for logger
            string logAsmPath = typeof(LogManager).Assembly.Location;
            logAsmPath = logAsmPath + "\\log.log";
            m_logMe = new Logger();
            m_logMe.Level = LogLevel.Fatal;
            m_logMe.LogFile = logAsmPath;
        }

        private static LogManager s_instance;

        public static LogManager CreateInstance()
        {
            if (s_instance == null)
            {
                s_instance = new LogManager();
            }
            return s_instance;
        }

        private bool GetLogger(string type, out Logger log)
        {
            log = null;
            return (!string.IsNullOrEmpty(type)) && (m_loggerPool.TryGetValue(type, out log));
        }

        /// <summary>
        /// 记录日志.
        /// </summary>
        /// <param name="msg">要记录的信息</param>
        /// <param name="level">日志级别</param>
        /// <param name="type">日志类型</param>
        public void Log(string msg, LogLevel level, string type)
        {
            if (m_init)
            {
                Logger log = null;
                if (GetLogger(type, out log))
                {
                    try
                    {
                        log.Log(msg, level);
                    }
                    catch (Exception exp)
                    {
                        m_logMe.Log(exp, m_logMe.Level);
                    }
                }
            }
        }

        /// <summary>
        /// 记录异常
        /// </summary>
        /// <param name="exp">要记录的异常</param>
        /// <param name="level">日志级别</param>
        /// <param name="type">日志类型</param>
        public void Log(Exception exp2log, LogLevel level, string type)
        {
            if (m_init)
            {
                Logger log = null;
                if (GetLogger(type, out log))
                {
                    try
                    {
                        log.Log(exp2log, level);
                    }
                    catch (Exception exp)
                    {
                        m_logMe.Log(exp, m_logMe.Level);
                    }
                }
            }
        }

        private bool m_init = false;

        /**
         * log.cfg文件格式:
         * LogType = LogDirectory @ LogLevel
         */
        /// <summary>
        /// 启动日志记录.
        /// </summary>
        /// <param name="logCfg">日志记录配置文件</param>
        public void Start(string logCfg)
        {
            string sep = "=@";
            if (!File.Exists(logCfg))
            {
                throw new LogException("无法找到指定的配置文件");
            }
            try
            {
                string cfgPath = logCfg.Substring(0, logCfg.LastIndexOf("\\"));
                string[] cfgTxt = File.ReadAllLines(logCfg);
                foreach (string logCfgLine in cfgTxt)
                {
                    string[] args = logCfgLine.Split(sep.ToCharArray());
                    args[0] = args[0].Trim();
                    args[1] = args[1].Trim();
                    args[2] = args[2].Trim();

                    if (DirectoryHelper.IsRelativePath(args[1]))
                    {
                        args[1] = DirectoryHelper.GetRelativePath(cfgPath, args[1]);
                    }
                    string parentPath = DirectoryHelper.GetParentPath(args[1]);
                    if (!Directory.Exists(parentPath))
                    {
                        Directory.CreateDirectory(parentPath);
                    }
                    LogLevel lvl = TransLevel(args[2]);
                    Logger log = new Logger();
                    log.LogFile = args[1];
                    log.Level = lvl;

                    m_loggerPool.Add(args[0], log);
                }

                m_init = true;
            }
            catch (IOException ioExp)
            {
                throw new LogException("解析配置文件发生IO异常", ioExp);
            }
            catch (Exception exp)
            {
                throw new LogException("解析配置文件发生异常", exp);
            }
        }

        private static string[] s_strLvl = { 
            "ALL",
            "DEBUG",
            "INFO",
            "WARN",
            "ERROR",
            "FATAL",
            "NONE"
        };

        private static LogLevel[] s_lvl ={
            LogLevel.All,
            LogLevel.Debug,
            LogLevel.Info,
            LogLevel.Warn,
            LogLevel.Error,
            LogLevel.Fatal,
            LogLevel.None
        };

        internal static LogLevel TransLevel(string strLvl)
        {
            for(int i = 0; i<s_strLvl.Length ; i++)
            {
                if (strLvl.Equals(s_strLvl[i], StringComparison.CurrentCultureIgnoreCase))
                {
                    return s_lvl[i];
                }
            }
            return LogLevel.None;
        }

        internal static string TransLevel(LogLevel level)
        {
            for (int i = 0; i < s_lvl.Length; i++)
            {
                if (s_lvl[i] == level)
                {
                    return s_strLvl[i];
                }
            }
            return string.Empty;
        }
    }

    public enum LogLevel
    {
        All = 63,
        Debug = 31,
        Info = 15,
        Warn = 7,
        Error = 3,
        Fatal = 1,
        None = 0
    }

    public class LogException : Exception
    {
        public LogException()
            : base()
        {

        }

        public LogException(string msg)
            : base(msg)
        {

        }

        public LogException(string msg, Exception cause)
            : base(msg, cause)
        {

        }

        public LogException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
    }
}
