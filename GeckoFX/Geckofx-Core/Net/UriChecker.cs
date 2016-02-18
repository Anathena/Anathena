using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Gecko.Net
{
	public sealed class UriChecker
		: Request
	{
		private nsIURIChecker _checker;

		internal UriChecker( nsIURIChecker checker )
			: base( checker )
		{
			_checker = checker;
		}

		public UriChecker( string uri )
			: this( Xpcom.CreateInstance<nsIURIChecker>( Contracts.UriChecker ) )
		{
			var nsUri = IOService.CreateNsIUri( uri );
			_checker.Init( nsUri );
			Marshal.ReleaseComObject( nsUri );
		}
	}
}
