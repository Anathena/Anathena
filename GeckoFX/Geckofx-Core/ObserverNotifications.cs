namespace Gecko
{
	/// <summary>
	/// Observer notification strings
	/// Some of them will be unavaliable in GeckoFX (they may be avaliable only in XUL App or Firefox/Seamonkey)
	/// https://developer.mozilla.org/en/Observer_Notifications
	/// </summary>
	public static class ObserverNotifications
	{
		public const string Everything = "*";

		public static class ApplicationStartup
		{
			public const string XpComStartup = "xpcom-startup";
			public const string AppStartup = "app-startup";
			public const string ProfileDoChange = "profile-do-change";
			public const string ProfileAfterChange = "profile-after-change";
			public const string FinalUiStartup = "final-ui-startup";
			public const string SessionstoreWindowsRestored = "sessionstore-windows-restored";
		}

		public static class ApplicationShutdown
		{
			public const string QuitApplicationRequested = "quit-application-requested";
			public const string QuitApplicationGranted = "quit-application-granted";
			public const string QuitApplication = "quit-application";
			public const string ProfileChangeNetTeardown = "profile-change-net-teardown";
			public const string ProfileChangeTeardown = "profile-change-teardown";
			public const string ProfileBeforeChange = "profile-before-change";
			public const string XpComWillShutdown = "xpcom-will-shutdown";
			public const string XpComShutdown = "xpcom-shutdown";
		}

		public static class Browser
		{
			public const string BrowserPurgeSessionHistory = "browser:purge-session-history";
			public const string BrowserPurgeDomainData = "browser:purge-domain-data";
			public const string BrowserLastWindowCloseRequested = "browser-lastwindow-close-requested";
			public const string BrowserLastWindowCloseGranted = "browser-lastwindow-close-granted";
			public const string BrowserDelayedStartupFinished = "browser-delayed-startup-finished";
		}

		public static class Documents
		{
			/// <summary>
			/// Subject is nsIDOMWindow
			/// </summary>
			public const string ChromeDocumentGlobalCreated="chrome-document-global-created";
			/// <summary>
			/// Subject is nsIDOMWindow
			/// Data - origin
			/// </summary>
			public const string ContentDocumentGlobalCreated = "content-document-global-created";
			/// <summary>
			/// Subject is Document
			/// </summary>
			public const string DocumentElementInserted = "document-element-inserted";
			/// <summary>
			/// Subject is nsIDOMWindow
			/// </summary>
			public const string UserInteractionActive = "user-interaction-active";
			/// <summary>
			/// Subject is nsIDOMWindow
			/// </summary>
			public const string UserInteractionInactive = "user-interaction-inactive";
		}

		public static class Windows
		{
			public const string DomWindowDestroyed = "dom-window-destroyed";
			public const string InnerWindowDestroyed = "inner-window-destroyed";
			public const string OuterWindowDestroyed = "outer-window-destroyed";
			public const string TopLevelWindowReady = "toplevel-window-ready";
			public const string XulWindowDestroyed = "xul-window-destroyed";
			public const string XulWindowRegistered = "xul-window-registered";
			public const string XulWindowVisible = "xul-window-visible";
		}

		public static class SpellingChecker
		{
			public const string SpellCheckDictionaryUpdate = "spellcheck-dictionary-update";
		}

		public static class IONotifications
		{
			public const string OfflineRequested = "offline-requested";
			public const string NetworkOfflineAboutToGoOffline = "network:offline-about-to-go-offline";
			public const string NetworkOfflineStatusChanged = "network:offline-status-changed";
		}

		public static class HttpRequests
		{
			public const string HttpOnModifyRequest="http-on-modify-request";
			public const string HttpOnOpeningRequest = "http-on-opening-request";
			public const string HttpOnExamineResponse = "http-on-examine-response";
			public const string HttpOnExamineCachedResponse = "http-on-examine-cached-response";
			public const string HttpOnExamineMergedResponse = "http-on-examine-merged-response";
		}
		
		public static class Cookies
		{
			public const string CookieChanged = "cookie-changed";
			public const string CookieRejected = "cookie-rejected";
		}

		public static class DownloadManager
		{
			public const string DownloadManagerUIDone = "download-manager-ui-done";
			public const string DownloadManagerRemoveDownload = "download-manager-remove-download";
		}

		public static class IdleService
		{
			public const string Idle = "idle";
			public const string IdleDaily = "idle-daily";
			public const string Back = "back";
		}

		public static class Computer
		{
			public const string Sleep = "sleep_notification";
			public const string Wake = "wake_notification";
		}

		public static class LoginManager
		{
			// TODO
		}

		public static class Places
		{
			// TODO
		}

		public static class SessionStore
		{
			// TODO
		}
		
		/// <summary>
		/// data - enter - on enter
		/// data - exit - on exit
		/// </summary>
		public const string PrivateBrowsing = "private-browsing";

		public static class Telemetry
		{
			public const string GatherTelemetry="gather-telemetry";
		}
	}
}