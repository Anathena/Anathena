using System;
using System.Runtime.InteropServices;

namespace Gecko
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct MutableHandle
	{
		private IntPtr _ptr;

		public MutableHandle(IntPtr ptr)
		{
			_ptr = ptr;
		}

		public IntPtr Handle
		{
			get { return _ptr; }
		}
	}
}