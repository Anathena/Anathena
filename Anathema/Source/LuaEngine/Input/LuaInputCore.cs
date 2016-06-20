using Anathema.Source.Engine;
using Anathema.Source.Engine.Processes;

namespace Anathema.Source.LuaEngine.Input
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