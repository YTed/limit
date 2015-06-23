using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Ted.Limit.Core
{
    //对应 StateButtonTool
    /// <summary>
    /// A tool that can reply to user's action on the form.
    /// </summary>
    public interface ITool : IItem, IActive
    {

        /// <summary>
        /// This method is called when a mouse button is pressed down, when this tool is active.  
        /// </summary>
        /// <param name="button"></param>
        /// <param name="shift"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void OnMouseDown(int button, int shift, int x, int y);

        /// <summary>
        /// This method is called when a mouse button is released, when this tool is active.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="shift"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void OnMouseUp(int button, int shift, int x, int y);

        /// <summary>
        /// This method is called when the mouse is moved while a mouse button is pressed down, when this tool is active.  
        /// </summary>
        /// <param name="button"></param>
        /// <param name="shift"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void OnMouseMove(int button, int shift, int x, int y);

        /// <summary>
        /// This method is called when the mouse is double-clicked when the tool is active. 
        /// </summary>
        void OnDblClick();

        /// <summary>
        /// This method is called when a key is pressed down, when this tool is active. 
        /// </summary>
        /// <param name="keycode"></param>
        /// <param name="shift"></param>
        void OnKeyDown(int keycode, int shift);

        /// <summary>
        /// This method is called when a key is release, when this tool is active. 
        /// </summary>
        /// <param name="keycode"></param>
        /// <param name="shift"></param>
        void OnKeyUp(int keycode, int shift);
    }
}
