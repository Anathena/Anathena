// place GTK stuff here :)
namespace Gecko
{
#if GTK
	partial class GeckoWebBrowser
	{

		// Only used on Linux.
		protected GtkDotNet.GtkWrapperNoThread m_wrapper;

	}
#endif
}
