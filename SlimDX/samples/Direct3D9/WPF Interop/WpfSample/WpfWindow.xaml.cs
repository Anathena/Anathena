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

using System;
using System.Windows;
using SlimDX.Wpf;

namespace WpfSample
{
	public partial class WpfWindow : Window
	{
		MyRenderEngine m_renderEngine;
		MyDataModel m_dataModel;

		public WpfWindow()
		{
			InitializeComponent();

			if(m_slimDXControl.DirectXStatus != DirectXStatus.Available)
			{
				MouseDown -= MouseDownHandler;

				if(m_slimDXControl.DirectXStatus == DirectXStatus.Unavailable_RemoteSession)
				{
					MessageBox.Show("DirectX not supported when using Remote Desktop", "Error intializing DirectX");
					System.Environment.Exit(1);
				}
				else if(m_slimDXControl.DirectXStatus == DirectXStatus.Unavailable_LowTier)
				{
					MessageBox.Show("Insufficient graphics acceleration on this machine", "Error intializing DirectX");
					System.Environment.Exit(1);
				}
				else if(m_slimDXControl.DirectXStatus == DirectXStatus.Unavailable_MissingDirectX)
				{
					MessageBox.Show("DirectX libraries are missing or need to be updated", "Error intializing DirectX");
					System.Environment.Exit(1);
				}
				else
				{
					MessageBox.Show("Unable to start DirectX (reason unknown)", "Error intializing DirectX");
					System.Environment.Exit(1);
				}
				return;
			}

			m_dataModel = new MyDataModel(m_slimDXControl, "lizard", System.Drawing.Color.Blue);
			m_renderEngine = new MyRenderEngine(m_dataModel);

			this.Loaded += new RoutedEventHandler(Window_Loaded);
			this.Closed += new EventHandler(Window_Closed);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			m_slimDXControl.SetRenderEngine(m_renderEngine);
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			m_slimDXControl.Shutdown();
		}

		private void MouseDownHandler(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if(e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
			{
				m_dataModel.Color = System.Drawing.Color.Blue;
			}
			else if(e.RightButton == System.Windows.Input.MouseButtonState.Pressed)
			{
				m_dataModel.Color = System.Drawing.Color.Red;
			}
		}
	}
}
