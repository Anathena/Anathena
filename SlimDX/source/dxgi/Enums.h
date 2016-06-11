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

#include <dxgi.h>

namespace SlimDX
{
	namespace DXGI
	{
		// NOTE: The enumerations defined in this file are in alphabetical order. When
		//       adding new enumerations or renaming existing ones, please make sure
		//       the ordering is maintained.
		
		[System::Flags]
		public enum class AdapterFlags : System::Int32
		{
			None = DXGI_ADAPTER_FLAG_NONE,
			Remote = DXGI_ADAPTER_FLAG_REMOTE
		};

		[System::Flags]
		public enum class DisplayModeEnumerationFlags : System::Int32
		{
			Interlaced = DXGI_ENUM_MODES_INTERLACED,
			Scaling = DXGI_ENUM_MODES_SCALING
		};
		
		public enum class DisplayModeRotation : System::Int32
		{
			Unspecified = DXGI_MODE_ROTATION_UNSPECIFIED,
			Identity = DXGI_MODE_ROTATION_IDENTITY,
			Rotate90Degrees = DXGI_MODE_ROTATION_ROTATE90,
			Rotate180Degrees = DXGI_MODE_ROTATION_ROTATE180,
			Rotate270Degrees = DXGI_MODE_ROTATION_ROTATE270
		};

		public enum class DisplayModeScaling : System::Int32
		{
			Unspecified = DXGI_MODE_SCALING_UNSPECIFIED,
			Centered = DXGI_MODE_SCALING_CENTERED,
			Stretched = DXGI_MODE_SCALING_STRETCHED
		};

		public enum class DisplayModeScanlineOrdering : System::Int32
		{
			Unspecified = DXGI_MODE_SCANLINE_ORDER_UNSPECIFIED,
			Progressive = DXGI_MODE_SCANLINE_ORDER_PROGRESSIVE,
			UpperFieldFirst = DXGI_MODE_SCANLINE_ORDER_UPPER_FIELD_FIRST,
			LowerFieldFirst = DXGI_MODE_SCANLINE_ORDER_LOWER_FIELD_FIRST
		};
		
		public enum class Format : System::Int32
		{
			Unknown = DXGI_FORMAT_UNKNOWN,
			R32G32B32A32_Typeless = DXGI_FORMAT_R32G32B32A32_TYPELESS,
			R32G32B32A32_Float = DXGI_FORMAT_R32G32B32A32_FLOAT,
			R32G32B32A32_UInt = DXGI_FORMAT_R32G32B32A32_UINT,
			R32G32B32A32_SInt = DXGI_FORMAT_R32G32B32A32_SINT,
			R32G32B32_Typeless = DXGI_FORMAT_R32G32B32_TYPELESS,
			R32G32B32_Float = DXGI_FORMAT_R32G32B32_FLOAT,
			R32G32B32_UInt = DXGI_FORMAT_R32G32B32_UINT,
			R32G32B32_SInt = DXGI_FORMAT_R32G32B32_SINT,
			R16G16B16A16_Typeless = DXGI_FORMAT_R16G16B16A16_TYPELESS,
			R16G16B16A16_Float = DXGI_FORMAT_R16G16B16A16_FLOAT,
			R16G16B16A16_UNorm = DXGI_FORMAT_R16G16B16A16_UNORM,
			R16G16B16A16_UInt = DXGI_FORMAT_R16G16B16A16_UINT,
			R16G16B16A16_SNorm = DXGI_FORMAT_R16G16B16A16_SNORM,
			R16G16B16A16_SInt = DXGI_FORMAT_R16G16B16A16_SINT,
			R32G32_Typeless = DXGI_FORMAT_R32G32_TYPELESS,
			R32G32_Float = DXGI_FORMAT_R32G32_FLOAT,
			R32G32_UInt = DXGI_FORMAT_R32G32_UINT,
			R32G32_SInt = DXGI_FORMAT_R32G32_SINT,
			R32G8X24_Typeless = DXGI_FORMAT_R32G8X24_TYPELESS,
			D32_Float_S8X24_UInt = DXGI_FORMAT_D32_FLOAT_S8X24_UINT,
			R32_Float_X8X24_Typeless = DXGI_FORMAT_R32_FLOAT_X8X24_TYPELESS,
			X32_Typeless_G8X24_UInt = DXGI_FORMAT_X32_TYPELESS_G8X24_UINT,
			R10G10B10A2_Typeless = DXGI_FORMAT_R10G10B10A2_TYPELESS,
			R10G10B10A2_UNorm = DXGI_FORMAT_R10G10B10A2_UNORM,
			R10G10B10A2_UInt = DXGI_FORMAT_R10G10B10A2_UINT,
			R11G11B10_Float = DXGI_FORMAT_R11G11B10_FLOAT,
			R8G8B8A8_Typeless = DXGI_FORMAT_R8G8B8A8_TYPELESS,
			R8G8B8A8_UNorm = DXGI_FORMAT_R8G8B8A8_UNORM,
			R8G8B8A8_UNorm_SRGB = DXGI_FORMAT_R8G8B8A8_UNORM_SRGB,
			R8G8B8A8_UInt = DXGI_FORMAT_R8G8B8A8_UINT,
			R8G8B8A8_SNorm = DXGI_FORMAT_R8G8B8A8_SNORM,
			R8G8B8A8_SInt = DXGI_FORMAT_R8G8B8A8_SINT,
			R16G16_Typeless = DXGI_FORMAT_R16G16_TYPELESS,
			R16G16_Float = DXGI_FORMAT_R16G16_FLOAT,
			R16G16_UNorm = DXGI_FORMAT_R16G16_UNORM,
			R16G16_UInt = DXGI_FORMAT_R16G16_UINT,
			R16G16_SNorm = DXGI_FORMAT_R16G16_SNORM,
			R16G16_SInt = DXGI_FORMAT_R16G16_SINT,
			R32_Typeless = DXGI_FORMAT_R32_TYPELESS,
			D32_Float = DXGI_FORMAT_D32_FLOAT,
			R32_Float = DXGI_FORMAT_R32_FLOAT,
			R32_UInt = DXGI_FORMAT_R32_UINT,
			R32_SInt = DXGI_FORMAT_R32_SINT,
			R24G8_Typeless = DXGI_FORMAT_R24G8_TYPELESS,
			D24_UNorm_S8_UInt = DXGI_FORMAT_D24_UNORM_S8_UINT,
			R24_UNorm_X8_Typeless = DXGI_FORMAT_R24_UNORM_X8_TYPELESS,
			X24_Typeless_G8_UInt = DXGI_FORMAT_X24_TYPELESS_G8_UINT,
			R8G8_Typeless = DXGI_FORMAT_R8G8_TYPELESS,
			R8G8_UNorm = DXGI_FORMAT_R8G8_UNORM,
			R8G8_UInt = DXGI_FORMAT_R8G8_UINT,
			R8G8_SNorm = DXGI_FORMAT_R8G8_SNORM ,
			R8G8_SInt = DXGI_FORMAT_R8G8_SINT,
			R16_Typeless = DXGI_FORMAT_R16_TYPELESS,
			R16_Float = DXGI_FORMAT_R16_FLOAT,
			D16_UNorm = DXGI_FORMAT_D16_UNORM,
			R16_UNorm = DXGI_FORMAT_R16_UNORM,
			R16_UInt = DXGI_FORMAT_R16_UINT ,
			R16_SNorm = DXGI_FORMAT_R16_SNORM ,
			R16_SInt = DXGI_FORMAT_R16_SINT,
			R8_Typeless = DXGI_FORMAT_R8_TYPELESS,
			R8_UNorm = DXGI_FORMAT_R8_UNORM,
			R8_UInt = DXGI_FORMAT_R8_UINT,
			R8_SNorm = DXGI_FORMAT_R8_SNORM,
			R8_SInt = DXGI_FORMAT_R8_SINT,
			A8_UNorm = DXGI_FORMAT_A8_UNORM,
			R1_UNorm = DXGI_FORMAT_R1_UNORM,
			R9G9B9E5_SharedExp = DXGI_FORMAT_R9G9B9E5_SHAREDEXP,
			R8G8_B8G8_UNorm = DXGI_FORMAT_R8G8_B8G8_UNORM,
			G8R8_G8B8_UNorm = DXGI_FORMAT_G8R8_G8B8_UNORM,
			BC1_Typeless = DXGI_FORMAT_BC1_TYPELESS,
			BC1_UNorm = DXGI_FORMAT_BC1_UNORM,
			BC1_UNorm_SRGB = DXGI_FORMAT_BC1_UNORM_SRGB,
			BC2_Typeless = DXGI_FORMAT_BC2_TYPELESS,
			BC2_UNorm = DXGI_FORMAT_BC2_UNORM,
			BC2_UNorm_SRGB = DXGI_FORMAT_BC2_UNORM_SRGB,
			BC3_Typeless = DXGI_FORMAT_BC3_TYPELESS,
			BC3_UNorm = DXGI_FORMAT_BC3_UNORM,
			BC3_UNorm_SRGB = DXGI_FORMAT_BC3_UNORM_SRGB,
			BC4_Typeless = DXGI_FORMAT_BC4_TYPELESS,
			BC4_UNorm = DXGI_FORMAT_BC4_UNORM,
			BC4_SNorm = DXGI_FORMAT_BC4_SNORM,
			BC5_Typeless = DXGI_FORMAT_BC5_TYPELESS,
			BC5_UNorm = DXGI_FORMAT_BC5_UNORM,
			BC5_SNorm = DXGI_FORMAT_BC5_SNORM,
			B5G6R5_UNorm = DXGI_FORMAT_B5G6R5_UNORM,
			B5G5R5A1_UNorm = DXGI_FORMAT_B5G5R5A1_UNORM,
			B8G8R8A8_UNorm = DXGI_FORMAT_B8G8R8A8_UNORM,
			B8G8R8X8_UNorm = DXGI_FORMAT_B8G8R8X8_UNORM,
			//////////////////////////////////////////////////////////////////////////
			// DirectX 11 Format entries
			//////////////////////////////////////////////////////////////////////////

			/// <summary>Not Supported below DirectX 11.</summary>
			R10G10B10_XR_Bias_A2_UNorm = DXGI_FORMAT_R10G10B10_XR_BIAS_A2_UNORM,
			/// <summary>Not Supported below DirectX 11.</summary>
			B8G8R8A8_Typeless = DXGI_FORMAT_B8G8R8A8_TYPELESS,
			/// <summary>Not Supported below DirectX 11.</summary>
			B8G8R8A8_UNorm_SRGB = DXGI_FORMAT_B8G8R8A8_UNORM_SRGB,
			/// <summary>Not Supported below DirectX 11.</summary>
			B8G8R8X8_Typeless = DXGI_FORMAT_B8G8R8X8_TYPELESS,
			/// <summary>Not Supported below DirectX 11.</summary>
			B8G8R8X8_UNorm_SRGB = DXGI_FORMAT_B8G8R8X8_UNORM_SRGB, 
			/// <summary>Not Supported below DirectX 11.</summary>
			BC6_Typeless = DXGI_FORMAT_BC6H_TYPELESS,
			/// <summary>Not Supported below DirectX 11.</summary>
			BC6_UFloat16 = DXGI_FORMAT_BC6H_UF16,
			/// <summary>Not Supported below DirectX 11.</summary>
			BC6_SFloat16 = DXGI_FORMAT_BC6H_SF16,
			/// <summary>Not Supported below DirectX 11.</summary>
			BC7_Typeless = DXGI_FORMAT_BC7_TYPELESS,
			/// <summary>Not Supported below DirectX 11.</summary>
			BC7_UNorm = DXGI_FORMAT_BC7_UNORM,
			/// <summary>Not Supported below DirectX 11.</summary>
			BC7_UNorm_SRGB = DXGI_FORMAT_BC7_UNORM_SRGB
		};
		
		[System::Flags]
		public enum class MapFlags : System::Int32
		{
			Read = DXGI_MAP_READ,
			Write = DXGI_MAP_WRITE,
			Discard = DXGI_MAP_DISCARD
		};
		
		[System::Flags]
		public enum class PresentFlags : System::Int32
		{
			None = 0,
			DoNotSequence = DXGI_PRESENT_DO_NOT_SEQUENCE,
			Restart = DXGI_PRESENT_RESTART,
			Test = DXGI_PRESENT_TEST
		};
		
		public enum class Residency : System::Int32
		{
			FullyResident = DXGI_RESIDENCY_FULLY_RESIDENT,
			ResidentInSharedMemory = DXGI_RESIDENCY_RESIDENT_IN_SHARED_MEMORY,
			EvictedToDisk = DXGI_RESIDENCY_EVICTED_TO_DISK
		};
		
#pragma warning(disable: 4369)
		public enum class ResourcePriority : System::Int32
		{
			Minimum = DXGI_RESOURCE_PRIORITY_MINIMUM,
			Low = DXGI_RESOURCE_PRIORITY_LOW,
			Normal = DXGI_RESOURCE_PRIORITY_NORMAL,
			High = DXGI_RESOURCE_PRIORITY_HIGH,
			Maximum = DXGI_RESOURCE_PRIORITY_MAXIMUM
		};
#pragma warning(default: 4369)

		[System::Flags]
		public enum class SwapChainFlags : System::Int32
		{
			None = 0,
			NonPrerotated = DXGI_SWAP_CHAIN_FLAG_NONPREROTATED,
			AllowModeSwitch = DXGI_SWAP_CHAIN_FLAG_ALLOW_MODE_SWITCH
		};

		public enum class SwapEffect : System::Int32
		{
			Discard = DXGI_SWAP_EFFECT_DISCARD,
			Sequential = DXGI_SWAP_EFFECT_SEQUENTIAL
		};
		
		[System::Flags]
		public enum class Usage : System::Int32
		{
			ShaderInput = DXGI_USAGE_SHADER_INPUT,
			RenderTargetOutput = DXGI_USAGE_RENDER_TARGET_OUTPUT,
			BackBuffer = DXGI_USAGE_BACK_BUFFER,
			Shared = DXGI_USAGE_SHARED,
			ReadOnly = DXGI_USAGE_READ_ONLY,
			UnorderedAccess = DXGI_USAGE_UNORDERED_ACCESS
		};
		
		/// <summary>
		/// Specifies values which control a swap chain's window association behavior.
		/// </summary>
		[System::Flags]
		public enum class WindowAssociationFlags : System::Int32
		{
			None = 0,
			IgnoreAll = DXGI_MWA_NO_WINDOW_CHANGES,
			IgnoreAltEnter = DXGI_MWA_NO_ALT_ENTER,
			IgnorePrintScreen = DXGI_MWA_NO_PRINT_SCREEN
		};
	}
}