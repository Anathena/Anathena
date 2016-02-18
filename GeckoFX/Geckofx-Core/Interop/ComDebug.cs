using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Gecko.Interop
{
#if DEBUG
	/// <summary>
	/// Class for gebug help
	/// </summary>
	public static class ComDebug
	{
		/// <summary>
		/// Gets reference count of gecko object
		/// </summary>
		/// <param name="xulrunnerObject"></param>
		/// <returns></returns>
		public static int GetRcwRefCount<T>(T xulrunnerObject)
			where T:class
		{
		    if (!Marshal.IsComObject(xulrunnerObject))
		        return -1;

			T supports = null;
			try
			{
				// Query itself :)
				supports = Xpcom.QueryInterface<T>( xulrunnerObject );
			}
			catch ( Exception )
			{
			} 
			// If this is not xulrunner COM object - simply return 0
			if (supports == null) return 0;
			int count = Marshal.ReleaseComObject(supports);
			return count;
		}

		public static int GetComRefCount<T>(T xulrunnerObject)
			where T:class
		{
			IntPtr pUnk = Marshal.GetIUnknownForObject(xulrunnerObject);
			if (pUnk == IntPtr.Zero) return -1;
			int count=Marshal.Release( pUnk );
			return count;
		}

		public static void WriteComRefCount<T>(T xulrunnerObject)
			where T:class
		{
			int hashCode = xulrunnerObject.GetHashCode();
			int refCount = GetComRefCount( xulrunnerObject );
			Console.WriteLine( "{0}({1}) - ref:{2}", hashCode, typeof( T ), refCount);
		}

		public static void WriteDebugInfo<T>(T xulrunnerObject)
			where T:class
		{
			int hashCode = xulrunnerObject.GetHashCode();
			int refCount = GetComRefCount(xulrunnerObject);
			int rcwCount = GetRcwRefCount(xulrunnerObject);
			Debug.WriteLine( string.Format( "{0}({1}) - ref:{2},rcw:{3}", hashCode, typeof (T), refCount, rcwCount ) );

			XulObjectInfo info = null;
			if (_xulObjects.TryGetValue( hashCode,out info ))
			{
				info.RefCount = refCount;
				info.RcwCount = rcwCount;
			}else
			{
				info=new XulObjectInfo();
				info.ID = hashCode;
				info.Type = typeof (T);
				info.RefCount = refCount;
				info.RcwCount = rcwCount;
				_xulObjects.Add( hashCode, info );
			}
		}

		public static void DebugFreeComObject<T>(ref T obj)
			where T:class 
		{
			var localObj = Interlocked.Exchange(ref obj, null);
			// if it is already null -> return
			if (localObj == null) return;
			var hash = localObj.GetHashCode();
			// release
			int count = Marshal.ReleaseComObject(localObj);
			Console.WriteLine( "ComRelease {0}({1}),rcw={2}", hash, typeof( T ), count );

			XulObjectInfo info = null;
			if ( _xulObjects.TryGetValue( hash, out info ) )
			{
				info.RcwCount = count;
				if (count==0)
				{
					_xulObjects.Remove( hash );
				}
			}
			else
			{
				Console.WriteLine("Untraced object free");
			}
		}


		public sealed class XulObjectInfo
		{
			public int ID;
			public Type Type;
			/// <summary>
			/// COM reference count
			/// </summary>
			public int RefCount;
			/// <summary>
			/// Runtime Callable Wraper reference Count
			/// </summary>
			public int RcwCount;
		}

		private static Dictionary<int, XulObjectInfo> _xulObjects = new Dictionary<int, XulObjectInfo>();
	}
#endif
}
