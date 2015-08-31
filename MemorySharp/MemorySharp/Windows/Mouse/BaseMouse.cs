/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System.Threading;

namespace Binarysharp.MemoryManagement.Windows.Mouse
{
    /// <summary>
    /// Abstract class defining a virtual mouse.
    /// </summary>
    public abstract class BaseMouse
    {
        #region Fields
        /// <summary>
        /// The reference of the <see cref="RemoteWindow"/> object.
        /// </summary>
        protected readonly RemoteWindow Window;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of a child of the <see cref="BaseMouse"/> class.
        /// </summary>
        /// <param name="window">The reference of the <see cref="RemoteWindow"/> object.</param>
        protected BaseMouse(RemoteWindow window)
        {
            // Save the parameter
            Window = window;
        }
        #endregion

        #region Abstract Methods
        /// <summary>
        /// Moves the cursor at the specified coordinate.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        protected abstract void MoveToAbsolute(int x, int y);
        /// <summary>
        /// Presses the left button of the mouse at the current cursor position.
        /// </summary>
        public abstract void PressLeft();
        /// <summary>
        /// Presses the middle button of the mouse at the current cursor position.
        /// </summary>
        public abstract void PressMiddle();
        /// <summary>
        /// Presses the right button of the mouse at the current cursor position.
        /// </summary>
        public abstract void PressRight();
        /// <summary>
        /// Releases the left button of the mouse at the current cursor position.
        /// </summary>
        public abstract void ReleaseLeft();
        /// <summary>
        /// Releases the middle button of the mouse at the current cursor position.
        /// </summary>
        public abstract void ReleaseMiddle();
        /// <summary>
        /// Releases the right button of the mouse at the current cursor position.
        /// </summary>
        public abstract void ReleaseRight();
        /// <summary>
        /// Scrolls horizontally using the wheel of the mouse at the current cursor position.
        /// </summary>
        /// <param name="delta">The amount of wheel movement.</param>
        public abstract void ScrollHorizontally(int delta = 120);
        /// <summary>
        /// Scrolls vertically using the wheel of the mouse at the current cursor position.
        /// </summary>
        /// <param name="delta">The amount of wheel movement.</param>
        public abstract void ScrollVertically(int delta = 120);
        #endregion

        #region Extended Methods
        #region ClickLeft
        /// <summary>
        /// Clicks the left button of the mouse at the current cursor position.
        /// </summary>
        public void ClickLeft()
        {
            PressLeft();
            ReleaseLeft();
        }
        #endregion
        #region ClickMiddle
        /// <summary>
        /// Clicks the middle button of the mouse at the current cursor position.
        /// </summary>
        public void ClickMiddle()
        {
            PressMiddle();
            ReleaseMiddle();
        }
        #endregion
        #region ClickRight
        /// <summary>
        /// Clicks the right button of the mouse at the current cursor position.
        /// </summary>
        public void ClickRight()
        {
            PressRight();
            ReleaseRight();
        }
        #endregion
        #region DoubleClickLeft
        /// <summary>
        /// Double clicks the left button of the mouse at the current cursor position.
        /// </summary>
        public void DoubleClickLeft()
        {
            ClickLeft();
            Thread.Sleep(10);
            ClickLeft();
        }
        #endregion
        #region MoveTo
        /// <summary>
        /// Moves the cursor at the specified coordinate from the position of the window.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        public void MoveTo(int x, int y)
        {
            MoveToAbsolute(Window.X + x, Window.Y + y);
        }
        #endregion
        #endregion
    }
}
