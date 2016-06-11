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
	ref class DataStream;
	
	/// <summary>
	/// A DataBox provides supporting information for a <see cref="SlimDX::DataStream"/> whose
	/// data is organized within three dimensions (a box).
	/// </summary>
	/// <unmanaged>None</unmanaged>
	public ref class DataBox
	{
	private:
		int m_RowPitch;
		int m_SlicePitch;
		DataStream^ m_Data;

	public:
		/// <summary>
		/// Initializes a new instance of the <see cref="DataBox"/> class.
		/// </summary>
		/// <param name="rowPitch">The row pitch, in bytes.</param>
		/// <param name="slicePitch">The slice pitch, in bytes.</param>
		/// <param name="data">The data.</param>
		DataBox( int rowPitch, int slicePitch, DataStream^ data );

		/// <summary>
		/// Gets or sets the number of bytes of data between two consecutive (1D) rows of data.
		/// </summary>
		property int RowPitch
		{
			int get();
			void set( int value );
		}

		/// <summary>
		/// Gets or sets the number of bytes of data between two consecutive (2D) slices of data.
		/// </summary>
		property int SlicePitch
		{
			int get();
			void set( int value );
		}
		
		/// <summary>
		/// Gets the <see cref="SlimDX::DataStream"/> containing the actual data bytes.
		/// </summary>
		property DataStream^ Data
		{
			DataStream^ get();
		}
	};
}