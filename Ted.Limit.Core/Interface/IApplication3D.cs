using System;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Analyst3D;
using ESRI.ArcGIS.Controls;

namespace Ted.Limit.Core
{
    public interface IApplication3D
    {
        IScene Scene { get;}

        ISceneControlDefault SceneControl { get;}
    }
}
