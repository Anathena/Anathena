namespace Gecko.Windows
{
	public class JumpListItem
	{
		internal nsIJumpListItem _item;

		internal JumpListItem(nsIJumpListItem item)
		{
			_item = item;
		}

		public short Type
		{
			get { return _item.GetTypeAttribute(); }
		}


		public override bool Equals(object obj)
		{
			if (obj is JumpListItem)
			{
				return _item.Equals( ( ( JumpListItem ) obj )._item );
			}
			if (obj is nsIJumpListItem)
			{
				return _item.Equals( ( ( nsIJumpListItem ) obj ) );
			}
			return false;
		}

		public override int GetHashCode() {
			return _item.GetHashCode();
		}

		internal static JumpListItem Create(nsIJumpListItem item)
		{
			return new JumpListItem( item );
		}
	}
}