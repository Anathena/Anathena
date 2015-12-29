using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema
{
    /// <summary>
    /// Defines the data contained in a single snapshot
    /// </summary>
    class Snapshot : IProcessObserver
    {
        private MemorySharp MemoryEditor;

        private DateTime TimeStamp;

        private List<RemoteRegion> MemoryRegions;
        private List<Int32> LabelMapping;
        private List<Byte[]> MemoryValues;

        public Snapshot()
        {
            InitializeObserver();
            MemoryRegions = new List<RemoteRegion>();
        }

        public void InitializeObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
        }

        public Snapshot(List<RemoteRegion> MemoryRegions)
        {
            this.MemoryRegions = MemoryRegions;

            MergeRegions();
        }

        public void SetTimeStampToNow()
        {
            TimeStamp = DateTime.Now;
        }

        public DateTime GetTimeStamp()
        {
            return TimeStamp;
        }

        public void ReadAllMemory()
        {
            MemoryValues = new List<Byte[]>(MemoryRegions.Count);
            Boolean InvalidRead = false;

            Parallel.For(0, MemoryValues.Count, Index =>
            {
                Boolean SuccessReading = false;
                MemoryValues[Index] = MemoryEditor.ReadBytes(MemoryRegions[Index].BaseAddress, MemoryRegions[Index].RegionSize, out SuccessReading, false);

                if (!SuccessReading)
                    InvalidRead = true;
            });

            // Deallocated page, we need to mask the current virtual pages with this snapshot
            if (InvalidRead)
            {
                // MaskRegions(SnapshotManager.GetSnapshotManagerInstance().SnapshotAllMemory());
            }

            // Things that call this may expect a 1 to 1 with the previous ReadAllMemory
        }

        public List<Byte[]> GetReadMemory()
        {
            return MemoryValues;
        }

        public List<RemoteRegion> GetMemoryRegions()
        {
            return MemoryRegions;
        }

        public List<Int32> GetLabelMapping()
        {
            return LabelMapping;
        }

        public UInt64 GetSize()
        {
            UInt64 Size = 0;

            if (MemoryRegions != null)
                for (Int32 Index = 0; Index < MemoryRegions.Count; Index++)
                    Size += (UInt64)MemoryRegions[Index].RegionSize;

            return Size;
        }

        /// <summary>
        /// Expands all memory regions in both directions by the specified amount. Useful for filtering methods that isolate
        /// changing bytes (ie 1 byte of an 8 byte integer), where we would want to grow to recover the other 7 bytes.
        /// </summary>
        /// <param name="GrowAmount"></param>
        public void GrowRegions(Int32 VariableSize)
        {
            Int32 GrowSize = VariableSize - 1;

            // MergeRegions();
        }

        /// <summary>
        /// Masks the current memory regions against another memory region, keeping the common elements of the two.
        /// </summary>
        /// <param name="Mask"></param>
        public void MaskRegions(Snapshot Mask)
        {

            // MergeRegions(); // Just for the sort
        }

        private void MapIndicies()
        {
            LabelMapping = new List<Int32>();

            Int32 Mapping = 0;
            for (Int32 PageIndex = 0; PageIndex < MemoryRegions.Count; PageIndex++)
            {
                LabelMapping.Add(Mapping);

                Mapping += MemoryRegions[PageIndex].RegionSize;
            }
        }

        /// <summary>
        /// Merges continguous regions in the current list of memory regions using a fast stack based algorithm O(nlogn + n)
        /// </summary>
        private void MergeRegions()
        {
            if (MemoryRegions.Count == 0)
                return;

            // First, sort by start address
            MemoryRegions.OrderBy(x => x.BaseAddress);

            // Create and initialize the stack with the first region
            Stack<RemoteRegion> CombinedRegions = new Stack<RemoteRegion>();
            CombinedRegions.Push(MemoryRegions[0]);

            // Build the remaining regions
            for (Int32 Index = CombinedRegions.Count; Index < MemoryRegions.Count; Index++)
            {
                RemoteRegion Top = CombinedRegions.Peek();

                // If the interval does not overlap, put it on the top of the stack
                if ((UInt64)Top.EndAddress < (UInt64)MemoryRegions[Index].BaseAddress - 1)
                {
                    CombinedRegions.Push(MemoryRegions[Index]);
                }
                // The interval overlaps; just merge it with the current top of the stack
                else if ((UInt64)Top.EndAddress <= (UInt64)MemoryRegions[Index].EndAddress)
                {
                    Top.RegionSize = (Int32)((UInt64)MemoryRegions[Index].EndAddress - (UInt64)Top.BaseAddress);
                    CombinedRegions.Pop();
                    CombinedRegions.Push(Top);
                }
            }

            // Replace memory regions with merged memory regions
            MemoryRegions = CombinedRegions.ToList();
            MemoryRegions.Reverse();
            MapIndicies();
        }
    }

    class Snapshot<T> : Snapshot
    {
        private List<T> MemoryLabels;

        public Snapshot() : base()
        {
            MemoryLabels = new List<T>();
        }

        public Snapshot(List<RemoteRegion> MemoryRegions) : base(MemoryRegions)
        {

        }

        public Snapshot(List<RemoteRegion> MemoryRegions, List<T> MemoryLabels) : base(MemoryRegions)
        {
            this.MemoryLabels = MemoryLabels;
        }

        public Boolean HasLabels()
        {
            if (MemoryLabels == null)
                return false;
            return true;
        }

        public void AssignLabels(List<T> MemoryLabels)
        {
            this.MemoryLabels = MemoryLabels;
        }

        public List<T> GetMemoryLabels()
        {
            return MemoryLabels;
        }
    }
}
