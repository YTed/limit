using System;
using System.Collections.Generic;
using System.Text;

using Ted.Limit.Core;

namespace Ted.Limit.ExtLoader
{
    /// <summary>
    /// 命令池
    /// </summary>
    interface ICommandPool
    {
        /// <summary>
        /// 添加扩展逻辑模块
        /// </summary>
        /// <param name="key">模块在系统中的标识</param>
        void AddExtModule(string key);

        /// <summary>
        /// 移除扩展逻辑模块
        /// </summary>
        /// <param name="key">模块在系统中的标识</param>
        void RemoveExtModule(string key);

        /// <summary>
        /// 判断是否已存在指定模块
        /// </summary>
        /// <param name="key">待查询模块的系统标识</param>
        /// <returns>存在则返回true</returns>
        bool ExistModule(string key);

        /// <summary>
        /// 添加扩展工具
        /// </summary>
        /// <param name="key">扩展工具在系统中的标识</param>
        /// <param name="item">扩展工具</param>
        /// <param name="moduleKey">扩展工具逻辑隶属的模块标识</param>
        void AddExtension(string key, object item, string moduleKey);

        /// <summary>
        /// 添加扩展工具
        /// </summary>
        /// <param name="items">扩展工具的键-值对列表</param>
        /// <param name="moduleKey">扩展工具逻辑隶属的模块标识</param>
        void AddExtRange(Dictionary<string, object> items, string moduleKey);

        /// <summary>
        /// 根据标识获得图形扩展工具
        /// </summary>
        /// <param name="key">扩展工具在系统中的标识</param>
        /// <returns>key标识的工具</returns>
        object GetExtension(string key);

        /// <summary>
        /// 移除key标识的扩展工具
        /// </summary>
        /// <param name="key">扩展工具在系统中的标识</param>
        void RemoveExtension(string key);

        /// <summary>
        /// 判断工具池中是否存在指定标识的工具
        /// </summary>
        /// <param name="key">待查询工具的标识</param>
        /// <returns>存在则返回true</returns>
        bool ExistExtension(string key);

        /// <summary>
        /// 用工具的键获得扩展模块的键
        /// </summary>
        /// <param name="toolKey"></param>
        /// <returns></returns>
        string GetModuleKey(string toolKey);

        
    }
}