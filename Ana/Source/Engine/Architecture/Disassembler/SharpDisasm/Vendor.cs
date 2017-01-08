namespace Ana.Source.Engine.Architecture.Disassembler.SharpDisasm
{
    /// <summary>
    /// Allows control over which vendor instructions should be disassembled.
    /// </summary>
    public enum Vendor
    {
        /// <summary>
        /// Allow AMD instructions.
        /// </summary>
        AMD = 0,

        /// <summary>
        /// Allow Intel instructions.
        /// </summary>
        Intel = 1,

        /// <summary>
        /// Allow both AMD and Intel instructions.
        /// </summary>
        Any = 2
    }
    //// End enum
}
//// End namespace