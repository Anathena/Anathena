namespace Gecko.Windows
{
	public sealed class JumpListSeparator
		:JumpListItem
	{
		private nsIJumpListSeparator _separator;
		internal JumpListSeparator(nsIJumpListSeparator item)
			:base(item)
		{
			_separator = item;
		}
	}
}