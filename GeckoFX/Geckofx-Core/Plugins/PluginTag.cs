namespace Gecko.Plugins
{
	public sealed class PluginTag
	{
		internal nsIPluginTag _pluginTag;

		internal PluginTag(nsIPluginTag pluginTag)
		{
			_pluginTag = pluginTag;
		}

		public bool Blocklisted
		{
			get { return _pluginTag.GetBlocklistedAttribute(); }
		}

		public string Description
		{
			get { return nsString.Get( _pluginTag.GetDescriptionAttribute ); }
		}

		public bool Disabled
		{
			get { return _pluginTag.GetDisabledAttribute(); }
		}

		public string FileName
		{
			get { return nsString.Get(_pluginTag.GetFilenameAttribute); }
		}

		public string Fullpath
		{
			get { return nsString.Get(_pluginTag.GetFullpathAttribute); }
		}

		public string Name
		{
			get { return nsString.Get(_pluginTag.GetNameAttribute); }
		}

		public string Version
		{
			get { return nsString.Get(_pluginTag.GetVersionAttribute); }
		}
	}
}