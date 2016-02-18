using System;

namespace Gecko.Interop
{
	public class ClassInfo
	{
		private ComPtr<nsIClassInfo> _classInfo;


		public ClassInfo( nsIClassInfo classInfo )
		{
			_classInfo = new ComPtr<nsIClassInfo>( classInfo );

			
        }

		public string ContractID
		{
			get { return _classInfo.Instance.GetContractIDAttribute(); }
		}
		public string ClassDescription
		{
			get { return _classInfo.Instance.GetClassDescriptionAttribute(); }
		}

		public ClassInfoFlags Flags
		{
			get
			{
				return (ClassInfoFlags) _classInfo.Instance.GetFlagsAttribute();
			}
		}
	}

	[Flags]
	public enum ClassInfoFlags
	{
		Singleton = (int) nsIClassInfoConsts.SINGLETON,
		ThreadSafe = (int) nsIClassInfoConsts.THREADSAFE,
		MainThreadOnly = (int) nsIClassInfoConsts.MAIN_THREAD_ONLY,
		DomObject = (int) nsIClassInfoConsts.DOM_OBJECT,
		PluginObject = (int) nsIClassInfoConsts.PLUGIN_OBJECT,
		ContentNode = (int) nsIClassInfoConsts.CONTENT_NODE,
		[Obsolete( "Obsolete since Gecko 2.0", true )]
		EagerClassInfo = (int) nsIClassInfoConsts.SINGLETON_CLASSINFO
	}
}