using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gecko.Interop;
using System.Runtime.InteropServices;

namespace Gecko.Plugins
{
	public static class PluginHost
	{
		private static ComPtr<nsIPluginHost> _pluginHost;

		static PluginHost()
		{
			_pluginHost = Xpcom.GetService2<nsIPluginHost>( Contracts.PluginHost );
		}

		public static void ReloadPlugins()
		{
			_pluginHost.Instance.ReloadPlugins();
		}

		public static PluginTag[] GetPluginTags()
		{
			PluginTag[] tags;
			IntPtr pluginTagsArrayPtr = IntPtr.Zero;
			try
			{
				uint aCount = 0;
				pluginTagsArrayPtr = _pluginHost.Instance.GetPluginTags(ref aCount);
				int count = (int)aCount;
				IntPtr[] tagPtrs = new IntPtr[count];
				tags = new PluginTag[count];
				Marshal.Copy(pluginTagsArrayPtr, tagPtrs, 0, tagPtrs.Length);

				for (int i = 0; i < count; i++)
				{
					object tag = null;
					try
					{
						tag = Xpcom.GetObjectForIUnknown(tagPtrs[i]);
						tags[i] = new PluginTag(Xpcom.QueryInterface<nsIPluginTag>(tag));
					}
					finally
					{
						if (tag != null)
							Marshal.ReleaseComObject(tag);
					}
				}
			}
			finally
			{
				if (pluginTagsArrayPtr != IntPtr.Zero)
					Xpcom.Free(pluginTagsArrayPtr);
			}
			return tags;
		}

		public static bool SiteHasData(PluginTag tag, string domain)
		{
			return nsString.Pass<bool, nsIPluginTag>(_pluginHost.Instance.SiteHasData, tag._pluginTag, domain);
		}

		public static void ClearSiteData(PluginTag tag, string domain, ulong flags, long maxAge)
		{
			nsString.Set(x => _pluginHost.Instance.ClearSiteData(tag._pluginTag, x, flags, maxAge), domain);
		}
	}
}
