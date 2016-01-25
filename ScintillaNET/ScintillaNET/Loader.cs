using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ScintillaNET
{
    internal sealed class Loader : ILoader
    {
        private readonly IntPtr self;
        private readonly NativeMethods.ILoaderVTable32 loader32;
        private readonly NativeMethods.ILoaderVTable64 loader64;
        private readonly Encoding encoding;

        public unsafe bool AddData(char[] data, int length)
        {
            if (data != null)
            {
                length = Helpers.Clamp(length, 0, data.Length);
                var bytes = Helpers.GetBytes(data, length, encoding, zeroTerminated: false);
                fixed (byte* bp = bytes)
                {
                    var status = (IntPtr.Size == 4 ? loader32.AddData(self, bp, bytes.Length) : loader64.AddData(self, bp, bytes.Length));
                    if (status != NativeMethods.SC_STATUS_OK)
                        return false;
                }
            }

            return true;
        }

        public Document ConvertToDocument()
        {
            var ptr = (IntPtr.Size == 4 ? loader32.ConvertToDocument(self) : loader64.ConvertToDocument(self));
            var document = new Document { Value = ptr };
            return document;
        }

        public int Release()
        {
            var count = (IntPtr.Size == 4 ? loader32.Release(self) : loader64.Release(self));
            return count;
        }

        public unsafe Loader(IntPtr ptr, Encoding encoding)
        {
            this.self = ptr;
            this.encoding = encoding;

            // http://stackoverflow.com/a/985820/2073621
            // http://stackoverflow.com/a/2094715/2073621
            // http://en.wikipedia.org/wiki/Virtual_method_table
            // http://www.openrce.org/articles/full_view/23
            // Because I know that I'm not going to remember all this... In C++, the first
            // variable of an object is a pointer (v[f]ptr) to the virtual table (v[f]table)
            // containing the addresses of each function. The first call below gets the vtable
            // address by following the object ptr to the vptr to the vtable. The second call
            // casts the vtable to a structure with the same memory layout so we can easily
            // invoke each function without having to do any pointer arithmetic. Depending on the
            // architecture, the function calling conventions can be different.

            IntPtr vfptr = *(IntPtr*)ptr;
            if(IntPtr.Size == 4)
                loader32 = (NativeMethods.ILoaderVTable32)Marshal.PtrToStructure(vfptr, typeof(NativeMethods.ILoaderVTable32));
            else
                loader64 = (NativeMethods.ILoaderVTable64)Marshal.PtrToStructure(vfptr, typeof(NativeMethods.ILoaderVTable64));
        }
    }
}
