namespace Gecko
{
	public class DomHashChangeEventArgs
		: DomEventArgs
	{
		private nsIDOMHashChangeEvent _hashChangeEvent;


		private DomHashChangeEventArgs( nsIDOMHashChangeEvent ev )
			: base( ev )
		{
			_hashChangeEvent = ev;
		}

		public static DomHashChangeEventArgs Create( nsIDOMHashChangeEvent ev )
		{
			return new DomHashChangeEventArgs( ev );
		}

		public string OldUrl
		{
			get { return nsString.Get( _hashChangeEvent.GetOldURLAttribute ); }
		}

		public string NewUrl
		{
			get { return nsString.Get( _hashChangeEvent.GetNewURLAttribute ); }
		}

		public void InitHashChangeEvent(string type,bool canBubble,bool cancelable,
		                                string oldUrl,string newUrl)
		{
			using (nsAString nType=new nsAString(type),nOld=new nsAString(oldUrl),nNew=new nsAString(newUrl))
			{
				_hashChangeEvent.InitHashChangeEvent( nType, canBubble, cancelable, nOld, nNew );
			}
		}
	}
}