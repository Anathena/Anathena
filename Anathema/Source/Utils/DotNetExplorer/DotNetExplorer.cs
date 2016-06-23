using Anathema.Source.Engine;
using Anathema.Source.Engine.Processes;

namespace Anathema.Source.Utils.DotNetExplorer
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class DotNetExplorer : IDotNetExplorerModel, IProcessObserver
    {
        private EngineCore EngineCore;

        public DotNetExplorer()
        {
            InitializeProcessObserver();
            Begin();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateEngineCore(EngineCore EngineCore)
        {
            this.EngineCore = EngineCore;

            RefreshVirtualPages();
        }

        public override void RefreshVirtualPages()
        {
            if (EngineCore == null)
                return;

            DotNetExplorerEventArgs Args = new DotNetExplorerEventArgs();
            OnEventUpdateVirtualPages(Args);
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void Update()
        {
            base.Update();

            if (EngineCore == null)
                return;

        }

        protected override void End() { }

    } // End class

} // End namespace