namespace Anathema.MemoryManagement.Threading
{
    #region SegmentRegisters
    /// <summary>
    /// List of segment registers.
    /// </summary>
    public enum SegmentRegisters
    {
        /// <summary>
        /// The code segment.
        /// </summary>
        Cs,
        /// <summary>
        /// The Data segment.
        /// </summary>
        Ds,
        /// <summary>
        /// The extra data segment.
        /// </summary>
        Es,
        /// <summary>
        /// The points to Thread Information Block (TIB).
        /// </summary>
        Fs,
        /// <summary>
        /// The extra data segment.
        /// </summary>
        Gs,
        /// <summary>
        /// The stack segment.
        /// </summary>
        Ss
    }
    #endregion

} // End namespace