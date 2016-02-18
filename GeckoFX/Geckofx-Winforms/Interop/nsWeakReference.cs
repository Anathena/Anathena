using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gecko.Interop
{
	/// <summary>
	/// Special implementation for Control objects
	/// </summary>
	sealed class ControlWeakReference
		: nsWeakReference
	{

		internal ControlWeakReference(Control control)
			:base(control)
		{
			
		}

		protected override IntPtr QueryReferentImplementation(object obj,ref Guid uuid)
		{
			// for Control we check it IsDisposed state
			// if control is disposed -> return IntPtr.Zero 
			return ((Control)obj).IsDisposed
				? IntPtr.Zero
				: base.QueryReferentImplementation(obj,ref uuid);
		}
	}

}
