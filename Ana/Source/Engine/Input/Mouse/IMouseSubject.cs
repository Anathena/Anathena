namespace Ana.Source.Engine.Input.Mouse
{
    internal interface IMouseSubject : IInputCapture
    {
        void Subscribe(IMouseObserver subject);

        void Unsubscribe(IMouseObserver subject);
    }
    //// End interface
}
//// End namespace