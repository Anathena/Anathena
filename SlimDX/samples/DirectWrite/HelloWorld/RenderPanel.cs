using System;
using System.Windows.Forms;
using SlimDX.SampleFramework;

namespace HelloWorld
{
	public partial class RenderPanel : UserControl
	{
		private DeviceContext2D apiContext;
		public DeviceContext2D Context2D { get; private set; }

		public RenderPanel()
		{
			InitializeComponent();
		}

		public void InitializeDevice()
		{
			DeviceSettings2D settings = new DeviceSettings2D
        		{
	        		Width = ClientSize.Width,
					Height = ClientSize.Height
        		};
			var result = new DeviceContext2D(Handle, settings);
			apiContext = result;
			Context2D = result;
		}

	}
}
