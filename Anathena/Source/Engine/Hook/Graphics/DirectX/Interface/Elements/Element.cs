using System;
using System.Runtime.Remoting;
using System.Security.Permissions;

namespace Anathena.Source.Engine.Hook.Graphics.DirectX.Interface.Elements
{
    [Serializable]
    public abstract class Element : MarshalByRefObject, ICloneable, IDisposable
    {
        public virtual Boolean Visible { get; set; }

        public Element()
        {
            Visible = true;
        }

        ~Element()
        {
            Dispose(false);
        }

        public virtual void Frame() { }

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
        /// <param name="Disposing">true if disposing both unmanaged and managed</param>
        protected virtual void Dispose(Boolean Disposing)
        {
            if (Disposing)
                Disconnect();
        }

        protected void SafeDispose(IDisposable DisposableObject)
        {
            if (DisposableObject != null)
                DisposableObject.Dispose();
        }

        /// <summary>
        /// Disconnects the remoting channel(s) of this object and all nested objects.
        /// </summary>
        private void Disconnect()
        {
            RemotingServices.Disconnect(this);
        }

        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.Infrastructure)]
        public override Object InitializeLifetimeService()
        {
            // Returning null designates an infinite non-expiring lease.
            // We must therefore ensure that RemotingServices.Disconnect() is called when
            // it's no longer needed otherwise there will be a memory leak.
            return null;
        }

    } // End class

} // End namespace