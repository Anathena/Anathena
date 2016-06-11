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

namespace SlimDX
{
	namespace XACT3
	{
		/// <summary>
		/// Information about a particular renderer.
		/// </summary>
		/// <unmanaged>XACT_RENDERER_DETAILS</unmanaged>
		public value class RendererDetails
		{
		private:
			bool isDefault;
			System::String^ name;
			System::String^ id;

		internal:
			RendererDetails(const XACT_RENDERER_DETAILS& rendererDetails);

		public:
			/// <summary>
			/// Gets a value that is true if this is the default audio renderer, otherwise false.
			/// </summary>
			property bool IsDefault 
			{
				bool get() { return isDefault; } 
			}

			/// <summary>
			/// Gets the name of the renderer.
			/// </summary>
			property System::String^ DisplayName 
			{
				System::String^ get() { return name; } 
			}

			/// <summary>
			/// Gets the id of the renderer.
			/// </summary>
			property System::String^ RendererId 
			{
				System::String^ get() { return id; }
			}
		};
	}
}
