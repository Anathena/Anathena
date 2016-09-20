namespace Ana.Source.Engine.InputCapture.Mouse
{
    public interface IMouseSubject : IInputCapture
    {
        void Subscribe(IMouseObserver Subject);

        void Unsubscribe(IMouseObserver Subject);

    } // End interface

} // End namespace