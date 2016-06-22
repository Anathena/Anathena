// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Address = System.UInt64;

namespace Microsoft.Diagnostics.Runtime.Desktop
{
    internal class DesktopAppDomain : ClrAppDomain
    {
        public override ClrRuntime Runtime
        {
            get
            {
                return _runtime;
            }
        }

        /// <summary>
        /// Address of the AppDomain.
        /// </summary>
        public override Address Address { get { return _address; } }

        /// <summary>
        /// The AppDomain's ID.
        /// </summary>
        public override int Id { get { return _id; } }

        /// <summary>
        /// The name of the AppDomain, as specified when the domain was created.
        /// </summary>
        public override string Name { get { return _name; } }
        public override IList<ClrModule> Modules { get { return _modules; } }

        internal int InternalId { get { return _internalId; } }

        public override string ConfigurationFile
        {
            get { return _runtime.GetConfigFile(_address); }
        }

        public override string ApplicationBase
        {
            get
            {
                string appBase = _runtime.GetAppBase(_address);
                if (string.IsNullOrEmpty(appBase))
                    return null;

                Uri uri = new Uri(appBase);
                try
                {
                    return uri.AbsolutePath.Replace('/', '\\');
                }
                catch (InvalidOperationException)
                {
                    return appBase;
                }
            }
        }

        internal DesktopAppDomain(DesktopRuntimeBase runtime, IAppDomainData data, string name)
        {
            _address = data.Address;
            _id = data.Id;
            _name = name;
            _internalId = s_internalId++;
            _runtime = runtime;
        }

        internal void AddModule(ClrModule module)
        {
            _modules.Add(module);
        }

        #region Private
        private Address _address;
        private string _name;
        private int _id, _internalId;
        private List<ClrModule> _modules = new List<ClrModule>();
        private DesktopRuntimeBase _runtime;

        private static int s_internalId;
        #endregion
    }
}
