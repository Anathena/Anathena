using System;
using System.Runtime.InteropServices;
using Gecko.Interop;

namespace Gecko
{
	public static class ConsoleService
	{
		internal static ComPtr<nsIConsoleService> _consoleService;

		static ConsoleService()
		{
			_consoleService = Xpcom.GetService2<nsIConsoleService>(Contracts.ConsoleService);
		}

		public static void Reset()
		{
			_consoleService.Instance.Reset();
		}
	}
}