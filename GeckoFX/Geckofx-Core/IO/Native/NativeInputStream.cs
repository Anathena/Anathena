using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

// This file will contain BaseNativeInputStream implementations, that DIFF'ers by memory allocation routines

namespace Gecko.IO.Native
{
	/// <summary>
	/// Replacement for ByteArrayInputStream
	/// Native memory will be faster then managed byte[] (ByteArrayInputStream)
	/// We must check what is faster NativeInputStream or NativeInputStream2
	/// </summary>
	public sealed class NativeInputStream
		: BaseNativeInputStream, IDisposable
	{
		private GCHandle _handle;

		#region ctor & dtor
		private NativeInputStream(byte[] array)
		{
			_handle = GCHandle.Alloc(array, GCHandleType.Pinned);
			Init( _handle.AddrOfPinnedObject(), array.Length );
		}


		~NativeInputStream()
		{
			Close();
		}

		public void Dispose()
		{
			Close();
			GC.SuppressFinalize( this );
		}
		#endregion

		public override void Close()
		{
			if (_handle.IsAllocated)
			{
				_handle.Free();
			}
		}

		

		public static NativeInputStream Create(byte[] buffer)
		{
			return buffer == null ? null : new NativeInputStream( buffer );
		}
	}
}
