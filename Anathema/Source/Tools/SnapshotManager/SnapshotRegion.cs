using Binarysharp.MemoryManagement.Memory;
using System;
using System.Collections;

namespace Anathema
{
    /// <summary>
    /// Defines a snapshot of memory in an external process.
    /// </summary>
    public class SnapshotRegion : RemoteRegion, IEnumerable
    {
        protected Byte[] CurrentValues;
        protected Byte[] PreviousValues;

        public SnapshotRegion(IntPtr BaseAddress, Int32 RegionSize) : base(null, BaseAddress, RegionSize) { }
        public SnapshotRegion(RemoteRegion RemoteRegion) : base(null, RemoteRegion.BaseAddress, RemoteRegion.RegionSize) { }

        /// <summary>
        /// Access a unified snapshot element at the specified index
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public SnapshotElement this[Int32 Index]
        {
            get
            {
                return new SnapshotElement(
                this, Index,
                CurrentValues == null ? new Byte?() : CurrentValues[Index],
                PreviousValues == null ? new Byte?() : PreviousValues[Index]
                );
            }
            set
            {
                if (value.CurrentValue != null) CurrentValues[Index] = value.CurrentValue.Value;
                if (value.PreviousValue != null) PreviousValues[Index] = value.PreviousValue.Value;
            }
        }

        public void SetCurrentValues(Byte[] NewValues)
        {
            PreviousValues = CurrentValues;
            CurrentValues = NewValues;
        }

        public Byte[] GetCurrentValues()
        {
            return CurrentValues;
        }

        public Byte[] GetPreviousValues()
        {
            return PreviousValues;
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

        /// <summary>
        /// Access a labeled unified snapshot element at the specified index
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public new SnapshotElement<T> this[Int32 Index]
        {
            get
            {
                return new SnapshotElement<T>(
                (SnapshotRegion<T>)this, Index,
                CurrentValues == null ? new Byte?() : CurrentValues[Index],
                PreviousValues == null ? new Byte?() : PreviousValues[Index],
                MemoryLabels == null ? new T?() : MemoryLabels[Index]
                );
            }
            set
            {
                if (value.CurrentValue != null) CurrentValues[Index] = value.CurrentValue.Value;
                if (value.PreviousValue != null) PreviousValues[Index] = value.PreviousValue.Value;
                if (value.MemoryLabel != null) MemoryLabels[Index] = value.MemoryLabel.Value;
            }
        }

        public override IEnumerator GetEnumerator()
        {
            for (Int32 Index = 0; Index < RegionSize; Index++)
                yield return this[Index];
        }
    }
}
