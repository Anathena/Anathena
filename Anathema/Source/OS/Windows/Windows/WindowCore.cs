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
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Binarysharp.MemoryManagement.Helpers;
using Binarysharp.MemoryManagement.Internals;
using Binarysharp.MemoryManagement.Native;

namespace Binarysharp.MemoryManagement.Windows
{
    /// <summary>
    /// Static core class providing tools for managing windows.
    /// </summary>
    public static class WindowCore
    {
        #region GetClassName
        /// <summary>
        /// Retrieves the name of the class to which the specified window belongs.
        /// </summary>
        /// <param name="windowHandle">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <returns>The return values is the class name string.</returns>
        public static string GetClassName(IntPtr windowHandle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(windowHandle, "windowHandle");

            // Get the window class name
            var stringBuilder = new StringBuilder(char.MaxValue);
            if (NativeMethods.GetClassName(windowHandle, stringBuilder, stringBuilder.Capacity) == 0)
                throw new Win32Exception("Couldn't get the class name of the window or the window has no class name.");

            return stringBuilder.ToString();
        }
        #endregion

        #region GetForegroundWindow
        /// <summary>
        /// Retrieves a handle to the foreground window (the window with which the user is currently working).
        /// </summary>
        /// <returns>A handle to the foreground window. The foreground window can be <c>IntPtr.Zero</c> in certain circumstances, such as when a window is losing activation.</returns>
        public static IntPtr GetForegroundWindow()
        {
            return NativeMethods.GetForegroundWindow();
        }
        #endregion

        #region GetSystemMetrics
        /// <summary>
        /// Retrieves the specified system metric or system configuration setting.
        /// </summary>
        /// <param name="metric">The system metric or configuration setting to be retrieved.</param>
        /// <returns>The return value is the requested system metric or configuration setting.</returns>
        public static int GetSystemMetrics(SystemMetrics metric)
        {
            var ret = NativeMethods.GetSystemMetrics(metric);

            if (ret != 0)
                return ret;

            throw new Win32Exception("The call of GetSystemMetrics failed. Unfortunately, GetLastError code doesn't provide more information.");
        }
        #endregion

        #region GetWindowText
        /// <summary>
        /// Gets the text of the specified window's title bar.
        /// </summary>
        /// <param name="windowHandle">A handle to the window containing the text.</param>
        /// <returns>The return value is the window's title bar.</returns>
        public static string GetWindowText(IntPtr windowHandle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(windowHandle, "windowHandle");

            // Get the size of the window's title
            var capacity = NativeMethods.GetWindowTextLength(windowHandle);
            // If the window doesn't contain any title
            if (capacity == 0)
                return string.Empty;

            // Get the text of the window's title bar text
            var stringBuilder = new StringBuilder(capacity + 1);
            if (NativeMethods.GetWindowText(windowHandle, stringBuilder, stringBuilder.Capacity) == 0)
                throw new Win32Exception("Couldn't get the text of the window's title bar or the window has no title.");

            return stringBuilder.ToString();
        }
        #endregion

        #region GetWindowPlacement
        /// <summary>
        /// Retrieves the show state and the restored, minimized, and maximized positions of the specified window.
        /// </summary>
        /// <param name="windowHandle">A handle to the window.</param>
        /// <returns>The return value is a <see cref="WindowPlacement"/> structure that receives the show state and position information.</returns>
        public static WindowPlacement GetWindowPlacement(IntPtr windowHandle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(windowHandle, "windowHandle");

            // Allocate a WindowPlacement structure
            WindowPlacement placement;
            placement.Length = Marshal.SizeOf(typeof(WindowPlacement));

            // Get the window placement
            if (!NativeMethods.GetWindowPlacement(windowHandle, out placement))
                throw new Win32Exception("Couldn't get the window placement.");

            return placement;
        }
        #endregion

        #region GetWindowProcessId
        /// <summary>
        /// Retrieves the identifier of the process that created the window. 
        /// </summary>
        /// <param name="windowHandle">A handle to the window.</param>
        /// <returns>The return value is the identifier of the process that created the window.</returns>
        public static int GetWindowProcessId(IntPtr windowHandle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(windowHandle, "windowHandle");

            // Get the process id
            int processId;
            NativeMethods.GetWindowThreadProcessId(windowHandle, out processId);

            return processId;
        }
        #endregion

        #region GetWindowThreadId
        /// <summary>
        /// Retrieves the identifier of the thread that created the specified window.
        /// </summary>
        /// <param name="windowHandle">A handle to the window.</param>
        /// <returns>The return value is the identifier of the thread that created the window.</returns>
        public static int GetWindowThreadId(IntPtr windowHandle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(windowHandle, "windowHandle");

            // Get the thread id
            int trash;
            return NativeMethods.GetWindowThreadProcessId(windowHandle, out trash);
        }
        #endregion

        #region EnumAllWindows
        /// <summary>
        /// Enumerates all the windows on the screen.
        /// </summary>
        /// <returns>A collection of handles of all the windows.</returns>
        public static IEnumerable<IntPtr> EnumAllWindows()
        {
            // Create the list of windows
            var list = new List<IntPtr>();

            // For each top-level windows
            foreach (var topWindow in EnumTopLevelWindows())
            {
                // Add this window to the list
                list.Add(topWindow);
                // Enumerate and add the children of this window
                list.AddRange(EnumChildWindows(topWindow));

            }

            // Return the list of windows
            return list;
        }
        #endregion

        #region EnumChildWindows
        /// <summary>
        /// Enumerates recursively all the child windows that belong to the specified parent window.
        /// </summary>
        /// <param name="parentHandle">The parent window handle.</param>
        /// <returns>A collection of handles of the child windows.</returns>
        public static IEnumerable<IntPtr> EnumChildWindows(IntPtr parentHandle)
        {
            // Create the list of windows
            var list = new List<IntPtr>();
            // Create the callback
            EnumWindowsProc callback = delegate(IntPtr windowHandle, IntPtr lParam)
            {
                list.Add(windowHandle);
                return true;
            };
            // Enumerate all windows
            NativeMethods.EnumChildWindows(parentHandle, callback, IntPtr.Zero);

            // Returns the list of the windows
            return list.ToArray();
        }
        #endregion

        #region EnumTopLevelWindows
        /// <summary>
        /// Enumerates all top-level windows on the screen. This function does not search child windows. 
        /// </summary>
        /// <returns>A collection of handles of top-level windows.</returns>
        public static IEnumerable<IntPtr> EnumTopLevelWindows()
        {
            // When passing a null pointer, this function is equivalent to EnumWindows
            return EnumChildWindows(IntPtr.Zero);
        }
        #endregion

        #region FlashWindow
        /// <summary>
        /// Flashes the specified window one time. It does not change the active state of the window.
        /// To flash the window a specified number of times, use the <see cref="FlashWindowEx(IntPtr, FlashWindowFlags, uint, TimeSpan)"/> function.
        /// </summary>
        /// <param name="windowHandle">A handle to the window to be flashed. The window can be either open or minimized.</param>
        /// <returns>
        /// The return value specifies the window's state before the call to the <see cref="FlashWindow"/> function. 
        /// If the window caption was drawn as active before the call, the return value is nonzero. Otherwise, the return value is zero.
        /// </returns>
        public static bool FlashWindow(IntPtr windowHandle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(windowHandle, "windowHandle");

            // Flash the window
            return NativeMethods.FlashWindow(windowHandle, true);
        }
        #endregion

        #region FlashWindowEx
        /// <summary>
        /// Flashes the specified window. It does not change the active state of the window.
        /// </summary>
        /// <param name="windowHandle">A handle to the window to be flashed. The window can be either opened or minimized.</param>
        /// <param name="flags">The flash status.</param>
        /// <param name="count">The number of times to flash the window.</param>
        /// <param name="timeout">The rate at which the window is to be flashed.</param>
        public static void FlashWindowEx(IntPtr windowHandle, FlashWindowFlags flags, uint count, TimeSpan timeout)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(windowHandle, "windowHandle");

            // Create the data structure
            var flashInfo = new FlashInfo
                                {
                                    Size = Marshal.SizeOf(typeof(FlashInfo)),
                                    Hwnd = windowHandle,
                                    Flags = flags,
                                    Count = count,
                                    Timeout = Convert.ToInt32(timeout.TotalMilliseconds)
                                };

            // Flash the window
            NativeMethods.FlashWindowEx(ref flashInfo);
        }

        /// <summary>
        /// Flashes the specified window. It does not change the active state of the window. The function uses the default cursor blink rate.
        /// </summary>
        /// <param name="windowHandle">A handle to the window to be flashed. The window can be either opened or minimized.</param>
        /// <param name="flags">The flash status.</param>
        /// <param name="count">The number of times to flash the window.</param>
        public static void FlashWindowEx(IntPtr windowHandle, FlashWindowFlags flags, uint count)
        {
            FlashWindowEx(windowHandle, flags, count, TimeSpan.FromMilliseconds(0));
        }

        /// <summary>
        /// Flashes the specified window. It does not change the active state of the window. The function uses the default cursor blink rate.
        /// </summary>
        /// <param name="windowHandle">A handle to the window to be flashed. The window can be either opened or minimized.</param>
        /// <param name="flags">The flash status.</param>
        public static void FlashWindowEx(IntPtr windowHandle, FlashWindowFlags flags)
        {
            FlashWindowEx(windowHandle, flags, 0);
        }
        #endregion

        #region MapVirtualKey
        /// <summary>
        /// Translates (maps) a virtual-key code into a scan code or character value, or translates a scan code into a virtual-key code.
        /// To specify a handle to the keyboard layout to use for translating the specified code, use the MapVirtualKeyEx function.
        /// </summary>
        /// <param name="key">
        /// The virtual key code or scan code for a key. How this value is interpreted depends on the value of the uMapType parameter.
        /// </param>
        /// <param name="translation">
        /// The translation to be performed. The value of this parameter depends on the value of the uCode parameter.
        /// </param>
        /// <returns>
        /// The return value is either a scan code, a virtual-key code, or a character value, depending on the value of uCode and uMapType. 
        /// If there is no translation, the return value is zero.
        /// </returns>
        public static uint MapVirtualKey(uint key, TranslationTypes translation)
        {
            return NativeMethods.MapVirtualKey(key, translation);
        }
        /// <summary>
        /// Translates (maps) a virtual-key code into a scan code or character value, or translates a scan code into a virtual-key code.
        /// To specify a handle to the keyboard layout to use for translating the specified code, use the MapVirtualKeyEx function.
        /// </summary>
        /// <param name="key">
        /// The virtual key code for a key. How this value is interpreted depends on the value of the uMapType parameter.
        /// </param>
        /// <param name="translation">
        /// The translation to be performed. The value of this parameter depends on the value of the uCode parameter.
        /// </param>
        /// <returns>
        /// The return value is either a scan code, a virtual-key code, or a character value, depending on the value of uCode and uMapType. 
        /// If there is no translation, the return value is zero.
        /// </returns>
        public static uint MapVirtualKey(Keys key, TranslationTypes translation)
        {
            return MapVirtualKey((uint)key, translation);
        }
        #endregion

        #region PostMessage
        /// <summary>
        /// Places (posts) a message in the message queue associated with the thread that created the specified window and returns without waiting for the thread to process the message.
        /// </summary>
        /// <param name="windowHandle">A handle to the window whose window procedure is to receive the message. The following values have special meanings.</param>
        /// <param name="message">The message to be posted.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        public static void PostMessage(IntPtr windowHandle, uint message, UIntPtr wParam, UIntPtr lParam)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(windowHandle, "windowHandle");

            // Post the message
            if (!NativeMethods.PostMessage(windowHandle, message, wParam, lParam))
                throw new Win32Exception(string.Format("Couldn't post the message '{0}'.", message));
        }
        /// <summary>
        /// Places (posts) a message in the message queue associated with the thread that created the specified window and returns without waiting for the thread to process the message.
        /// </summary>
        /// <param name="windowHandle">A handle to the window whose window procedure is to receive the message. The following values have special meanings.</param>
        /// <param name="message">The message to be posted.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        public static void PostMessage(IntPtr windowHandle, WindowsMessages message, UIntPtr wParam, UIntPtr lParam)
        {
            PostMessage(windowHandle, (uint)message, wParam, lParam);
        }
        #endregion

        #region SendInput
        /// <summary>
        /// Synthesizes keystrokes, mouse motions, and button clicks.
        /// </summary>
        /// <param name="inputs">An array of <see cref="Input"/> structures. Each structure represents an event to be inserted into the keyboard or mouse input stream.</param>
        public static void SendInput(Input[] inputs)
        {
            // Check if the array passed in parameter is not empty
            if (inputs != null && inputs.Length != 0)
            {
                if (NativeMethods.SendInput(inputs.Length, inputs, MarshalType<Input>.Size) == 0)
                    throw new Win32Exception("Couldn't send the inputs.");
            }
            else
            {
                throw new ArgumentException("The parameter cannot be null or empty.", "inputs");
            }
        }
        /// <summary>
        /// Synthesizes keystrokes, mouse motions, and button clicks.
        /// </summary>
        /// <param name="input">A structure represents an event to be inserted into the keyboard or mouse input stream.</param>
        public static void SendInput(Input input)
        {
            SendInput(new[] { input });
        }
        #endregion

        #region SendMessage
        /// <summary>
        /// Sends the specified message to a window or windows.
        /// The SendMessage function calls the window procedure for the specified window and does not return until the window procedure has processed the message.
        /// </summary>
        /// <param name="windowHandle">A handle to the window whose window procedure will receive the message.</param>
        /// <param name="message">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
        public static IntPtr SendMessage(IntPtr windowHandle, uint message, UIntPtr wParam, IntPtr lParam)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(windowHandle, "windowHandle");

            // Send the message
            return NativeMethods.SendMessage(windowHandle, message, wParam, lParam);
        }
        /// <summary>
        /// Sends the specified message to a window or windows.
        /// The SendMessage function calls the window procedure for the specified window and does not return until the window procedure has processed the message.
        /// </summary>
        /// <param name="windowHandle">A handle to the window whose window procedure will receive the message.</param>
        /// <param name="message">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
        public static IntPtr SendMessage(IntPtr windowHandle, WindowsMessages message, UIntPtr wParam, IntPtr lParam)
        {
            return SendMessage(windowHandle, (uint)message, wParam, lParam);
        }
        #endregion

        #region SetForegroundWindow
        /// <summary>
        /// Brings the thread that created the specified window into the foreground and activates the window. 
        /// The window is restored if minimized. Performs no action if the window is already activated.
        /// </summary>
        /// <param name="windowHandle">A handle to the window that should be activated and brought to the foreground.</param>
        /// <returns>
        /// If the window was brought to the foreground, the return value is <c>true</c>, otherwise the return value is <c>false</c>.
        /// </returns>
        public static void SetForegroundWindow(IntPtr windowHandle)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(windowHandle, "windowHandle");

            // If the window is already activated, do nothing
            if (GetForegroundWindow() == windowHandle)
                return;

            // Restore the window if minimized
            ShowWindow(windowHandle, WindowStates.Restore);

            // Activate the window
            if(!NativeMethods.SetForegroundWindow(windowHandle))
                throw new ApplicationException("Couldn't set the window to foreground.");
        }
        #endregion

        #region SetWindowPlacement
        /// <summary>
        /// Sets the current position and size of the specified window.
        /// </summary>
        /// <param name="windowHandle">A handle to the window.</param>
        /// <param name="left">The x-coordinate of the upper-left corner of the window.</param>
        /// <param name="top">The y-coordinate of the upper-left corner of the window.</param>
        /// <param name="height">The height of the window.</param>
        /// <param name="width">The width of the window.</param>
        public static void SetWindowPlacement(IntPtr windowHandle, int left, int top, int height, int width)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(windowHandle, "windowHandle");

            // Get a WindowPlacement structure of the current window
            var placement = GetWindowPlacement(windowHandle);

            // Set the values
            placement.NormalPosition.Left = left;
            placement.NormalPosition.Top = top;
            placement.NormalPosition.Height = height;
            placement.NormalPosition.Width = width;

            // Set the window placement
            SetWindowPlacement(windowHandle, placement);
        }
        /// <summary>
        /// Sets the show state and the restored, minimized, and maximized positions of the specified window.
        /// </summary>
        /// <param name="windowHandle">A handle to the window.</param>
        /// <param name="placement">A pointer to the <see cref="WindowPlacement"/> structure that specifies the new show state and window positions.</param>
        public static void SetWindowPlacement(IntPtr windowHandle, WindowPlacement placement)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(windowHandle, "windowHandle");

            // If the debugger is attached and the state of the window is ShowDefault, there's an issue where the window disappears
            if (Debugger.IsAttached && placement.ShowCmd == WindowStates.ShowNormal)
                placement.ShowCmd = WindowStates.Restore;

            // Set the window placement
            if (!NativeMethods.SetWindowPlacement(windowHandle, ref placement))
                throw new Win32Exception("Couldn't set the window placement.");
        }
        #endregion

        #region SetWindowText
        /// <summary>
        /// Sets the text of the specified window's title bar.
        /// </summary>
        /// <param name="windowHandle">A handle to the window whose text is to be changed.</param>
        /// <param name="title">The new title text.</param>
        public static void SetWindowText(IntPtr windowHandle, string title)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(windowHandle, "windowHandle");

            // Set the text of the window's title bar
            if (!NativeMethods.SetWindowText(windowHandle, title))
                throw new Win32Exception("Couldn't set the text of the window's title bar.");
        }
        #endregion

        #region ShowWindow
        /// <summary>
        /// Sets the specified window's show state.
        /// </summary>
        /// <param name="windowHandle">A handle to the window.</param>
        /// <param name="state">Controls how the window is to be shown.</param>
        /// <returns>If the window was previously visible, the return value is <c>true</c>, otherwise the return value is <c>false</c>.</returns>
        public static bool ShowWindow(IntPtr windowHandle, WindowStates state)
        {
            // Check if the handle is valid
            HandleManipulator.ValidateAsArgument(windowHandle, "windowHandle");

            // Change the state of the window
            return NativeMethods.ShowWindow(windowHandle, state);
        }
        #endregion
    }
}
