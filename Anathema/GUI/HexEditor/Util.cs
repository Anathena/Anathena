using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Be.HexEditor
{
    static class Util
    {
        /// <summary>
        /// Contains true, if we are in design mode of Visual Studio
        /// </summary>
        private static bool _designMode;

        /// <summary>
        /// Initializes an instance of Util class
        /// </summary>
        static Util()
        {
            // design mode is true if host process is: Visual Studio, Visual Studio Express Versions (C#, VB, C++) or SharpDevelop
            var designerHosts = new List<string>() { "devenv", "vcsexpress", "vbexpress", "vcexpress", "sharpdevelop" };
            using (var process = System.Diagnostics.Process.GetCurrentProcess())
            {
                var processName = process.ProcessName.ToLower();
                _designMode = designerHosts.Contains(processName);
            }
        }

        /// <summary>
        /// Gets true, if we are in design mode of Visual Studio
        /// </summary>
        /// <remarks>
        /// In Visual Studio 2008 SP1 the designer is crashing sometimes on windows forms. 
        /// The DesignMode property of Control class is buggy and cannot be used, so use our own implementation instead.
        /// </remarks>
        public static bool DesignMode
        {
            get
            {
                return _designMode;
            }
        }

        public static string GetDisplayBytes(long size)
        {
            const long multi = 1024;
            long kb = multi;
            long mb = kb*multi;
            long gb = mb*multi;
            long tb = gb*multi;

            const string BYTES = "Bytes";
            const string KB = "KB";
            const string MB = "MB";
            const string GB = "GB";
            const string TB = "TB";

            string result;
            if (size < kb)
                result = string.Format("{0} {1}", size, BYTES);
            else if(size < mb)
                result = string.Format("{0} {1} ({2} Bytes)", 
                    ConvertToOneDigit(size, kb), KB, ConvertBytesDisplay(size));
            else if(size < gb)
                result = string.Format("{0} {1} ({2} Bytes)",
                    ConvertToOneDigit(size, mb), MB, ConvertBytesDisplay(size));
            else if(size < tb)
                result = string.Format("{0} {1} ({2} Bytes)",
                    ConvertToOneDigit(size, gb), GB, ConvertBytesDisplay(size));
            else
                result = string.Format("{0} {1} ({2} Bytes)",
                    ConvertToOneDigit(size, tb), TB, ConvertBytesDisplay(size));

            return result;
        }

        static string ConvertBytesDisplay(long size)
        {
            return size.ToString("###,###,###,###,###", CultureInfo.CurrentCulture);
        }

        static string ConvertToOneDigit(long size, long quan)
        {
            double quotient = (double)size / (double)quan;
            string result = quotient.ToString("0.#", CultureInfo.CurrentCulture);
            return result;
        }
    }
}
