namespace ScintillaNET
{
    /// <summary>
    /// Fold actions.
    /// </summary>
    public enum FoldAction
    {
        /// <summary>
        /// Contract the fold.
        /// </summary>
        Contract = NativeMethods.SC_FOLDACTION_CONTRACT,

        /// <summary>
        /// Expand the fold.
        /// </summary>
        Expand = NativeMethods.SC_FOLDACTION_EXPAND,

        /// <summary>
        /// Toggle between contracted and expanded.
        /// </summary>
        Toggle = NativeMethods.SC_FOLDACTION_TOGGLE
    }
}
