using Gecko.Interop;
using Gecko.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// PLZ keep all Windows Forms related code here
namespace Gecko
{
    partial class GeckoWebBrowser
        : Control
    {
        #region Overridden Properties

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Image BackgroundImage
        {
            get { return base.BackgroundImage; }
            set { base.BackgroundImage = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override ImageLayout BackgroundImageLayout
        {
            get { return base.BackgroundImageLayout; }
            set { base.BackgroundImageLayout = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        #endregion

        #region Overriden WinForms functions

        protected virtual IEnumerable<string> DefaultEvents
        {
            get
            {
                yield return "submit";
                yield return "keydown";
                yield return "keyup";
                yield return "keypress";
                yield return "mousemove";
                yield return "mouseover";
                yield return "mouseout";
                yield return "mousedown";
                yield return "mouseup";
                yield return "click";
                yield return "dblclick";
                yield return "compositionstart";
                yield return "compositionend";
                yield return "contextmenu";
                yield return "DOMMouseScroll";
                yield return "focus";
                yield return "blur";
                // Load event added here rather than DOMDocument as DOMDocument recreated when navigating
                // ths losing attached listener.
                yield return "load";
                yield return "DOMContentLoaded";
                yield return "readystatechange";
                yield return "change";
                yield return "hashchange";
                yield return "dragstart";
                yield return "dragleave";
                yield return "drag";
                yield return "drop";
                yield return "dragend";
                yield return "mozfullscreenchange"; //TODO: change to "fullscreenchange" after prefix removed
            }
        }

        protected ComPtr<nsIDOMEventTarget> EventTarget { get; private set; }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (!this.DesignMode)
            {
                Xpcom.Initialize();
                WindowCreator.Register();
                LauncherDialogFactory.Register();

                WebBrowser = Xpcom.CreateInstance<nsIWebBrowser>(Contracts.WebBrowser);
                WebBrowserFocus = (nsIWebBrowserFocus)WebBrowser;
                BaseWindow = (nsIBaseWindow)WebBrowser;
                WebNav = (nsIWebNavigation)WebBrowser;

                WebBrowser.SetContainerWindowAttribute(this);
                BaseWindow.InitWindow(this.Handle, IntPtr.Zero, 0, 0, this.Width, this.Height);


                BaseWindow.Create();

                Guid nsIWebProgressListenerGUID = typeof(nsIWebProgressListener).GUID;
                Guid nsIWebProgressListener2GUID = typeof(nsIWebProgressListener2).GUID;
                WebBrowser.AddWebBrowserListener(this.GetWeakReference(), ref nsIWebProgressListenerGUID);
                WebBrowser.AddWebBrowserListener(this.GetWeakReference(), ref nsIWebProgressListener2GUID);

                if (UseHttpActivityObserver)
                {
                    ObserverService.AddObserver(this, ObserverNotifications.HttpRequests.HttpOnModifyRequest, false);
                    Net.HttpActivityDistributor.AddObserver(this);
                }

                // var domEventListener = new GeckoDOMEventListener(this);

                {
                    var domWindow = WebBrowser.GetContentDOMWindowAttribute();
                    EventTarget = domWindow.GetWindowRootAttribute().AsComPtr();
                    Marshal.ReleaseComObject(domWindow);
                }

                foreach (string sEventName in this.DefaultEvents)
                {
                    using (var eventType = new nsAString(sEventName))
                    {
                        EventTarget.Instance.AddEventListener(eventType, this, true, true, 2);
                    }
                }

                // history
                {
                    var sessionHistory = WebNav.GetSessionHistoryAttribute();
                    if (sessionHistory != null) sessionHistory.AddSHistoryListener(this);
                }

                BaseWindow.SetVisibilityAttribute(true);

                // this fix prevents the browser from crashing if the first page loaded is invalid (missing file, invalid URL, etc)
                using (var doc = Document)
                {
                    if (doc != null)
                    {
                        // only for html documents
                        doc.Cookie = "";
                    }
                    WindowMediator.RegisterWindow(this);
                }
            }

            base.OnHandleCreated(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (BaseWindow != null)
            {
                this.Stop();

                WindowMediator.UnregisterWindow(this);

                nsIDocShell docShell = Xpcom.QueryInterface<nsIDocShell>(BaseWindow);
                if (docShell != null && !docShell.IsBeingDestroyed())
                {
                    try
                    {
                        var window = Xpcom.QueryInterface<nsIDOMWindow>(docShell);
                        if (window != null)
                        {
                            try
                            {
                                if (!window.GetClosedAttribute()) window.Close();
                            }
                            finally
                            {
                                Xpcom.FreeComObject(ref window);
                            }
                        }
                    }
                    finally
                    {
                        Xpcom.FreeComObject(ref docShell);
                    }
                }

                if (EventTarget != null)
                {
                    //Remove Event Listener			
                    foreach (string sEventType in this.DefaultEvents)
                    {
                        using (var eventType = new nsAString(sEventType))
                        {
                            EventTarget.Instance.RemoveEventListener(eventType, this, true);
                        }
                    }
                    EventTarget.Dispose();
                    EventTarget = null;
                }

                BaseWindow.Destroy();

                Xpcom.FreeComObject(ref CommandParams);

                var webBrowserFocus = this.WebBrowserFocus;
                this.WebBrowserFocus = null;
                Xpcom.FreeComObject(ref webBrowserFocus);
                Xpcom.FreeComObject(ref WebNav);
                Xpcom.FreeComObject(ref BaseWindow);
                Xpcom.FreeComObject(ref WebBrowser);
            }

            base.OnHandleDestroyed(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            if (WebBrowserFocus != null)
                WebBrowserFocus.Activate();

            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            if (WebBrowserFocus != null && !IsBusy)
                WebBrowserFocus.Deactivate();

            base.OnLeave(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (BaseWindow != null)
            {
                BaseWindow.SetPositionAndSize(0, 0, ClientSize.Width != 0 ? ClientSize.Width : 1, ClientSize.Height != 0 ? ClientSize.Height : 1, true);
            }

            base.OnSizeChanged(e);
        }

        #region protected override void WndProc(ref Message m)

        protected override void WndProc(ref Message m)
        {
            const int WM_GETDLGCODE = 0x87;
            const int DLGC_WANTALLKEYS = 0x4;
            const int WM_MOUSEACTIVATE = 0x21;
            const int MA_ACTIVATE = 0x1;
            const int WM_IME_SETCONTEXT = 0x0281;
            const int WM_PAINT = 0x000F;
            const int WM_SETFOCUS = 0x0007;
            const int WM_KILLFOCUS = 0x0008;


            const int ISC_SHOWUICOMPOSITIONWINDOW = unchecked((int)0x80000000);
            if (!DesignMode)
            {
                IntPtr focus;
                switch (m.Msg)
                {
                    case WM_GETDLGCODE:
                        m.Result = (IntPtr)DLGC_WANTALLKEYS;
                        return;
                    case WM_SETFOCUS:
                        break;
                    case WM_MOUSEACTIVATE:
                        // TODO FIXME: port for Linux
                        if (Xpcom.IsWindows)
                        {
                            m.Result = (IntPtr)MA_ACTIVATE;
                            focus = User32.GetFocus();
                            // Console.WriteLine( "focus {0:X8}, Handle {1:X8}", focus.ToInt32(), Handle.ToInt32() );
                            if (!IsSubWindow(Handle, focus))
                            {
                                //	var str = string.Format( "+WM_MOUSEACTIVATE {0:X8} lastfocus", focus.ToInt32() );
                                //	System.Diagnostics.Debug.WriteLine( str );
                                Console.WriteLine("Activating");
                                if (WebBrowserFocus != null)
                                    WebBrowserFocus.Activate();
                                if (Window != null)
                                    Services.WindowWatcher.ActiveWindow = Window;
                            }
                            else
                            {
                                //	var str = string.Format( "-WM_MOUSEACTIVATE {0:X8} lastfocus", focus.ToInt32() );
                                //	System.Diagnostics.Debug.WriteLine( str );
                            }
                            if (Window != null && !Window.Equals(Services.WindowWatcher.ActiveWindow))
                            {
                                if (WebBrowserFocus != null)
                                    WebBrowserFocus.Activate();
                                if (Window != null)
                                    Services.WindowWatcher.ActiveWindow = Window;
                            }
                            return;
                        }
                        return;

                    //http://msdn.microsoft.com/en-US/library/windows/desktop/dd374142%28v=vs.85%29.aspx
                    case WM_IME_SETCONTEXT:
                        focus = User32.GetFocus();
                        if (User32.IsChild(Handle, focus))
                        {
                            break;
                        }

                        if (WebBrowserFocus != null)
                        {
                            //	var str = string.Format( "WM_IME_SETCONTEXT {0} {1} {2} (focus on {3})", m.HWnd.ToString( "X8" ), m.WParam, m.LParam.ToString( "X8" ), focus.ToString( "X8" ) );
                            //	System.Diagnostics.Debug.WriteLine( str );


                            var param = m.LParam.ToInt64();
                            if ((param & ISC_SHOWUICOMPOSITIONWINDOW) != 0)
                            {

                            }
                            if (m.WParam == IntPtr.Zero)
                            {
                                // zero
                                RemoveInputFocus();
                                WebBrowserFocus.Deactivate();
                            }
                            else
                            {
                                // non-zero (1)
                                WebBrowserFocus.Activate();
                                SetInputFocus();
                            }
                            return;
                        }

                        break;
                    case WM_PAINT:
                        break;
                }
            }

            // Firefox 17+ can crash when handing this windows message so we just ignore it.
            if (m.Msg == 0x128 /*WM_UPDATEUISTATE*/)
                return;

            base.WndProc(ref m);
        }

        private bool IsSubWindow(IntPtr window, IntPtr candidate)
        {
            // search parent until desktop (flash window is owned by other process)
            while (candidate != IntPtr.Zero)
            {
                candidate = User32.GetParent(candidate);
                if (window == candidate)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.DesignMode)
            {
                string versionString =
                    ((AssemblyFileVersionAttribute)
                      Attribute.GetCustomAttribute(GetType().Assembly, typeof(AssemblyFileVersionAttribute))).Version;

                using (
                    Brush brush = new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.SolidDiamond,
                                                                           Color.FromArgb(240, 240, 240), Color.White))
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);

                e.Graphics.DrawString(
                    string.Format("GeckoFX v{0}\r\n" + "http://bitbucket.org/geckofx/", versionString),
                    SystemFonts.MessageBoxFont,
                    Brushes.Black,
                    new RectangleF(2, 2, this.Width - 4, this.Height - 4));
                e.Graphics.DrawRectangle(SystemPens.ControlDark, 0, 0, Width - 1, Height - 1);
            }
            base.OnPaint(e);
        }

        protected override void OnPrint(PaintEventArgs e)
        {
            base.OnPrint(e);
            if (!this.DesignMode)
            {
                ImageCreator creator = new ImageCreator(this);
                byte[] mBytes = creator.CanvasGetPngImage((uint)0, (uint)0, (uint)this.Width, (uint)this.Height);
                using (Image image = Image.FromStream(new System.IO.MemoryStream(mBytes)))
                {
                    e.Graphics.DrawImage(image, 0.0f, 0.0f);
                }
            }
        }

        /// <summary>
        /// Enable default fullscreen windowing for HTML5 fullscreen.
        /// 
        /// You also have to set pref "full-screen-api.enabled" to true to enable fullscreen of gecko.
        /// 
        /// When the page enters fullscreen state, move this browser into a fullscreen Form;
        /// When the page exits fullscreen state, move this browser back into its original parent, with original index.
        /// 
        /// This method should only be called AFTER this browser has been added into its parent.
        /// After calling this method, this browser's Dock and index should not be changed anymore.
        /// 
        /// If this method is not called, the fullscreen element only fills the viewport of this browser.
        /// 
        /// You can also implement your fullscreen windowing by listening to the FullscreenChange event.
        /// 
        /// TODO: implement confirm prompt. Currently enters fullscreen without user's confirm.
        /// </summary>
        public void EnableDefaultFullscreen()
        {
            Control browserParent = Parent;
            int browserIndex = Parent.Controls.IndexOf(this);
            var browserDock = Dock;
            Form fullscreenWindow = null;
            FullscreenChange += (s, e) =>
            {
                if (Document.MozFullScreen && fullscreenWindow == null)
                {
                    fullscreenWindow = new Form();
                    Dock = DockStyle.Fill;
                    fullscreenWindow.Controls.Add(this);
                    fullscreenWindow.WindowState = FormWindowState.Maximized;
                    fullscreenWindow.TopMost = true;
                    fullscreenWindow.FormBorderStyle = FormBorderStyle.None;
                    fullscreenWindow.Show();
                    fullscreenWindow.FormClosing += (sn, ev) =>
                    {
                        Dock = browserDock;
                        browserParent.Controls.Add(this);
                        browserParent.Controls.SetChildIndex(this, browserIndex);
                    };
                }
                else if (!this.Document.MozFullScreen && fullscreenWindow != null)
                {
                    fullscreenWindow.Close();
                    fullscreenWindow = null;
                }
            };
        }

        /// <summary>
        /// This method is called by gecko when showing a print dialog; window handle
        /// returned here is the dialog's owner. If model print dialog is not required, just returns NULL.
        /// 
        /// TODO: Gecko destroys the window returned after the dialog is dismissed, so we can't return this.Handle, we
        /// create and return a temp window instead. This may be a bug of gecko.
        /// 
        /// See https://bitbucket.org/geckofx/geckofx-29.0/issue/50/printing-with-native-printingpromptservice for more info.
        /// </summary>
        /// <returns></returns>
        IntPtr nsIEmbeddingSiteWindow.GetSiteWindowAttribute()
        {
            const string name = "TempSubWindow";
            var temp = Controls[name];
            if (temp == null)
            {
                temp = new Control()
                {
                    Top = -10,
                    Left = -10,
                    Width = 1,
                    Height = 1,
                    Name = name
                };
                temp.HandleDestroyed += (s, e) => Controls.Remove(temp);
                Controls.Add(temp);
            }
            return temp.Handle;
        }

        #endregion

        #region Internal classes

        #region class ToolTipWindow

        /// <summary>
        /// A window to contain a tool tip.
        /// </summary>
        private class ToolTipWindow : ToolTip
        {
        }

        #endregion

        #endregion


        public void ForceRedraw()
        {
            BaseWindow.Repaint(true);
        }


        #region UserInterfaceThreadInvoke
        /// <summary>
        /// UI platform independent call function from UI thread
        /// </summary>
        /// <param name="action"></param>
        public void UserInterfaceThreadInvoke(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SafeAction(action)));
            }
            else
            {
                SafeAction(action);
            }
        }

        /// <summary>
        /// Exception handler for action
        /// </summary>
        /// <param name="action"></param>
        private void SafeAction(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Invoking exception"));
            }
        }

        /// <summary>
        /// UI platform independent call function from UI thread
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public T UserInterfaceThreadInvoke<T>(Func<T> func)
        {
            if (InvokeRequired)
            {
                return (T)Invoke(new Func<T>(() => SafeFunc(func)));
            }
            return SafeFunc(func);
        }

        /// <summary>
        /// exception handler for function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        private T SafeFunc<T>(Func<T> func)
        {
            T ret = default(T);
            try
            {
                ret = func();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Invoking exception"));
            }
            return ret;
        }
        #endregion
    }
}
