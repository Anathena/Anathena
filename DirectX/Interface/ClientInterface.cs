using System;
using System.Drawing;
using System.Threading;

namespace DirectXShell.Interface
{
    [Serializable]
    public delegate void RecordingStartedEvent(CaptureConfig Config);
    [Serializable]
    public delegate void RecordingStoppedEvent();
    [Serializable]
    public delegate void MessageReceivedEvent(MessageReceivedEventArgs Message);
    [Serializable]
    public delegate void ScreenshotReceivedEvent(ScreenshotReceivedEventArgs Response);
    [Serializable]
    public delegate void DisconnectedEvent();
    [Serializable]
    public delegate void ScreenshotRequestedEvent(ScreenshotRequest Request);
    [Serializable]
    public delegate void DisplayTextEvent(DisplayTextEventArgs Args);

    [Serializable]
    public class ClientInterface : MarshalByRefObject
    {
        /// <summary>
        /// The client process Id
        /// </summary>
        public int ProcessId { get; set; }
        public bool IsRecording { get; set; }

        private Object AccessLock;
        private Guid? RequestId;
        private Action<Screenshot> CompleteScreenshot;
        private ManualResetEvent Wait;

        public ClientInterface()
        {
            AccessLock = new Object();
            RequestId = null;
            CompleteScreenshot = null;
            Wait = new ManualResetEvent(false);
        }

        #region Server-side Events

        /// <summary>
        /// Server event for sending debug and error information from the client to server
        /// </summary>
        public event MessageReceivedEvent RemoteMessage;

        /// <summary>
        /// Server event for receiving screenshot image data
        /// </summary>
        public event ScreenshotReceivedEvent ScreenshotReceived;

        #endregion

        #region Client-side Events

        /// <summary>
        /// Client event used to communicate to the client that it is time to start recording
        /// </summary>
        public event RecordingStartedEvent RecordingStarted;

        /// <summary>
        /// Client event used to communicate to the client that it is time to stop recording
        /// </summary>
        public event RecordingStoppedEvent RecordingStopped;

        /// <summary>
        /// Client event used to communicate to the client that it is time to create a screenshot
        /// </summary>
        public event ScreenshotRequestedEvent ScreenshotRequested;

        /// <summary>
        /// Client event used to notify the hook to exit
        /// </summary>
        public event DisconnectedEvent Disconnected;

        /// <summary>
        /// Client event used to display a piece of text in-game
        /// </summary>
        public event DisplayTextEvent DisplayText;

        #endregion

        #region Video Capture

        /// <summary>
        /// If not <see cref="IsRecording"/> will invoke the <see cref="RecordingStarted"/> event, starting a new recording. 
        /// </summary>
        /// <param name="Config">The configuration for the recording</param>
        /// <remarks>Handlers in the server and remote process will be be invoked.</remarks>
        public void StartRecording(CaptureConfig Config)
        {
            if (IsRecording)
                return;

            SafeInvokeRecordingStarted(Config);
            IsRecording = true;
        }

        /// <summary>
        /// If <see cref="IsRecording"/>, will invoke the <see cref="RecordingStopped"/> event, finalising any existing recording.
        /// </summary>
        /// <remarks>Handlers in the server and remote process will be be invoked.</remarks>
        public void StopRecording()
        {
            if (!IsRecording)
                return;

            SafeInvokeRecordingStopped();
            IsRecording = false;
        }

        #endregion

        #region Still image Capture


        /// <summary>
        /// Get a fullscreen screenshot with the default timeout of 2 seconds
        /// </summary>
        public Screenshot GetScreenshot()
        {
            return GetScreenshot(Rectangle.Empty, new TimeSpan(0, 0, 2), null, ImageFormatEnum.Bitmap);
        }

        /// <summary>
        /// Get a screenshot of the specified region
        /// </summary>
        /// <param name="Region">the region to capture (x=0,y=0 is top left corner)</param>
        /// <param name="Timeout">maximum time to wait for the screenshot</param>
        public Screenshot GetScreenshot(Rectangle Region, TimeSpan Timeout, Size? Resize, ImageFormatEnum Format)
        {
            lock (AccessLock)
            {
                Screenshot Result = null;
                RequestId = Guid.NewGuid();
                Wait.Reset();

                SafeInvokeScreenshotRequested(new ScreenshotRequest(RequestId.Value, Region)
                {
                    Format = Format,
                    Resize = Resize,
                });

                CompleteScreenshot = (sc) =>
                {
                    try
                    {
                        Interlocked.Exchange(ref Result, sc);
                    }
                    catch
                    {
                    }
                    Wait.Set();

                };

                Wait.WaitOne(Timeout);
                CompleteScreenshot = null;
                return Result;
            }
        }

        public IAsyncResult BeginGetScreenshot(Rectangle region, TimeSpan timeout, AsyncCallback callback = null, Size? resize = null, ImageFormatEnum format = ImageFormatEnum.Bitmap)
        {
            Func<Rectangle, TimeSpan, Size?, ImageFormatEnum, Screenshot> getScreenshot = GetScreenshot;

            return getScreenshot.BeginInvoke(region, timeout, resize, format, callback, getScreenshot);
        }

        public Screenshot EndGetScreenshot(IAsyncResult result)
        {
            Func<Rectangle, TimeSpan, Size?, ImageFormatEnum, Screenshot> getScreenshot = result.AsyncState as Func<Rectangle, TimeSpan, Size?, ImageFormatEnum, Screenshot>;
            if (getScreenshot != null)
            {
                return getScreenshot.EndInvoke(result);
            }
            else
                return null;
        }

        public void SendScreenshotResponse(Screenshot screenshot)
        {
            if (RequestId != null && screenshot != null && screenshot.RequestId == RequestId.Value)
            {
                if (CompleteScreenshot != null)
                {
                    CompleteScreenshot(screenshot);
                }
            }
        }

        #endregion

        /// <summary>
        /// Tell the client process to disconnect
        /// </summary>
        public void Disconnect()
        {
            SafeInvokeDisconnected();
        }

        /// <summary>
        /// Send a message to all handlers of <see cref="ClientInterface.RemoteMessage"/>.
        /// </summary>
        /// <param name="MessageType"></param>
        /// <param name="Format"></param>
        /// <param name="Args"></param>
        public void Message(MessageType MessageType, String Format, params Object[] Args)
        {
            Message(MessageType, String.Format(Format, Args));
        }

        public void Message(MessageType MessageType, String Message)
        {
            SafeInvokeMessageRecevied(new MessageReceivedEventArgs(MessageType, Message));
        }

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

        private void SafeInvokeRecordingStarted(CaptureConfig Config)
        {
            if (RecordingStarted == null)
                return;

            RecordingStartedEvent Listener = null;

            foreach (Delegate Delegate in RecordingStarted.GetInvocationList())
            {
                try
                {
                    Listener = (RecordingStartedEvent)Delegate;
                    Listener.Invoke(Config);
                }
                catch
                {
                    RecordingStarted -= Listener;
                }
            }
        }

        private void SafeInvokeRecordingStopped()
        {
            if (RecordingStopped == null)
                return;

            RecordingStoppedEvent Listener = null;

            foreach (Delegate Delegate in RecordingStopped.GetInvocationList())
            {
                try
                {
                    Listener = (RecordingStoppedEvent)Delegate;
                    Listener.Invoke();
                }
                catch
                {
                    RecordingStopped -= Listener;
                }
            }
        }

        private void SafeInvokeMessageRecevied(MessageReceivedEventArgs EventArgs)
        {
            if (RemoteMessage == null)
                return;

            MessageReceivedEvent Listener = null;

            foreach (Delegate Delegate in RemoteMessage.GetInvocationList())
            {
                try
                {
                    Listener = (MessageReceivedEvent)Delegate;
                    Listener.Invoke(EventArgs);
                }
                catch
                {
                    //Could not reach the destination, so remove it
                    //from the list
                    RemoteMessage -= Listener;
                }
            }
        }

        private void SafeInvokeScreenshotRequested(ScreenshotRequest EventArgs)
        {
            if (ScreenshotRequested == null)
                return;

            ScreenshotRequestedEvent Listener = null;

            foreach (Delegate Delegate in ScreenshotRequested.GetInvocationList())
            {
                try
                {
                    Listener = (ScreenshotRequestedEvent)Delegate;
                    Listener.Invoke(EventArgs);
                }
                catch
                {
                    ScreenshotRequested -= Listener;
                }
            }
        }

        private void SafeInvokeScreenshotReceived(ScreenshotReceivedEventArgs EventArgs)
        {
            if (ScreenshotReceived == null)
                return;

            ScreenshotReceivedEvent Listener = null;

            foreach (Delegate Delegate in ScreenshotReceived.GetInvocationList())
            {
                try
                {
                    Listener = (ScreenshotReceivedEvent)Delegate;
                    Listener.Invoke(EventArgs);
                }
                catch
                {
                    ScreenshotReceived -= Listener;
                }
            }
        }

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

    } // End class


    /// <summary>
    /// Client event proxy for marshalling event handlers
    /// </summary>
    public class ClientCaptureInterfaceEventProxy : MarshalByRefObject
    {
        /// <summary>
        /// Client event used to communicate to the client that it is time to start recording
        /// </summary>
        public event RecordingStartedEvent RecordingStarted;

        /// <summary>
        /// Client event used to communicate to the client that it is time to stop recording
        /// </summary>
        public event RecordingStoppedEvent RecordingStopped;

        /// <summary>
        /// Client event used to communicate to the client that it is time to create a screenshot
        /// </summary>
        public event ScreenshotRequestedEvent ScreenshotRequested;

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

        public void RecordingStartedProxyHandler(CaptureConfig Config)
        {
            RecordingStarted?.Invoke(Config);
        }

        public void RecordingStoppedProxyHandler()
        {
            RecordingStopped?.Invoke();
        }


        public void DisconnectedProxyHandler()
        {
            Disconnected?.Invoke();
        }

        public void ScreenshotRequestedProxyHandler(ScreenshotRequest Request)
        {
            ScreenshotRequested?.Invoke(Request);
        }

        public void DisplayTextProxyHandler(DisplayTextEventArgs Args)
        {
            DisplayText?.Invoke(Args);
        }

    } // End class

} // End namespace