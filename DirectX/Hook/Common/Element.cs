using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Permissions;
using System.Text;

namespace Capture.Hook.Common
{
    [Serializable]
    public abstract class Element: MarshalByRefObject, IOverlayElement, IDisposable
    {
        public virtual bool Hidden { get; set; }

        ~Element()
        {
            Dispose(false);
        }

        public virtual void Frame()
        {
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and optionally managed resources
        /// </summary>
        /// <param name="disposing">true if disposing both unmanaged and managed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Disconnect();
            }
        }

        protected void SafeDispose(IDisposable disposableObj)
        {
            if (disposableObj != null)
                disposableObj.Dispose();
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
