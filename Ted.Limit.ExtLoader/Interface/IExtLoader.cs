using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.ExtLoader
{
    /// <summary>
    /// IExtLoader是系统运行时动态添加扩展模块用到的接口
    /// </summary>
    interface IExtLoader
    {
        /// <summary>
        /// 从指定的.ted格式文件加载模块
        /// </summary>
        /// <param name="ExtDefPath">.ted文件的路径</param>
        /// <returns>扩展模块</returns>
        IExt LoadFromFile(string ExtDefPath);

        /// <summary>
        /// 把指定的模块输出到文件夹
        /// </summary>
        /// <param name="OutPath">指定的文件夹</param>
        /// <param name="ext">要保存的扩展模块</param>
        void SaveToFile(string OutPath, IExt ext);
    }
}
