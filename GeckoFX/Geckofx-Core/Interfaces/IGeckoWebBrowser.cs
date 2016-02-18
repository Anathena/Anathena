using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.IO;

namespace Gecko
{
	#region public enum GeckoLoadFlags
	[Flags]
	public enum GeckoLoadFlags
	{
		/// <summary>
		/// This is the default value for the load flags parameter.
		/// </summary>
		None = (int)nsIWebNavigationConsts.LOAD_FLAGS_NONE,

		/// <summary>
		/// This flag specifies that the load should have the semantics of an HTML
		/// META refresh (i.e., that the cache should be validated).  This flag is
		/// only applicable to loadURI.
		/// XXX the meaning of this flag is poorly defined.
		/// </summary>
		IsRefresh = (int)nsIWebNavigationConsts.LOAD_FLAGS_IS_REFRESH,

		/// <summary>
		/// This flag specifies that the load should have the semantics of a link
		/// click.  This flag is only applicable to loadURI.
		/// XXX the meaning of this flag is poorly defined.
		/// </summary>
		IsLink = (int)nsIWebNavigationConsts.LOAD_FLAGS_IS_LINK,

		/// <summary>
		/// This flag specifies that history should not be updated.  This flag is only
		/// applicable to loadURI.
		/// </summary>
		BypassHistory = (int)nsIWebNavigationConsts.LOAD_FLAGS_BYPASS_HISTORY,

		/// <summary>
		/// This flag specifies that any existing history entry should be replaced.
		/// This flag is only applicable to loadURI.
		/// </summary>
		ReplaceHistory = (int)nsIWebNavigationConsts.LOAD_FLAGS_REPLACE_HISTORY,

		/// <summary>
		/// This flag specifies that the local web cache should be bypassed, but an
		/// intermediate proxy cache could still be used to satisfy the load.
		/// </summary>
		BypassCache = (int)nsIWebNavigationConsts.LOAD_FLAGS_BYPASS_CACHE,

		/// <summary>
		/// This flag specifies that any intermediate proxy caches should be bypassed
		/// (i.e., that the content should be loaded from the origin server).
		/// </summary>
		BypassProxy = (int)nsIWebNavigationConsts.LOAD_FLAGS_BYPASS_PROXY,

		/// <summary>
		/// This flag specifies that a reload was triggered as a result of detecting
		/// an incorrect character encoding while parsing a previously loaded
		/// document.
		/// </summary>
		CharsetChange = (int)nsIWebNavigationConsts.LOAD_FLAGS_CHARSET_CHANGE,

		/// <summary>
		/// If this flag is set, Stop() will be called before the load starts
		/// and will stop both content and network activity (the default is to
		/// only stop network activity).  Effectively, this passes the
		/// STOP_CONTENT flag to Stop(), in addition to the STOP_NETWORK flag.
		/// </summary>
		StopContent = (int)nsIWebNavigationConsts.LOAD_FLAGS_STOP_CONTENT,

		/// <summary>
		/// A hint this load was prompted by an external program: take care!
		/// </summary>
		FromExternal = (int)nsIWebNavigationConsts.LOAD_FLAGS_FROM_EXTERNAL,

		/// <summary>
		/// This flag specifies that the URI may be submitted to a third-party
		/// server for correction. This should only be applied to non-sensitive
		/// URIs entered by users.
		/// </summary>
		AllowThirdPartyFixup = (int)nsIWebNavigationConsts.LOAD_FLAGS_ALLOW_THIRD_PARTY_FIXUP,

		/// <summary>
		/// This flag specifies that this is the first load in this object.
		/// Set with care, since setting incorrectly can cause us to assume that
		/// nothing was actually loaded in this object if the load ends up being
		/// handled by an external application.
		/// </summary>
		FirstLoad =(int) nsIWebNavigationConsts.LOAD_FLAGS_FIRST_LOAD,
	}
	#endregion

	public interface IGeckoWebBrowser
	{
		#region Properties

		GeckoDocument Document
		{
			get;
		}

		GeckoWindow Window
		{
			get;
		}

		bool IsDisposed
		{
			get;
		}

		IntPtr Handle
		{
			get;
		}
		#endregion
	
		#region Methods

		/// <summary>
		/// Navigates to the specified URL.
		/// In order to find out when Navigate has finished attach a handler to NavigateFinishedNotifier.NavigateFinished.
		/// </summary>
		/// <param name="url">The url to navigate to.</param>
		void Navigate(string url);

		/// <summary>
		/// Navigates to the specified URL using the given load flags.
		/// In order to find out when Navigate has finished attach a handler to NavigateFinishedNotifier.NavigateFinished.
		/// </summary>
		/// <param name="url">The url to navigate to.  If the url is empty or null, the browser does not navigate and the method returns false.</param>
		/// <param name="loadFlags">Flags which specify how the page is loaded.</param>
		bool Navigate(string url, GeckoLoadFlags loadFlags);

		/// <summary>
		///  Navigates to the specified URL using the given load flags, referrer and post data
		///  In order to find out when Navigate has finished attach a handler to NavigateFinishedNotifier.NavigateFinished.
		/// </summary>
		/// <param name="url">The url to navigate to.  If the url is empty or null, the browser does not navigate and the method returns false.</param>
		/// <param name="loadFlags">Flags which specify how the page is loaded.</param>
		/// <param name="referrer">The referring URL, or null.</param>
		/// <param name="postData">post data and headers, or null</param>
		/// <returns>true if Navigate started. false otherwise.</returns>
		bool Navigate(string url, GeckoLoadFlags loadFlags, string referrer, MimeInputStream postData);

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
		bool Navigate(string url, GeckoLoadFlags loadFlags, string referrer, MimeInputStream postData, MimeInputStream headers);

		/// <summary>
		/// Navigates to the previous page in the history, if one is available.
		/// </summary>
		/// <returns></returns>
		bool GoBack();

		/// <summary>
		/// Navigates to the next page in the history, if one is available.
		/// </summary>
		/// <returns></returns>
		bool GoForward();

		/// <summary>
		/// Reloads the current page.
		/// </summary>
		/// <returns>true on success</returns>
		bool Reload();


		#endregion

		/// <summary>
		/// UI platform independent call function from UI thread
		/// </summary>
		/// <param name="action"></param>
		void UserInterfaceThreadInvoke( Action action );
		/// <summary>
		/// UI platform independent call function from UI thread
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="func"></param>
		/// <returns></returns>
		T UserInterfaceThreadInvoke<T>( Func<T> func );

		#region Events
		event EventHandler<DomEventArgs> Load;

		event EventHandler<Gecko.Events.GeckoDocumentCompletedEventArgs> DocumentCompleted;

		event EventHandler<Gecko.Events.GeckoNavigationErrorEventArgs> NavigationError;

		#endregion
	}


	#region public enum GeckoWindowFlags
	[Flags]
	public enum GeckoWindowFlags
	{
		Default = 0x1,
		WindowBorders = 0x2,
		WindowClose = 0x4,
		WindowResize = 0x8,
		MenuBar = 0x10,
		ToolBar = 0x20,
		LocationBar = 0x40,
		StatusBar = 0x80,
		PersonalToolbar = 0x100,
		ScrollBars = 0x200,
		TitleBar = 0x400,
		Extra = 0x800,

		CreateWithSize = 0x1000,
		CreateWithPosition = 0x2000,

		WindowMin = 0x00004000,
		WindowPopup = 0x00008000,
		WindowRaised = 0x02000000,
		WindowLowered = 0x04000000,
		CenterScreen = 0x08000000,
		Dependent = 0x10000000,
		Modal = 0x20000000,
		OpenAsDialog = 0x40000000,
		OpenAsChrome = unchecked( ( int ) 0x80000000 ),
		All = 0x00000ffe,
	}
	#endregion
}
