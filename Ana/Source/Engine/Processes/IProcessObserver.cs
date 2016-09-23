namespace Ana.Source.Engine.Processes
{
    internal interface IProcessObserver
    {
        void InitializeProcessObserver();
        void UpdateEngineCore(EngineCore EngineCore);

    } // End interface

} // End namespace