/*
* Copyright (c) 2007-2009 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/
using System.Runtime.InteropServices;
using System.Threading;
using SlimDX;
using SlimDX.XAudio2;
using SlimDX.Multimedia;

namespace BasicSound
{
    class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern short GetAsyncKeyState(int key);
        const int VK_ESCAPE = 0x1B;

        static void Main()
        {
            XAudio2 device = new XAudio2();

            MasteringVoice masteringVoice = new MasteringVoice(device);

            // play a PCM file
            PlayPCM(device, "MusicMono.wav");

            // play a 5.1 PCM wave extensible file
            PlayPCM(device, "MusicSurround.wav");

            masteringVoice.Dispose();
            device.Dispose();
        }

        static void PlayPCM(XAudio2 device, string fileName)
        {
            //WaveStream stream = new WaveStream(fileName);
            var s = System.IO.File.OpenRead(fileName);
            WaveStream stream = new WaveStream(s);
            s.Close();

            AudioBuffer buffer = new AudioBuffer();
            buffer.AudioData = stream;
            buffer.AudioBytes = (int)stream.Length;
            buffer.Flags = BufferFlags.EndOfStream;

            SourceVoice sourceVoice = new SourceVoice(device, stream.Format);
            sourceVoice.SubmitSourceBuffer(buffer);
            sourceVoice.Start();

            // loop until the sound is done playing
            while (sourceVoice.State.BuffersQueued > 0)
            {
                if (GetAsyncKeyState(VK_ESCAPE) != 0)
                    break;

                Thread.Sleep(10);
            }

            // wait until the escape key is released
            while (GetAsyncKeyState(VK_ESCAPE) != 0)
                Thread.Sleep(10);

            // cleanup the voice
            buffer.Dispose();
            sourceVoice.Dispose();
            stream.Dispose();
        }
    }
}
