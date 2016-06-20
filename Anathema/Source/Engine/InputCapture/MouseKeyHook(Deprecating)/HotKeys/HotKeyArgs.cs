using System;

namespace Anathema.Source.Engine.InputCapture.MouseKeyHook.HotKeys
{
    /// <summary>
    ///     The event arguments passed when a HotKeySet's OnHotKeysDownHold event is triggered.
    /// </summary>
    public sealed class HotKeyArgs : EventArgs
    {
        private readonly DateTime MTimeOfExecution;

        /// <summary>
        ///     Creates an instance of the HotKeyArgs.
        ///     <param name="TriggeredAt">Time when the event was triggered</param>
        /// </summary>
        public HotKeyArgs(DateTime TriggeredAt)
        {
            MTimeOfExecution = TriggeredAt;
        }

        /// <summary>
        ///     Time when the event was triggered
        /// </summary>
        public DateTime Time
        {
            get { return MTimeOfExecution; }
        }

    } // End class

} // End namespace