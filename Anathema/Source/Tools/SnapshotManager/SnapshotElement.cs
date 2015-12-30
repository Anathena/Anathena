using System;

namespace Anathema
{
    /// <summary>
    /// Class used by SnapshotRegion as a wrapper for indexing into the raw collection of data
    /// </summary>
    public class SnapshotElement
    {
        public Byte? CurrentValue;
        public Byte? PreviousValue;

        protected SnapshotElement() { }
        public SnapshotElement(Byte? CurrentValue, Byte? PreviousValue)
        {
            this.CurrentValue = CurrentValue;
            this.PreviousValue = PreviousValue;
        }
    }

    public class SnapshotElement<T> : SnapshotElement where T : struct
    {
        public T? Label;

        public SnapshotElement(Byte? CurrentValue, Byte? PreviousValue, T? Label) : base(CurrentValue, PreviousValue)
        {
            this.Label = Label;
        }
    }
}
