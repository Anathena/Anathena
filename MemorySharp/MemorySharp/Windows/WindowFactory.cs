/*
 * MemorySharp Library
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2014 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Binarysharp.MemoryManagement.Internals;

namespace Binarysharp.MemoryManagement.Windows
{
    /// <summary>
    /// Class providing tools for manipulating windows.
    /// </summary>
    public class WindowFactory : IFactory
    {
        #region Fields
        /// <summary>
        /// The reference of the <see cref="MemorySharp"/> object.
        /// </summary>
        private readonly MemorySharp _memorySharp;
        #endregion

        #region Properties
        #region ChildWindows
        /// <summary>
        /// Gets all the child windows that belong to the application.
        /// </summary>
        public IEnumerable<RemoteWindow> ChildWindows
        {
            get { return ChildWindowHandles.Select(handle => new RemoteWindow(_memorySharp, handle)); }
        }
        #endregion
        #region ChildWindowHandles (internal)
        /// <summary>
        /// Gets all the child window handles that belong to the application.
        /// </summary>
        internal IEnumerable<IntPtr> ChildWindowHandles
        {
            get { return WindowCore.EnumChildWindows(_memorySharp.Native.MainWindowHandle); }
        }
        #endregion
        #region MainWindow
        /// <summary>
        /// Gets the main window of the application.
        /// </summary>
        public RemoteWindow MainWindow
        {
            get { return new RemoteWindow(_memorySharp, MainWindowHandle); }
        }
        #endregion
        #region MainWindowHandle (internal)
        /// <summary>
        /// Gets the main window handle of the application.
        /// </summary>
        public IntPtr MainWindowHandle
        {
            get { return _memorySharp.Native.MainWindowHandle; }
        }
        #endregion
        #region This
        /// <summary>
        /// Gets all the windows that have the same specified title.
        /// </summary>
        /// <param name="windowTitle">The window title string.</param>
        /// <returns>A collection of <see cref="RemoteWindow"/>.</returns>
        public IEnumerable<RemoteWindow> this[string windowTitle]
        {
            get { return GetWindowsByTitle(windowTitle); }
        } 
        #endregion
        #region Windows
        /// <summary>
        /// Gets all the windows that belong to the application.
        /// </summary>
        public IEnumerable<RemoteWindow> RemoteWindows
        {
            get { return WindowHandles.Select(handle => new RemoteWindow(_memorySharp, handle)); }
        }
        #endregion
        #region WindowHandles (internal)
        /// <summary>
        /// Gets all the window handles that belong to the application.
        /// </summary>
        internal IEnumerable<IntPtr> WindowHandles
        {
            get
            {
                return new List<IntPtr>(ChildWindowHandles) {MainWindowHandle};
            }
        }
        #endregion
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowFactory"/> class.
        /// </summary>
        /// <param name="memorySharp">The reference of the <see cref="MemorySharp"/> object.</param>
        internal WindowFactory(MemorySharp memorySharp)
        {
            // Save the parameter
            _memorySharp = memorySharp;
        }
        #endregion

        #region Methods
        #region Dispose (implementation of IFactory)
        /// <summary>
        /// Releases all resources used by the <see cref="WindowFactory"/> object.
        /// </summary>
        public void Dispose()
        {
            // Nothing to dispose... yet
        }
        #endregion
        #region GetWindowsByClassName
        /// <summary>
        /// Gets all the windows that have the specified class name.
        /// </summary>
        /// <param name="className">The class name string.</param>
        /// <returns>A collection of <see cref="RemoteWindow"/>.</returns>
        public IEnumerable<RemoteWindow> GetWindowsByClassName(string className)
        {
            return WindowHandles
                .Where(handle => WindowCore.GetClassName(handle) == className)
                .Select(handle => new RemoteWindow(_memorySharp, handle));
        }
        #endregion
        #region GetWindowsByTitle
        /// <summary>
        /// Gets all the windows that have the same specified title.
        /// </summary>
        /// <param name="windowTitle">The window title string.</param>
        /// <returns>A collection of <see cref="RemoteWindow"/>.</returns>
        public IEnumerable<RemoteWindow> GetWindowsByTitle(string windowTitle)
        {
            return WindowHandles
                .Where(handle => WindowCore.GetWindowText(handle) == windowTitle)
                .Select(handle => new RemoteWindow(_memorySharp, handle));
        }
        #endregion
        #region GetWindowsByTitleContains
        /// <summary>
        /// Gets all the windows that contain the specified title.
        /// </summary>
        /// <param name="windowTitle">A part a window title string.</param>
        /// <returns>A collection of <see cref="RemoteWindow"/>.</returns>
        public IEnumerable<RemoteWindow> GetWindowsByTitleContains(string windowTitle)
        {
            return WindowHandles
                .Where(handle => WindowCore.GetWindowText(handle).Contains(windowTitle))
                .Select(handle => new RemoteWindow(_memorySharp, handle));
        }
        #endregion
        #endregion
    }
}
