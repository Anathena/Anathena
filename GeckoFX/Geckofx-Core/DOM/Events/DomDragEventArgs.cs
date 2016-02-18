namespace Gecko
{
	/// <summary>
	/// Provides data about a drag event
	/// </summary>
	public class DomDragEventArgs
		: DomMouseEventArgs
	{
		private nsIDOMDragEvent _domDragEvent;

		private DomDragEventArgs(nsIDOMDragEvent ev)
			: base(ev)
		{
			_domDragEvent = ev;
		}

		public static DomDragEventArgs Create(nsIDOMDragEvent ev)
		{
			return new DomDragEventArgs( ev );
		}

		

		public nsIDOMDataTransfer GetDataTransfer()
		{
			return _domDragEvent.GetDataTransferAttribute();
		}
	}
}