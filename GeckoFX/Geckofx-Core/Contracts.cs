using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gecko
{
	public static class Contracts
	{
		#region base services
		public const string PreferenceService = "@mozilla.org/preferences-service;1";
		public const string BrowserSearchService = "@mozilla.org/browser/search-service;1";
		public const string ConsoleService = "@mozilla.org/consoleservice;1";
		public const string ObserverService = "@mozilla.org/observer-service;1";
		public const string ExceptionService = "@mozilla.org/exceptionservice;1";
		public const string NSSErrorsService = "@mozilla.org/nss_errors_service;1";
		#endregion

		#region appshell
		public const string AppShellService = "@mozilla.org/appshell/appShellService;1";
		public const string WindowMediator = "@mozilla.org/appshell/window-mediator;1";
		#endregion

		#region browser
		public const string NavHistoryService = "@mozilla.org/browser/nav-history-service;1";
		#endregion

		#region dom
		public const string XPathEvaluator = "@mozilla.org/dom/xpath-evaluator;1";
		#endregion

		#region embedding,embedcomp
		public const string WebBrowser = "@mozilla.org/embedding/browser/nsWebBrowser;1";
		public const string WindowWatcher = "@mozilla.org/embedcomp/window-watcher;1";
		#endregion

		#region gfx
		public const string ScreenManager = "@mozilla.org/gfx/screenmanager;1";
		#endregion

		#region image
		public const string ImageCache = "@mozilla.org/image/cache;1";
		public const string ImageContainer = "@mozilla.org/image/container;1";
		#endregion

		#region io
		public const string StringInputStream = "@mozilla.org/io/string-input-stream;1";
		#endregion

		#region js
		public const string Exception = "@mozilla.org/js/xpc/Exception;1";
		public const string DebuggerService = "@mozilla.org/js/jsd/debugger-service;1";
		public const string RuntimeService = "@mozilla.org/js/xpc/RuntimeService;1";
		#endregion

		#region libjar
		public const string ZipReader = "@mozilla.org/libjar/zip-reader;1";
		#endregion

		#region network
		public const string MimeInputStream = "@mozilla.org/network/mime-input-stream;1";
		public const string DnsService = "@mozilla.org/network/dns-service;1";
		public const string StreamListenerTee = "@mozilla.org/network/stream-listener-tee;1";
		public const string CacheService = "@mozilla.org/network/cache-service;1";
		public const string NetworkIOService = "@mozilla.org/network/io-service;1";
		public const string HttpActivityDistributor = "@mozilla.org/network/http-activity-distributor;1";
		public const string UriChecker = "@mozilla.org/network/urichecker;1";
		#endregion

		#region plugin
		public const string PluginHost = "@mozilla.org/plugin/host;1";
		#endregion

		#region security
		public const string CertOverride = "@mozilla.org/security/certoverride;1";
		public const string X509CertDb = "@mozilla.org/security/x509certdb;1";
		public const string RandomGenerator = "@mozilla.org/security/random-generator;1";
		public const string KeyObjectFactory = "@mozilla.org/security/keyobjectfactory;1";
		public const string Hash = "@mozilla.org/security/hash;1";
		#endregion

		#region wifi
		public const string WiFiMonitor = "@mozilla.org/wifi/monitor;1";
		#endregion

		#region xmlextras
		public const string DomParser = "@mozilla.org/xmlextras/domparser;1";
		#endregion

		#region xpcom
		public const string VersionComparator = "@mozilla.org/xpcom/version-comparator;1";
		#endregion

		#region xre
		public const string XulRuntime = "@mozilla.org/xre/app-info;1";
		#endregion

		public const string WritableVariant = "@mozilla.org/variant;1";
		public const string Variant = "@mozilla.org/variant;1";

		public const string Array = "@mozilla.org/array;1";

		public const string BinaryOutputStream = "@mozilla.org/binaryoutputstream;1";
		public const string ScriptableInputStream = "@mozilla.org/scriptableinputstream;1";
		public const string StorageStream = "@mozilla.org/storagestream;1";
		public const string Pipe = "@mozilla.org/pipe;1";

		public const string Sound = "@mozilla.org/sound;1";
		
		public const string CategoryManager = "@mozilla.org/categorymanager;1";
		public const string CookieManager = "@mozilla.org/cookiemanager;1";
		public const string SystemPrincipal = "@mozilla.org/systemprincipal;1";



		public const string WindowsTaskbar = "@mozilla.org/windows-taskbar;1";

		#region nsISupportsPrimitive's
		public const string SupportsID = "@mozilla.org/supports-id;1";
		public const string SupportsString = "@mozilla.org/supports-string;1";
		public const string SupportsBool = "@mozilla.org/supports-PRBool;1";
		public const string SupportsByte = "@mozilla.org/supports-PRUint8;1";
		public const string SupportsUInt16 = "@mozilla.org/supports-PRUint16;1";
		public const string SupportsUInt32 = "@mozilla.org/supports-PRUint32;1";
		public const string SupportsUInt64 = "@mozilla.org/supports-PRUint64;1";
		public const string SupportsTime = "@mozilla.org/supports-PRTime;1";
		public const string SupportsChar = "@mozilla.org/supports-char;1";
		public const string SupportsInt16 = "@mozilla.org/supports-PRInt16;1";
		public const string SupportsInt32 = "@mozilla.org/supports-PRInt32;1";
		public const string SupportsInt64 = "@mozilla.org/supports-PRInt64;1";
		public const string SupportsFloat = "@mozilla.org/supports-float;1";
		public const string SupportsDouble = "@mozilla.org/supports-double;1";
		public const string SupportsVoid = "@mozilla.org/supports-void;1";
		public const string SupportsInterfacePointer = "@mozilla.org/supports-interface-pointer;1";
		#endregion

		
		

		#region External components (XULRunner components)
		public const string PrivateBrowsing = "@mozilla.org/privatebrowsing;1";
		#endregion
	}
}
