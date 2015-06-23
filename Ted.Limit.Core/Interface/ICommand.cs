using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Ted.Limit.Core
{
    //对应 ButtonTool
    /// <summary>
    /// A command act directly when the command button is clicked.
    /// </summary>
    public interface ICommand : IItem, IActive
    {

    }
}
