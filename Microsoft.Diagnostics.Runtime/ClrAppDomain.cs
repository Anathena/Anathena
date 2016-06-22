using System;
using System.Collections.Generic;
using Address = System.UInt64;

namespace Microsoft.Diagnostics.Runtime
{
    /// <summary>
    /// Represents an AppDomain in the target runtime.
    /// </summary>
    public abstract class ClrAppDomain
    {
        /// <summary>
        /// Gets the runtime associated with this ClrAppDomain.
        /// </summary>
        public abstract ClrRuntime Runtime { get; }

        /// <summary>
        /// Address of the AppDomain.
        /// </summary>
        public abstract Address Address { get; }

        /// <summary>
        /// The AppDomain's ID.
        /// </summary>
        public abstract int Id { get; }

        /// <summary>
        /// The name of the AppDomain, as specified when the domain was created.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Returns a list of modules loaded into this AppDomain.
        /// </summary>
        public abstract IList<ClrModule> Modules { get; }

        /// <summary>
        /// Returns the config file used for the AppDomain.  This may be null if there was no config file
        /// loaded, or if the targeted runtime does not support enumerating that data.
        /// </summary>
        public abstract string ConfigurationFile { get; }

        /// <summary>
        /// Returns the base directory for this AppDomain.  This may return null if the targeted runtime does
        /// not support enumerating this information.
        /// </summary>
        public abstract string ApplicationBase { get; }

        /// <summary>
        /// To string override.
        /// </summary>
        /// <returns>The name of this AppDomain.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
