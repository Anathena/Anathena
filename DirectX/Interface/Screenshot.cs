using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.Remoting;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace Capture.Interface
{
    public class Screenshot : MarshalByRefObject, IDisposable
    {
        Guid _requestId;
        public Guid RequestId
        {
            get
            {
                return _requestId;
            }
        }

        public ImageFormat Format { get; set; }

        public System.Drawing.Imaging.PixelFormat PixelFormat { get; set; }
        public int Stride { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        byte[] _data;
        public byte[] Data
        {
            get
            {
                return _data;
            }
        }

        private bool _disposed;

        public Screenshot(Guid requestId, byte[] data)
        {
            _requestId = requestId;
            _data = data;
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

        protected virtual void Dispose(bool disposeManagedResources)
        {
            if (!_disposed)
            {
                if (disposeManagedResources)
                {
                    Disconnect();
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// Disconnects the remoting channel(s) of this object and all nested objects.
        /// </summary>
        private void Disconnect()
        {
            RemotingServices.Disconnect(this);
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.Infrastructure)]
        public override object InitializeLifetimeService()
        {
            // Returning null designates an infinite non-expiring lease.
            // We must therefore ensure that RemotingServices.Disconnect() is called when
            // it's no longer needed otherwise there will be a memory leak.
            return null;
        }
    }
}
