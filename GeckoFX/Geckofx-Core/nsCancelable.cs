namespace Gecko
{
    public class nsCancelable
	{
		private nsICancelable _cancelable;

		internal nsCancelable(nsICancelable cancelable)
		{
			_cancelable = cancelable;
		}

		public void Cancel(int reason)
		{
			_cancelable.Cancel( reason );
		}
	}
}
