using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ana.Source.Snapshots
{
    /// <summary>
    /// Class used by SnapshotRegion as a wrapper for indexing into the raw collection of data
    /// </summary>
    [Obfuscation(ApplyToMembers = true, Exclude = true)]
    internal abstract class SnapshotElement
    {
        // Variables required for committing changes back to the region from which this element comes
        protected SnapshotRegion parent;
        protected unsafe Byte* currentValuePointer;
        protected unsafe Byte* previousValuePointer;
        protected Int32 currentElementIndex;
        protected TypeCode currentType;

        public Type ElementType { get { return parent.ElementType; } set { } }
        public IntPtr BaseAddress { get { return parent.BaseAddress + currentElementIndex; } }
        public Boolean Valid { set { parent.Valid[currentElementIndex] = value; } }

        public SnapshotElement(SnapshotRegion Parent)
        {
            this.parent = Parent;
        }

        public unsafe void InitializePointers(Int32 index = 0)
        {
            currentElementIndex = index;
            currentType = Type.GetTypeCode(parent.ElementType);

            Byte[] currentValues = parent.GetCurrentValues();
            if (currentValues != null && currentValues.Count() > 0)
            {
                fixed (Byte* Base = &currentValues[index])
                {
                    currentValuePointer = Base;
                }
            }
            else
            {
                currentValuePointer = null;
            }

            Byte[] previousValues = parent.GetPreviousValues();
            if (previousValues != null && previousValues.Count() > 0)
            {
                fixed (Byte* Base = &previousValues[index])
                {
                    previousValuePointer = Base;
                }
            }
            else
            {
                previousValuePointer = null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void IncrementPointers()
        {
            currentElementIndex++;
            currentValuePointer++;
            previousValuePointer++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void AddPointers(Int32 Alignment)
        {
            currentElementIndex += Alignment;
            currentValuePointer += Alignment;
            previousValuePointer += Alignment;
        }

        private unsafe dynamic GetValue(Byte* array)
        {
            switch (currentType)
            {
                case TypeCode.Byte: return *array;
                case TypeCode.SByte: return *(SByte*)array;
                case TypeCode.Int16: return *(Int16*)array;
                case TypeCode.Int32: return *(Int32*)array;
                case TypeCode.Int64: return *(Int64*)array;
                case TypeCode.UInt16: return *(UInt16*)array;
                case TypeCode.UInt32: return *(UInt32*)array;
                case TypeCode.UInt64: return *(UInt64*)array;
                case TypeCode.Single: return *(Single*)array;
                case TypeCode.Double: return *(Double*)array;
                default: return 0;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean Changed()
        {
            return (GetValue(currentValuePointer) != GetValue(previousValuePointer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean Unchanged()
        {
            return (GetValue(currentValuePointer) == GetValue(previousValuePointer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean Increased()
        {
            return (GetValue(currentValuePointer) > GetValue(previousValuePointer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean Decreased()
        {
            return (GetValue(currentValuePointer) < GetValue(previousValuePointer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean EqualToValue(dynamic value)
        {
            return (GetValue(currentValuePointer) == value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean NotEqualToValue(dynamic value)
        {
            return (GetValue(currentValuePointer) != value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean GreaterThanValue(dynamic value)
        {
            return (GetValue(currentValuePointer) > value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean GreaterThanOrEqualToValue(dynamic value)
        {
            return (GetValue(currentValuePointer) >= value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean LessThanValue(dynamic value)
        {
            return (GetValue(currentValuePointer) < value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean LessThanOrEqualToValue(dynamic value)
        {
            return (GetValue(currentValuePointer) <= value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean IncreasedByValue(dynamic value)
        {
            return (GetValue(currentValuePointer) == unchecked(GetValue(previousValuePointer) + value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean DecreasedByValue(dynamic value)
        {
            return (GetValue(currentValuePointer) == unchecked(GetValue(previousValuePointer) - value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean IsScientificNotation()
        {
            return ((String)GetValue(currentValuePointer).ToString()).ToLower().Contains('e');
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe dynamic GetValue()
        {
            return (GetValue(currentValuePointer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe dynamic GetPreviousValue()
        {
            return (GetValue(previousValuePointer));
        }

        public dynamic Value
        {
            get
            {
                return GetValue();
            }
        }

        public dynamic PreviousValue
        {
            get
            {
                return 0;
                // TODO: This is pretty problematic for ScanResults since it may not be set (and thus crash)
                // return GetPreviousValue();
            }
        }

    } // End class

    /// <summary>
    /// Class used by SnapshotRegion as a wrapper for indexing into the raw collection of data
    /// </summary>
    internal class SnapshotElement<LabelType> : SnapshotElement where LabelType : struct
    {
        public unsafe LabelType? ElementLabel
        {
            get
            {
                return Parent.ElementLabels == null ? null : Parent.ElementLabels[currentElementIndex];
            }

            set
            {
                Parent.ElementLabels[currentElementIndex] = value;
            }
        }

        private SnapshotRegion<LabelType> Parent { get; set; }

        public SnapshotElement(SnapshotRegion<LabelType> Parent) : base(Parent)
        {
            this.Parent = Parent;
        }

    } // End class

} // End namespace