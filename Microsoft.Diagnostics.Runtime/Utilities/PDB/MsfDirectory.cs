// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Microsoft.Diagnostics.Runtime.Utilities.Pdb
{
    internal class MsfDirectory
    {
        internal MsfDirectory(PdbStreamHelper reader, PdbFileHeader head, BitAccess bits)
        {
            int pages = reader.PagesFromSize(head.DirectorySize);

            // 0..n in page of directory pages.
            bits.MinCapacity(head.DirectorySize);
            int directoryRootPages = head.DirectoryRoot.Length;
            int pagesPerPage = head.PageSize / 4;
            int pagesToGo = pages;
            for (int i = 0; i < directoryRootPages; i++)
            {
                int pagesInThisPage = pagesToGo <= pagesPerPage ? pagesToGo : pagesPerPage;
                reader.Seek(head.DirectoryRoot[i], 0);
                bits.Append(reader.Reader, pagesInThisPage * 4);
                pagesToGo -= pagesInThisPage;
            }
            bits.Position = 0;

            DataStream stream = new DataStream(head.DirectorySize, bits, pages);
            bits.MinCapacity(head.DirectorySize);
            stream.Read(reader, bits);

            // 0..3 in directory pages
            int count;
            bits.ReadInt32(out count);

            // 4..n
            int[] sizes = new int[count];
            bits.ReadInt32(sizes);

            // n..m
            _streams = new DataStream[count];
            for (int i = 0; i < count; i++)
            {
                if (sizes[i] <= 0)
                {
                    _streams[i] = new DataStream();
                }
                else
                {
                    _streams[i] = new DataStream(sizes[i], bits,
                                                reader.PagesFromSize(sizes[i]));
                }
            }
        }

        internal DataStream[] _streams;
    }
}
