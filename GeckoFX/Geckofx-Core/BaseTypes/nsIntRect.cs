using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko
{
	// TODO: make this class binary marshalable from struct nsRect
	/*	  
	 *	struct NS_GFX nsRect {
		nscoord x, y;
		nscoord width, height;
	 *
	 * nscoord could be a PRInt32 or a float?
	 * 
	 * never mind. we see it in debugger
	 * float -> 32 bit
	 * int -> 32 bit
	 */
	[StructLayout(LayoutKind.Sequential)]
	public class nsIntRect
	{
		public int X;
		public int Y;
		public int Width;
		public int Height;
	}
}
