// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Diagnostics.Runtime.Utilities.Pdb
{
    /// <summary>
    /// Represents a sequence point (multiple lines) in a source file.
    /// </summary>
    public struct PdbSequencePoint
    {
        /// <summary>
        /// The IL offset of this line.
        /// </summary>
        public uint Offset { get; private set; }

        /// <summary>
        /// The first line of this sequence point.
        /// </summary>
        public uint LineBegin { get; private set; }

        /// <summary>
        /// The last line of this sequence point.
        /// </summary>
        public uint LineEnd { get; private set; }

        /// <summary>
        /// The first column of the first line of this sequence point.
        /// </summary>
        public ushort ColBegin { get; private set; }

        /// <summary>
        /// The last column of the last line of this sequence point.
        /// </summary>
        public ushort ColEnd { get; private set; }

        internal PdbSequencePoint(uint offset, uint lineBegin, ushort colBegin, uint lineEnd, ushort colEnd)
        {
            Offset = offset;
            LineBegin = lineBegin;
            ColBegin = colBegin;
            LineEnd = lineEnd;
            ColEnd = colEnd;
        }

        /// <summary>
        /// ToString override.
        /// </summary>
        public override string ToString()
        {
            if (LineBegin == LineEnd)
                return string.Format("iloffs: {0} line: {1}", Offset, LineBegin);

            return string.Format("iloffs: {0} lines: {1}-{2}", Offset, LineBegin, LineEnd);
        }
    }
}
