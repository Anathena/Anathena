namespace Ana.Source.Engine.Input.Controller
{
    /// <summary>
    /// Interface for an object which will capture controller input
    /// </summary>
    internal interface IControllerSubject : IInputCapture
    {
        void Subscribe(IControllerObserver subject);

        void Unsubscribe(IControllerObserver subject);
    }
    //// End interface
}
//// End namespace