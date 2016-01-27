using System;
using System.Linq;
using Binarysharp.Assemblers.Fasm;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using FASMSharedInterface;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace FASMHelper
{
    class Program
    {
        private static EventWaitHandle ProcessStartingEvent;
        private static SynchronizationContext MainThreadMessageQueue = null;
        private static Stream StdInput;

        static void Main(String[] args)
        {
            InitializeAutoExit();

            // Create an event to have the client wait until we are finished starting the FASM service
            ProcessStartingEvent = new EventWaitHandle(false, EventResetMode.ManualReset, @"Global\FASMServerStarted");

            // Start the FASM service
            IpcChannel IpcChannel = new IpcChannel("FASMChannel");
            ChannelServices.RegisterChannel(IpcChannel, true);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(FASMAssembler), "FASMObj", WellKnownObjectMode.SingleCall);
            Console.WriteLine("Anathema FASM helper service to assemble x86/x64 instructions.");

            // Indicate that the FASM console is ready to receive commands
            ProcessStartingEvent.Set();

            // Keep console open
            Console.ReadLine();
        }

        public class FASMAssembler : MarshalByRefObject, ISharedAssemblyInterface
        {
            public FASMAssembler() { }

            public Byte[] Assemble(Boolean IsProcess32Bit, String Assembly, Int64 BaseAddress)
            {
                if (Assembly == null)
                    return null;

                // Add header information about process
                Assembly = String.Format((IsProcess32Bit ? "use32\n" : "use64\n") + "org 0x{0:X8}\n", BaseAddress) + Assembly;

                // Print fully assembly to console
                Console.WriteLine("\n" + Assembly + "\n");

                Byte[] Result;
                try
                {
                    // Call C++ FASM wrapper which will call the 32-bit FASM library which can assemble all x86/x64 instructions
                    Result = FasmNet.Assemble(Assembly);

                    // Print bytes to console
                    Result.ToList().ForEach(x => Console.Write(x.ToString() + " "));
                }
                catch
                {
                    Result = null;
                }
                return Result;
            }
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