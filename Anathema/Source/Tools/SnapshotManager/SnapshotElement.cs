using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Anathema
{
    /// <summary>
    /// Class used by SnapshotRegion as a wrapper for indexing into the raw collection of data
    /// </summary>
    public abstract class SnapshotElement
    {
        // Variables required for committing changes back to the region from which this element comes
        protected SnapshotRegion Parent;
        protected Int32 Index;

        public readonly IntPtr BaseAddress;     // Address of this Element
        public readonly Byte[] PreviousValue;   // Raw previous and values
        public readonly Byte[] CurrentValue;    // Raw current values

        private Boolean _Valid;
        public Boolean Valid { get { return _Valid; } set { _Valid = value; Parent[Index] = this; } }
        public Type ElementType { get; set; }   // Type for interpreting the stored values

        public SnapshotElement(IntPtr BaseAddress, Byte[] CurrentValue, Byte[] PreviousValue)
        {
            this.BaseAddress = BaseAddress;
            this.CurrentValue = CurrentValue;
            this.PreviousValue = PreviousValue;
        }

        private dynamic GetValue(Byte[] Array)
        {
            switch (Type.GetTypeCode(ElementType))
            {
                case TypeCode.Byte: return Array[0];
                case TypeCode.SByte: return (SByte)Array[0];
                case TypeCode.Int16: return BitConverter.ToInt16(Array, 0);
                case TypeCode.Int32: return BitConverter.ToInt32(Array, 0);
                case TypeCode.Int64: return BitConverter.ToInt64(Array, 0);
                case TypeCode.UInt16: return BitConverter.ToUInt16(Array, 0);
                case TypeCode.UInt32: return BitConverter.ToUInt32(Array, 0);
                case TypeCode.UInt64: return BitConverter.ToUInt64(Array, 0);
                case TypeCode.Single: return BitConverter.ToSingle(Array, 0);
                case TypeCode.Double: return BitConverter.ToDouble(Array, 0);
                default: return 0;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Changed()
        {
            return !CurrentValue.SequenceEqual(PreviousValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Unchanged()
        {
            return CurrentValue.SequenceEqual(PreviousValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Increased()
        {
            return (GetValue(CurrentValue) > GetValue(PreviousValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean Decreased()
        {
            return (GetValue(CurrentValue) < GetValue(PreviousValue));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean EqualToValue(dynamic Value)
        {
            return (GetValue(CurrentValue) == Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean NotEqualToValue(dynamic Value)
        {
            return (GetValue(CurrentValue) != Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean GreaterThanValue(dynamic Value)
        {
            return (GetValue(CurrentValue) > Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean GreaterThanOrEqualToValue(dynamic Value)
        {
            return (GetValue(CurrentValue) >= Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean LessThanValue(dynamic Value)
        {
            return (GetValue(CurrentValue) < Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean LessThanOrEqualToValue(dynamic Value)
        {
            return (GetValue(CurrentValue) <= Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean IncreasedByValue(dynamic Value)
        {
            return (GetValue(CurrentValue) == GetValue(PreviousValue) + Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean DecreasedByValue(dynamic Value)
        {
            return (GetValue(CurrentValue) == GetValue(PreviousValue) - Value);
        }
    }

    /// <summary>
    /// Class used by SnapshotRegion as a wrapper for indexing into the raw collection of data
    /// </summary>
    public class SnapshotElement<LabelType> : SnapshotElement where LabelType : struct
    {
        private LabelType? _ElementLabel;
        public LabelType? ElementLabel { get { return _ElementLabel; } set { _ElementLabel = value; Parent[Index] = this; } }
        
        public SnapshotElement(IntPtr BaseAddress, SnapshotRegion Parent, Int32 Index, Type ElementType, Boolean Valid, Byte[] CurrentValue, Byte[] PreviousValue, LabelType? ElementLabel)
            : base(BaseAddress, CurrentValue, PreviousValue)
        {
            this.Parent = Parent;
            this.Index = Index;
            this.ElementType = ElementType;
            this.Valid = Valid;
            this._ElementLabel = ElementLabel;
        }
    }
}
