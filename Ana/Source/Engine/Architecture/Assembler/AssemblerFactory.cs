namespace Ana.Source.Engine.Architecture.Assembler
{
    class AssemblerFactory
    {
        public static IAssembler GetAssembler()
        {
            return new Fasm32Assembler();
        }
    }
    //// End class
}
//// End namespace