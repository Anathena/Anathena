using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using Binarysharp.MemoryManagement.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    /*
    The algorithm:
    0) Potential pre-processing -- no idea how many valid pointers exist in a process, but we may be able to:
        - Store all pointers to use
        - Store all regions that contain a pointer

    1) Start with a base address. Convert this to a range that spans 1024 in each direction, add this to the target list
    REPEAT FOR N LEVELS:
    2) Search for all pointer values that fall in in the target list
    3) Convert these pointers to spanning regions, and add them to the target list, clearing the old list

    5) Retrace pointers. We lose the offsets with this algorithm and need to recover them, but it should be fast, right?
        
    */
    class PointerScanner : IPointerScannerModel
    {
        private MemorySharp MemoryEditor;

        public event PointerScannerEventHandler EventUpdateProcessTitle;

        private List<RemoteModule> Modules;
        private List<RemoteRegion> AcceptedBases;

        private List<RemoteRegion> TargetRegions;
        private Dictionary<Int32, List<SnapshotElement>> Pointers;

        public PointerScanner()
        {
            Modules = new List<RemoteModule>();
            AcceptedBases = new List<RemoteRegion>();
            InitializeProcessObserver();
        }
        
        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        private void CollectModules()
        {
            Modules = MemoryEditor.Modules.RemoteModules.ToList();
        }

        private void SetAcceptedBases()
        {
            if (MemoryEditor == null)
                return;

            // Gather regions from every module as valid base addresses
            Modules.ForEach(x => AcceptedBases.Add(new RemoteRegion(MemoryEditor, x.BaseAddress, x.Size)));
        }

        public void BeginPointerScanner()
        {
            RemoteModule k = MemoryEditor.Modules.MainModule;
        }

    } // End class

} // End namespace