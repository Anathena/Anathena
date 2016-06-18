using System;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Interface
{
    [Serializable]
    public delegate void DisconnectedEvent();
    [Serializable]
    public delegate void DisplayTextEvent(DisplayTextEventArgs Args);

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

        /// <summary>
        /// Client event used to display a piece of text in-game
        /// </summary>
        public event DisplayTextEvent DisplayText;

        #endregion

        /// <summary>
        /// Display text in-game for the default duration of 5 seconds
        /// </summary>
        /// <param name="Text"></param>
        public void DisplayInGameText(String Text)
        {
            DisplayInGameText(Text, new TimeSpan(0, 0, 5));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="duration"></param>
        public void DisplayInGameText(String Text, TimeSpan Duration)
        {
            if (Duration.TotalMilliseconds <= 0)
                throw new ArgumentException("Duration must be larger than 0", "duration");

            SafeInvokeDisplayText(new DisplayTextEventArgs(Text, Duration));
        }

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

        private void SafeInvokeDisplayText(DisplayTextEventArgs DisplayTextEventArgs)
        {
            if (DisplayText == null)
                return;

            DisplayTextEvent Listener = null;

            foreach (Delegate Delegate in DisplayText.GetInvocationList())
            {
                try
                {
                    Listener = (DisplayTextEvent)Delegate;
                    Listener.Invoke(DisplayTextEventArgs);
                }
                catch
                {
                    DisplayText -= Listener;
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


    /// <summary>
    /// Client event proxy for marshalling event handlers
    /// </summary>
    public class ClientCaptureInterfaceEventProxy : MarshalByRefObject
    {
        /// <summary>
        /// Client event used to notify the hook to exit
        /// </summary>
        public event DisconnectedEvent Disconnected;

        /// <summary>
        /// Client event used to display in-game text
        /// </summary>
        public event DisplayTextEvent DisplayText;

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

        public void DisplayTextProxyHandler(DisplayTextEventArgs Args)
        {
            DisplayText?.Invoke(Args);
        }

    } // End class

} // End namespace