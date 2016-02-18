namespace Gecko
{
	public class DomUIEventArgs
		: DomEventArgs
	{
		nsIDOMUIEvent _Event;

		internal DomUIEventArgs(nsIDOMUIEvent ev)
			: base(ev)
		{
			_Event = ev;
		}
		
		public static DomUIEventArgs Create(nsIDOMUIEvent ev)
		{
			if (ev is nsIDOMMouseEvent)
			{
				return DomMouseEventArgs.Create( (nsIDOMMouseEvent)ev );
			}
			return new DomUIEventArgs( ev );
		}
		
		
		public int Detail
		{
			get { return _Event.GetDetailAttribute(); }
		}
	};
}