using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Anathema.Source.Utils.Snapshots
{
    /// <summary>
    /// Class used by SnapshotRegion as a wrapper for indexing into the raw collection of data
    /// </summary>
    public abstract class SnapshotElement
    {
        // Variables required for committing changes back to the region from which this element comes
        protected SnapshotRegion Parent;
        protected unsafe Byte* CurrentValuePointer;
        protected unsafe Byte* PreviousValuePointer;
        protected Int32 CurrentElementIndex;
        protected TypeCode CurrentType;

        public Type ElementType { get { return Parent.ElementType; } set { } }
        public IntPtr BaseAddress { get { return Parent.BaseAddress + CurrentElementIndex; } }
        public Boolean Valid { set { Parent.Valid[CurrentElementIndex] = value; } }

        public SnapshotElement(SnapshotRegion Parent)
        {
            this.Parent = Parent;
        }

        public unsafe void InitializePointers(Int32 Index = 0)
        {
            CurrentElementIndex = Index;
            CurrentType = Type.GetTypeCode(Parent.ElementType);

            Byte[] CurrentValues = Parent.GetCurrentValues();
            if (CurrentValues != null)
            {
                fixed (Byte* Base = &CurrentValues[Index])
                {
                    CurrentValuePointer = Base;
                }
            }
            else
            {
                CurrentValuePointer = null;
            }

            Byte[] PreviousValues = Parent.GetPreviousValues();
            if (PreviousValues != null)
            {
                fixed (Byte* Base = &PreviousValues[Index])
                {
                    PreviousValuePointer = Base;
                }
            }
            else
            {
                PreviousValuePointer = null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void IncrementPointers()
        {
            CurrentElementIndex++;
            CurrentValuePointer++;
            PreviousValuePointer++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void AddPointers(Int32 Alignment)
        {
            CurrentElementIndex += Alignment;
            CurrentValuePointer += Alignment;
            PreviousValuePointer += Alignment;
        }

        [Obfuscation(Exclude = true)]
        private unsafe dynamic GetValue(Byte* Array)
        {
            switch (CurrentType)
            {
                case TypeCode.Byte: return *Array;
                case TypeCode.SByte: return *(SByte*)Array;
                case TypeCode.Int16: return *(Int16*)Array;
                case TypeCode.Int32: return *(Int32*)Array;
                case TypeCode.Int64: return *(Int64*)Array;
                case TypeCode.UInt16: return *(UInt16*)Array;
                case TypeCode.UInt32: return *(UInt32*)Array;
                case TypeCode.UInt64: return *(UInt64*)Array;
                case TypeCode.Single: return *(Single*)Array;
                case TypeCode.Double: return *(Double*)Array;
                default: return 0;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean Changed()
        {
            return (GetValue(CurrentValuePointer) != GetValue(PreviousValuePointer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean Unchanged()
        {
            return (GetValue(CurrentValuePointer) == GetValue(PreviousValuePointer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean Increased()
        {
            return (GetValue(CurrentValuePointer) > GetValue(PreviousValuePointer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Boolean Decreased()
        {
            return (GetValue(CurrentValuePointer) < GetValue(PreviousValuePointer));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obfuscation(Exclude = true)]
        public unsafe Boolean EqualToValue(dynamic Value)
        {
            return (GetValue(CurrentValuePointer) == Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obfuscation(Exclude = true)]
        public unsafe Boolean NotEqualToValue(dynamic Value)
        {
            return (GetValue(CurrentValuePointer) != Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obfuscation(Exclude = true)]
        public unsafe Boolean GreaterThanValue(dynamic Value)
        {
            return (GetValue(CurrentValuePointer) > Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obfuscation(Exclude = true)]
        public unsafe Boolean GreaterThanOrEqualToValue(dynamic Value)
        {
            return (GetValue(CurrentValuePointer) >= Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obfuscation(Exclude = true)]
        public unsafe Boolean LessThanValue(dynamic Value)
        {
            return (GetValue(CurrentValuePointer) < Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obfuscation(Exclude = true)]
        public unsafe Boolean LessThanOrEqualToValue(dynamic Value)
        {
            return (GetValue(CurrentValuePointer) <= Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obfuscation(Exclude = true)]
        public unsafe Boolean IncreasedByValue(dynamic Value)
        {
            return (GetValue(CurrentValuePointer) == unchecked(GetValue(PreviousValuePointer) + Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obfuscation(Exclude = true)]
        public unsafe Boolean DecreasedByValue(dynamic Value)
        {
            return (GetValue(CurrentValuePointer) == unchecked(GetValue(PreviousValuePointer) - Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obfuscation(Exclude = true)]
        public unsafe Boolean IsScientificNotation()
        {
            return ((String)GetValue(CurrentValuePointer).ToString()).ToLower().Contains('e');
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [Obfuscation(Exclude = true)]
        public unsafe dynamic GetValue()
        {
            return (GetValue(CurrentValuePointer));
        }

    } // End class

    /// <summary>
    /// Class used by SnapshotRegion as a wrapper for indexing into the raw collection of data
    /// </summary>
    public class SnapshotElement<LabelType> : SnapshotElement where LabelType : struct
    {
        new SnapshotRegion<LabelType> Parent;

        public unsafe LabelType? ElementLabel { get { return Parent.ElementLabels == null ? null : Parent.ElementLabels[CurrentElementIndex]; } set { Parent.ElementLabels[CurrentElementIndex] = value; } }

        public SnapshotElement(SnapshotRegion<LabelType> Parent) : base(Parent)
        {
            this.Parent = Parent;
        }

    } // End class

} // End namespace