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
using Gecko.DOM;
using Gecko.Services;


#endregion END LICENSE BLOCK

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Gecko.Interop;
using System.Threading;
using Gecko.Windows;


// XpCom function are declared like
//XPCOM_API(nsresult)
//NS_InitXPCOM2(nsIServiceManager* *result, 
//              nsIFile* binDirectory,
//              nsIDirectoryServiceProvider* appFileLocationProvider);

// XPCOM_API(type) is EXPORT_XPCOM_API(type)

// #define EXPORT_XPCOM_API(type) is NS_EXTERN_C NS_EXPORT type NS_FROZENCALL

//NS_FROZENCALL is __cdecl

// so all XPCOM_API functions MUST BE Cdecl

namespace Gecko
{
	/// <summary>
	/// Provides low-level access to XPCOM.
	/// </summary>
	public static class Xpcom
	{
		#region XpCom Methods
		/// <summary>
		/// Declaration in nsXPCOM.h
		/// XPCOM_API(nsresult) NS_InitXPCOM2(nsIServiceManager* *result, nsIFile* binDirectory, nsIDirectoryServiceProvider* appFileLocationProvider);
		/// </summary>
		/// <param name="serviceManager"></param>
		/// <param name="binDirectory"></param>
		/// <param name="appFileLocationProvider"></param>
		/// <returns></returns>
		[DllImport("xul", CharSet = CharSet.Ansi,CallingConvention = CallingConvention.Cdecl)]
		static extern int NS_InitXPCOM2([MarshalAs(UnmanagedType.Interface)] out nsIServiceManager serviceManager, [MarshalAs(UnmanagedType.IUnknown)] object binDirectory, nsIDirectoryServiceProvider appFileLocationProvider);

		/// <summary>
		/// Shutdown XPCOM. You must call this method after you are finished
		/// using xpcom.
		/// 
		/// Declaration in nsXPCOM.h
		/// XPCOM_API(nsresult) NS_ShutdownXPCOM(nsIServiceManager* servMgr);
		/// </summary>
		/// <param name="serviceManager"></param>
		/// <returns></returns>
		[DllImport("xul", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		static extern int NS_ShutdownXPCOM([MarshalAs(UnmanagedType.Interface)] nsIServiceManager serviceManager);

		/// <summary>
		/// Declaration in nsXPCOM.h
		/// XPCOM_API(nsresult) NS_NewLocalFile(const nsAString &path, bool followLinks, nsIFile* *result);
		/// </summary>
		/// <param name="path"></param>
		/// <param name="followLinks"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		[DllImport("xul", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		static extern int NS_NewLocalFile([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Gecko.CustomMarshalers.AStringMarshaler))] nsAString path, bool followLinks, [MarshalAs(UnmanagedType.IUnknown)] out object result);

		/// <summary>
		/// Declaration in nsXPCOM.h
		/// XPCOM_API(nsresult) NS_GetComponentManager(nsIComponentManager* *result);
		/// </summary>
		/// <param name="componentManager"></param>
		/// <returns></returns>
		[DllImport("xul", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		static extern int NS_GetComponentManager([MarshalAs(UnmanagedType.Interface)]out nsIComponentManager componentManager);

		/// <summary>
		/// Declaration in nsXPCOM.h
		/// XPCOM_API(nsresult) NS_GetComponentRegistrar(nsIComponentRegistrar* *result);
		/// </summary>
		/// <param name="componentRegistrar"></param>
		/// <returns></returns>
		[DllImport("xul", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
		static extern int NS_GetComponentRegistrar([MarshalAs(UnmanagedType.Interface)] out nsIComponentRegistrar componentRegistrar);

		/// <summary>
		/// XPCOM_API(void*) NS_Alloc(PRSize size);
		/// </summary>
		/// <param name="size"></param>
		/// <returns></returns>
		[DllImport("xul", EntryPoint = "NS_Alloc", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr Alloc(IntPtr size);

		/// <summary>
		/// XPCOM_API(void*) NS_Realloc(void* ptr, PRSize size);
		/// </summary>
		/// <param name="ptr"></param>
		/// <param name="size"></param>
		/// <returns></returns>
		[DllImport("xul", EntryPoint = "NS_Realloc", CallingConvention = CallingConvention.Cdecl)]
		public static extern IntPtr Realloc(IntPtr ptr, IntPtr size);

		/// <summary>
		/// XPCOM_API(void) NS_Free(void* ptr)
		/// </summary>
		/// <param name="ptr"></param>
		[DllImport("xul", EntryPoint = "NS_Free", CallingConvention = CallingConvention.Cdecl)]
		public static extern void Free(IntPtr ptr);
		#endregion

		#region Fields
		static readonly bool _isMono;
		static readonly bool _is64Bit;
		static readonly bool _is32Bit;
		static bool _IsInitialized;
		static string _ProfileDirectory;
		static int _XpcomThreadId;
		private static string _xulrunnerVersion;
		private static COMGC _comGC;
		#endregion


		static Xpcom()
		{
			// get runtime information at first start
			//http://www.mono-project.com/Guide:_Porting_Winforms_Applications
			_isMono = Type.GetType( "Mono.Runtime" ) != null;
			_is64Bit = IntPtr.Size == 8;
			_is32Bit = IntPtr.Size == 4;
		}

		#region Events

		/// <summary>
		/// Occurs once after XpCom has been initalized.
		/// </summary>
		public static event Action BeforeInitalization;

		/// <summary>
		/// Occurs once after XpCom has been initalized.
		/// </summary>
		public static event Action AfterInitalization;

		#endregion

		#region CLR runtime
		/// <summary>
		/// GeckoFX is running on Linux
		/// </summary>
		public static bool IsLinux
		{
			get { return Environment.OSVersion.Platform == PlatformID.Unix; }
		}

		/// <summary>
		/// GeckoFX is running on Windows
		/// </summary>
		public static bool IsWindows
		{
			get { return Environment.OSVersion.Platform != PlatformID.Unix; ; }
		}

		/// <summary>
		/// GeckoFX is running on Mono CLR implementation
		/// </summary>
		public static bool IsMono
		{
			get { return _isMono; }
		}

		/// <summary>
		///  GeckoFX is running on Microsoft CLR implementation (.NET Framework)
		/// </summary>
		public static bool IsDotNet
		{
			get { return !_isMono; }
		}

		public static bool Is32Bit
		{
			get { return _is32Bit; }
		}

		public static bool Is64Bit
		{
			get { return _is64Bit; }
		}        
		#endregion

		public static bool IsInitialized
		{
			get { return _IsInitialized; }
		}

		/// <summary>
		/// Current Xulrunner version
		/// </summary>
		public static string XulRunnerVersion
		{
			get { return _xulrunnerVersion; }
		}

		internal static ChromeContext ChromeContext { get; private set; }

		public static nsIComponentManager ComponentManager;
		public static nsIComponentRegistrar ComponentRegistrar;
		public static nsIServiceManager ServiceManager;

		#region Init & shudown
		/// <summary>
		/// Initializes XPCOM using the current directory as the XPCOM directory.
		/// </summary>
		public static void Initialize()
		{
			Initialize(null);
		}

		/// <summary>
		/// Initializes XPCOM using the specified directory.
		/// </summary>
		/// <param name="binDirectory">The directory which contains xul.dll.</param>
		public static void Initialize(string binDirectory)
		{
			if (_IsInitialized)
				return;

            Debug.WriteLineIf(Thread.CurrentThread.GetApartmentState() != ApartmentState.STA, "Warning: Main Entry point missing [STAThread] attribute.");
            Debug.Assert(Thread.CurrentThread.GetApartmentState() == ApartmentState.STA, "Main Entry point missing [STAThread] attribute.");		    

			if (BeforeInitalization != null)
				BeforeInitalization();

			Interlocked.Exchange(ref _XpcomThreadId, Thread.CurrentThread.ManagedThreadId);

			if (IsWindows)
				Kernel32.SetDllDirectory(binDirectory);
			
			string folder = binDirectory ?? Environment.CurrentDirectory;
			string xpcomPath = Path.Combine(folder, IsLinux ? "libxul.so" : "xul.dll");
						
			try
			{
				// works on windows
				// but some classes can be not exist on mono (may be)
				// so make it in another function(stack allocation is making on function start)
				ReadXulrunnerVersion(xpcomPath);
			}
			catch ( Exception )
			{
			}
			

			if (binDirectory != null)
			{
				Environment.SetEnvironmentVariable("PATH",
					Environment.GetEnvironmentVariable("PATH") + ";" + binDirectory, EnvironmentVariableTarget.Process);
			}
			
			object mreAppDir = null;
			
			if (binDirectory != null)
			{
				using (nsAString str = new nsAString(Path.GetFullPath(binDirectory)))
					if (NS_NewLocalFile(str, true, out mreAppDir) != 0)
					{
						throw new Exception("Failed on NS_NewLocalFile");
					}
			}
			
			// temporarily change the current directory so NS_InitEmbedding can find all the DLLs it needs
			String oldCurrent = Environment.CurrentDirectory;
			Environment.CurrentDirectory = folder;
			
			nsIServiceManager serviceManager;
			//int res = NS_InitXPCOM2(out serviceManagerPtr, mreAppDir, new DirectoryServiceProvider());


			//Note: the error box that this can generate can't be prevented with try/catch, and res is 0 even if it fails
			//REVIEW: how else can we determine what happened and give a more useful answer, to help new GeckoFX users,
			//Telling them that probably the version of firefox or xulrunner didn't match this library version?
			
			// calling NS_InitXPCOM2 invokes AddRef to the returned nsIServerManager, but as this gets assigned to the __ComObject serviceManager
			// Release will be called by the when the GC runs __ComObject finaliser.
			int res = NS_InitXPCOM2(out serviceManager, mreAppDir, null);
			
			// change back
			Environment.CurrentDirectory = oldCurrent;
			
			if (res != 0)
			{
				throw new Exception("Failed on NS_InitXPCOM2");
			}
			
			ServiceManager = (nsIServiceManager)serviceManager;
			
			// get some global objects we will need later
			NS_GetComponentManager(out ComponentManager);
			NS_GetComponentRegistrar(out ComponentRegistrar);

			if (IsMono)
			{
				_comGC = new COMGC();
			}

			// RegisterProvider is necessary to get link styles etc.
			nsIDirectoryService directoryService = GetService<nsIDirectoryService>("@mozilla.org/file/directory_service;1");
			if (directoryService != null)
				directoryService.RegisterProvider(new DirectoryServiceProvider());

			_IsInitialized = true;
			GlobalJSContextHolder.Initialize();

            // On Linux calling CreateWindowlessBrowser too early crashes with passing null to gdk_window_enable_synchronized_configure()
            // the gdkwidgets window is null.
            if (!Xpcom.IsLinux)
                InitChromeContext();

			XULAppInfoFactory.Init();
			PromptFactoryFactory.Init();

			if (AfterInitalization != null)
				AfterInitalization();
		}

        public static void InitChromeContext()
        {
            if (Xpcom.ChromeContext != null)
                return;

            Xpcom.ChromeContext = new ChromeContext();
        }

		private static void ReadXulrunnerVersion(string xulDll)
		{
			FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo( xulDll );
			_xulrunnerVersion = fileVersionInfo.FileVersion;
		}

		public static void Shutdown()
		{
			GeckoPreferences.Shutdown();
			WindowWatcher.Shutdown();
			WindowMediator.Shutdown();

			PromptFactoryFactory.Shutdown();
			Xpcom.DisposeObject(ref _comGC );

			if (Xpcom.ChromeContext != null)
			{
				Xpcom.ChromeContext.Dispose();
				Xpcom.ChromeContext = null;
			}
			
			if (ComponentRegistrar != null)
				Marshal.ReleaseComObject(ComponentRegistrar);
			
			if (ComponentManager != null)
				Marshal.ReleaseComObject(ComponentManager);

			if (ServiceManager != null)
			{
				// NS_ShutdownXPCOM calls Release on the ServiceManager COM objects.
				// However since the ServiceManager is a __ComObject its finaliser will also call release.
				var ptr = Marshal.GetIUnknownForObject(ServiceManager);
				NS_ShutdownXPCOM(ServiceManager);
				Marshal.ReleaseComObject(ServiceManager);
			}

			_IsInitialized = false;
		}

		public static void AssertCorrectThread()
		{
			if (Thread.CurrentThread.ManagedThreadId != _XpcomThreadId)
			{
				throw new InvalidOperationException("GeckoFx can only be called from the same thread on which it was initialized (normally the UI thread).");
			}
		}

		public static bool InvokeRequired
		{
			get
			{
				return Thread.CurrentThread.ManagedThreadId != _XpcomThreadId;
			}
		}

		#endregion

		/// <summary>
		/// Gets or sets the path to the directory which contains the user profile.
		/// The default directory is Geckofx\DefaultProfile in the user's local application data directory.
		/// </summary>
		public static string ProfileDirectory
		{
			get
			{
				if (_ProfileDirectory == null)
				{
					string directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Path.Combine("Geckofx", "DefaultProfile"));

					if (!Directory.Exists(directory))
					{
						Directory.CreateDirectory(directory);
					}
					return directory;
				}
				return _ProfileDirectory;
			}
			set
			{
				if (!String.IsNullOrEmpty(value))
				{
					if (!Directory.Exists(value))
					{
						throw new DirectoryNotFoundException();
					}
				}
				_ProfileDirectory = value;
			}
		}

		#region External Methods

		#region XPConnect

		public static ComPtr<nsIXPConnect> XPConnect
		{
			get
			{
				return Xpcom.GetService<nsIXPConnect>("@mozilla.org/js/xpc/XPConnect;1").AsComPtr();
			}
		}

		[UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.ThisCall)]
		internal delegate IntPtr GetJSObjectFromHolderDelegate([MarshalAs(UnmanagedType.Interface)] nsIXPConnectJSObjectHolder @this);

		[UnmanagedFunctionPointer(System.Runtime.InteropServices.CallingConvention.ThisCall)]
		internal delegate IntPtr GetSafeJSContextDelegate([MarshalAs(UnmanagedType.Interface)] nsIXPConnect @this);

		#endregion

		public static object NewNativeLocalFile(string filename)
		{
			object result;
			
			using (nsAString str = new nsAString(filename))
				if (NS_NewLocalFile(str, true, out result) == 0)
					return result;
			
			return null;
		}

		#region CreateInstance
		//public static object CreateInstance(Guid classIID)
		//{
		//	Guid iid = typeof(nsISupports).GUID;
		//	return ComponentManager.CreateInstance(ref classIID, null, ref iid);
		//}
		
		//public static object CreateInstance(string contractID)
		//{
		//	return CreateInstance<nsISupports>(contractID);
		//}
		
		public static TInterfaceType CreateInstance<TInterfaceType>(string contractID)
		{
			Guid iid = typeof(TInterfaceType).GUID;
			IntPtr ptr = ComponentManager.CreateInstanceByContractID(contractID, null, ref iid);
			return (TInterfaceType)Xpcom.GetObjectForIUnknown(ptr);
		}

		/// <summary>
		/// Creation function for temporal variables with automatic release
		/// </summary>
		/// <typeparam name="TInterfaceType"></typeparam>
		/// <param name="contractID"></param>
		/// <returns></returns>
		public static ComPtr<TInterfaceType> CreateInstance2<TInterfaceType>( string contractID )
			where TInterfaceType : class
		{
			var instance = CreateInstance<TInterfaceType>( contractID );
			return new ComPtr<TInterfaceType>( instance );
		}

		#endregion

		#region QueryInterface
		
		public static TInterfaceType QueryInterface<TInterfaceType>(object obj)
		{
			return (TInterfaceType)QueryInterface(obj, typeof(TInterfaceType).GUID);
		}

		public static object QueryInterface(object obj, Guid iid)
		{
			AssertCorrectThread();

			if (obj == null)
				return null;

			// get an nsISupports (aka IUnknown) pointer from the object
			IntPtr pUnk = Marshal.GetIUnknownForObject(obj);
			if (pUnk == IntPtr.Zero)
				return null;
			
			// query interface
			IntPtr ppv;
			Marshal.QueryInterface(pUnk, ref iid, out ppv);
			
			// if QueryInterface didn't work, try using nsIInterfaceRequestor instead
			if (ppv == IntPtr.Zero)
			{
				// QueryInterface the object for nsIInterfaceRequestor
				Guid interfaceRequestorIID = typeof(nsIInterfaceRequestor).GUID;
				IntPtr pInterfaceRequestor;
				Marshal.QueryInterface(pUnk, ref interfaceRequestorIID, out pInterfaceRequestor);
				
				// if we got a pointer to nsIInterfaceRequestor
				if (pInterfaceRequestor != IntPtr.Zero)
				{
					// convert it to a managed interface
					QI_nsIInterfaceRequestor req = (QI_nsIInterfaceRequestor)Xpcom.GetObjectForIUnknown(pInterfaceRequestor);
					
					if (req != null)
					{
					
						try
						{
							req.GetInterface(ref iid, out ppv);
							// clean up
							Marshal.ReleaseComObject(req);
						}
						catch(NullReferenceException ex)
						{
							Debug.WriteLine("NullRefException from native code.\n" + ex.ToString());
						}
					}
					Marshal.Release(pInterfaceRequestor);
				}
			}
			
			object result = (ppv != IntPtr.Zero) ? Xpcom.GetObjectForIUnknown(ppv) : null;
			
			Marshal.Release(pUnk);
			if (ppv != IntPtr.Zero)
				Marshal.Release(ppv);
			
			return result;
		}
		#endregion

		#region GetService
		public static IntPtr GetService(Guid classIID)
		{
			AssertCorrectThread();

			Guid iid = typeof(nsISupports).GUID;
			return ServiceManager.GetService(ref classIID, ref iid);
		}
		
		//public static object GetService(string contractID)
		//{
		//    return GetService<nsISupports>(contractID);
		//}
		
		public static TInterfaceType GetService<TInterfaceType>(string contractID)
		{
			AssertCorrectThread();

			Guid iid = typeof(TInterfaceType).GUID;
			IntPtr ptr = ServiceManager.GetServiceByContractID(contractID, ref iid);
			return (TInterfaceType)Xpcom.GetObjectForIUnknown(ptr);
		}

		/// <summary>
		/// Create service wrapped into ComPtr'T
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="contractID"></param>
		/// <returns></returns>
		public static ComPtr<T> GetService2<T>(string contractID)
			where T : class
		{
			if (!Xpcom.IsInitialized)
			{
				throw new GeckoException("Xpcom.Initialize must be called before using of any xulrunner/gecko-fx services");
			}
			var service = Xpcom.GetService<T>(contractID);
			var ret = new ComPtr<T>(service);
			return ret;
		}
		#endregion
		/// <summary>
		/// Registers a factory to be used to instantiate a particular class identified by ClassID, and creates an association of class name and ContractID with the class.
		/// </summary>
		/// <param name="classID">The ClassID of the class being registered.</param>
		/// <param name="className">The name of the class being registered. This value is intended as a human-readable name for the class and need not be globally unique.</param>
		/// <param name="contractID">The ContractID of the class being registered.</param>
		/// <param name="factory">The nsIFactory instance of the class being registered.</param>
		public static void RegisterFactory(Guid classID, string className, string contractID, nsIFactory factory)
		{
			ComponentRegistrar.RegisterFactory(ref classID, className, contractID, factory);
		}

		
		#endregion

		public static TInterfaceType GetInterface<TInterfaceType>( this nsIInterfaceRequestor requestor )
		{
			return ( TInterfaceType ) GetInterface( requestor, typeof( TInterfaceType ).GUID );
		}

		private static object GetInterface( nsIInterfaceRequestor requestor, Guid iid )
		{
			IntPtr ppv = IntPtr.Zero;
			try
			{
				ppv = requestor.GetInterface( ref iid );
			}
			catch ( Exception ex )
			{
				Debug.WriteLine( "Exception \n" + ex.ToString() );
			}
			object result = ( ppv != IntPtr.Zero ) ? GetObjectForIUnknown( ppv ) : null;
			int count = Marshal.Release( ppv );
			return result;
		}


		///	<summary>
		/// Helper method for WeakReference
		///	</summary>
		internal static IntPtr QueryReferent(object obj, ref Guid uuid )
		{		
			Xpcom.AssertCorrectThread();

			IntPtr ppv, pUnk = Marshal.GetIUnknownForObject(obj);

			Marshal.QueryInterface(pUnk, ref uuid, out ppv);

			Marshal.Release(pUnk);

			return ppv;
   
		}
		
		public static object GetObjectForIUnknown(IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
				return null;
						
			// Mono (2.10.8.1) bug : Marshal.GetObjectForIUnknown is not incrementing the COM objects ref count.			
			if (IsMono)			
				Marshal.AddRef(ptr);
			
			return Marshal.GetObjectForIUnknown(ptr);
		}

		internal static void DisposeObject<T>(ref T obj)
			where T :class ,IDisposable
		{
			// take it to local variable
			var localObj = Interlocked.Exchange(ref obj, null);
			// if it is already null -> return
			if (localObj == null) return;
			// Dispose
			localObj.Dispose();
		}

		public static void FreeComObject<T>(ref T obj)
			where T : class
		{
#if false
			// When debug -> use special version, that writes debug information
			ComDebug.DebugFreeComObject( ref obj );
#else	
			// take it to local variable
			var localObj = Interlocked.Exchange(ref obj, null);
			// if it is already null -> return
			if (localObj == null) return;

			if (Marshal.IsComObject(localObj)) // Is real COM? Not CLR object?
			{
				if (IsMono && Xpcom.InvokeRequired)
				{
					// Xpcom.Shutdown() may have already been called.
					if (_comGC != null)
						_comGC.Free(ref localObj);
				}
				else
					Marshal.ReleaseComObject(localObj);
			}
#endif
		}

		/// <summary>
		/// special version should be used only if we absolutly sure
		/// Use for only for determinated free (using ..., create & free, etc)
		/// Don't use in destructors
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		internal static void FreeComObjectDeterminate<T>( ref T obj )
			where T : class
		{
			// take it to local variable
			var localObj = Interlocked.Exchange(ref obj, null);
			// if it is already null -> return
			if (localObj == null) return;
			Marshal.ReleaseComObject(localObj);
		}
	


		

		internal static void FinalFreeComObject<T>(ref T obj)
			where T : class
		{
			// take it to local variable
			var localObj = Interlocked.Exchange(ref obj, null);
			// if it is already null -> return
			if (localObj == null) return;
			// release
			if (Marshal.IsComObject(localObj)) // Is real COM? Not CLR object?
			{
				if (IsMono && Xpcom.InvokeRequired)
				{
					// Xpcom.Shutdown() may have already been called.
					if (_comGC != null)
						_comGC.FinalFree(ref localObj);
				}
				else
				{
					Marshal.FinalReleaseComObject(localObj);
				}
			}
		}

		public static IntPtr WebBrowserGetInterface<T>(T geckoWebBrowser, nsIWebBrowser instance, ref Guid uuid)
		where T : IGeckoWebBrowser
		{

			object obj = geckoWebBrowser;

			// note: when a new window is created, gecko calls GetInterface on the webbrowser to get a DOMWindow in order
			// to set the starting url
			if (instance != null)
			{
				if (uuid == typeof(nsIDOMWindow).GUID)
				{
					obj = instance.GetContentDOMWindowAttribute();
				}
				else if (uuid == typeof(nsIDOMDocument).GUID)
				{
					obj = instance.GetContentDOMWindowAttribute().GetDocumentAttribute();
				}
			}

			IntPtr ppv, pUnk = Marshal.GetIUnknownForObject(obj);

			Marshal.QueryInterface(pUnk, ref uuid, out ppv);

			Marshal.Release(pUnk);

			return ppv;
		}

		#region Internal class & interface declarations
		#region QI_nsIInterfaceRequestor	
		/// <summary>
		/// A special declaration of nsIInterfaceRequestor used only for QueryInterface, using PreserveSig
		/// to prevent .NET from throwing an exception when the interface doesn't exist.
		/// </summary>
		[Guid("033a1470-8b2a-11d3-af88-00a024ffc08c"), ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		interface QI_nsIInterfaceRequestor
		{

			[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
			[PreserveSig]
			int GetInterface(ref Guid uuid, out IntPtr pUnk);
		}
		#endregion

		#region DirectoryServiceProvider
		/// <summary>
		/// A simple nsIDirectoryServiceProvider which provides the profile directory, etc.
		/// This is still an incomplete implementation and can cause issues -- if this happens, add the missing item.
		/// For lists of items in DirectoryService, see
		/// https://developer.mozilla.org/en-US/Add-ons/Code_snippets/File_I_O#Getting_files_in_special_directories
		/// http://mxr.mozilla.org/mozilla-central/source/xpcom/build/nsXULAppAPI.h
		/// </summary>
		class DirectoryServiceProvider : nsIDirectoryServiceProvider
		{
			public nsIFile GetFile(string prop, ref bool persistent)
			{
				switch (prop)
				{
					case "ProfD":
					case "ProfLD":
					case "ProfDS":
					case "ProfLDS":
						return (nsIFile)NewNativeLocalFile(ProfileDirectory ?? "");
					case "UMimTyp": // required to handle mailto protocol, etc.
						return (nsIFile)NewNativeLocalFile(Path.Combine(ProfileDirectory ?? "", "mimeTypes.rdf"));
					case "UChrm":
						return (nsIFile)NewNativeLocalFile(Path.Combine(ProfileDirectory, "chrome"));
					case "TmpD":
						return (nsIFile)NewNativeLocalFile(Path.GetTempPath());
					default:
						Debug.WriteLine("Gecko.Xpcom.DirectoryServiceProvider.GetFile: not implemented: " + prop);
						return null;
				}
			}
		}
		#endregion

		/// <summary>
		/// Class for time conversion
		/// Contains time conversion static methods
		/// </summary>
		public static class Time
		{
			private static readonly DateTime _utcLinuxStartEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

			public static DateTime EpochStart
			{
				get { return _utcLinuxStartEpoch; }
			}

			public static DateTime FromSecondsSinceEpoch(uint time)
			{
				return _utcLinuxStartEpoch.AddSeconds(time);
			}

			public static uint ToSecondsSinceEpoch(DateTime time)
			{
				return (uint)(time.ToUniversalTime() - _utcLinuxStartEpoch).TotalSeconds;
			}
		}

		#endregion

		public static Uri ToUri(this nsIURI value)
		{
			if (value == null) return null;
			var spec = nsString.Get(value.GetSpecAttribute);
			Uri result;
			return Uri.TryCreate(spec, UriKind.Absolute, out result) ? result : null;
		}

		public static int StrictHashcode<T>(T obj)
		{
			IntPtr pUnk = Marshal.GetIUnknownForObject(obj);
			int ret = pUnk.ToInt32();
			Marshal.Release(pUnk);
			return ret;
		}

		public static bool StrictEquals<T>(T obj1, T obj2)
		{
			IntPtr pUnk1 = Marshal.GetIUnknownForObject(obj1);
			IntPtr pUnk2 = Marshal.GetIUnknownForObject(obj2);
			bool ret = pUnk1 == pUnk2;
			Marshal.Release(pUnk2);
			Marshal.Release(pUnk1);
			return ret;
		}

		/// <summary>
		/// Get nsIURI from attribute, convert it to Uri, and free nsIURI object
		/// </summary>
		/// <param name="uriFunc"></param>
		/// <returns></returns>
		public static Uri TranslateUriAttribute( Func<nsIURI> uriFunc )
		{
			Uri ret = null;
			var uri = uriFunc();
			if ( uri != null )
			{
				ret = uri.ToUri();
				Marshal.ReleaseComObject(uri);
			}
			
			return ret;
		}
	}
}
