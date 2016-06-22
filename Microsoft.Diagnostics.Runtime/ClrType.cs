// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Address = System.UInt64;

namespace Microsoft.Diagnostics.Runtime
{
    /// <summary>
    /// The architecture of a process.
    /// </summary>
    public enum Architecture
    {
        /// <summary>
        /// Unknown.  Should never be exposed except in case of error.
        /// </summary>
        Unknown,

        /// <summary>
        /// x86.
        /// </summary>
        X86,

        /// <summary>
        /// x64
        /// </summary>
        Amd64,

        /// <summary>
        /// ARM
        /// </summary>
        Arm
    }

    /// <summary>
    /// This is a representation of the metadata element type.  These values
    /// directly correspond with Clr's CorElementType.
    /// </summary>
    public enum ClrElementType
    {
        /// <summary>
        /// Not one of the other types.
        /// </summary>
        Unknown = 0x0,
        /// <summary>
        /// ELEMENT_TYPE_BOOLEAN
        /// </summary>
        Boolean = 0x2,
        /// <summary>
        /// ELEMENT_TYPE_CHAR
        /// </summary>
        Char = 0x3,

        /// <summary>
        /// ELEMENT_TYPE_I1
        /// </summary>
        Int8 = 0x4,

        /// <summary>
        /// ELEMENT_TYPE_U1
        /// </summary>
        UInt8 = 0x5,

        /// <summary>
        /// ELEMENT_TYPE_I2
        /// </summary>
        Int16 = 0x6,

        /// <summary>
        /// ELEMENT_TYPE_U2
        /// </summary>
        UInt16 = 0x7,

        /// <summary>
        /// ELEMENT_TYPE_I4
        /// </summary>
        Int32 = 0x8,

        /// <summary>
        /// ELEMENT_TYPE_U4
        /// </summary>
        UInt32 = 0x9,

        /// <summary>
        /// ELEMENT_TYPE_I8
        /// </summary>
        Int64 = 0xa,

        /// <summary>
        /// ELEMENT_TYPE_U8
        /// </summary>
        UInt64 = 0xb,

        /// <summary>
        /// ELEMENT_TYPE_R4
        /// </summary>
        Float = 0xc,

        /// <summary>
        /// ELEMENT_TYPE_R8
        /// </summary>
        Double = 0xd,

        /// <summary>
        /// ELEMENT_TYPE_STRING
        /// </summary>
        String = 0xe,

        /// <summary>
        /// ELEMENT_TYPE_PTR
        /// </summary>
        Pointer = 0xf,

        /// <summary>
        /// ELEMENT_TYPE_VALUETYPE
        /// </summary>
        Struct = 0x11,

        /// <summary>
        /// ELEMENT_TYPE_CLASS
        /// </summary>
        Class = 0x12,

        /// <summary>
        /// ELEMENT_TYPE_ARRAY
        /// </summary>
        Array = 0x14,

        /// <summary>
        /// ELEMENT_TYPE_I
        /// </summary>
        NativeInt = 0x18,

        /// <summary>
        /// ELEMENT_TYPE_U
        /// </summary>
        NativeUInt = 0x19,

        /// <summary>
        /// ELEMENT_TYPE_FNPTR
        /// </summary>
        FunctionPointer = 0x1B,

        /// <summary>
        /// ELEMENT_TYPE_OBJECT
        /// </summary>
        Object = 0x1C,

        /// <summary>
        /// ELEMENT_TYPE_SZARRAY
        /// </summary>
        SZArray = 0x1D,
    }


    /// <summary>
    /// An interface implementation in the target process.
    /// </summary>
    public abstract class ClrInterface
    {
        /// <summary>
        /// The typename of the interface.
        /// </summary>
        abstract public string Name { get; }

        /// <summary>
        /// The interface that this interface inherits from.
        /// </summary>
        abstract public ClrInterface BaseInterface { get; }

        /// <summary>
        /// Display string for this interface.
        /// </summary>
        /// <returns>Display string for this interface.</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Equals override.
        /// </summary>
        /// <param name="obj">Object to compare to.</param>
        /// <returns>True if this interface equals another.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ClrInterface))
                return false;

            ClrInterface rhs = (ClrInterface)obj;
            if (Name != rhs.Name)
                return false;

            if (BaseInterface == null)
            {
                return rhs.BaseInterface == null;
            }
            else
            {
                return BaseInterface.Equals(rhs.BaseInterface);
            }
        }

        /// <summary>
        /// GetHashCode override.
        /// </summary>
        /// <returns>A hashcode for this object.</returns>
        public override int GetHashCode()
        {
            int hashCode = 0;

            if (Name != null)
                hashCode ^= Name.GetHashCode();

            if (BaseInterface != null)
                hashCode ^= BaseInterface.GetHashCode();

            return hashCode;
        }
    }

    /// <summary>
    /// A representation of a type in the target process.
    /// </summary>
    public abstract class ClrType
    {
        /// <summary>
        /// Retrieves the first type handle in EnumerateMethodTables().  MethodTables
        /// are unique to an AppDomain/Type pair, so when there are multiple domains
        /// there will be multiple MethodTable for a class.
        /// </summary>
        abstract public ulong MethodTable { get; }

        /// <summary>
        /// Enumerates all MethodTable for this type in the process.  MethodTable
        /// are unique to an AppDomain/Type pair, so when there are multiple domains
        /// there may be multiple MethodTable.  Note that even if a type could be
        /// used in an AppDomain, that does not mean we actually have a MethodTable
        /// if the type hasn't been created yet.
        /// </summary>
        /// <returns>An enumeration of MethodTable in the process for this given
        /// type.</returns>
        abstract public IEnumerable<ulong> EnumerateMethodTables();

        /// <summary>
        /// Returns the metadata token of this type.
        /// </summary>
        abstract public uint MetadataToken { get; }

        /// <summary>
        /// Types have names.
        /// </summary>
        abstract public string Name { get; }
        /// <summary>
        /// GetSize returns the size in bytes for the total overhead of the object 'objRef'.   
        /// </summary>
        abstract public ulong GetSize(Address objRef);
        /// <summary>
        /// EnumeationRefsOfObject will call 'action' once for each object reference inside 'objRef'.  
        /// 'action' is passed the address of the outgoing refernece as well as an integer that
        /// represents the field offset.  While often this is the physical offset of the outgoing
        /// refernece, abstractly is simply something that can be given to GetFieldForOffset to 
        /// return the field information for that object reference  
        /// </summary>
        abstract public void EnumerateRefsOfObject(Address objRef, Action<Address, int> action);

        /// <summary>
        /// Does the same as EnumerateRefsOfObject, but does additional bounds checking to ensure
        /// we don't loop forever with inconsistent data.
        /// </summary>
        abstract public void EnumerateRefsOfObjectCarefully(Address objRef, Action<Address, int> action);

        /// <summary>
        /// Returns true if the type CAN contain references to other objects.  This is used in optimizations 
        /// and 'true' can always be returned safely.  
        /// </summary>
        virtual public bool ContainsPointers { get { return true; } }

        /// <summary>
        /// All types know the heap they belong to.  
        /// </summary>
        abstract public ClrHeap Heap { get; }

        /// <summary>
        /// Returns true if this object is a 'RuntimeType' (that is, the concrete System.RuntimeType class
        /// which is what you get when calling "typeof" in C#).
        /// </summary>
        virtual public bool IsRuntimeType { get { return false; } }

        /// <summary>
        /// Returns the concrete type (in the target process) that this RuntimeType represents.
        /// Note you may only call this function if IsRuntimeType returns true.
        /// </summary>
        /// <param name="obj">The RuntimeType object to get the concrete type for.</param>
        /// <returns>The underlying type that this RuntimeType actually represents.  May return null if the
        ///          underlying type has not been fully constructed by the runtime, or if the underlying type
        ///          is actually a typehandle (which unfortunately ClrMD cannot convert into a ClrType due to
        ///          limitations in the underlying APIs.  (So always null-check the return value of this
        ///          function.) </returns>
        virtual public ClrType GetRuntimeType(ulong obj) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns the module this type is defined in.
        /// </summary>
        virtual public ClrModule Module { get { return null; } }

        /// <summary>
        /// Returns a method based on its token.
        /// </summary>
        /// <param name="token">The token of the method to return.</param>
        /// <returns>A ClrMethod for the given token, null if no such methodDesc exists.</returns>
        internal virtual ClrMethod GetMethod(uint token) { return null; }

        /// <summary>
        /// Returns the ElementType of this Type.  Can return ELEMENT_TYPE_VOID on error.
        /// </summary>
        virtual public ClrElementType ElementType { get { return ClrElementType.Unknown; } internal set { throw new NotImplementedException(); } }

        /// <summary>
        /// Returns true if this type is a primitive (int, float, etc), false otherwise.
        /// </summary>
        /// <returns>True if this type is a primitive (int, float, etc), false otherwise.</returns>
        virtual public bool IsPrimitive { get { return ClrRuntime.IsPrimitive(ElementType); } }

        /// <summary>
        /// Returns true if this type is a ValueClass (struct), false otherwise.
        /// </summary>
        /// <returns>True if this type is a ValueClass (struct), false otherwise.</returns>
        virtual public bool IsValueClass { get { return ClrRuntime.IsValueClass(ElementType); } }

        /// <summary>
        /// Returns true if this type is an object reference, false otherwise.
        /// </summary>
        /// <returns>True if this type is an object reference, false otherwise.</returns>
        virtual public bool IsObjectReference { get { return ClrRuntime.IsObjectReference(ElementType); } }

        /// <summary>
        /// Returns the list of interfaces this type implements.
        /// </summary>
        abstract public IList<ClrInterface> Interfaces { get; }

        /// <summary>
        /// Returns true if the finalization is suppressed for an object.  (The user program called
        /// System.GC.SupressFinalize.  The behavior of this function is undefined if the object itself
        /// is not finalizable.
        /// </summary>
        virtual public bool IsFinalizeSuppressed(Address obj) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns whether objects of this type are finalizable.
        /// </summary>
        abstract public bool IsFinalizable { get; }

        // Visibility:
        /// <summary>
        /// Returns true if this type is marked Public.
        /// </summary>
        abstract public bool IsPublic { get; }

        /// <summary>
        /// returns true if this type is marked Private.
        /// </summary>
        abstract public bool IsPrivate { get; }

        /// <summary>
        /// Returns true if this type is accessable only by items in its own assembly.
        /// </summary>
        abstract public bool IsInternal { get; }

        /// <summary>
        /// Returns true if this nested type is accessable only by subtypes of its outer type.
        /// </summary>
        abstract public bool IsProtected { get; }

        // Other attributes:
        /// <summary>
        /// Returns true if this class is abstract.
        /// </summary>
        abstract public bool IsAbstract { get; }

        /// <summary>
        /// Returns true if this class is sealed.
        /// </summary>
        abstract public bool IsSealed { get; }

        /// <summary>
        /// Returns true if this type is an interface.
        /// </summary>
        abstract public bool IsInterface { get; }

        /// <summary>
        /// Returns all possible fields in this type.   It does not return dynamically typed fields.  
        /// Returns an empty list if there are no fields.
        /// </summary>
        virtual public IList<ClrInstanceField> Fields { get { return null; } }

        /// <summary>
        /// Returns a list of static fields on this type.  Returns an empty list if there are no fields.
        /// </summary>
        virtual public IList<ClrStaticField> StaticFields { get { return null; } }

        /// <summary>
        /// Returns a list of thread static fields on this type.  Returns an empty list if there are no fields.
        /// </summary>
        virtual public IList<ClrThreadStaticField> ThreadStaticFields { get { return null; } }

        /// <summary>
        /// Gets the list of methods this type implements.
        /// </summary>
        virtual public IList<ClrMethod> Methods { get { return null; } }

        /// <summary>
        /// When you enumerate a object, the offset within the object is returned.  This offset might represent
        /// nested fields (obj.Field1.Field2).    GetFieldOffset returns the first of these field (Field1), 
        /// and 'remaining' offset with the type of Field1 (which must be a struct type).   Calling 
        /// GetFieldForOffset repeatedly until the childFieldOffset is 0 will retrieve the whole chain.  
        /// </summary>
        /// <returns>true if successful.  Will fail if it 'this' is an array type</returns>
        abstract public bool GetFieldForOffset(int fieldOffset, bool inner, out ClrInstanceField childField, out int childFieldOffset);

        /// <summary>
        /// Returns the field given by 'name', case sensitive.  Returns NULL if no such field name exists (or on error).
        /// </summary>
        abstract public ClrInstanceField GetFieldByName(string name);

        /// <summary>
        /// Returns the field given by 'name', case sensitive.  Returns NULL if no such field name exists (or on error).
        /// </summary>
        abstract public ClrStaticField GetStaticFieldByName(string name);

        /// <summary>
        /// If this type inherits from another type, this is that type.  Can return null if it does not inherit (or is unknown)
        /// </summary>
        abstract public ClrType BaseType { get; }

        /// <summary>
        /// Returns true if the given object is a Com-Callable-Wrapper.  This is only supported in v4.5 and later.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if this is a CCW.</returns>
        virtual public bool IsCCW(Address obj) { return false; }

        /// <summary>
        /// Returns the CCWData for the given object.  Note you may only call this function if IsCCW returns true.
        /// </summary>
        /// <returns>The CCWData associated with the object, undefined result of obj is not a CCW.</returns>
        virtual public CcwData GetCCWData(Address obj)
        {
            return null;
        }

        /// <summary>
        /// Returns true if the given object is a Runtime-Callable-Wrapper.  This is only supported in v4.5 and later.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>True if this is an RCW.</returns>
        virtual public bool IsRCW(Address obj) { return false; }

        /// <summary>
        /// Returns the RCWData for the given object.  Note you may only call this function if IsRCW returns true.
        /// </summary>
        /// <returns>The RCWData associated with the object, undefined result of obj is not a RCW.</returns>
        virtual public RcwData GetRCWData(Address obj)
        {
            return null;
        }

        /// <summary>
        /// Indicates if the type is in fact a pointer. If so, the pointer operators
        /// may be used.
        /// </summary>
        virtual public bool IsPointer { get { return false; } }

        /// <summary>
        /// Gets the type of the element referenced by the pointer.
        /// </summary>
        virtual public ClrType ComponentType { get; internal set; }

        /// <summary>
        /// A type is an array if you can use the array operators below, Abstractly arrays are objects 
        /// that whose children are not statically known by just knowing the type.  
        /// </summary>
        virtual public bool IsArray { get { return false; } }

        /// <summary>
        /// If the type is an array, then GetArrayLength returns the number of elements in the array.  Undefined
        /// behavior if this type is not an array.
        /// </summary>
        abstract public int GetArrayLength(Address objRef);

        /// <summary>
        /// Returns the absolute address to the given array element.  You may then make a direct memory read out
        /// of the process to get the value if you want.
        /// </summary>
        abstract public Address GetArrayElementAddress(Address objRef, int index);

        /// <summary>
        /// Returns the array element value at the given index.  Returns 'null' if the array element is of type
        /// VALUE_CLASS.
        /// </summary>
        abstract public object GetArrayElementValue(Address objRef, int index);

        /// <summary>
        /// Returns the size of individual elements of an array.
        /// </summary>
        abstract public int ElementSize { get; }

        /// <summary>
        /// Returns the base size of the object.
        /// </summary>
        abstract public int BaseSize { get; }

        /// <summary>
        /// Returns true if this type is System.String.
        /// </summary>
        virtual public bool IsString { get { return false; } }

        /// <summary>
        /// Returns true if this type represents free space on the heap.
        /// </summary>
        virtual public bool IsFree { get { return false; } }

        /// <summary>
        /// Returns true if this type is an exception (that is, it derives from System.Exception).
        /// </summary>
        virtual public bool IsException { get { return false; } }

        /// <summary>
        /// Returns true if this type is an enum.
        /// </summary>
        virtual public bool IsEnum { get { return false; } }

        /// <summary>
        /// Returns the element type of this enum.
        /// </summary>
        virtual public ClrElementType GetEnumElementType() { throw new NotImplementedException(); }

        /// <summary>
        /// Returns a list of names in the enum.
        /// </summary>
        virtual public IEnumerable<string> GetEnumNames() { throw new NotImplementedException(); }

        /// <summary>
        /// Gets the name of the value in the enum, or null if the value doesn't have a name.
        /// This is a convenience function, and has undefined results if the same value appears
        /// twice in the enum.
        /// </summary>
        /// <param name="value">The value to lookup.</param>
        /// <returns>The name of one entry in the enum with this value, or null if none exist.</returns>
        virtual public string GetEnumName(object value) { throw new NotImplementedException(); }

        /// <summary>
        /// Gets the name of the value in the enum, or null if the value doesn't have a name.
        /// This is a convenience function, and has undefined results if the same value appears
        /// twice in the enum.
        /// </summary>
        /// <param name="value">The value to lookup.</param>
        /// <returns>The name of one entry in the enum with this value, or null if none exist.</returns>
        virtual public string GetEnumName(int value) { throw new NotImplementedException(); }

        /// <summary>
        /// Attempts to get the integer value for a given enum entry.  Note you should only call this function if
        /// GetEnumElementType returns ELEMENT_TYPE_I4.
        /// </summary>
        /// <param name="name">The name of the value to get (taken from GetEnumNames).</param>
        /// <param name="value">The value to write out.</param>
        /// <returns>True if we successfully filled value, false if 'name' is not a part of the enumeration.</returns>
        virtual public bool TryGetEnumValue(string name, out int value) { throw new NotImplementedException(); }

        /// <summary>
        /// Attempts to get the value for a given enum entry.  The type of "value" can be determined by the
        /// return value of GetEnumElementType.
        /// </summary>
        /// <param name="name">The name of the value to get (taken from GetEnumNames).</param>
        /// <param name="value">The value to write out.</param>
        /// <returns>True if we successfully filled value, false if 'name' is not a part of the enumeration.</returns>
        virtual public bool TryGetEnumValue(string name, out object value) { throw new NotImplementedException(); }

        /// <summary>
        /// Returns true if instances of this type have a simple value.
        /// </summary>
        virtual public bool HasSimpleValue { get { return false; } }

        /// <summary>
        /// Returns the simple value of an instance of this type.  Undefined behavior if HasSimpleValue returns false.
        /// For example ELEMENT_TYPE_I4 is an "int" and the return value of this function would be an int.
        /// </summary>
        /// <param name="address">The address of an instance of this type.</param>
        virtual public object GetValue(Address address) { return null; }

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// A representation of a field in the target process.
    /// </summary>
    public abstract class ClrField
    {
        /// <summary>
        /// The name of the field.
        /// </summary>
        abstract public string Name { get; }

        /// <summary>
        /// The type of the field.  Note this property may return null on error.  There is a bug in several versions
        /// of our debugging layer which causes this.  You should always null-check the return value of this field.
        /// </summary>
        abstract public ClrType Type { get; }

        /// <summary>
        /// Returns the element type of this field.  Note that even when Type is null, this should still tell you
        /// the element type of the field.
        /// </summary>
        abstract public ClrElementType ElementType { get; }

        /// <summary>
        /// Returns true if this field is a primitive (int, float, etc), false otherwise.
        /// </summary>
        /// <returns>True if this field is a primitive (int, float, etc), false otherwise.</returns>
        virtual public bool IsPrimitive { get { return ClrRuntime.IsPrimitive(ElementType); } }

        /// <summary>
        /// Returns true if this field is a ValueClass (struct), false otherwise.
        /// </summary>
        /// <returns>True if this field is a ValueClass (struct), false otherwise.</returns>
        virtual public bool IsValueClass { get { return ClrRuntime.IsValueClass(ElementType); } }

        /// <summary>
        /// Returns true if this field is an object reference, false otherwise.
        /// </summary>
        /// <returns>True if this field is an object reference, false otherwise.</returns>
        virtual public bool IsObjectReference { get { return ClrRuntime.IsObjectReference(ElementType); } }

        /// <summary>
        /// Gets the size of this field.
        /// </summary>
        abstract public int Size { get; }

        /// <summary>
        /// Returns true if this field is public.
        /// </summary>
        abstract public bool IsPublic { get; }

        /// <summary>
        /// Returns true if this field is private.
        /// </summary>
        abstract public bool IsPrivate { get; }

        /// <summary>
        /// Returns true if this field is internal.
        /// </summary>
        abstract public bool IsInternal { get; }

        /// <summary>
        /// Returns true if this field is protected.
        /// </summary>
        abstract public bool IsProtected { get; }

        /// <summary>
        /// Returns true if this field has a simple value (meaning you may call "GetFieldValue" in one of the subtypes
        /// of this class).
        /// </summary>
        abstract public bool HasSimpleValue { get; }

        /// <summary>
        /// If the field has a well defined offset from the base of the object, return it (otherwise -1). 
        /// </summary>
        virtual public int Offset { get { return -1; } }

        /// <summary>
        /// Returns a string representation of this object.
        /// </summary>
        /// <returns>A string representation of this object.</returns>
        public override string ToString()
        {
            var type = Type;
            if (type != null)
                return string.Format("{0} {1}", type.Name, Name);

            return Name;
        }
    }

    /// <summary>
    /// Represents an instance field of a type.   Fundamentally it respresents a name and a type 
    /// </summary>
    public abstract class ClrInstanceField : ClrField
    {
        /// <summary>
        /// Returns the value of this field.  Equivalent to GetFieldValue(objRef, false).
        /// </summary>
        /// <param name="objRef">The object to get the field value for.</param>
        /// <returns>The value of the field.</returns>
        virtual public object GetValue(Address objRef)
        {
            return GetValue(objRef, false, true);
        }

        /// <summary>
        /// Returns the value of this field, optionally specifying if this field is
        /// on a value class which is on the interior of another object.
        /// </summary>
        /// <param name="objRef">The object to get the field value for.</param>
        /// <param name="interior">Whether the enclosing type of this field is a value class,
        /// and that value class is embedded in another object.</param>
        /// <returns>The value of the field.</returns>
        virtual public object GetValue(Address objRef, bool interior)
        {
            return GetValue(objRef, interior, true);
        }

        /// <summary>
        /// Returns the value of this field, optionally specifying if this field is
        /// on a value class which is on the interior of another object.
        /// </summary>
        /// <param name="objRef">The object to get the field value for.</param>
        /// <param name="interior">Whether the enclosing type of this field is a value class,
        /// and that value class is embedded in another object.</param>
        /// <param name="convertStrings">When true, the value of a string field will be 
        /// returned as a System.String object; otherwise the address of the String object will be returned.</param>
        /// <returns>The value of the field.</returns>
        abstract public object GetValue(Address objRef, bool interior, bool convertStrings);

        /// <summary>
        /// Returns the address of the value of this field.  Equivalent to GetFieldAddress(objRef, false).
        /// </summary>
        /// <param name="objRef">The object to get the field address for.</param>
        /// <returns>The value of the field.</returns>
        virtual public Address GetAddress(Address objRef)
        {
            return GetAddress(objRef, false);
        }


        /// <summary>
        /// Returns the address of the value of this field.  Equivalent to GetFieldAddress(objRef, false).
        /// </summary>
        /// <param name="objRef">The object to get the field address for.</param>
        /// <param name="interior">Whether the enclosing type of this field is a value class,
        /// and that value class is embedded in another object.</param>
        /// <returns>The value of the field.</returns>
        abstract public Address GetAddress(Address objRef, bool interior);
    }

    /// <summary>
    /// Represents a static field in the target process.
    /// </summary>
    public abstract class ClrStaticField : ClrField
    {
        /// <summary>
        /// Returns whether this static field has been initialized in a particular AppDomain
        /// or not.  If a static variable has not been initialized, then its class constructor
        /// may have not been run yet.  Calling GetFieldValue on an uninitialized static
        /// will result in returning either NULL or a value of 0.
        /// </summary>
        /// <param name="appDomain">The AppDomain to see if the variable has been initialized.</param>
        /// <returns>True if the field has been initialized (even if initialized to NULL or a default
        /// value), false if the runtime has not initialized this variable.</returns>
        abstract public bool IsInitialized(ClrAppDomain appDomain);

        /// <summary>
        /// Gets the value of the static field.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the value.</param>
        /// <returns>The value of this static field.</returns>
        virtual public object GetValue(ClrAppDomain appDomain)
        {
            return GetValue(appDomain, true);
        }

        /// <summary>
        /// Gets the value of the static field.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the value.</param>
        /// <param name="convertStrings">When true, the value of a string field will be 
        /// returned as a System.String object; otherwise the address of the String object will be returned.</param>
        /// <returns>The value of this static field.</returns>
        abstract public object GetValue(ClrAppDomain appDomain, bool convertStrings);

        /// <summary>
        /// Returns the address of the static field's value in memory.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the field's address.</param>
        /// <returns>The address of the field's value.</returns>
        abstract public Address GetAddress(ClrAppDomain appDomain);

        /// <summary>
        /// Returns true if the static field has a default value (and if we can obtain it).
        /// </summary>
        virtual public bool HasDefaultValue { get { return false; } }

        /// <summary>
        /// The default value of the field.
        /// </summary>
        /// <returns>The default value of the field.</returns>
        virtual public object GetDefaultValue() { throw new NotImplementedException(); }
    }

    /// <summary>
    /// Represents a thread static value in the target process.
    /// </summary>
    public abstract class ClrThreadStaticField : ClrField
    {
        /// <summary>
        /// Gets the value of the field.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the field's value.</param>
        /// <param name="thread">The thread on which to get the field's value.</param>
        /// <returns>The value of the field.</returns>
        virtual public object GetValue(ClrAppDomain appDomain, ClrThread thread)
        {
            return GetValue(appDomain, thread, true);
        }

        /// <summary>
        /// Gets the value of the field.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the field's value.</param>
        /// <param name="thread">The thread on which to get the field's value.</param>
        /// <param name="convertStrings">When true, the value of a string field will be 
        /// returned as a System.String object; otherwise the address of the String object will be returned.</param>
        /// <returns>The value of the field.</returns>
        abstract public object GetValue(ClrAppDomain appDomain, ClrThread thread, bool convertStrings);

        /// <summary>
        /// Gets the address of the field.
        /// </summary>
        /// <param name="appDomain">The AppDomain in which to get the field's address.</param>
        /// <param name="thread">The thread on which to get the field's address.</param>
        /// <returns>The address of the field.</returns>
        abstract public Address GetAddress(ClrAppDomain appDomain, ClrThread thread);
    }

    /// <summary>
    /// A wrapper class for exception objects which help with common tasks for exception objects.
    /// Create this using GCHeap.GetExceptionObject.  You may call that when GCHeapType.IsException
    /// returns true.
    /// </summary>
    public abstract class ClrException
    {
        /// <summary>
        /// Returns the GCHeapType for this exception object.
        /// </summary>
        abstract public ClrType Type { get; }

        /// <summary>
        /// Returns the exception message.
        /// </summary>
        abstract public string Message { get; }

        /// <summary>
        /// Returns the address of the exception object.
        /// </summary>
        abstract public Address Address { get; }

        /// <summary>
        /// Returns the inner exception, if one exists, null otherwise.
        /// </summary>
        abstract public ClrException Inner { get; }

        /// <summary>
        /// Returns the HRESULT associated with this exception (or S_OK if there isn't one).
        /// </summary>
        abstract public int HResult { get; }

        /// <summary>
        /// Returns the StackTrace for this exception.  Note that this may be empty or partial depending
        /// on the state of the exception in the process.  (It may have never been thrown or we may be in
        /// the middle of constructing the stackwalk.)  This returns an empty list if no stack trace is
        /// associated with this exception object.
        /// </summary>
        abstract public IList<ClrStackFrame> StackTrace { get; }
    }
    /// <summary>
    /// The COM implementation details of a single CCW entry.
    /// </summary>
    public abstract class ComInterfaceData
    {
        /// <summary>
        /// The CLR type this represents.
        /// </summary>
        public abstract ClrType Type { get; }

        /// <summary>
        /// The interface pointer of Type.
        /// </summary>
        public abstract Address InterfacePointer { get; }
    }

    /// <summary>
    /// Helper for Com Callable Wrapper objects.  (CCWs are CLR objects exposed to native code as COM
    /// objects).
    /// </summary>
    public abstract class CcwData
    {
        /// <summary>
        /// Returns the pointer to the IUnknown representing this CCW.
        /// </summary>
        public abstract Address IUnknown { get; }

        /// <summary>
        /// Returns the pointer to the managed object representing this CCW.
        /// </summary>
        public abstract Address Object { get; }

        /// <summary>
        /// Returns the CLR handle associated with this CCW.
        /// </summary>
        public abstract Address Handle { get; }

        /// <summary>
        /// Returns the refcount of this CCW.
        /// </summary>
        public abstract int RefCount { get; }

        /// <summary>
        /// Returns the interfaces that this CCW implements.
        /// </summary>
        public abstract IList<ComInterfaceData> Interfaces { get; }
    }

    /// <summary>
    /// Helper for Runtime Callable Wrapper objects.  (RCWs are COM objects which are exposed to the runtime
    /// as managed objects.)
    /// </summary>
    public abstract class RcwData
    {
        /// <summary>
        /// Returns the pointer to the IUnknown representing this CCW.
        /// </summary>
        public abstract Address IUnknown { get; }

        /// <summary>
        /// Returns the external VTable associated with this RCW.  (It's useful to resolve the VTable as a symbol
        /// which will tell you what the underlying native type is...if you have the symbols for it loaded).
        /// </summary>
        public abstract Address VTablePointer { get; }

        /// <summary>
        /// Returns the RefCount of the RCW.
        /// </summary>
        public abstract int RefCount { get; }

        /// <summary>
        /// Returns the managed object associated with this of RCW.
        /// </summary>
        public abstract Address Object { get; }

        /// <summary>
        /// Returns true if the RCW is disconnected from the underlying COM type.
        /// </summary>
        public abstract bool Disconnected { get; }

        /// <summary>
        /// Returns the thread which created this RCW.
        /// </summary>
        public abstract uint CreatorThread { get; }

        /// <summary>
        /// Returns the internal WinRT object associated with this RCW (if one exists).
        /// </summary>
        public abstract ulong WinRTObject { get; }

        /// <summary>
        /// Returns the list of interfaces this RCW implements.
        /// </summary>
        public abstract IList<ComInterfaceData> Interfaces { get; }
    }

    /// <summary>
    /// The way a method was JIT'ed.
    /// </summary>
    public enum MethodCompilationType
    {
        /// <summary>
        /// Method is not yet JITed and no NGEN image exists.
        /// </summary>
        None,

        /// <summary>
        /// Method was JITed.
        /// </summary>
        Jit,

        /// <summary>
        /// Method was NGEN'ed (pre-JITed).
        /// </summary>
        Ngen
    }

    /// <summary>
    /// Represents a method on a class.
    /// </summary>
    public abstract class ClrMethod
    {
        /// <summary>
        /// Retrieves the first MethodDesc in EnumerateMethodDescs().  For single
        /// AppDomain programs this is the only MethodDesc.  MethodDescs
        /// are unique to an Method/AppDomain pair, so when there are multiple domains
        /// there will be multiple MethodDescs for a method.
        /// </summary>
        abstract public ulong MethodDesc { get; }

        /// <summary>
        /// Enumerates all method descs for this method in the process.  MethodDescs
        /// are unique to an Method/AppDomain pair, so when there are multiple domains
        /// there will be multiple MethodDescs for a method.
        /// </summary>
        /// <returns>An enumeration of method handles in the process for this given
        /// method.</returns>
        abstract public IEnumerable<ulong> EnumerateMethodDescs();

        /// <summary>
        /// The name of the method.  For example, "void System.Foo.Bar(object o, int i)" would return "Bar".
        /// </summary>
        abstract public string Name { get; }

        /// <summary>
        /// Returns the full signature of the function.  For example, "void System.Foo.Bar(object o, int i)"
        /// would return "System.Foo.Bar(System.Object, System.Int32)"
        /// </summary>
        abstract public string GetFullSignature();

        /// <summary>
        /// Returns the instruction pointer in the target process for the start of the method's assembly.
        /// </summary>
        abstract public Address NativeCode { get; }

        /// <summary>
        /// Gets the ILOffset of the given address within this method.
        /// </summary>
        /// <param name="addr">The absolute address of the code (not a relative offset).</param>
        /// <returns>The IL offset of the given address.</returns>
        abstract public int GetILOffset(ulong addr);

        /// <summary>
        /// Returns the way this method was compiled.
        /// </summary>
        abstract public MethodCompilationType CompilationType { get; }

        /// <summary>
        /// Returns the IL to native offset mapping.
        /// </summary>
        abstract public ILToNativeMap[] ILOffsetMap { get; }

        /// <summary>
        /// Returns the metadata token of the current method.
        /// </summary>
        abstract public uint MetadataToken { get; }

        /// <summary>
        /// Returns the enclosing type of this method.
        /// </summary>
        abstract public ClrType Type { get; }

        // Visibility:
        /// <summary>
        /// Returns if this method is public.
        /// </summary>
        abstract public bool IsPublic { get; }

        /// <summary>
        /// Returns if this method is private.
        /// </summary>
        abstract public bool IsPrivate { get; }

        /// <summary>
        /// Returns if this method is internal.
        /// </summary>
        abstract public bool IsInternal { get; }

        /// <summary>
        /// Returns if this method is protected.
        /// </summary>
        abstract public bool IsProtected { get; }

        // Attributes:
        /// <summary>
        /// Returns if this method is static.
        /// </summary>
        abstract public bool IsStatic { get; }
        /// <summary>
        /// Returns if this method is final.
        /// </summary>
        abstract public bool IsFinal { get; }
        /// <summary>
        /// Returns if this method is a PInvoke.
        /// </summary>
        abstract public bool IsPInvoke { get; }
        /// <summary>
        /// Returns if this method is a special method.
        /// </summary>
        abstract public bool IsSpecialName { get; }
        /// <summary>
        /// Returns if this method is runtime special method.
        /// </summary>
        abstract public bool IsRTSpecialName { get; }

        /// <summary>
        /// Returns if this method is virtual.
        /// </summary>
        abstract public bool IsVirtual { get; }
        /// <summary>
        /// Returns if this method is abstract.
        /// </summary>
        abstract public bool IsAbstract { get; }

        /// <summary>
        /// Returns whether this method is an instance constructor.
        /// </summary>
        virtual public bool IsConstructor { get { return Name == ".ctor"; } }

        /// <summary>
        /// Returns whether this method is a static constructor.
        /// </summary>
        virtual public bool IsClassConstructor { get { return Name == ".cctor"; } }
    }

    /// <summary>
    /// A method's mapping from IL to native offsets.
    /// </summary>
    public struct ILToNativeMap
    {
        /// <summary>
        /// The IL offset for this entry.
        /// </summary>
        public int ILOffset;

        /// <summary>
        /// The native start offset of this IL entry.
        /// </summary>
        public ulong StartAddress;

        /// <summary>
        /// The native end offset of this IL entry.
        /// </summary>
        public ulong EndAddress;

        /// <summary>
        /// To string.
        /// </summary>
        /// <returns>A visual display of the map entry.</returns>
        public override string ToString()
        {
            return string.Format("{0,2:X} - [{1:X}-{2:X}]", ILOffset, StartAddress, EndAddress);
        }

#pragma warning disable 0169
        /// <summary>
        /// Reserved.
        /// </summary>
        private int _reserved;
#pragma warning restore 0169
    }

}