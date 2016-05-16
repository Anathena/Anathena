using Gecko.Services;
using System.Collections.Generic;

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
