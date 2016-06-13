using Anathema.Source.Graphics;
using Anathema.Source.SystemInternals.Graphics;
using Anathema.Source.SystemInternals.OperatingSystems;
using Anathema.Source.SystemInternals.Processes;
using Anathema.Source.Utils.Extensions;

namespace Anathema.Source.SystemInternals.LuaWrapper.Graphics
{
    class LuaGraphicsCore : IGraphicsCore, IProcessObserver
    {
        private Engine Engine;
        private IGraphicsInterface GraphicsInterface;

        public LuaGraphicsCore()
        {
            InitializeProcessObserver();

            GraphicsInterface = GraphicsFactory.GetGraphicsInterface();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngine(Engine Engine)
        {
            this.Engine = Engine;
        }

        public void Inject()
        {
            this.PrintDebugTag();

            GraphicsInterface.Inject(Engine.Memory.GetProcess());
        }

        public void Uninject()
        {
            this.PrintDebugTag();

            GraphicsInterface.Uninject();
        }

        public void DoRequest()
        {
            this.PrintDebugTag();

            GraphicsInterface.DoRequest();
        }

    } // End class

} // End namespace