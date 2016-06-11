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

#include "Enums.h"

namespace SlimDX
{
	/// <summary>
	/// Provides access to the Direct3D performance API, which allows applications to gather performance
	/// data and integrate with PIX for Windows in order to perform detailed analysis.
	/// </summary>
	/// <unmanaged>D3DPERF</unmanaged>
	public ref class Performance sealed
	{
	private:
		Performance() { }

	public:
		/// <summary>
		/// Marks the beginning of a user-defined event. PIX can use this event to trigger an action.
		/// </summary>
		/// <param name="color">Event color. This is the color used to display the event in the event view.</param>
		/// <param name="name">Event name.</param>
		/// <returns>
		/// The zero-based level of the hierarchy that this event is starting in. If an error occurs,
		/// the return value will be negative.
		/// </returns>
		static int BeginEvent( Color4 color, System::String^ name );

		/// <summary>
		/// Marks the end of a user-defined event. PIX can use this event to trigger an action.
		/// </summary>
		/// <returns>
		/// The zero-based level of the hierarchy in which the event is ending. If an error occurs,
		/// the return value will be negative.
		/// </returns>
		static int EndEvent();

		/// <summary>
		/// Determines the current state of the profiler from the target program.
		/// </summary>
		/// <returns><c>true</c> when PIX is profiling the target program; otherwise, <c>false</c>.</returns>
		static bool GetStatus();

		/// <summary>
		/// Determined whether a performance profiler is requesting that the current frame be resubmitted to
		/// Direct3D for performance analysis. This function is not currently supported by PIX.
		/// </summary>
		/// <returns><c>true</c> if the caller should repeate the same sequence of calls; otherwise, <c>false</c>.</returns>
		static bool QueryRepeatFrame();

		/// <summary>
		/// Marks an instantaneous event. PIX can use this event to trigger an action.
		/// </summary>
		/// <param name="color">Event color. This is the color used to display the event in the event view.</param>
		/// <param name="name">Event name.</param>
		static void SetMarker( Color4 color, System::String^ name );

		/// <summary>
		/// Sets profiler options specified by the target platform.
		/// </summary>
		static void SetOptions( PerformanceOptions options );

		/// <summary>
		/// Marks a series of frames with the specified color and name in the PIXRun file. 
		/// This function is not currently supported by PIX.
		/// </summary>
		/// <param name="color">Event color. This is the color used to display the event in the event view.</param>
		/// <param name="name">Event name.</param>
		static void SetRegion( Color4 color, System::String^ name );
	};
}
