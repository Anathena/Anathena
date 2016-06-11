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
#include "DeviceChildDxgi.h"

namespace SlimDX
{
	namespace DXGI
	{
		ref class Factory;
		ref class Output;
		value class FrameStatistics;
		value class ModeDescription;
		value class SwapChainDescription;
		
		/// <summary>
		/// Holds one or more surfaces that store rendered data prior to presenting
		/// that data to an output.
		/// </summary>
		/// <unmanaged>IDXGISwapChain</unmanaged>
		public ref class SwapChain : DeviceChild
		{
			COMOBJECT(IDXGISwapChain, SwapChain);
		
		public:
			/// <summary>
			/// Gets the swap chain's description.
			/// </summary>
			property SwapChainDescription Description
			{
				SwapChainDescription get();
			}
			
			/// <summary>
			/// Get performance statistics about the last render frame.
			/// </summary>
			property DXGI::FrameStatistics FrameStatistics
			{
				DXGI::FrameStatistics get();
			}
			
			/// <summary>
			/// Gets the number of times this swap chain's Present method has been called.
			/// </summary>
			property int PresentCount
			{
				int get();
			}
			
			/// <summary>
			/// Gets the output (the display monitor) that contains the majority of the client area of the target window.
			/// </summary>
			property Output^ ContainingOutput
			{
				Output^ get();
			}

			/// <summary>
			/// Gets or sets a value indicating whether the swap chain is rendering in full screen mode.
			/// </summary>
			property bool IsFullScreen
			{
				bool get();
				void set(bool value);
			}
			
			/// <summary>
			/// Initializes a new instance of the <see cref="SwapChain"/> class.
			/// </summary>
			/// <param name="factory">The factory used to create the swap chain.</param>
			/// <param name="device">The device used to present images to the swap chain.</param>
			/// <param name="description">Swap chain properties.</param>
			SwapChain( Factory^ factory, ComObject^ device, SwapChainDescription description );

			/// <summary>
			/// Releases all resources used by the <see cref="SwapChain"/>.
			/// </summary>
			virtual ~SwapChain();
			
			/// <summary>
			/// Gets the full screen state.
			/// </summary>
			/// <param name="isFullScreen">Receives a value indicating if the swap chain is running in full screen mode.</param>
			/// <param name="target">Receives the full screen output if the swap chain is in full screen mode. Otherwise ignored.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result GetFullScreenState( [Out] bool% isFullScreen, [Out] Output^% target );
			
			/// <summary>
			/// Sets the full screen state.
			/// </summary>
			/// <param name="isFullScreen">Set to true for full screen mode, false for windowed mode.</param>
			/// <param name="target">If the current display mode is full screen, this parameter must be the
			/// output containing the swap chain; otherwise, this parameter is ignored. If you set this parameter
			/// to null, DXGI will choose the output based on the swap-chain's device and the output window's placement.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetFullScreenState( bool isFullScreen, Output^ target );
			
			/// <summary>
			/// Changes the swap chain's back buffer size, format, and count.
			/// </summary>
			/// <param name="count">The new back buffer count.</param>
			/// <param name="width">The new back buffer width.</param>
			/// <param name="height">The new back buffer height.</param>
			/// <param name="format">The new back buffer format.</param>
			/// <param name="flags">Flags controlling swap chain functionality.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result ResizeBuffers( int count, int width, int height, Format format, SwapChainFlags flags );
			
			/// <summary>
			/// Changes the output target's size.
			/// </summary>
			/// <param name="description">Properties of the new output display mode.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result ResizeTarget( ModeDescription description );
			
			/// <summary>
			/// Presents a rendered image to the attached output.
			/// </summary>
			/// <param name="syncInterval">Specified how to synchronize the presentation with the vertical blank interrupt. 0
			/// indicates that presentation should occur immediately, without synchronization. Any other value indicates that
			/// presentation should be synchonized with the specified next vertical blank.</param>
			/// <param name="flags">Flags controlling presentation behavior.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result Present( int syncInterval, PresentFlags flags );
		};
	}
}