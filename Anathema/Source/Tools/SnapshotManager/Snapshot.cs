using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Anathema
{
    public class SnapshotElement
    {
        public Byte CurrentValue;
        public Byte PreviousValue;

        protected SnapshotElement() { }
        public SnapshotElement(Byte CurrentValue, Byte PreviousValue)
        {
            this.CurrentValue = CurrentValue;
            this.PreviousValue = PreviousValue;
        }
    }

    public class SnapshotRegion : RemoteRegion
    {
        public Byte[] CurrentValues;
        public Byte[] PreviousValues;

        public SnapshotRegion(IntPtr BaseAddress, Int32 RegionSize) : base(null, BaseAddress, RegionSize) { }
        public SnapshotRegion(RemoteRegion RemoteRegion) : base(null, RemoteRegion.BaseAddress, RemoteRegion.RegionSize) { }
        public SnapshotElement this[Int32 Index]
        {
            get { return new SnapshotElement(CurrentValues[Index], PreviousValues[Index]); }
            set { CurrentValues[Index] = value.CurrentValue; PreviousValues[Index] = value.PreviousValue; }
        }
    }

    public class SnapshotElement<T> : SnapshotElement
    {
        public T Label;

        public SnapshotElement(Byte CurrentValue, Byte PreviousValue, T Label) : base(CurrentValue, PreviousValue)
        {
            this.Label = Label;
        }
    }

    public class LabeledRegion<T> : SnapshotRegion
    {
        public T[] MemoryLabels;

        public LabeledRegion(IntPtr BaseAddress, Int32 RegionSize) : base(BaseAddress, RegionSize) { }
        public LabeledRegion(RemoteRegion RemoteRegion) : base(RemoteRegion) { }
        public LabeledRegion(SnapshotRegion SnapshotRegion) : base(SnapshotRegion)
        {
            CurrentValues = SnapshotRegion.CurrentValues == null ? null : (Byte[])SnapshotRegion.CurrentValues.Clone();
            PreviousValues = SnapshotRegion.PreviousValues == null ? null : (Byte[])SnapshotRegion.PreviousValues.Clone();
        }
        public new SnapshotElement<T> this[Int32 Index]
        {
            get { return new SnapshotElement<T>(CurrentValues[Index], PreviousValues[Index], MemoryLabels[Index]); }
            set { CurrentValues[Index] = value.CurrentValue; PreviousValues[Index] = value.PreviousValue; MemoryLabels[Index] = value.Label; }
        }
    }

    /// <summary>
    /// Defines the data contained in a single snapshot
    /// </summary>
    class Snapshot : IProcessObserver
    {
        private MemorySharp MemoryEditor;

        protected SnapshotRegion[] SnapshotData;
        private DateTime TimeStamp;

        public Snapshot()
        {
            this.SnapshotData = null;

            Initialize();
            MergeRegions();
        }

        public Snapshot(SnapshotRegion[] SnapshotData)
        {
            this.SnapshotData = SnapshotData;

            Initialize();
            MergeRegions();
        }

        public void Initialize()
        {
            InitializeObserver();
        }

        public void InitializeObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateMemoryEditor(MemorySharp MemoryEditor)
        {
            this.MemoryEditor = MemoryEditor;
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
            Boolean InvalidRead = false;

            Parallel.ForEach(SnapshotData, (Data) =>
            {
                Data.PreviousValues = Data.CurrentValues;

                Boolean SuccessReading = false;
                Data.CurrentValues = MemoryEditor.ReadBytes(Data.BaseAddress, Data.RegionSize, out SuccessReading, false);

                if (!SuccessReading)
                {
                    InvalidRead = true;
                }
            });

            // Deallocated page, we need to mask the current virtual pages with this snapshot
            if (InvalidRead)
            {
                // MaskRegions(SnapshotManager.GetSnapshotManagerInstance().SnapshotAllMemory());
            }

            // Things that call this may expect a 1 to 1 with the previous ReadAllMemory
        }

        public SnapshotRegion[] GetSnapshotData()
        {
            return SnapshotData;
        }

        public UInt64 GetSize()
        {
            return (UInt64)SnapshotData.AsEnumerable().Sum(x => (Int64)x.RegionSize);
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

        /// <summary>
        /// Merges continguous regions in the current list of memory regions using a fast stack based algorithm O(nlogn + n)
        /// </summary>
        private void MergeRegions()
        {
            if (SnapshotData == null || SnapshotData.Length == 0)
                return;

            // First, sort by start address
            Array.Sort(SnapshotData, (x, y) => ((UInt64)x.BaseAddress).CompareTo((UInt64)y.BaseAddress));

            // Create and initialize the stack with the first region
            Stack<SnapshotRegion> CombinedRegions = new Stack<SnapshotRegion>();
            CombinedRegions.Push(SnapshotData[0]);

            // Build the remaining regions
            for (Int32 Index = CombinedRegions.Count; Index < SnapshotData.Length; Index++)
            {
                SnapshotRegion Top = CombinedRegions.Peek();

                // If the interval does not overlap, put it on the top of the stack
                if ((UInt64)Top.EndAddress < (UInt64)SnapshotData[Index].BaseAddress - 1)
                {
                    CombinedRegions.Push(SnapshotData[Index]);
                }
                // The interval overlaps; just merge it with the current top of the stack
                else if ((UInt64)Top.EndAddress <= (UInt64)SnapshotData[Index].EndAddress)
                {
                    Top.RegionSize = (Int32)((UInt64)SnapshotData[Index].EndAddress - (UInt64)Top.BaseAddress);
                    CombinedRegions.Pop();
                    CombinedRegions.Push(Top);
                }
            }

            // Replace memory regions with merged memory regions
            SnapshotData = CombinedRegions.ToArray();
            Array.Sort(SnapshotData, (x, y) => ((UInt64)x.BaseAddress).CompareTo((UInt64)y.BaseAddress));
        }
    }

    class Snapshot<T> : Snapshot
    {
        public Snapshot() : base()
        {

        }

        public Snapshot(Snapshot BaseSnapshot)
        {
            // Copy and convert the snapshot data to a labeled format
            SnapshotData = new LabeledRegion<T>[BaseSnapshot.GetSnapshotData().Length];
            for (Int32 RegionIndex = 0; RegionIndex < SnapshotData.Length; RegionIndex++)
                SnapshotData[RegionIndex] = new LabeledRegion<T>(BaseSnapshot.GetSnapshotData()[RegionIndex]);

            Initialize();
        }

        public Snapshot(LabeledRegion<T>[] SnapshotData)
        {
            this.SnapshotData = SnapshotData;
            Initialize();
        }

        public new LabeledRegion<T>[] GetSnapshotData()
        {
            return (LabeledRegion<T>[])SnapshotData;
        }
    }
}
