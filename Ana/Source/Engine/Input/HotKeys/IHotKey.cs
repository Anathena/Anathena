namespace Ana.Source.Engine.Input.HotKeys
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// An interface defining a hotkey, which is activated by a given set of input
    /// </summary>
    [KnownType(typeof(ControllerHotkey))]
    [KnownType(typeof(KeyboardHotkey))]
    [KnownType(typeof(MouseHotkey))]
    [DataContract]
    internal abstract class IHotkey
    {
        /// <summary>
        /// Determines if the current set of activation hotkeys are empty.
        /// </summary>
        /// <returns>True if there are hotkeys, otherwise false.</returns>
        public abstract Boolean HasHotkey();

        /// <summary>
        /// Clones the hotkey.
        /// </summary>
        /// <returns>A clone of the hotkey.</returns>
        public abstract IHotkey Clone();
    }
    //// End interface
}
//// End namespace