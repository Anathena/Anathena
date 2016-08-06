using System;
using System.IO;
using System.ServiceModel;
using System.Threading;

namespace AnathenaProxy
{
    /// <summary>
    /// Proxy service to be contained by a 32 and 64 bit service, with services exposed via IPC. Useful for certain things that
    /// Anathena requires, such as:
    /// - FASM Compiler, which can only be run in 32 bit mode
    /// - Microsoft.Diagnostics.Runtime, which can only be used on processes of the same bitness
    /// </summary>
    public class AnathenaProxy
    {
        private static EventWaitHandle ProcessStartingEvent;
        private static SynchronizationContext MainThreadMessageQueue;
        private static Stream StdInput;

        public AnathenaProxy(String PipeName, String WaitEventName)
        {
            // Create an event to have the client wait until we are finished starting the service
            ProcessStartingEvent = new EventWaitHandle(false, EventResetMode.ManualReset, WaitEventName);

            InitializeAutoExit();

            ServiceHost ServiceHost = new ServiceHost(typeof(ProxyService));
            NetNamedPipeBinding Binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            ServiceHost.AddServiceEndpoint(typeof(IProxyService), Binding, PipeName);
            ServiceHost.Open();

            ProcessStartingEvent.Set();

            Console.WriteLine("Anathena proxy library loaded");
            Console.ReadLine();
        }

        #region Automatic exiting logic for when parent process dies

        private static void InitializeAutoExit()
        {
            StdInput = Console.OpenStandardInput();

            // Feel free to use a better way to post to the message loop from here if you know one ;)    
            System.Windows.Forms.Timer HandoffToMessageLoopTimer = new System.Windows.Forms.Timer();
            HandoffToMessageLoopTimer.Interval = 100;
            HandoffToMessageLoopTimer.Tick += new EventHandler((Sender, Args) => { PostMessageLoopInitialization(HandoffToMessageLoopTimer); });
            HandoffToMessageLoopTimer.Start();
        }

        private static void PostMessageLoopInitialization(System.Windows.Forms.Timer Timer)
        {
            if (MainThreadMessageQueue == null)
            {
                Timer.Stop();
                MainThreadMessageQueue = SynchronizationContext.Current;
            }

            // constantly monitor standard input on a background thread that will signal the main thread when stuff happens.
            BeginMonitoringStdIn(null);
        }

        private static void BeginMonitoringStdIn(Object State)
        {
            if (SynchronizationContext.Current == MainThreadMessageQueue)
            {
                // we're already running on the main thread - proceed.
                Byte[] buffer = new Byte[128];

                StdInput.BeginRead(buffer, 0, buffer.Length, (AsyncResult) =>
                {
                    if (StdInput.EndRead(AsyncResult) == 0)
                    {
                        MainThreadMessageQueue.Post(new SendOrPostCallback(ApplicationTeardown), null);
                    }
                    else
                    {
                        BeginMonitoringStdIn(null);
                    }
                }, null);
            }
            else
            {
                // Not invoked from the main thread, dispatch another call to this method on the main thread and return
                MainThreadMessageQueue.Post(new SendOrPostCallback(BeginMonitoringStdIn), null);
            }
        }

        private static void ApplicationTeardown(Object State)
        {
            // Tear down your application gracefully here
            StdInput.Close();
        }

        #endregion

    } // End class

} // End namespace