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

#include "../ComObject.h"

#include "../dxgi/Adapter.h"
#include "../dxgi/Enums.h"
#include "../dxgi/SwapChainDxgi.h"
#include "../dxgi/SwapChainDescription.h"
#include "../math/Color4.h"

#include "Enums11.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace Direct3D11
	{
		ref class DeviceContext;
		value class CounterCapabilities;
		value class CounterMetadata;

		/// <summary>
		/// Represents a virtual adapter used to perform rendering and create resources.
		/// </summary>
		/// <unmanaged>ID3D11Device</unmanaged>
		public ref class Device : ComObject
		{
			COMOBJECT(ID3D11Device, Device);

		private:
			DeviceContext^ immediateContext;
			void Initialize( DXGI::Adapter^ adapter, DriverType driverType, DeviceCreationFlags flags, const D3D_FEATURE_LEVEL *featureLevels, int count );

		public:
			/// <summary>
			/// The maximum number of multisample quality levels supported.
			/// </summary>
			literal int MultisampleCountMaximum = D3D11_MAX_MULTISAMPLE_SAMPLE_COUNT;

			/// <summary>
			/// Gets the parent factory associated with the device.
			/// </summary>
			property SlimDX::DXGI::Factory^ Factory
			{
				SlimDX::DXGI::Factory^ get();
			}

			/// <summary>
			/// Gets the flags used to create the device.
			/// </summary>
			property DeviceCreationFlags CreationFlags
			{
				DeviceCreationFlags get();
			}

			/// <summary>
			/// Gets the reason why the device was removed.
			/// </summary>
			property Result DeviceRemovedReason
			{
				Result get();
			}

			/// <summary>
			/// Gets the feature level of the hardware device.
			/// </summary>
			property FeatureLevel FeatureLevel
			{
				Direct3D11::FeatureLevel get();
			}

			/// <summary>
			/// Gets the intrinsic immediate context for the device.
			/// </summary>
			property DeviceContext^ ImmediateContext
			{
				DeviceContext^ get();
			}

			/// <summary>
			/// Gets a value indicating whether the current device is using the reference rasterizer.
			/// </summary>
			property bool IsReferenceDevice
			{
				bool get();
			}

			/// <summary>
			/// Gets or sets a friendly debug name for the object. This name is used in debug messages
			/// as well as in the PIX object browser.
			/// </summary>
			property System::String^ DebugName
			{
				System::String^ get();
				void set(System::String^ value);
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="Device"/> class using the highest supported feature level.
			/// </summary>
			/// <param name="adapter">The video adapter on which the device should be created.</param>
			Device( DXGI::Adapter^ adapter );

			/// <summary>
			/// Initializes a new instance of the <see cref="Device"/> class using the highest supported feature level.
			/// </summary>
			/// <param name="adapter">The video adapter on which the device should be created.</param>
			/// <param name="flags">A list of runtime layers to enable.</param>
			Device( DXGI::Adapter^ adapter, DeviceCreationFlags flags );

			/// <summary>
			/// Initializes a new instance of the <see cref="Device"/> class.
			/// </summary>
			/// <param name="adapter">The video adapter on which the device should be created.</param>
			/// <param name="flags">A list of runtime layers to enable.</param>
			/// <param name="featureLevels">A list of feature levels which determine the order of feature levels to attempt to create.</param>
			Device( DXGI::Adapter^ adapter, DeviceCreationFlags flags, ... array<Direct3D11::FeatureLevel>^ featureLevels );

			/// <summary>
			/// Initializes a new instance of the <see cref="Device"/> class using the highest supported feature level.
			/// </summary>
			/// <param name="driverType">The type of device to create.</param>
			Device( DriverType driverType );

			/// <summary>
			/// Initializes a new instance of the <see cref="Device"/> class using the highest supported feature level.
			/// </summary>
			/// <param name="driverType">The type of device to create.</param>
			/// <param name="flags">A list of runtime layers to enable.</param>
			Device( DriverType driverType, DeviceCreationFlags flags );

			/// <summary>
			/// Initializes a new instance of the <see cref="Device"/> class.
			/// </summary>
			/// <param name="driverType">The type of device to create.</param>
			/// <param name="flags">A list of runtime layers to enable.</param>
			/// <param name="featureLevels">A list of feature levels which determine the order of feature levels to attempt to create.</param>
			Device( DriverType driverType, DeviceCreationFlags flags, ... array<Direct3D11::FeatureLevel>^ featureLevels );

			/// <summary>
			/// Gets information about the device's performance counters.
			/// </summary>
			/// <returns>Information about the device's performance counters.</returns>
			CounterCapabilities GetCounterCapabilities();

			/// <summary>
			/// Gets metadata (name, type, measurement units, et cetera) for a specific counter.
			/// </summary>
			/// <param name="counterKind">The type of the counter to retrieve information about.</param>
			/// <returns>Metadata for the specified counter.</returns>
			CounterMetadata GetCounterMetadata( CounterKind counterKind );
			
			/// <summary>
			/// Gets information about the supported applications of a specified format.
			/// </summary>
			/// <param name="format">The format for which to check support.</param>
			/// <returns>Flags indicating usage contexts in which the specified format is supported.</returns>
			FormatSupport CheckFormatSupport( DXGI::Format format );

			/// <summary>
			/// Gets information about the supported compute shader applications of a specified format.
			/// </summary>
			/// <param name="format">The format for which to check support.</param>
			/// <returns>Flags indicating usage contexts in which the specified format is supported.</returns>
			ComputeShaderFormatSupport CheckComputeShaderFormatSupport( DXGI::Format format );

			/// <summary>
			/// Checks for hardware support of the specified device feature.
			/// </summary>
			/// <returns><c>true</c> if the specified feature is supported; otherwise, <c>false</c>.</returns>
			bool CheckFeatureSupport( Feature feature );

			/// <summary>
			/// Checks the level of hardware multithreading supported by this device.
			/// </summary>
			/// <param name="supportsConcurrentResources">When the method completes, contains a value indicating whether resources can be created concurrently on multiple threads while drawing.</param>
			/// <param name="supportCommandLists">When the method completes, contains a value indicating whether command lists are supported by the current driver.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result CheckThreadingSupport( [Out] bool% supportsConcurrentResources, [Out] bool% supportCommandLists );

			/// <summary>
			/// Gets the number of quality levels supported during multisampling of resources using a specified format.
			/// </summary>
			/// <param name="format">The format to check support for.</param>
			/// <param name="sampleCount">The multisample count for which to check support.</param>
			/// <returns>The number of quality levels supported. 0 if an error occured or if the format/sampleCount pair is not supported.</returns>
			int CheckMultisampleQualityLevels( DXGI::Format format, int sampleCount );

			/// <summary>
			/// Gives the device access to a shared resource created on a different device.
			/// </summary>
			/// <typeparam name="T">The type of the resource.</typeparam>
			/// <param name="handle">A handle to the shared resource.</param>
			/// <returns>The new reference to the shared resource.</returns>
			generic<typename T> where T : ComObject
			T OpenSharedResource(System::IntPtr handle);

			/// <summary>
			/// Gets the highest supported hardware feature level of the primary adapter.
			/// </summary>
			/// <returns>The highest supported hardware feature level.</returns>
			static Direct3D11::FeatureLevel GetSupportedFeatureLevel();

			/// <summary>
			/// Gets the highest supported hardware feature level of the given adapter.
			/// </summary>
			/// <param name="adapter">The adapter for which the feature level is to be retrieved.</param>
			/// <returns>The highest supported hardware feature level.</returns>
			static Direct3D11::FeatureLevel GetSupportedFeatureLevel( DXGI::Adapter^ adapter );

			/// <summary>
			/// Initializes a new instance of the <see cref="Device"/> class along with a new <see cref="SlimDX::DXGI::SwapChain"/> used for rendering.
			/// </summary>
			/// <param name="adapter">The video adapter on which the device should be created.</param>
			/// <param name="flags">A list of runtime layers to enable.</param>
			/// <param name="swapChainDescription">Details used to create the swap chain.</param>
			/// <param name="device">When the method completes, contains the created device instance.</param>
			/// <param name="swapChain">When the method completes, contains the created swap chain instance.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			static Result CreateWithSwapChain( DXGI::Adapter^ adapter, DeviceCreationFlags flags, DXGI::SwapChainDescription swapChainDescription, [Out] Device^ %device, [Out] DXGI::SwapChain^ %swapChain );
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Device"/> class along with a new <see cref="SlimDX::DXGI::SwapChain"/> used for rendering.
			/// </summary>
			/// <param name="adapter">The video adapter on which the device should be created.</param>
			/// <param name="flags">A list of runtime layers to enable.</param>
			/// <param name="featureLevels">A list of feature levels which determine the order of feature levels to attempt to create.</param>
			/// <param name="swapChainDescription">Details used to create the swap chain.</param>
			/// <param name="device">When the method completes, contains the created device instance.</param>
			/// <param name="swapChain">When the method completes, contains the created swap chain instance.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			static Result CreateWithSwapChain( DXGI::Adapter^ adapter, DeviceCreationFlags flags, array<Direct3D11::FeatureLevel>^ featureLevels, DXGI::SwapChainDescription swapChainDescription, [Out] Device^ %device, [Out] DXGI::SwapChain^ %swapChain );

			/// <summary>
			/// Initializes a new instance of the <see cref="Device"/> class along with a new <see cref="SlimDX::DXGI::SwapChain"/> used for rendering.
			/// </summary>
			/// <param name="driverType">The type of device to create.</param>
			/// <param name="flags">A list of runtime layers to enable.</param>
			/// <param name="swapChainDescription">Details used to create the swap chain.</param>
			/// <param name="device">When the method completes, contains the created device instance.</param>
			/// <param name="swapChain">When the method completes, contains the created swap chain instance.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			static Result CreateWithSwapChain( DriverType driverType, DeviceCreationFlags flags, DXGI::SwapChainDescription swapChainDescription, [Out] Device^ %device, [Out] DXGI::SwapChain^ %swapChain );
			
			/// <summary>
			/// Initializes a new instance of the <see cref="Device"/> class along with a new <see cref="SlimDX::DXGI::SwapChain"/> used for rendering.
			/// </summary>
			/// <param name="driverType">The type of device to create.</param>
			/// <param name="flags">A list of runtime layers to enable.</param>
			/// <param name="featureLevels">A list of feature levels which determine the order of feature levels to attempt to create.</param>
			/// <param name="swapChainDescription">Details used to create the swap chain.</param>
			/// <param name="device">When the method completes, contains the created device instance.</param>
			/// <param name="swapChain">When the method completes, contains the created swap chain instance.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			static Result CreateWithSwapChain( DriverType driverType, DeviceCreationFlags flags, array<Direct3D11::FeatureLevel>^ featureLevels, DXGI::SwapChainDescription swapChainDescription, [Out] Device^ %device, [Out] DXGI::SwapChain^ %swapChain );

			/// <summary>
			/// This overload has been deprecated. Use one of the alternatives that does not take both an adapter and a driver type.
			/// </summary>
			[System::Obsolete("This method is confusing because only one of the adapter/driverType parameters is valid during the call. Use one of the overloads that explicitly uses the parameter you wish to provide.")]
			static Result CreateWithSwapChain( DXGI::Adapter^ adapter, DriverType driverType, DeviceCreationFlags flags, DXGI::SwapChainDescription swapChainDescription, [Out] Device^ %device, [Out] DXGI::SwapChain^ %swapChain );

			/// <summary>
			/// This overload has been deprecated. Use one of the alternatives that does not take both an adapter and a driver type.
			/// </summary>
			[System::Obsolete("This method is confusing because only one of the adapter/driverType parameters is valid during the call. Use one of the overloads that explicitly uses the parameter you wish to provide.")]
			static Result CreateWithSwapChain( DXGI::Adapter^ adapter, DriverType driverType, DeviceCreationFlags flags, array<Direct3D11::FeatureLevel>^ featureLevels, DXGI::SwapChainDescription swapChainDescription, [Out] Device^ %device, [Out] DXGI::SwapChain^ %swapChain );
		};
	}
};