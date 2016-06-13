using System;
using System.Drawing.Imaging;
using System.Runtime.Remoting;
using System.Security.Permissions;

namespace DirectXShell.Interface
{
    public class Screenshot : MarshalByRefObject, IDisposable
    {
        public Guid RequestId { get; set; }

        public ImageFormatEnum Format { get; set; }

        public PixelFormat PixelFormat { get; set; }
        public Int32 Stride { get; set; }
        public Int32 Height { get; set; }
        public Int32 Width { get; set; }

        public Byte[] Data { get; set; }

        private Boolean Disposed;

        public Screenshot(Guid RequestId, Byte[] Data)
        {
            this.RequestId = RequestId;
            this.Data = Data;
        }

        ~Screenshot()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean DisposeManagedResources)
        {
            if (Disposed)
                return;

            if (DisposeManagedResources)
                Disconnect();

            Disposed = true;
        }

        /// <summary>
        /// Disconnects the remoting channel(s) of this object and all nested objects.
        /// </summary>
        private void Disconnect()
        {
            RemotingServices.Disconnect(this);
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.Infrastructure)]
        public override Object InitializeLifetimeService()
        {
            // Returning null designates an infinite non-expiring lease.
            // We must therefore ensure that RemotingServices.Disconnect() is called when
            // it's no longer needed otherwise there will be a memory leak.
            return null;
        }

    } // End class

} // End namespace