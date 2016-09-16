namespace SharpDX.XAPO
{
    using Multimedia;
    using System;
    using System.Runtime.InteropServices;

    public partial struct LockParameters
    {
        /// <summary>
        /// Gets or sets the waveformat.
        /// </summary>
        /// <value>The format.</value>
        public WaveFormat Format { get; set; }

        // Internal native struct used for marshalling
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal partial struct __Native
        {
            public IntPtr FormatPointer;
            public Int32 MaxFrameCount;
            internal unsafe void __MarshalFree()
            {
                if (FormatPointer != IntPtr.Zero)
                    Marshal.FreeCoTaskMem(FormatPointer);
            }
        }

        internal unsafe void __MarshalFree(ref __Native @ref)
        {
            @ref.__MarshalFree();
        }

        internal unsafe void __MarshalTo(ref __Native @ref)
        {
            @ref.FormatPointer = IntPtr.Zero;
            if (Format != null)
            {
                Int32 sizeOfFormat = Marshal.SizeOf(Format);
                @ref.FormatPointer = Marshal.AllocCoTaskMem(sizeOfFormat);
                Marshal.StructureToPtr(Format, @ref.FormatPointer, false);
            }
            @ref.MaxFrameCount = this.MaxFrameCount;
        }

        internal unsafe void __MarshalFrom(ref __Native @ref)
        {
            this.Format = null;
            this.FormatPointer = @ref.FormatPointer;
            if (this.FormatPointer != IntPtr.Zero)
                this.Format = WaveFormat.MarshalFrom(this.FormatPointer);
            this.MaxFrameCount = @ref.MaxFrameCount;
        }


        // Method to marshal from native to managed struct
        internal unsafe void __MarshalFrom(__Native* @ref)
        {
            this.Format = null;
            this.FormatPointer = @ref->FormatPointer;
            if (this.FormatPointer != IntPtr.Zero)
                this.Format = WaveFormat.MarshalFrom(this.FormatPointer);
            this.MaxFrameCount = @ref->MaxFrameCount;
        }
    }
    //// End class
}
//// End namespace