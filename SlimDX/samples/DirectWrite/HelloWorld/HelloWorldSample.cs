/*
* Copyright (c) 2007-2012 SlimDX Group
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using SlimDX;
using SlimDX.Direct2D;
using SlimDX.DirectWrite;
using SlimDX.SampleFramework;
using FontStyle=SlimDX.DirectWrite.FontStyle;

namespace HelloWorld
{
	/// <summary>
	/// Demonstrates how to render a simple colored triangle with Direct2D.
	/// </summary>
	class HelloWorldSample : Sample
	{
		private MainForm _mainForm;
		private SolidColorBrush _simpleTextBrush;

		/// <summary>
		/// Disposes of object resources.
		/// </summary>
		/// <param name="disposeManagedResources">If true, managed resources should be
		/// disposed of in addition to unmanaged resources.</param>
		protected override void Dispose(bool disposeManagedResources)
		{
			if (disposeManagedResources)
			{
				_simpleTextBrush.Dispose();
				_multiformattedLayout.Dispose();
				_multiformattedTextBrush.Dispose();
			}

			base.Dispose(disposeManagedResources);
		}

		protected override Form CreateForm(SampleConfiguration configuration)
		{
			_mainForm = new MainForm();
			return _mainForm;
		}

		private SlimDX.DirectWrite.Factory _writeFactory;
		private TextFormat _textFormat;
		float _dpiY;
		float _dpiX;
		private SolidColorBrush _multiformattedTextBrush;
		private TextLayout _multiformattedLayout;

		private WindowRenderTarget SimpleTextRenderTarget
		{
			get { return _mainForm.SimpleTextRenderPanel.Context2D.RenderTarget; }
		}

		/// <summary>
		/// In a derived class, implements logic to initialize the sample.
		/// </summary>
		protected override void OnInitialize()
		{
			DeviceSettings2D settings = new DeviceSettings2D
			{
				Width = WindowWidth,
				Height = WindowHeight
			};

			InitializeDevice(settings);
			_writeFactory = new SlimDX.DirectWrite.Factory(SlimDX.DirectWrite.FactoryType.Shared);
			_textFormat = _writeFactory.CreateTextFormat("Gabriola", FontWeight.Regular, FontStyle.Normal, FontStretch.Normal, 72.0f, "en-us");
			_textFormat.TextAlignment = TextAlignment.Center;
			_textFormat.ParagraphAlignment = ParagraphAlignment.Center;
			using (Graphics graphics = Graphics.FromHwnd(_mainForm.Handle))
			{
				_dpiY = graphics.DpiY/96.0f;
				_dpiX = graphics.DpiX/96.0f;
			}

			InitializeSimpleText();
			InitializeMultiformattedText();
		}

		private void InitializeMultiformattedText()
		{
			_mainForm.MultiformattedTextRenderPanel.InitializeDevice();
			_multiformattedTextBrush = new SolidColorBrush(MultiformattedRenderTarget, new Color4(0.0f, 0.0f, 0.0f));
			string text = "Hello World using   DirectWrite!";
			_multiformattedLayout = _writeFactory.CreateTextLayout(text, _textFormat, 640.0f, 480.0f);
			_multiformattedLayout.SetFontSize(100.0f, new TextRange(20, 6));
			_multiformattedLayout.SetUnderline(true, new TextRange(20, 11));
			_multiformattedLayout.SetFontWeight(FontWeight.Bold, new TextRange(20, 11));
			using (Typography typography = _writeFactory.CreateTypography())
			{
				typography.AddFeature(new FontFeature(FontFeatureTag.StylisticSet7, 1));
				_multiformattedLayout.SetTypography(typography, new TextRange(0, text.Length));
			}
		}

		protected WindowRenderTarget MultiformattedRenderTarget
		{
			get { return _mainForm.MultiformattedTextRenderPanel.Context2D.RenderTarget; }
		}

		private void InitializeSimpleText()
		{
			_mainForm.SimpleTextRenderPanel.InitializeDevice();
			_simpleTextBrush = new SolidColorBrush(SimpleTextRenderTarget, new Color4(0.0f, 0.0f, 0.0f));
		}

		private int SelectedTab
		{
			get { return _mainForm.TabControl.SelectedIndex; }
		}
		/// <summary>
		/// In a derived class, implements logic that should occur before all
		/// other rendering.
		/// </summary>
		protected override void OnRenderBegin()
		{
			switch (SelectedTab)
			{
			case 0:
				RenderBegin(SimpleTextRenderTarget);
				break;

			case 1:
				RenderBegin(MultiformattedRenderTarget);
				break;

			default:
				break;
			}
		}

		private static void RenderBegin(RenderTarget renderTarget)
		{
			renderTarget.BeginDraw();
			renderTarget.Transform = Matrix3x2.Identity;
			renderTarget.Clear(new Color4(1.0f, 1.0f, 1.0f));
		}

		/// <summary>
		/// In a derived class, implements logic to render the sample.
		/// </summary>
		protected override void OnRender()
		{
			switch (SelectedTab)
			{
			case 0:
				SimpleTextRender();
				break;

			case 1:
				MultiformattedTextRender();
				break;
			}
		}

		private void MultiformattedTextRender()
		{
			RectangleF layoutRect = _mainForm.MultiformattedTextRenderPanel.ClientRectangle;
			MultiformattedRenderTarget.DrawTextLayout(new PointF(layoutRect.Left/_dpiX, layoutRect.Top/_dpiY),
				_multiformattedLayout, _multiformattedTextBrush);
		}

		private void SimpleTextRender()
		{
			RectangleF layoutRect = _mainForm.SimpleTextRenderPanel.ClientRectangle;
			layoutRect = new RectangleF(layoutRect.Left/_dpiX, layoutRect.Top/_dpiY, layoutRect.Width/_dpiX, layoutRect.Height/_dpiY);
			SimpleTextRenderTarget.DrawText("Hello World using  DirectWrite!", _textFormat, layoutRect, _simpleTextBrush);
		}

		/// <summary>
		/// In a derived class, implements logic that should occur after all
		/// other rendering.
		/// </summary>
		protected override void OnRenderEnd()
		{
			switch (SelectedTab)
			{
			case 0:
				SimpleTextRenderTarget.EndDraw();
				break;

			case 1:
				MultiformattedRenderTarget.EndDraw();
				break;
			}
		}
	}
}
