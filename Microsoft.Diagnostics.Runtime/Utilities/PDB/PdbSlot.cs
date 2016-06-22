// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Microsoft.Diagnostics.Runtime.Utilities.Pdb
{
    /// <summary>
    /// The representation of a local variable slot.
    /// </summary>
    public class PdbSlot
    {
        /// <summary>
        /// The slot number.
        /// </summary>
        public uint Slot { get; private set; }

        /// <summary>
        /// The name of this variable slot.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// the flags associated with this slot.
        /// </summary>
        public ushort Flags { get; private set; }
        //internal uint segment;
        //internal uint address;

        internal PdbSlot(BitAccess bits, out uint typind)
        {
            AttrSlotSym slot;

            bits.ReadUInt32(out slot.index);
            bits.ReadUInt32(out slot.typind);
            bits.ReadUInt32(out slot.offCod);
            bits.ReadUInt16(out slot.segCod);
            bits.ReadUInt16(out slot.flags);
            bits.ReadCString(out slot.name);

            Slot = slot.index;
            Name = slot.name;
            Flags = slot.flags;
            //Console.WriteLinesegment = slot.segCod;
            //Console.WriteLineaddress = slot.offCod;

            typind = slot.typind;
        }
    }
}
