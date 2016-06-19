namespace Anathema.Source.SystemInternals.InputCapture.MouseKeyHook.Implementation
{
    internal class AppEventFacade : EventFacade
    {
        protected override MouseListener CreateMouseListener()
        {
            return new AppMouseListener();
        }

        protected override KeyListener CreateKeyListener()
        {
            return new AppKeyListener();
        }

    } // End class

} // End namespace