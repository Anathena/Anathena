using Anathema.Source.SystemInternals.Graphics.DirectX.Interface;
using System;

namespace Anathema.Source.SystemInternals.Hook
{

    [Serializable]
    public delegate void DisconnectedEvent();

    [Serializable]
    public class HookCommunication : MarshalByRefObject
    {
        /// <summary>
        /// The client process Id
        /// </summary>
        private Int32 ProcessId { get; set; }

        private String ProjectDirectory { get; set; }

        public DirextXGraphicsInterface GraphicsInterface { get; set; }

        public HookCommunication(Int32 ProcessId, String ProjectDirectory)
        {
            GraphicsInterface = new DirextXGraphicsInterface();
            GraphicsInterface.ProcessId = ProcessId;
            GraphicsInterface.ProjectDirectory = ProjectDirectory;
        }

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

        public void Disconnect()
        {
            SafeInvokeDisconnected();
        }

    } // End class

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
            // Returning null holds the object alive until it is explicitly destroyed
            return null;
        }

        public void DisconnectedProxyHandler()
        {
            Disconnected?.Invoke();
        }

    } // End class

} // End namespace