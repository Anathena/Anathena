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

#include "Viewport11.h"

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class RasterizerState;
		
		/// <summary>
		/// Defines a wrapper for rasterizer related commands on the device.
		/// </summary>
		public ref class RasterizerWrapper
		{
		private:
			ID3D11DeviceContext* deviceContext;
			
		internal:
			RasterizerWrapper( ID3D11DeviceContext* device );
			
		public:
			/// <summary>
			/// Gets or sets the rasterizer state for the rasterizer stage of the pipeline.
			/// </summary>
			property RasterizerState^ State
			{
				void set( RasterizerState^ value );
				RasterizerState^ get();
			}

			/// <summary>
			/// Binds a single viewport to the rasterizer stage.
			/// </summary>
			/// <param name="viewport">The viewport to bind.</param>
			void SetViewports( Viewport viewport );

			/// <summary>
			/// Binds a set of viewports to the rasterizer stage.
			/// </summary>
			/// <param name="viewports">The set of viewports to bind.</param>
			void SetViewports( ... array<Viewport>^ viewports );

			/// <summary>
			/// Gets the set of viewports currently bound to the rasterizer stage.
			/// </summary>
			/// <param name="viewports">The array to fill with the bound viewports.</param>
			void GetViewports( array<Viewport>^ viewports );

			/// <summary>
			/// Gets the set of viewports currently bound to the rasterizer stage.
			/// </summary>
			/// <returns>An array of bound viewports.</returns>
			array<Viewport>^ GetViewports();

			/// <summary>
			/// Gets the first viewport currently bound to the rasterizer stage.
			/// </summary>
			/// <returns>The first bound viewport.</returns>
			Viewport GetViewport();

			/// <summary>
			/// Binds a single scissor rectangle to the rasterizer stage.
			/// </summary>
			/// <param name="scissorRectangle">The scissor rectangle to bind.</param>
			void SetScissorRectangles( System::Drawing::Rectangle scissorRectangle );

			/// <summary>
			/// Binds a set of scissor rectangles to the rasterizer stage.
			/// </summary>
			/// <param name="scissorRectangles">The set of scissor rectangles to bind.</param>
			void SetScissorRectangles( ... array<System::Drawing::Rectangle>^ scissorRectangles );

			/// <summary>
			/// Gets the set of scissor rectangles currently bound to the rasterizer stage.
			/// </summary>
			/// <param name="scissorRectangles">The array to fill with the bound scissor rectangles.</param>
			void GetScissorRectangles( array<System::Drawing::Rectangle>^ scissorRectangles );

			/// <summary>
			/// Gets the set of scissor rectangles currently bound to the rasterizer stage.
			/// </summary>
			/// <returns>An array of bound scissor rectangles.</returns>
			array<System::Drawing::Rectangle>^ GetScissorRectangles();
		};
	}
};