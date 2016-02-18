using System.Runtime.InteropServices;

namespace Gecko
{
	/// <summary>
	/// The IUnknown/nsISupports ptr for this interface can be retrieved by calling JS_GetContextPrivate on a JSContext*.
	/// </summary>
	[ComImport]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("274840b6-7349-4798-be24-bd75a64699b7")]
	public interface nsIScriptContext
	{
		// Methods on this interface aren't directly callable via (XP)COM, due to
		// them having the wrong calling convention. It may be possible to invoke the
		// methods by via the vtable.
	}
}