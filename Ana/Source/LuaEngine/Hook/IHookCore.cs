namespace Ana.Source.LuaEngine.Hook
{
    using System;

    /// <summary>
    /// Interface for environment manipulations in a hooked process
    /// </summary>
    internal interface IHookCore
    {
        void SetSpeed(Double speed);

        void SetRandomSeed(UInt32 seed);
    }
    //// End interface
}
//// End namespace