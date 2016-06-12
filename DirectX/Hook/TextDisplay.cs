using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capture.Hook
{
    public class TextDisplay
    {
        long _startTickCount = 0;

        public TextDisplay()
        {
            _startTickCount = DateTime.Now.Ticks;
            Display = true;
        }

        /// <summary>
        /// Must be called each frame
        /// </summary>
        public void Frame()
        {
            if (Display && Math.Abs(DateTime.Now.Ticks - _startTickCount) > Duration.Ticks)
            {
                Display = false;
            }
        }

        public bool Display { get; set; }
        public String Text { get; set; }
        public TimeSpan Duration { get; set; }
        public float Remaining
        {
            get
            {
                if (Display)
                {
                    return (float)Math.Abs(DateTime.Now.Ticks - _startTickCount) / (float)Duration.Ticks;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
