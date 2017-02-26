namespace Ana.Source.Engine.Input.HotKeys
{
    using System;

    /// <summary>
    /// An interface defining a hotkey, which is activated by a given set of input
    /// </summary>
    internal interface IHotkey
    {
        /// <summary>
        /// Determines if the current set of activation hotkeys are empty.
        /// </summary>
        /// <returns>True if there are hotkeys, otherwise false.</returns>
        Boolean HasHotkey();

        /// <summary>
        /// Clones the hotkey.
        /// </summary>
        /// <returns>A clone of the hotkey.</returns>
        IHotkey Clone();
    }
    //// End interface
}
//// End namespace