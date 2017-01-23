namespace Ana.Source.Engine.OperatingSystems.Windows.Pe.Structures
{
    using System;

    internal class StructureFactory<T> where T : AbstractStructure
    {
        private T instance;
        private Boolean instanceAlreadyParsed;
        private Byte[] buff;
        private UInt32 offset;

        public Exception ParseException { get; private set; }

        public StructureFactory(Byte[] buff, UInt32 offset)
        {
            this.buff = buff;
            this.offset = offset;
        }

        public T GetInstance()
        {
            // The structure was already parsed. Return the result.
            if (instanceAlreadyParsed)
                return instance;

            // The structure wasn't parsed before. Create and save a new
            // instance and return it.
            instanceAlreadyParsed = true;
            instance = CreateInstance<T>(buff, offset);

            return instance;
        }

        private N CreateInstance<N>(Byte[] buff, UInt32 offset) where N : AbstractStructure
        {
            N instance = null;

            try
            {
                instance = Activator.CreateInstance(typeof(N), buff, offset) as N;
            }
            catch (Exception exception)
            {
                ParseException = exception;
            }

            return instance;
        }
    }
    //// End class
}
//// End namespace