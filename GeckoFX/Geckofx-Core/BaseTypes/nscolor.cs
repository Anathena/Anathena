using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko
{
	// TODO: make this class binary marshalable from struct nscolor
	/// <summary>
	/// nscolor is 32 bit integer value
	/// </summary>
	[StructLayout(LayoutKind.Explicit,Size = 4)]
	public struct nscolor
	{
		[FieldOffset( 0 )]
		private int _value;

		public nscolor(byte r,byte g,byte b)
			:this(255,r,g,b)
		{
			
		}

		public nscolor(byte a,byte r,byte g,byte b)
		{
			_value = MakeNsColor(a, r, g, b);
		}

		public byte A
		{
			get { return ( byte ) ( ( _value & 0xFF000000 ) >> 24 ); }
		}

		public byte R
		{
			get { return (byte)(_value & 0x000000FF); }
		}

		public byte G
		{
			get { return (byte)((_value & 0x0000FF00) >> 8); }
		}

		public byte B
		{
			get { return (byte)((_value & 0x00FF0000) >> 16); }
		}

		public static int MakeNsColor(byte a,byte r,byte g,byte b)
		{
			return ((a << 24) | ((b) << 16) | ((g) << 8) | (r));
		}
	}
}
