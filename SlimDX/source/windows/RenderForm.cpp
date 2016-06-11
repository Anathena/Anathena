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
#include "stdafx.h"

#include "../InternalHelpers.h"
#include "../Resources.h"

#include "RenderForm.h"

using namespace System;
using namespace System::Drawing;
using namespace System::ComponentModel;
using namespace System::Windows::Forms;

namespace SlimDX
{
namespace Windows
{
	RenderForm::RenderForm()
	{
		Construct( "SlimDX" );
	}

	RenderForm::RenderForm( System::String^ text )
	{
		Construct( text );
	}

	void RenderForm::Construct( System::String^ text )
	{
		Text = text;
		ClientSize = System::Drawing::Size( 800, 600 );
		MinimumSize = System::Drawing::Size( 200, 200 );

		ResizeRedraw = true;
		SetStyle( ControlStyles::AllPaintingInWmPaint | ControlStyles::UserPaint, true );

		Icon = SlimDX::Resources::BlackIcon;
	}

	void RenderForm::OnResizeBegin( EventArgs^ e )
	{
		Form::OnResizeBegin(e);

		sizeMove = true;
		cachedSize = Size;
		OnPauseRendering(e);
	}

	void RenderForm::OnResizeEnd( EventArgs^ e )
	{
		Form::OnResizeEnd(e);

		OnUserResized(e);
		UpdateScreen();

		sizeMove = false;
		OnResumeRendering(e);
	}

	void RenderForm::OnLoad( EventArgs^ e )
	{
		Form::OnLoad(e);
		cachedSize = Size;
		UpdateScreen();
	}

	void RenderForm::OnPaintBackground( PaintEventArgs^ e )
	{
		SLIMDX_UNREFERENCED_PARAMETER( e );
	}

	void RenderForm::OnPauseRendering(EventArgs^ e)
	{
		if (&RenderForm::PauseRendering != nullptr)
			PauseRendering(this, e);
	}

	void RenderForm::OnResumeRendering(EventArgs^ e)
	{
		if (&RenderForm::ResumeRendering != nullptr)
			ResumeRendering(this, e);
	}

	void RenderForm::OnUserResized(EventArgs^ e)
	{
		if (&RenderForm::UserResized != nullptr)
			UserResized(this, e);
	}

	void RenderForm::OnMonitorChanged(EventArgs^ e)
	{
		if (&RenderForm::MonitorChanged != nullptr)
			MonitorChanged(this, e);
	}

	void RenderForm::OnAppActivated(EventArgs^ e)
	{
		if (&RenderForm::AppActivated != nullptr)
			AppActivated(this, e);
	}

	void RenderForm::OnAppDeactivated(EventArgs^ e)
	{
		if (&RenderForm::AppDeactivated != nullptr)
			AppDeactivated(this, e);
	}

	void RenderForm::OnSystemSuspend(EventArgs^ e)
	{
		if (&RenderForm::SystemSuspend != nullptr)
			SystemSuspend(this, e);
	}

	void RenderForm::OnSystemResume(EventArgs^ e)
	{
		if (&RenderForm::SystemResume != nullptr)
			SystemResume(this, e);
	}

	void RenderForm::OnScreensaver(CancelEventArgs^ e)
	{
		if (&RenderForm::Screensaver != nullptr)
			Screensaver(this, e);
	}

	void RenderForm::WndProc( Message% m )
	{
		Int64 wparam = m.WParam.ToInt64();

		if (m.Msg == WM_SIZE)
		{
			if (wparam == SIZE_MINIMIZED)
			{
				minimized = true;
				maximized = false;
				OnPauseRendering(EventArgs::Empty);
			}
			else
			{
				RECT rect;
				GetClientRect(reinterpret_cast<HWND>(m.HWnd.ToPointer()), &rect);
				if (rect.bottom - rect.top == 0)
				{
					// Rapidly clicking the task bar to minimize and restore a window
                    // can cause a WM_SIZE message with SIZE_RESTORED when 
                    // the window has actually become minimized due to rapid change
                    // so just ignore this message
				}
				else if (wparam == SIZE_MAXIMIZED)
				{
					if (minimized)
						OnResumeRendering(EventArgs::Empty);

					minimized = false;
					maximized = true;

					OnUserResized(EventArgs::Empty);
					UpdateScreen();
				}
				else if (wparam == SIZE_RESTORED)
				{
					if (minimized)
						OnResumeRendering(EventArgs::Empty);

					minimized = false;

					if (!sizeMove && (Size != cachedSize || maximized))
					{
						maximized = false;

						OnUserResized(EventArgs::Empty);
						UpdateScreen();
						cachedSize = Size;
					}

					maximized = false;
				}
			}
		}
		else if (m.Msg == WM_ACTIVATEAPP)
		{
			if (wparam != 0)
				OnAppActivated(EventArgs::Empty);
			else
				OnAppDeactivated(EventArgs::Empty);
		}
		else if (m.Msg == WM_POWERBROADCAST)
		{
			if (wparam == PBT_APMQUERYSUSPEND)
			{
				OnSystemSuspend(EventArgs::Empty);
				m.Result = IntPtr(1);
				return;
			}
			else if (wparam == PBT_APMRESUMESUSPEND)
			{
				OnSystemResume(EventArgs::Empty);
				m.Result = IntPtr(1);
				return;
			}
		}
		else if (m.Msg == WM_MENUCHAR)
		{
			// A menu is active and the user presses a key that does not correspond to any mnemonic or accelerator key
            // So just ignore and don't beep
			m.Result = IntPtr(MAKELRESULT(0, MNC_CLOSE));
			return;
		}
		else if (m.Msg == WM_SYSCOMMAND)
		{
			wparam &= 0xFFF0;
			if (wparam == SC_MONITORPOWER || wparam == SC_SCREENSAVE)
			{
				CancelEventArgs^ e = gcnew CancelEventArgs();
				OnScreensaver(e);
				if (e->Cancel)
				{
					m.Result = IntPtr::Zero;
					return;
				}
			}
		}

		Form::WndProc(m);
	}

	void RenderForm::UpdateScreen()
	{
		DisplayMonitor^ current = DisplayMonitor::FromWindow(Handle);
		if (monitor != nullptr && monitor->DeviceName != current->DeviceName)
		{
			monitor = current;
			OnMonitorChanged(EventArgs::Empty);
		}

		monitor = current;
	}
}
}