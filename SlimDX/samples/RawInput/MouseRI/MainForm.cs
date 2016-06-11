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
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using SlimDX;
using SlimDX.RawInput;
using SlimDX.Multimedia;

namespace Mouse
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // setup the device
            Device.RegisterDevice(UsagePage.Generic, UsageId.Mouse, DeviceFlags.None);
            Device.MouseInput += mouse_MouseInput;
        }

        void mouse_MouseInput(object sender, MouseInputEventArgs e)
        {
            buttonsLabel.Text = Enum.Format(typeof(MouseButtonFlags), e.ButtonFlags, "G");
            extraLabel.Text = e.ExtraInformation.ToString(CultureInfo.CurrentCulture);
            rawButtonsLabel.Text = e.RawButtons.ToString(CultureInfo.CurrentCulture);
            locationLabel.Text = new Point(e.X, e.Y).ToString();
            stateLabel.Text = Enum.Format(typeof(MouseMode), e.Mode, "G");
            wheelLabel.Text = e.WheelDelta.ToString(CultureInfo.CurrentCulture);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
