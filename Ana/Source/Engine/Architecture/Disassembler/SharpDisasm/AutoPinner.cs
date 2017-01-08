namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Utility class to safely manage pinned objects.
    /// </summary>
    public sealed class AutoPinner : IDisposable
    {
        /// <summary>
        /// TODO TODO.
        /// </summary>
        private Boolean disposed;

        /// <summary>
        /// TODO TODO.
        /// </summary>
        private GCHandle pinnedObj;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoPinner" /> class.
        /// </summary>
        /// <param name="obj">Object to pin.</param>
        public AutoPinner(Object obj)
        {
            this.disposed = false;
            this.pinnedObj = GCHandle.Alloc(obj, GCHandleType.Pinned);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="AutoPinner" /> class.
        /// </summary>
        ~AutoPinner()
        {
            this.Dispose();
        }

        /// <summary>
        /// TODO TODO.
        /// </summary>
        /// <param name="ap">TODO ap.</param>
        public static implicit operator IntPtr(AutoPinner ap)
        {
            if (ap.disposed)
            {
                throw new ObjectDisposedException("AutoPinner");
            }

            return ap.pinnedObj.AddrOfPinnedObject();
        }

        /// <summary>
        /// Gets address of binned object.
        /// </summary>
        /// <returns>Address of pinned object as an IntPtr.</returns>
        public IntPtr AddrOfPinnedObject()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("AutoPinner");
            }

            return this.pinnedObj.AddrOfPinnedObject();
        }

        /// <summary>
        /// TODO TODO.
        /// </summary>
        public void Dispose()
        {
            if (this.pinnedObj.IsAllocated)
            {
                this.pinnedObj.Free();
            }

            this.disposed = true;
        }
    }
    //// End class
}
//// End namespace