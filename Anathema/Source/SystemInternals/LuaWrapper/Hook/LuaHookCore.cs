using Anathema.Source.SystemInternals.OperatingSystems;
using Anathema.Source.SystemInternals.Processes;
using Anathema.Source.SystemInternals.SpeedHack;
using System;

namespace Anathema.Source.SystemInternals.LuaWrapper.Hook
{
    class LuaHookCore : IHookCore, IProcessObserver
    {
        private Engine Engine;
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

        public void UpdateEngine(Engine Engine)
        {
            this.Engine = Engine;
        }

        /// <summary>
        /// Gives access to the speed hook in the target process, injecting the hooks if needed
        /// </summary>
        /// <returns></returns>
        private ISpeedHackInterface GetSpeedHackInterface()
        {
            lock (AccessLock)
            {
                if (Engine.HookCreator.GetSpeedHackInterface() == null)
                    Engine.HookCreator.Inject(Engine.Memory.GetProcess());

                return Engine.HookCreator.GetSpeedHackInterface();
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