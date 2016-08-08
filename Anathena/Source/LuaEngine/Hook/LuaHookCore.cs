using Anathena.Source.Engine;
using Anathena.Source.Engine.Hook.SpeedHack;
using Anathena.Source.Engine.Processes;
using System;

namespace Anathena.Source.LuaEngine.Hook
{
    class LuaHookCore : IHookCore, IProcessObserver
    {
        private EngineCore EngineCore;
        private Object AccessLock;

        public LuaHookCore()
        {
            InitializeProcessObserver();

            AccessLock = new Object();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;
        }

        /// <summary>
        /// Gives access to the speed hook in the target process, injecting the hooks if needed
        /// </summary>
        /// <returns></returns>
        private ISpeedHackInterface GetSpeedHackInterface()
        {
            lock (AccessLock)
            {
                if (EngineCore.HookCreator.GetSpeedHackInterface() == null)
                    EngineCore.HookCreator.Inject(EngineCore.Memory.GetProcess());

                return EngineCore.HookCreator.GetSpeedHackInterface();
            }
        }

        public void SetSpeed(Double Speed)
        {
            GetSpeedHackInterface().SetSpeed(Speed);
        }

        public void SetRandomSeed(UInt32 Seed)
        {
            throw new NotImplementedException();
        }

    } // End class

} // End namespace