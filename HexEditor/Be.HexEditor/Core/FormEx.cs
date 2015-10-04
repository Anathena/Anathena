using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Be.HexEditor.Core
{
    public partial class FormEx : Form
    {
        // DPI at design time
        public const float DpiAtDesign = 96F;

        // Old (previous) DPI
        float dpiOld = 0;
        [DefaultValue(0), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float DpiOld
        {
            get { return dpiOld; }
            set { dpiOld = value; }
        }

        // New (current) DPI
        float dpiNew = 0;
        [DefaultValue(0), DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden)]
        public float DpiNew
        {
            get { return dpiNew; }
            set { dpiNew = value; }
        }

        // Flag to set whether this window is being moved by user
        bool isBeingMoved = false;

        // Flag to set whether this window will be adjusted later
        bool willBeAdjusted = false;

        // Method for adjustment
        ResizeMethod method = ResizeMethod.Immediate;

        enum ResizeMethod
        {
            Immediate,
            Delayed
        }

        enum DelayedState
        {
            Initial,
            Waiting,
            Resized,
            Aborted
        }

        public FormEx()
        {
            //this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.Font = SystemFonts.MessageBoxFont;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResizeBegin += new System.EventHandler(this.MainForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.Move += new System.EventHandler(this.MainForm_Move);
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            if(!Util.DesignMode)
                AdjustWindowInitial();
        }

        // Adjust location, size and font size of Controls according to new DPI.
        void AdjustWindowInitial()
        {
            // Hold initial DPI used at loading this window.
            DpiOld = this.CurrentAutoScaleDimensions.Width;

            // Check current DPI.
            DpiNew = GetDpiWindowMonitor();

            AdjustWindow();
        }

        // Catch window message of DPI change.
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Check if Windows 8.1 or newer and if not, ignore message.
            if (!IsEightOneOrNewer()) return;

            const int WM_DPICHANGED = 0x02e0; // 0x02E0 from WinUser.h

            if (m.Msg == WM_DPICHANGED)
            {
                // wParam
                short lo = W32.GetLoWord(m.WParam.ToInt32());

                // lParam
                W32.RECT r = (W32.RECT)Marshal.PtrToStructure(m.LParam, typeof(W32.RECT));

                // Hold new DPI as target for adjustment.
                dpiNew = lo;

                switch (method)
                {
                    case ResizeMethod.Immediate:
                        if (dpiOld != lo)
                        {
                            MoveWindow();
                            AdjustWindow();
                        }
                        break;

                    case ResizeMethod.Delayed:
                        if (dpiOld != lo)
                        {
                            if (isBeingMoved)
                            {
                                willBeAdjusted = true;
                            }
                            else
                            {
                                AdjustWindow();
                            }
                        }
                        else
                        {
                            if (willBeAdjusted)
                            {
                                willBeAdjusted = false;
                            }
                        }
                        break;
                }
            }
        }

        // Detect user began moving this window.
        void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            isBeingMoved = true;
        }

        // Detect user ended moving this window.
        void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            isBeingMoved = false;
        }

        // Detect this window is moved.
        void MainForm_Move(object sender, EventArgs e)
        {
            if (willBeAdjusted && IsLocationGood())
            {
                willBeAdjusted = false;

                AdjustWindow();
            }
        }

        // Get new location of this window after DPI change.
        void MoveWindow()
        {
            if (Util.DesignMode) return;

            if (dpiOld == 0) return; // Abort.

            float factor = dpiNew / dpiOld;

            // Prepare new rectangles shrinked or expanded sticking four corners.
            int widthDiff = (int)(this.ClientSize.Width * factor) - this.ClientSize.Width;
            int heightDiff = (int)(this.ClientSize.Height * factor) - this.ClientSize.Height;

            List<W32.RECT> rectList = new List<W32.RECT>();

            // Left-Top corner
            rectList.Add(new W32.RECT
            {
                left = this.Bounds.Left,
                top = this.Bounds.Top,
                right = this.Bounds.Right + widthDiff,
                bottom = this.Bounds.Bottom + heightDiff
            });

            // Right-Top corner
            rectList.Add(new W32.RECT
            {
                left = this.Bounds.Left - widthDiff,
                top = this.Bounds.Top,
                right = this.Bounds.Right,
                bottom = this.Bounds.Bottom + heightDiff
            });

            // Left-Bottom corner
            rectList.Add(new W32.RECT
            {
                left = this.Bounds.Left,
                top = this.Bounds.Top - heightDiff,
                right = this.Bounds.Right + widthDiff,
                bottom = this.Bounds.Bottom
            });

            // Right-Bottom corner
            rectList.Add(new W32.RECT
            {
                left = this.Bounds.Left - widthDiff,
                top = this.Bounds.Top - heightDiff,
                right = this.Bounds.Right,
                bottom = this.Bounds.Bottom
            });

            // Get handle to monitor that has the largest intersection with each rectangle.
            for (int i = 0; i <= rectList.Count - 1; i++)
            {
                W32.RECT rectBuf = rectList[i];

                IntPtr handleMonitor = W32.MonitorFromRect(ref rectBuf, W32.MONITOR_DEFAULTTONULL);

                if (handleMonitor != IntPtr.Zero)
                {
                    // Check if at least Left-Top corner or Right-Top corner is inside monitors.
                    IntPtr handleLeftTop = W32.MonitorFromPoint(new W32.POINT(rectBuf.left, rectBuf.top), W32.MONITOR_DEFAULTTONULL);
                    IntPtr handleRightTop = W32.MonitorFromPoint(new W32.POINT(rectBuf.right, rectBuf.top), W32.MONITOR_DEFAULTTONULL);

                    if ((handleLeftTop != IntPtr.Zero) || (handleRightTop != IntPtr.Zero))
                    {
                        // Check if DPI of the monitor matches.
                        if (GetDpiSpecifiedMonitor(handleMonitor) == dpiNew)
                        {
                            // Move this window.
                            this.Location = new Point(rectBuf.left, rectBuf.top);
                            
                            break;
                        }
                    }
                }
            }
        }

        // Check if current location of this window is good for delayed adjustment.
        bool IsLocationGood()
        {
            if (dpiOld == 0) return false; // Abort.

            float factor = dpiNew / dpiOld;

            // Prepare new rectangle shrinked or expanded sticking Left-Top corner.
            int widthDiff = (int)(this.ClientSize.Width * factor) - this.ClientSize.Width;
            int heightDiff = (int)(this.ClientSize.Height * factor) - this.ClientSize.Height;

            W32.RECT rect = new W32.RECT()
            {
                left = this.Bounds.Left,
                top = this.Bounds.Top,
                right = this.Bounds.Right + widthDiff,
                bottom = this.Bounds.Bottom + heightDiff
            };

            // Get handle to monitor that has the largest intersection with the rectangle.
            IntPtr handleMonitor = W32.MonitorFromRect(ref rect, W32.MONITOR_DEFAULTTONULL);

            if (handleMonitor != IntPtr.Zero)
            {
                // Check if DPI of the monitor matches.
                if (GetDpiSpecifiedMonitor(handleMonitor) == dpiNew)
                {
                    return true;
                }
            }

            return false;
        }

        float _factor;
        [DefaultValue(1), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float Factor
        {
            get { return _factor; }
            private set 
            {
                if (_factor == value)
                    return;
                _factor = value;

                if (FactorChanged != null)
                    FactorChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler FactorChanged;

        // Adjust this window.
        protected virtual void AdjustWindow()
        {
            if (Util.DesignMode) return;

            if ((dpiOld == 0) || (dpiOld == dpiNew)) return; // Abort.

            float factor = dpiNew / dpiOld;

            //MessageBox.Show(string.Format("new{0}, old{1}, factor: {2}", dpiNew, dpiOld, factor));

            dpiOld = dpiNew;

            // Adjust location and size of Controls (except location of this window itself).
            this.Scale(new SizeF(factor, factor));

            // Adjust Font size of Controls.
            this.AdjustFont(factor);
        }

        protected virtual void AdjustFont(float factor)
        {
            if (Util.DesignMode) return;

            var dic = GetChildControlFontSizes(this);

            CoreUtil.ScaleFont(this, factor);

            foreach(var item in dic)
            {
                // not affected by parent font?
                if (item.Key.Font.Size == item.Value)
                {
                    CoreUtil.ScaleFont(item.Key, factor);
                    continue;
                }
            }
        }

        // Get child Controls in a specified Control.
        Dictionary<Control, float> GetChildControlFontSizes(Control parent)
        {
            var dic = new Dictionary<Control, float>();
            FillChildControlFontSizes(dic, parent);
            return dic;
        }

        void FillChildControlFontSizes(Dictionary<Control, float> dic, Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                dic.Add(child, child.Font.Size);
                FillChildControlFontSizes(dic, child);
            }
        }


        #region DPI

        // Get DPI of monitor containing this window by GetDpiForMonitor.
        float GetDpiWindowMonitor()
        {
            // Get handle to this window.
            IntPtr handleWindow = Process.GetCurrentProcess().MainWindowHandle;

            // Get handle to monitor.
            IntPtr handleMonitor = W32.MonitorFromWindow(handleWindow, W32.MONITOR_DEFAULTTOPRIMARY);

            // Get DPI.
            return GetDpiSpecifiedMonitor(handleMonitor);
        }

        // Get DPI of a specified monitor by GetDpiForMonitor.
        float GetDpiSpecifiedMonitor(IntPtr handleMonitor)
        {
            // Check if GetDpiForMonitor function is available.
            if (!IsEightOneOrNewer()) return this.CurrentAutoScaleDimensions.Width;

            // Get DPI.
            uint dpiX = 0;
            uint dpiY = 0;

            int result = W32.GetDpiForMonitor(handleMonitor, W32.Monitor_DPI_Type.MDT_Default, out dpiX, out dpiY);

            if (result != 0) // If not S_OK (= 0)
            {
                throw new Exception("Failed to get DPI of monitor containing this window.");
            }

            return (float)dpiX;
        }

        // Get DPI for all monitors by GetDeviceCaps.
        float GetDpiDeviceMonitor()
        {
            int dpiX = 0;
            IntPtr screen = IntPtr.Zero;

            try
            {
                screen = W32.GetDC(IntPtr.Zero);
                dpiX = W32.GetDeviceCaps(screen, W32.LOGPIXELSX);
            }
            finally
            {
                if (screen != IntPtr.Zero)
                {
                    W32.ReleaseDC(IntPtr.Zero, screen);
                }
            }

            return (float)dpiX;
        }

        #endregion


        #region OS Version

        // Check if OS is Windows 8.1 or newer.
        bool IsEightOneOrNewer()
        {
            // To get this value correctly, it is required to include ID of Windows 8.1 in the manifest file.
            return (6.3 <= GetVersion());
        }

        // Get OS version in Double.
        private double GetVersion()
        {
            OperatingSystem os = Environment.OSVersion;

            return os.Version.Major + ((double)os.Version.Minor / 10);
        }

        #endregion

    }
}
