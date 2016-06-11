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
#include <vcclr.h>

#include "../ComObject.h"
#include "../Math/Matrix.h"
#include "../DataStream.h"
#include "../Utilities.h"
#include "../SlimDXException.h"

#include "Enums.h"
#include "Device.h"
#include "PixelShader9.h"
#include "VertexShader9.h"
#include "Texture.h"
#include "Effect9.h"
#include "EffectStateManager.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Direct3D9
{
	IEffectStateManagerShim::IEffectStateManagerShim( IEffectStateManager^ wrappedInterface, Effect^ effect )
	{
		m_WrappedInterface = wrappedInterface;
		m_Effect = effect;

		refCount = 1;
	}

	HRESULT IEffectStateManagerShim::QueryInterface( const IID &iid, LPVOID *ppv )
	{
		if( iid == IID_ID3DXEffectStateManager )
		{
			AddRef();
			*reinterpret_cast<ID3DXEffectStateManager**>( ppv ) = this;
			return S_OK;
		}

		return E_NOTIMPL;
	}

	ULONG IEffectStateManagerShim::AddRef()
	{
		return ++refCount;
	}

	ULONG IEffectStateManagerShim::Release()
	{
		if( --refCount == 0 )
			delete this;
		return refCount;
	}

	HRESULT IEffectStateManagerShim::LightEnable( DWORD Index, BOOL Enable )
	{
		try
		{
			m_WrappedInterface->EnableLight( Index, Enable > 0 );
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT IEffectStateManagerShim::SetFVF( DWORD FVF )
	{
		try
		{
			m_WrappedInterface->SetFVF( static_cast<VertexFormat>( FVF ) );
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT IEffectStateManagerShim::SetLight( DWORD index, const D3DLIGHT9* pLight )
	{
		try
		{
			Light light;
			light.Ambient = Color4::FromUnmanaged( pLight->Ambient );
			light.Diffuse = Color4::FromUnmanaged( pLight->Diffuse );
			light.Specular = Color4::FromUnmanaged( pLight->Specular );
			light.Attenuation0 = pLight->Attenuation0;
			light.Attenuation1 = pLight->Attenuation1;
			light.Attenuation2 = pLight->Attenuation2;
			light.Direction = Vector3( pLight->Direction.x, pLight->Direction.y, pLight->Direction.z );
			light.Position = Vector3( pLight->Position.x, pLight->Position.y, pLight->Position.z );
			light.Falloff = pLight->Falloff;
			light.Phi = pLight->Phi;
			light.Range = pLight->Range;
			light.Theta = pLight->Theta;
			light.Type = static_cast<LightType>( pLight->Type );

			m_WrappedInterface->SetLight( index, light );
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT IEffectStateManagerShim::SetMaterial( const D3DMATERIAL9* pMaterial )
	{
		try
		{
			Material material;
			material.Ambient = Color4::FromUnmanaged( pMaterial->Ambient );
			material.Specular = Color4::FromUnmanaged( pMaterial->Specular );
			material.Diffuse = Color4::FromUnmanaged( pMaterial->Diffuse );
			material.Emissive = Color4::FromUnmanaged( pMaterial->Emissive );
			material.Power = pMaterial->Power;

			m_WrappedInterface->SetMaterial( material );
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT IEffectStateManagerShim::SetNPatchMode( FLOAT nSegments )
	{
		try
		{
			m_WrappedInterface->SetNPatchMode( nSegments );
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT IEffectStateManagerShim::SetTransform( D3DTRANSFORMSTATETYPE State, const D3DMATRIX* pMatrix )
	{
		try
		{
			m_WrappedInterface->SetTransform( static_cast<TransformState>( State ), Matrix::FromD3DXMATRIX( *pMatrix ) );
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT IEffectStateManagerShim::SetPixelShader( LPDIRECT3DPIXELSHADER9 pShader )
	{
		try
		{
			if( pShader == NULL )
				m_WrappedInterface->SetPixelShader( nullptr );
			else
			{
				pShader->AddRef();
				m_WrappedInterface->SetPixelShader( PixelShader::FromPointer( pShader, m_Effect ) );
			}
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT IEffectStateManagerShim::SetPixelShaderConstantB( UINT StartRegister, const BOOL* pConstantData, UINT RegisterCount )
	{
		try
		{
			array<bool>^ data = gcnew array<bool>( RegisterCount );
			for( UINT i = 0; i < RegisterCount; i++ )
				data[i] = ( pConstantData[i] > 0 );

			m_WrappedInterface->SetPixelShaderConstant( StartRegister, data );
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT IEffectStateManagerShim::SetPixelShaderConstantF( UINT StartRegister, const FLOAT* pConstantData, UINT RegisterCount )
	{
		try
		{
			array<float>^ data = gcnew array<float>( RegisterCount * 4 );
			Marshal::Copy( IntPtr( const_cast<FLOAT*>( pConstantData ) ), data, 0, RegisterCount * 4 );

			m_WrappedInterface->SetPixelShaderConstant( StartRegister, data );
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT IEffectStateManagerShim::SetPixelShaderConstantI( UINT StartRegister, const INT* pConstantData, UINT RegisterCount )
	{
		try
		{
			array<int>^ data = gcnew array<int>( RegisterCount * 4 );
			Marshal::Copy( IntPtr( const_cast<INT*>( pConstantData ) ), data, 0, RegisterCount * 4 );

			m_WrappedInterface->SetPixelShaderConstant( StartRegister, data );
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT IEffectStateManagerShim::SetVertexShader( LPDIRECT3DVERTEXSHADER9 pShader )
	{
		try
		{
			if( pShader == NULL )
				m_WrappedInterface->SetVertexShader( nullptr );
			else
			{
				pShader->AddRef();
				m_WrappedInterface->SetVertexShader( VertexShader::FromPointer( pShader, m_Effect ) );
			}
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT IEffectStateManagerShim::SetVertexShaderConstantB( UINT StartRegister, const BOOL* pConstantData, UINT RegisterCount )
	{
		try
		{
			array<bool>^ data = gcnew array<bool>( RegisterCount );
			for( UINT i = 0; i < RegisterCount; i++ )
				data[i] = ( pConstantData[i] > 0 );

			m_WrappedInterface->SetVertexShaderConstant( StartRegister, data );
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT IEffectStateManagerShim::SetVertexShaderConstantF( UINT StartRegister, const FLOAT* pConstantData, UINT RegisterCount )
	{
		try
		{
			array<float>^ data = gcnew array<float>( RegisterCount * 4 );
			Marshal::Copy( IntPtr( const_cast<FLOAT*>( pConstantData ) ), data, 0, RegisterCount * 4 );

			m_WrappedInterface->SetVertexShaderConstant( StartRegister, data );
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT IEffectStateManagerShim::SetVertexShaderConstantI( UINT StartRegister, const INT* pConstantData, UINT RegisterCount )
	{
		try
		{
			array<int>^ data = gcnew array<int>( RegisterCount * 4 );
			Marshal::Copy( IntPtr( const_cast<INT*>( pConstantData ) ), data, 0, RegisterCount * 4 );

			m_WrappedInterface->SetVertexShaderConstant( StartRegister, data );
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT IEffectStateManagerShim::SetRenderState( D3DRENDERSTATETYPE State, DWORD Value )
	{
		try
		{
			m_WrappedInterface->SetRenderState( static_cast<RenderState>( State ), Value );
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT IEffectStateManagerShim::SetSamplerState( DWORD Sampler, D3DSAMPLERSTATETYPE Type, DWORD Value )
	{
		try
		{
			m_WrappedInterface->SetSamplerState( Sampler, static_cast<SamplerState>( Type ), Value );
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT IEffectStateManagerShim::SetTexture( DWORD Stage, LPDIRECT3DBASETEXTURE9 pTexture )
	{
		try
		{
			if( pTexture == NULL )
				m_WrappedInterface->SetTexture( Stage, nullptr );
			else
			{
				pTexture->AddRef();
				m_WrappedInterface->SetTexture( Stage, BaseTexture::FromUnmanaged( pTexture ) );
			}
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}

	HRESULT IEffectStateManagerShim::SetTextureStageState( DWORD Stage, D3DTEXTURESTAGESTATETYPE Type, DWORD Value )
	{
		try
		{
			m_WrappedInterface->SetTextureStageState( Stage, static_cast<TextureStage>( Type ), Value );
		}
		catch( SlimDXException^ ex)
		{
			return ex->ResultCode.Code;
		}
		catch( Exception^ )
		{
			return E_FAIL;
		}

		return S_OK;
	}
}
}