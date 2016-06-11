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
#include "DisplayMode.h"
#include "AdapterDetails.h"
#include "Capabilities.h"
#include "AdapterInformation.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace Direct3D9
	{
		/// <summary>
		/// Used to create Microsoft Direct3D objects and set up the environment. This class 
		/// includes methods for enumerating and retrieving capabilities of the device.
		/// </summary>
		/// <unmanaged>IDirect3D9</unmanaged>
		public ref class Direct3D : ComObject
		{
			COMOBJECT(IDirect3D9, Direct3D);

		private:
			AdapterCollection^ adapters;

		private protected:
			Direct3D( bool doNotConstruct );

		public:
			Direct3D();

			/// <summary>
			/// Gets or sets a value indicating whether Direct3D should check WHQL levels. If this value
			/// is set to <c>true</c>, Direct3D can connect to the internet in order to download
			/// new Microsoft Windows Hardware Quality Labs (WHQL) certificates.
			/// </summary>
			property bool CheckWhql;

			/// <summary>
			/// Gets a collection of installed adapters.
			/// </summary>
			property AdapterCollection^ Adapters
			{
				AdapterCollection^ get() { return adapters; }
			private protected:
				void set( AdapterCollection^ value ) { adapters = value; }
			}

			/// <summary>
			/// Gets the number of adapters on the system.
			/// </summary>
			property int AdapterCount
			{
				int get() { return InternalPointer->GetAdapterCount(); }
			}

			/// <summary>
			/// Tests the device to see if it supports conversion from one display format to another.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <param name="deviceType">The desired device type.</param>
			/// <param name="sourceFormat">Source adapter format.</param>
			/// <param name="targetFormat">Destination adapter format.</param>
			/// <param name="result">When the method completes, contains a <see cref="SlimDX::Result"/> object describing the result of the operation.</param>
			/// <returns><c>true</c> if the conversion is possible; otherwise, <c>false</c>.</returns>
			bool CheckDeviceFormatConversion(int adapter, DeviceType deviceType, Format sourceFormat, Format targetFormat, [Out] Result% result);

			/// <summary>
			/// Tests the device to see if it supports conversion from one display format to another.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <param name="deviceType">The desired device type.</param>
			/// <param name="sourceFormat">Source adapter format.</param>
			/// <param name="targetFormat">Destination adapter format.</param>
			/// <returns><c>true</c> if the conversion is possible; otherwise, <c>false</c>.</returns>
			bool CheckDeviceFormatConversion(int adapter, DeviceType deviceType, Format sourceFormat, Format targetFormat);

			/// <summary>
			/// Determines whether a surface format is available as a specified resource type and can be
			/// used as a texture, depth-stencil buffer, or render target, or any combination of the three,
			/// on a device representing this adapter.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <param name="deviceType">The desired device type.</param>
			/// <param name="adapterFormat">Format of the display mode into which the adapter will be placed.</param>
			/// <param name="usage">Requested usage options for the surface.</param>
			/// <param name="resourceType">Resource type requested for use with the queried format.</param>
			/// <param name="checkFormat">The format that will be checked for compatibility.</param>
			/// <param name="result">When the method completes, contains a <see cref="SlimDX::Result"/> object describing the result of the operation.</param>
			/// <returns><c>true</c> if the format is compatible with the specified device; otherwise, <c>false</c>.</returns>
			bool CheckDeviceFormat( int adapter, DeviceType deviceType, Format adapterFormat, Usage usage, ResourceType resourceType, Format checkFormat, [Out] Result% result );
			
			/// <summary>
			/// Determines whether a surface format is available as a specified resource type and can be
			/// used as a texture, depth-stencil buffer, or render target, or any combination of the three,
			/// on a device representing this adapter.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <param name="deviceType">The desired device type.</param>
			/// <param name="adapterFormat">Format of the display mode into which the adapter will be placed.</param>
			/// <param name="usage">Requested usage options for the surface.</param>
			/// <param name="resourceType">Resource type requested for use with the queried format.</param>
			/// <param name="checkFormat">The format that will be checked for compatibility.</param>
			/// <returns><c>true</c> if the format is compatible with the specified device; otherwise, <c>false</c>.</returns>
			bool CheckDeviceFormat( int adapter, DeviceType deviceType, Format adapterFormat, Usage usage, ResourceType resourceType, Format checkFormat );

			/// <summary>
			/// Verifies whether a hardware accelerated device type can be used on this adapter.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <param name="deviceType">The desired device type.</param>
			/// <param name="adapterFormat">Format of the display mode into which the adapter will be placed.</param>
			/// <param name="backBufferFormat">Format of the back buffer.</param>
			/// <param name="windowed">Value indicating whether the device type will be used in full-screen or windowed mode.</param>
			/// <param name="result">When the method completes, contains a <see cref="SlimDX::Result"/> object describing the result of the operation.</param>
			/// <returns><c>true</c> if the device can be used with the specified settings; otherwise, <c>false</c>.</returns>
			bool CheckDeviceType( int adapter, DeviceType deviceType, Format adapterFormat, Format backBufferFormat, bool windowed, [Out] Result% result );
			
			/// <summary>
			/// Verifies whether a hardware accelerated device type can be used on this adapter.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <param name="deviceType">The desired device type.</param>
			/// <param name="adapterFormat">Format of the display mode into which the adapter will be placed.</param>
			/// <param name="backBufferFormat">Format of the back buffer.</param>
			/// <param name="windowed">Value indicating whether the device type will be used in full-screen or windowed mode.</param>
			/// <returns><c>true</c> if the device can be used with the specified settings; otherwise, <c>false</c>.</returns>
			bool CheckDeviceType( int adapter, DeviceType deviceType, Format adapterFormat, Format backBufferFormat, bool windowed );

			/// <summary>
			/// Determines whether a depth-stencil format is compatible with a render-target format in a particular display mode.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <param name="deviceType">The desired device type.</param>
			/// <param name="adapterFormat">Format of the display mode into which the adapter will be placed.</param>
			/// <param name="renderTargetFormat">Format of the render target surface.</param>
			/// <param name="depthStencilFormat">Format of the depth-stencil surface.</param>
			/// <param name="result">When the method completes, contains a <see cref="SlimDX::Result"/> object describing the result of the operation.</param>
			/// <returns><c>true</c> if the depth-stencil surface is compatible with the render target format; otherwise, <c>false</c>.</returns>
			bool CheckDepthStencilMatch( int adapter, DeviceType deviceType, Format adapterFormat, Format renderTargetFormat, Format depthStencilFormat, [Out] Result% result );
			
			/// <summary>
			/// Determines whether a depth-stencil format is compatible with a render-target format in a particular display mode.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <param name="deviceType">The desired device type.</param>
			/// <param name="adapterFormat">Format of the display mode into which the adapter will be placed.</param>
			/// <param name="renderTargetFormat">Format of the render target surface.</param>
			/// <param name="depthStencilFormat">Format of the depth-stencil surface.</param>
			/// <returns><c>true</c> if the depth-stencil surface is compatible with the render target format; otherwise, <c>false</c>.</returns>
			bool CheckDepthStencilMatch( int adapter, DeviceType deviceType, Format adapterFormat, Format renderTargetFormat, Format depthStencilFormat );

			/// <summary>
			/// Determines if a multisampling technique is available on this device.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <param name="deviceType">The desired device type.</param>
			/// <param name="surfaceFormat">Format of the surface to be multisampled.</param>
			/// <param name="windowed"><c>true</c> to inquire about windowed multisampling, <c>false</c> to inquire about fullscreen multisampling.</param>
			/// <param name="multisampleType">The multisampling technique to test.</param>
			/// <param name="qualityLevels">When the method completes, contains the number of quality stops available for a given multisample type.</param>
			/// <param name="result">When the method completes, contains a <see cref="SlimDX::Result"/> object describing the result of the operation.</param>
			/// <returns><c>true</c> if the device can perform the specified multisampling method; otherwise, <c>false</c>.</returns>
			bool CheckDeviceMultisampleType( int adapter, DeviceType deviceType, Format surfaceFormat, bool windowed, MultisampleType multisampleType, [Out] int% qualityLevels, [Out] Result% result );
			
			/// <summary>
			/// Determines if a multisampling technique is available on this device.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <param name="deviceType">The desired device type.</param>
			/// <param name="surfaceFormat">Format of the surface to be multisampled.</param>
			/// <param name="windowed"><c>true</c> to inquire about windowed multisampling, <c>false</c> to inquire about fullscreen multisampling.</param>
			/// <param name="multisampleType">The multisampling technique to test.</param>
			/// <param name="qualityLevels">When the method completes, contains the number of quality stops available for a given multisample type.</param>
			/// <returns><c>true</c> if the device can perform the specified multisampling method; otherwise, <c>false</c>.</returns>
			bool CheckDeviceMultisampleType( int adapter, DeviceType deviceType, Format surfaceFormat, bool windowed, MultisampleType multisampleType, [Out] int% qualityLevels );

			/// <summary>
			/// Determines if a multisampling technique is available on this device.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <param name="deviceType">The desired device type.</param>
			/// <param name="surfaceFormat">Format of the surface to be multisampled.</param>
			/// <param name="windowed"><c>true</c> to inquire about windowed multisampling, <c>false</c> to inquire about fullscreen multisampling.</param>
			/// <param name="multisampleType">The multisampling technique to test.</param>
			/// <returns><c>true</c> if the device can perform the specified multisampling method; otherwise, <c>false</c>.</returns>
			bool CheckDeviceMultisampleType( int adapter, DeviceType deviceType, Format surfaceFormat, bool windowed, MultisampleType multisampleType );

			/// <summary>
			/// Retrieves the current display mode for the specified adapter.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <returns>The current display mode for the specified adapter.</returns>
            DisplayMode GetAdapterDisplayMode( int adapter );

			/// <summary>
			/// Retrieves information about the specified adapter.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <returns>Information about the specified adapter.</returns>
            AdapterDetails^ GetAdapterIdentifier( int adapter );

			/// <summary>
			/// Returns the number of display modes available on the specified adapter.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <param name="format">Desired surface format.</param>
			/// <returns>The number of display modes available on the specified adapter.</returns>
            int GetAdapterModeCount( int adapter, Format format );

			/// <summary>
			/// Queries the device to determine whether the specified adapter supports the requested 
			/// format and display mode.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <param name="format">Desired surface format.</param>
			/// <param name="modeIndex">Index of the desired display mode.</param>
			/// <returns>The display mode that matches the specified information.</returns>
            DisplayMode EnumerateAdapterModes( int adapter, Format format, int modeIndex );

			/// <summary>
			/// Returns the handle of the monitor associated with the Direct3D object.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <returns>Handle of the monitor associated with the Direct3D object.</returns>
            System::IntPtr GetAdapterMonitor( int adapter );

			/// <summary>
			/// Retrieves device-specific information about a device.
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <param name="deviceType">Desired device type.</param>
			/// <returns>A <see cref="Capabilities"/> object containing the capabilities of the adapter.</returns>
			Capabilities^ GetDeviceCaps( int adapter, DeviceType deviceType );

			/// <summary>
			/// Determines whether the device supports Render-To-Vertex-Buffer (R2VB).
			/// </summary>
			/// <param name="adapter">Index of the adapter to use.</param>
			/// <param name="deviceType">The desired device type.</param>
			/// <returns><c>true</c> if the adapter supports R2VB; otherwise, <c>false</c>.</returns>
			bool SupportsR2VB( int adapter, DeviceType deviceType );

			Result RegisterSoftwareDevice( System::IntPtr entryPoint );
		};
	}
}
