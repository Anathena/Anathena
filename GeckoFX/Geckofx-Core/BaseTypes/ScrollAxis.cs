using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Gecko
{
	public enum WhenToScroll : int
	{
		SCROLL_ALWAYS = 0 ,
		SCROLL_IF_NOT_VISIBLE = 1,
		SCROLL_IF_NOT_FULLY_VISIBLE = 2
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct ScrollAxis
	{
		Int16 mWhereToScroll;
		WhenToScroll mWhenToScroll;
	}
}
