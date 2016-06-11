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
#include "../CollectionShim.h"

#include "DisplayMonitor.h"

using namespace System;
using namespace System::Drawing;
using namespace System::Collections::Generic;
using namespace msclr::interop;

namespace SlimDX
{
namespace Windows
{
	DisplayMonitor::DisplayMonitor(IntPtr handle)
	{
		if (handle == IntPtr::Zero)
			throw gcnew ArgumentNullException("handle");

		this->handle = reinterpret_cast<HMONITOR>(handle.ToPointer());

		MONITORINFOEX info;
		info.cbSize = sizeof(MONITORINFOEX);
		if (!GetMonitorInfo(this->handle, &info))
			return;

		bounds = marshal_as<System::Drawing::Rectangle>(info.rcMonitor);
		workingArea = marshal_as<System::Drawing::Rectangle>(info.rcWork);
		isPrimary = info.dwFlags == MONITORINFOF_PRIMARY;
		deviceName = gcnew String(info.szDevice);

		HDC hdc = CreateDC(NULL, info.szDevice, NULL, NULL);
		if (!hdc)
			return;

		bitDepth = GetDeviceCaps(hdc, BITSPIXEL);
		bitDepth *= GetDeviceCaps(hdc, PLANES);
		refreshRate = GetDeviceCaps(hdc, VREFRESH);

		DeleteDC(hdc);
	}

	DisplayMonitor^ DisplayMonitor::FromWindow(IntPtr windowHandle)
	{
		return FromWindow(windowHandle, MonitorSearchFlags::DefaultToNull);
	}

	DisplayMonitor^ DisplayMonitor::FromWindow(IntPtr windowHandle, MonitorSearchFlags flags)
	{
		HMONITOR handle = MonitorFromWindow(reinterpret_cast<HWND>(windowHandle.ToPointer()), static_cast<DWORD>(flags));
		if (handle == NULL)
			return nullptr;

		return gcnew DisplayMonitor(IntPtr(handle));
	}

	DisplayMonitor^ DisplayMonitor::FromRectangle(System::Drawing::Rectangle rectangle)
	{
		return FromRectangle(rectangle, MonitorSearchFlags::DefaultToNull);
	}

	DisplayMonitor^ DisplayMonitor::FromRectangle(System::Drawing::Rectangle rectangle, MonitorSearchFlags flags)
	{
		RECT rect = marshal_as<RECT>(rectangle);
		HMONITOR handle = MonitorFromRect(&rect, static_cast<DWORD>(flags));
		if (handle == NULL)
			return nullptr;

		return gcnew DisplayMonitor(IntPtr(handle));
	}

	DisplayMonitor^ DisplayMonitor::FromPoint(System::Drawing::Point rectangle)
	{
		return FromPoint(rectangle, MonitorSearchFlags::DefaultToNull);
	}

	DisplayMonitor^ DisplayMonitor::FromPoint(System::Drawing::Point rectangle, MonitorSearchFlags flags)
	{
		POINT point = marshal_as<POINT>(rectangle);
		HMONITOR handle = MonitorFromPoint(point, static_cast<DWORD>(flags));
		if (handle == NULL)
			return nullptr;

		return gcnew DisplayMonitor(IntPtr(handle));
	}

	BOOL CALLBACK EnumProc(HMONITOR hMonitor, HDC, LPRECT, LPARAM dwData)
	{
		CollectionShim<DisplayMonitor^> *shim = reinterpret_cast<CollectionShim<DisplayMonitor^>*>(dwData);

		shim->GetItems()->Add(gcnew DisplayMonitor(IntPtr(hMonitor)));

		return TRUE;
	}

	array<DisplayMonitor^>^ DisplayMonitor::EnumerateMonitors()
	{
		CollectionShim<DisplayMonitor^> shim(gcnew List<DisplayMonitor^>());

		BOOL result = EnumDisplayMonitors(NULL, NULL, EnumProc, reinterpret_cast<LPARAM>(&shim));
		if (!result)
			return nullptr;

		return shim.GetItems()->ToArray();
	}

	bool DisplayMonitor::operator == ( DisplayMonitor^ left, DisplayMonitor^ right )
	{
		if( ReferenceEquals( left, nullptr ) )
			return ReferenceEquals( right, nullptr );

		return DisplayMonitor::Equals( left, right );
	}

	bool DisplayMonitor::operator != ( DisplayMonitor^ left, DisplayMonitor^ right )
	{
		return !( left == right );
	}

	int DisplayMonitor::GetHashCode()
	{		
		return reinterpret_cast<int>(handle);
	}

	bool DisplayMonitor::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<DisplayMonitor^>( value ) );
	}

	bool DisplayMonitor::Equals( DisplayMonitor^ value )
	{
		if( value == nullptr )
			return false;

		if( ReferenceEquals( this, value ) )
			return true;

		return handle == value->handle;
	}
}
}