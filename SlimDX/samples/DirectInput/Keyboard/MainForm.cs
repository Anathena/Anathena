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
using System.Text;
using System.Windows.Forms;
using SlimDX;
using SlimDX.DirectInput;
using System.Collections.Generic;

namespace KeyboardTest
{
    public partial class MainForm : Form
    {
        Keyboard keyboard;

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

            if (disableCheck.Checked)
                cooperativeLevel |= CooperativeLevel.NoWinKey;

            // create the device
            try
            {
                keyboard = new Keyboard(dinput);
                keyboard.SetCooperativeLevel(this, cooperativeLevel);
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
                keyboard.Properties.BufferSize = 8;
            }

            // acquire the device
            keyboard.Acquire();

            // set the timer to go off 12 times a second to read input
            // NOTE: Normally applications would read this much faster.
            // This rate is for demonstration purposes only.
            timer.Interval = 1000 / 12;
            timer.Start();
        }

        void ReadImmediateData()
        {
            if (keyboard.Acquire().IsFailure)
                return;

            if (keyboard.Poll().IsFailure)
                return;

            KeyboardState state = keyboard.GetCurrentState();
            if (Result.Last.IsFailure)
                return;

            StringBuilder data = new StringBuilder();
            foreach (Key key in state.PressedKeys)
                data.Append(Enum.GetName(typeof(Key), key) + " ");

            dataBox.Text = data.ToString();
        }

        void ReadBufferedData()
        {
            if (keyboard.Acquire().IsFailure)
                return;

            if (keyboard.Poll().IsFailure)
                return;

            IEnumerable<KeyboardState> bufferedData = keyboard.GetBufferedData();
            if (Result.Last.IsFailure)
                return;

            StringBuilder data = new StringBuilder();
            foreach (KeyboardState packet in bufferedData)
            {
                foreach (Key key in packet.PressedKeys)
                    data.Append(Enum.GetName(typeof(Key), key) + " ");
            }

            dataBox.Text = data.ToString();
        }

        void ReleaseDevice()
        {
            timer.Stop();

            if (keyboard != null)
            {
                keyboard.Unacquire();
                keyboard.Dispose();
            }
            keyboard = null;
        }

        #region Boilerplate

        public MainForm()
        {
            InitializeComponent();
            UpdateUI();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            if (keyboard == null)
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

        private void disableCheck_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUI();
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
            if (keyboard != null)
            {
                createButton.Text = "Release Device";
                dataBox.Text = "";

                exclusiveRadio.Enabled = false;
                nonexclusiveRadio.Enabled = false;
                foregroundRadio.Enabled = false;
                backgroundRadio.Enabled = false;
                disableCheck.Enabled = false;
                immediateRadio.Enabled = false;
                bufferedRadio.Enabled = false;
            }
            else
            {
                createButton.Text = "Create Device";
                dataBox.Text = "Device not created. Choose settings and click 'Create Device' then type to see results";

                exclusiveRadio.Enabled = true;
                nonexclusiveRadio.Enabled = true;
                foregroundRadio.Enabled = true;
                backgroundRadio.Enabled = true;
                immediateRadio.Enabled = true;
                bufferedRadio.Enabled = true;

                if (!exclusiveRadio.Checked && foregroundRadio.Checked)
                    disableCheck.Enabled = true;
                else
                    disableCheck.Enabled = false;
            }

            StringBuilder text = new StringBuilder();
            if (!foregroundRadio.Checked && exclusiveRadio.Checked)
            {
                text.Append("For security reasons, background exclusive keyboard access ");
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
                    text.Append("application cannot get data from the device. When an application ");
                    text.Append("has exclusive access to the keyboard, DirectInput suppresses all ");
                    text.Append("messages including the Windows key except CTRL+ALT+DEL and ALT+TAB");
                    text.AppendLine();
                    text.AppendLine();
                }
                else
                {
                    text.Append("Nonexclusive mode means that other applications can acquire ");
                    text.Append("the device in exclusive or nonexclusive mode.");

                    if (disableCheck.Checked)
                    {
                        text.Append("The Windows key will also be disabled so that users cannot ");
                        text.Append("inadvertently break out of the application.");
                    }

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

                text.Append("The sample will read the keyboard 12 times a second. Typically ");
                text.Append("an application would poll the keyboard much faster than this, ");
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