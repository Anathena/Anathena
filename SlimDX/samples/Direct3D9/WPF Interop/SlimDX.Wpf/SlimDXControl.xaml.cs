// Copyright (c) 2010, LizardTech, a Celartem company
// All rights reserved.
//                                                              
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
// 
//     * Redistributions of source code must retain the above copyright notice,
//       this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of LizardTech nor the names of its contributors may be
//       used to endorse or promote products derived from this software without
//       specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
// ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
// SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
// ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.

// uncomment this to force your Vista or Win7 box to act more like an XP box...
//#define USE_XP_MODE

// uncomment this to render each and every frame, as opposed to only when the user signals an update is needed
//#define USE_ALWAYSRENDER_MODE

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using SlimDX;
using SlimDX.Direct3D9;

[assembly: CLSCompliant(true)]
namespace SlimDX.Wpf
{
	public enum DirectXStatus
	{
		Available,
		Unavailable_RemoteSession,
		Unavailable_LowTier,
		Unavailable_MissingDirectX,
		Unavailable_Unknown
	};

	public partial class SlimDXControl : ContentControl
	{
		public SlimDXControl()
		{
			InitializeComponent();
		}

		public void SetRenderEngine(IRenderEngine renderEngine)
		{
			RenderEngine = renderEngine;

			DeviceCreated += new EventHandler(RenderEngine.OnDeviceCreated);
			DeviceDestroyed += new EventHandler(RenderEngine.OnDeviceDestroyed);
			DeviceLost += new EventHandler(RenderEngine.OnDeviceLost);
			DeviceReset += new EventHandler(RenderEngine.OnDeviceReset);
			MainLoop += new EventHandler(RenderEngine.OnMainLoop);

			return;
		}

		#region locals
		// D3D settings
		private Direct3D _direct3D;
		private Direct3DEx _direct3DEx;
		private Device _device;
		private DeviceEx _deviceEx;
		private Surface _backBufferSurface;

		// device settings
		Format _adapterFormat = Format.X8R8G8B8;
		Format _backbufferFormat = Format.A8R8G8B8;
		Format _depthStencilFormat = Format.D16;
		CreateFlags _createFlags = CreateFlags.Multithreaded | CreateFlags.FpuPreserve;
		private PresentParameters _presentParameters;

		private bool _sizeChanged;
		#endregion

		#region Properties
		public IRenderEngine RenderEngine { private set; get; }

		public Image Image
		{
			get { return this._image; }
		}

		public bool UseDeviceEx
		{
			get;
			private set;
		}

		public Direct3D Direct3D
		{
			get
			{
				if(UseDeviceEx)
					return _direct3DEx;
				else
					return _direct3D;
			}
		}

		public Device Device
		{
			get
			{
				if(UseDeviceEx)
					return _deviceEx;
				else
					return _device;
			}
		}

		public DirectXStatus DirectXStatus
		{
			get;
			private set;
		}
		#endregion

		#region Events
		public event EventHandler MainLoop;
		public event EventHandler DeviceCreated;
		public event EventHandler DeviceDestroyed;
		public event EventHandler DeviceLost;
		public event EventHandler DeviceReset;
		#endregion

		#region Event raisers
		protected virtual void OnInitialize()
		{
			return;
		}

		protected virtual void OnMainLoop(EventArgs e)
		{
			if(MainLoop != null)
				MainLoop(this, e);
			return;
		}

		protected virtual void OnDeviceCreated(EventArgs e)
		{
			if(DeviceCreated != null)
				DeviceCreated(this, e);
			ForceRendering();
		}

		protected virtual void OnDeviceDestroyed(EventArgs e)
		{
			if(DeviceDestroyed != null)
				DeviceDestroyed(this, e);
		}

		protected virtual void OnDeviceLost(EventArgs e)
		{
			if(DeviceLost != null)
				DeviceLost(this, e);
		}

		protected virtual void OnDeviceReset(EventArgs e)
		{
			if(DeviceReset != null)
				DeviceReset(this, e);
		}
		#endregion

		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);
			InitializeDirect3D();
			InitializeDevice();

			if(DirectXStatus != DirectXStatus.Available)
			{
				Shutdown();
			}
		}

		protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
		{
			base.OnRenderSizeChanged(sizeInfo);

			if(sizeInfo.HeightChanged || sizeInfo.WidthChanged)
				_sizeChanged = true;

		}

		/// <summary>
		/// Initializes the Direct3D objects and sets the Available flag
		/// </summary>
		private void InitializeDirect3D()
		{
			DirectXStatus = DirectXStatus.Unavailable_Unknown;

			ReleaseDevice();
			ReleaseDirect3D();

			// assume that we can't run at all under terminal services
			if(GetSystemMetrics(SM_REMOTESESSION) != 0)
			{
				DirectXStatus = DirectXStatus.Unavailable_RemoteSession;
				return;
			}

			int renderingTier = (RenderCapability.Tier >> 16);
			if(renderingTier < 2)
			{
				DirectXStatus = DirectXStatus.Unavailable_LowTier;
				return;
			}

#if USE_XP_MODE
         _direct3D = new Direct3D();
         UseDeviceEx = false;
#else
			try
			{
				_direct3DEx = new Direct3DEx();
				UseDeviceEx = true;
			}
			catch
			{
				try
				{
					_direct3D = new Direct3D();
					UseDeviceEx = false;
				}
				catch(Direct3DX9NotFoundException)
				{
					DirectXStatus = DirectXStatus.Unavailable_MissingDirectX;
					return;
				}
				catch
				{
					DirectXStatus = DirectXStatus.Unavailable_Unknown;
					return;
				}
			}
#endif

			bool ok;
			Result result;

			ok = Direct3D.CheckDeviceType(0, DeviceType.Hardware, _adapterFormat, _backbufferFormat, true, out result);
			if(!ok)
			{
				//const int D3DERR_NOTAVAILABLE = -2005530518;
				//if (result.Code == D3DERR_NOTAVAILABLE)
				//{
				//   ReleaseDirect3D();
				//   Available = Status.Unavailable_NotReady;
				//   return;
				//}
				ReleaseDirect3D();
				return;
			}

			ok = Direct3D.CheckDepthStencilMatch(0, DeviceType.Hardware, _adapterFormat, _backbufferFormat, _depthStencilFormat, out result);
			if(!ok)
			{
				ReleaseDirect3D();
				return;
			}

			Capabilities deviceCaps = Direct3D.GetDeviceCaps(0, DeviceType.Hardware);
			if((deviceCaps.DeviceCaps & DeviceCaps.HWTransformAndLight) != 0)
				_createFlags |= CreateFlags.HardwareVertexProcessing;
			else
				_createFlags |= CreateFlags.SoftwareVertexProcessing;

			DirectXStatus = DirectXStatus.Available;

			return;
		}

		/// <summary>
		/// Initializes the Device and starts up the d3dimage object
		/// </summary>
		private void InitializeDevice()
		{
			if(DirectXStatus != DirectXStatus.Available)
				return;

			Debug.Assert(Direct3D != null);

			ReleaseDevice();

			HwndSource hwnd = new HwndSource(0, 0, 0, 0, 0, _d3dimage.PixelWidth, _d3dimage.PixelHeight, "SlimDXControl", IntPtr.Zero);

			_presentParameters = new PresentParameters();
			_presentParameters.SwapEffect = SwapEffect.Copy;
			_presentParameters.DeviceWindowHandle = hwnd.Handle;
			_presentParameters.Windowed = true;
			_presentParameters.BackBufferWidth = ((int) ActualWidth <= 0) ? 1 : (int) ActualWidth;
			_presentParameters.BackBufferHeight = ((int) ActualHeight <= 0) ? 1 : (int) ActualHeight;
			_presentParameters.BackBufferFormat = _backbufferFormat;
			_presentParameters.AutoDepthStencilFormat = _depthStencilFormat;

			try
			{
				if(UseDeviceEx)
				{
					_deviceEx = new DeviceEx((Direct3DEx) Direct3D, 0,
					   DeviceType.Hardware,
					   hwnd.Handle,
					   _createFlags,
					   _presentParameters);
				}
				else
				{
					_device = new Device(Direct3D, 0,
					   DeviceType.Hardware,
					   hwnd.Handle,
					   _createFlags,
					   _presentParameters);
				}
			}
			catch(Direct3D9Exception)
			{
				DirectXStatus = DirectXStatus.Unavailable_Unknown;
				return;
			}
			// call the user's ones
			OnDeviceCreated(EventArgs.Empty);
			OnDeviceReset(EventArgs.Empty);

			{
				// is it the case that someone else is nulling these out on us?  seems so
				// this means we need to be careful not to let multiple copies of the delegate get onto the list
				// not sure what else we can do here...
				CompositionTarget.Rendering -= OnRendering;
				_d3dimage.IsFrontBufferAvailableChanged -= OnIsFrontBufferAvailableChanged;
				CompositionTarget.Rendering += OnRendering;
				_d3dimage.IsFrontBufferAvailableChanged += OnIsFrontBufferAvailableChanged;
			}

			_d3dimage.Lock();
			_backBufferSurface = Device.GetBackBuffer(0, 0);
			_d3dimage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, _backBufferSurface.ComPointer);
			_d3dimage.Unlock();

			return;
		}

		#region Shutdown and Release
		public void Shutdown()
		{
			ReleaseDevice();
			ReleaseDirect3D();
		}

		private void ReleaseDevice()
		{
			if(_device != null)
			{
				if(!_device.Disposed)
				{
					_device.Dispose();
					_device = null;
					OnDeviceDestroyed(EventArgs.Empty);
				}
			}

			if(_deviceEx != null)
			{
				if(!_deviceEx.Disposed)
				{
					_deviceEx.Dispose();
					_deviceEx = null;
					OnDeviceDestroyed(EventArgs.Empty);
				}
			}

			_d3dimage.Lock();
			_d3dimage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, IntPtr.Zero);
			_d3dimage.Unlock();

			ReleaseBackBuffer();
		}

		private void ReleaseDirect3D()
		{
			if(_direct3D != null && !_direct3D.Disposed)
			{
				_direct3D.Dispose();
				_direct3D = null;
			}

			if(_direct3DEx != null && !_direct3DEx.Disposed)
			{
				_direct3DEx.Dispose();
				_direct3DEx = null;
			}
		}
		#endregion

		#region Support for controlling on-demand vs. per-frame rendering
		// set this to true when something has changed, to signal that the rendering loop should not be skipped
		private bool _needsRendering;
		public void ForceRendering()
		{
			lock(this)
			{
				_needsRendering = true;
			}
		}

		private bool ResetForceRendering()
		{
			bool ret;
			lock(this)
			{
				ret = _needsRendering;
				_needsRendering = false;
			}
#if USE_ALWAYSRENDER_MODE
         return true;
#else
			return ret;
#endif
		}
		#endregion

		private void OnRendering(object sender, EventArgs e)
		{
			Debug.Assert(_d3dimage.IsFrontBufferAvailable);

			if(DirectXStatus != DirectXStatus.Available)
				return;

			bool needToReset = false;

			try
			{
				if(Direct3D == null)
				{
					InitializeDirect3D();
				}
				if(Device == null)
				{
					InitializeDevice();
					ForceRendering();
				}
				if(Device == null)
				{
					// device might still be not available, so we'll just have to try again next time around
					return;
				}

				if(_sizeChanged)
				{
					_presentParameters.BackBufferWidth = (int) ActualWidth;
					_presentParameters.BackBufferHeight = (int) ActualHeight;
					ReleaseBackBuffer();
					OnDeviceLost(EventArgs.Empty);
					Device.Reset(_presentParameters);
					OnDeviceReset(EventArgs.Empty);
					_sizeChanged = false;
					ForceRendering();
				}

				bool needsRendering = ResetForceRendering();
				if(needsRendering)
				{
					_d3dimage.Lock();
					if(_d3dimage.IsFrontBufferAvailable)
					{
						Result result = Device.TestCooperativeLevel();
						if(result.IsFailure)
						{
							// we'll change the status in the needToReset block below
							DirectXStatus = DirectXStatus.Unavailable_Unknown;
							throw new Direct3D9Exception("Device.TestCooperativeLevel() failed");
						}

						Device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, new Color4(System.Drawing.Color.Black), 1, 0);
						Device.BeginScene();

						try
						{
							// call the user's method
							OnMainLoop(EventArgs.Empty);
						}
						catch(Direct3D9Exception d3dex)
						{
							if(d3dex.Message.StartsWith("D3DERR_OUTOFVIDEOMEMORY"))
							{
								needToReset = true;
							}
							else
							{
								throw;
							}
						}
						catch(Exception)
						{
							//MessageBox.Show(ex.Message, "Caught Exception in SlimDXControl.OnRendering() [d] " + ex.ToString());
							throw;
						}

						Device.EndScene();
						Device.Present();

					}
					_backBufferSurface = Device.GetBackBuffer(0, 0);
					_d3dimage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, _backBufferSurface.ComPointer);
					_d3dimage.AddDirtyRect(new Int32Rect(0, 0, _d3dimage.PixelWidth, _d3dimage.PixelHeight));
					_d3dimage.Unlock();
				}
			}
			catch(Direct3D9Exception)
			{
				needToReset = true;
			}
			catch(Exception)
			{
				//MessageBox.Show(ex.Message, "Caught Exception in SlimDXControl.OnRendering() [a] " + ex.ToString());
				throw;
			}

			if(needToReset)
			{
				try
				{
					InitializeDirect3D();
					InitializeDevice();
					if(DirectXStatus != DirectXStatus.Available)
					{
						// we were once available (because we were able to enter the OnRender function), but now we're not available
						// This could be due to a return from Ctrl-Alt-Del under XP and things just aren't ready yet.
						// Keep everything nulled out and we'll just try the next time around.
						ReleaseDevice();
						ReleaseDirect3D();
						DirectXStatus = DirectXStatus.Available;
					}
					else
					{
						// we're available now, that's good
						// force a rendering next time around
						ForceRendering();
					}
				}
				catch(Direct3D9Exception)
				{
					//MessageBox.Show(ex.Message, "Caught Exception in SlimDXControl.OnRendering() [b] " + ex.ToString());
					throw;
				}
				catch(Exception)
				{
					//MessageBox.Show(ex.Message, "Caught Exception in SlimDXControl.OnRendering() [c] " + ex.ToString());
					throw;
				}
			}

			return;
		}

		#region front & back buffer management
		private void ReleaseBackBuffer()
		{
			if(_backBufferSurface != null && !_backBufferSurface.Disposed)
			{
				_backBufferSurface.Dispose();
				_backBufferSurface = null;

				_d3dimage.Lock();
				_d3dimage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, IntPtr.Zero);
				_d3dimage.Unlock();
			}
		}

		private void OnIsFrontBufferAvailableChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			try
			{
				if(_d3dimage.IsFrontBufferAvailable)
				{
					InitializeDevice();
				}
				else
				{
					CompositionTarget.Rendering -= OnRendering;
				}
			}
			catch(Exception)
			{
				//MessageBox.Show(ex.Message, "Caught Exception in SlimDXControl.OnIsFrontBufferAvailableChanged()");
				throw;
			}
		}
		#endregion

		#region DLL imports
		// can't figure out how to access remote session status through .NET
		[System.Runtime.InteropServices.DllImport("user32")]
		private static extern int GetSystemMetrics(int smIndex);
		private const int SM_REMOTESESSION = 0x1000;
		#endregion
	}
}
