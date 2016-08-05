using SharpDX.DirectInput;

namespace Anathema.Source.Engine.InputCapture
{
    /// <summary>
    /// Defines a keyboard, controller, or mouse binding
    /// </summary>
    public class InputBinding
    {
        private Key HotKey;

        public InputBinding()
        {

        }

        // TODO: Maybe subscribe to needed input methods, check for OnKey(whatever), and then
        // - Callback?
        // - Notify?
        // - Raise event?
        // How am I supposed to bind this to a fucking thing
        // How the fuck will this interface with LUA
        // Why

    } // End class

} // End namespace