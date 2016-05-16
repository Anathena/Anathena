namespace ScintillaNET
{
    /// <summary>
    /// The caret visual style.
    /// </summary>
    public enum CaretStyle
    {
        /// <summary>
        /// The caret is not displayed.
        /// </summary>
        Invisible = NativeMethods.CARETSTYLE_INVISIBLE,

        /// <summary>
        /// The caret is drawn as a vertical line.
        /// </summary>
        Line = NativeMethods.CARETSTYLE_LINE,

        /// <summary>
        /// The caret is drawn as a block.
        /// </summary>
        Block = NativeMethods.CARETSTYLE_BLOCK
    }
}
