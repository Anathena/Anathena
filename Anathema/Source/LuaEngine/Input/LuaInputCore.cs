using Anathema.Source.Engine;
using Anathema.Source.Engine.Processes;

namespace Anathema.Source.LuaEngine.Input
{
    class LuaInputCore : IInputCore, IProcessObserver
    {
        private EngineCore Engine;

        public LuaInputCore()
        {
            InitializeProcessObserver();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore Engine)
        {
            this.Engine = Engine;
        }

    } // End class

} // End namespace