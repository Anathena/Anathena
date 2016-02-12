using System;

namespace Anathema
{
    public interface FUTUREIOperatingSystemInterface
    {
        // Addressing
        IntPtr GetStackAddress();
        IntPtr[] GetHeapAddresses();

        // Reading
        dynamic Read(Type ElementType, IntPtr Address, out Boolean Success);
        T Read<T>(IntPtr Address, out Boolean Success);
        Byte[] ReadBytes(IntPtr Address, Int32 Count, out Boolean Success);

        // Writing
        void Write(Type ElementType, IntPtr Address, dynamic Value);
        void Write<T>(IntPtr Address, T Value);
        void WriteBytes(IntPtr Address, Byte[] Values);

    } // End interface

} // End namespace