using Anathema.Source.SystemInternals.InputCapture.MouseKeyHook.Implementation;

namespace Anathema.Source.SystemInternals.InputCapture.MouseKeyHook
{
    /// <summary>
    /// This is the class to start with.
    /// </summary>
    public static class MouseKeyCapture
    {
        /// <summary>
        /// Here you find all application wide events. Both mouse and keyboard.
        /// </summary>
        /// <returns>
        /// Returned instance is used for event subscriptions.
        /// You can refetch it (you will get the same instance anyway).
        /// </returns>
        public static IKeyboardMouseEvents AppEvents()
        {
            return new AppEventFacade();
        }

        /// <summary>
        /// Here you find all application wide events. Both mouse and keyboard.
        /// </summary>
        /// <returns>
        /// Returned instance is used for event subscriptions. You can refetch it (you will get the same instance anyway).
        /// </returns>
        public static IKeyboardMouseEvents GlobalEvents()
        {
            return new GlobalEventFacade();
        }

    } // End class

} // End namespace