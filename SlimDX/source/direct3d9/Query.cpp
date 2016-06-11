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

#include "../ComObject.h"

#include "Direct3D9Exception.h"
#include "Device.h"
#include "Query.h"
#include "BandwidthTimings.h"
#include "CacheUtilization.h"
#include "InterfaceTimings.h"
#include "PipelineTimings.h"
#include "ResourceManager.h"
#include "ResourceStats.h"
#include "StageTimings.h"
#include "VCache.h"
#include "VertexStats.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace SlimDX
{
namespace Direct3D9
{
	Query::Query( SlimDX::Direct3D9::Device^ device, QueryType type )
	{
		IDirect3DQuery9* query;
		HRESULT hr = device->InternalPointer->CreateQuery( static_cast<D3DQUERYTYPE>( type ), &query );
		
		if( RECORD_D3D9( hr ).IsFailure )
			throw gcnew Direct3D9Exception( "Failed to create Query." );

		Construct(query);
	}

	int Query::DataSize::get()
	{
		return InternalPointer->GetDataSize();
	}

	QueryType Query::Type::get()
	{
		return static_cast<QueryType>( InternalPointer->GetType() );
	}

	SlimDX::Direct3D9::Device^ Query::Device::get()
	{
		IDirect3DDevice9* device;
		HRESULT hr = InternalPointer->GetDevice( &device );
		
		if( RECORD_D3D9( hr ).IsFailure )
			return nullptr;

		return SlimDX::Direct3D9::Device::FromPointer( device );
	}

	Result Query::Issue( SlimDX::Direct3D9::Issue flags )
	{
		HRESULT hr = InternalPointer->Issue( static_cast<DWORD>( flags ) );
		return RECORD_D3D9( hr );
	}

	bool Query::CheckStatus( bool flush )
	{
		HRESULT hr = InternalPointer->GetData( NULL, 0, flush ? D3DGETDATA_FLUSH : 0 );
		
		switch( hr )
		{
		case S_OK:
			return true;
		case S_FALSE:
			return false;
			
		default:
			RECORD_D3D9( hr );
			return false;
		}
	}

	generic<typename T>
	T Query::GetData( bool flush )
	{
		HRESULT hr = S_OK;
		QueryType type = Type;

		switch( type )
		{
		case QueryType::BandwidthTimings:
			if( T::typeid != BandwidthTimings::typeid )
				hr = D3DERR_INVALIDCALL;
			break;

		case QueryType::CacheUtilization:
			if( T::typeid != CacheUtilization::typeid )
				hr = D3DERR_INVALIDCALL;
			break;

		case QueryType::Event:
			if( T::typeid != bool::typeid )
				hr = D3DERR_INVALIDCALL;
			break;

		case QueryType::InterfaceTimings:
			if( T::typeid != InterfaceTimings::typeid )
				hr = D3DERR_INVALIDCALL;
			break;

		case QueryType::Occlusion:
			if( T::typeid != Int32::typeid && T::typeid != UInt32::typeid )
				hr = D3DERR_INVALIDCALL;
			break;

		case QueryType::PipelineTimings:
			if( T::typeid != PipelineTimings::typeid )
				hr = D3DERR_INVALIDCALL;
			break;

		case QueryType::ResourceManager:
			if( T::typeid != ResourceManager::typeid )
				hr = D3DERR_INVALIDCALL;
			break;

		case QueryType::Timestamp:
			if( T::typeid != Int64::typeid && T::typeid != UInt64::typeid )
				hr = D3DERR_INVALIDCALL;
			break;

		case QueryType::TimestampDisjoint:
			if( T::typeid != bool::typeid )
				hr = D3DERR_INVALIDCALL;
			break;

		case QueryType::TimestampFreq:
			if( T::typeid != Int64::typeid && T::typeid != UInt64::typeid )
				hr = D3DERR_INVALIDCALL;
			break;

		case QueryType::VCache:
			if( T::typeid != VCache::typeid )
				hr = D3DERR_INVALIDCALL;
			break;

		case QueryType::VertexStats:
			if( T::typeid != VertexStats::typeid )
				hr = D3DERR_INVALIDCALL;
			break;

		case QueryType::VertexTimings:
			if( T::typeid != StageTimings::typeid )
				hr = D3DERR_INVALIDCALL;
			break;

		}

		RECORD_D3D9( hr );

		DWORD flags = flush ? D3DGETDATA_FLUSH : 0;

		//get the actual data now
		if( type == QueryType::Event || type == QueryType::TimestampDisjoint )
		{
			//need to marshal BOOL (int) to bool
			BOOL value = FALSE;
			hr = InternalPointer->GetData( &value, sizeof(BOOL), flags );
			RECORD_D3D9( hr );
			//we know that T is a bool, but the runtime does not
			return (T) (value > 0);
		}
		else
		{
			T data;
			hr = InternalPointer->GetData( &data, static_cast<DWORD>( sizeof(T) ), flags );
			RECORD_D3D9( hr );
			return data;
		}
	}
}
}
