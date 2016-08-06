using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using System.Threading.Tasks;

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
        private const Int32 ParentCheckDelayMs = 500;

        public AnathenaProxy(Int32 ParentProcessId, String PipeName, String WaitEventName)
        {
            // Create an event to have the client wait until we are finished starting the service
            EventWaitHandle ProcessStartingEvent = new EventWaitHandle(false, EventResetMode.ManualReset, WaitEventName);

            InitializeAutoExit(ParentProcessId);

            ServiceHost ServiceHost = new ServiceHost(typeof(ProxyService));
            ServiceHost.Description.Behaviors.Remove(typeof(ServiceDebugBehavior));
            ServiceHost.Description.Behaviors.Add(new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true });
            NetNamedPipeBinding Binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            ServiceHost.AddServiceEndpoint(typeof(IProxyService), Binding, PipeName);
            ServiceHost.Open();

            ProcessStartingEvent.Set();

            Console.WriteLine("Anathena proxy library loaded");
            Console.ReadLine();
        }

        public static Boolean IsRunning(Int32 ParentProcessId)
        {
            try
            {
                Process.GetProcessById(ParentProcessId);
            }
            catch (ArgumentException)
            {
                return false;
            }

            return true;
        }

        private void InitializeAutoExit(Int32 ParentProcessId)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    if (!IsRunning(ParentProcessId))
                        break;

                    Thread.Sleep(ParentCheckDelayMs);
                }

                Environment.Exit(0);
            });
        }

    } // End class

} // End namespace