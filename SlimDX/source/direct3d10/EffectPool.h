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

#include "../d3dcompiler/EnumsDC.h"

#include "Enums.h"

using System::Runtime::InteropServices::OutAttribute;

namespace SlimDX
{
	namespace Direct3D10
	{
		ref class Device;
		ref class Effect;
		
		public ref class EffectPool : public ComObject
		{
			COMOBJECT(ID3D10EffectPool, EffectPool);

		private:
			static EffectPool^ FromMemory_Internal( Device^ device, const void* data, SIZE_T size, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags, [Out] System::String^ %compilationErrors );

		public:
			Effect^ AsEffect();
			
			static EffectPool^ FromFile( Device^ device, System::String^ fileName, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags );
			static EffectPool^ FromFile( Device^ device, System::String^ fileName, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags, [Out] System::String^ %compilationErrors );
			static EffectPool^ FromStream( Device^ device, System::IO::Stream^ stream, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags );
			static EffectPool^ FromStream( Device^ device, System::IO::Stream^ stream, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags, [Out] System::String^ %compilationErrors );
			static EffectPool^ FromMemory( Device^ device, array<System::Byte>^ memory, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags );
			static EffectPool^ FromMemory( Device^ device, array<System::Byte>^ memory, System::String^ profile, D3DCompiler::ShaderFlags shaderFlags, D3DCompiler::EffectFlags effectFlags, [Out] System::String^ %compilationErrors );
		};
	}
};