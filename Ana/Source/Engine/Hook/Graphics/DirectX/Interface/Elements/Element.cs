namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface.Elements
{
    using System;
    using System.Runtime.Remoting;
    using System.Security.Permissions;

    [Serializable]
    internal abstract class Element : MarshalByRefObject, ICloneable, IDisposable
    {
        public Element()
        {
            this.Visible = true;
        }

        ~Element()
        {
            this.Dispose(false);
        }

        public virtual Boolean Visible { get; set; }

        public virtual void Frame()
        {
        }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.Infrastructure)]
        public override Object InitializeLifetimeService()
        {
            // Returning null designates an infinite non-expiring lease.
            // We must therefore ensure that RemotingServices.Disconnect() is called when
            // it's no longer needed otherwise there will be a memory leak.
            return null;
        }

        /// <summary>
        /// Releases unmanaged and optionally managed resources
        /// </summary>
        /// <param name="disposing">true if disposing both unmanaged and managed</param>
        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                this.Disconnect();
            }
        }

        protected void SafeDispose(IDisposable disposableObject)
        {
            if (disposableObject != null)
            {
                disposableObject.Dispose();
            }
        }

        /// <summary>
        /// Disconnects the remoting channel(s) of this object and all nested objects.
        /// </summary>
        private void Disconnect()
        {
            RemotingServices.Disconnect(this);
        }
    }
    //// End class
}
//// End namespace