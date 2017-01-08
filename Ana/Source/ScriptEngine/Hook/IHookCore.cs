namespace Ana.Source.ScriptEngine.Hook
{
    using System;

    /// <summary>
    /// Interface for environment manipulations in a hooked process.
    /// </summary>
    public interface IHookCore
    {
        void SetSpeed(Double speed);

        void SetRandomSeed(UInt32 seed);
    }
    //// End interface
}
//// End namespace