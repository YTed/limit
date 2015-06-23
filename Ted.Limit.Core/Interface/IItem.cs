using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Ted.Limit.Core
{
    /// <summary>
    /// The UI item that display in the user's form.
    /// </summary>
    public interface IItem
    {
        event PorpertyChanged OnPorpertyChanged;

        /// <summary>
        /// Gets the bitmap that is used as the icon on this command, 
        /// shown when the command is placed on a toolbar in small Icon mode.
        /// </summary>
        Bitmap SmallImage { get;}

        /// <summary>
        /// Gets the bitmap that is used as the icon on this command, 
        /// shown when the command is placed on a toolbar in big Icon mode.
        /// </summary>
        Bitmap LargeImage { get;}

        /// <summary>
        /// The name of the category with which the command is associated, 
        /// used by the Customize dialog box.
        /// </summary>
        string Category { get;}

        /// <summary>
        /// 组件在系统中的唯一标识
        /// </summary>
        string Key { get;}

        /// <summary>
        /// 用于显示的名称
        /// </summary>
        string Name { get;}

        /// <summary>
        /// Indicates whether or not this command is checked.
        /// </summary>
        bool Checked { get;set;}

        /// <summary>
        /// Indicates whether or not this command is enabled.
        /// </summary>
        bool Enabled { get;set;}

        /// <summary>
        /// tool tip of the item
        /// </summary>
        string Tooltip { get;}


    }

    /// <summary>
    /// The activable object , i.e. which can be 
    /// click , activate or deactivate.
    /// </summary>
    public interface IActive
    {
        /// <summary>
        /// Occurs when this command is created.
        /// </summary>
        /// <param name="hook"></param>
        void OnCreate(object hook);

        /// <summary>
        /// Occurs when this command is clicked.
        /// </summary>
        void OnClick();

        /// <summary>
        /// Occurs when this command is deactive
        /// </summary>
        void Deactive();
    }

    /// <summary>
    /// Group of items.
    /// </summary>
    public interface IGroup
    {
        /// <summary>
        /// specify this in the appliction
        /// </summary>
        string Key { get;}

        /// <summary>
        /// get item enumerator
        /// </summary>
        IItem[] Items { get;}
    }
}
