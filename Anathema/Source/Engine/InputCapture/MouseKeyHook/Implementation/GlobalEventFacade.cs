namespace Anathema.Source.Engine.InputCapture.MouseKeyHook.Implementation
{
    internal class GlobalEventFacade : EventFacade
    {
        protected override MouseListener CreateMouseListener()
        {
            return new GlobalMouseListener();
        }

        protected override KeyListener CreateKeyListener()
        {
            return new GlobalKeyListener();
        }

    } // End class

} // End namespace