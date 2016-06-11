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
using System.Windows.Forms;
using SlimDX;
using SlimDX.DirectInput;
using System.Collections.Generic;

namespace JoystickTest
{
    public partial class MainForm : Form
    {
        Joystick joystick;

        JoystickState state = new JoystickState();
        int numPOVs;
        int SliderCount;

        void CreateDevice()
        {
            // make sure that DirectInput has been initialized
            DirectInput dinput = new DirectInput();

            // search for devices
            foreach (DeviceInstance device in dinput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
            {
                // create the device
                try
                {
                    joystick = new Joystick(dinput, device.InstanceGuid);
                    joystick.SetCooperativeLevel(this, CooperativeLevel.Exclusive | CooperativeLevel.Foreground);
                    break;
                }
                catch (DirectInputException)
                {
                }
            }

            if (joystick == null)
            {
                MessageBox.Show("There are no joysticks attached to the system.");
                return;
            }

            foreach (DeviceObjectInstance deviceObject in joystick.GetObjects())
            {
                if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
                    joystick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-1000, 1000);

                UpdateControl(deviceObject);
            }

            // acquire the device
            joystick.Acquire();

            // set the timer to go off 12 times a second to read input
            // NOTE: Normally applications would read this much faster.
            // This rate is for demonstration purposes only.
            timer.Interval = 1000 / 12;
            timer.Start();
        }

        void ReadImmediateData()
        {
            if (joystick.Acquire().IsFailure)
                return;

            if (joystick.Poll().IsFailure)
                return;

            state = joystick.GetCurrentState();
            if (Result.Last.IsFailure)
                return;

            UpdateUI();
        }

        void ReleaseDevice()
        {
            timer.Stop();

            if (joystick != null)
            {
                joystick.Unacquire();
                joystick.Dispose();
            }
            joystick = null;
        }

        #region Boilerplate

        public MainForm()
        {
            InitializeComponent();
            UpdateUI();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ReadImmediateData();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            ReleaseDevice();
            Close();
        }

        void UpdateUI()
        {
            if (joystick == null)
                createDeviceButton.Text = "Create Device";
            else
                createDeviceButton.Text = "Release Device";

            string strText = null;

            label_X.Text = state.X.ToString(CultureInfo.CurrentCulture);
            label_Y.Text = state.Y.ToString(CultureInfo.CurrentCulture);
            label_Z.Text = state.Z.ToString(CultureInfo.CurrentCulture);

            label_XRot.Text = state.RotationX.ToString(CultureInfo.CurrentCulture);
            label_YRot.Text = state.RotationY.ToString(CultureInfo.CurrentCulture);
            label_ZRot.Text = state.RotationZ.ToString(CultureInfo.CurrentCulture);

            int[] slider = state.GetSliders();

            label_S0.Text = slider[0].ToString(CultureInfo.CurrentCulture);
            label_S1.Text = slider[1].ToString(CultureInfo.CurrentCulture);

            int[] pov = state.GetPointOfViewControllers();

            label_P0.Text = pov[0].ToString(CultureInfo.CurrentCulture);
            label_P1.Text = pov[1].ToString(CultureInfo.CurrentCulture);
            label_P2.Text = pov[2].ToString(CultureInfo.CurrentCulture);
            label_P3.Text = pov[3].ToString(CultureInfo.CurrentCulture);

            bool[] buttons = state.GetButtons();

            for (int b = 0; b < buttons.Length; b++)
            {
                if (buttons[b])
                    strText += b.ToString("00 ", CultureInfo.CurrentCulture);
            }
            label_ButtonList.Text = strText;
        }

        void UpdateControl(DeviceObjectInstance d)
        {
            if (ObjectGuid.XAxis == d.ObjectTypeGuid)
            {
                label_XAxis.Enabled = true;
                label_X.Enabled = true;
            }
            if (ObjectGuid.YAxis == d.ObjectTypeGuid)
            {
                label_YAxis.Enabled = true;
                label_Y.Enabled = true;
            }
            if (ObjectGuid.ZAxis == d.ObjectTypeGuid)
            {
                label_ZAxis.Enabled = true;
                label_Z.Enabled = true;
            }
            if (ObjectGuid.RotationalXAxis == d.ObjectTypeGuid)
            {
                label_XRotation.Enabled = true;
                label_XRot.Enabled = true;
            }
            if (ObjectGuid.RotationalYAxis == d.ObjectTypeGuid)
            {
                label_YRotation.Enabled = true;
                label_YRot.Enabled = true;
            }
            if (ObjectGuid.RotationalZAxis == d.ObjectTypeGuid)
            {
                label_ZRotation.Enabled = true;
                label_ZRot.Enabled = true;
            }

            if (ObjectGuid.Slider == d.ObjectTypeGuid)
            {
                switch (SliderCount++)
                {
                    case 0:
                        label_Slider0.Enabled = true;
                        label_S0.Enabled = true;
                        break;

                    case 1:
                        label_Slider1.Enabled = true;
                        label_S1.Enabled = true;
                        break;
                }
            }

            if (ObjectGuid.PovController == d.ObjectTypeGuid)
            {
                switch (numPOVs++)
                {
                    case 0:
                        label_POV0.Enabled = true;
                        label_P0.Enabled = true;
                        break;

                    case 1:
                        label_POV1.Enabled = true;
                        label_P1.Enabled = true;
                        break;

                    case 2:
                        label_POV2.Enabled = true;
                        label_P2.Enabled = true;
                        break;

                    case 3:
                        label_POV3.Enabled = true;
                        label_P3.Enabled = true;
                        break;
                }
            }
        }

        private void createDeviceButton_Click(object sender, EventArgs e)
        {
            if (joystick == null)
                CreateDevice();
            else
                ReleaseDevice();
            UpdateUI();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ReleaseDevice();
        }

        #endregion
    }
}