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
                CurrentValues == null ? (Byte?)null : CurrentValues[Index],
                PreviousValues == null ? (Byte?)null : PreviousValues[Index]
                );
            }
            set
            {
                CurrentValues[Index] = value.CurrentValue.Value;
                PreviousValues[Index] = value.PreviousValue.Value;
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
            for (Int32 Index = 0; Index < CurrentValues.Length; Index++)
                yield return this[Index];
        }
    }

    /// <summary>
    /// Defines a snapshot of memory in an external process, as well as assigned labels to this memory.
    /// </summary>
    public class LabeledRegion<T> : SnapshotRegion where T : struct
    {
        public T[] MemoryLabels;

        public LabeledRegion(IntPtr BaseAddress, Int32 RegionSize) : base(BaseAddress, RegionSize) { }
        public LabeledRegion(RemoteRegion RemoteRegion) : base(RemoteRegion) { }
        public LabeledRegion(SnapshotRegion SnapshotRegion) : base(SnapshotRegion)
        {
            CurrentValues = SnapshotRegion.GetCurrentValues() == null ? null : (Byte[])SnapshotRegion.GetCurrentValues().Clone();
            PreviousValues = SnapshotRegion.GetPreviousValues() == null ? null : (Byte[])SnapshotRegion.GetPreviousValues().Clone();
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
                CurrentValues == null ? (Byte?)null : CurrentValues[Index],
                PreviousValues == null ? (Byte?)null : PreviousValues[Index],
                MemoryLabels == null ? (T?)null : MemoryLabels[Index]
                );
            }
            set
            {
                CurrentValues[Index] = value.CurrentValue.Value;
                PreviousValues[Index] = value.PreviousValue.Value;
                MemoryLabels[Index] = value.Label.Value;
            }
        }

        public override IEnumerator GetEnumerator()
        {
            for (Int32 Index = 0; Index < CurrentValues.Length; Index++)
                yield return this[Index];
        }
    }
}
