using Gecko.Interop;
using Gecko.IO;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Gecko
{
    public partial class GeckoWebBrowser
        : HwndHost,
        IGeckoWebBrowser,
        // window chrome
        nsIWebBrowserChrome,
        nsIEmbeddingSiteWindow,
        nsIInterfaceRequestor,
        // weak reference creation
        nsISupportsWeakReference
    {
        private WebProgressListener _webProgressListener = new WebProgressListener();
        private nsIWeakReference _webProgressWeakReference;


        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            Loaded += new System.Windows.RoutedEventHandler(GeckoWebBrowser_Loaded);
            HwndSourceParameters param = new HwndSourceParameters("web browser container");
            param.Width = 100;
            param.Height = 100;
            param.ParentWindow = hwndParent.Handle;
            param.WindowStyle = 0x10000000 | 0x40000000;
            _source = new HwndSource(param);
            return new HandleRef(this, _source.Handle);
        }

        void GeckoWebBrowser_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Xpcom.Initialize();
            _webBrowser = Xpcom.CreateInstance2<nsIWebBrowser>(Contracts.WebBrowser);
            _webBrowserFocus = (nsIWebBrowserFocus)_webBrowser.Instance;
            _baseWindow = (nsIBaseWindow)_webBrowser.Instance;
            _webNav = (nsIWebNavigation)_webBrowser.Instance;
            _webBrowser.Instance.SetContainerWindowAttribute(this);
            _baseWindow.InitWindow(Handle, IntPtr.Zero, 0, 0, (int)ActualWidth, (int)ActualHeight);
            _baseWindow.Create();

            #region nsIWebProgressListener/nsIWebProgressListener2
            Guid nsIWebProgressListenerGUID = typeof(nsIWebProgressListener).GUID;
            Guid nsIWebProgressListener2GUID = typeof(nsIWebProgressListener2).GUID;
            _webProgressWeakReference = _webProgressListener.GetWeakReference();
            _webBrowser.Instance.AddWebBrowserListener(_webProgressWeakReference, ref nsIWebProgressListenerGUID);
            _webBrowser.Instance.AddWebBrowserListener(_webProgressWeakReference, ref nsIWebProgressListener2GUID);
            #endregion
            _baseWindow.SetVisibilityAttribute(true);
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            #region nsIWebProgressListener/nsIWebProgressListener2
            _webProgressListener.IsListening = false;
            Guid nsIWebProgressListenerGUID = typeof(nsIWebProgressListener).GUID;
            Guid nsIWebProgressListener2GUID = typeof(nsIWebProgressListener2).GUID;
            _webBrowser.Instance.RemoveWebBrowserListener(_webProgressWeakReference, ref nsIWebProgressListenerGUID);
            _webBrowser.Instance.RemoveWebBrowserListener(_webProgressWeakReference, ref nsIWebProgressListener2GUID);
            _webProgressWeakReference = null;
            _webProgressListener = null;
            #endregion
            //_webNav.Stop(  );
            _webBrowser.FinalRelease();
            _webBrowser.Dispose();
            _webBrowser = null;
            _source.Dispose();
        }

        #region IGeckoWebBrowser

        public GeckoDocument Document
        {
            get
            {
                if (_webBrowser == null)
                    return null;

                nsIWebBrowserChrome chromeBrowser = this;
                nsIWebBrowser browser = chromeBrowser.GetWebBrowserAttribute();
                var domWindow = browser.GetContentDOMWindowAttribute();
                //var domDocument = domWindow.GetDocumentAttribute();
                Marshal.ReleaseComObject(domWindow);
                return null;
                //return GeckoDomDocument.CreateDomDocumentWraper(domDocument) as GeckoDocument;
            }
        }

        public GeckoWindow Window
        {
            get
            {
                if (_webBrowser == null || _webBrowser.Instance == null)
                {
                    return null;
                }

                return _webBrowser.Instance.GetContentDOMWindowAttribute().Wrap(x => new GeckoWindow(x));
            }
        }

        public bool IsDisposed
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Navigates to the specified URL.
        /// In order to find out when Navigate has finished attach a handler to NavigateFinishedNotifier.NavigateFinished.
        /// </summary>
        /// <param name="url">The url to navigate to.</param>
        public void Navigate(string url)
        {
            Navigate(url, 0, null, null, null);
        }

        /// <summary>
        /// Navigates to the specified URL using the given load flags.
        /// In order to find out when Navigate has finished attach a handler to NavigateFinishedNotifier.NavigateFinished.
        /// </summary>
        /// <param name="url">The url to navigate to.  If the url is empty or null, the browser does not navigate and the method returns false.</param>
        /// <param name="loadFlags">Flags which specify how the page is loaded.</param>
        public bool Navigate(string url, GeckoLoadFlags loadFlags)
        {
            return Navigate(url, loadFlags, null, null, null);
        }

        /// <summary>
        ///  Navigates to the specified URL using the given load flags, referrer and post data
        ///  In order to find out when Navigate has finished attach a handler to NavigateFinishedNotifier.NavigateFinished.
        /// </summary>
        /// <param name="url">The url to navigate to.  If the url is empty or null, the browser does not navigate and the method returns false.</param>
        /// <param name="loadFlags">Flags which specify how the page is loaded.</param>
        /// <param name="referrer">The referring URL, or null.</param>
        /// <param name="postData">post data and headers, or null</param>
        /// <returns>true if Navigate started. false otherwise.</returns>
        public bool Navigate(string url, GeckoLoadFlags loadFlags, string referrer, MimeInputStream postData)
        {
            return Navigate(url, loadFlags, referrer, postData, null);
        }

        /// <summary>
        ///  Navigates to the specified URL using the given load flags, referrer and post data
        ///  In order to find out when Navigate has finished attach a handler to NavigateFinishedNotifier.NavigateFinished.
        /// </summary>
        /// <param name="url">The url to navigate to.  If the url is empty or null, the browser does not navigate and the method returns false.</param>
        /// <param name="loadFlags">Flags which specify how the page is loaded.</param>
        /// <param name="referrer">The referring URL, or null.</param>
        /// <param name="postData">post data and headers, or null</param>
        /// <param name="headers">headers, or null</param>
        /// <returns>true if Navigate started. false otherwise.</returns>
        public bool Navigate(string url, GeckoLoadFlags loadFlags, string referrer, MimeInputStream postData, MimeInputStream headers)
        {
            if (string.IsNullOrEmpty(url))
                return false;

            // added these from http://code.google.com/p/geckofx/issues/detail?id=5 so that it will work even if browser isn't currently shown
            //if (!IsHandleCreated) CreateHandle();
            //if (IsBusy) this.Stop();


            //	if (!IsHandleCreated)
            //		throw new InvalidOperationException("Cannot call Navigate() before the window handle is created.");

            // WebNav.LoadURI throws an exception if we try to open a file that doesn't exist...
            Uri created;
            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out created) && created.IsAbsoluteUri && created.IsFile)
            {
                if (!File.Exists(created.LocalPath) && !Directory.Exists(created.LocalPath))
                    return false;
            }

            nsIURI referrerUri = null;
            if (!string.IsNullOrEmpty(referrer))
            {
                //referrerUri = Xpcom.GetService<nsIIOService>("@mozilla.org/network/io-service;1").NewURI(new nsAUTF8String(referrer), null, null);
                referrerUri = IOService.CreateNsIUri(referrer);
            }


            _webNav.LoadURI(url, (uint)loadFlags, referrerUri, postData != null ? postData._inputStream : null, headers != null ? headers._inputStream : null);

            return true;
        }

        public bool GoBack()
        {
            bool ok;
            try
            {
                _webNav.GoBack();
                ok = true;
            }
            catch (Exception)
            {
                ok = false;
            }
            return ok;

        }

        public bool GoForward()
        {
            bool ok;
            try
            {
                _webNav.GoForward();
                ok = true;
            }
            catch (Exception)
            {
                ok = false;
            }
            return ok;
        }

        public bool Reload()
        {
            bool ok;
            try
            {
                _webNav.Reload(0);
                ok = true;
            }
            catch (Exception)
            {
                ok = false;
            }
            return ok;
        }

        public event EventHandler<Events.GeckoDocumentCompletedEventArgs> DocumentCompleted;

        public event EventHandler<Events.GeckoNavigationErrorEventArgs> NavigationError;

        /// <summary>
        /// UI platform independent call function from UI thread
        /// </summary>
        /// <param name="action"></param>
        public void UserInterfaceThreadInvoke(Action action)
        {
            if (Dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                Dispatcher.Invoke(action);
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
            if (Dispatcher.CheckAccess())
            {
                return func();
            }
            return (T)Dispatcher.Invoke(func);
        }



        #endregion

        protected override void OnGotMouseCapture(System.Windows.Input.MouseEventArgs e)
        {
            base.OnGotMouseCapture(e);
        }


        protected override void OnRenderSizeChanged(System.Windows.SizeChangedInfo sizeInfo)
        {
            if (_baseWindow != null)
            {
                _baseWindow.SetPositionAndSize(0, 0, (int)sizeInfo.NewSize.Width, (int)sizeInfo.NewSize.Height, true);
            }

            base.OnRenderSizeChanged(sizeInfo);
        }
    }
}
