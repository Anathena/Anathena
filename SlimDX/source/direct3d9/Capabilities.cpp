#include "stdafx.h"
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
#include <d3d9.h>
#include <d3dx9.h>

#include "Capabilities.h"

using namespace System;

namespace SlimDX
{
namespace Direct3D9
{
	bool VertexShader20Caps::operator == ( VertexShader20Caps left, VertexShader20Caps right )
	{
		return VertexShader20Caps::Equals( left, right );
	}

	bool VertexShader20Caps::operator != ( VertexShader20Caps left, VertexShader20Caps right )
	{
		return !VertexShader20Caps::Equals( left, right );
	}

	int VertexShader20Caps::GetHashCode()
	{
		return Caps.GetHashCode() + DynamicFlowControlDepth.GetHashCode() + TempCount.GetHashCode()
			 + StaticFlowControlDepth.GetHashCode();
	}

	bool VertexShader20Caps::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<VertexShader20Caps>( value ) );
	}

	bool VertexShader20Caps::Equals( VertexShader20Caps value )
	{
		return ( Caps == value.Caps && DynamicFlowControlDepth == value.DynamicFlowControlDepth && TempCount == value.TempCount
			 && StaticFlowControlDepth == value.StaticFlowControlDepth );
	}

	bool VertexShader20Caps::Equals( VertexShader20Caps% value1, VertexShader20Caps% value2 )
	{
		return ( value1.Caps == value2.Caps && value1.DynamicFlowControlDepth == value2.DynamicFlowControlDepth && value1.TempCount == value2.TempCount
			 && value1.StaticFlowControlDepth == value2.StaticFlowControlDepth );
	}

	bool PixelShader20Caps::operator == ( PixelShader20Caps left, PixelShader20Caps right )
	{
		return PixelShader20Caps::Equals( left, right );
	}

	bool PixelShader20Caps::operator != ( PixelShader20Caps left, PixelShader20Caps right )
	{
		return !PixelShader20Caps::Equals( left, right );
	}

	int PixelShader20Caps::GetHashCode()
	{
		return Caps.GetHashCode() + DynamicFlowControlDepth.GetHashCode() + TempCount.GetHashCode()
			 + StaticFlowControlDepth.GetHashCode() + InstructionSlotCount.GetHashCode();
	}

	bool PixelShader20Caps::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<PixelShader20Caps>( value ) );
	}

	bool PixelShader20Caps::Equals( PixelShader20Caps value )
	{
		return ( Caps == value.Caps && DynamicFlowControlDepth == value.DynamicFlowControlDepth && TempCount == value.TempCount
			 && StaticFlowControlDepth == value.StaticFlowControlDepth &&
			 InstructionSlotCount == value.InstructionSlotCount );
	}

	bool PixelShader20Caps::Equals( PixelShader20Caps% value1, PixelShader20Caps% value2 )
	{
		return ( value1.Caps == value2.Caps && value1.DynamicFlowControlDepth == value2.DynamicFlowControlDepth && value1.TempCount == value2.TempCount
			 && value1.StaticFlowControlDepth == value2.StaticFlowControlDepth &&
			 value1.InstructionSlotCount == value2.InstructionSlotCount );
	}

	Capabilities::Capabilities( D3DCAPS9& caps )
	{
		DeviceType = static_cast<SlimDX::Direct3D9::DeviceType>( caps.DeviceType );
		AdapterOrdinal = caps.AdapterOrdinal;

		Caps = static_cast<SlimDX::Direct3D9::Caps>( caps.Caps );
		Caps2 = static_cast<SlimDX::Direct3D9::Caps2>( caps.Caps2 );
		Caps3 = static_cast<SlimDX::Direct3D9::Caps3>( caps.Caps3 );
		PresentationIntervals = static_cast<SlimDX::Direct3D9::PresentInterval>( caps.PresentationIntervals );

		CursorCaps = static_cast<SlimDX::Direct3D9::CursorCaps>( caps.CursorCaps );

		DeviceCaps = static_cast<SlimDX::Direct3D9::DeviceCaps>( caps.DevCaps );

		PrimitiveMiscCaps = static_cast<SlimDX::Direct3D9::PrimitiveMiscCaps>( caps.PrimitiveMiscCaps );
		RasterCaps = static_cast<SlimDX::Direct3D9::RasterCaps>( caps.RasterCaps );
		DepthCompareCaps = static_cast<CompareCaps>( caps.ZCmpCaps );
		SourceBlendCaps = static_cast<BlendCaps>( caps.SrcBlendCaps );
		DestinationBlendCaps = static_cast<BlendCaps>( caps.DestBlendCaps );
		AlphaCompareCaps = static_cast<CompareCaps>( caps.AlphaCmpCaps );
		ShadeCaps = static_cast<SlimDX::Direct3D9::ShadeCaps>( caps.ShadeCaps );
		TextureCaps = static_cast<SlimDX::Direct3D9::TextureCaps>( caps.TextureCaps );
		TextureFilterCaps = static_cast<FilterCaps>( caps.TextureFilterCaps );
		CubeTextureFilterCaps = static_cast<FilterCaps>( caps.CubeTextureFilterCaps );
		VolumeTextureFilterCaps = static_cast<FilterCaps>( caps.VolumeTextureFilterCaps );
		TextureAddressCaps = static_cast<SlimDX::Direct3D9::TextureAddressCaps>( caps.TextureAddressCaps );
		VolumeTextureAddressCaps = static_cast<SlimDX::Direct3D9::TextureAddressCaps>( caps.VolumeTextureAddressCaps );

		LineCaps = static_cast<SlimDX::Direct3D9::LineCaps>( caps.LineCaps );

		MaxTextureWidth = caps.MaxTextureWidth;
		MaxTextureHeight = caps.MaxTextureHeight;
		MaxVolumeExtent = caps.MaxVolumeExtent;

		MaxTextureRepeat = caps.MaxTextureRepeat;
		MaxTextureAspectRatio = caps.MaxTextureAspectRatio;
		MaxAnisotropy = caps.MaxAnisotropy;
		MaxVertexW = caps.MaxVertexW;

		GuardBandLeft = caps.GuardBandLeft;
		GuardBandTop = caps.GuardBandTop;
		GuardBandRight = caps.GuardBandRight;
		GuardBandBottom = caps.GuardBandBottom;

		ExtentsAdjust = caps.ExtentsAdjust;
		StencilCaps = static_cast<SlimDX::Direct3D9::StencilCaps>( caps.StencilCaps );

		FVFCaps = static_cast<VertexFormatCaps>( caps.FVFCaps );
		TextureOperationCaps = static_cast<SlimDX::Direct3D9::TextureOperationCaps>( caps.TextureOpCaps );
		MaxTextureBlendStages = caps.MaxTextureBlendStages;
		MaxSimultaneousTextures = caps.MaxSimultaneousTextures;

		VertexProcessingCaps = static_cast<SlimDX::Direct3D9::VertexProcessingCaps>( caps.VertexProcessingCaps );
		MaxActiveLights = caps.MaxActiveLights;
		MaxUserClipPlanes = caps.MaxUserClipPlanes;
		MaxVertexBlendMatrices = caps.MaxVertexBlendMatrices;
		MaxVertexBlendMatrixIndex = caps.MaxVertexBlendMatrixIndex;

		MaxPointSize = caps.MaxPointSize;

		MaxPrimitiveCount = caps.MaxPrimitiveCount;
		MaxVertexIndex = caps.MaxVertexIndex;
		MaxStreams = caps.MaxStreams;
		MaxStreamStride = caps.MaxStreamStride;

		VertexShaderVersion = gcnew Version( D3DSHADER_VERSION_MAJOR( caps.VertexShaderVersion ), 
			D3DSHADER_VERSION_MINOR( caps.VertexShaderVersion ) );
		MaxVertexShaderConstants = caps.MaxVertexShaderConst;

		PixelShaderVersion = gcnew Version( D3DSHADER_VERSION_MAJOR( caps.PixelShaderVersion ), 
			D3DSHADER_VERSION_MINOR( caps.PixelShaderVersion ) );
		PixelShader1xMaxValue = caps.PixelShader1xMaxValue;

		DeviceCaps2 = static_cast<SlimDX::Direct3D9::DeviceCaps2>( caps.DevCaps2 );

		MaxNPatchTessellationLevel = caps.MaxNpatchTessellationLevel;

		MasterAdapterOrdinal = caps.MasterAdapterOrdinal;
		AdapterOrdinalInGroup = caps.AdapterOrdinalInGroup;
		NumberOfAdaptersInGroup = caps.NumberOfAdaptersInGroup;
		DeclarationTypes = static_cast<DeclarationTypeCaps>( caps.DeclTypes );
		SimultaneousRTCount = caps.NumSimultaneousRTs;
		StretchRectFilterCaps = static_cast<FilterCaps>( caps.StretchRectFilterCaps );
		VS20Caps = *reinterpret_cast<VertexShader20Caps*>( &caps.VS20Caps );
		PS20Caps = *reinterpret_cast<PixelShader20Caps*>( &caps.PS20Caps );
		VertexTextureFilterCaps = static_cast<FilterCaps>( caps.VertexTextureFilterCaps );
		MaxVShaderInstructionsExecuted = caps.MaxVShaderInstructionsExecuted;
		MaxPShaderInstructionsExecuted = caps.MaxPShaderInstructionsExecuted;
		MaxVertexShader30InstructionSlots = caps.MaxVertexShader30InstructionSlots;
		MaxPixelShader30InstructionSlots = caps.MaxPixelShader30InstructionSlots;
	}

	bool Capabilities::operator == ( Capabilities^ left, Capabilities^ right )
	{
		if( ReferenceEquals( left, nullptr ) )
			return ReferenceEquals( right, nullptr );

		return Capabilities::Equals( left, right );
	}

	bool Capabilities::operator != ( Capabilities^ left, Capabilities^ right )
	{
		return !( left == right );
	}

	int Capabilities::GetHashCode()
	{
		return DeviceType.GetHashCode() + AdapterOrdinal.GetHashCode();
	}

	bool Capabilities::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Capabilities^>( value ) );
	}

	bool Capabilities::Equals( Capabilities^ value )
	{
		if( value == nullptr )
			return false;

		if( ReferenceEquals( this, value ) )
			return true;

		return ( DeviceType == value->DeviceType && AdapterOrdinal == value->AdapterOrdinal && Caps == value->Caps
			 && Caps2 == value->Caps2 && Caps3 == value->Caps3 && PresentationIntervals == value->PresentationIntervals
			 && CursorCaps == value->CursorCaps && DeviceCaps == value->DeviceCaps && PrimitiveMiscCaps == value->PrimitiveMiscCaps
			 && RasterCaps == value->RasterCaps && DepthCompareCaps == value->DepthCompareCaps && SourceBlendCaps == value->SourceBlendCaps
			 && DestinationBlendCaps == value->DestinationBlendCaps && AlphaCompareCaps == value->AlphaCompareCaps && ShadeCaps == value->ShadeCaps
			 && TextureCaps == value->TextureCaps && TextureFilterCaps == value->TextureFilterCaps && CubeTextureFilterCaps == value->CubeTextureFilterCaps
			 && VolumeTextureFilterCaps == value->VolumeTextureFilterCaps && TextureAddressCaps == value->TextureAddressCaps && VolumeTextureAddressCaps == value->VolumeTextureAddressCaps
			 && LineCaps == value->LineCaps && MaxTextureWidth == value->MaxTextureWidth && MaxTextureHeight == value->MaxTextureHeight
			 && MaxVolumeExtent == value->MaxVolumeExtent && MaxTextureRepeat == value->MaxTextureRepeat && MaxTextureAspectRatio == value->MaxTextureAspectRatio
			 && MaxAnisotropy == value->MaxAnisotropy && MaxVertexW == value->MaxVertexW && GuardBandLeft == value->GuardBandLeft
			 && GuardBandTop == value->GuardBandTop && GuardBandRight == value->GuardBandRight && GuardBandBottom == value->GuardBandBottom
			 && ExtentsAdjust == value->ExtentsAdjust && StencilCaps == value->StencilCaps && FVFCaps == value->FVFCaps
			 && TextureOperationCaps == value->TextureOperationCaps && MaxTextureBlendStages == value->MaxTextureBlendStages && MaxSimultaneousTextures == value->MaxSimultaneousTextures
			 && VertexProcessingCaps == value->VertexProcessingCaps && MaxActiveLights == value->MaxActiveLights && MaxUserClipPlanes == value->MaxUserClipPlanes
			 && MaxVertexBlendMatrices == value->MaxVertexBlendMatrices && MaxVertexBlendMatrixIndex == value->MaxVertexBlendMatrixIndex && MaxPointSize == value->MaxPointSize
			 && MaxPrimitiveCount == value->MaxPrimitiveCount && MaxVertexIndex == value->MaxVertexIndex && MaxStreams == value->MaxStreams
			 && MaxStreamStride == value->MaxStreamStride && VertexShaderVersion == value->VertexShaderVersion && MaxVertexShaderConstants == value->MaxVertexShaderConstants
			 && PixelShaderVersion == value->PixelShaderVersion && PixelShader1xMaxValue == value->PixelShader1xMaxValue && DeviceCaps2 == value->DeviceCaps2
			 && MaxNPatchTessellationLevel == value->MaxNPatchTessellationLevel && MasterAdapterOrdinal == value->MasterAdapterOrdinal && AdapterOrdinalInGroup == value->AdapterOrdinalInGroup
			 && NumberOfAdaptersInGroup == value->NumberOfAdaptersInGroup && DeclarationTypes == value->DeclarationTypes && SimultaneousRTCount == value->SimultaneousRTCount
			 && StretchRectFilterCaps == value->StretchRectFilterCaps && VS20Caps == value->VS20Caps && PS20Caps == value->PS20Caps
			 && VertexTextureFilterCaps == value->VertexTextureFilterCaps && MaxVShaderInstructionsExecuted == value->MaxVShaderInstructionsExecuted && MaxPShaderInstructionsExecuted == value->MaxPShaderInstructionsExecuted
			 && MaxVertexShader30InstructionSlots == value->MaxVertexShader30InstructionSlots && MaxPixelShader30InstructionSlots == value->MaxPixelShader30InstructionSlots );
	}

	bool Capabilities::Equals( Capabilities^ value1, Capabilities^ value2 )
	{
		return value1->Equals( value2 );
	}
}
}