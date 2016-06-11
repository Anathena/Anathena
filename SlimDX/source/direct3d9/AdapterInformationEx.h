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
#include "AdapterExCollection.h"
#include "DisplayModeFilter.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		ref class AdapterDetails;
		value class DisplayModeEx;
		ref class Capabilities;
		ref class DisplayModeExCollection;

		/// <summary>Dscribes the capabilities and properties of an available adapter.</summary>
		/// <unmanaged>None</unmanaged>
		public ref class AdapterInformationEx
		{
		private:
			IDirect3D9Ex *m_direct3D;
			AdapterDetails^ details;

		internal:
			initonly int m_Adapter;

			AdapterInformationEx( IDirect3D9Ex *direct3D, unsigned int adapter, bool checkWhql );

		public:
			/// <summary>
			/// Gets the adapter ordinal.
			/// </summary>
			property int Adapter
			{
				int get() { return m_Adapter; }
			}

			/// <summary>
			/// Gets the monitor associated with the adapter.
			/// </summary>
			property System::IntPtr Monitor { System::IntPtr get(); }

			/// <summary>
			/// Gets the current extended display mode of the adapter.
			/// </summary>
			property DisplayModeEx CurrentDisplayMode { DisplayModeEx get(); }

			/// <summary>
			/// Gets a list of all possible display modes for the adapter.
			/// </summary>
			/// <param name="filter">The desired characteristics of the display mode.</param>
			/// <returns>A collection of possibly display modes.</returns>
            DisplayModeExCollection^ GetDisplayModes( DisplayModeFilter filter );

			/// <summary>
			/// Gets the capabilities of the adapter.
			/// </summary>
			/// <param name="type">The desired device type.</param>
			/// <returns>A <see cref="Capabilities"/> object containing the capabilities of the adapter.</returns>
            Capabilities^ GetCaps( DeviceType type );

			/// <summary>
			/// Gets or sets the adapter details.
			/// </summary>
			property AdapterDetails^ Details
			{
				AdapterDetails^ get() { return details; }
			}
		};
	}
}