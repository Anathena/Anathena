// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


// #define DEBUG_SERIALIZE

using System;
using System.Collections.Generic;
using System.ComponentModel; // For Win32Excption;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using Address = System.UInt64;

#pragma warning disable 649

namespace Microsoft.Diagnostics.Runtime.Utilities
{
    /// <summary>
    /// SymPathElement represents the text between the semicolons in a symbol path.  It can be a symbol server specification or a simple directory path. 
    /// 
    /// SymPathElement follows functional conventions.  After construction everything is read-only. 
    /// </summary>
    public class SymPathElement
    {
        /// <summary>
        /// returns a list of SymPathElements from a semicolon delimited string representing a symbol path
        /// </summary>
        public static List<SymPathElement> GetElements(string symbolPath)
        {
            List<SymPathElement> result = new List<SymPathElement>();
            string[] entries = (symbolPath ?? "").Split(';');
            result = new List<SymPathElement>(entries.Length);

            foreach (string element in entries)
                if (!string.IsNullOrEmpty(element))
                    result.Add(new SymPathElement(element));

            return result;
        }

        /// <summary>
        /// returns true if this element of the symbol server path a symbol server specification
        /// </summary>
        public bool IsSymServer { get; private set; }
        /// <summary>
        /// returns the local cache for a symbol server specifcation.  returns null if not specified
        /// </summary>
        public string Cache { get; set; }
        /// <summary>
        /// returns location to look for symbols.  This is either a directory specification or an URL (for symbol servers)
        /// </summary>
        public string Target { get; private set; }

        /// <summary>
        /// IsRemote returns true if it looks like the target is not on the local machine.
        /// </summary>
        public bool IsRemote
        {
            get
            {
                if (Target != null)
                {
                    if (Target.StartsWith(@"\\"))
                        return true;
                    // We assume drive letters from the back of the alphabet are remote.  
                    if (2 <= Target.Length && Target[1] == ':')
                    {
                        char driveLetter = Char.ToUpperInvariant(Target[0]);
                        if ('T' <= driveLetter && driveLetter <= 'Z')
                            return true;
                    }
                }

                if (!IsSymServer)
                    return false;
                if (Cache != null)
                    return true;
                if (Target == null)
                    return false;

                if (Target.StartsWith("http:/", StringComparison.OrdinalIgnoreCase))
                    return true;
                return false;
            }
        }
        /// <summary>
        /// returns the string repsentation for the symbol server path element (e.g. SRV*c:\temp*\\symbols\symbols)
        /// </summary>
        public override string ToString()
        {
            if (IsSymServer)
            {
                var ret = "SRV";
                if (Cache != null)
                    ret += "*" + Cache;
                if (Target != null)
                    ret += "*" + Target;
                return ret;
            }
            else
                return Target;
        }
        #region overrides

        /// <summary>
        /// Implements object interface
        /// </summary>
        public override bool Equals(object obj)
        {
            var asSymPathElem = obj as SymPathElement;
            if (asSymPathElem == null)
                return false;
            return
                Target == asSymPathElem.Target &&
                Cache == asSymPathElem.Cache &&
                IsSymServer == asSymPathElem.IsSymServer;
        }
        /// <summary>
        /// Implements object interface
        /// </summary>
        public override int GetHashCode()
        {
            return Target.GetHashCode();
        }
        #endregion
        #region private
        internal SymPathElement InsureHasCache(string defaultCachePath)
        {
            if (!IsSymServer)
                return this;
            if (Cache != null)
                return this;
            if (Target == defaultCachePath)
                return this;
            return new SymPathElement(true, defaultCachePath, Target);
        }
        internal SymPathElement LocalOnly()
        {
            if (!IsRemote)
                return this;
            if (Cache != null)
                return new SymPathElement(true, null, Cache);
            return null;
        }

        /// <summary>
        /// returns a new SymPathElement with the corresponding properties initialized
        /// </summary>
        public SymPathElement(bool isSymServer, string cache, string target)
        {
            IsSymServer = isSymServer;
            Cache = cache;
            Target = target;
        }
        internal SymPathElement(string strElem)
        {
            var m = Regex.Match(strElem, @"^\s*(SRV\*|http:)((\s*.*?\s*)\*)?\s*(.*?)\s*$", RegexOptions.IgnoreCase);
            if (m.Success)
            {
                IsSymServer = true;
                Cache = m.Groups[3].Value;
                if (m.Groups[1].Value.Equals("http:", StringComparison.CurrentCultureIgnoreCase))
                    Target = "http:" + m.Groups[4].Value;
                else
                    Target = m.Groups[4].Value;
                if (Cache.Length == 0)
                    Cache = null;
                if (Target.Length == 0)
                    Target = null;
                return;
            }
            m = Regex.Match(strElem, @"^\s*CACHE\*(.*?)\s*$", RegexOptions.IgnoreCase);
            if (m.Success)
            {
                IsSymServer = true;
                Cache = m.Groups[1].Value;
            }
            else
                Target = strElem.Trim();
        }
        #endregion
    }
}
