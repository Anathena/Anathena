using Microsoft.Diagnostics.Runtime;
using System;
using System.Diagnostics;

namespace AnathenaProxy
{
    public class ClrService : MarshalByRefObject, IClrServiceInterface
    {
        private const Int32 AttachTimeout = 5000;

        public ClrService() { }

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