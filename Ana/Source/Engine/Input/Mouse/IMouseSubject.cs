namespace Ana.Source.Engine.Input.Mouse
{
    /// <summary>
    /// Interface for an object which will capture mouse input
    /// </summary>
    internal interface IMouseSubject : IInputCapture
    {
        void Subscribe(IMouseObserver subject);

        void Unsubscribe(IMouseObserver subject);
    }
    //// End interface
}
//// End namespace