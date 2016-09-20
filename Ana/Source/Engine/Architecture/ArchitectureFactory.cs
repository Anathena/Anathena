namespace Ana.Source.Engine.Architecture
{
    using System;

    public static class ArchitectureFactory
    {
        public static IArchitecture GetArchitecture()
        {
            // For now, x86/64 is all that is supported. If this changes, we will need to determine the architecture here
            ArchitectureType architectureType = ArchitectureType.x86_64;

            switch (architectureType)
            {
                case ArchitectureType.x86_64:
                    return new x86_64Architecture();
                default:
                    throw new Exception("Unsupported Architecture Specified");
            }
        }

    }
    //// End class
}
//// End namespace