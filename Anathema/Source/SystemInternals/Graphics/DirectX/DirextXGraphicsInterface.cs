using System;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Interface
{
    [Serializable]
    public class DirextXGraphicsInterface : MarshalByRefObject, IGraphicsInterface
    {
        /// <summary>
        /// The client process Id
        /// </summary>
        public Int32 ProcessId { get; set; }

        public String ProjectDirectory { get; set; }

        public DirextXGraphicsInterface() { }

        #region Client-side Events

        /// <summary>
        /// Client event used to notify the hook to exit
        /// </summary>
        public event DisconnectedEvent Disconnected;

        #endregion

        #region Private: Invoke message handlers

        private void SafeInvokeDisconnected()
        {
            if (Disconnected == null)
                return;

            DisconnectedEvent Listener = null;

            foreach (Delegate Delegate in Disconnected.GetInvocationList())
            {
                try
                {
                    Listener = (DisconnectedEvent)Delegate;
                    Listener.Invoke();
                }
                catch
                {
                    Disconnected -= Listener;
                }
            }
        }
        
        #endregion

        /// <summary>
        /// Empty method to ensure we can call the client without crashing
        /// </summary>
        public void Ping() { }

        Guid IGraphicsInterface.CreateText(String Text, Int32 LocationX, Int32 LocationY)
        {
            return new Guid();
        }

        Guid IGraphicsInterface.CreateImage(String Path, Int32 LocationX, Int32 LocationY)
        {
            return new Guid();
        }

        public void ShowObject(Guid Guid)
        {
            throw new NotImplementedException();
        }

        public void HideObject(Guid Guid)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            SafeInvokeDisconnected();
        }

    } // End class

    [Serializable]
    public delegate void DisconnectedEvent();

    /// <summary>
    /// Client event proxy for marshalling event handlers
    /// </summary>
    public class ClientCaptureInterfaceEventProxy : MarshalByRefObject
    {
        /// <summary>
        /// Client event used to notify the hook to exit
        /// </summary>
        public event DisconnectedEvent Disconnected;

        public override Object InitializeLifetimeService()
        {
            //Returning null holds the object alive
            //until it is explicitly destroyed
            return null;
        }

        public void DisconnectedProxyHandler()
        {
            Disconnected?.Invoke();
        }

    } // End class

} // End namespace