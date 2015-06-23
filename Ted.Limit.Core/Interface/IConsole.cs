using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ted.Limit.Core
{
    /// <summary>
    /// 控制台提示,用于后台程序运行时,向控制台输出信息的回调函数.
    /// </summary>
    /// <param name="msg">程序运行信息</param>
    public delegate void ConsoleTips(string msg);

    /// <summary>
    /// 后台程序接口定义.
    /// </summary>
    public interface IConsole
    {
        /// <summary>
        /// 获取调用此后台程序的命令,系统唯一.
        /// </summary>
        string Command { get;}

        /// <summary>
        /// 获取后台程序的帮助
        /// </summary>
        string Help { get;}

        /// <summary>
        /// 按用户输入的参数运行后台程序.
        /// </summary>
        /// <param name="param">用户输入的参数</param>
        /// <param name="tips">控制台输出回调函数</param>
        /// <returns>操作结束时的输出</returns>
        /// <exception cref="ConsoleArgumentException">输入参数不正确</exception>
        string Run(string[] param, ConsoleTips tips);
    }

    /// <summary>
    /// 当调用IConsole的Run方法时由于输入了非法参数而抛出的异常
    /// </summary>
    public class ConsoleArgumentException : Exception
    {
        public ConsoleArgumentException()
            : base()
        {

        }

        public ConsoleArgumentException(string msg)
            : base(msg)
        {

        }

        public ConsoleArgumentException(string msg, Exception cause)
            : base(msg, cause)
        {

        }

        public ConsoleArgumentException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
    }
}
