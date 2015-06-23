using System;
using System.Collections.Generic;
using System.Text;

using Ted.Limit.Core;

namespace Ted.Limit.ExtLoader
{
    /// <summary>
    /// IShareUnit是系统在启动时,读取.su文件并对扩展模块进行预加载
    /// 所使用到的接口
    /// </summary>
    interface IShareUnit
    {
        /// <summary>
        /// 从一段.su格式的字符串中解析ShareUnit结构
        /// </summary>
        /// <param name="suText">.su格式的字符串</param>
        /// <returns>加载信息</returns>
        string Load(string suText);

        /// <summary>
        /// 加载具有指定键的模块
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IMake LoadModule(string key);

        IMake[] AllModules();

        /// <summary>
        /// 获得当前ShareUnit的.su格式字符串
        /// </summary>
        string TextFormat { get;}

        /// <summary>
        /// 从ShareUnit结构中移除具有指定键的扩展模块
        /// </summary>
        /// <param name="key">扩展模块的键</param>
        void Remove(string key);

        /// <summary>
        /// 向ShareUnit结构中添加指定的扩展模块
        /// </summary>
        /// <param name="ExtDefPath">扩展模块的.ted文件路径</param>
        /// <returns>扩展模块的制造器</returns>
        IMake Add(string ExtDefPath, out string key);

        /// <summary>
        /// 判断ShareUnit中是否存在指定键的扩展模块
        /// </summary>
        /// <param name="key">扩展模块的键</param>
        /// <returns>存在则返回true</returns>
        bool Exist(string key);

        /// <summary>
        /// 获取所有扩展模块的键。
        /// </summary>
        string[] All { get;}

        /// <summary>
        /// 返回指定键的资源文件夹路径
        /// </summary>
        /// <param name="moduleKey"></param>
        /// <returns></returns>
        string GetResourcePath(string moduleKey);
    }
}
