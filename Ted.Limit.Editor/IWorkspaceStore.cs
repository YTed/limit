using System;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Ted.Limit.Editor
{
    /// <summary>
    /// 定义一组获取工作空间的方法
    /// </summary>
    public interface IWorkspaceStore
    {
        /// <summary>
        /// 获得指定要素图层的编辑工作空间
        /// </summary>
        /// <param name="FeatLyr"></param>
        /// <returns></returns>
        /// 不允许返回空值
        IWorkspaceEdit GetWorkspaceEdit(IFeatureLayer FeatLyr);
    }
}
