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
#pragma once

#include "DisplayMonitor.h"

namespace SlimDX
{
	namespace Windows
	{
		public ref class RenderForm : System::Windows::Forms::Form
		{
		private:
			void Construct( System::String^ text );
			void UpdateScreen();

			DisplayMonitor^ monitor;
			System::Drawing::Size cachedSize;
			bool sizeMove;
			bool minimized;
			bool maximized;

		protected:
			virtual void OnUserResized( System::EventArgs^ e );
			virtual void OnMonitorChanged( System::EventArgs^ e );
			virtual void OnAppActivated( System::EventArgs^ e );
			virtual void OnAppDeactivated( System::EventArgs^ e );
			virtual void OnPauseRendering( System::EventArgs^ e );
			virtual void OnResumeRendering( System::EventArgs^ e );
			virtual void OnSystemSuspend( System::EventArgs^ e );
			virtual void OnSystemResume( System::EventArgs^ e );
			virtual void OnScreensaver( System::ComponentModel::CancelEventArgs^ e );

			virtual void OnResizeBegin( System::EventArgs^ e ) override;
			virtual void OnResizeEnd( System::EventArgs^ e ) override;
			virtual void OnLoad( System::EventArgs^ e ) override;
			virtual void OnPaintBackground( System::Windows::Forms::PaintEventArgs^ e ) override;

			virtual void WndProc( System::Windows::Forms::Message% m ) override;

		public:
			RenderForm();
			RenderForm( System::String^ text );

			event System::EventHandler^ PauseRendering;
			event System::EventHandler^ ResumeRendering;
			event System::EventHandler^ UserResized;
			event System::EventHandler^ MonitorChanged;
			event System::EventHandler^ AppActivated;
			event System::EventHandler^ AppDeactivated;
			event System::EventHandler^ SystemSuspend;
			event System::EventHandler^ SystemResume;
			event System::ComponentModel::CancelEventHandler^ Screensaver;

			property DisplayMonitor^ Monitor
			{
				DisplayMonitor^ get() { return monitor; }
			}
		};
	}
}