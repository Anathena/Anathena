using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using Binarysharp.MemoryManagement.Modules;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    /*
    Trace-Retrace Algorithm:
    0) Potential pre-processing -- no idea how many valid pointers exist in a process, but we may be able to:
        - Store all pointers to use
        - Store all regions that contain a pointer

    1) Start with a base address. Convert this to a range that spans 1024 in each direction, add this to the target list
    2) REPEAT FOR N LEVELS:
        - Search for all pointer values that fall in in the target list
        - Convert these pointers to spanning regions, and add them to the target list, clearing the old list

    3) Retrace pointers. We will not trace pointers with invalid bases. Loop from last level to first level:
        - Compare pointer to all pointers in the previous level. Store offsets from current level to all pointers in previous level.
    */

    class PointerScanner : IPointerScannerModel
    {
        private MemorySharp MemoryEditor;

        public event PointerScannerEventHandler EventUpdateProcessTitle;

        private Int32 MaxPointerLevel;
        private List<RemoteModule> Modules;
        private List<RemoteRegion> AcceptedBases;
        
        private ConcurrentDictionary<UInt64, UInt64> PointerPool;
        private List<RemoteRegion> TargetRegions;
        // private Dictionary<Int32, List<SnapshotElement>> Pointers;

        public PointerScanner()
        {
            PointerPool = new ConcurrentDictionary<UInt64, UInt64>();
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

        public void SetMaxPointerLevel(Int32 MaxPointerLevel)
        {
            this.MaxPointerLevel = MaxPointerLevel;
        }

        public void BeginPointerScanner()
        {
            Initialize();
            Trace();
            Retrace();
        }

        private void Trace()
        {

        }

        private void Retrace()
        {

        }

        private void Initialize()
        {
            BuildPointerPool();
            CollectModules();
            SetAcceptedBases();
        }
        
        private void BuildPointerPool()
        {
            PointerPool.Clear();
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

    } // End class

} // End namespace