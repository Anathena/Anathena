using Anathema.MemoryManagement;
using Anathema.MemoryManagement.Memory;
using Anathema.Utils.Extensions;
using Anathema.Utils.OS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Anathema
{
    /// <summary>
    /// Defines a snapshot of memory in an external process.
    /// </summary>
    public abstract class SnapshotRegion : NormalizedRegion, IEnumerable
    {
        protected Int32 RegionExtension;    // Variable to indicate a safe number of read-over bytes
        protected unsafe Byte[] CurrentValues;      // Most recently read values
        protected unsafe Byte[] PreviousValues;     // Previously read values

        protected Int32 Alignment;

        // These fields are public as an access optimization from SnapshotElement, and otherwise should be accessed via get/set functions
        public Type ElementType;                        // Element type for this
        public BitArray Valid;                          // Valid bits for use in filtering scans
        public SnapshotElement CurrentSnapshotElement;  // Regions only access one element at a time, so it is held here to avoid uneccessary memory usage

        public SnapshotRegion(IntPtr BaseAddress, Int32 RegionSize) : base(BaseAddress, RegionSize) { RegionExtension = 0; }
        public SnapshotRegion(NormalizedRegion RemoteRegion) : base(RemoteRegion.BaseAddress, RemoteRegion.RegionSize) { RegionExtension = 0; }
        public SnapshotRegion(SnapshotRegion SnapshotRegion) : base(SnapshotRegion.BaseAddress, SnapshotRegion.RegionSize) { this.RegionExtension = SnapshotRegion.RegionExtension; }

        public unsafe abstract SnapshotElement this[Int32 Index] { get; }

        /// <summary>
        /// Expands a region by a given size in both directions (default is element type size) unconditionally
        /// </summary>
        public void ExpandRegion() { ExpandRegion(GetElementReadOverSize()); }
        public void ExpandRegion(Int32 ExpandSize)
        {
            // Expand with negative overflow protection
            if (BaseAddress.ToUInt64() > checked((UInt32)ExpandSize))
                this.BaseAddress = this.BaseAddress.Subtract(ExpandSize);
            else
                this.BaseAddress = IntPtr.Zero;

            // Expand with overflow protection
            Int32 NewRegionSize = unchecked(RegionSize + ExpandSize * 2);
            this.RegionSize = Math.Max(this.RegionSize, NewRegionSize);
        }

        /// <summary>
        /// Fills a region into the available extension space
        /// </summary>
        public void FillRegion()
        {
            Int32 ExpandSize = RegionExtension;
            RegionExtension = 0;

            // Expand with overflow protection
            Int32 NewRegionSize = unchecked(RegionSize + ExpandSize);
            this.RegionSize = Math.Max(this.RegionSize, NewRegionSize);
        }

        /// <summary>
        /// Shrinks a region by the current element size. The old space is marked as extension space.
        /// This is important for elements with values that span several bytes, which need to read past the region size.
        /// </summary>
        public void RelaxRegion()
        {
            Int32 RelaxSize = GetElementReadOverSize();

            FillRegion();

            if (RegionSize >= RelaxSize)
            {
                this.RegionSize -= RelaxSize;
                RegionExtension = RelaxSize;
            }
            else
            {
                RegionExtension = RegionSize;
                RegionSize = 0;
            }
        }

        public void MarkAllValid()
        {
            Valid = new BitArray(RegionSize, true);
        }

        public void MarkAllInvalid()
        {
            Valid = new BitArray(RegionSize, false);
        }

        public void SetElementType(Type ElementType)
        {
            this.ElementType = ElementType;

            if (ElementType == null)
                return;

            // Adjust extension space accordingly
            RelaxRegion();
        }

        public void SetAlignment(Int32 Alignment)
        {
            this.Alignment = Alignment;

            // Enforce alignment constraint on base address
            if (BaseAddress.Mod(Alignment).ToUInt64() != 0)
            {
                unchecked
                {
                    this.BaseAddress = this.BaseAddress.Subtract(BaseAddress.Mod(Alignment));
                    this.BaseAddress = this.BaseAddress.Add(Alignment);

                    this.RegionSize -= Alignment - BaseAddress.Mod(Alignment).ToInt32();
                    if (RegionSize < 0)
                        RegionSize = 0;
                }
            }
        }

        /// <summary>
        /// Determines how many extra bytes an element will need to read to determine it's value
        /// </summary>
        /// <returns></returns>
        public Int32 GetElementReadOverSize()
        {
            return Marshal.SizeOf(ElementType) - 1;
        }

        public Int32 GetRegionExtension()
        {
            return RegionExtension;
        }

        public void SetCurrentValues(Byte[] NewValues)
        {
            PreviousValues = CurrentValues;
            CurrentValues = NewValues;

            CurrentSnapshotElement.InitializePointers();
        }

        public void SetPreviousValues(Byte[] NewValues)
        {
            PreviousValues = NewValues;

            CurrentSnapshotElement.InitializePointers();
        }

        public Byte[] GetCurrentValues()
        {
            return CurrentValues;
        }

        public Byte[] GetPreviousValues()
        {
            return PreviousValues;
        }

        public Type GetElementType()
        {
            return ElementType;
        }

        public Int32 GetAlignment()
        {
            return Alignment;
        }

        /// <summary>
        /// Returns true if an region can be compared with itself: previous and current values are initialized
        /// </summary>
        /// <returns></returns>
        public Boolean CanCompare()
        {
            if (PreviousValues == null || CurrentValues == null || PreviousValues.Length != CurrentValues.Length)
                return false;
            return true;
        }

        public Boolean HasValues()
        {
            if (CurrentValues == null)
                return false;
            return true;
        }

        public Byte[] ReadAllSnapshotMemory(OSInterface OSInterface, Boolean KeepValues = true)
        {
            Boolean SuccessReading = false;
            Byte[] CurrentValues = OSInterface.Process.ReadBytes(this.BaseAddress, this.RegionSize + RegionExtension, out SuccessReading);

            if (!SuccessReading)
                throw new ScanFailedException();

            if (KeepValues)
                SetCurrentValues(CurrentValues);

            return CurrentValues;
        }

        public IEnumerator GetEnumerator()
        {
            if (RegionSize <= 0)
                yield break;

            // Prevent the GC from moving buffers around
            GCHandle CurrentValuesHandle = GCHandle.Alloc(CurrentValues, GCHandleType.Pinned);
            GCHandle PreviousValuesHandle = GCHandle.Alloc(PreviousValues, GCHandleType.Pinned);

            CurrentSnapshotElement.InitializePointers();

            // Return the first element. This allows us to call IncrementPointers each loop unconditionally, with small speed gains.
            yield return CurrentSnapshotElement;

            if (Alignment == 1)
            {
                // Utilizes ++ operators, fast but we check every address
                for (Int32 Index = 1; Index < RegionSize; Index++)
                {
                    CurrentSnapshotElement.IncrementPointers();
                    yield return CurrentSnapshotElement;
                }
            }
            else
            {
                // Utilizes += operators, faster because we access far less addresses
                for (Int32 Index = Alignment; Index < RegionSize; Index += Alignment)
                {
                    CurrentSnapshotElement.AddPointers(Alignment);
                    yield return CurrentSnapshotElement;
                }
            }

            // Let the GC do what it wants now
            CurrentValuesHandle.Free();
            PreviousValuesHandle.Free();
        }

    } // End class

    /// <summary>
    /// Defines a snapshot of memory in an external process, as well as assigned labels to this memory.
    /// </summary>
    public class SnapshotRegion<LabelType> : SnapshotRegion where LabelType : struct
    {
        public LabelType?[] ElementLabels;      // Labels for individual elements

        public SnapshotRegion(IntPtr BaseAddress, Int32 RegionSize) : base(BaseAddress, RegionSize) { CurrentSnapshotElement = new SnapshotElement<LabelType>(this); }
        public SnapshotRegion(NormalizedRegion RemoteRegion) : base(RemoteRegion) { CurrentSnapshotElement = new SnapshotElement<LabelType>(this); }
        public SnapshotRegion(SnapshotRegion SnapshotRegion) : base(SnapshotRegion)
        {
            CurrentSnapshotElement = new SnapshotElement<LabelType>(this);
            SetCurrentValues(SnapshotRegion.GetCurrentValues() == null ? null : (Byte[])SnapshotRegion.GetCurrentValues().Clone());
            SetPreviousValues(SnapshotRegion.GetPreviousValues() == null ? null : (Byte[])SnapshotRegion.GetPreviousValues().Clone());
        }

        public LabelType?[] GetElementLabels()
        {
            return ElementLabels;
        }

        public void SetElementLabels(LabelType? ElementLabel)
        {
            this.ElementLabels = Enumerable.Repeat(ElementLabel, RegionSize).ToArray();
        }

        public void SetElementLabels(LabelType?[] ElementLabels)
        {
            this.ElementLabels = ElementLabels;
        }

        /// <summary>
        /// Indexer to access a labeled unified snapshot element at the specified index
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public unsafe override SnapshotElement this[Int32 Index]
        {
            get
            {
                SnapshotElement<LabelType> Element = new SnapshotElement<LabelType>(this);
                Element.InitializePointers(Index);
                return Element;
            }
        }

        /// <summary>
        /// Retrieves a subset of snapshot regions where valid bits are true. Discards valid bits afterwards.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SnapshotRegion<LabelType>> GetValidRegions()
        {
            List<SnapshotRegion<LabelType>> ValidRegions = new List<SnapshotRegion<LabelType>>();
            for (Int32 StartIndex = 0; StartIndex < (Valid == null ? 0 : Valid.Length); StartIndex += Alignment)
            {
                if (!Valid[StartIndex])
                    continue;

                // Get the length of this valid region
                Int32 ValidRegionSize = 0;
                do
                {
                    // We only care if the aligned elements are valid
                    ValidRegionSize += Alignment;
                }
                while (StartIndex + ValidRegionSize < Valid.Length && Valid[StartIndex + ValidRegionSize]);

                // Create new subregion from this valid region
                SnapshotRegion<LabelType> SubRegion = new SnapshotRegion<LabelType>(this.BaseAddress + StartIndex, ValidRegionSize);

                // Collect the current values. Attempts to collect out of bounds into extended space.
                SubRegion.SetCurrentValues(CurrentValues.LargestSubArray(StartIndex, ValidRegionSize + GetElementReadOverSize()));

                // Collect the element labels. Attempts to collect out of bounds into extended space.
                SubRegion.SetElementLabels(ElementLabels.LargestSubArray(StartIndex, ValidRegionSize + GetElementReadOverSize()));

                // If we were able to grab values into the extended space, update the extension size.
                if (SubRegion.GetCurrentValues() != null)
                    SubRegion.RegionExtension = SubRegion.GetCurrentValues().Length - ValidRegionSize;
                else
                    SubRegion.RegionExtension = GetElementReadOverSize();

                SubRegion.SetElementType(ElementType);
                SubRegion.SetAlignment(Alignment);

                ValidRegions.Add(SubRegion);

                StartIndex += ValidRegionSize;
            }

            // Free memory for GC on valid array
            Valid = null;

            return ValidRegions;
        }

    } // End class

} // End namespace