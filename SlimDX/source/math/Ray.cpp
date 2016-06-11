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

#include <d3dx9.h>

#include "BoundingBox.h"
#include "BoundingSphere.h"
#include "Plane.h"
#include "Ray.h"

using namespace System;
using namespace System::Globalization;

namespace SlimDX
{
	Ray::Ray( Vector3 position, Vector3 direction )
	{
		Position = position;
		Direction = direction;
	}

	bool Ray::Intersects( Ray ray, Plane plane, [Out] float% distance )
	{
		ray.Direction.Normalize();
		float dotDirection = (plane.Normal.X * ray.Direction.X) + (plane.Normal.Y * ray.Direction.Y) + (plane.Normal.Z * ray.Direction.Z);

		if( Math::Abs( dotDirection ) < 0.000001f )
		{
			distance = 0;
			return false;
		}

		float dotPosition = (plane.Normal.X * ray.Position.X) + (plane.Normal.Y * ray.Position.Y) + (plane.Normal.Z * ray.Position.Z);
		float num = ( -plane.D - dotPosition ) / dotDirection;

		if( num < 0.0f )
		{
			if( num < -0.000001f )
			{
				distance = 0;
				return false;
			}
			num = 0.0f;
		}

		distance = num;
		return true;
	}

	bool Ray::Intersects( Ray ray, Vector3 vertex1, Vector3 vertex2, Vector3 vertex3, [Out] float% distance )
	{
		float u, v;
		return Intersects( ray, vertex1, vertex2, vertex3, distance, u, v );
	}

	bool Ray::Intersects( Ray ray, Vector3 vertex1, Vector3 vertex2, Vector3 vertex3, [Out] float% distance, [Out] float% barycentricU, [Out] float% barycentricV )
	{
		pin_ptr<float> pinnedDist = &distance;
		pin_ptr<float> pinnedU = &barycentricU;
		pin_ptr<float> pinnedV = &barycentricV;

		if( D3DXIntersectTri( reinterpret_cast<D3DXVECTOR3*>( &vertex1 ), 
			reinterpret_cast<D3DXVECTOR3*>( &vertex2 ), reinterpret_cast<D3DXVECTOR3*>( &vertex3 ),
			reinterpret_cast<D3DXVECTOR3*>( &ray.Position ), reinterpret_cast<D3DXVECTOR3*>( &ray.Direction ),
			reinterpret_cast<FLOAT*>( pinnedU ), reinterpret_cast<FLOAT*>( pinnedV ), reinterpret_cast<FLOAT*>( pinnedDist ) ) )
			return true;
		else
			return false;
	}

	bool Ray::Intersects( Ray ray, BoundingBox box, [Out] float% distance )
	{
		float d = 0.0f;
		float maxValue = float::MaxValue;

		ray.Direction.Normalize();
		if( Math::Abs( ray.Direction.X ) < 0.0000001 )
		{
			if( ray.Position.X < box.Minimum.X || ray.Position.X > box.Maximum.X )
			{
				distance = 0.0f;
				return false;
			}
		}
		else
		{
			float inv = 1.0f / ray.Direction.X;
			float min = (box.Minimum.X - ray.Position.X) * inv;
			float max = (box.Maximum.X - ray.Position.X) * inv;

			if( min > max )
			{
				float temp = min;
				min = max;
				max = temp;
			}

			d = Math::Max( min, d );
			maxValue = Math::Min( max, maxValue );

			if( d > maxValue )
			{
				distance = 0.0f;
				return false;
			}
		}

		if( Math::Abs( ray.Direction.Y ) < 0.0000001 )
		{
			if( ray.Position.Y < box.Minimum.Y || ray.Position.Y > box.Maximum.Y )
			{
				distance = 0.0f;
				return false;
			}
		}
		else
		{
			float inv = 1.0f / ray.Direction.Y;
			float min = (box.Minimum.Y - ray.Position.Y) * inv;
			float max = (box.Maximum.Y - ray.Position.Y) * inv;

			if( min > max )
			{
				float temp = min;
				min = max;
				max = temp;
			}

			d = Math::Max( min, d );
			maxValue = Math::Min( max, maxValue );

			if( d > maxValue )
			{
				distance = 0.0f;
				return false;
			}
		}

		if( Math::Abs( ray.Direction.Z ) < 0.0000001 )
		{
			if( ray.Position.Z < box.Minimum.Z || ray.Position.Z > box.Maximum.Z )
			{
				distance = 0.0f;
				return false;
			}
		}
		else
		{
			float inv = 1.0f / ray.Direction.Z;
			float min = (box.Minimum.Z - ray.Position.Z) * inv;
			float max = (box.Maximum.Z - ray.Position.Z) * inv;

			if( min > max )
			{
				float temp = min;
				min = max;
				max = temp;
			}

			d = Math::Max( min, d );
			maxValue = Math::Min( max, maxValue );

			if( d > maxValue )
			{
				distance = 0.0f;
				return false;
			}
		}

		distance = d;
		return true;
	}

	bool Ray::Intersects( Ray ray, BoundingSphere sphere, [Out] float% distance )
	{
		float x = sphere.Center.X - ray.Position.X;
		float y = sphere.Center.Y - ray.Position.Y;
		float z = sphere.Center.Z - ray.Position.Z;
		float pyth = (x * x) + (y * y) + (z * z);
		float rr = sphere.Radius * sphere.Radius;

		if( pyth <= rr )
		{
			distance = 0.0f;
			return true;
		}

		ray.Direction.Normalize();
		float dot = (x * ray.Direction.X) + (y * ray.Direction.Y) + (z * ray.Direction.Z);
		if( dot < 0.0f )
		{
			distance = 0.0f;
			return false;
		}

		float temp = pyth - (dot * dot);
		if( temp > rr )
		{
			distance = 0.0f;
			return false;
		}

		distance = dot - static_cast<float>( Math::Sqrt( static_cast<double>( rr - temp ) ) );
		return true;
	}

	bool Ray::operator == ( Ray left, Ray right )
	{
		return Ray::Equals( left, right );
	}

	bool Ray::operator != ( Ray left, Ray right )
	{
		return !Ray::Equals( left, right );
	}

	String^ Ray::ToString()
	{
		return String::Format( CultureInfo::CurrentCulture, "Position:{0} Direction:{1}", Position.ToString(), Direction.ToString() );
	}

	int Ray::GetHashCode()
	{
		return Position.GetHashCode() + Direction.GetHashCode();
	}

	bool Ray::Equals( Object^ value )
	{
		if( value == nullptr )
			return false;

		if( value->GetType() != GetType() )
			return false;

		return Equals( safe_cast<Ray>( value ) );
	}

	bool Ray::Equals( Ray value )
	{
		return ( Position == value.Position && Direction == value.Direction );
	}

	bool Ray::Equals( Ray% value1, Ray% value2 )
	{
		return ( value1.Position == value2.Position && value1.Direction == value2.Direction );
	}
}