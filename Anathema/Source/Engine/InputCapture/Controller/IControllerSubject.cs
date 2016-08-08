namespace Anathema.Source.Engine.InputCapture.Controller
{
    public interface IControllerSubject : IInputCapture
    {
        void Subscribe(IControllerObserver Subject);

        void Unsubscribe(IControllerObserver Subject);

    } // End interface

} // End namespace