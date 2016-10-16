namespace Ana.Source.Snapshots
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Class used by SnapshotRegion as a wrapper for indexing into the raw collection of data
    /// </summary>
    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    internal abstract class SnapshotElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotElement" /> class
        /// </summary>
        /// <param name="parent">The parent region that contains this element</param>
        public SnapshotElement(SnapshotRegion parent)
        {
            this.Parent = parent;
        }

        public Type ElementType
        {
            get
            {
                return this.Parent.ElementType;
            }

            set
            {
            }
        }

        public IntPtr BaseAddress
        {
            get
            {
                return this.Parent.BaseAddress + this.CurrentElementIndex;
            }
        }

        public Boolean Valid
        {
            set
            {
                this.Parent.Valid[this.CurrentElementIndex] = value;
            }
        }

        public dynamic Value
        {
            get
            {
                return this.GetCurrentValue();
            }
        }

        public dynamic PreviousValue
        {
            get
            {
                return this.GetPreviousValue();
            }
        }

        protected SnapshotRegion Parent { get; set; }

        protected unsafe Byte* CurrentValuePointer { get; set; }

        protected unsafe Byte* PreviousValuePointer { get; set; }

        protected Int32 CurrentElementIndex { get; set; }

        protected TypeCode CurrentType { get; set; }

        public unsafe void InitializePointers(Int32 index = 0)
        {
            this.CurrentElementIndex = index;
            this.CurrentType = Type.GetTypeCode(this.Parent.ElementType);
            Byte[] currentValues = this.Parent.GetCurrentValues();
            Byte[] previousValues = this.Parent.GetPreviousValues();

            if (currentValues != null && currentValues.Count() > 0)
            {
                fixed (Byte* pointerBase = &currentValues[index])
                {
                    this.CurrentValuePointer = pointerBase;
                }
            }
            else
            {
                this.CurrentValuePointer = null;
            }

            if (previousValues != null && previousValues.Count() > 0)
            {
                fixed (Byte* pointerBase = &previousValues[index])
                {
                    this.PreviousValuePointer = pointerBase;
                }
            }
            else
            {
                this.PreviousValuePointer = null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void IncrementPointers()
        {
            this.CurrentElementIndex++;
            this.CurrentValuePointer++;
            this.PreviousValuePointer++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void AddPointers(Int32 alignment)
        {
            this.CurrentElementIndex += alignment;
            this.CurrentValuePointer += alignment;
            this.PreviousValuePointer += alignment;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean Changed()
        {
            return this.GetValue(this.CurrentValuePointer) != this.GetValue(this.PreviousValuePointer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean Unchanged()
        {
            return this.GetValue(this.CurrentValuePointer) == this.GetValue(this.PreviousValuePointer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean Increased()
        {
            return this.GetValue(this.CurrentValuePointer) > this.GetValue(this.PreviousValuePointer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean Decreased()
        {
            return this.GetValue(this.CurrentValuePointer) < this.GetValue(this.PreviousValuePointer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean EqualToValue(dynamic value)
        {
            return this.GetValue(this.CurrentValuePointer) == value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean NotEqualToValue(dynamic value)
        {
            return this.GetValue(this.CurrentValuePointer) != value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean GreaterThanValue(dynamic value)
        {
            return this.GetValue(this.CurrentValuePointer) > value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean GreaterThanOrEqualToValue(dynamic value)
        {
            return this.GetValue(this.CurrentValuePointer) >= value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean LessThanValue(dynamic value)
        {
            return this.GetValue(this.CurrentValuePointer) < value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean LessThanOrEqualToValue(dynamic value)
        {
            return this.GetValue(this.CurrentValuePointer) <= value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean IncreasedByValue(dynamic value)
        {
            return this.GetValue(this.CurrentValuePointer) == unchecked(this.GetValue(this.PreviousValuePointer) + value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean DecreasedByValue(dynamic value)
        {
            return this.GetValue(this.CurrentValuePointer) == unchecked(this.GetValue(this.PreviousValuePointer) - value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean IsScientificNotation()
        {
            return ((String)this.GetValue(this.CurrentValuePointer).ToString()).ToLower().Contains('e');
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe dynamic GetCurrentValue()
        {
            return this.GetValue(this.CurrentValuePointer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe dynamic GetPreviousValue()
        {
            return this.GetValue(this.PreviousValuePointer);
        }

        public unsafe Boolean HasCurrentValue()
        {
            if (this.CurrentValuePointer == (Byte*)0)
            {
                return false;
            }

            return true;
        }

        public unsafe Boolean HasPreviousValue()
        {
            if (this.PreviousValuePointer == (Byte*)0)
            {
                return false;
            }

            return true;
        }

        private unsafe dynamic GetValue(Byte* array)
        {
            switch (this.CurrentType)
            {
                case TypeCode.Byte:
                    return *array;
                case TypeCode.SByte:
                    return *(SByte*)array;
                case TypeCode.Int16:
                    return *(Int16*)array;
                case TypeCode.Int32:
                    return *(Int32*)array;
                case TypeCode.Int64:
                    return *(Int64*)array;
                case TypeCode.UInt16:
                    return *(UInt16*)array;
                case TypeCode.UInt32:
                    return *(UInt32*)array;
                case TypeCode.UInt64:
                    return *(UInt64*)array;
                case TypeCode.Single:
                    return *(Single*)array;
                case TypeCode.Double:
                    return *(Double*)array;
                default:
                    throw new Exception("Invalid element type");
            }
        }
    }
    //// End class

    /// <summary>
    /// Class used by SnapshotRegion as a wrapper for indexing into the raw collection of data
    /// </summary>
    /// <typeparam name="LabelType">The label type of the snapshot element</typeparam>
    internal class SnapshotElement<LabelType> : SnapshotElement where LabelType : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SnapshotElement{LabelType}" /> class
        /// </summary>
        /// <param name="parent">The parent region that contains this element</param>
        public SnapshotElement(SnapshotRegion<LabelType> parent) : base(parent)
        {
            this.Parent = parent;
        }

        public unsafe LabelType? ElementLabel
        {
            get
            {
                return this.Parent.ElementLabels == null ? null : this.Parent.ElementLabels[this.CurrentElementIndex];
            }

            set
            {
                this.Parent.ElementLabels[this.CurrentElementIndex] = value;
            }
        }

        private new SnapshotRegion<LabelType> Parent { get; set; }
    }
    //// End class
}
//// End namespace