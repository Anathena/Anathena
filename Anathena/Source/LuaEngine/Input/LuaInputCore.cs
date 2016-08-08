using Anathena.Source.Engine;
using Anathena.Source.Engine.Processes;

namespace Anathena.Source.LuaEngine.Input
{
    class LuaInputCore : IInputCore, IProcessObserver
    {
        private EngineCore EngineCore;

        public LuaInputCore()
        {
            InitializeProcessObserver();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;
        }

    } // End class

} // End namespace