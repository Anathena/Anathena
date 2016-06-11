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
using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using SlimDX;
using SlimDX.DirectInput;
using System.Collections.Generic;

namespace MouseTest
{
    public partial class MainForm : Form
    {
        Mouse mouse;

        void CreateDevice()
        {
            // make sure that DirectInput has been initialized
            DirectInput dinput = new DirectInput();

            // build up cooperative flags
            CooperativeLevel cooperativeLevel;

            if (exclusiveRadio.Checked)
                cooperativeLevel = CooperativeLevel.Exclusive;
            else
                cooperativeLevel = CooperativeLevel.Nonexclusive;

            if (foregroundRadio.Checked)
                cooperativeLevel |= CooperativeLevel.Foreground;
            else
                cooperativeLevel |= CooperativeLevel.Background;

            // create the device
            try
            {
                mouse = new Mouse(dinput);
                mouse.SetCooperativeLevel(this, cooperativeLevel);
            }
            catch (DirectInputException e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            if (!immediateRadio.Checked)
            {
                // since we want to use buffered data, we need to tell DirectInput
                // to set up a buffer for the data
                mouse.Properties.BufferSize = 8;
            }

            // acquire the device
            mouse.Acquire();

            // set the timer to go off 12 times a second to read input
            // NOTE: Normally applications would read this much faster.
            // This rate is for demonstration purposes only.
            timer.Interval = 1000 / 12;
            timer.Start();
        }

        void ReadImmediateData()
        {
            if (mouse.Acquire().IsFailure)
                return;

            if (mouse.Poll().IsFailure)
                return;

            MouseState state = mouse.GetCurrentState();
            if (Result.Last.IsFailure)
                return;

            StringBuilder data = new StringBuilder();
            data.AppendFormat(CultureInfo.CurrentCulture, "(X={0} Y={1} Z={2})", state.X, state.Y, state.Z);
            for (int i = 0; i < 8; i++)
            {
                data.Append(" B");
                data.Append(i);
                data.Append("=");
                if (state.IsPressed(i))
                    data.Append("1");
                else
                    data.Append("0");
            }

            dataBox.Text = data.ToString();
        }

        void ReadBufferedData()
        {
            if (mouse.Acquire().IsFailure)
                return;

            if (mouse.Poll().IsFailure)
                return;

            IList<MouseState> bufferedData = mouse.GetBufferedData();
            if (Result.Last.IsFailure || bufferedData.Count == 0)
                return;

            StringBuilder data = new StringBuilder();

            MouseState result = new MouseState();
            foreach (MouseState packet in bufferedData)
            {
                result.X += packet.X;
                result.Y += packet.Y;
                result.Z += packet.Z;
            }

            data.AppendFormat(CultureInfo.CurrentCulture, "(X={0} Y={1} Z={2})", result.X, result.Y, result.Z);
            for (int i = 0; i < 8; i++)
            {
                data.Append(" B");
                data.Append(i);
                data.Append("=");
                if (bufferedData[bufferedData.Count - 1].IsPressed(i))
                    data.Append("1");
                else
                    data.Append("0");
            }

            dataBox.Text = data.ToString();
        }

        void ReleaseDevice()
        {
            timer.Stop();

            if (mouse != null)
            {
                mouse.Unacquire();
                mouse.Dispose();
            }
            mouse = null;
        }

        #region Boilerplate

        public MainForm()
        {
            InitializeComponent();
            UpdateUI();
        }

        private void exclusiveRadio_Click(object sender, EventArgs e)
        {
            exclusiveRadio.Checked = true;
            nonexclusiveRadio.Checked = false;
            UpdateUI();
        }

        private void foregroundRadio_Click(object sender, EventArgs e)
        {
            foregroundRadio.Checked = true;
            backgroundRadio.Checked = false;
            UpdateUI();
        }

        private void nonexclusiveRadio_Click(object sender, EventArgs e)
        {
            nonexclusiveRadio.Checked = true;
            exclusiveRadio.Checked = false;
            UpdateUI();
        }

        private void backgroundRadio_Click(object sender, EventArgs e)
        {
            backgroundRadio.Checked = true;
            foregroundRadio.Checked = false;
            UpdateUI();
        }

        private void immediateRadio_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void bufferedRadio_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            if (mouse == null)
                CreateDevice();
            else
                ReleaseDevice();
            UpdateUI();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            ReleaseDevice();
            Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (immediateRadio.Checked)
                ReadImmediateData();
            else
                ReadBufferedData();
        }

        void UpdateUI()
        {
            if (mouse != null)
            {
                createButton.Text = "Release Device";
                dataBox.Text = "";

                exclusiveRadio.Enabled = false;
                nonexclusiveRadio.Enabled = false;
                foregroundRadio.Enabled = false;
                backgroundRadio.Enabled = false;
                immediateRadio.Enabled = false;
                bufferedRadio.Enabled = false;

                if (exclusiveRadio.Checked)
                    helpLabel.Text = "Press Enter to release the mouse device and display the cursor again.";
            }
            else
            {
                createButton.Text = "Create Device";
                dataBox.Text = "Device not created. Choose settings and click 'Create Device' then type to see results";
                helpLabel.Text = "";

                exclusiveRadio.Enabled = true;
                nonexclusiveRadio.Enabled = true;
                foregroundRadio.Enabled = true;
                backgroundRadio.Enabled = true;
                immediateRadio.Enabled = true;
                bufferedRadio.Enabled = true;
            }

            StringBuilder text = new StringBuilder();
            if (!foregroundRadio.Checked && exclusiveRadio.Checked)
            {
                text.Append("For security reasons, background exclusive mouse access ");
                text.Append("is not allowed.");
                text.AppendLine();
                text.AppendLine();
            }
            else
            {
                if (foregroundRadio.Checked)
                {
                    text.Append("Foreground cooperative level means that the ");
                    text.Append("application has access to data only when in the ");
                    text.Append("foreground or, in other words, has the input focus. ");
                    text.Append("If the application moves to the background, the device ");
                    text.Append("is automatically unacquired, or made unavailable.");
                    text.AppendLine();
                    text.AppendLine();
                }
                else
                {
                    text.Append("Background cooperative level really mean foreground and ");
                    text.Append("background. A device with a background cooperative level ");
                    text.Append("can be acquired and used by an application at any time.");
                    text.AppendLine();
                    text.AppendLine();
                }

                if (exclusiveRadio.Checked)
                {
                    text.Append("Exclusive mode prevents other application from also acquiring ");
                    text.Append("the device exclusively. The fact that your application is using ");
                    text.Append("a device at the exclusive level does not mean that other ");
                    text.Append("application cannot get data from the device. Windows itself ");
                    text.Append("requires exclusive access to the mouse because mouse events such ");
                    text.Append("as clicking on an inactive window could force an application to ");
                    text.Append("unacquire the device, with potentially harmful results, such as ");
                    text.Append("a loss of data from the input buffer. Therefore, when an application ");
                    text.Append("has exclusive access to the mouse, Windows is not allowed any access ");
                    text.Append("at all. No mouse messages are generated. A further side effect is ");
                    text.Append("that the cursor disappears.");
                    text.AppendLine();
                    text.AppendLine();
                }
                else
                {
                    text.Append("Nonexclusive mode means that other applications can acquire ");
                    text.Append("the device in exclusive or nonexclusive mode.");
                    text.AppendLine();
                    text.AppendLine();
                }

                if (immediateRadio.Checked)
                {
                    text.Append("Immediate data is a snapshot of the current state of a device. ");
                    text.Append("It provides no data about what has happened with the device since ");
                    text.Append("the last call, apart from implicit information that you can derive ");
                    text.Append("by comparing the current state with the last one. ");
                    text.Append("Events in between calls are lost.");

                    text.AppendLine();
                    text.AppendLine();
                }
                else
                {
                    text.Append("Buffered data is a record of events that are stored until an ");
                    text.Append("application retrieves them. With buffered data, events are ");
                    text.Append("stored until you are ready to deal with them. If the ");
                    text.Append("buffer overflows, new data is lost.");

                    text.AppendLine();
                    text.AppendLine();
                }

                text.Append("The sample will read the mouse 12 times a second. Typically ");
                text.Append("an application would poll the mouse much faster than this, ");
                text.Append("but this slow rate is simply for the purposes of demonstration.");
            }

            behaviorLabel.Text = text.ToString();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ReleaseDevice();
        }

        #endregion
    }
}