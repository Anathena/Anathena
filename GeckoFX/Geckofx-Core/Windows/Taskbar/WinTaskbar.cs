using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Interop;

namespace Gecko.Windows
{
	/// <summary>
	/// Class for controling Windows 7 taskbar behavior
	/// ONLY Windows 7+
	/// Check Available property if platform is supported
	/// </summary>
	public sealed class WinTaskbar
	{
		private readonly ComPtr<nsIWinTaskbar> _winTaskbar;

		public WinTaskbar()
		{
			_winTaskbar = Xpcom.CreateInstance2<nsIWinTaskbar>( Contracts.WindowsTaskbar );
		}

		public bool Available
		{
			get { return _winTaskbar.Instance.GetAvailableAttribute(); }
		}

		public string DefaultGroupId
		{
			get { return nsString.Get(_winTaskbar.Instance.GetDefaultGroupIdAttribute); }
		}

		public JumpListBuilder CreateJumpListBuilder()
		{
			return new JumpListBuilder(_winTaskbar.Instance.CreateJumpListBuilder());
		}

		//_winTaskbar.CreateTaskbarTabPreview(  )

		//_winTaskbar.GetTaskbarProgress(  )

		//_winTaskbar.GetTaskbarWindowPreview(  )

		//_winTaskbar.PrepareFullScreen(  );

		public void PrepareFullScreenHWND( IntPtr aWindow, bool aFullScreen )
		{
			_winTaskbar.Instance.PrepareFullScreenHWND(aWindow, aFullScreen);
		}

		public void SetGroupIdForWindow(GeckoWindow aParent, string aIdentifier)
		{
			nsString.Set(x => _winTaskbar.Instance.SetGroupIdForWindow(aParent.DomWindow, x), aIdentifier);
		}

	}
}
