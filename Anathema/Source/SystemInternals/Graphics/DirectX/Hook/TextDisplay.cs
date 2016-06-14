using System;

namespace Anathema.Source.SystemInternals.Graphics.DirectXHook.Hook
{
    public class TextDisplay
    {
        private Int64 StartTickCount = 0;
        public bool Display { get; set; }
        public String Text { get; set; }
        public TimeSpan Duration { get; set; }
        public float Remaining { get { return Display ? (float)Math.Abs(DateTime.Now.Ticks - StartTickCount) / (float)Duration.Ticks : 0; } }

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
            {
                Display = false;
            }
        }

    } // End class

} // End namespace