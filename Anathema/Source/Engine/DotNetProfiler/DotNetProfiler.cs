using Anathema.Source.Engine.Processes;
using Microsoft.Diagnostics.Runtime;
using System.Diagnostics;

namespace Anathema.Source.Engine.DotNetProfiler
{
    class DotNetProfiler : IProcessObserver
    {
        private EngineCore EngineCore;

        public DotNetProfiler()
        {
            InitializeProcessObserver();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;

        }

        private static ClrRuntime CreateRuntime(Process Target)
        {
            // Create the data target.  This tells us the versions of CLR loaded in the target process.
            //DataTarget dataTarget = DataTarget.LoadCrashDump();

            // Note I just take the first version of CLR in the process.  You can loop over every loaded
            // CLR to handle the SxS case where both v2 and v4 are loaded in the process.
            //ClrInfo version = dataTarget.ClrVersions[0];

            // Now that we have the DataTarget, the version of CLR, and the right dac, we create and return a
            // ClrRuntime instance.
            //  return version.CreateRuntime(dac);
            return null;
        }

        private void CollectModules()
        {
            ClrRuntime Runtime = CreateRuntime(EngineCore.Memory.GetProcess());
            ClrHeap Heap = Runtime.GetHeap();

            foreach (ClrSegment Segment in Heap.Segments)
            {

            }
        }

    } // End class

} // End namespace