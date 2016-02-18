using System;
using System.Runtime.InteropServices;
using Gecko.Interop;

namespace Gecko
{
	public static class RandomGenerator
	{
		private static ComPtr<nsIRandomGenerator> _randomGenerator;

		static RandomGenerator()
		{
			_randomGenerator = Xpcom.GetService2<nsIRandomGenerator>(Contracts.RandomGenerator);
		}

		/// <summary>
		/// Generate random bytes
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public static byte[] GenerateRandomBytes(int count)
		{
			IntPtr pointer;
			var ret = new byte[count];
			_randomGenerator.Instance.GenerateRandomBytes((uint)count, out pointer);
			Marshal.Copy( pointer, ret, 0, count );
			Xpcom.Free( pointer );
			return ret;
		}
	}
}