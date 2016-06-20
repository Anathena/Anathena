using Anathema.Source.Engine.OperatingSystems;

namespace Anathema.Source.Engine.Processes
{
    interface IProcessObserver
    {
        void InitializeProcessObserver();
        void UpdateEngineCore(EngineCore Engine);

    } // End interface

} // End namespace