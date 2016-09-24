using Ana.Source.Engine.SpeedManipulator;
using System;

namespace Ana.Source.LuaEngine.Hook
{
    internal class LuaHookCore : IHookCore
    {
        private Object AccessLock;

        public LuaHookCore()
        {
            AccessLock = new Object();
        }

        /// <summary>
        /// Gives access to the speed hook in the target process, injecting the hooks if needed
        /// </summary>
        /// <returns></returns>
        private ISpeedManipulator GetSpeedHackInterface()
        {
            return null;
        }

        public void SetSpeed(Double Speed)
        {

        }

        public void SetRandomSeed(UInt32 Seed)
        {
            throw new NotImplementedException();
        }

    } // End class

} // End namespace