using System;

namespace ScintillaNET
{
    /// <summary>
    /// Specifies the change that triggered a <see cref="Scintilla.UpdateUI" /> event.
    /// </summary>
    /// <remarks>This enumeration has a FlagsAttribute attribute that allows a bitwise combination of its member values.</remarks>
    [Flags]
    public enum UpdateChange
    {
        /// <summary>
        /// Contents, styling or markers have been changed.
        /// </summary>
        Content = NativeMethods.SC_UPDATE_CONTENT,

        /// <summary>
        /// Selection has been changed.
        /// </summary>
        Selection = NativeMethods.SC_UPDATE_SELECTION,

        /// <summary>
        /// Scrolled vertically.
        /// </summary>
        VScroll = NativeMethods.SC_UPDATE_V_SCROLL,

        /// <summary>
        /// Scrolled horizontally.
        /// </summary>
        HScroll = NativeMethods.SC_UPDATE_H_SCROLL
    }
}
