// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;

namespace Microsoft.Diagnostics.Runtime.Utilities.Pdb
{
    internal class DataStream
    {
        internal DataStream()
        {
        }

        internal DataStream(int contentSize, BitAccess bits, int count)
        {
            this._contentSize = contentSize;
            if (count > 0)
            {
                this._pages = new int[count];
                bits.ReadInt32(this._pages);
            }
        }

        internal void Read(PdbStreamHelper reader, BitAccess bits)
        {
            bits.MinCapacity(_contentSize);
            Read(reader, 0, bits.Buffer, 0, _contentSize);
        }

        internal void Read(PdbStreamHelper reader, int position,
                         byte[] bytes, int offset, int data)
        {
            if (position + data > _contentSize)
            {
                throw new PdbException("DataStream can't read off end of stream. " +
                                               "(pos={0},siz={1})",
                                       position, data);
            }
            if (position == _contentSize)
            {
                return;
            }

            int left = data;
            int page = position / reader.PageSize;
            int rema = position % reader.PageSize;

            // First get remained of first page.
            if (rema != 0)
            {
                int todo = reader.PageSize - rema;
                if (todo > left)
                {
                    todo = left;
                }

                reader.Seek(_pages[page], rema);
                reader.Read(bytes, offset, todo);

                offset += todo;
                left -= todo;
                page++;
            }

            // Now get the remaining pages.
            while (left > 0)
            {
                int todo = reader.PageSize;
                if (todo > left)
                {
                    todo = left;
                }

                reader.Seek(_pages[page], 0);
                reader.Read(bytes, offset, todo);

                offset += todo;
                left -= todo;
                page++;
            }
        }

        internal int Length
        {
            get { return _contentSize; }
        }


        internal int _contentSize;
        internal int[] _pages;
    }
}
