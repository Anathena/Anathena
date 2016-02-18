using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Interop;

namespace Gecko
{
#if false
	/// <summary>
	/// By default Xulrunner DOES NOT IMPLEMENT PrivateBrowsingService
	/// YOU MUST REGISTER YOUR OWN IMPLEMENTATION BEFORE USE THIS WRAPPER!!!
	/// </summary>
	public static class PrivateBrowsingService
	{
		private static ServiceWrapper<nsIPrivateBrowsingService> _privateBrowsingService;
		static PrivateBrowsingService()
		{
			_privateBrowsingService = new ServiceWrapper<nsIPrivateBrowsingService>(Contracts.PrivateBrowsing);
		}
	}
#endif
}
