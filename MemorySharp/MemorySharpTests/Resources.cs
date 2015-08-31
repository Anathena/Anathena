/*
 * MemorySharp Library v1.0.0
 * http://www.binarysharp.com/
 *
 * Copyright (C) 2012-2013 Jämes Ménétrey (a.k.a. ZenLulz).
 * This library is released under the MIT License.
 * See the file LICENSE for more information.
*/

using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Binarysharp.MemoryManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;

namespace MemorySharpTests
{
    internal static class Resources
    {
        /// <summary>
        /// Static constructor.
        /// </summary>
        static Resources()
        {
            if(ProcessTest == null)
                throw new Exception("You probably forgot to launch the test process.");
            _path = ProcessTest.MainModule.FileName;
        }

        /// <summary>
        /// The path of the test process.
        /// </summary>
        private static readonly string _path;

        /// <summary>
        /// A new instance of the <see cref="Point"/> structure.
        /// </summary>
        internal static Point CustomStruct
        {
            get { return new Point { X = 4, Y = 5, Z = 6 }; }
        }

        /// <summary>
        /// Provides the path of a test library (dll).
        /// </summary>
        internal static string LibraryTest
        {
            get
            {
                return Path.Combine(Environment.SystemDirectory, "apphelp.dll");
            }
        }

        /// <summary>
        /// A new instance of the <see cref="MemorySharp"/> class.
        /// </summary>
        internal static MemorySharp MemorySharp
        {
            get
            {
                return new MemorySharp(ProcessTest);
            }
        }

        /// <summary>
        /// The process itself.
        /// </summary>
        internal static Process ProcessSelf
        {
            get { return Process.GetCurrentProcess(); }
        }

        /// <summary>
        /// The process used for tests.
        /// </summary>
        internal static Process ProcessTest
        {
            get
            {
                return Process.GetProcessesByName("notepad++").FirstOrDefault();
            }
        }

        /// <summary>
        /// Performs the ending tests.
        /// </summary>
        internal static void EndTests(MemorySharp memorySharp)
        {
            Assert.AreEqual(0, memorySharp.Memory.RemoteAllocations.Count());
            Assert.AreEqual(0, memorySharp.Modules.InjectedModules.Count());
        }

        /// <summary>
        /// Restart the test process.
        /// </summary>
        internal static void Restart()
        {
            // Kill the process
            if (ProcessTest != null)
                ProcessTest.Kill();
            // Start it
            Process.Start(_path);
            Thread.Sleep(2000);
        }
    }

    /// <summary>
    /// A sample test structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Point : IEquatable<Point>
    {
        public int X;
        public int Y;
        public int Z;

        #region Equality methods
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Point && Equals((Point)obj);
        }

        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !left.Equals(right);
        }
        #endregion
    }
}
