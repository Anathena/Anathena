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

#undef COMOBJECT
#undef COMOBJECT_BASE
#undef COMOBJECT_CUSTOM

#define COMOBJECT_BASE(nativeType) \
	public: \
		static property System::Guid NativeInterface { System::Guid get() { return Utilities::ConvertNativeGuid( IID_ ## nativeType ); } } \
		property nativeType* InternalPointer { nativeType* get() new { return static_cast<nativeType*>( UnknownPointer ); } } \
	private:

// This macro provides the basic infrastructure for SlimDX ComObject subclasses. 
#define COMOBJECT(nativeType, managedType) \
	public protected: \
		managedType( nativeType* pointer, ComObject^ owner ) { Construct( pointer, owner ); } \
		managedType( System::IntPtr pointer ) { Construct( pointer, NativeInterface ); } \
		managedType( nativeType* pointer, ComObject^ owner, bool addToTable ) { Construct( pointer, owner, addToTable ); } \
	internal: \
		static managedType^ FromPointerReflectionThunk( System::IntPtr pointer ) { return FromPointer( static_cast<nativeType*>( pointer.ToPointer() ) ); } \
		static managedType^ FromPointerReflectionThunk( System::IntPtr pointer, ComObject^ owner ) { return FromPointer( static_cast<nativeType*>( pointer.ToPointer()), owner ); } \
		static managedType^ FromPointer( nativeType* pointer ) { return FromPointer( pointer, nullptr, ComObjectFlags::None ); } \
		static managedType^ FromPointer( nativeType* pointer, ComObject^ owner ) { return FromPointer( pointer, owner, ComObjectFlags::None ); } \
		static managedType^ FromPointer( nativeType* pointer, ComObject^ owner, ComObjectFlags flags ) { return ConstructFromPointer<managedType,nativeType>( pointer, owner, flags ); } \
	public: \
		static managedType^ FromPointer( System::IntPtr pointer ) { return ConstructFromUserPointer<managedType>( pointer ); } \
	COMOBJECT_BASE(nativeType)

// This macro provides the basic infrastructure for SlimDX ComObject subclasses, but allows
// the subclass to customize the behavior of the creation process for that subclass. This macro
// should be applied instead of the regular COMOBJECT() macro when such customization is required.
// The subclass must provide a body for the following methods:
//   * managedType( nativeType* pointer, ComObject^ owner )
//   * managedType( System::IntPtr pointer )
//   * managedType^ FromPointer( nativeType* pointer, ComObject^ owner, ComObjectFlags flags )
//   * managedType^ FromPointer( System::IntPtr pointer )
//
// Partial specialization is not supported; if the subclass needs special behavior for only
// a subset of the above methods, it must still implement all of them, copying the standard
// implementation from the COMOBJECT() macro for the appropriate non-specialized methods.
#define COMOBJECT_CUSTOM(nativeType, managedType) \
	public protected: \
		managedType( nativeType* pointer, ComObject^ owner ); \
		managedType( System::IntPtr pointer ); \
	internal: \
		static managedType^ FromPointerReflectionThunk( System::IntPtr pointer ) { return FromPointer( static_cast<nativeType*>( pointer.ToPointer() ) ); } \
		static managedType^ FromPointerReflectionThunk( System::IntPtr pointer, ComObject^ owner ) { return FromPointer( static_cast<nativeType*>( pointer.ToPointer()), owner ); } \
		static managedType^ FromPointer( nativeType* pointer ) { return FromPointer( pointer, nullptr, ComObjectFlags::None ); } \
		static managedType^ FromPointer( nativeType* pointer, ComObject^ owner ) { return FromPointer( pointer, owner, ComObjectFlags::None ); } \
		static managedType^ FromPointer( nativeType* pointer, ComObject^ owner, ComObjectFlags flags ); \
	public: \
		static managedType^ FromPointer( System::IntPtr pointer ); \
	COMOBJECT_BASE(nativeType)
