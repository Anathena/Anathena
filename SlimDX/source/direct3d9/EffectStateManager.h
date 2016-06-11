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
	namespace Direct3D9
	{
		ref class Effect;

		public interface struct IEffectStateManager
		{
			virtual void EnableLight( int index, bool enable ) = 0;
			virtual void SetFVF( VertexFormat fvf ) = 0;
			virtual void SetLight( int index, Light light ) = 0;
			virtual void SetMaterial( Material material ) = 0;
			virtual void SetNPatchMode( float segments ) = 0;
			virtual void SetTransform( TransformState state, Matrix value ) = 0;

			virtual void SetPixelShader( PixelShader^ pixelShader ) = 0;
			virtual void SetPixelShaderConstant( int startRegister, array<bool>^ data ) = 0;
			virtual void SetPixelShaderConstant( int startRegister, array<float>^ data ) = 0;
			virtual void SetPixelShaderConstant( int startRegister, array<int>^ data ) = 0;

			virtual void SetVertexShader( VertexShader^ vertexShader ) = 0;
			virtual void SetVertexShaderConstant( int startRegister, array<bool>^ data ) = 0;
			virtual void SetVertexShaderConstant( int startRegister, array<float>^ data ) = 0;
			virtual void SetVertexShaderConstant( int startRegister, array<int>^ data ) = 0;

			virtual void SetRenderState( RenderState state, int value ) = 0;
			virtual void SetSamplerState( int sampler, SamplerState type, int value ) = 0;
			virtual void SetTexture( int sampler, BaseTexture^ texture ) = 0;
			virtual void SetTextureStageState( int stage, TextureStage type, int value ) = 0;
		};

		class IEffectStateManagerShim : public ID3DXEffectStateManager
		{
		private:
			int refCount;
			gcroot<IEffectStateManager^> m_WrappedInterface;
			gcroot<Effect^> m_Effect;

		public:
			IEffectStateManagerShim( IEffectStateManager^ wrappedInterface, Effect^ effect );

			IEffectStateManager^ GetManager() { return m_WrappedInterface; }

			HRESULT WINAPI QueryInterface( const IID &iid, LPVOID *ppv );
			ULONG   WINAPI AddRef();
			ULONG   WINAPI Release();

			HRESULT WINAPI LightEnable( DWORD Index, BOOL Enable );
			HRESULT WINAPI SetFVF( DWORD FVF );
			HRESULT WINAPI SetLight( DWORD index, const D3DLIGHT9* pLight );
			HRESULT WINAPI SetMaterial( const D3DMATERIAL9* pMaterial );
			HRESULT WINAPI SetNPatchMode( FLOAT nSegments );
			HRESULT WINAPI SetTransform( D3DTRANSFORMSTATETYPE State, const D3DMATRIX* pMatrix );

			HRESULT WINAPI SetPixelShader( LPDIRECT3DPIXELSHADER9 pShader );
			HRESULT WINAPI SetPixelShaderConstantB( UINT StartRegister, const BOOL* pConstantData, UINT RegisterCount );
			HRESULT WINAPI SetPixelShaderConstantF( UINT StartRegister, const FLOAT* pConstantData, UINT RegisterCount );
			HRESULT WINAPI SetPixelShaderConstantI( UINT StartRegister, const INT* pConstantData, UINT RegisterCount );

			HRESULT WINAPI SetVertexShader( LPDIRECT3DVERTEXSHADER9 pShader );
			HRESULT WINAPI SetVertexShaderConstantB( UINT StartRegister, const BOOL* pConstantData, UINT RegisterCount );
			HRESULT WINAPI SetVertexShaderConstantF( UINT StartRegister, const FLOAT* pConstantData, UINT RegisterCount );
			HRESULT WINAPI SetVertexShaderConstantI( UINT StartRegister, const INT* pConstantData, UINT RegisterCount );

			HRESULT WINAPI SetRenderState( D3DRENDERSTATETYPE State, DWORD Value );
			HRESULT WINAPI SetSamplerState( DWORD Sampler, D3DSAMPLERSTATETYPE Type, DWORD Value );
			HRESULT WINAPI SetTexture( DWORD Stage, LPDIRECT3DBASETEXTURE9 pTexture );
			HRESULT WINAPI SetTextureStageState( DWORD Stage, D3DTEXTURESTAGESTATETYPE Type, DWORD Value );
		};
	}
}
