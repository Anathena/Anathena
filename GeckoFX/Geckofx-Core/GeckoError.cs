namespace Gecko
{
	public static class GeckoError
	{

		/**
		 * @name Standard Module Offset Code. Each Module should identify a unique number
		 *       and then all errors associated with that module become offsets from the
		 *       base associated with that module id. There are 16 bits of code bits for
		 *       each module.
		 */

		public const int NS_ERROR_MODULE_XPCOM = 1;
		public const int NS_ERROR_MODULE_BASE = 2;
		public const int NS_ERROR_MODULE_GFX = 3;
		public const int NS_ERROR_MODULE_WIDGET = 4;
		public const int NS_ERROR_MODULE_CALENDAR = 5;
		public const int NS_ERROR_MODULE_NETWORK = 6;
		public const int NS_ERROR_MODULE_PLUGINS = 7;
		public const int NS_ERROR_MODULE_LAYOUT = 8;
		public const int NS_ERROR_MODULE_HTMLPARSER = 9;
		public const int NS_ERROR_MODULE_RDF = 10;
		public const int NS_ERROR_MODULE_UCONV = 11;
		public const int NS_ERROR_MODULE_REG = 12;
		public const int NS_ERROR_MODULE_FILES = 13;
		public const int NS_ERROR_MODULE_DOM = 14;
		public const int NS_ERROR_MODULE_IMGLIB = 15;
		public const int NS_ERROR_MODULE_MAILNEWS = 16;
		public const int NS_ERROR_MODULE_EDITOR = 17;
		public const int NS_ERROR_MODULE_XPCONNECT = 18;
		public const int NS_ERROR_MODULE_PROFILE = 19;
		public const int NS_ERROR_MODULE_LDAP = 20;
		public const int NS_ERROR_MODULE_SECURITY = 21;
		public const int NS_ERROR_MODULE_DOM_XPATH = 22;
		/* 23 used to be NS_ERROR_MODULE_DOM_RANGE (see bug 711047) */
		public const int NS_ERROR_MODULE_URILOADER = 24;
		public const int NS_ERROR_MODULE_CONTENT = 25;
		public const int NS_ERROR_MODULE_PYXPCOM = 26;
		public const int NS_ERROR_MODULE_XSLT = 27;
		public const int NS_ERROR_MODULE_IPC = 28;
		public const int NS_ERROR_MODULE_SVG = 29;
		public const int NS_ERROR_MODULE_STORAGE = 30;
		public const int NS_ERROR_MODULE_SCHEMA = 31;
		public const int NS_ERROR_MODULE_DOM_FILE = 32;
		public const int NS_ERROR_MODULE_DOM_INDEXEDDB = 33;
		public const int NS_ERROR_MODULE_DOM_FILEHANDLE = 34;

		/* NS_ERROR_MODULE_GENERAL should be used by modules that do not
		 * care if return code values overlap. Callers of methods that
		 * return such codes should be aware that they are not
		 * globally unique. Implementors should be careful about blindly
		 * returning codes from other modules that might also use
		 * the generic base.
		 */
		public const int NS_ERROR_MODULE_GENERAL = 51;

		/**
		 * @name Severity Code.  This flag identifies the level of warning
		 */

		public const int NS_ERROR_SEVERITY_SUCCESS = 0;
		public const int NS_ERROR_SEVERITY_ERROR = 1;

		/**
		 * @name Mozilla Code.  This flag separates consumers of mozilla code
		 *       from the native platform
		 */

		public const int NS_ERROR_MODULE_BASE_OFFSET = 0x45;

		/* Helpers for defining our enum, to be undef'd later */
		public static uint SUCCESS_OR_FAILURE(uint sev, uint module, uint code)
		{
			return (sev << 31) | ((module + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (code);
		}

		/* Helper file for nsError.h, via preprocessor magic */
		/* Standard "it worked" return value */
		public const int NS_OK = 0;

		/* ======================================================================= */
		/* Core errors, not part of any modules */
		/* ======================================================================= */
		public const int NS_ERROR_BASE = unchecked((int)0xC1F30000);
		/* Returned when an instance is not initialized */
		public const int NS_ERROR_NOT_INITIALIZED = NS_ERROR_BASE + 1;
		/* Returned when an instance is already initialized */
		public const int NS_ERROR_ALREADY_INITIALIZED = NS_ERROR_BASE + 2;
		/* Returned by a not implemented function */
		public const int NS_ERROR_NOT_IMPLEMENTED = unchecked((int)0x80004001);
		/* Returned when a given interface is not supported. */
		public const int NS_NOINTERFACE = unchecked((int)0x80004002);
		public const int NS_ERROR_NO_INTERFACE = NS_NOINTERFACE;
		public const int NS_ERROR_INVALID_POINTER = unchecked((int)0x80004003);
		public const int NS_ERROR_NULL_POINTER = NS_ERROR_INVALID_POINTER;
		/* Returned when a function aborts */
		public const int NS_ERROR_ABORT = unchecked((int)0x80004004);
		/* Returned when a function fails */
		public const int NS_ERROR_FAILURE = unchecked((int)0x80004005);
		/* Returned when an unexpected error occurs */
		public const int NS_ERROR_UNEXPECTED = unchecked((int)0x8000ffff);
		/* Returned when a memory allocation fails */
		public const int NS_ERROR_OUT_OF_MEMORY = unchecked((int)0x8007000e);
		/* Returned when an illegal value is passed */
		public const int NS_ERROR_ILLEGAL_VALUE = unchecked((int)0x80070057);
		public const int NS_ERROR_INVALID_ARG = NS_ERROR_ILLEGAL_VALUE;
		/* Returned when a class doesn't allow aggregation */
		public const int NS_ERROR_NO_AGGREGATION = unchecked((int)0x80040110);
		/* Returned when an operation can't complete due to an unavailable resource */
		public const int NS_ERROR_NOT_AVAILABLE = unchecked((int)0x80040111);
		/* Returned when a class is not registered */
		public const int NS_ERROR_FACTORY_NOT_REGISTERED = unchecked((int)0x80040154);
		/* Returned when a class cannot be registered, but may be tried again later */
		public const int NS_ERROR_FACTORY_REGISTER_AGAIN = unchecked((int)0x80040155);
		/* Returned when a dynamically loaded factory couldn't be found */
		public const int NS_ERROR_FACTORY_NOT_LOADED = unchecked((int)0x800401f8);
		/* Returned when a factory doesn't support signatures */
		public const int NS_ERROR_FACTORY_NO_SIGNATURE_SUPPORT = NS_ERROR_BASE + 0x101;
		/* Returned when a factory already is registered */
		public const int NS_ERROR_FACTORY_EXISTS = NS_ERROR_BASE + 0x100;


		/* ======================================================================= */
		/* 1: NS_ERROR_MODULE_XPCOM */
		/* ======================================================================= */
		/* Result codes used by nsIVariant */
		public const int NS_ERROR_CANNOT_CONVERT_DATA = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		public const int NS_ERROR_OBJECT_IS_IMMUTABLE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);
		public const int NS_ERROR_LOSS_OF_SIGNIFICANT_DATA = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (3);
		/* Result code used by nsIThreadManager */
		public const int NS_ERROR_NOT_SAME_THREAD = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (4);
		/* Various operations are not permitted during XPCOM shutdown and will fail
		 * with this exception. */
		public const int NS_ERROR_ILLEGAL_DURING_SHUTDOWN = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (30);
		public const int NS_ERROR_SERVICE_NOT_AVAILABLE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (22);

		public const int NS_SUCCESS_LOSS_OF_INSIGNIFICANT_DATA = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_XPCOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		/* Used by nsCycleCollectionParticipant */
		public const int NS_SUCCESS_INTERRUPTED_TRAVERSE = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_XPCOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);
		/* DEPRECATED */
		public const int NS_ERROR_SERVICE_NOT_FOUND = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_XPCOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (22);
		/* DEPRECATED */
		public const int NS_ERROR_SERVICE_IN_USE = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_XPCOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (23);


		/* ======================================================================= */
		/* 2: NS_ERROR_MODULE_BASE */
		/* ======================================================================= */
		/* I/O Errors */

		/*  Stream closed */
		public const int NS_BASE_STREAM_CLOSED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_BASE + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);
		/*  Error from the operating system */
		public const int NS_BASE_STREAM_OSERROR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_BASE + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (3);
		/*  Illegal arguments */
		public const int NS_BASE_STREAM_ILLEGAL_ARGS = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_BASE + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (4);
		/*  For unichar streams */
		public const int NS_BASE_STREAM_NO_CONVERTER = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_BASE + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (5);
		/*  For unichar streams */
		public const int NS_BASE_STREAM_BAD_CONVERSION = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_BASE + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (6);
		public const int NS_BASE_STREAM_WOULD_BLOCK = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_BASE + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (7);


		/* ======================================================================= */
		/* 3: NS_ERROR_MODULE_GFX */
		/* ======================================================================= */
		/* error codes for printer device contexts */
		/* Unix: print command (lp/lpr) not found */
		public const int NS_ERROR_GFX_PRINTER_CMD_NOT_FOUND = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);
		/* Unix: print command returned an error */
		public const int NS_ERROR_GFX_PRINTER_CMD_FAILURE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (3);
		/* no printer available (e.g. cannot find _any_ printer) */
		public const int NS_ERROR_GFX_PRINTER_NO_PRINTER_AVAILABLE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (4);
		/* _specified_ (by name) printer not found */
		public const int NS_ERROR_GFX_PRINTER_NAME_NOT_FOUND = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (5);
		/* access to printer denied */
		public const int NS_ERROR_GFX_PRINTER_ACCESS_DENIED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (6);
		/* invalid printer attribute (for example: unsupported paper size etc.) */
		public const int NS_ERROR_GFX_PRINTER_INVALID_ATTRIBUTE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (7);
		/* printer not "ready" (offline ?) */
		public const int NS_ERROR_GFX_PRINTER_PRINTER_NOT_READY = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (9);
		/* printer out of paper */
		public const int NS_ERROR_GFX_PRINTER_OUT_OF_PAPER = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (10);
		/* generic printer I/O error */
		public const int NS_ERROR_GFX_PRintER_PRINTER_IO_ERROR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (11);
		/* print-to-file: could not open output file */
		public const int NS_ERROR_GFX_PRINTER_COULD_NOT_OPEN_FILE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (12);
		/* print-to-file: I/O error while printing to file */
		public const int NS_ERROR_GFX_PRINTER_FILE_IO_ERROR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (13);
		/* print preview: needs at least one printer */
		public const int NS_ERROR_GFX_PRINTER_PRINTPREVIEW = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (14);
		/* print: starting document */
		public const int NS_ERROR_GFX_PRINTER_STARTDOC = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (15);
		/* print: ending document */
		public const int NS_ERROR_GFX_PRINTER_ENDDOC = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (16);
		/* print: starting page */
		public const int NS_ERROR_GFX_PRINTER_STARTPAGE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (17);
		/* print: ending page */
		public const int NS_ERROR_GFX_PRINTER_ENDPAGE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (18);
		/* print: print while in print preview */
		public const int NS_ERROR_GFX_PRINTER_PRINT_WHILE_PREVIEW = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (19);
		/* requested page size not supported by printer */
		public const int NS_ERROR_GFX_PRINTER_PAPER_SIZE_NOT_SUPPORTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (20);
		/* requested page orientation not supported */
		public const int NS_ERROR_GFX_PRINTER_ORIENTATION_NOT_SUPPORTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (21);
		/* requested colorspace not supported (like printing "color" on a
			 "grayscale"-only printer) */
		public const int NS_ERROR_GFX_PRINTER_COLORSPACE_NOT_SUPPORTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (22);
		/* too many copies requested */
		public const int NS_ERROR_GFX_PRINTER_TOO_MANY_COPIES = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (23);
		/* driver configuration error */
		public const int NS_ERROR_GFX_PRINTER_DRIVER_CONFIGURATION_ERROR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (24);
		/* The document is still being loaded, can't Print Preview */
		public const int NS_ERROR_GFX_PRINTER_DOC_IS_BUSY_PP = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (25);
		/* The document was asked to be destroyed while we were preparing printing */
		public const int NS_ERROR_GFX_PRINTER_DOC_WAS_DESTORYED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (26);
		/* Cannot Print or Print Preview XUL Documents */
		public const int NS_ERROR_GFX_PRINTER_NO_XUL = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (27);
		/* The toolkit no longer supports the Print Dialog (for embedders) */
		public const int NS_ERROR_GFX_NO_PRINTDIALOG_IN_TOOLKIT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (28);
		/* The was wasn't any Print Prompt service registered (this shouldn't happen) */
		public const int NS_ERROR_GFX_NO_PRINTROMPTSERVICE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (29);
		/* requested plex mode not supported by printer */
		public const int NS_ERROR_GFX_PRINTER_PLEX_NOT_SUPPORTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (30);
		/* The document is still being loaded */
		public const int NS_ERROR_GFX_PRINTER_DOC_IS_BUSY = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (31);
		/* Printing is not implemented */
		public const int NS_ERROR_GFX_PRINTING_NOT_IMPLEMENTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (32);
		/* Cannot load the matching print NS_ERROR_MODULE_GFX */
		public const int NS_ERROR_GFX_COULD_NOT_LOAD_PRINT_MODULE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (33);
		/* requested resolution/quality mode not supported by printer */
		public const int NS_ERROR_GFX_PRINTER_RESOLUTION_NOT_SUPPORTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (34);
		/* Font cmap is strangely structured - avoid this font! */
		public const int NS_ERROR_GFX_CMAP_MALFORMED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GFX + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (51);


		/* ======================================================================= */
		/* 6: NS_ERROR_MODULE_NETWORK */
		/* ======================================================================= */
		/* General async request error codes:
		 *
		 * These error codes are commonly passed through callback methods to indicate
		 * the status of some requested async request.
		 *
		 * For example, see nsIRequestObserver::onStopRequest.
		 */

		/* The async request completed successfully. */
		public const int NS_BINDING_SUCCEEDED = NS_OK;

		/* The async request failed for some unknown reason.  */
		public const int NS_BINDING_FAILED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		/* The async request failed because it was aborted by some user action. */
		public const int NS_BINDING_ABORTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);
		/* The async request has been "redirected" to a different async request.
		 * (e.g., an HTTP redirect occurred).
		 *
		 * This error code is used with load groups to notify the load group observer
		 * when a request in the load group is redirected to another request. */
		public const int NS_BINDING_REDIRECTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (3);
		/* The async request has been "retargeted" to a different "handler."
		 *
		 * This error code is used with load groups to notify the load group observer
		 * when a request in the load group is removed from the load group and added
		 * to a different load group. */
		public const int NS_BINDING_RETARGETED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (4);

		/* Miscellaneous error codes: These errors are not typically passed via
		 * onStopRequest. */

		/* The URI is malformed. */
		public const int NS_ERROR_MALFORMED_URI = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (10);
		/* The requested action could not be completed while the object is busy.
		 * Implementations of nsIChannel::asyncOpen will commonly return this error
		 * if the channel has already been opened (and has not yet been closed). */
		public const int NS_ERROR_IN_PROGRESS = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (15);
		/* Returned from nsIChannel::asyncOpen to indicate that OnDataAvailable will
		 * not be called because there is no content available.  This is used by
		 * helper app style protocols (e.g., mailto).  XXX perhaps this should be a
		 * success code. */
		public const int NS_ERROR_NO_CONTENT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (17);
		/* The URI scheme corresponds to an unknown protocol handler. */
		public const int NS_ERROR_UNKNOWN_PROTOCOL = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (18);
		/* The content encoding of the source document was incorrect, for example
		 * returning a plain HTML document advertised as Content-Encoding: gzip */
		public const int NS_ERROR_INVALID_CONTENT_ENCODING = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (27);
		/* A transport level corruption was found in the source document. for example
		 * a document with a calculated checksum that does not match the Content-MD5
		 * http header. */
		public const int NS_ERROR_CORRUPTED_CONTENT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (29);
		/* While parsing for the first component of a header field using syntax as in
		 * Content-Disposition or Content-Type, the first component was found to be
		 * empty, such as in: Content-Disposition: ; filename=foo */
		public const int NS_ERROR_FIRST_HEADER_FIELD_COMPONENT_EMPTY = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (34);
		/* Returned from nsIChannel::asyncOpen when trying to open the channel again
		 * (reopening is not supported). */
		public const int NS_ERROR_ALREADY_OPENED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (73);

		/* Connectivity error codes: */

		/* The connection is already established.  XXX unused - consider removing. */
		public const int NS_ERROR_ALREADY_CONNECTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (11);
		/* The connection does not exist.  XXX unused - consider removing. */
		public const int NS_ERROR_NOT_CONNECTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (12);
		/* The connection attempt failed, for example, because no server was
		 * listening at specified host:port. */
		public const int NS_ERROR_CONNECTION_REFUSED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (13);
		/* The connection was lost due to a timeout error.  */
		public const int NS_ERROR_NET_TIMEOUT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (14);
		/* The requested action could not be completed while the networking library
		 * is in the offline state. */
		public const int NS_ERROR_OFFLINE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (16);
		/* The requested action was prohibited because it would have caused the
		 * networking library to establish a connection to an unsafe or otherwise
		 * banned port. */
		public const int NS_ERROR_PORT_ACCESS_NOT_ALLOWED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (19);
		/* The connection was established, but no data was ever received. */
		public const int NS_ERROR_NET_RESET = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (20);
		/* The connection was established, but the data transfer was interrupted. */
		public const int NS_ERROR_NET_INTERRUPT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (71);
		/* The connection attempt to a proxy failed. */
		public const int NS_ERROR_PROXY_CONNECTION_REFUSED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (72);

		/* XXX really need to better rationalize these error codes.  are consumers of
		 * necko really expected to know how to discern the meaning of these?? */
		/* This request is not resumable, but it was tried to resume it, or to
		 * request resume-specific data. */
		public const int NS_ERROR_NOT_RESUMABLE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (25);
		/* The request failed as a result of a detected redirection loop.  */
		public const int NS_ERROR_REDIRECT_LOOP = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (31);
		/* It was attempted to resume the request, but the entity has changed in the
		 * meantime. */
		public const int NS_ERROR_ENTITY_CHANGED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (32);
		/* The request failed because the content type returned by the server was not
		 * a type expected by the channel (for nested channels such as the JAR
		 * channel). */
		public const int NS_ERROR_UNSAFE_CONTENT_TYPE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (74);
		/* The request failed because the user tried to access to a remote XUL
		 * document from a website that is not in its white-list. */
		public const int NS_ERROR_REMOTE_XUL = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (75);


		/* FTP specific error codes: */

		public const int NS_ERROR_FTP_LOGIN = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (21);
		public const int NS_ERROR_FTP_CWD = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (22);
		public const int NS_ERROR_FTP_PASV = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (23);
		public const int NS_ERROR_FTP_PWD = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (24);
		public const int NS_ERROR_FTP_LIST = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (28);

		/* DNS specific error codes: */

		/* The lookup of a hostname failed.  This generally refers to the hostname
		 * from the URL being loaded. */
		public const int NS_ERROR_UNKNOWN_HOST = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (30);
		/* A low or medium priority DNS lookup failed because the pending queue was
		 * already full. High priorty (the default) always makes room */
		public const int NS_ERROR_DNS_LOOKUP_QUEUE_FULL = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (33);
		/* The lookup of a proxy hostname failed.  If a channel is configured to
		 * speak to a proxy server, then it will generate this error if the proxy
		 * hostname cannot be resolved. */
		public const int NS_ERROR_UNKNOWN_PROXY_HOST = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (42);


		/* Socket specific error codes: */

		/* The specified socket type does not exist. */
		public const int NS_ERROR_UNKNOWN_SOCKET_TYPE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (51);
		/* The specified socket type could not be created. */
		public const int NS_ERROR_SOCKET_CREATE_FAILED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (52);


		/* Cache specific error codes: */
		public const int NS_ERROR_CACHE_KEY_NOT_FOUND = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (61);
		public const int NS_ERROR_CACHE_DATA_IS_STREAM = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (62);
		public const int NS_ERROR_CACHE_DATA_IS_NOT_STREAM = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (63);
		public const int NS_ERROR_CACHE_WAIT_FOR_VALIDATION = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (64);
		public const int NS_ERROR_CACHE_ENTRY_DOOMED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (65);
		public const int NS_ERROR_CACHE_READ_ACCESS_DENIED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (66);
		public const int NS_ERROR_CACHE_WRITE_ACCESS_DENIED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (67);
		public const int NS_ERROR_CACHE_IN_USE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (68);
		/* Error passed through onStopRequest if the document could not be fetched
		 * from the cache. */
		public const int NS_ERROR_DOCUMENT_NOT_CACHED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (70);

		/* Effective TLD Service specific error codes: */

		/* The requested number of domain levels exceeds those present in the host
		 * string. */
		public const int NS_ERROR_INSUFFICIENT_DOMAIN_LEVELS = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (80);
		/* The host string is an IP address. */
		public const int NS_ERROR_HOST_IS_IP_ADDRESS = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (81);


		/* StreamLoader specific result codes: */

		/* Result code returned by nsIStreamLoaderObserver to indicate that the
		 * observer is taking over responsibility for the data buffer, and the loader
		 * should NOT free it. */
		public const int NS_SUCCESS_ADOPTED_DATA = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (90);

		/* FTP */
		public const int NS_NET_STATUS_BEGIN_FTP_TRANSACTION = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (27);
		public const int NS_NET_STATUS_END_FTP_TRANSACTION = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (28);

		/* This success code may be returned by nsIAuthModule::getNextToken to                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
		 * indicate that the authentication is finished and thus there's no need
		 * to call getNextToken again. */
		public const int NS_SUCCESS_AUTH_FINISHED = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (40);

		/* These are really not "results", they're statuses, used by nsITransport and
		 * friends.  This is abuse of nsresult, but we'll put up with it for now. */
		/* nsITransport */
		public const int NS_NET_STATUS_READING = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (8);
		public const int NS_NET_STATUS_WRITING = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (9);

		/* nsISocketTransport */
		public const int NS_NET_STATUS_RESOLVING_HOST = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (3);
		public const int NS_NET_STATUS_RESOLVED_HOST = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (11);
		public const int NS_NET_STATUS_CONNECTING_TO = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (7);
		public const int NS_NET_STATUS_CONNECTED_TO = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (4);
		public const int NS_NET_STATUS_SENDING_TO = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (5);
		public const int NS_NET_STATUS_WAITING_FOR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (10);
		public const int NS_NET_STATUS_RECEIVING_FROM = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_NETWORK + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (6);


		/* ======================================================================= */
		/* 7: NS_ERROR_MODULE_PLUGINS */
		/* ======================================================================= */
		public const int NS_ERROR_PLUGINS_PLUGINSNOTCHANGED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_PLUGINS + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1000);
		public const int NS_ERROR_PLUGIN_DISABLED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_PLUGINS + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1001);
		public const int NS_ERROR_PLUGIN_BLOCKLISTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_PLUGINS + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1002);
		public const int NS_ERROR_PLUGIN_TIME_RANGE_NOT_SUPPORTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_PLUGINS + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1003);
		public const int NS_ERROR_PLUGIN_CLICKTOPLAY = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_PLUGINS + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1004);


		/* ======================================================================= */
		/* 8: NS_ERROR_MODULE_LAYOUT */
		/* ======================================================================= */
		/* Return code for nsITableLayout */
		public const int NS_TABLELAYOUT_CELL_NOT_FOUND = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_LAYOUT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (0);
		/* Return code for nsFrame::GetNextPrevLineFromeBlockFrame */
		public const int NS_POSITION_BEFORE_TABLE = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_LAYOUT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (3);
		/** Return codes for nsPresState::GetProperty() */
		/* Returned if the property exists */
		public const int NS_STATE_PROPERTY_EXISTS = NS_OK;
		/* Returned if the property does not exist */
		public const int NS_STATE_PROPERTY_NOT_THERE = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_LAYOUT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (5);


		/* ======================================================================= */
		/* 9: NS_ERROR_MODULE_HTMLPARSER */
		/* ======================================================================= */
		public const int NS_ERROR_HTMLPARSER_CONTINUE = NS_OK;

		public const int NS_ERROR_HTMLPARSER_EOF = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1000);
		public const int NS_ERROR_HTMLPARSER_UNKNOWN = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1001);
		public const int NS_ERROR_HTMLPARSER_CANTPROPAGATE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1002);
		public const int NS_ERROR_HTMLPARSER_CONTEXTMISMATCH = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1003);
		public const int NS_ERROR_HTMLPARSER_BADFILENAME = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1004);
		public const int NS_ERROR_HTMLPARSER_BADURL = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1005);
		public const int NS_ERROR_HTMLPARSER_INVALIDPARSERCONTEXT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1006);
		public const int NS_ERROR_HTMLPARSER_INTERRUPTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1007);
		public const int NS_ERROR_HTMLPARSER_BLOCK = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1008);
		public const int NS_ERROR_HTMLPARSER_BADTOKENIZER = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1009);
		public const int NS_ERROR_HTMLPARSER_BADATTRIBUTE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1010);
		public const int NS_ERROR_HTMLPARSER_UNRESOLVEDDTD = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1011);
		public const int NS_ERROR_HTMLPARSER_MISPLACEDTABLECONTENT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1012);
		public const int NS_ERROR_HTMLPARSER_BADDTD = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1013);
		public const int NS_ERROR_HTMLPARSER_BADCONTEXT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1014);
		public const int NS_ERROR_HTMLPARSER_STOPPARSING = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1015);
		public const int NS_ERROR_HTMLPARSER_UNTERMINATEDSTRINGLITERAL = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1016);
		public const int NS_ERROR_HTMLPARSER_HIERARCHYTOODEEP = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1017);
		public const int NS_ERROR_HTMLPARSER_FAKE_ENDTAG = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1018);
		public const int NS_ERROR_HTMLPARSER_INVALID_COMMENT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1019);

		public const int NS_HTMLTOKENS_NOT_AN_ENTITY = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2000);
		public const int NS_HTMLPARSER_VALID_META_CHARSET = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_HTMLPARSER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (3000);


		/* ======================================================================= */
		/* 10: NS_ERROR_MODULE_RDF */
		/* ======================================================================= */
		/* Returned from nsIRDFDataSource::Assert() and Unassert() if the assertion
		 * (or unassertion was accepted by the datasource */
		public const int NS_RDF_ASSERTION_ACCEPTED = NS_OK;
		/* Returned from nsIRDFCursor::Advance() if the cursor has no more
		 * elements to enumerate */
		public const int NS_RDF_CURSOR_EMPTY = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_RDF + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		/* Returned from nsIRDFDataSource::GetSource() and GetTarget() if the
		 * source/target has no value */
		public const int NS_RDF_NO_VALUE = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_RDF + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);
		/* Returned from nsIRDFDataSource::Assert() and Unassert() if the assertion
		 * (or unassertion) was rejected by the datasource; i.e., the datasource was
		 * not willing to record the statement. */
		public const int NS_RDF_ASSERTION_REJECTED = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_RDF + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (3);
		/* Return this from rdfITripleVisitor to stop cycling */
		public const int NS_RDF_STOP_VISIT = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_RDF + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (4);


		/* ======================================================================= */
		/* 11: NS_ERROR_MODULE_UCONV */
		/* ======================================================================= */
		public const int NS_ERROR_UCONV_NOCONV = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_UCONV + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		public const int NS_ERROR_UDEC_ILLEGALINPUT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_UCONV + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (14);

		public const int NS_SUCCESS_USING_FALLBACK_LOCALE = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_UCONV + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);
		public const int NS_OK_UDEC_EXACTLENGTH = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_UCONV + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (11);
		public const int NS_OK_UDEC_MOREINPUT = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_UCONV + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (12);
		public const int NS_OK_UDEC_MOREOUTPUT = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_UCONV + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (13);
		public const int NS_OK_UDEC_NOBOMFOUND = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_UCONV + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (14);
		public const int NS_OK_UENC_EXACTLENGTH = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_UCONV + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (33);
		public const int NS_OK_UENC_MOREOUTPUT = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_UCONV + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (34);
		public const int NS_ERROR_UENC_NOMAPPING = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_UCONV + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (35);
		public const int NS_OK_UENC_MOREINPUT = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_UCONV + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (36);

		/* BEGIN DEPRECATED */
		public const int NS_EXACT_LENGTH = NS_OK_UDEC_EXACTLENGTH;
		public const int NS_PARTIAL_MORE_INPUT = NS_OK_UDEC_MOREINPUT;
		public const int NS_PARTIAL_MORE_OUTPUT = NS_OK_UDEC_MOREOUTPUT;
		public const int NS_ERROR_ILLEGAL_INPUT = NS_ERROR_UDEC_ILLEGALINPUT;
		/* END DEPRECATED */


		/* ======================================================================= */
		/* 13: NS_ERROR_MODULE_FILES */
		/* ======================================================================= */
		public const int NS_ERROR_FILE_UNRECOGNIZED_PATH = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		public const int NS_ERROR_FILE_UNRESOLVABLE_SYMLINK = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);
		public const int NS_ERROR_FILE_EXECUTION_FAILED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (3);
		public const int NS_ERROR_FILE_UNKNOWN_TYPE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (4);
		public const int NS_ERROR_FILE_DESTINATION_NOT_DIR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (5);
		public const int NS_ERROR_FILE_TARGET_DOES_NOT_EXIST = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (6);
		public const int NS_ERROR_FILE_COPY_OR_MOVE_FAILED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (7);
		public const int NS_ERROR_FILE_ALREADY_EXISTS = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (8);
		public const int NS_ERROR_FILE_INVALID_PATH = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (9);
		public const int NS_ERROR_FILE_DISK_FULL = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (10);
		public const int NS_ERROR_FILE_CORRUPTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (11);
		public const int NS_ERROR_FILE_NOT_DIRECTORY = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (12);
		public const int NS_ERROR_FILE_IS_DIRECTORY = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (13);
		public const int NS_ERROR_FILE_IS_LOCKED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (14);
		public const int NS_ERROR_FILE_TOO_BIG = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (15);
		public const int NS_ERROR_FILE_NO_DEVICE_SPACE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (16);
		public const int NS_ERROR_FILE_NAME_TOO_LONG = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (17);
		public const int NS_ERROR_FILE_NOT_FOUND = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (18);
		public const int NS_ERROR_FILE_READ_ONLY = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (19);
		public const int NS_ERROR_FILE_DIR_NOT_EMPTY = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (20);
		public const int NS_ERROR_FILE_ACCESS_DENIED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (21);

		public const int NS_SUCCESS_FILE_DIRECTORY_EMPTY = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		/* Result codes used by nsIDirectoryServiceProvider2 */
		public const int NS_SUCCESS_AGGREGATE_RESULT = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_FILES + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);


		/* ======================================================================= */
		/* 14: NS_ERROR_MODULE_DOM */
		/* ======================================================================= */
		/* XXX If you add a new DOM error code, also add an error string to
		 * dom/src/base/domerr.msg */

		/* Standard DOM error codes: http://dvcs.w3.org/hg/domcore/raw-file/tip/Overview.html */
		public const int NS_ERROR_DOM_INDEX_SIZE_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		public const int NS_ERROR_DOM_HIERARCHY_REQUEST_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (3);
		public const int NS_ERROR_DOM_WRONG_DOCUMENT_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (4);
		public const int NS_ERROR_DOM_INVALID_CHARACTER_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (5);
		public const int NS_ERROR_DOM_NO_MODIFICATION_ALLOWED_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (7);
		public const int NS_ERROR_DOM_NOT_FOUND_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (8);
		public const int NS_ERROR_DOM_NOT_SUPPORTED_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (9);
		public const int NS_ERROR_DOM_INUSE_ATTRIBUTE_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (10);
		public const int NS_ERROR_DOM_INVALID_STATE_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (11);
		public const int NS_ERROR_DOM_SYNTAX_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (12);
		public const int NS_ERROR_DOM_INVALID_MODIFICATION_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (13);
		public const int NS_ERROR_DOM_NAMESPACE_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (14);
		public const int NS_ERROR_DOM_INVALID_ACCESS_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (15);
		public const int NS_ERROR_DOM_TYPE_MISMATCH_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (17);
		public const int NS_ERROR_DOM_SECURITY_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (18);
		public const int NS_ERROR_DOM_NETWORK_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (19);
		public const int NS_ERROR_DOM_ABORT_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (20);
		public const int NS_ERROR_DOM_URL_MISMATCH_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (21);
		public const int NS_ERROR_DOM_QUOTA_EXCEEDED_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (22);
		public const int NS_ERROR_DOM_TIMEOUT_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (23);
		public const int NS_ERROR_DOM_INVALID_NODE_TYPE_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (24);
		public const int NS_ERROR_DOM_DATA_CLONE_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (25);
		/* XXX Should be JavaScript native errors */
		public const int NS_ERROR_TYPE_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (26);
		public const int NS_ERROR_RANGE_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (27);
		/* StringEncoding API errors from http://wiki.whatwg.org/wiki/StringEncoding */
		public const int NS_ERROR_DOM_ENCODING_NOT_SUPPORTED_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (28);
		public const int NS_ERROR_DOM_ENCODING_NOT_UTF_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (29);
		public const int NS_ERROR_DOM_ENCODING_DECODE_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (30);
		/* DOM error codes defined by us */
		public const int NS_ERROR_DOM_SECMAN_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1001);
		public const int NS_ERROR_DOM_WRONG_TYPE_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1002);
		public const int NS_ERROR_DOM_NOT_OBJECT_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1003);
		public const int NS_ERROR_DOM_NOT_XPC_OBJECT_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1004);
		public const int NS_ERROR_DOM_NOT_NUMBER_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1005);
		public const int NS_ERROR_DOM_NOT_BOOLEAN_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1006);
		public const int NS_ERROR_DOM_NOT_FUNCTION_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1007);
		public const int NS_ERROR_DOM_TOO_FEW_PARAMETERS_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1008);
		public const int NS_ERROR_DOM_BAD_DOCUMENT_DOMAIN = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1009);
		public const int NS_ERROR_DOM_PROP_ACCESS_DENIED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1010);
		public const int NS_ERROR_DOM_XPCONNECT_ACCESS_DENIED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1011);
		public const int NS_ERROR_DOM_BAD_URI = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1012);
		public const int NS_ERROR_DOM_RETVAL_UNDEFINED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1013);
		public const int NS_ERROR_DOM_QUOTA_REACHED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1014);


		/* ======================================================================= */
		/* 15: NS_ERROR_MODULE_IMGLIB */
		/* ======================================================================= */
		public const int NS_IMAGELIB_SUCCESS_LOAD_FINISHED = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_IMGLIB + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (0);
		public const int NS_IMAGELIB_CHANGING_OWNER = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_IMGLIB + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);

		public const int NS_IMAGELIB_ERROR_FAILURE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_IMGLIB + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (5);
		public const int NS_IMAGELIB_ERROR_NO_DECODER = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_IMGLIB + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (6);
		public const int NS_IMAGELIB_ERROR_NOT_FINISHED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_IMGLIB + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (7);
		public const int NS_IMAGELIB_ERROR_NO_ENCODER = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_IMGLIB + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (9);


		/* ======================================================================= */
		/* 17: NS_ERROR_MODULE_EDITOR */
		/* ======================================================================= */
		public const int NS_ERROR_EDITOR_NO_SELECTION = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_EDITOR + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		public const int NS_ERROR_EDITOR_NO_TEXTNODE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_EDITOR + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);
		public const int NS_FOUND_TARGET = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_EDITOR + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (3);

		public const int NS_EDITOR_ELEMENT_NOT_FOUND = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_EDITOR + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);


		/* ======================================================================= */
		/* 18: NS_ERROR_MODULE_XPCONNECT */
		/* ======================================================================= */
		public const int NS_ERROR_XPC_NOT_ENOUGH_ARGS = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		public const int NS_ERROR_XPC_NEED_OUT_OBJECT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);
		public const int NS_ERROR_XPC_CANT_SET_OUT_VAL = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (3);
		public const int NS_ERROR_XPC_NATIVE_RETURNED_FAILURE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (4);
		public const int NS_ERROR_XPC_CANT_GET_INTERFACE_INFO = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (5);
		public const int NS_ERROR_XPC_CANT_GET_PARAM_IFACE_INFO = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (6);
		public const int NS_ERROR_XPC_CANT_GET_METHOD_INFO = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (7);
		public const int NS_ERROR_XPC_UNEXPECTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (8);
		public const int NS_ERROR_XPC_BAD_CONVERT_JS = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (9);
		public const int NS_ERROR_XPC_BAD_CONVERT_NATIVE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (10);
		public const int NS_ERROR_XPC_BAD_CONVERT_JS_NULL_REF = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (11);
		public const int NS_ERROR_XPC_BAD_OP_ON_WN_PROTO = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (12);
		public const int NS_ERROR_XPC_CANT_CONVERT_WN_TO_FUN = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (13);
		public const int NS_ERROR_XPC_CANT_DEFINE_PROP_ON_WN = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (14);
		public const int NS_ERROR_XPC_CANT_WATCH_WN_STATIC = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (15);
		public const int NS_ERROR_XPC_CANT_EXPORT_WN_STATIC = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (16);
		public const int NS_ERROR_XPC_SCRIPTABLE_CALL_FAILED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (17);
		public const int NS_ERROR_XPC_SCRIPTABLE_CTOR_FAILED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (18);
		public const int NS_ERROR_XPC_CANT_CALL_WO_SCRIPTABLE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (19);
		public const int NS_ERROR_XPC_CANT_CTOR_WO_SCRIPTABLE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (20);
		public const int NS_ERROR_XPC_CI_RETURNED_FAILURE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (21);
		public const int NS_ERROR_XPC_GS_RETURNED_FAILURE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (22);
		public const int NS_ERROR_XPC_BAD_CID = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (23);
		public const int NS_ERROR_XPC_BAD_IID = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (24);
		public const int NS_ERROR_XPC_CANT_CREATE_WN = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (25);
		public const int NS_ERROR_XPC_JS_THREW_EXCEPTION = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (26);
		public const int NS_ERROR_XPC_JS_THREW_NATIVE_OBJECT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (27);
		public const int NS_ERROR_XPC_JS_THREW_JS_OBJECT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (28);
		public const int NS_ERROR_XPC_JS_THREW_NULL = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (29);
		public const int NS_ERROR_XPC_JS_THREW_STRING = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (30);
		public const int NS_ERROR_XPC_JS_THREW_NUMBER = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (31);
		public const int NS_ERROR_XPC_JAVASCRIPT_ERROR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (32);
		public const int NS_ERROR_XPC_JAVASCRIPT_ERROR_WITH_DETAILS = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (33);
		public const int NS_ERROR_XPC_CANT_CONVERT_PRIMITIVE_TO_ARRAY = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (34);
		public const int NS_ERROR_XPC_CANT_CONVERT_OBJECT_TO_ARRAY = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (35);
		public const int NS_ERROR_XPC_NOT_ENOUGH_ELEMENTS_IN_ARRAY = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (36);
		public const int NS_ERROR_XPC_CANT_GET_ARRAY_INFO = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (37);
		public const int NS_ERROR_XPC_NOT_ENOUGH_CHARS_IN_STRING = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (38);
		public const int NS_ERROR_XPC_SECURITY_MANAGER_VETO = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (39);
		public const int NS_ERROR_XPC_INTERFACE_NOT_SCRIPTABLE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (40);
		public const int NS_ERROR_XPC_INTERFACE_NOT_FROM_NSISUPPORTS = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (41);
		public const int NS_ERROR_XPC_CANT_GET_JSOBJECT_OF_DOM_OBJECT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (42);
		public const int NS_ERROR_XPC_CANT_SET_READ_ONLY_CONSTANT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (43);
		public const int NS_ERROR_XPC_CANT_SET_READ_ONLY_ATTRIBUTE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (44);
		public const int NS_ERROR_XPC_CANT_SET_READ_ONLY_METHOD = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (45);
		public const int NS_ERROR_XPC_CANT_ADD_PROP_TO_WRAPPED_NATIVE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (46);
		public const int NS_ERROR_XPC_CALL_TO_SCRIPTABLE_FAILED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (47);
		public const int NS_ERROR_XPC_JSOBJECT_HAS_NO_FUNCTION_NAMED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (48);
		public const int NS_ERROR_XPC_BAD_ID_STRING = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (49);
		public const int NS_ERROR_XPC_BAD_INITIALIZER_NAME = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (50);
		public const int NS_ERROR_XPC_HAS_BEEN_SHUTDOWN = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (51);
		public const int NS_ERROR_XPC_CANT_MODIFY_PROP_ON_WN = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (52);
		public const int NS_ERROR_XPC_BAD_CONVERT_JS_ZERO_ISNOT_NULL = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (53);
		/* any new errors here should have an associated entry added in xpc.msg */

		public const int NS_SUCCESS_I_DID_SOMETHING = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		/* Classes that want to only be touched by chrome (or from code whose
		 * filename begins with chrome://global/) shoudl return this from their
		 * scriptable helper's PreCreate hook. */
		public const int NS_SUCCESS_CHROME_ACCESS_ONLY = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);
		/* Classes that want slim wrappers should return
		 * NS_SUCCESS_ALLOW_SLIM_WRAPPERS from their scriptable helper's PreCreate
		 * hook. They must also force a parent for their wrapper (from the PreCreate
		 * hook), they must implement nsWrapperCache and their scriptable helper must
		 * implement nsXPCClassInfo and must return DONT_ASK_INSTANCE_FOR_SCRIPTABLE
		 * in the flags. */
		public const int NS_SUCCESS_ALLOW_SLIM_WRAPPERS = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_XPCONNECT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (3);


		/* ======================================================================= */
		/* 19: NS_ERROR_MODULE_PROFILE */
		/* ======================================================================= */
		public const int NS_ERROR_LAUNCHED_CHILD_PROCESS = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_PROFILE + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (200);


		/* ======================================================================= */
		/* 21: NS_ERROR_MODULE_SECURITY */
		/* ======================================================================= */
		/* Error code for CSP */
		public const int NS_ERROR_CSP_FRAME_ANCESTOR_VIOLATION = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SECURITY + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (99);

		/* CMS specific nsresult error codes.  Note: the numbers used here correspond
		 * to the values in nsICMSMessageErrors.idl. */
		public const int NS_ERROR_CMS_VERIFY_NOT_SIGNED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SECURITY + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1024);
		public const int NS_ERROR_CMS_VERIFY_NO_CONTENT_INFO = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SECURITY + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1025);
		public const int NS_ERROR_CMS_VERIFY_BAD_DIGEST = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SECURITY + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1026);
		public const int NS_ERROR_CMS_VERIFY_NOCERT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SECURITY + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1028);
		public const int NS_ERROR_CMS_VERIFY_UNTRUSTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SECURITY + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1029);
		public const int NS_ERROR_CMS_VERIFY_ERROR_UNVERIFIED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SECURITY + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1031);
		public const int NS_ERROR_CMS_VERIFY_ERROR_PROCESSING = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SECURITY + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1032);
		public const int NS_ERROR_CMS_VERIFY_BAD_SIGNATURE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SECURITY + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1033);
		public const int NS_ERROR_CMS_VERIFY_DIGEST_MISMATCH = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SECURITY + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1034);
		public const int NS_ERROR_CMS_VERIFY_UNKNOWN_ALGO = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SECURITY + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1035);
		public const int NS_ERROR_CMS_VERIFY_UNSUPPORTED_ALGO = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SECURITY + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1036);
		public const int NS_ERROR_CMS_VERIFY_MALFORMED_SIGNATURE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SECURITY + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1037);
		public const int NS_ERROR_CMS_VERIFY_HEADER_MISMATCH = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SECURITY + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1038);
		public const int NS_ERROR_CMS_VERIFY_NOT_YET_ATTEMPTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SECURITY + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1039);
		public const int NS_ERROR_CMS_VERIFY_CERT_WITHOUT_ADDRESS = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SECURITY + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1040);
		public const int NS_ERROR_CMS_ENCRYPT_NO_BULK_ALG = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SECURITY + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1056);
		public const int NS_ERROR_CMS_ENCRYPT_INCOMPLETE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SECURITY + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1057);


		/* ======================================================================= */
		/* 22: NS_ERROR_MODULE_DOM_XPATH */
		/* ======================================================================= */
		/* DOM error codes from http://www.w3.org/TR/DOM-Level-3-XPath/ */
		public const int NS_ERROR_DOM_INVALID_EXPRESSION_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_XPATH + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (51);
		public const int NS_ERROR_DOM_TYPE_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_XPATH + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (52);


		/* ======================================================================= */
		/* 24: NS_ERROR_MODULE_URILOADER */
		/* ======================================================================= */
		public const int NS_ERROR_WONT_HANDLE_CONTENT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_URILOADER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		/* The load has been cancelled because it was found on a malware or phishing
		 * blacklist. */
		public const int NS_ERROR_MALWARE_URI = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_URILOADER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (30);
		public const int NS_ERROR_PHISHING_URI = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_URILOADER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (31);
		/* Used when "Save Link As..." doesn't see the headers quickly enough to
		 * choose a filename.  See nsContextMenu.js. */
		public const int NS_ERROR_SAVE_LINK_AS_TIMEOUT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_URILOADER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (32);
		/* Used when the data from a channel has already been parsed and cached so it
		 * doesn't need to be reparsed from the original source. */
		public const int NS_ERROR_PARSED_DATA_CACHED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_URILOADER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (33);

		/* This success code indicates that a refresh header was found and
		 * successfully setup.  */
		public const int NS_REFRESHURI_HEADER_FOUND = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_URILOADER + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);


		/* ======================================================================= */
		/* 25: NS_ERROR_MODULE_CONTENT */
		/* ======================================================================= */
		/* Error codes for image loading */
		public const int NS_ERROR_IMAGE_SRC_CHANGED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_CONTENT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (4);
		public const int NS_ERROR_IMAGE_BLOCKED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_CONTENT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (5);
		/* Error codes for content policy blocking */
		public const int NS_ERROR_CONTENT_BLOCKED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_CONTENT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (6);
		public const int NS_ERROR_CONTENT_BLOCKED_SHOW_ALT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_CONTENT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (7);
		/* Success variations of content policy blocking */
		public const int NS_PROPTABLE_PROP_NOT_THERE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_CONTENT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (10);
		/* Error code for XBL */
		public const int NS_ERROR_XBL_BLOCKED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_CONTENT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (15);

		/* XXX this is not really used */
		public const int NS_HTML_STYLE_PROPERTY_NOT_THERE = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_CONTENT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);
		public const int NS_CONTENT_BLOCKED = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_CONTENT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (8);
		public const int NS_CONTENT_BLOCKED_SHOW_ALT = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_CONTENT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (9);
		public const int NS_PROPTABLE_PROP_OVERWRITTEN = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_CONTENT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (11);
		/* Error codes for FindBroadcaster in nsXULDocument.cpp */
		public const int NS_FINDBROADCASTER_NOT_FOUND = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_CONTENT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (12);
		public const int NS_FINDBROADCASTER_FOUND = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_CONTENT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (13);
		public const int NS_FINDBROADCASTER_AWAIT_OVERLAYS = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_CONTENT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (14);


		/* ======================================================================= */
		/* 27: NS_ERROR_MODULE_XSLT */
		/* ======================================================================= */
		public const int NS_ERROR_XPATH_INVALID_ARG = NS_ERROR_INVALID_ARG;

		public const int NS_ERROR_XSLT_PARSE_FAILURE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		public const int NS_ERROR_XPATH_PARSE_FAILURE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);
		public const int NS_ERROR_XSLT_ALREADY_SET = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (3);
		public const int NS_ERROR_XSLT_EXECUTION_FAILURE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (4);
		public const int NS_ERROR_XPATH_UNKNOWN_FUNCTION = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (5);
		public const int NS_ERROR_XSLT_BAD_RECURSION = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (6);
		public const int NS_ERROR_XSLT_BAD_VALUE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (7);
		public const int NS_ERROR_XSLT_NODESET_EXPECTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (8);
		public const int NS_ERROR_XSLT_ABORTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (9);
		public const int NS_ERROR_XSLT_NETWORK_ERROR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (10);
		public const int NS_ERROR_XSLT_WRONG_MIME_TYPE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (11);
		public const int NS_ERROR_XSLT_LOAD_RECURSION = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (12);
		public const int NS_ERROR_XPATH_BAD_ARGUMENT_COUNT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (13);
		public const int NS_ERROR_XPATH_BAD_EXTENSION_FUNCTION = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (14);
		public const int NS_ERROR_XPATH_PAREN_EXPECTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (15);
		public const int NS_ERROR_XPATH_INVALID_AXIS = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (16);
		public const int NS_ERROR_XPATH_NO_NODE_TYPE_TEST = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (17);
		public const int NS_ERROR_XPATH_BRACKET_EXPECTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (18);
		public const int NS_ERROR_XPATH_INVALID_VAR_NAME = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (19);
		public const int NS_ERROR_XPATH_UNEXPECTED_END = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (20);
		public const int NS_ERROR_XPATH_OPERATOR_EXPECTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (21);
		public const int NS_ERROR_XPATH_UNCLOSED_LITERAL = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (22);
		public const int NS_ERROR_XPATH_BAD_COLON = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (23);
		public const int NS_ERROR_XPATH_BAD_BANG = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (24);
		public const int NS_ERROR_XPATH_ILLEGAL_CHAR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (25);
		public const int NS_ERROR_XPATH_BINARY_EXPECTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (26);
		public const int NS_ERROR_XSLT_LOAD_BLOCKED_ERROR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (27);
		public const int NS_ERROR_XPATH_INVALID_EXPRESSION_EVALUATED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (28);
		public const int NS_ERROR_XPATH_UNBALANCED_CURLY_BRACE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (29);
		public const int NS_ERROR_XSLT_BAD_NODE_NAME = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (30);
		public const int NS_ERROR_XSLT_VAR_ALREADY_SET = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (31);

		public const int NS_XSLT_GET_NEW_HANDLER = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_XSLT + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);


		/* ======================================================================= */
		/* 29: NS_ERROR_MODULE_SVG */
		/* ======================================================================= */
		/* SVG DOM error codes from http://www.w3.org/TR/SVG11/svgdom.html */
		public const int NS_ERROR_DOM_SVG_WRONG_TYPE_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SVG + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (0);
		/* Yes, the spec says "INVERTABLE", not "INVERTIBLE" */
		public const int NS_ERROR_DOM_SVG_MATRIX_NOT_INVERTABLE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_SVG + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);


		/* ======================================================================= */
		/* 30: NS_ERROR_MODULE_STORAGE */
		/* ======================================================================= */
		/* To add additional errors to Storage, please append entries to the bottom
		 * of the list in the following format:
		 *   NS_ERROR_STORAGE_YOUR_ERR,  FAILURE(n)
		 * where n is the next unique positive integer.  You must also add an entry
		 * to js/xpconnect/src/xpc.msg under the code block beginning with the
		 * comment 'storage related codes (from mozStorage.h)', in the following
		 * format: 'XPC_MSG_DEF(NS_ERROR_STORAGE_YOUR_ERR, "brief description of your
		 * error")' */
		public const int NS_ERROR_STORAGE_BUSY = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_STORAGE + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		public const int NS_ERROR_STORAGE_IOERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_STORAGE + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);
		public const int NS_ERROR_STORAGE_CONSTRAINT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_STORAGE + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (3);


		/* ======================================================================= */
		/* 32: NS_ERROR_MODULE_DOM_FILE */
		/* ======================================================================= */
		public const int NS_ERROR_DOM_FILE_NOT_FOUND_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_FILE + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (0);
		public const int NS_ERROR_DOM_FILE_NOT_READABLE_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_FILE + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		public const int NS_ERROR_DOM_FILE_ABORT_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_FILE + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);


		/* ======================================================================= */
		/* 33: NS_ERROR_MODULE_DOM_INDEXEDDB */
		/* ======================================================================= */
		/* IndexedDB error codes http://dvcs.w3.org/hg/IndexedDB/raw-file/tip/Overview.html */
		public const int NS_ERROR_DOM_INDEXEDDB_UNKNOWN_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_INDEXEDDB + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		public const int NS_ERROR_DOM_INDEXEDDB_NOT_FOUND_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_INDEXEDDB + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (3);
		public const int NS_ERROR_DOM_INDEXEDDB_CONSTRAINT_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_INDEXEDDB + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (4);
		public const int NS_ERROR_DOM_INDEXEDDB_DATA_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_INDEXEDDB + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (5);
		public const int NS_ERROR_DOM_INDEXEDDB_NOT_ALLOWED_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_INDEXEDDB + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (6);
		public const int NS_ERROR_DOM_INDEXEDDB_TRANSACTION_INACTIVE_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_INDEXEDDB + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (7);
		public const int NS_ERROR_DOM_INDEXEDDB_ABORT_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_INDEXEDDB + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (8);
		public const int NS_ERROR_DOM_INDEXEDDB_READ_ONLY_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_INDEXEDDB + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (9);
		public const int NS_ERROR_DOM_INDEXEDDB_TIMEOUT_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_INDEXEDDB + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (10);
		public const int NS_ERROR_DOM_INDEXEDDB_QUOTA_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_INDEXEDDB + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (11);
		public const int NS_ERROR_DOM_INDEXEDDB_VERSION_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_INDEXEDDB + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (12);
		public const int NS_ERROR_DOM_INDEXEDDB_RECOVERABLE_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_INDEXEDDB + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1001);


		/* ======================================================================= */
		/* 34: NS_ERROR_MODULE_DOM_FILEHANDLE */
		/* ======================================================================= */
		public const int NS_ERROR_DOM_FILEHANDLE_UNKNOWN_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_FILEHANDLE + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		public const int NS_ERROR_DOM_FILEHANDLE_NOT_ALLOWED_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_FILEHANDLE + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);
		public const int NS_ERROR_DOM_FILEHANDLE_LOCKEDFILE_INACTIVE_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_FILEHANDLE + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (3);
		public const int NS_ERROR_DOM_FILEHANDLE_ABORT_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_FILEHANDLE + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (4);
		public const int NS_ERROR_DOM_FILEHANDLE_READ_ONLY_ERR = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_DOM_FILEHANDLE + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (5);


		/* ======================================================================= */
		/* 51: NS_ERROR_MODULE_GENERAL */
		/* ======================================================================= */
		/* Error code used internally by the incremental downloader to cancel the
		 * network channel when the download is already complete. */
		public const int NS_ERROR_DOWNLOAD_COMPLETE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GENERAL + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		/* Error code used internally by the incremental downloader to cancel the
		 * network channel when the response to a range request is 200 instead of
		 * 206. */
		public const int NS_ERROR_DOWNLOAD_NOT_PARTIAL = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GENERAL + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2);
		public const int NS_ERROR_UNORM_MOREOUTPUT = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GENERAL + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (33);

		public const int NS_ERROR_DOCSHELL_REQUEST_REJECTED = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GENERAL + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1001);
		/* This is needed for displaying an error message when navigation is
		 * attempted on a document when printing The value arbitrary as long as it
		 * doesn't conflict with any of the other values in the errors in
		 * DisplayLoadError */
		public const int NS_ERROR_DOCUMENT_IS_PRINTMODE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GENERAL + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (2001);

		public const int NS_SUCCESS_DONT_FIXUP = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_GENERAL + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		/* This success code may be returned by nsIAppStartup::Run to indicate that
		 * the application should be restarted.  This condition corresponds to the
		 * case in which nsIAppStartup::Quit was called with the eRestart flag. */
		public const int NS_SUCCESS_RESTART_APP = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_GENERAL + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (1);
		public const int NS_SUCCESS_UNORM_NOTFOUND = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_GENERAL + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (17);


		/* a11y */
		/* raised when current pivot's position is needed but it's not in the tree */
		public const int NS_ERROR_NOT_IN_TREE = (NS_ERROR_SEVERITY_ERROR << 31) | ((NS_ERROR_MODULE_GENERAL + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (38);

		/* see Accessible::GetAttrValue */
		public const int NS_OK_NO_ARIA_VALUE = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_GENERAL + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (33);
		/* see nsTextEquivUtils */
		public const int NS_OK_NO_NAME_CLAUSE_HANDLED = (NS_ERROR_SEVERITY_SUCCESS << 31) | ((NS_ERROR_MODULE_GENERAL + NS_ERROR_MODULE_BASE_OFFSET) << 16) | (34);
	}
}
