using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Interop;
using Gecko.Services;

namespace Gecko.Utils
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class VersionComparer
		:IComparer<string>
	{

		public VersionComparer() {}

		public int Compare(string x, string y)
		{
			return VersionComparator.Compare( x, y );
		}
	}
}
