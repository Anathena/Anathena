namespace Anathena.Source.Engine.Architecture.Disassembler.SharpDisasm
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Utility class to safely manage pinned objects
    /// </summary>
    public sealed class AutoPinner : IDisposable
    {
        private Boolean disposed;
        private GCHandle pinnedObj;

        public AutoPinner(Object obj)
        {
            this.disposed = false;
            this.pinnedObj = GCHandle.Alloc(obj, GCHandleType.Pinned);
        }

        ~AutoPinner()
        {
            this.Dispose();
        }

        public static implicit operator IntPtr(AutoPinner ap)
        {
            if (ap.disposed)
            {
                throw new ObjectDisposedException("AutoPinner");
            }

            return ap.pinnedObj.AddrOfPinnedObject();
        }

        public IntPtr AddrOfPinnedObject()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("AutoPinner");
            }

            return this.pinnedObj.AddrOfPinnedObject();
        }

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