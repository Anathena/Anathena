using Gecko.Interop;

namespace Gecko.Cache
{
	public sealed class CacheDeviceInfo
	{
		private ComPtr<nsICacheDeviceInfo> _cacheDeviceInfo;

		internal CacheDeviceInfo(nsICacheDeviceInfo cacheDeviceInfo)
		{
			_cacheDeviceInfo = new ComPtr<nsICacheDeviceInfo>(cacheDeviceInfo);
		}

		public string Description
		{
			get { return _cacheDeviceInfo.Instance.GetDescriptionAttribute(); }
		}

		public uint EntryCount
		{
			get { return _cacheDeviceInfo.Instance.GetEntryCountAttribute(); }
		}

		public uint MaximumSize
		{
			get { return _cacheDeviceInfo.Instance.GetMaximumSizeAttribute(); }
		}

		public uint TotalSize
		{
			get { return _cacheDeviceInfo.Instance.GetTotalSizeAttribute(); }
		}

		public string UsageReport
		{
			get { return _cacheDeviceInfo.Instance.GetUsageReportAttribute(); }
		}
	}
}