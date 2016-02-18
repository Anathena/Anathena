using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko
{
	// TODO: make this class binary marshalable from struct gfxRect
	[StructLayout(LayoutKind.Sequential)]
	public class gfxRect
	{
		public double X;
		public double Y;
		public double Width;
		public double Height;
	}
}
