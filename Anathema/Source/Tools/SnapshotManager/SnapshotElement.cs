using System;

namespace Anathema
{
    /// <summary>
    /// Class used by SnapshotRegion as a wrapper for indexing into the raw collection of data
    /// </summary>
    public class SnapshotElement
    {
        protected SnapshotRegion Parent;
        protected Int32 Index;
        public IntPtr BaseAddress;

        private Byte? _CurrentValue;
        private Byte? _PreviousValue;
        public Byte? CurrentValue { get { return _CurrentValue; } set { _CurrentValue = value; Parent[Index] = this; } }
        public Byte? PreviousValue { get { return _PreviousValue; } set { _PreviousValue = value; Parent[Index] = this; } }

        protected SnapshotElement() { }
        public SnapshotElement(IntPtr BaseAddress, SnapshotRegion Parent, Int32 Index, Byte? CurrentValue, Byte? PreviousValue)
        {
            this.BaseAddress = BaseAddress;
            this.Parent = Parent;
            this.Index = Index;
            this._CurrentValue = CurrentValue;
            this._PreviousValue = PreviousValue;
        }
    }

    public class SnapshotElement<T> : SnapshotElement where T : struct
    {
        private new SnapshotRegion<T> Parent;
        private T? _MemoryLabel;
        public T? MemoryLabel { get { return _MemoryLabel; } set { _MemoryLabel = value; Parent[Index] = this; } }

        public SnapshotElement(IntPtr BaseAddress, SnapshotRegion<T> Parent, Int32 Index, Byte? CurrentValue, Byte? PreviousValue, T? Label) : base(BaseAddress, Parent, Index, CurrentValue, PreviousValue)
        {
            this.Parent = Parent;
            this._MemoryLabel = Label;
        }
    }
}
