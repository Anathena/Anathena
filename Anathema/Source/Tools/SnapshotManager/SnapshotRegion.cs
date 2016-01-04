using Binarysharp.MemoryManagement.Memory;
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
    public class SnapshotRegion : RemoteRegion, IEnumerable
    {
        protected Byte[] CurrentValues;
        protected Byte[] PreviousValues;
        protected Type ElementTypes;
        protected BitArray Valid;

        public SnapshotRegion(IntPtr BaseAddress, Int32 RegionSize) : base(null, BaseAddress, RegionSize) { }
        public SnapshotRegion(RemoteRegion RemoteRegion) : base(null, RemoteRegion.BaseAddress, RemoteRegion.RegionSize) { }

        /// <summary>
        /// Indexer to access a unified snapshot element at the specified index
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public SnapshotElement this[Int32 Index]
        {
            get
            {
                return new SnapshotElement(
                BaseAddress + Index, this, Index,
                ElementTypes,
                Valid == null ? false : Valid[Index],
                CurrentValues == null ? (Byte[])null : CurrentValues.SubArray(Index, Marshal.SizeOf(ElementTypes)),
                PreviousValues == null ? (Byte[])null : PreviousValues.SubArray(Index, Marshal.SizeOf(ElementTypes))
                );
            }
            set
            {
                if (this.Valid != null) Valid[Index] = value.Valid;
            }
        }

        /// <summary>
        /// Returns all subregions of this region which are marked as valid
        /// </summary>
        /// <returns></returns>
        public List<SnapshotRegion> GetValidRegions()
        {
            List<SnapshotRegion> ValidRegions = new List<SnapshotRegion>();
            for (Int32 StartIndex = 0; StartIndex < Valid.Length; StartIndex++)
            {
                if (!Valid[StartIndex])
                    continue;

                Int32 ValidRegionSize = 0;
                while (StartIndex + (++ValidRegionSize) < Valid.Length && Valid[StartIndex + ValidRegionSize]) { }

                SnapshotRegion SubRegion = new SnapshotRegion(this.BaseAddress + StartIndex, ValidRegionSize);
                if (CurrentValues != null)
                    SubRegion.SetCurrentValues(CurrentValues.SubArray(StartIndex, ValidRegionSize));
                SubRegion.SetElementTypes(ElementTypes);

                ValidRegions.Add(SubRegion);

                StartIndex += ValidRegionSize;
            }

            return ValidRegions;
        }

        public void MarkAllValid()
        {
            Valid = new BitArray(RegionSize, true);
        }

        public void MarkAllInvalid()
        {
            Valid = new BitArray(RegionSize, false);
        }

        public void SetElementTypes(Type ElementType)
        {
            this.ElementTypes = ElementType;
        }

        public void SetCurrentValues(Byte[] NewValues, Boolean KeepPreviousValues = true)
        {
            PreviousValues = CurrentValues;
            CurrentValues = NewValues;

            if (!KeepPreviousValues)
                PreviousValues = null;
        }

        public Byte[] GetCurrentValues()
        {
            return CurrentValues;
        }

        public Byte[] GetPreviousValues()
        {
            return PreviousValues;
        }

        public Type GetElementTypes()
        {
            return ElementTypes;
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

        public virtual IEnumerator GetEnumerator()
        {
            for (Int32 Index = 0; Index < RegionSize; Index++)
                yield return this[Index];
        }
    }

    /// <summary>
    /// Defines a snapshot of memory in an external process, as well as assigned labels to this memory.
    /// </summary>
    public class SnapshotRegion<T> : SnapshotRegion where T : struct
    {
        private T?[] MemoryLabels;

        public SnapshotRegion(IntPtr BaseAddress, Int32 RegionSize) : base(BaseAddress, RegionSize) { }
        public SnapshotRegion(RemoteRegion RemoteRegion) : base(RemoteRegion) { }
        public SnapshotRegion(SnapshotRegion SnapshotRegion) : base(SnapshotRegion)
        {
            CurrentValues = SnapshotRegion.GetCurrentValues() == null ? null : (Byte[])SnapshotRegion.GetCurrentValues().Clone();
            PreviousValues = SnapshotRegion.GetPreviousValues() == null ? null : (Byte[])SnapshotRegion.GetPreviousValues().Clone();
            MemoryLabels = new T?[SnapshotRegion.RegionSize];
        }

        public T?[] GetMemoryLabels()
        {
            return MemoryLabels;
        }

        public void SetMemoryLabels(T? MemoryLabel)
        {
            this.MemoryLabels = Enumerable.Repeat(MemoryLabel, RegionSize).ToArray();
        }

        public void SetMemoryLabels(T?[] MemoryLabels)
        {
            this.MemoryLabels = MemoryLabels;
        }

        /// <summary>
        /// Indexer to access a labeled unified snapshot element at the specified index
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public new SnapshotElement<T> this[Int32 Index]
        {
            get
            {
                return new SnapshotElement<T>(
                BaseAddress + Index, this, Index,
                ElementTypes,
                Valid == null ? false : Valid[Index],
                CurrentValues == null ? (Byte[])null : CurrentValues.SubArray(Index, System.Runtime.InteropServices.Marshal.SizeOf(ElementTypes)),
                PreviousValues == null ? (Byte[])null : PreviousValues.SubArray(Index, System.Runtime.InteropServices.Marshal.SizeOf(ElementTypes)),
                MemoryLabels == null ? (T?)null : MemoryLabels[Index]
                );
            }
            set
            {
                if (value.ElementType != null) ElementTypes = value.ElementType; else ElementTypes = null;
                if (this.Valid != null) Valid[Index] = value.Valid;
                if (value.MemoryLabel != null) MemoryLabels[Index] = value.MemoryLabel.Value; else MemoryLabels[Index] = null;
            }
        }

        public new List<SnapshotRegion<T>> GetValidRegions()
        {
            List<SnapshotRegion<T>> ValidRegions = new List<SnapshotRegion<T>>();
            for (Int32 StartIndex = 0; StartIndex < Valid.Length; StartIndex++)
            {
                if (!Valid[StartIndex])
                    continue;

                Int32 ValidRegionSize = 0;
                while (StartIndex + (++ValidRegionSize) < Valid.Length && Valid[StartIndex + ValidRegionSize]) { }

                SnapshotRegion<T> SubRegion = new SnapshotRegion<T>(this.BaseAddress + StartIndex, ValidRegionSize);
                if (CurrentValues != null)
                    SubRegion.SetCurrentValues(CurrentValues.SubArray(StartIndex, ValidRegionSize));
                SubRegion.SetElementTypes(ElementTypes);
                if (MemoryLabels != null)
                    SubRegion.SetMemoryLabels(MemoryLabels.SubArray(StartIndex, ValidRegionSize));

                ValidRegions.Add(SubRegion);

                StartIndex += ValidRegionSize;
            }

            return ValidRegions;
        }

        public override IEnumerator GetEnumerator()
        {
            for (Int32 Index = 0; Index < RegionSize; Index++)
                yield return this[Index];
        }
    }
}