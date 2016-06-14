using System;

namespace DirectXHook.Hook
{
    /// <summary>
    /// Used to determine the FPS
    /// </summary>
    public class FramesPerSecond
    {
        Int32 Frames = 0;
        Int32 LastTickCount = 0;
        Single LastFrameRate = 0;

        /// <summary>
        /// Must be called each frame
        /// </summary>
        public void Frame()
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
        public Single GetFPS()
        {
            return LastFrameRate;
        }

    } // End class

} // End namespace