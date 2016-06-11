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

#include "Enums.h"

namespace SlimDX
{
	namespace Direct3D9
	{
		ref class Device;
		
		/// <summary>Represents a base class for all Direct3D resources.</summary>
		/// <unmanaged>IDirect3DResource9</unmanaged>
		public ref class Resource abstract : public ComObject
		{
			COMOBJECT_BASE(IDirect3DResource9);

		protected:
			/// <summary>
			/// Initializes a new instance of the <see cref="Resource"/> class.
			/// </summary>
			Resource() { }

		public:
			static Resource^ FromPointer( System::IntPtr pointer );

			/// <summary>
			/// Preloads a managed resource.
			/// </summary>
			void Preload();

			/// <summary>
			/// The resource has the lowest priority possible.
			/// </summary>
			literal int PriorityMinimum = D3D9_RESOURCE_PRIORITY_MINIMUM;

			/// <summary>
			/// The resource is scheduled with low priority.
			/// </summary>
			literal int PriorityLow = D3D9_RESOURCE_PRIORITY_LOW;

			/// <summary>
			/// The resource is scheduled with normal priority.
			/// </summary>
			literal int PriorityNormal = D3D9_RESOURCE_PRIORITY_NORMAL;

			/// <summary>
			/// The resource is scheduled with high priority.
			/// </summary>
			literal int PriorityHigh = D3D9_RESOURCE_PRIORITY_HIGH;

			/// <summary>
			/// The resource has the maximum priority possible.
			/// </summary>
			literal int PriorityMaximum = D3D9_RESOURCE_PRIORITY_MAXIMUM;

			/// <summary>
			/// Gets the device associated with the resource.
			/// </summary>
			property SlimDX::Direct3D9::Device^ Device
			{
				SlimDX::Direct3D9::Device^ get();
			}

			/// <summary>
			/// Gets or sets the resource's priority.
			/// </summary>
			property int Priority
			{
				int get() { return InternalPointer->GetPriority(); }
				void set(int value) { InternalPointer->SetPriority( value ); }
			};
		
			/// <summary>
			/// Gets the type of the resource.
			/// </summary>
			property SlimDX::Direct3D9::ResourceType ResourceType
			{
				SlimDX::Direct3D9::ResourceType get() { return ( SlimDX::Direct3D9::ResourceType ) InternalPointer->GetType(); }
			}

			/// <summary>
			/// Gets or sets a friendly debug name for the object. This name is used in debug messages
			/// as well as in the PIX object browser.
			/// </summary>
			property System::String^ DebugName
			{
				System::String^ get();
				void set(System::String^ value);
			}

			generic<typename T> where T : value class
			Result SetPrivateData(System::Guid guid, T data);

			generic<typename T> where T : value class
			T GetPrivateData(System::Guid guid);
		};
	}
}
