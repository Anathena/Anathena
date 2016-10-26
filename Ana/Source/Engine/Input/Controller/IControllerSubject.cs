namespace Ana.Source.Engine.Input.Controller
{
    internal interface IControllerSubject : IInputCapture
    {
        void Subscribe(IControllerObserver subject);

        void Unsubscribe(IControllerObserver subject);
    }
    //// End interface
}
//// End namespace