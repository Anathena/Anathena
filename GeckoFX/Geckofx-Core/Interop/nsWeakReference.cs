using System;

namespace Gecko.Interop
{
	/// <summary>
	/// nsIWeakReference implementation for .NET object
	/// </summary>
	public class nsWeakReference
		: nsIWeakReference
	{
		protected WeakReference _weakReference;

		public nsWeakReference(object obj)
		{
			_weakReference = new WeakReference(obj, false);
		}

		IntPtr nsIWeakReference.QueryReferent(ref Guid uuid)
		{
			// If object is alive we take it to QueryReferentImplementation
			// else return IntPtr.Zero
			return _weakReference.IsAlive
				       ? QueryReferentImplementation(_weakReference.Target, ref uuid)
				       : IntPtr.Zero;
		}

		protected virtual IntPtr QueryReferentImplementation(object obj, ref Guid uuid)
		{
			// by default we make QueryReferent
			return Xpcom.QueryReferent(obj, ref uuid);
		}
	}
}