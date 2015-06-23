using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.ExtLoader
{
    /// <summary>
    /// 扩展模块的定义接口
    /// </summary>
    interface IExt
    {
        /// <summary>
        /// 工具制造器
        /// </summary>
        Ted.Limit.Core.IMake Make { get;}

        /// <summary>
        /// 扩展模块在系统中的标识
        /// </summary>
        string Key { get;}

        /// <summary>
        /// 获得组成扩展模块的Parts
        /// </summary>
        IExtPart[] Parts { get;}
    }

    /// <summary>
    /// 扩展模块的组成部分
    /// </summary>
    interface IExtPart
    {
        /// <summary>
        /// 组件的类型
        /// </summary>
        ExtPartType PartType { get;}

        /// <summary>
        /// 组件中指定索引的值
        /// </summary>
        /// <param name="idx">索引</param>
        /// <returns>值</returns>
        string GetValue(int idx);

        /// <summary>
        /// 组件中指定索引的键
        /// </summary>
        /// <param name="idx">索引</param>
        /// <returns>键</returns>
        string GetKey(int idx);

        /// <summary>
        /// 组件中值的个数
        /// </summary>
        int ValueCount { get;}
    }

    /// <summary>
    /// .ted格式的各个部分
    /// </summary>
    enum ExtPartType
    {
        /// <summary>
        /// 占位符
        /// </summary>
        None,
        /// <summary>
        /// 版本.
        /// 示例:
        /// [version]
        /// 1
        /// </summary>
        Version,
        /// <summary>
        /// 程序集名称
        /// [assembly]
        /// Ted.Is.Handsome
        /// </summary>
        Assembly,
        /// <summary>
        /// 程序集相对路径
        /// [path]
        /// ..\way\to\fetch\Ted's\heart
        /// </summary>
        Path,
        /// <summary>
        /// 工具制造器的类名
        /// [make]
        /// Ted.Loves.Su
        /// </summary>
        Make,
        /// <summary>
        /// 扩展模块的关键字,用于在UI界面中呈现给用户
        /// [key]
        /// KeyOfTedIsHandsome
        /// </summary>
        Key,
        /// <summary>
        /// 资源文件夹路径
        /// </summary>
        Resources,
        /// <summary>
        /// 扩展模块的依赖文件
        /// </summary>
        Dependency,
        /// <summary>
        /// 程序生成,工具条键和类名
        /// [toolbar]
        /// BTW = By.The.Way
        /// WTF = What.The.Fuck
        /// </summary>
        Toolbar,
        /// <summary>
        /// 程序生成,工具键和类名
        /// [tool]
        /// Ted = The.Most.Handsome.Boy
        /// </summary>
        Tool
    }
}
