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
using System.Windows.Resources;
using SlimDX;
using SlimDX.Direct3D9;
using System.IO;
using System.Drawing;
using SlimDX.Wpf;

namespace WpfSample
{
	public class MyRenderEngine : IRenderEngine
	{
		private Sprite _sprite;
		private Texture _texture;
		private MyDataModel _dataModel;

		public MyRenderEngine(MyDataModel dataModel) :
			base()
		{
			_dataModel = dataModel;
		}

		public void OnDeviceCreated(object sender, EventArgs e)
		{
			return;
		}

		public void OnDeviceDestroyed(object sender, EventArgs e)
		{
			if(_sprite != null && !_sprite.Disposed)
			{
				_sprite.Dispose();
			}
			if(_texture != null && !_texture.Disposed)
			{
				_texture.Dispose();
				_texture = null;
			}
		}

		public void OnDeviceLost(object sender, EventArgs e)
		{
			if(_sprite != null && !_sprite.Disposed)
			{
				_sprite.Dispose();
			}
			if(_texture != null && !_texture.Disposed)
			{
				_texture.Dispose();
				_texture = null;
			}
		}

		public void OnDeviceReset(object sender, EventArgs e)
		{
			SlimDXControl control = sender as SlimDXControl;
			if(control == null)
				throw new ArgumentNullException("sender");

			if(_sprite != null)
			{
				_sprite.Dispose();
			}
			_sprite = new Sprite(control.Device);

			if(_texture == null)
				_texture = Texture.FromStream(control.Device, _dataModel.Stream, Usage.None, Pool.Default);

			return;
		}

		public void OnMainLoop(object sender, EventArgs e)
		{
			_sprite.Begin(SpriteFlags.AlphaBlend);
			_sprite.Draw(_texture, Vector3.Zero, Vector3.Zero, _dataModel.Color4);
			_sprite.End();
		}
	}
}
