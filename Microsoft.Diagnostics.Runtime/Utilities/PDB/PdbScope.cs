// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Microsoft.Diagnostics.Runtime.Utilities.Pdb
{
    /// <summary>
    /// Represents a scope within a function or class.
    /// </summary>
    public class PdbScope
    {
        /// <summary>
        /// A list of constants defined in this scope.
        /// </summary>
        public PdbConstant[] Constants { get; private set; }

        /// <summary>
        /// A list of variable slots in this function.
        /// </summary>
        public PdbSlot[] Slots { get; private set; }

        /// <summary>
        /// A list of sub-scopes within this scope.
        /// </summary>
        public PdbScope[] Scopes { get; private set; }

        /// <summary>
        /// A list of namespaces used in this scope.
        /// </summary>
        public string[] UsedNamespaces { get; private set; }

        /// <summary>
        /// The address of this scope.
        /// </summary>
        public uint Address { get; private set; }

        /// <summary>
        /// The IL offset of this scope.
        /// </summary>
        public uint Offset { get; private set; }

        /// <summary>
        /// The length of this scope.
        /// </summary>
        public uint Length { get; private set; }

        internal PdbScope(uint address, uint length, PdbSlot[] slots, PdbConstant[] constants, string[] usedNamespaces)
        {
            this.Constants = constants;
            this.Slots = slots;
            this.Scopes = new PdbScope[0];
            this.UsedNamespaces = usedNamespaces;
            this.Address = address;
            this.Offset = 0;
            this.Length = length;
        }

        internal PdbScope(uint funcOffset, BlockSym32 block, BitAccess bits, out uint typind)
        {
            //this.segment = block.seg;
            this.Address = block.off;
            this.Offset = block.off - funcOffset;
            this.Length = block.len;
            typind = 0;

            int constantCount;
            int scopeCount;
            int slotCount;
            int namespaceCount;
            PdbFunction.CountScopesAndSlots(bits, block.end, out constantCount, out scopeCount, out slotCount, out namespaceCount);
            Constants = new PdbConstant[constantCount];
            Scopes = new PdbScope[scopeCount];
            Slots = new PdbSlot[slotCount];
            UsedNamespaces = new string[namespaceCount];
            int constant = 0;
            int scope = 0;
            int slot = 0;
            int usedNs = 0;

            while (bits.Position < block.end)
            {
                ushort siz;
                ushort rec;

                bits.ReadUInt16(out siz);
                int star = bits.Position;
                int stop = bits.Position + siz;
                bits.Position = star;
                bits.ReadUInt16(out rec);

                switch ((SYM)rec)
                {
                    case SYM.S_BLOCK32:
                        {
                            BlockSym32 sub = new BlockSym32();

                            bits.ReadUInt32(out sub.parent);
                            bits.ReadUInt32(out sub.end);
                            bits.ReadUInt32(out sub.len);
                            bits.ReadUInt32(out sub.off);
                            bits.ReadUInt16(out sub.seg);
                            bits.SkipCString(out sub.name);

                            bits.Position = stop;
                            Scopes[scope++] = new PdbScope(funcOffset, sub, bits, out typind);
                            break;
                        }

                    case SYM.S_MANSLOT:
                        Slots[slot++] = new PdbSlot(bits, out typind);
                        bits.Position = stop;
                        break;

                    case SYM.S_UNAMESPACE:
                        bits.ReadCString(out UsedNamespaces[usedNs++]);
                        bits.Position = stop;
                        break;

                    case SYM.S_END:
                        bits.Position = stop;
                        break;

                    case SYM.S_MANCONSTANT:
                        Constants[constant++] = new PdbConstant(bits);
                        bits.Position = stop;
                        break;

                    default:
                        //throw new PdbException("Unknown SYM in scope {0}", (SYM)rec);
                        bits.Position = stop;
                        break;
                }
            }

            if (bits.Position != block.end)
            {
                throw new Exception("Not at S_END");
            }

            ushort esiz;
            ushort erec;
            bits.ReadUInt16(out esiz);
            bits.ReadUInt16(out erec);

            if (erec != (ushort)SYM.S_END)
            {
                throw new Exception("Missing S_END");
            }
        }
    }
}
