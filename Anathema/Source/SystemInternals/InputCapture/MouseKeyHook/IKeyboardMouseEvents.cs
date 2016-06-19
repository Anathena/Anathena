using System;

namespace Anathema.Source.SystemInternals.InputCapture.MouseKeyHook
{
    /// <summary>
    /// Provides keyboard and mouse events.
    /// </summary>
    public interface IKeyboardMouseEvents : IKeyboardEvents, IMouseEvents, IDisposable
    {

    } // End interface

} // End namespace