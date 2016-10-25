namespace Ana.Source.Engine.InputCapture.Controller
{
    internal interface IControllerSubject : IInputCapture
    {
        void Subscribe(IControllerObserver subject);

        void Unsubscribe(IControllerObserver subject);
    }
    //// End interface
}
//// End namespace