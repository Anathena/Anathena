namespace Gecko.Windows
{
	public sealed class JumpListShortcut
		: JumpListItem
	{
		private nsIJumpListShortcut _shortcut;

		internal JumpListShortcut(nsIJumpListShortcut item)
			:base(item)
		{
			_shortcut = item;
		}
	}
}