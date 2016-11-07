namespace Ana.Source.Engine.Hook.Unrandomizer
{
    using System;

    internal interface IUnrandomizerInterface
    {
        void SetSeed(UInt32 seed);

        void Disconnect();
    }
    //// End interface
}
//// End namespace