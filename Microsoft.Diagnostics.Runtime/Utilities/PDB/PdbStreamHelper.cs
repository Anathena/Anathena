// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;

namespace Microsoft.Diagnostics.Runtime.Utilities.Pdb
{
    internal class PdbStreamHelper
    {
        internal PdbStreamHelper(Stream reader, int pageSize)
        {
            this.PageSize = pageSize;
            this.Reader = reader;
        }

        internal void Seek(int page, int offset)
        {
            Reader.Seek(page * PageSize + offset, SeekOrigin.Begin);
        }

        internal void Read(byte[] bytes, int offset, int count)
        {
            Reader.Read(bytes, offset, count);
        }

        internal int PagesFromSize(int size)
        {
            return (size + PageSize - 1) / (PageSize);
        }

        internal readonly int PageSize;
        internal readonly Stream Reader;
    }
}
