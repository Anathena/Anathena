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

#include "../d3dcompiler/IncludeDC.h"
#include "../d3dcompiler/ShaderMacroDC.h"
#include "../d3dcompiler/EnumsDC.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace Direct3D10
	{
		ref class Device;
		ref class EffectConstantBuffer;
		ref class EffectTechnique;
		ref class EffectVariable;
		ref class EffectPool;
		value class EffectDescription;
		
		public ref class Effect : public ComObject
		{
			COMOBJECT(ID3D10Effect, Effect);
		
		private:
			static Effect^ FromMemory_Internal( SlimDX::Direct3D10::Device^ device, const void* memory, SIZE_T size, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags, EffectPool^ pool, D3DCompiler::Include^ include, array<D3DCompiler::ShaderMacro>^ macros, System::String^* compilationErrors );

		public:
			property EffectDescription Description
			{
				EffectDescription get();
			}
			
			property bool IsOptimized
			{
				bool get();
			}
			
			property bool IsPooled
			{
				bool get();
			}
			
			property bool IsValid
			{
				bool get();
			}
			
			property Device^ Device
			{
				SlimDX::Direct3D10::Device^ get();
			}
			
			EffectConstantBuffer^ GetConstantBufferByIndex( int index );
			EffectConstantBuffer^ GetConstantBufferByName( System::String^ name );
			
			EffectTechnique^ GetTechniqueByIndex( int index );
			EffectTechnique^ GetTechniqueByName( System::String^ name );
			
			EffectVariable^ GetVariableByIndex( int index );
			EffectVariable^ GetVariableByName( System::String^ name );
			EffectVariable^ GetVariableBySemantic( System::String^ name );
			
			Result Optimize();
			
			static Effect^ FromFile( SlimDX::Direct3D10::Device^ device, System::String^ fileName, System::String^ profile );
			static Effect^ FromFile( SlimDX::Direct3D10::Device^ device, System::String^ fileName, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags );
			static Effect^ FromFile( SlimDX::Direct3D10::Device^ device, System::String^ fileName, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags, EffectPool^ pool, D3DCompiler::Include^ include, array<D3DCompiler::ShaderMacro>^ macros );
			static Effect^ FromFile( SlimDX::Direct3D10::Device^ device, System::String^ fileName, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags, EffectPool^ pool, D3DCompiler::Include^ include, array<D3DCompiler::ShaderMacro>^ macros, [Out] System::String^ %compilationErrors );
			
			static Effect^ FromMemory( SlimDX::Direct3D10::Device^ device, array<System::Byte>^ memory, System::String^ profile );
			static Effect^ FromMemory( SlimDX::Direct3D10::Device^ device, array<System::Byte>^ memory, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags );
			static Effect^ FromMemory( SlimDX::Direct3D10::Device^ device, array<System::Byte>^ memory, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags, EffectPool^ pool, D3DCompiler::Include^ include, array<D3DCompiler::ShaderMacro>^ macros );
			static Effect^ FromMemory( SlimDX::Direct3D10::Device^ device, array<System::Byte>^ memory, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags, EffectPool^ pool, D3DCompiler::Include^ include, array<D3DCompiler::ShaderMacro>^ macros, [Out] System::String^ %compilationErrors );
			
			static Effect^ FromStream( SlimDX::Direct3D10::Device^ device, System::IO::Stream^ stream, System::String^ profile );
			static Effect^ FromStream( SlimDX::Direct3D10::Device^ device, System::IO::Stream^ stream, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags );
			static Effect^ FromStream( SlimDX::Direct3D10::Device^ device, System::IO::Stream^ stream, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags, EffectPool^ pool, D3DCompiler::Include^ include, array<D3DCompiler::ShaderMacro>^ macros );
			static Effect^ FromStream( SlimDX::Direct3D10::Device^ device, System::IO::Stream^ stream, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags, EffectPool^ pool,D3DCompiler:: Include^ include, array<D3DCompiler::ShaderMacro>^ macros, [Out] System::String^ %compilationErrors );

			static Effect^ FromString( SlimDX::Direct3D10::Device^ device, System::String^ code, System::String^ profile );
			static Effect^ FromString( SlimDX::Direct3D10::Device^ device, System::String^ code, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags );
			static Effect^ FromString( SlimDX::Direct3D10::Device^ device, System::String^ code, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags, EffectPool^ pool, D3DCompiler::Include^ include, array<D3DCompiler::ShaderMacro>^ macros );
			static Effect^ FromString( SlimDX::Direct3D10::Device^ device, System::String^ code, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags, EffectPool^ pool, D3DCompiler::Include^ include, array<D3DCompiler::ShaderMacro>^ macros, [Out] System::String^ %compilationErrors );
		};
	}
};