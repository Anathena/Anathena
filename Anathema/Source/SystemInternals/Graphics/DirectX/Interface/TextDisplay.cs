using System;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Interface
{
    public class TextDisplay
    {
        private Int64 StartTickCount = 0;
        public Boolean Display { get; set; }
        public String Text { get; set; }
        public TimeSpan Duration { get; set; }
        public Single Remaining { get { return Display ? (Single)Math.Abs(DateTime.Now.Ticks - StartTickCount) / (Single)Duration.Ticks : 0; } }

        public TextDisplay()
        {
            StartTickCount = DateTime.Now.Ticks;
            Display = true;
        }

        /// <summary>
        /// Must be called each frame
        /// </summary>
        public void Frame()
        {
            if (Display && Math.Abs(DateTime.Now.Ticks - StartTickCount) > Duration.Ticks)
                Display = false;
        }

    } // End class

} // End namespace