using System;
using System.Collections.Generic;
using System.Text;

using Ted.Limit.Core;

namespace Ted.Limit.ExtLoader
{
    /// <summary>
    /// 负责扩展模块初始化和扩展模块的动态加载、保存、错误控制。
    /// </summary>
    public interface IExtManager
    {
        /// <summary>
        /// 应用程序钩子.
        /// </summary>
        object Application { get;set;}

        /// <summary>
        /// 加载共享单元格式文件
        /// </summary>
        /// <param name="suPath">.su文件路径</param>
        void LoadShareUnit(string suPath);

        /// <summary>
        /// 保存共享单元格式文件
        /// </summary>
        /// <param name="suPath">.su文件路径</param>
        void SaveShareUnit(string suPath);

        /// <summary>
        /// 运行时加载.ted文件
        /// </summary>
        /// <param name="path">.ted文件路径</param>
        /// <param name="key">模块在系统中的标识</param>
        /// <returns>扩展模块包含的工具制造器</returns>
        IMake LoadExtDef(string path, ref string key);

        /// <summary>
        /// 加载具有指定键的扩展模块的工具.
        /// </summary>
        /// <param name="key"></param>
        void LoadModule(string key);

        /// <summary>
        /// 移除具有指定键的扩展模块.
        /// </summary>
        /// <param name="key"></param>
        void RemoveModule(string key);

        /// <summary>
        /// 根据键值获取工具
        /// </summary>
        /// <param name="key">指定的键</param>
        /// <returns>key所对应的工具.</returns>
        object GetTool(string key);

        /// <summary>
        /// 返回指定工具的资源文件夹路径
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetResource(string key);

        /// <summary>
        /// 获取指定工具所属的模块的键
        /// </summary>
        /// <param name="key">工具键</param>
        /// <returns></returns>
        string GetModuleKey(string key);

        /// <summary>
        /// 获取所有扩展模块制造器.
        /// </summary>
        /// <returns></returns>
        IMake[] AllModules();
    }
}
