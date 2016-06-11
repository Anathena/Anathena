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

#include <vcclr.h>
#include "Device.h"
#include "BaseEffect.h"
#include "EffectStateManager.h"
#include "IResettable.h"
#include "Macro.h"
#include "Include.h"
#include "EffectPool9.h"

namespace SlimDX
{
	ref class DataStream;

	namespace Direct3D9
	{
		/// <summary>
		/// Represents a Direct3D rendering effect.
		/// </summary>
		/// <unmanaged>ID3DXEffect</unmanaged>
		public ref class Effect : public BaseEffect, public IResettable
		{
			COMOBJECT_CUSTOM(ID3DXEffect, Effect);

		private:
			IEffectStateManagerShim *shim;
			
			static Effect^ FromMemory_Internal( Device^ device, const void* memory, UINT size, array<Macro>^ preprocessorDefines, Include^ includeFile, System::String^ skipConstants, ShaderFlags flags, EffectPool^ pool, System::String^* compilationErrors, System::Exception^* exception );

		public:
			/// <summary>
			/// Releases all resources used by the <see cref="Effect"/>.
			/// </summary>
			virtual ~Effect() { if( shim != NULL ) shim->Release(); shim = NULL; }

			/// <summary>
			/// Creates an effect from a block of memory.
			/// </summary>
			/// <param name="device">The device used to create the effect.</param>
			/// <param name="memory">A block of memory containing the effect data.</param>
			/// <param name="preprocessorDefines">A set of preprocessor definitions.</param>
			/// <param name="includeFile">An include file handler.</param>
			/// <param name="skipConstants">A set of constants that will be ignored by the effect system.</param>
			/// <param name="flags">A set of flags describing effect creation options.</param>
			/// <param name="pool">The pool used for shared effect parameters.</param>
			/// <param name="compilationErrors">When the method completes, contains compilation errors and warnings.</param>
			/// <returns>The created effect object.</returns>
			static Effect^ FromMemory( Device^ device, array<System::Byte>^ memory, array<Macro>^ preprocessorDefines, Include^ includeFile, System::String^ skipConstants, ShaderFlags flags, EffectPool^ pool, [Out] System::String^ %compilationErrors );

			/// <summary>
			/// Creates an effect from a block of memory.
			/// </summary>
			/// <param name="device">The device used to create the effect.</param>
			/// <param name="memory">A block of memory containing the effect data.</param>
			/// <param name="preprocessorDefines">A set of preprocessor definitions.</param>
			/// <param name="includeFile">An include file handler.</param>
			/// <param name="skipConstants">A set of constants that will be ignored by the effect system.</param>
			/// <param name="flags">A set of flags describing effect creation options.</param>
			/// <param name="pool">The pool used for shared effect parameters.</param>
			/// <returns>The created effect object.</returns>
			static Effect^ FromMemory( Device^ device, array<System::Byte>^ memory, array<Macro>^ preprocessorDefines, Include^ includeFile, System::String^ skipConstants, ShaderFlags flags, EffectPool^ pool );

			/// <summary>
			/// Creates an effect from a block of memory.
			/// </summary>
			/// <param name="device">The device used to create the effect.</param>
			/// <param name="memory">A block of memory containing the effect data.</param>
			/// <param name="preprocessorDefines">A set of preprocessor definitions.</param>
			/// <param name="includeFile">An include file handler.</param>
			/// <param name="skipConstants">A set of constants that will be ignored by the effect system.</param>
			/// <param name="flags">A set of flags describing effect creation options.</param>
			/// <returns>The created effect object.</returns>
			static Effect^ FromMemory( Device^ device, array<System::Byte>^ memory, array<Macro>^ preprocessorDefines, Include^ includeFile, System::String^ skipConstants, ShaderFlags flags );

			/// <summary>
			/// Creates an effect from a block of memory.
			/// </summary>
			/// <param name="device">The device used to create the effect.</param>
			/// <param name="memory">A block of memory containing the effect data.</param>
			/// <param name="flags">A set of flags describing effect creation options.</param>
			/// <returns>The created effect object.</returns>
			static Effect^ FromMemory( Device^ device, array<System::Byte>^ memory, ShaderFlags flags );

			/// <summary>
			/// Creates an effect from a stream.
			/// </summary>
			/// <param name="device">The device used to create the effect.</param>
			/// <param name="stream">A stream containing the effect data.</param>
			/// <param name="preprocessorDefines">A set of preprocessor definitions.</param>
			/// <param name="includeFile">An include file handler.</param>
			/// <param name="skipConstants">A set of constants that will be ignored by the effect system.</param>
			/// <param name="flags">A set of flags describing effect creation options.</param>
			/// <param name="pool">The pool used for shared effect parameters.</param>
			/// <param name="compilationErrors">When the method completes, contains compilation errors and warnings.</param>
			/// <returns>The created effect object.</returns>
			static Effect^ FromStream( Device^ device, System::IO::Stream^ stream, array<Macro>^ preprocessorDefines, Include^ includeFile, System::String^ skipConstants, ShaderFlags flags, EffectPool^ pool, [Out] System::String^ %compilationErrors );

			/// <summary>
			/// Creates an effect from a stream.
			/// </summary>
			/// <param name="device">The device used to create the effect.</param>
			/// <param name="stream">A stream containing the effect data.</param>
			/// <param name="preprocessorDefines">A set of preprocessor definitions.</param>
			/// <param name="includeFile">An include file handler.</param>
			/// <param name="skipConstants">A set of constants that will be ignored by the effect system.</param>
			/// <param name="flags">A set of flags describing effect creation options.</param>
			/// <param name="pool">The pool used for shared effect parameters.</param>
			/// <returns>The created effect object.</returns>
			static Effect^ FromStream( Device^ device, System::IO::Stream^ stream, array<Macro>^ preprocessorDefines, Include^ includeFile, System::String^ skipConstants, ShaderFlags flags, EffectPool^ pool );

			/// <summary>
			/// Creates an effect from a stream.
			/// </summary>
			/// <param name="device">The device used to create the effect.</param>
			/// <param name="stream">A stream containing the effect data.</param>
			/// <param name="preprocessorDefines">A set of preprocessor definitions.</param>
			/// <param name="includeFile">An include file handler.</param>
			/// <param name="skipConstants">A set of constants that will be ignored by the effect system.</param>
			/// <param name="flags">A set of flags describing effect creation options.</param>
			/// <returns>The created effect object.</returns>
			static Effect^ FromStream( Device^ device, System::IO::Stream^ stream, array<Macro>^ preprocessorDefines, Include^ includeFile, System::String^ skipConstants, ShaderFlags flags );

			/// <summary>
			/// Creates an effect from a stream.
			/// </summary>
			/// <param name="device">The device used to create the effect.</param>
			/// <param name="stream">A stream containing the effect data.</param>
			/// <param name="flags">A set of flags describing effect creation options.</param>
			/// <returns>The created effect object.</returns>
			static Effect^ FromStream( Device^ device, System::IO::Stream^ stream, ShaderFlags flags );

			/// <summary>
			/// Creates an effect from a string.
			/// </summary>
			/// <param name="device">The device used to create the effect.</param>
			/// <param name="sourceData">A string containing the effect data.</param>
			/// <param name="preprocessorDefines">A set of preprocessor definitions.</param>
			/// <param name="includeFile">An include file handler.</param>
			/// <param name="skipConstants">A set of constants that will be ignored by the effect system.</param>
			/// <param name="flags">A set of flags describing effect creation options.</param>
			/// <param name="pool">The pool used for shared effect parameters.</param>
			/// <param name="compilationErrors">When the method completes, contains compilation errors and warnings.</param>
			/// <returns>The created effect object.</returns>
			static Effect^ FromString( Device^ device, System::String^ sourceData, array<Macro>^ preprocessorDefines, Include^ includeFile, System::String^ skipConstants, ShaderFlags flags, EffectPool^ pool, [Out] System::String^ %compilationErrors );

			/// <summary>
			/// Creates an effect from a string.
			/// </summary>
			/// <param name="device">The device used to create the effect.</param>
			/// <param name="sourceData">A string containing the effect data.</param>
			/// <param name="preprocessorDefines">A set of preprocessor definitions.</param>
			/// <param name="includeFile">An include file handler.</param>
			/// <param name="skipConstants">A set of constants that will be ignored by the effect system.</param>
			/// <param name="flags">A set of flags describing effect creation options.</param>
			/// <param name="pool">The pool used for shared effect parameters.</param>
			/// <returns>The created effect object.</returns>
			static Effect^ FromString( Device^ device, System::String^ sourceData, array<Macro>^ preprocessorDefines, Include^ includeFile, System::String^ skipConstants, ShaderFlags flags, EffectPool^ pool );

			/// <summary>
			/// Creates an effect from a string.
			/// </summary>
			/// <param name="device">The device used to create the effect.</param>
			/// <param name="sourceData">A string containing the effect data.</param>
			/// <param name="preprocessorDefines">A set of preprocessor definitions.</param>
			/// <param name="includeFile">An include file handler.</param>
			/// <param name="skipConstants">A set of constants that will be ignored by the effect system.</param>
			/// <param name="flags">A set of flags describing effect creation options.</param>
			/// <returns>The created effect object.</returns>
			static Effect^ FromString( Device^ device, System::String^ sourceData, array<Macro>^ preprocessorDefines, Include^ includeFile, System::String^ skipConstants, ShaderFlags flags );

			/// <summary>
			/// Creates an effect from a string.
			/// </summary>
			/// <param name="device">The device used to create the effect.</param>
			/// <param name="sourceData">A string containing the effect data.</param>
			/// <param name="flags">A set of flags describing effect creation options.</param>
			/// <returns>The created effect object.</returns>
			static Effect^ FromString( Device^ device, System::String^ sourceData, ShaderFlags flags );

			/// <summary>
			/// Creates an effect from a file.
			/// </summary>
			/// <param name="device">The device used to create the effect.</param>
			/// <param name="fileName">The name of the source file containing the effect data.</param>
			/// <param name="preprocessorDefines">A set of preprocessor definitions.</param>
			/// <param name="includeFile">An include file handler.</param>
			/// <param name="skipConstants">A set of constants that will be ignored by the effect system.</param>
			/// <param name="flags">A set of flags describing effect creation options.</param>
			/// <param name="pool">The pool used for shared effect parameters.</param>
			/// <param name="compilationErrors">When the method completes, contains compilation errors and warnings.</param>
			/// <returns>The created effect object.</returns>
			static Effect^ FromFile( Device^ device, System::String^ fileName, array<Macro>^ preprocessorDefines, Include^ includeFile, System::String^ skipConstants, ShaderFlags flags, EffectPool^ pool, [Out] System::String^ %compilationErrors );

			/// <summary>
			/// Creates an effect from a file.
			/// </summary>
			/// <param name="device">The device used to create the effect.</param>
			/// <param name="fileName">The name of the source file containing the effect data.</param>
			/// <param name="preprocessorDefines">A set of preprocessor definitions.</param>
			/// <param name="includeFile">An include file handler.</param>
			/// <param name="skipConstants">A set of constants that will be ignored by the effect system.</param>
			/// <param name="flags">A set of flags describing effect creation options.</param>
			/// <param name="pool">The pool used for shared effect parameters.</param>
			/// <returns>The created effect object.</returns>
			static Effect^ FromFile( Device^ device, System::String^ fileName, array<Macro>^ preprocessorDefines, Include^ includeFile, System::String^ skipConstants, ShaderFlags flags, EffectPool^ pool );

			/// <summary>
			/// Creates an effect from a file.
			/// </summary>
			/// <param name="device">The device used to create the effect.</param>
			/// <param name="fileName">The name of the source file containing the effect data.</param>
			/// <param name="preprocessorDefines">A set of preprocessor definitions.</param>
			/// <param name="includeFile">An include file handler.</param>
			/// <param name="skipConstants">A set of constants that will be ignored by the effect system.</param>
			/// <param name="flags">A set of flags describing effect creation options.</param>
			/// <returns>The created effect object.</returns>
			static Effect^ FromFile( Device^ device, System::String^ fileName, array<Macro>^ preprocessorDefines, Include^ includeFile, System::String^ skipConstants, ShaderFlags flags );

			/// <summary>
			/// Creates an effect from a file.
			/// </summary>
			/// <param name="device">The device used to create the effect.</param>
			/// <param name="fileName">The name of the source file containing the effect data.</param>
			/// <param name="flags">A set of flags describing effect creation options.</param>
			/// <returns>The created effect object.</returns>
			static Effect^ FromFile( Device^ device, System::String^ fileName, ShaderFlags flags );

			/// <summary>
			/// Creates a copy of the effect.
			/// </summary>
			/// <param name="device">The device associated with the effect.</param>
			/// <returns>The cloned effect.</returns>
			Effect^ Clone( Device^ device );

			/// <summary>
			/// Starts an active technique.
			/// </summary>
			/// <param name="flags">Rendering flags.</param>
			/// <returns>The number of passes required to complete the technique.</returns>
			int Begin( FX flags );

			/// <summary>
			/// Starts an active technique.
			/// </summary>
			/// <returns>The number of passes required to complete the technique.</returns>
			int Begin();

			/// <summary>
			/// Ends the active rendering technique.
			/// </summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result End();

			/// <summary>
			/// Begins an effect pass.
			/// </summary>
			/// <param name="pass">Index of the pass to start.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result BeginPass( int pass );

			/// <summary>
			/// Ends the current effect pass.
			/// </summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result EndPass();

			/// <summary>
			/// Starts capturing state changes in a parameter block.
			/// </summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result BeginParameterBlock();

			/// <summary>
			/// Stops capturing state changes.
			/// </summary>
			/// <returns>A handle to the parameter state block.</returns>
			EffectHandle^ EndParameterBlock();

			/// <summary>
			/// Applies a parameter state block to the effect.
			/// </summary>
			/// <param name="parameterBlock">Handle to the parameter block.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result ApplyParameterBlock( EffectHandle^ parameterBlock );

			/// <summary>
			/// Deletes a stored parameter state block.
			/// </summary>
			/// <param name="parameterBlock">Handle to the parameter block.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result DeleteParameterBlock( EffectHandle^ parameterBlock );

			/// <summary>
			/// Determines whether a parameter is used by a technique.
			/// </summary>
			/// <param name="parameter">Handle of the parameter.</param>
			/// <param name="technique">Handle of the technique.</param>
			/// <returns><c>true</c> if the parameter is used by the technique; otherwise, <c>false</c>.</returns>
			bool IsParameterUsed( EffectHandle^ parameter, EffectHandle^ technique );
			
			/// <summary>
			/// Propagates state changes that occur inside of an active pass to the device before rendering.
			/// </summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result CommitChanges();

			/// <summary>
			/// Searches for the next valid technique.
			/// </summary>
			/// <param name="technique">The starting point for the search.</param>
			/// <returns>The next valid technique.</returns>
			EffectHandle^ FindNextValidTechnique( EffectHandle^ technique );

			/// <summary>
			/// Validates a technique.
			/// </summary>
			/// <param name="technique">Handle of the technique.</param>
			/// <returns><c>true</c> if the technique is valid; otherwise, <c>false</c>.</returns>
			bool ValidateTechnique( EffectHandle^ technique );

			/// <summary>Called when the Direct3D device has been lost.</summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			virtual Result OnLostDevice();

			/// <summary>Ccalled when the Direct3D device has been reset.</summary>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			virtual Result OnResetDevice();

			/// <summary>
			/// Sets a contiguous range of shader constants.
			/// </summary>
			/// <param name="handle">Handle to the value to set.</param>
			/// <param name="data">The data used to set the handle.</param>
			/// <param name="offset">The starting offset into the data, in bytes.</param>
			/// <param name="count">The number of bytes of data to set.</param>
			/// <returns>A <see cref="SlimDX::Result"/> object describing the result of the operation.</returns>
			Result SetRawValue( EffectHandle^ handle, DataStream^ data, int offset, int count );

			Result SetRawValue( EffectHandle^ handle, DataStream^ data );
			Result SetRawValue( EffectHandle^ handle, array<float>^ data );
			Result SetRawValue( EffectHandle^ handle, array<float>^ data, int startIndex, int count );

			/// <summary>
			/// Disassembles the effect.
			/// </summary>		
			/// <param name="enableColorCode"><c>true</c> to enable color coding to make the disassembly easier to read, <c>false</c> to disable color coding.</param>
			/// <returns>
			/// A <see cref="SlimDX::DataStream"/> that contains the disassembled effect.
			/// </returns>
			DataStream^ Disassemble(bool enableColorCode);

			/// <summary>
			/// Gets or sets the currently active technique.
			/// </summary>
			property EffectHandle^ Technique
			{
				EffectHandle^ get();
				void set( EffectHandle^ value );
			}

			/// <summary>
			/// Gets or sets the effect state manager.
			/// </summary>
			property IEffectStateManager^ StateManager
			{
				IEffectStateManager^ get();
				void set( IEffectStateManager^ value );
			}

			/// <summary>
			/// Gets the device associated with the effect.
			/// </summary>
			property Device^ Device
			{
				SlimDX::Direct3D9::Device^ get();
			}

			/// <summary>
			/// Gets the pool of shared effect parameters.
			/// </summary>
			property EffectPool^ Pool
			{
				EffectPool^ get();
			}
		};
	}
}
