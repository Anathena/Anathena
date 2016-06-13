using System;

namespace DirectXShell.Hook.Common
{
    public class FramesPerSecond : TextElement
    {
        private String FPSFormat = "{0:N0} fps";
        public override String Text
        {
            get
            {
                return String.Format(FPSFormat, GetFPS());
            }
            set
            {
                FPSFormat = value;
            }
        }

        private Int32 Frames;
        private Int32 LastTickCount;
        private Single LastFrameRate;

        public FramesPerSecond(System.Drawing.Font font) : base(font)
        {
            Frames = 0;
            LastTickCount = 0;
            LastFrameRate = 0;
        }

        /// <summary>
        /// Must be called each frame
        /// </summary>
        public override void Frame()
        {
            Frames++;

            if (Math.Abs(Environment.TickCount - LastTickCount) > 1000)
            {
                LastFrameRate = (Single)Frames * 1000 / Math.Abs(Environment.TickCount - LastTickCount);
                LastTickCount = Environment.TickCount;
                Frames = 0;
            }
        }

        /// <summary>
        /// Return the current frames per second
        /// </summary>
        /// <returns></returns>
        public float GetFPS()
        {
            return LastFrameRate;
        }

    } // End class

} // End namespace