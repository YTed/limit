using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ted.Limit.ExtLoader
{
    /// <summary>
    /// 找不到指定扩展模块时抛出此异常
    /// </summary>
    public class ExtNotFoundException : ExtLoadingException
    {
        /// <summary>
        /// 初始化ExtLoadingException类新的实例
        /// </summary>
        public ExtNotFoundException()
            : base()
        {

        }

        /// <summary>
        /// 用给定的描述初始化ExtLoadingException类新的实例
        /// </summary>
        /// <param name="msg">描述错误的信息</param>
        public ExtNotFoundException(string msg)
            : base(msg)
        {

        }

        /// <summary>
        /// 以指定的
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="cause"></param>
        public ExtNotFoundException(string msg, Exception cause)
            : base(msg, cause)
        {

        }

        /// <summary>
        /// 用序列化数据初始化ExtLoadingException类新的实例
        /// </summary>
        /// <param name="info">它存有引发有关异常的序列化数据</param>
        /// <param name="context">它存有有关异常的上下文信息</param>
        public ExtNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }

    /// <summary>
    /// 指定的.ted文件格式错误时抛出此异常
    /// </summary>
    public class ExtDefFormatException : ExtLoadingException
    {
        /// <summary>
        /// 初始化ExtLoadingException类新的实例
        /// </summary>
        public ExtDefFormatException()
            : base()
        {

        }

        /// <summary>
        /// 用给定的描述初始化ExtLoadingException类新的实例
        /// </summary>
        /// <param name="msg">描述错误的信息</param>
        public ExtDefFormatException(string msg)
            : base(msg)
        {

        }

        /// <summary>
        /// 用给定的描述和内含异常初始化ExtLoadingException类新的实例
        /// </summary>
        /// <param name="msg">描述错误的信息</param>
        /// <param name="cause">导致当前异常的异常</param>
        public ExtDefFormatException(string msg, Exception cause)
            : base(msg, cause)
        {

        }

        /// <summary>
        /// 用序列化数据初始化ExtLoadingException类新的实例
        /// </summary>
        /// <param name="info">它存有引发有关异常的序列化数据</param>
        /// <param name="context">它存有有关异常的上下文信息</param>
        public ExtDefFormatException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }

    /// <summary>
    /// 加载扩展模块发生不可恢复的错误时抛出此异常。
    /// </summary>
    public class ExtError : ExtLoadingException
    {
        /// <summary>
        /// 初始化ExtLoadingException类新的实例
        /// </summary>
        public ExtError()
            : base()
        {

        }

        /// <summary>
        /// 用给定的描述初始化ExtLoadingException类新的实例
        /// </summary>
        /// <param name="msg">描述错误的信息</param>
        public ExtError(string msg)
            : base(msg)
        {

        }

        /// <summary>
        /// 用给定的描述和内含异常初始化ExtLoadingException类新的实例
        /// </summary>
        /// <param name="msg">描述错误的信息</param>
        /// <param name="cause">导致当前异常的异常</param>
        public ExtError(string msg, Exception cause)
            : base(msg, cause)
        {

        }

        /// <summary>
        /// 用序列化数据初始化ExtLoadingException类新的实例
        /// </summary>
        /// <param name="info">它存有引发有关异常的序列化数据</param>
        /// <param name="context">它存有有关异常的上下文信息</param>
        public ExtError(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }

    /// <summary>
    /// 系统已加载具有指定的键的模块,或者工具池中已存在具有相同的键的工具时抛出此异常
    /// </summary>
    public class ExtExistException : ExtLoadingException
    {
        /// <summary>
        /// 初始化ExtLoadingException类新的实例
        /// </summary>
        public ExtExistException()
            : base()
        {

        }

        /// <summary>
        /// 用给定的描述初始化ExtLoadingException类新的实例
        /// </summary>
        /// <param name="msg">描述错误的信息</param>
        public ExtExistException(string msg)
            : base(msg)
        {

        }

        /// <summary>
        /// 用给定的描述和内含异常初始化ExtLoadingException类新的实例
        /// </summary>
        /// <param name="msg">描述错误的信息</param>
        /// <param name="cause">导致当前异常的异常</param>
        public ExtExistException(string msg, Exception cause)
            : base(msg, cause)
        {

        }

        /// <summary>
        /// 用序列化数据初始化ExtLoadingException类新的实例
        /// </summary>
        /// <param name="info">它存有引发有关异常的序列化数据</param>
        /// <param name="context">它存有有关异常的上下文信息</param>
        public ExtExistException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }

    /// <summary>
    /// 模块加载引起的异常
    /// </summary>
    public class ExtLoadingException : Exception
    {
        /// <summary>
        /// 初始化ExtLoadingException类新的实例
        /// </summary>
        public ExtLoadingException()
            : base()
        {

        }

        /// <summary>
        /// 用给定的描述初始化ExtLoadingException类新的实例
        /// </summary>
        /// <param name="msg">描述错误的信息</param>
        public ExtLoadingException(string msg)
            : base(msg)
        {

        }

        /// <summary>
        /// 用给定的描述和内含异常初始化ExtLoadingException类新的实例
        /// </summary>
        /// <param name="msg">描述错误的信息</param>
        /// <param name="cause">导致当前异常的异常</param>
        public ExtLoadingException(string msg, Exception cause)
            : base(msg, cause)
        {

        }

        /// <summary>
        /// 用序列化数据初始化ExtLoadingException类新的实例
        /// </summary>
        /// <param name="info">它存有引发有关异常的序列化数据</param>
        /// <param name="context">它存有有关异常的上下文信息</param>
        public ExtLoadingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }
    }
}
