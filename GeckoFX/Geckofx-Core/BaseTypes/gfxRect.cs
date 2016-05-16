using System.Runtime.InteropServices;

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
