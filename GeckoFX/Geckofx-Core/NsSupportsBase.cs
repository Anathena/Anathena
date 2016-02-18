using System;
using System.Runtime.InteropServices;

namespace Gecko
{
	public abstract class NsSupportsBase : nsISupports
	{
		public virtual IntPtr QueryInterface(ref System.Guid uuid)
		{
			var pUnk = Marshal.GetIUnknownForObject(this);
			IntPtr ppv;
			try
			{
				int error = Marshal.QueryInterface(pUnk, ref uuid, out ppv);
				if (error != 0)
					Marshal.ThrowExceptionForHR(error);
			}
			finally
			{
				Marshal.Release(pUnk);
			}
			return ppv;
		}

		public virtual int AddRef()
		{
			IntPtr pUnk = Marshal.GetIUnknownForObject(this);
			Marshal.AddRef(pUnk);
			return Marshal.Release(pUnk);
		}

		public virtual int Release()
		{
			IntPtr pUnk = Marshal.GetIUnknownForObject(this);
			Marshal.Release(pUnk);
			return Marshal.Release(pUnk);
		}
	}
}
