namespace Ana.Source.Engine.InputCapture.Mouse
{
    internal interface IMouseSubject : IInputCapture
    {
        void Subscribe(IMouseObserver subject);

        void Unsubscribe(IMouseObserver subject);
    }
    //// End interface
}
//// End namespace