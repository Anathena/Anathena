using System;
using System.Collections.Generic;
using System.Linq;

namespace Anathema
{
    /// <summary>
    /// Class used by SnapshotRegion as a wrapper for indexing into the raw collection of data
    /// </summary>
    public class SnapshotElement
    {
        public IntPtr BaseAddress;

        protected SnapshotRegion Parent;
        protected Int32 Index;

        private Type ElementType;

        private Byte[] _CurrentValue;
        private Byte[] _PreviousValue;
        public Byte[] CurrentValue { get { return _CurrentValue; } set { _CurrentValue = value; Parent[Index] = this; } }
        public Byte[] PreviousValue { get { return _PreviousValue; } set { _PreviousValue = value; Parent[Index] = this; } }

        protected SnapshotElement() { }
        public SnapshotElement(IntPtr BaseAddress, Type ElementType, SnapshotRegion Parent, Int32 Index, Byte[] CurrentValue, Byte[] PreviousValue)
        {
            this.BaseAddress = BaseAddress;
            this.ElementType = ElementType;
            this.Parent = Parent;
            this.Index = Index;
            this._CurrentValue = CurrentValue;
            this._PreviousValue = PreviousValue;
        }

        private dynamic GetValue(Byte[] Array)
        {
            dynamic Value = 0;
            var @switch = new Dictionary<Type, Action> {
                    { typeof(Byte), () => Value = Array[0] },
                    { typeof(SByte), () => Value = (SByte)Array[0] },
                    { typeof(Int16), () => Value = BitConverter.ToInt16(Array, 0) },
                    { typeof(Int32), () => Value = BitConverter.ToInt32(Array, 0) },
                    { typeof(Int64), () => Value = BitConverter.ToInt64(Array, 0) },
                    { typeof(UInt16), () => Value = BitConverter.ToUInt16(Array, 0) },
                    { typeof(UInt32), () => Value = BitConverter.ToUInt32(Array, 0) },
                    { typeof(UInt64), () => Value = BitConverter.ToUInt64(Array, 0) },
                    { typeof(Single), () => Value = BitConverter.ToSingle(Array, 0) },
                    { typeof(Double), () => Value = BitConverter.ToDouble(Array, 0) }
                };

            if (@switch.ContainsKey(ElementType))
                @switch[ElementType]();

            return Value;
        }

        public Boolean Changed()
        {
            return !CurrentValue.SequenceEqual(PreviousValue);
        }

        public Boolean Unchanged()
        {
            return CurrentValue.SequenceEqual(PreviousValue);
        }

        public Boolean Increased()
        {
            return (GetValue(CurrentValue) > GetValue(PreviousValue));
        }

        public Boolean Decreased()
        {
            return (GetValue(CurrentValue) < GetValue(PreviousValue));
        }

        public Boolean IncreasedInclusive()
        {
            return (GetValue(CurrentValue) >= GetValue(PreviousValue));
        }

        public Boolean DecreasedInclusive()
        {
            return (GetValue(CurrentValue) <= GetValue(PreviousValue));
        }

        /// <summary>
        /// Returns true if an element can be compared with itself: previous and current values are initialized
        /// </summary>
        /// <returns></returns>
        public Boolean CanCompare()
        {
            if (PreviousValue == null || CurrentValue == null || PreviousValue.Length != CurrentValue.Length)
                return false;
            return true;
        }
    }

    public class SnapshotElement<T> : SnapshotElement where T : struct
    {
        private new SnapshotRegion<T> Parent;
        private T? _MemoryLabel;
        public T? MemoryLabel { get { return _MemoryLabel; } set { _MemoryLabel = value; Parent[Index] = this; } }

        public SnapshotElement(IntPtr BaseAddress, Type ElementType, SnapshotRegion<T> Parent, Int32 Index, Byte[] CurrentValue, Byte[] PreviousValue, T? Label)
            : base(BaseAddress, ElementType, Parent, Index, CurrentValue, PreviousValue)
        {
            this.Parent = Parent;
            this._MemoryLabel = Label;
        }
    }
}
