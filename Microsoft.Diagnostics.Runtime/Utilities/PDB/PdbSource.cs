// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Microsoft.Diagnostics.Runtime.Utilities.Pdb
{
    /// <summary>
    /// A source file in the program.
    /// </summary>
    public class PdbSource
    {
        /// <summary>
        /// The name of the source file.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// The DocType for this source.
        /// </summary>
        public Guid DocType { get; private set; }

        /// <summary>
        /// Pdb source language.
        /// </summary>
        public Guid Language { get; private set; }

        /// <summary>
        /// Pdb source vendor
        /// </summary>
        public Guid Vendor { get; private set; }

        /// <summary>
        /// Pdb algorithm id.
        /// </summary>
        public Guid AlgorithmId { get; private set; }

        /// <summary>
        /// Checksum for this pdb.
        /// </summary>
        public byte[] Checksum { get; private set; }

        /// <summary>
        /// The embeded source in this pdb.
        /// </summary>
        public byte[] Source { get; private set; }

        internal PdbSource(string name, Guid doctype, Guid language, Guid vendor, Guid algorithmId, byte[] checksum, byte[] source)
        {
            Name = name;
            DocType = doctype;
            Language = language;
            Vendor = vendor;
            AlgorithmId = algorithmId;
            Checksum = checksum;
            Source = source;
        }
    }
}
