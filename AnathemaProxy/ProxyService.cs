using Anathema.Assemblers.Fasm;
using Microsoft.Diagnostics.Runtime;
using System;
using System.Diagnostics;
using System.ServiceModel;

namespace AnathenaProxy
{
    /// <summary>
    /// Proxy service to be contained by a 32 and 64 bit service, with services exposed via IPC. Useful for certain things that
    /// Anathena requires, such as:
    /// - FASM Compiler, which can only be run in 32 bit mode
    /// - Microsoft.Diagnostics.Runtime, which can only be used on processes of the same bitness
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ProxyService : IProxyService
    {
        private const Int32 AttachTimeout = 5000;

        public ProxyService() { }

        public Byte[] Assemble(Boolean IsProcess32Bit, String Assembly, UInt64 BaseAddress)
        {
            if (Assembly == null)
                return null;

            // Add header information about process
            if (IsProcess32Bit)
                Assembly = String.Format("use32\n" + "org 0x{0:X8}\n", BaseAddress) + Assembly;
            else
                Assembly = String.Format("use64\n" + "org 0x{0:X16}\n", BaseAddress) + Assembly;

            // Print fully assembly to console
            Console.WriteLine("\n" + Assembly + "\n");

            Byte[] Result;
            try
            {
                // Call C++ FASM wrapper which will call the 32-bit FASM library which can assemble all x86/x64 instructions
                Result = FasmNet.Assemble(Assembly);

                // Print bytes to console
                Array.ForEach(Result, (X => Console.Write(X.ToString() + " ")));
            }
            catch
            {
                Result = null;
            }
            return Result;
        }

        public ClrHeap GetProcessClrHeap(Process TargetProcess)
        {
            ClrHeap Heap = null;

            try
            {
                if (TargetProcess == null)
                    return null;

                DataTarget DataTarget = DataTarget.AttachToProcess(TargetProcess.Id, AttachTimeout, AttachFlag.Passive);

                if (DataTarget.ClrVersions.Count <= 0)
                    return null;

                ClrInfo Version = DataTarget.ClrVersions[0];
                ClrRuntime Runtime = Version.CreateRuntime();
                Heap = Runtime.GetHeap();
            }
            catch { }

            return Heap;
        }

    } // End class

} // End namespace