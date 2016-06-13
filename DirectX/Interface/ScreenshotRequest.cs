using System;
using System.Drawing;
using System.Runtime.Remoting;
using System.Security.Permissions;

namespace DirectXShell.Interface
{
    [Serializable]
    public class ScreenshotRequest : MarshalByRefObject, IDisposable
    {
        public Guid RequestId { get; set; }
        public Rectangle Region { get; set; }
        public Size? Resize { get; set; }
        public ImageFormatEnum Format { get; set; }

        public ScreenshotRequest(Rectangle Region, Size Resize) : this(Guid.NewGuid(), Region, Resize) { }
        public ScreenshotRequest(Rectangle Region) : this(Guid.NewGuid(), Region, null) { }
        public ScreenshotRequest(Guid RequestId, Rectangle Region) : this(RequestId, Region, null) { }
        public ScreenshotRequest(Guid RequestId, Rectangle Region, Size? Resize)
        {
            this.RequestId = RequestId;
            this.Region = Region;
            this.Resize = Resize;
        }

        public ScreenshotRequest Clone()
        {
            return new ScreenshotRequest(RequestId, Region, Resize) { Format = Format };
        }

        ~ScreenshotRequest()
        {
            Dispose(false);
        }

        private Boolean Disposed;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean Disposing)
        {
            if (Disposed)
                return;

            if (Disposing)
            {
                Disconnect();
            }

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