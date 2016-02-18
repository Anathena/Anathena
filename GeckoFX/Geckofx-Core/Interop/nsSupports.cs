namespace Gecko.Interop
{
	public class nsSupports
	{
		internal nsISupports _nsISupports;

		public nsSupports(nsISupports nsISupports)
		{
			_nsISupports = nsISupports;
		}

		public T QueryInterface<T>()
		{
			return _nsISupports.GetManagedWrapper<T>();
		}
	}
}