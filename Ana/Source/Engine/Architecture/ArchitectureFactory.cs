namespace Ana.Source.Engine.Architecture
{
    using System;

    /// <summary>
    /// A factory that returns an object for assembling and disassembling instructions based on the system architecture
    /// </summary>
    internal static class ArchitectureFactory
    {
        /// <summary>
        /// Gets an object for assembling and disassembling instructions
        /// </summary>
        /// <returns>Returns an object for assembling and disassembling instructions based on the system architecture</returns>
        public static IArchitecture GetArchitecture()
        {
            // For now, x86/64 is all that is supported. If this changes, we will need to determine the architecture here
            ArchitectureType architectureType = ArchitectureType.x86_64;

            switch (architectureType)
            {
                case ArchitectureType.x86_64:
                    return new Architecture86_64();
                default:
                    throw new Exception("Unsupported Architecture Specified");
            }
        }
    }
    //// End class
}
//// End namespace