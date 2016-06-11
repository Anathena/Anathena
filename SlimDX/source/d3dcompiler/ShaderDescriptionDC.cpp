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
#include "stdafx.h"

#include "ShaderDescriptionDC.h"

using namespace System;

namespace SlimDX
{
namespace D3DCompiler
{
	ShaderDescription::ShaderDescription( const D3D11_SHADER_DESC &desc )
	{
		Version = static_cast<ShaderVersion>(desc.Version);
		Creator = gcnew String(desc.Creator);
		Flags = static_cast<ShaderFlags>(desc.Flags);
		ConstantBuffers = desc.ConstantBuffers;
		BoundResources = desc.BoundResources;
		InputParameters = desc.InputParameters;
		OutputParameters = desc.OutputParameters;
		InstructionCount = desc.InstructionCount;
		TempRegisterCount = desc.TempRegisterCount;
		TempArrayCount = desc.TempArrayCount;
		DefineCount = desc.DefCount;
		DeclarationCount = desc.DclCount;
		TextureNormalInstructions = desc.TextureNormalInstructions;
		TextureLoadInstructions = desc.TextureLoadInstructions;
		TextureCompareInstructions = desc.TextureCompInstructions;
		TextureBiasInstructions = desc.TextureBiasInstructions;
		TextureGradientInstructions = desc.TextureGradientInstructions;
		FloatInstructionCount = desc.FloatInstructionCount;
		IntInstructionCount = desc.IntInstructionCount;
		UintInstructionCount = desc.UintInstructionCount;
		StaticFlowControlCount = desc.StaticFlowControlCount;
		DynamicFlowControlCount = desc.DynamicFlowControlCount;
		MacroInstructionCount = desc.MacroInstructionCount;
		ArrayInstructionCount = desc.ArrayInstructionCount;
		CutInstructionCount = desc.CutInstructionCount;
		EmitInstructionCount = desc.EmitInstructionCount;
		GeometryShaderOutputTopology = static_cast<Direct3D11::PrimitiveTopology>(desc.GSOutputTopology);
		MaximumOutputVertexCount = desc.GSMaxOutputVertexCount;
		InputPrimitive = static_cast<D3DCompiler::InputPrimitive>(desc.InputPrimitive);
		PatchConstantParameters = desc.PatchConstantParameters;
		GeometryShaderInstanceCount = desc.cGSInstanceCount;
		ControlPoints = desc.cControlPoints;
		HullShaderOutputPrimitive = static_cast<TessellatorOutputPrimitive>(desc.HSOutputPrimitive);
		HullShaderPartitioning = static_cast<TessellatorPartitioning>(desc.HSPartitioning);
		TessellatorDomain = static_cast<D3DCompiler::TessellatorDomain>(desc.TessellatorDomain);
		BarrierInstructions = desc.cBarrierInstructions;
		InterlockedInstructions = desc.cInterlockedInstructions;
		TextureStoreInstructions = desc.cTextureStoreInstructions;
	}
}
}