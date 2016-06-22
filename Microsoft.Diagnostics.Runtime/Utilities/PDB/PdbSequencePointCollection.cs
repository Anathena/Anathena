// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Microsoft.Diagnostics.Runtime.Utilities.Pdb
{
    /// <summary>
    /// A collection of sequence points (usually for a single function).
    /// </summary>
    public class PdbSequencePointCollection
    {
        /// <summary>
        /// The source file these sequence points came from.
        /// </summary>
        public PdbSource File { get; private set; }

        /// <summary>
        /// A list of IL sequence points in this collection.
        /// </summary>
        public PdbSequencePoint[] Lines { get; private set; }

        internal PdbSequencePointCollection(PdbSource file, uint count)
        {
            File = file;
            Lines = new PdbSequencePoint[count];
        }
    }
}
