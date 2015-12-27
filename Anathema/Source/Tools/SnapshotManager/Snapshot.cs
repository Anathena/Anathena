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
    class Snapshot
    {
        private DateTime TimeStamp;
        private List<RemoteRegion> MemoryRegions;
        private List<Object> MemoryLabels;

        public Snapshot()
        {
            MemoryRegions = new List<RemoteRegion>();
            MemoryLabels = new List<Object>();
        }

        public Snapshot(List<RemoteRegion> MemoryRegions)
        {
            this.MemoryRegions = MemoryRegions;
            MemoryLabels = new List<Object>();

            MergeRegions();
        }

        public Snapshot(List<RemoteRegion> MemoryRegions, List<Object> MemoryLabels)
        {
            this.MemoryRegions = MemoryRegions;
            this.MemoryLabels = MemoryLabels;

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
        
        public void AssignLabels(List<Object> MemoryLabels)
        {
            this.MemoryLabels = MemoryLabels;
        }

        public List<RemoteRegion> GetMemoryRegions()
        {
            return MemoryRegions;
        }

        public List<Object> GetMemoryLabels()
        {
            return MemoryLabels;
        }

        /// <summary>
        /// Expands all memory regions in both directions by the specified amount. Useful for filtering methods that isolate
        /// changing bytes (ie 1 byte of an 8 byte integer), where we would want to grow to recover the other 7 bytes.
        /// </summary>
        /// <param name="GrowAmount"></param>
        public void GrowRegions(Int32 VariableSize)
        {
            Int32 GrowSize = VariableSize - 1;

        }

        /// <summary>
        /// Masks the current memory regions against another memory region, keeping the common elements of the two.
        /// </summary>
        /// <param name="MaskingRegions"></param>
        public void MaskRegions(List<RemoteRegion> MaskingRegions)
        {

        }
        
        /// <summary>
        /// Merges continguous regions in the current list of memory regions using a fast stack based algorithm O(nlogn) + O(n)
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
                if ((UInt64)Top.EndAddress + 1 < (UInt64)MemoryRegions[Index].EndAddress)
                {
                    CombinedRegions.Push(MemoryRegions[Index]);
                }
                // The interval overlaps; just merge it with the current top of the stack
                else if ((UInt64)(Top.EndAddress) < (UInt64)MemoryRegions[Index].EndAddress)
                {
                    Top.RegionSize = (Int32)((UInt64)MemoryRegions[Index].EndAddress - (UInt64)Top.BaseAddress);
                    CombinedRegions.Pop();
                    CombinedRegions.Push(Top);
                }
            }

            // Replace memory regions with merged memory regions
            CombinedRegions.OrderBy(x => x.BaseAddress);
            MemoryRegions = CombinedRegions.ToList();
        }
    }
}
