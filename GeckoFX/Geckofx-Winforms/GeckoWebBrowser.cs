#region ***** BEGIN LICENSE BLOCK *****
/* Version: MPL 1.1/GPL 2.0/LGPL 2.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is Skybound Software code.
 *
 * The Initial Developer of the Original Code is Skybound Software.
 * Portions created by the Initial Developer are Copyright (C) 2008-2009
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
 *
 * Alternatively, the contents of this file may be used under the terms of
 * either the GNU General Public License Version 2 or later (the "GPL"), or
 * the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
 * in which case the provisions of the GPL or the LGPL are applicable instead
 * of those above. If you wish to allow use of your version of this file only
 * under the terms of either the GPL or the LGPL, and not to allow others to
 * use your version of this file under the terms of the MPL, indicate your
 * decision by deleting the provisions above and replace them with the notice
 * and other provisions required by the GPL or the LGPL. If you do not delete
 * the provisions above, a recipient may use your version of this file under
 * the terms of any one of the MPL, the GPL or the LGPL.
 */
#endregion END LICENSE BLOCK

using Gecko.Events;
using Gecko.Interop;
using Gecko.IO;
using Gecko.Net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Gecko
{
	/// <summary>
	/// A Gecko-based web browser.
	/// </summary>
	public partial class GeckoWebBrowser :
		 IGeckoWebBrowser,
		nsIWebBrowserChrome,
		nsIContextMenuListener2,
		nsIWebProgressListener,
		nsIWebProgressListener2,
		//nsIWebBrowserChromeFocus, -- TODO
		nsIInterfaceRequestor,
		nsIEmbeddingSiteWindow,
		nsIDOMEventListener,
		nsISHistoryListener,
		nsITooltipListener,
		nsIObserver,
		nsIHttpActivityObserver,
		nsISupportsWeakReference
	//nsIWindowProvider,
	{
		#region Fields
		/// <summary>
		/// Additional DOM message listeners
		/// </summary>
		Dictionary<string, Action<string>> _messageEventListeners = new Dictionary<string, Action<string>>();
		/// <summary>
		/// nsIWebBrowser instance
		/// </summary>
		nsIWebBrowser WebBrowser;
		/// <summary>
		/// nsIWebBrowser casted to nsIBaseWindow
		/// </summary>
		nsIBaseWindow BaseWindow;
		/// <summary>
		/// nsIWebBrowser casted no nsIWebNavigation
		/// </summary>
		nsIWebNavigation WebNav;
		nsICommandParams CommandParams;

		uint ChromeFlags;
		bool m_javascriptDebuggingEnabled;

		GeckoWindow _Window;
		GeckoDomDocument _Document;
		#endregion

		#region public static methods

		[Obsolete("Just provide custom PromptFactory.PromptServiceCreator", false)]
		public static void UseCustomPrompt()
		{
		}

		#endregion

		/// <summary>
		/// Initializes a new instance of <see cref="GeckoWebBrowser"/>.
		/// </summary>
		public GeckoWebBrowser()
		{
			NavigateFinishedNotifier = new NavigateFinishedNotifier(this);
		}

		#region protected override void Dispose(bool disposing)
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				//var count = Gecko.Interop.ComDebug.GetRefCount(WebBrowser);
				if (NavigateFinishedNotifier != null)
					NavigateFinishedNotifier.Dispose();
				//count = Gecko.Interop.ComDebug.GetRefCount(WebBrowser);

				if (_Window != null)
					_Window.Dispose();
				_Window = null;
				if (_Document != null)
					_Document.Dispose();
				_Document = null;
			}
			base.Dispose(disposing);
		}

		protected virtual bool HasShutdownStarted()
		{
			return Environment.HasShutdownStarted || AppDomain.CurrentDomain.IsFinalizingForUnload();
		}

		#endregion

		public nsIWebBrowserFocus WebBrowserFocus
		{
			get; protected set;
		}

		/// <summary>
		/// returns null if window is not editable.
		/// </summary>
		public nsIEditor Editor
		{
			get
			{
				var editingSession = Xpcom.CreateInstance<nsIEditingSession>("@mozilla.org/editor/editingsession;1");
				var returnValue = editingSession.GetEditorForWindow((nsIDOMWindow)Window.DomWindow);
				Marshal.ReleaseComObject(editingSession);
				return returnValue;
			}
		}

		// defaults to false
		public bool UseHttpActivityObserver
		{
			get;
			set;
		}


		class WindowCreator : nsIWindowCreator2
		{
			static WindowCreator()
			{
				// call window watcher service
				Services.WindowWatcher.WindowCreator = new WindowCreator();
			}

			public static void Register()
			{
				// calling this method simply invokes the static ctor
			}

			private nsIWebBrowserChrome DoCreateChromeWindow(nsIWebBrowserChrome parent, uint chromeFlags, uint contextFlags, nsIURI uri, ref bool cancel)
			{
				var url = "";
				if (uri != null)
					url = (nsString.Get(uri.GetSpecAttribute)).ToString();
				else
					url = "about:blank";

				// for chrome windows, we can use the AppShellService to create the window using some built-in xulrunner code
				GeckoWindowFlags flags = (GeckoWindowFlags)chromeFlags;
				if ((flags & GeckoWindowFlags.OpenAsChrome) != 0)
				{
					// obtain the services we need
					// nsIAppShellService appShellService = Xpcom.GetService<nsIAppShellService>("@mozilla.org/appshell/appShellService;1");

					// create the child window
					nsIXULWindow xulChild = AppShellService.CreateTopLevelWindow(null, null, chromeFlags, -1, -1);

					// this little gem allows the GeckoWebBrowser to be properly activated when it gains the focus again
					if (parent is GeckoWebBrowser && (flags & GeckoWindowFlags.OpenAsDialog) != 0)
					{
						EventHandler gotFocus = null;
						gotFocus = delegate (object sender, EventArgs e)
						{
							var geckoWebBrowser = (GeckoWebBrowser)sender;
							geckoWebBrowser.GotFocus -= gotFocus;

							if (geckoWebBrowser.WebBrowserFocus != null)
								geckoWebBrowser.WebBrowserFocus.Activate();
						};
						(parent as GeckoWebBrowser).GotFocus += gotFocus;
					}

					// return the chrome
					return Xpcom.QueryInterface<nsIWebBrowserChrome>(xulChild);
				}

				GeckoWebBrowser browser = parent as GeckoWebBrowser;
				if (browser != null)
				{
					var e = new GeckoCreateWindow2EventArgs(flags, url);
					if (uri != null) // called by CreateChromeWindow2()
						browser.OnCreateWindow2(e);
					browser.OnCreateWindow(e);

					if (e.Cancel)
					{
						cancel = true;
						return null;
					}

					if (e.WebBrowser != null)
					{
						// set flags
						((nsIWebBrowserChrome)e.WebBrowser).SetChromeFlagsAttribute(chromeFlags);
						return e.WebBrowser;
					}

					nsIXULWindow xulChild = AppShellService.CreateTopLevelWindow(null, null, chromeFlags, e.InitialWidth, e.InitialHeight);
					return Xpcom.QueryInterface<nsIWebBrowserChrome>(xulChild);
				}
				return null;
			}

			public nsIWebBrowserChrome CreateChromeWindow(nsIWebBrowserChrome parent, uint chromeFlags)
			{
				bool cancel = false;
				return DoCreateChromeWindow(parent, chromeFlags, 0, null, ref cancel);
			}

			public nsIWebBrowserChrome CreateChromeWindow2(nsIWebBrowserChrome parent, uint chromeFlags, uint contextFlags, nsIURI uri, ref bool cancel)
			{
				return DoCreateChromeWindow(parent, chromeFlags, contextFlags, uri, ref cancel);
			}
		}

		#region Navigation
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
			if (!IsHandleCreated) CreateHandle();
			if (IsBusy) this.Stop();

			if (!IsHandleCreated)
				throw new InvalidOperationException("Cannot call Navigate() before the window handle is created.");

			nsIURI referrerUri = null;
			if (!string.IsNullOrEmpty(referrer))
			{
				referrerUri = IOService.CreateNsIUri(referrer);
			}

			// We want Navigate() to return immediately and to fire events asynchronously. Howerver,
			// WebNav.LoadURI() may fire 'Navigating' event synchronously, so we call it asynchronously.
			// WebNav.LoadURI may throw exceptions for some inaccessable urls
			// (see https://bugzilla.mozilla.org/show_bug.cgi?id=995298), 
			// so we convert them into NavigationError events.
			BeginInvoke(new Action(() =>
			{
				if (IsDisposed)
					return;

				try
				{
					WebNav.LoadURI(
						url, (uint)loadFlags, referrerUri, postData != null ? postData._inputStream : null,
						headers != null ? headers._inputStream : null);
				}
				catch (COMException ce)
				{
					OnNavigationError(new GeckoNavigationErrorEventArgs(url, Window, ce.ErrorCode));
				}
				catch (Exception e)
				{
					OnNavigationError(new GeckoNavigationErrorEventArgs(url, Window, GeckoError.NS_ERROR_UNEXPECTED));
				}
			}));

			return true;
		}

		#endregion

		/// <summary>
		/// Loads supplied html string.
		/// Note: LoadHtml isn't intended to load complex Html Documents.		
		/// In order to find out when LoadHtml has finished attach a handler to DocumentCompleted Event.
		/// </summary>
		/// <param name="htmlDocument"></param>
		public void LoadHtml(string htmlDocument)
		{
			LoadBase64EncodedData("text/html", htmlDocument);
		}

		/// <summary>
		/// Load supplied string.encoded as base64.
		/// </summary>
		/// <param name="type">the type of the data eg. text/html </param>
		/// <param name="data">string that will be encoded as base64 </param>
		public void LoadBase64EncodedData(string type, string data)
		{
			var bytes = System.Text.Encoding.UTF8.GetBytes(data);
			Navigate(string.Concat("data:", type, ";base64,", Convert.ToBase64String(bytes)));
		}

		public void LoadHtml(string content, string url)
		{
			if (url != null)
				LoadContent(content, url, "text/html");
			else
				LoadHtml(content);
		}

		public void LoadContent(string content, string url, string contentType)
		{
			if (url == null)
				throw new ArgumentNullException("url");

			//// Control handle must be created so we can get a nsIDocShell.
			if (CouldFindOrCreateHandle())
				InternalLoadContent(content, url, contentType);
			else //No handle could be created yet, so postpone loading the content until the Handle has been created
				HandleCreated += (sender, args) => InternalLoadContent(content, url, contentType);
		}

		private void InternalLoadContent(string content, string url, string contentType)
		{
			using (var sContentType = new nsACString(contentType))
			using (var sUtf8 = new nsACString("UTF8"))
			{
				ByteArrayInputStream inputStream = null;
				try
				{
					inputStream = ByteArrayInputStream.Create(System.Text.Encoding.UTF8.GetBytes(content != null ? content : string.Empty));

					nsIDocShell docShell = Xpcom.QueryInterface<nsIDocShell>(this.WebBrowser);
					docShell.LoadStream(inputStream, IOService.CreateNsIUri(url), sContentType, sUtf8, null);
					Marshal.ReleaseComObject(docShell);
				}
				finally
				{
					if (inputStream != null)
						inputStream.Close();
				}
			}
		}

		private bool CouldFindOrCreateHandle()
		{
			if (this.IsDisposed)
				return false;

			if (!IsHandleCreated)
				CreateHandle();
			return IsHandleCreated;
		}

		[Obsolete]
		public NavigateFinishedNotifier NavigateFinishedNotifier;

		/// <summary>
		/// Gets or sets whether all default items are removed from the standard context menu.
		/// </summary>
		[DefaultValue(false), Description("Removes default items from the standard context menu.  The ShowContextMenu event is still raised to add custom items.")]
		public bool NoDefaultContextMenu
		{
			get { return _NoDefaultContextMenu; }
			set { _NoDefaultContextMenu = value; }
		}
		bool _NoDefaultContextMenu;

		/// <summary>
		/// Gets or sets the text displayed in the status bar.
		/// </summary>
		[Browsable(false), DefaultValue("")]
		public string StatusText
		{
			get { return _StatusText ?? ""; }
			set
			{
				if (StatusText != value)
				{
					_StatusText = value;
					OnStatusTextChanged(EventArgs.Empty);
				}
			}
		}
		string _StatusText;



		/// <summary>
		/// Gets the title of the document loaded into the web browser.
		/// </summary>
		[Browsable(false), DefaultValue("")]
		public string DocumentTitle
		{
			get { return _DocumentTitle ?? ""; }
			private set
			{
				if (DocumentTitle == value) return;
				_DocumentTitle = value;
				OnDocumentTitleChanged(EventArgs.Empty);
			}
		}
		string _DocumentTitle;



		/// <summary>
		/// Gets whether the browser may navigate back in the history.
		/// </summary>
		[BrowsableAttribute(false)]
		public bool CanGoBack
		{
			get { return _CanGoBack; }
		}
		bool _CanGoBack;

		/// <summary>
		/// Gets whether the browser may navigate forward in the history.
		/// </summary>
		[BrowsableAttribute(false)]
		public bool CanGoForward
		{
			get { return _CanGoForward; }
		}
		bool _CanGoForward;



		/// <summary>Raises the CanGoBackChanged or CanGoForwardChanged events when necessary.</summary>
		void UpdateCommandStatus()
		{
			bool canGoBack = false;
			bool canGoForward = false;
			if (WebNav != null)
			{
				canGoBack = WebNav.GetCanGoBackAttribute();
				canGoForward = WebNav.GetCanGoForwardAttribute();
			}

			if (_CanGoBack != canGoBack)
			{
				_CanGoBack = canGoBack;
				OnCanGoBackChanged(EventArgs.Empty);
			}

			if (_CanGoForward != canGoForward)
			{
				_CanGoForward = canGoForward;
				OnCanGoForwardChanged(EventArgs.Empty);
			}
		}

		/// <summary>
		/// Navigates to the previous page in the history, if one is available.
		/// </summary>
		/// <returns></returns>
		public bool GoBack()
		{
			if (!CanGoBack)
				return false;
			BeginInvoke(new Action(() =>
			{
				try
				{
					WebNav.GoBack();
				}
				catch (COMException ex)
				{
					int i = History.Index;
					string url = i > 0 ? History[i - 1].Url.ToString() : "";
					this.OnNavigationError(new GeckoNavigationErrorEventArgs(url, Window, ex.ErrorCode));
				}
			}));
			return true;
		}

		/// <summary>
		/// Navigates to the next page in the history, if one is available.
		/// </summary>
		/// <returns></returns>
		public bool GoForward()
		{
			if (!CanGoForward)
				return false;
			BeginInvoke(new Action(() =>
			{
				try
				{
					WebNav.GoForward();
				}
				catch (COMException ex)
				{
					int i = History.Index;
					string url = i < History.Count - 1 ? History[i + 1].Url.ToString() : "";
					this.OnNavigationError(new GeckoNavigationErrorEventArgs(url, Window, ex.ErrorCode));
				}
			}));
			return true;
		}

		/// <summary>
		/// Cancels any pending navigation and also stops any sound or animation.
		/// </summary>
		public void Stop()
		{
			// We want Stop() to return immediately and to fire events asynchronously. Howerver,
			// WebNav.Stop() may fire 'NavigationError' event synchronously, so we call it asynchronously.
			BeginInvoke(new Action(() =>
			{
				if (WebNav == null)
					return;
				try
				{
					WebNav.Stop((int)nsIWebNavigationConsts.STOP_ALL);
				}
				catch (COMException ex)
				{
					if (ex.ErrorCode == GeckoError.NS_ERROR_UNEXPECTED)
						return;
					throw;
				}
			}));
		}

		/// <summary>
		/// Reloads the current page.
		/// </summary>
		/// <returns></returns>
		public bool Reload()
		{
			return Reload(GeckoLoadFlags.None);
		}

		/// <summary>
		/// Reloads the current page using the specified flags.
		/// </summary>
		/// <param name="flags"></param>
		/// <returns></returns>
		public bool Reload(GeckoLoadFlags flags)
		{
			// We want Reload() to return immediately and to fire events asynchronously.
			BeginInvoke(new Action(() =>
			{
				try
				{
					WebNav.Reload((uint)flags);
				}
				catch (COMException e)
				{
					OnNavigationError(new GeckoNavigationErrorEventArgs(Url.ToString(), Window, e.ErrorCode));
				}
			}));

			return true;
		}

		nsIClipboardCommands ClipboardCommands
		{
			get { return _ClipboardCommands ?? (_ClipboardCommands = Xpcom.QueryInterface<nsIClipboardCommands>(WebBrowser)); }
		}

		nsIClipboardCommands _ClipboardCommands;

		delegate bool CanPerformMethod();

		bool CanPerform(CanPerformMethod method)
		{
			// in xulrunner (tested on version 5.0)
			// nsIController.IsCommandEnabled("cmd_copyImageContents") can return E_FAIL when clicking on certain objects.
			// this seems to me like a xulrunner bug.
			try
			{
				return method();
			}
			catch (COMException e)
			{
				if ((e.ErrorCode & 0xFFFFFFFF) != 0x80004005)
					throw e;

				return false;
			}
		}

		/// <summary>
		/// Gets whether the image contents of the selection may be copied to the clipboard as an image.
		/// </summary>
		[Browsable(false)]
		public bool CanCopyImageContents
		{
			get { return CanPerform(ClipboardCommands.CanCopyImageContents); }
		}

		/// <summary>
		/// Copies the image contents of the selection to the clipboard as an image.
		/// </summary>
		/// <returns></returns>
		public bool CopyImageContents()
		{
			if (CanCopyImageContents)
			{
				ClipboardCommands.CopyImageContents();
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns true if the <see cref="CopyImageLocation"/> command is enabled.
		/// </summary>
		[Browsable(false)]
		public bool CanCopyImageLocation
		{
			get { return CanPerform(ClipboardCommands.CanCopyImageLocation); }
		}

		/// <summary>
		/// Copies the location of the currently selected image to the clipboard.
		/// </summary>
		/// <returns></returns>
		public bool CopyImageLocation()
		{
			if (CanCopyImageLocation)
			{
				try
				{
					ClipboardCommands.CopyImageLocation();
				}
				catch (COMException comException)
				{
					if ((comException.ErrorCode & 0xFFFFFFFF) != 0x80004005)
						throw comException;
				}

				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns true if the <see cref="CopyLinkLocation"/> command is enabled.
		/// </summary>
		[Browsable(false)]
		public bool CanCopyLinkLocation
		{
			get { return CanPerform(ClipboardCommands.CanCopyLinkLocation); }
		}

		/// <summary>
		/// Copies the location of the currently selected link to the clipboard.
		/// </summary>
		/// <returns></returns>
		public bool CopyLinkLocation()
		{
			if (CanCopyLinkLocation)
			{
				ClipboardCommands.CopyLinkLocation();
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns true if the <see cref="CopySelection"/> command is enabled.
		/// </summary>
		[Browsable(false)]
		public bool CanCopySelection
		{
			get { return CanPerform(ClipboardCommands.CanCopySelection); }
		}

		/// <summary>
		/// Copies the selection to the clipboard.
		/// </summary>
		/// <returns></returns>
		public bool CopySelection()
		{
			if (CanCopySelection)
			{
				ClipboardCommands.CopySelection();
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns true if the <see cref="CutSelection"/> command is enabled.
		/// </summary>
		[Browsable(false)]
		public bool CanCutSelection
		{
			get { return CanPerform(ClipboardCommands.CanCutSelection); }
		}

		/// <summary>
		/// Cuts the selection to the clipboard.
		/// </summary>
		/// <returns></returns>
		public bool CutSelection()
		{
			if (CanCutSelection)
			{
				ClipboardCommands.CutSelection();
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns true if the <see cref="Paste"/> command is enabled.
		/// </summary>
		[Browsable(false)]
		public bool CanPaste
		{
			get { return CanPerform(ClipboardCommands.CanPaste); }
		}

		/// <summary>
		/// Pastes the contents of the clipboard at the current selection.
		/// </summary>
		/// <returns></returns>
		public bool Paste()
		{
			if (CanPaste)
			{
				ClipboardCommands.Paste();
				return true;
			}
			return false;
		}

		/// <summary>
		/// Selects the entire document.
		/// </summary>
		/// <returns></returns>
		public void SelectAll()
		{
			ClipboardCommands.SelectAll();
		}

		/// <summary>
		/// Empties the current selection.
		/// </summary>
		/// <returns></returns>
		public void SelectNone()
		{
			ClipboardCommands.SelectNone();
		}

		nsICommandManager CommandManager
		{
			get { return _CommandManager ?? (_CommandManager = Xpcom.QueryInterface<nsICommandManager>(WebBrowser)); }
		}
		nsICommandManager _CommandManager;

		/// <summary>
		/// Returns true if the undo command is enabled.
		/// </summary>
		[Browsable(false)]
		public bool CanUndo
		{
			get { return CommandManager.IsCommandEnabled("cmd_undo", null); }
		}

		/// <summary>
		/// Undoes last executed command.
		/// </summary>
		/// <returns></returns>
		public bool Undo()
		{
			if (CanUndo)
			{
				CommandManager.DoCommand("cmd_undo", null, null);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns true if the redo command is enabled.
		/// </summary>
		[Browsable(false)]
		public bool CanRedo
		{
			get { return CommandManager.IsCommandEnabled("cmd_redo", null); }
		}

		/// <summary>
		/// Redoes last undone command.
		/// </summary>
		/// <returns></returns>
		public bool Redo()
		{
			if (CanRedo)
			{
				CommandManager.DoCommand("cmd_redo", null, null);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Executes the command with the specified name.
		/// </summary>
		/// <param name="name">The name of the command to execute.  See http://developer.mozilla.org/en/docs/Editor_Embedding_Guide for a list of available commands.</param>
		public void ExecuteCommand(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentException("name");

			CommandManager.DoCommand(name, null, null);
		}

		public void ExecuteCommand(string name, string pname, string pvalue)
		{
			CommandParams = Xpcom.CreateInstance<nsICommandParams>("@mozilla.org/embedcomp/command-params;1");
			if (string.IsNullOrEmpty(name))
				throw new ArgumentException("name");

			CommandParams.SetCStringValue(pname, pvalue);
			if (CommandManager.IsCommandEnabled(name, null))
			{
				CommandManager.DoCommand(name, CommandParams, null);
			}
		}

		/// <summary>
		/// Gets the <see cref="Url"/> currently displayed in the web browser.
		/// Use the <see cref="Navigate(string)"/> method to change the URL.
		/// </summary>
		[BrowsableAttribute(false)]
		public Uri Url
		{
			get
			{
				if (WebNav == null)
					return null;

				nsIURI locationComObject = WebNav.GetCurrentURIAttribute();
				var uri = locationComObject.ToUri();
				Xpcom.FreeComObject(ref locationComObject);
				return uri ?? new Uri("about:blank");
			}
		}

		/// <summary>
		/// Gets the <see cref="Url"/> of the current page's referrer.
		/// </summary>
		[BrowsableAttribute(false)]
		public Uri ReferrerUrl
		{
			get
			{
				if (WebNav == null)
					return null;

				nsIURI location = WebNav.GetReferringURIAttribute();
				var uri = location.ToUri();
				Xpcom.FreeComObject(ref location);
				return uri ?? new Uri("about:blank");
			}
		}

		/// <summary>
		/// Gets the <see cref="GeckoWindow"/> object for this browser.
		/// </summary>
		[Browsable(false)]
		public GeckoWindow Window
		{
			get
			{
				if (WebBrowser == null)
					return null;

				if (_Window != null)
				{
					var window = WebBrowser.GetContentDOMWindowAttribute();
					if (_Window.DomWindow == window)
						return _Window;
					_Window.Dispose();
				}
				_Window = WebBrowser.GetContentDOMWindowAttribute().Wrap(x => new GeckoWindow(x));
				return _Window;
			}
		}

		/// <summary>
		/// Gets the <see cref="GeckoDocument"/> for the page currently loaded in the browser.
		/// </summary>
		[Browsable(false)]
		public GeckoDomDocument DomDocument
		{
			get
			{
				if (WebBrowser == null)
					return null;

				if (_Document != null)
				{
					var domDocument = Window.DomWindow.GetDocumentAttribute();
					if (_Document.NativeDomDocument == domDocument)
						return _Document;
					// In some situations when ajax is used dom document wrapper is 1 per page,
					// therefore we have to create a new one.
					_Document.Dispose();
				}
				_Document = GeckoDomDocument.CreateDomDocumentWraper(Window.DomWindow.GetDocumentAttribute());
				return _Document;
			}
		}

		public GeckoDocument Document
		{
			get { return DomDocument as GeckoDocument; }
		}


		public void SetInputFocus()
		{

		}

		public void RemoveInputFocus()
		{

		}

		public bool HasInputFocus()
		{
			return false;
		}

		/// <summary>
		/// Gets whether the browser is busy loading a page.
		/// </summary>
		[Browsable(false)]
		public bool IsBusy
		{
			get { return _IsBusy; }
			private set { _IsBusy = value; }
		}
		bool _IsBusy;



		/// <summary>
		/// Saves the current document to the specified file name.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public void SaveDocument(string path)
		{
			SaveDocument(path, null);
		}


		/// <summary>
		/// Saves the current document to the specified file name.
		/// </summary>
		/// <param name="path">Patht to the file you want to save</param>
		/// <param name="outputMimeType">the mimmtype of the save file, or null</param>
		/// <returns></returns>
		public void SaveDocument(string path, string outputMimeType)//hatton added mimeType param
		{
			if (!Directory.Exists(Path.GetDirectoryName(path)))
				throw new System.IO.DirectoryNotFoundException();
			else if (this.Document == null)
				throw new InvalidOperationException("No document has been loaded into the web browser.");

			//If the file is locked, we'd like to throw a nice .net error, not just the E_FAIL we get from COM
			//Also, this seems to get us through with no error, more often.
			if (File.Exists(path))
			{
				File.Delete(path);
			}


			nsIWebBrowserPersist persist = Xpcom.QueryInterface<nsIWebBrowserPersist>(WebBrowser);
			if (persist != null)
			{
				persist.SaveDocument(DomDocument._domDocument, (nsISupports)Xpcom.NewNativeLocalFile(path), null,
					outputMimeType, 0, 0);
			}
			else
			{
				throw new InvalidOperationException();
			}
		}

		/// <summary>
		/// Gets the session history for the current browser.
		/// </summary>
		[Browsable(false)]
		public GeckoSessionHistory History
		{
			get
			{
				if (WebNav == null)
					return null;

				return _History ?? (_History = new GeckoSessionHistory(WebNav));
			}
		}
		GeckoSessionHistory _History;

		public GeckoMarkupDocumentViewer GetMarkupDocumentViewer()
		{
			if (WebNav == null)
				return null;

			nsIDocShell shell = Xpcom.QueryInterface<nsIDocShell>(WebNav);
			nsIContentViewer contentViewer = shell.GetContentViewerAttribute();
			Marshal.ReleaseComObject(shell);

			return new GeckoMarkupDocumentViewer((nsIMarkupDocumentViewer)contentViewer);
		}

		#region nsIWebBrowserChrome Members

		void nsIWebBrowserChrome.SetStatus(uint statusType, string status)
		{
			this.StatusText = status;
		}

		nsIWebBrowser nsIWebBrowserChrome.GetWebBrowserAttribute()
		{
			return this.WebBrowser;
		}

		void nsIWebBrowserChrome.SetWebBrowserAttribute(nsIWebBrowser webBrowser)
		{
			this.WebBrowser = webBrowser;
		}

		System.UInt32 nsIWebBrowserChrome.GetChromeFlagsAttribute()
		{
			return this.ChromeFlags;
		}

		void nsIWebBrowserChrome.SetChromeFlagsAttribute(uint flags)
		{
			this.ChromeFlags = (uint)flags;
		}

		void nsIWebBrowserChrome.DestroyBrowserWindow()
		{
			//throw new NotImplementedException();
			OnWindowClosed(EventArgs.Empty);
		}

		void nsIWebBrowserChrome.SizeBrowserTo(int cx, int cy)
		{
			OnWindowSetBounds(new GeckoWindowSetBoundsEventArgs(new Rectangle(0, 0, cx, cy), BoundsSpecified.Size));
		}

		void nsIWebBrowserChrome.ShowAsModal()
		{
			//throw new NotImplementedException();
			Debug.WriteLine("ShowAsModal");

			_IsWindowModal = true;
		}
		bool _IsWindowModal;

		bool nsIWebBrowserChrome.IsWindowModal()
		{
			//throw new NotImplementedException();
			Debug.WriteLine("IsWindowModal");

			return _IsWindowModal;
		}

		void nsIWebBrowserChrome.ExitModalEventLoop(int status)
		{
			//throw new NotImplementedException();
			Debug.WriteLine("ExitModalEventLoop");

			_IsWindowModal = false;
		}

		#endregion

		#region nsIContextMenuListener2 Members

		void nsIContextMenuListener2.OnShowContextMenu(uint aContextFlags, nsIContextMenuInfo info)
		{
			// if we don't have a target node, we can't do anything by default.  this happens in XUL forms (i.e. about:config)
			if (info.GetTargetNodeAttribute() == null)
				return;

			ContextMenu menu = new ContextMenu();

			// no default items are added when the context menu is disabled
			if (!this.NoDefaultContextMenu)
			{
				List<MenuItem> optionals = new List<MenuItem>();

				if (this.CanUndo || this.CanRedo)
				{
					optionals.Add(new MenuItem("Undo", delegate { Undo(); }));
					optionals.Add(new MenuItem("Redo", delegate { Redo(); }));

					optionals[0].Enabled = this.CanUndo;
					optionals[1].Enabled = this.CanRedo;
				}
				else
				{
					optionals.Add(new MenuItem("Back", delegate { GoBack(); }));
					optionals.Add(new MenuItem("Forward", delegate { GoForward(); }));

					optionals[0].Enabled = this.CanGoBack;
					optionals[1].Enabled = this.CanGoForward;
				}

				optionals.Add(new MenuItem("-"));

				if (this.CanCopyImageContents)
					optionals.Add(new MenuItem("Copy Image Contents", delegate { CopyImageContents(); }));

				if (this.CanCopyImageLocation)
					optionals.Add(new MenuItem("Copy Image Location", delegate { CopyImageLocation(); }));

				if (this.CanCopyLinkLocation)
					optionals.Add(new MenuItem("Copy Link Location", delegate { CopyLinkLocation(); }));

				if (this.CanCopySelection)
					optionals.Add(new MenuItem("Copy Selection", delegate { CopySelection(); }));

				MenuItem mnuSelectAll = new MenuItem("Select All");
				mnuSelectAll.Click += delegate { SelectAll(); };

				GeckoDomDocument doc = GeckoDomDocument.CreateDomDocumentWraper(info.GetTargetNodeAttribute().GetOwnerDocumentAttribute());

				string viewSourceUrl = (doc == null) ? null : Convert.ToString(doc.Uri);

				MenuItem mnuViewSource = new MenuItem("View Source");
				mnuViewSource.Enabled = !string.IsNullOrEmpty(viewSourceUrl);
				mnuViewSource.Click += delegate { ViewSource(viewSourceUrl); };

				MenuItem mnuOpenInSystemBrowser = new MenuItem("View In System Browser");//nice for debugging with firefox/firebug
				mnuOpenInSystemBrowser.Enabled = !string.IsNullOrEmpty(viewSourceUrl);
				mnuOpenInSystemBrowser.Click += delegate { ViewInSystemBrowser(viewSourceUrl); };


				string properties = (doc != null && doc.Uri == Document.Uri) ? "Page Properties" : "IFRAME Properties";

				MenuItem mnuProperties = new MenuItem(properties);
				mnuProperties.Enabled = doc != null;
				mnuProperties.Click += delegate { ShowPageProperties(doc); };

				menu.MenuItems.AddRange(optionals.ToArray());
				menu.MenuItems.Add(mnuSelectAll);
				menu.MenuItems.Add("-");
				menu.MenuItems.Add(mnuViewSource);
				menu.MenuItems.Add(mnuOpenInSystemBrowser);
				menu.MenuItems.Add(mnuProperties);
			}

			// get image urls
			Uri backgroundImageSrc = null, imageSrc = null;
			nsIURI src;
			try
			{
				src = info.GetBackgroundImageSrcAttribute();
				backgroundImageSrc = src.ToUri();
				Marshal.ReleaseComObject(src);
			}
			catch (COMException comException)
			{
				if ((comException.ErrorCode & 0xFFFFFFFF) != 0x80004005)
					throw comException;
			}

			try
			{
				src = info.GetImageSrcAttribute();
				if (src != null)
				{
					imageSrc = src.ToUri();
					Marshal.ReleaseComObject(src);
				}
			}
			catch (COMException comException)
			{
				if ((comException.ErrorCode & 0xFFFFFFFF) != 0x80004005)
					throw comException;
			}

			// get associated link.  note that this needs to be done manually because GetAssociatedLink returns a non-zero
			// result when no associated link is available, so an exception would be thrown by nsString.Get()
			string associatedLink = null;
			try
			{
				using (nsAString str = new nsAString())
				{
					info.GetAssociatedLinkAttribute(str);
					associatedLink = str.ToString();
				}
			}
			catch (COMException comException) { }

			GeckoContextMenuEventArgs e = new GeckoContextMenuEventArgs(
				PointToClient(MousePosition), menu, associatedLink, backgroundImageSrc, imageSrc,
				GeckoNode.Create(Xpcom.QueryInterface<nsIDOMNode>(info.GetTargetNodeAttribute()))
				);

			OnShowContextMenu(e);

			if (e.ContextMenu != null && e.ContextMenu.MenuItems.Count > 0)
			{
				e.ContextMenu.Show(this, e.Location);
			}
		}

		private void ViewInSystemBrowser(string url)
		{
			Process.Start(url);
		}

		/// <summary>
		/// Opens a new window which contains the source code for the current page.
		/// </summary>
		public void ViewSource()
		{
			ViewSource(Url.ToString());
		}

		/// <summary>
		/// Opens a new window which contains the source code for the specified page.
		/// </summary>
		/// <param name="url"></param>
		public void ViewSource(string url)
		{
			Form form = new Form();
			form.Text = "View Source";
			GeckoWebBrowser browser = new GeckoWebBrowser();
			browser.Dock = DockStyle.Fill;
			form.Controls.Add(browser);
			form.Load += delegate { browser.Navigate("view-source:" + url); };
			try
			{
#warning when geckoFX is used in WPF application there are no forms. We should rewrite this code
				var outerForm = FindForm();
				if (outerForm != null)
				{
					form.Icon = outerForm.Icon;
				}
			}
			catch (Exception)
			{

			}

			form.ClientSize = this.ClientSize;
			form.StartPosition = FormStartPosition.CenterParent;
			form.Show();
		}

		/// <summary>
		/// Displays a properties dialog for the current page.
		/// </summary>
		public void ShowPageProperties()
		{
			ShowPageProperties(Document);
		}

		public void ShowPageProperties(GeckoDomDocument document)
		{
			if (document == null)
				throw new ArgumentNullException("document");

			new PropertiesDialog((nsIDOMDocument)document.DomObject).ShowDialog(this);
		}

		#endregion

		#region nsIInterfaceRequestor Members

		IntPtr nsIInterfaceRequestor.GetInterface(ref Guid uuid)
		{
			object obj = this;

			// note: when a new window is created, gecko calls GetInterface on the webbrowser to get a DOMWindow in order
			// to set the starting url
			if (this.WebBrowser != null)
			{
				if (uuid == typeof(nsIDOMWindow).GUID)
				{
					obj = this.WebBrowser.GetContentDOMWindowAttribute();
				}
				else if (uuid == typeof(nsIDOMDocument).GUID)
				{
					obj = this.WebBrowser.GetContentDOMWindowAttribute().GetDocumentAttribute();
				}
			}

			IntPtr ppv, pUnk = Marshal.GetIUnknownForObject(obj);

			Marshal.QueryInterface(pUnk, ref uuid, out ppv);

			Marshal.Release(pUnk);

			return ppv;
		}

		#endregion

		#region nsIEmbeddingSiteWindow Members

		void nsIEmbeddingSiteWindow.SetDimensions(uint flags, int x, int y, int cx, int cy)
		{
			const int DIM_FLAGS_POSITION = 1;
			const int DIM_FLAGS_SIZE_INNER = 2;
			const int DIM_FLAGS_SIZE_OUTER = 4;

			BoundsSpecified specified = 0;
			if ((flags & DIM_FLAGS_POSITION) != 0)
			{
				specified |= BoundsSpecified.Location;
			}
			if ((flags & DIM_FLAGS_SIZE_INNER) != 0 || (flags & DIM_FLAGS_SIZE_OUTER) != 0)
			{
				specified |= BoundsSpecified.Size;
			}

			OnWindowSetBounds(new GeckoWindowSetBoundsEventArgs(new Rectangle(x, y, cx, cy), specified));
		}

		unsafe void nsIEmbeddingSiteWindow.GetDimensions(uint flags, int* x, int* y, int* cx, int* cy)
		{
			int localX = (x != (void*)0) ? *x : 0;
			int localY = (y != (void*)0) ? *y : 0;
			int localCX = 0;
			int localCY = 0;
			if (!IsDisposed)
			{
				if ((flags & nsIEmbeddingSiteWindowConstants.DIM_FLAGS_POSITION) != 0)
				{
					Point pt = PointToScreen(Point.Empty);
					localX = pt.X;
					localY = pt.Y;
				}
				localCX = ClientSize.Width;
				localCY = ClientSize.Height;

				if ((this.ChromeFlags & (int)GeckoWindowFlags.OpenAsChrome) != 0)
				{
					BaseWindow.GetSize(ref localCX, ref localCY);
				}

				if ((flags & nsIEmbeddingSiteWindowConstants.DIM_FLAGS_SIZE_INNER) == 0)
				{
					Control topLevel = TopLevelControl;
					if (topLevel != null)
					{
						Size nonClient = new Size(topLevel.Width - ClientSize.Width, topLevel.Height - ClientSize.Height);
						localCX += nonClient.Width;
						localCY += nonClient.Height;
					}
				}
			}
			if (x != (void*)0) *x = localX;
			if (y != (void*)0) *y = localY;
			if (cx != (void*)0) *cx = localCX;
			if (cy != (void*)0) *cy = localCY;
		}

		void nsIEmbeddingSiteWindow.SetFocus()
		{
			Focus();
			if (BaseWindow != null)
			{
				BaseWindow.SetFocus();
			}
		}

		bool nsIEmbeddingSiteWindow.GetVisibilityAttribute()
		{
			return Visible;
		}

		void nsIEmbeddingSiteWindow.SetVisibilityAttribute(bool aVisibility)
		{
			//if (aVisibility)
			//{
			//      Form form = FindForm();
			//      if (form != null)
			//      {
			//            form.Visible = aVisibility;
			//      }
			//}

			Visible = aVisibility;
		}

		string nsIEmbeddingSiteWindow.GetTitleAttribute()
		{
			return DocumentTitle;
		}

		void nsIEmbeddingSiteWindow.SetTitleAttribute(string aTitle)
		{
			DocumentTitle = aTitle;
		}

		void nsIEmbeddingSiteWindow.Blur()
		{
			// TODO: implement.
		}

		#endregion

		#region nsIWebProgressListener Members

		void nsIWebProgressListener.OnStateChange(nsIWebProgress aWebProgress, nsIRequest aRequest, uint aStateFlags, int aStatus)
		{
			const int NS_BINDING_ABORTED = unchecked((int)0x804B0002);

			#region validity checks
			// The request parametere may be null
			if (aRequest == null)
				return;

			// Ignore ViewSource requests, they don't provide the URL
			// see: http://mxr.mozilla.org/mozilla-central/source/netwerk/protocol/viewsource/nsViewSourceChannel.cpp#114
			{
				var viewSource = Xpcom.QueryInterface<nsIViewSourceChannel>(aRequest);
				if (viewSource != null)
				{
					Marshal.ReleaseComObject(viewSource);
					return;
				}
			}

			#endregion validity checks

			using (var request = Gecko.Net.Request.CreateRequest(aRequest))
			{
				#region request parameters
				Uri destUri = null;
				Uri.TryCreate(request.Name, UriKind.Absolute, out destUri);
				var domWindow = aWebProgress.GetDOMWindowAttribute().Wrap(x => new GeckoWindow(x));

				/* This flag indicates that the state transition is for a request, which includes but is not limited to document requests.
				 * Other types of requests, such as requests for inline content (for example images and stylesheets) are considered normal requests.
				 */
				bool stateIsRequest = ((aStateFlags & nsIWebProgressListenerConstants.STATE_IS_REQUEST) != 0);

				/* This flag indicates that the state transition is for a document request. This flag is set in addition to STATE_IS_REQUEST.
				 * A document request supports the nsIChannel interface and its loadFlags attribute includes the nsIChannel ::LOAD_DOCUMENT_URI flag.
				 * A document request does not complete until all requests associated with the loading of its corresponding document have completed.
				 * This includes other document requests (for example corresponding to HTML <iframe> elements).
				 * The document corresponding to a document request is available via the DOMWindow attribute of onStateChange()'s aWebProgress parameter.
				 */
				bool stateIsDocument = ((aStateFlags & nsIWebProgressListenerConstants.STATE_IS_DOCUMENT) != 0);

				/* This flag indicates that the state transition corresponds to the start or stop of activity in the indicated nsIWebProgress instance.
				 * This flag is accompanied by either STATE_START or STATE_STOP, and it may be combined with other State Type Flags.
				 * 
				 * Unlike STATE_IS_WINDOW, this flag is only set when activity within the nsIWebProgress instance being observed starts or stops.
				 * If activity only occurs in a child nsIWebProgress instance, then this flag will be set to indicate the start and stop of that activity.
				 * For example, in the case of navigation within a single frame of a HTML frameset, a nsIWebProgressListener instance attached to the
				 * nsIWebProgress of the frameset window will receive onStateChange() calls with the STATE_IS_NETWORK flag set to indicate the start and
				 * stop of said navigation. In other words, an observer of an outer window can determine when activity, that may be constrained to a
				 * child window or set of child windows, starts and stops.
				 */
				bool stateIsNetwork = ((aStateFlags & nsIWebProgressListenerConstants.STATE_IS_NETWORK) != 0);

				/* This flag indicates that the state transition corresponds to the start or stop of activity in the indicated nsIWebProgress instance.
				 * This flag is accompanied by either STATE_START or STATE_STOP, and it may be combined with other State Type Flags.
				 * This flag is similar to STATE_IS_DOCUMENT. However, when a document request completes, two onStateChange() calls with STATE_STOP are generated.
				 * The document request is passed as aRequest to both calls. The first has STATE_IS_REQUEST and STATE_IS_DOCUMENT set, and the second has
				 * the STATE_IS_WINDOW flag set (and possibly the STATE_IS_NETWORK flag set as well -- see above for a description of when the STATE_IS_NETWORK
				 * flag may be set). This second STATE_STOP event may be useful as a way to partition the work that occurs when a document request completes.
				 */
				bool stateIsWindow = ((aStateFlags & nsIWebProgressListenerConstants.STATE_IS_WINDOW) != 0);
				#endregion request parameters

				#region STATE_START
				/* This flag indicates the start of a request.
				 * This flag is set when a request is initiated.
				 * The request is complete when onStateChange() is called for the same request with the STATE_STOP flag set.
				 */
				if ((aStateFlags & nsIWebProgressListenerConstants.STATE_START) != 0)
				{

					// TODO: replace to aWebProgress.GetIsTopLevelAttribute() // Gecko 24+
					if (stateIsNetwork && domWindow.IsTopWindow())
					{
						IsBusy = true;

						GeckoNavigatingEventArgs ea = new GeckoNavigatingEventArgs(destUri, domWindow);
						OnNavigating(ea);

						if (ea.Cancel)
						{
							aRequest.Cancel(NS_BINDING_ABORTED);
							//TODO: change the following handling of cancelled request

							// clear busy state
							IsBusy = false;

							// clear progress bar
							OnProgressChanged(new GeckoProgressEventArgs(100, 100));

							// clear status bar
							StatusText = "";
						}
					}
					else if (stateIsDocument)
					{
						GeckoNavigatingEventArgs ea = new GeckoNavigatingEventArgs(destUri, domWindow);
						OnFrameNavigating(ea);

						if (ea.Cancel)
						{
							// TODO: test it on Linux
							if (!Xpcom.IsLinux)
								aRequest.Cancel(NS_BINDING_ABORTED);
						}
					}
				}
				#endregion STATE_START

				#region STATE_REDIRECTING
				/* This flag indicates that a request is being redirected.
				 * The request passed to onStateChange() is the request that is being redirected.
				 * When a redirect occurs, a new request is generated automatically to process the new request.
				 * Expect a corresponding STATE_START event for the new request, and a STATE_STOP for the redirected request.
				 */
				else if ((aStateFlags & nsIWebProgressListenerConstants.STATE_REDIRECTING) != 0)
				{

					// make sure we're loading the top-level window
					GeckoRedirectingEventArgs ea = new GeckoRedirectingEventArgs(destUri, domWindow);
					OnRedirecting(ea);

					if (ea.Cancel)
					{
						aRequest.Cancel(NS_BINDING_ABORTED);
					}
				}
				#endregion STATE_REDIRECTING

				#region STATE_TRANSFERRING
				/* This flag indicates that data for a request is being transferred to an end consumer.
				 * This flag indicates that the request has been targeted, and that the user may start seeing content corresponding to the request.
				 */
				else if ((aStateFlags & nsIWebProgressListenerConstants.STATE_TRANSFERRING) != 0)
				{
				}
				#endregion STATE_TRANSFERRING

				#region STATE_STOP
				/* This flag indicates the completion of a request.
				 * The aStatus parameter to onStateChange() indicates the final status of the request.
				 */
				else if ((aStateFlags & nsIWebProgressListenerConstants.STATE_STOP) != 0)
				{
					/* aStatus
					 * Error status code associated with the state change.
					 * This parameter should be ignored unless aStateFlags includes the STATE_STOP bit.
					 * The status code indicates success or failure of the request associated with the state change.
					 * 
					 * Note: aStatus may be a success code even for server generated errors, such as the HTTP 404 File Not Found error.
					 * In such cases, the request itself should be queried for extended error information (for example for HTTP requests see nsIHttpChannel).
					 */

					if (stateIsNetwork)
					{
						// clear busy state
						IsBusy = false;
						if (aStatus == 0)
						{
							// navigating to a unrenderable file (.zip, .exe, etc.) causes the request pending;
							// also an OnStateChange call with aStatus:804B0004(NS_BINDING_RETARGETED) has been generated previously.
							if (!request.IsPending)
							{
								// kill any cached document and raise DocumentCompleted event
								OnDocumentCompleted(new GeckoDocumentCompletedEventArgs(destUri, domWindow));

								// clear progress bar
								OnProgressChanged(new GeckoProgressEventArgs(100, 100));
							}
						}
						else
						{
							OnNavigationError(new GeckoNavigationErrorEventArgs(request.Name, domWindow, aStatus));
						}
						// clear status bar
						StatusText = "";
					}

					if (stateIsRequest)
					{
						if ((aStatus & 0xff0000) == ((GeckoError.NS_ERROR_MODULE_SECURITY + GeckoError.NS_ERROR_MODULE_BASE_OFFSET) << 16))
						{
							var ea = new GeckoNSSErrorEventArgs(destUri, aStatus);
							OnNSSError(ea);
							if (ea.Handled)
							{
								aRequest.Cancel(GeckoError.NS_BINDING_ABORTED);
							}
						}

						if (aStatus == GeckoError.NS_BINDING_RETARGETED)
						{
							GeckoRetargetedEventArgs ea = new GeckoRetargetedEventArgs(destUri, domWindow, request);
							OnRetargeted(ea);
						}
					}
				}
				#endregion STATE_STOP
				if (domWindow != null)
				{
					domWindow.Dispose();
				}
			}
		}

		void nsIWebProgressListener.OnProgressChange(nsIWebProgress aWebProgress, nsIRequest aRequest, int aCurSelfProgress, int aMaxSelfProgress, int aCurTotalProgress, int aMaxTotalProgress)
		{
			// Note: If any progress value is unknown, then its value is replaced with -1.
			using (var request = Gecko.Net.Request.CreateRequest(aRequest))
			{
				if ((aCurSelfProgress != -1) && (aMaxSelfProgress != -1))
					OnRequestProgressChanged(new GeckoRequestProgressEventArgs(aCurTotalProgress, aMaxTotalProgress, aRequest));

				if ((aCurTotalProgress != -1) && (aMaxTotalProgress != -1))
					OnProgressChanged(new GeckoProgressEventArgs(aCurTotalProgress, aMaxTotalProgress));
			}
		}

		void nsIWebProgressListener.OnLocationChange(nsIWebProgress aWebProgress, nsIRequest aRequest, nsIURI aLocation, uint flags)
		{
			if (IsDisposed) return;

			Uri uri = new Uri(nsString.Get(aLocation.GetSpecAttribute));
			using (var domWindow = aWebProgress.GetDOMWindowAttribute().Wrap(x => new GeckoWindow(x)))
			{

				bool sameDocument = (flags & nsIWebProgressListenerConstants.LOCATION_CHANGE_SAME_DOCUMENT) != 0;
				bool errorPage = (flags & nsIWebProgressListenerConstants.LOCATION_CHANGE_ERROR_PAGE) != 0;
				var ea = new GeckoNavigatedEventArgs(uri, aRequest, domWindow, sameDocument, errorPage);

				OnNavigated(ea);
			}
			UpdateCommandStatus();
		}

		void nsIWebProgressListener.OnStatusChange(nsIWebProgress aWebProgress, nsIRequest aRequest, int aStatus, string aMessage)
		{
			if (aWebProgress.GetIsLoadingDocumentAttribute())
			{
				StatusText = aMessage;
				UpdateCommandStatus();
			}
		}

		void nsIWebProgressListener.OnSecurityChange(nsIWebProgress aWebProgress, nsIRequest aRequest, uint aState)
		{
			SecurityState = (GeckoSecurityState)aState;
		}

		/// <summary>
		/// Gets a value which indicates whether the current page is secure.
		/// </summary>
		[Browsable(false)]
		public GeckoSecurityState SecurityState
		{
			get { return _SecurityState; }
			private set
			{
				if (_SecurityState == value) return;
				_SecurityState = value;
				OnSecurityStateChanged(EventArgs.Empty);
			}
		}
		GeckoSecurityState _SecurityState;


		#region public event EventHandler SecurityStateChanged
		/// <summary>
		/// Occurs when the value of the <see cref="SecurityState"/> property is changed.
		/// </summary>
		[Category("Property Changed"), Description("Occurs when the value of the SecurityState property is changed.")]
		public event EventHandler SecurityStateChanged
		{
			add { this.Events.AddHandler(SecurityStateChangedEvent, value); }
			remove { this.Events.RemoveHandler(SecurityStateChangedEvent, value); }
		}
		private static object SecurityStateChangedEvent = new object();

		/// <summary>Raises the <see cref="SecurityStateChanged"/> event.</summary>
		/// <param name="e">The data for the event.</param>
		protected virtual void OnSecurityStateChanged(EventArgs e)
		{
			if (((EventHandler)this.Events[SecurityStateChangedEvent]) != null)
				((EventHandler)this.Events[SecurityStateChangedEvent])(this, e);
		}
		#endregion

		#endregion

		#region nsIWebProgressListener2 Members

		#region implemented in nsIWebProgressListener
		void nsIWebProgressListener2.OnStateChange(nsIWebProgress aWebProgress, nsIRequest aRequest, uint aStateFlags, int aStatus)
		{
			throw new NotImplementedException("implemented in nsIWebProgressListener");
		}

		void nsIWebProgressListener2.OnProgressChange(nsIWebProgress aWebProgress, nsIRequest aRequest, int aCurSelfProgress, int aMaxSelfProgress, int aCurTotalProgress, int aMaxTotalProgress)
		{
			throw new NotImplementedException("implemented in nsIWebProgressListener");
		}

		void nsIWebProgressListener2.OnLocationChange(nsIWebProgress aWebProgress, nsIRequest aRequest, nsIURI aLocation, uint aFlags)
		{
			throw new NotImplementedException("implemented in nsIWebProgressListener");
		}

		void nsIWebProgressListener2.OnStatusChange(nsIWebProgress aWebProgress, nsIRequest aRequest, int aStatus, string aMessage)
		{
			throw new NotImplementedException("implemented in nsIWebProgressListener");
		}

		void nsIWebProgressListener2.OnSecurityChange(nsIWebProgress aWebProgress, nsIRequest aRequest, uint aState)
		{
			throw new NotImplementedException("implemented in nsIWebProgressListener");
		}
		#endregion implemented in nsIWebProgressListener

		void nsIWebProgressListener2.OnProgressChange64(nsIWebProgress aWebProgress, nsIRequest aRequest, long aCurSelfProgress, long aMaxSelfProgress, long aCurTotalProgress, long aMaxTotalProgress)
		{
			// Note: If any progress value is unknown, then its value is replaced with -1.

			if ((aCurSelfProgress != -1) && (aMaxSelfProgress != -1))
				OnRequestProgressChanged(new GeckoRequestProgressEventArgs(aCurTotalProgress, aMaxSelfProgress, aRequest));

			if ((aCurTotalProgress != -1) && (aMaxTotalProgress != -1))
				OnProgressChanged(new GeckoProgressEventArgs(aCurTotalProgress, aMaxTotalProgress));
		}

		bool nsIWebProgressListener2.OnRefreshAttempted(nsIWebProgress aWebProgress, nsIURI aRefreshURI, int aMillis, bool aSameURI)
		{
			Uri destUri = new Uri(nsString.Get(aRefreshURI.GetSpecAttribute));
			bool cancel = false;
			using (var domWindow = aWebProgress.GetDOMWindowAttribute().Wrap(x => new GeckoWindow(x)))
			{
				GeckoNavigatingEventArgs ea = new GeckoNavigatingEventArgs(destUri, domWindow);
				cancel = ea.Cancel;
			}
			//OnRefreshAttempt();
			return !cancel;
		}

		#endregion


		#region nsIDOMEventListener Members

		void nsIDOMEventListener.HandleEvent(nsIDOMEvent e)
		{
			if (e == null) return;

			OnHandleDomEvent(DomEventArgs.Create(e));
		}

		protected virtual void OnHandleDomEvent(DomEventArgs e)
		{
			switch (e.Type)
			{
				case "keydown":
					OnDomKeyDown((DomKeyEventArgs)e);
					break;
				case "keyup":
					OnDomKeyUp((DomKeyEventArgs)e);
					break;
				case "keypress":
					OnDomKeyPress((DomKeyEventArgs)e);
					break;
				case "mousedown":
					OnDomMouseDown((DomMouseEventArgs)e);
					break;
				case "mouseup":
					OnDomMouseUp((DomMouseEventArgs)e);
					break;
				case "mousemove":
					OnDomMouseMove((DomMouseEventArgs)e);
					break;
				case "mouseover":
					OnDomMouseOver((DomMouseEventArgs)e);
					break;
				case "mouseout":
					OnDomMouseOut((DomMouseEventArgs)e);
					break;
				case "click":
					OnDomClick((DomMouseEventArgs)e);
					break;
				case "dblclick":
					OnDomDoubleClick((DomMouseEventArgs)e);
					break;
				case "submit":
					OnDomSubmit(e);
					break;
				case "compositionstart":
					OnDomCompositionStart(e);
					break;
				case "compositionend":
					OnDomCompositionEnd(e);
					break;
				case "contextmenu":
					OnDomContextMenu((DomMouseEventArgs)e);
					break;
				case "DOMMouseScroll":
					OnDomMouseScroll((DomMouseEventArgs)e);
					break;
				case "focus":
					OnDomFocus(e);
					break;
				case "blur":
					OnDomBlur(e);
					break;
				case "load":
					OnLoad(e);
					break;
				case "DOMContentLoaded":
					OnDOMContentLoaded(e);
					break;
				case "readystatechange":
					OnReadyStateChange(e);
					break;
				case "change":
					OnDomContentChanged(e);
					break;
				case "hashchange":
					OnHashChange((DomHashChangeEventArgs)e);
					break;
				case "dragstart":
					OnDomDragStart((DomDragEventArgs)e);
					break;
				case "dragenter":
					OnDomDragEnter((DomDragEventArgs)e);
					break;
				case "dragover":
					OnDomDragOver((DomDragEventArgs)e);
					break;
				case "dragleave":
					OnDomDragLeave((DomDragEventArgs)e);
					break;
				case "drag":
					OnDomDrag((DomDragEventArgs)e);
					break;
				case "drop":
					OnDomDrop((DomDragEventArgs)e);
					break;
				case "dragend":
					OnDomDragEnd((DomDragEventArgs)e);
					break;
				case "mozfullscreenchange": //TODO: change to "fullscreenchange" after prefix removed
					OnFullscreenChange(e);
					break;
			}
			if (e is DomMessageEventArgs)
			{
				Action<string> action;
				DomMessageEventArgs mea = (DomMessageEventArgs)e;
				if (_messageEventListeners.TryGetValue(e.Type, out action))
				{
					action.Invoke(mea.Message);
				}
			}

			if (e != null && e.Cancelable && e.Handled)
				e.PreventDefault();
		}

		#endregion

		#region nsIWindowProvider Members
		//int nsIWindowProvider.provideWindow(nsIDOMWindow aParent, uint aChromeFlags, bool aPositionSpecified, bool aSizeSpecified, nsIURI aURI, nsAString aName, nsAString aFeatures, out bool aWindowIsNew, out nsIDOMWindow ret)
		//{
		//      if (!this.IsDisposed)
		//      {
		//            GeckoCreateWindowEventArgs e = new GeckoCreateWindowEventArgs((GeckoWindowFlags)aChromeFlags);                
		//            this.OnCreateWindow(e);

		//            if (e.WebBrowser != null)
		//            {
		//                  aWindowIsNew = true;
		//                  ret = (nsIDOMWindow)e.WebBrowser.Window.DomWindow;
		//                  return 0;
		//            }
		//            else
		//            {
		//                  System.Media.SystemSounds.Beep.Play();
		//            }
		//      }

		//      aWindowIsNew = true;
		//      ret = null;
		//      return -1;
		//}
		#endregion

		#region nsISHistoryListener Members
		void nsISHistoryListener.OnHistoryNewEntry(nsIURI aNewURI)
		{
			OnHistoryNewEntry(new GeckoHistoryEventArgs(new Uri(nsString.Get(aNewURI.GetSpecAttribute))));
		}

		bool nsISHistoryListener.OnHistoryGoBack(nsIURI aBackURI)
		{
			GeckoHistoryEventArgs e = new GeckoHistoryEventArgs(new Uri(nsString.Get(aBackURI.GetSpecAttribute)));
			OnHistoryGoBack(e);
			return !e.Cancel;
		}

		bool nsISHistoryListener.OnHistoryGoForward(nsIURI aForwardURI)
		{
			GeckoHistoryEventArgs e = new GeckoHistoryEventArgs(new Uri(nsString.Get(aForwardURI.GetSpecAttribute)));
			OnHistoryGoForward(e);
			return !e.Cancel;
		}

		bool nsISHistoryListener.OnHistoryReload(nsIURI aReloadURI, uint aReloadFlags)
		{
			GeckoHistoryEventArgs e = new GeckoHistoryEventArgs(new Uri(nsString.Get(aReloadURI.GetSpecAttribute)));
			OnHistoryReload(e);
			return !e.Cancel;
		}

		bool nsISHistoryListener.OnHistoryGotoIndex(int aIndex, nsIURI aGotoURI)
		{
			GeckoHistoryGotoIndexEventArgs e = new GeckoHistoryGotoIndexEventArgs(new Uri(nsString.Get(aGotoURI.GetSpecAttribute)), aIndex);
			OnHistoryGotoIndex(e);
			return !e.Cancel;
		}

		bool nsISHistoryListener.OnHistoryPurge(int aNumEntries)
		{
			GeckoHistoryPurgeEventArgs e = new GeckoHistoryPurgeEventArgs(aNumEntries);
			OnHistoryPurge(e);
			return !e.Cancel;
		}

		public void OnHistoryReplaceEntry(int aIndex)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region nsITooltipListener Members

		void nsITooltipListener.OnShowTooltip(int aXCoords, int aYCoords, string aTipText)
		{
			if (true.Equals(GeckoPreferences.User["browser.chrome.toolbar_tips"]))
			{
				ToolTip = new ToolTipWindow();
				ToolTip.Show(aTipText, this, new Point(aXCoords, aYCoords + 24));
			}
		}

		ToolTipWindow ToolTip;

		void nsITooltipListener.OnHideTooltip()
		{
			if (ToolTip != null)
			{
				ToolTip.Hide(this);
				ToolTip.Dispose();
				ToolTip = null;
			}

		}
		#endregion

		/// <summary>
		/// Register a listener for a custom jscrip-initiated MessageEvent
		/// https://developer.mozilla.org/en/DOM/document.createEvent
		/// http://help.dottoro.com/ljknkjqd.php
		/// </summary>
		/// <param name="eventName"></param>
		/// <param name="action"></param>
		/// <example>AddMessageEventListener("callMe", (message=>MessageBox.Show(message)));</example>
		public void AddMessageEventListener(string eventName, Action<string> action)
		{
			AddMessageEventListener(eventName, action, true);
		}

		/// <summary>
		/// Register a listener for a custom jscrip-initiated MessageEvent
		/// https://developer.mozilla.org/en/DOM/document.createEvent
		/// http://help.dottoro.com/ljknkjqd.php
		/// </summary>
		/// <param name="eventName"></param>
		/// <param name="action"></param>
		/// <example>AddMessageEventListener("callMe", (message=>MessageBox.Show(message)));</example>
		public void AddMessageEventListener(string eventName, Action<string> action, bool useCapture)
		{
			nsIDOMEventTarget target = Xpcom.QueryInterface<nsIDOMEventTarget>(Xpcom.QueryInterface<nsIDOMWindow>(WebBrowser.GetContentDOMWindowAttribute()).GetWindowRootAttribute());
			if (target != null)
			{
				// the argc parameter is the number of optionial argumetns we are passing. 
				// (useCapture and wantsUntrusted are specified as optional so we always pass 2 when calling interface from C#)
				target.AddEventListener(new nsAString(eventName), this, /*Review*/ useCapture, true, 2);
				_messageEventListeners.Add(eventName, action);
			}
		}


		public void Observe(nsISupports aSubject, string aTopic, string aData)
		{
			if (aTopic.Equals(ObserverNotifications.HttpRequests.HttpOnModifyRequest))
			{
				using (var httpChannel = HttpChannel.Create(aSubject))
				{

					var origUri = httpChannel.OriginalUri;

					var uri = httpChannel.Uri;
					var uriRef = httpChannel.Referrer;
					var reqMethod = httpChannel.RequestMethod;
					var reqHeaders = httpChannel.GetRequestHeaders();
					byte[] reqBody = null;
					bool? reqBodyContainsHeaders = null;

					#region POST data

					var uploadChannel = Xpcom.QueryInterface<nsIUploadChannel>(aSubject);
					var uploadChannel2 = Xpcom.QueryInterface<nsIUploadChannel2>(aSubject);

					if (uploadChannel != null)
					{
						var uc = new UploadChannel(uploadChannel);
						var uploadStream = uc.UploadStream;

						if (uploadStream != null)
						{
							if (uploadStream.CanSeek)
							{
								var rdr = new BinaryReader(uploadStream);
								var reqBodyStream = new MemoryStream();
								try
								{
									reqBody = new byte[] { };
									int avl = 0;
									while ((avl = ((int)uploadStream.Available)) > 0)
									{
										reqBodyStream.Write(rdr.ReadBytes(avl), 0, avl);
									}
									reqBody = reqBodyStream.ToArray();

									if (uploadChannel2 != null)
										reqBodyContainsHeaders = uploadChannel2.GetUploadStreamHasHeadersAttribute();
								}
								catch (IOException ex)
								{
									// failed to read body, ignore
								}

								// rewind stream, so browser can read it as usual
								uploadStream.Seek(0, 0);
							}
						}
					}

					#endregion POST data

					var evt = new GeckoObserveHttpModifyRequestEventArgs(uri, uriRef, reqMethod, reqBody, reqHeaders, httpChannel, reqBodyContainsHeaders);

					OnObserveHttpModifyRequest(evt);

					if (evt.Cancel)
					{
						httpChannel.Cancel(nsIHelperAppLauncherConstants.NS_BINDING_ABORTED);
					}
				}
			}
		}

		#region nsIHttpActivityObserver members

		public Dictionary<nsIHttpChannel, GeckoJavaScriptHttpChannelWrapper> origJavaScriptHttpChannels = new Dictionary<nsIHttpChannel, GeckoJavaScriptHttpChannelWrapper>();

		public bool IsAjaxBusy
		{
			get
			{
				return (origJavaScriptHttpChannels.Count > 0);
			}
		}

		public int ActiveAjaxHttpChannels
		{
			get
			{
				return origJavaScriptHttpChannels.Count;
			}
		}

		private uint activeNetworkChannels = 0;
		private List<string> activeNetworkChannelUrls = new List<string>();

		public uint ActiveNetworkChannels
		{
			get
			{
				return activeNetworkChannels;
			}
		}

		public List<string> ActiveNetworkChannelUrls
		{
			get { return activeNetworkChannelUrls; }
		}

		//public void ObserveActivity(nsISupports aHttpChannel, uint aActivityType, uint aActivitySubtype, uint aTimestamp, ulong aExtraSizeData, nsACString aExtraStringData)
		public void ObserveActivity(nsISupports aHttpChannel,
												 UInt32 aActivityType,
												 UInt32 aActivitySubtype,
												 Int64 aTimestamp,
												 UInt64 aExtraSizeData,
					 nsACStringBase aExtraStringData)
		{
			nsIHttpChannel httpChannel = Xpcom.QueryInterface<nsIHttpChannel>(aHttpChannel);

			if (httpChannel != null)
			{
				switch (aActivityType)
				{
					case nsIHttpActivityObserverConstants.ACTIVITY_TYPE_SOCKET_TRANSPORT:
						switch (aActivitySubtype)
						{
							case nsISocketTransportConstants.STATUS_RESOLVING:
								break;
							case nsISocketTransportConstants.STATUS_RESOLVED:
								break;
							case nsISocketTransportConstants.STATUS_CONNECTING_TO:
								break;
							case nsISocketTransportConstants.STATUS_CONNECTED_TO:
								break;
							case nsISocketTransportConstants.STATUS_SENDING_TO:
								break;
							case nsISocketTransportConstants.STATUS_WAITING_FOR:
								break;
							case nsISocketTransportConstants.STATUS_RECEIVING_FROM:
								break;
						}
						break;
					case nsIHttpActivityObserverConstants.ACTIVITY_TYPE_HTTP_TRANSACTION:
						switch (aActivitySubtype)
						{
							case nsIHttpActivityObserverConstants.ACTIVITY_SUBTYPE_REQUEST_HEADER:
								{
									activeNetworkChannels++;
									ActiveNetworkChannelUrls.Add(nsString.Get(httpChannel.GetURIAttribute().GetSpecAttribute));

									var callbacks = httpChannel.GetNotificationCallbacksAttribute();

									if (callbacks != null)
									{
										var httpChannelXHR = Xpcom.QueryInterface<nsIXMLHttpRequest>(callbacks);

										if (httpChannelXHR != null)
										{
											nsIXMLHttpRequestEventTarget mXMLRequestEvent = Xpcom.QueryInterface<nsIXMLHttpRequestEventTarget>(httpChannelXHR);

											if (mXMLRequestEvent != null)
											{
												GeckoJavaScriptHttpChannelWrapper mEventListener = new GeckoJavaScriptHttpChannelWrapper(this, httpChannel);
												origJavaScriptHttpChannels.Add(httpChannel, mEventListener);

												using (nsAString mLoads = new nsAString("load"))
												{
													mXMLRequestEvent.AddEventListener(mLoads, mEventListener, true, false, 0);
												}

												using (nsAString mLoads = new nsAString("abort"))
												{
													mXMLRequestEvent.AddEventListener(mLoads, mEventListener, true, false, 0);
												}

												using (nsAString mLoads = new nsAString("error"))
												{
													mXMLRequestEvent.AddEventListener(mLoads, mEventListener, true, false, 0);
												}

												Marshal.ReleaseComObject(mXMLRequestEvent);
											}

											Marshal.ReleaseComObject(httpChannelXHR);
										}

										Marshal.ReleaseComObject(callbacks);
									}

								}
								break;
							case nsIHttpActivityObserverConstants.ACTIVITY_SUBTYPE_REQUEST_BODY_SENT:
								break;
							case nsIHttpActivityObserverConstants.ACTIVITY_SUBTYPE_RESPONSE_START:
								break;
							case nsIHttpActivityObserverConstants.ACTIVITY_SUBTYPE_RESPONSE_COMPLETE:
								break;
							case nsIHttpActivityObserverConstants.ACTIVITY_SUBTYPE_TRANSACTION_CLOSE:
								activeNetworkChannels--;
								ActiveNetworkChannelUrls.Remove(nsString.Get(httpChannel.GetURIAttribute().GetSpecAttribute));
								break;
						}
						break;
				}
			}
		}

		public bool GetIsActiveAttribute()
		{
			return true;
		}
		#endregion nsIHttpActivityObserver members

		public nsIWeakReference GetWeakReference()
		{
			return new ControlWeakReference(this);
		}
	}

	#region public enum GeckoSecurityState
	public enum GeckoSecurityState
	{
		/// <summary>
		/// This flag indicates that the data corresponding to the request was received over an insecure channel.
		/// </summary>
		Insecure = unchecked((int)nsIWebProgressListenerConstants.STATE_IS_INSECURE),
		/// <summary>
		/// This flag indicates an unknown security state.  This may mean that the request is being loaded as part of
		/// a page in which some content was received over an insecure channel.
		/// </summary>
		Broken = unchecked((int)nsIWebProgressListenerConstants.STATE_IS_BROKEN),
		/// <summary>
		/// This flag indicates that the data corresponding to the request was received over a secure channel.
		/// The degree of security is expressed by GeckoSecurityStrength.
		/// </summary>
		Secure = unchecked((int)nsIWebProgressListenerConstants.STATE_IS_SECURE),
	}
	#endregion

	#region GeckoJavaScriptHttpChannelWrapper
	public class GeckoJavaScriptHttpChannelWrapper : nsIDOMEventListener
	{
		private readonly GeckoWebBrowser m_browser;
		private readonly nsIHttpChannel m_httpChannel;
		private readonly nsIXMLHttpRequest m_notificationCallsbacks;

		public GeckoJavaScriptHttpChannelWrapper(GeckoWebBrowser p_browser, nsIHttpChannel p_httpChannel)
		{
			m_browser = p_browser;
			m_httpChannel = p_httpChannel;

			m_notificationCallsbacks = Xpcom.QueryInterface<nsIXMLHttpRequest>(m_httpChannel.GetNotificationCallbacksAttribute());
		}

		public void HandleEvent(nsIDOMEvent @event)
		{
			// This is how to get the fields, but we don't need them by default since sometimes they are undefined
			//var xhr_uri = (new Uri(nsString.Get(m_httpChannel.GetOriginalURIAttribute().GetSpecAttribute))).ToString();
			//var xhr_status = m_notificationCallsbacks.GetStatusAttribute();
			var xhr_readyState = m_notificationCallsbacks.GetReadyStateAttribute();

			// remove when finished
			if (xhr_readyState == 4)
			{
				m_browser.origJavaScriptHttpChannels.Remove(m_httpChannel);
			}
		}
	}
	#endregion GeckoJavaScriptHttpChannelWrapper
}
