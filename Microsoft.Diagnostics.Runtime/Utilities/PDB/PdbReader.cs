// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Collections.Generic;

namespace Microsoft.Diagnostics.Runtime.Utilities.Pdb
{
    /// <summary>
    /// An object that can map offsets in an IL stream to source locations and block scopes.
    /// </summary>
    public sealed class PdbReader :
      IDisposable
    {
        private IEnumerable<PdbSource> _sources;
        private Dictionary<uint, PdbFunction> _pdbFunctionMap = new Dictionary<uint, PdbFunction>();
        private List<StreamReader> _sourceFilesOpenedByReader = new List<StreamReader>();
        private int _ver;
        private int _sig;
        private int _age;
        private Guid _guid;

        /// <summary>
        /// Gets the properties of a given pdb.  Throws IOException on error.
        /// </summary>
        /// <param name="pdbFile">The pdb file to load.</param>
        /// <param name="signature">The signature of pdbFile.</param>
        /// <param name="age">The age of pdbFile.</param>
        public static void GetPdbProperties(string pdbFile, out Guid signature, out int age)
        {
            BitAccess bits = new BitAccess(512 * 1024);
            using (FileStream pdbStream = File.OpenRead(pdbFile))
            {
                PdbFileHeader header = new PdbFileHeader(pdbStream, bits);
                PdbStreamHelper reader = new PdbStreamHelper(pdbStream, header.PageSize);
                MsfDirectory dir = new MsfDirectory(reader, header, bits);

                dir._streams[1].Read(reader, bits);

                int ver, sig;
                bits.ReadInt32(out ver);    //  0..3  Version
                bits.ReadInt32(out sig);    //  4..7  Signature
                bits.ReadInt32(out age);    //  8..11 Age
                bits.ReadGuid(out signature);       // 12..27 GUID
            }
        }

        /// <summary>
        /// Allocates an object that can map some kinds of ILocation objects to IPrimarySourceLocation objects. 
        /// For example, a PDB reader that maps offsets in an IL stream to source locations.
        /// </summary>
        public PdbReader(Stream pdbStream)
        {
            Init(pdbStream);
        }

        /// <summary>
        /// Constructs a PdbReader from a path on disk.
        /// </summary>
        /// <param name="fileName">The pdb on disk to load.</param>
        public PdbReader(string fileName)
        {
            using (FileStream fs = File.OpenRead(fileName))
                Init(fs);
        }

        private void Init(Stream pdbStream)
        {
            foreach (PdbFunction pdbFunction in PdbFile.LoadFunctions(pdbStream, true, out _ver, out _sig, out _age, out _guid, out _sources))
                _pdbFunctionMap[pdbFunction.Token] = pdbFunction;
        }

        /// <summary>
        /// A collection of all sources in this pdb.
        /// </summary>
        public IEnumerable<PdbSource> Sources { get { return _sources; } }

        /// <summary>
        /// A collection of all functions in this pdb.
        /// </summary>
        internal IEnumerable<PdbFunction> Functions { get { return _pdbFunctionMap.Values; } }

        /// <summary>
        /// The version of this PDB.
        /// </summary>
        public int Version { get { return _ver; } }

        /// <summary>
        /// The Guid signature of this pdb.  Should be compared to the corresponding pdb signature in the matching PEFile.
        /// </summary>
        public Guid Signature { get { return _guid; } }

        /// <summary>
        /// The age of this pdb.  Should be compared to the corresponding pdb age in the matching PEFile.
        /// </summary>
        public int Age { get { return _age; } }

        /// <summary>
        /// Closes all of the source files that have been opened to provide the contents source locations corresponding to IL offsets.
        /// </summary>
        public void Dispose()
        {
            this.Close();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Closes all of the source files that have been opened to provide the contents source locations corresponding to IL offsets.
        /// </summary>
        ~PdbReader()
        {
            this.Close();
        }

        private void Close()
        {
            foreach (var source in _sourceFilesOpenedByReader)
                source.Dispose();
        }

        /// <summary>
        /// Retreives a PdbFunction by its metadata token.
        /// </summary>
        /// <param name="methodToken"></param>
        /// <returns></returns>
        public PdbFunction GetFunctionFromToken(uint methodToken)
        {
            PdbFunction result = null;
            _pdbFunctionMap.TryGetValue(methodToken, out result);
            return result;
        }
    }
}