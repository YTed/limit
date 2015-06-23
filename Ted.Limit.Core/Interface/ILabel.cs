using System;
using System.Collections.Generic;
using System.Text;

namespace Ted.Limit.Core
{
    public interface ILabel : IItem , IActive
    {
        string Text { get;}
    }
}
