// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Text;

#pragma warning disable 649

namespace Microsoft.Diagnostics.Runtime.Utilities
{
    /// <summary>
    /// PEFile is a reader for the information in a Portable Exectable (PE) FILE.   This is what EXEs and DLLs are.  
    /// 
    /// It can read both 32 and 64 bit PE files.  
    /// </summary>
    public unsafe sealed class PEFile : IDisposable
    {
        /// <summary>
        /// Parses a PEFile from a given stream. If it is valid, a new PEFile object is
        /// constructed and returned. Otherwise, null is returned.
        /// </summary>
        public static PEFile TryLoad(Stream stream, bool virt)
        {
            PEBuffer headerBuff = new PEBuffer(stream);
            PEHeader hdr = PEHeader.FromBuffer(headerBuff, virt);

            if (hdr == null)
                return null;

            PEFile pefile = new PEFile();
            pefile.Init(stream, "stream", virt, headerBuff, hdr);
            return pefile;
        }

        private PEFile()
        {
        }

        /// <summary>
        /// Create a new PEFile header reader.
        /// </summary>
        /// <param name="filePath">The path to the file on disk.</param>
        public PEFile(string filePath)
        {
            Init(File.OpenRead(filePath), filePath, false);
        }

        /// <summary>
        /// Constructor which allows you to specify a stream instead of file on disk.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <param name="virt">Whether the stream is currently in virtual memory (true)
        /// or if this reading from disk (false).</param>
        public PEFile(Stream stream, bool virt)
        {
            Init(stream, "stream", virt);
        }

        private void Init(Stream stream, string filePath, bool virt, PEBuffer buffer = null, PEHeader header = null)
        {
            if (buffer == null)
                buffer = new PEBuffer(stream);

            if (header == null)
                header = PEHeader.FromBuffer(buffer, virt);

            _virt = virt;
            _stream = stream;
            _headerBuff = buffer;
            Header = header;
            if (header != null && header.PEHeaderSize > _headerBuff.Length)
                throw new InvalidOperationException("Bad PE Header in " + filePath);
        }
        /// <summary>
        /// The Header for the PE file.  This contains the infor in a link /dump /headers 
        /// </summary>
        public PEHeader Header { get; private set; }

        /// <summary>
        /// Looks up the debug signature information in the EXE.   Returns true and sets the parameters if it is found. 
        /// 
        /// If 'first' is true then the first entry is returned, otherwise (by default) the last entry is used 
        /// (this is what debuggers do today).   Thus NGEN images put the IL PDB last (which means debuggers 
        /// pick up that one), but we can set it to 'first' if we want the NGEN PDB.
        /// </summary>
        public bool GetPdbSignature(out string pdbName, out Guid pdbGuid, out int pdbAge, bool first = false)
        {
            pdbName = null;
            pdbGuid = Guid.Empty;
            pdbAge = 0;
            bool ret = false;

            if (Header.DebugDirectory.VirtualAddress != 0)
            {
                var buff = AllocBuff();
                var debugEntries = (IMAGE_DEBUG_DIRECTORY*)FetchRVA(Header.DebugDirectory.VirtualAddress, Header.DebugDirectory.Size, buff);
                Debug.Assert(Header.DebugDirectory.Size % sizeof(IMAGE_DEBUG_DIRECTORY) == 0);
                int debugCount = Header.DebugDirectory.Size / sizeof(IMAGE_DEBUG_DIRECTORY);
                for (int i = 0; i < debugCount; i++)
                {
                    if (debugEntries[i].Type == IMAGE_DEBUG_TYPE.CODEVIEW)
                    {
                        var stringBuff = AllocBuff();
                        int ptr = _virt ? (int)debugEntries[i].AddressOfRawData : (int)debugEntries[i].PointerToRawData;
                        var info = (CV_INFO_PDB70*)stringBuff.Fetch(ptr, debugEntries[i].SizeOfData);
                        if (info->CvSignature == CV_INFO_PDB70.PDB70CvSignature)
                        {
                            // If there are several this picks the last one.  
                            pdbGuid = info->Signature;
                            pdbAge = info->Age;
                            pdbName = info->PdbFileName;
                            ret = true;
                            if (first)
                                break;
                        }
                        FreeBuff(stringBuff);
                    }
                }
                FreeBuff(buff);
            }
            return ret;
        }

        private PdbInfo _pdb;
        /// <summary>
        /// Holds information about the pdb for the current PEFile
        /// </summary>
        public PdbInfo PdbInfo
        {
            get
            {
                if (_pdb == null)
                {
                    string pdbName;
                    Guid pdbGuid;
                    int pdbAge;

                    if (GetPdbSignature(out pdbName, out pdbGuid, out pdbAge))
                        _pdb = new PdbInfo(pdbName, pdbGuid, pdbAge);
                }

                return _pdb;
            }
        }
        internal static bool TryGetIndexProperties(string filename, out int timestamp, out int filesize)
        {
            try
            {
                using (PEFile pefile = new PEFile(filename))
                {
                    var header = pefile.Header;
                    timestamp = header.TimeDateStampSec;
                    filesize = (int)header.SizeOfImage;
                    return true;
                }
            }
            catch
            {
                timestamp = 0;
                filesize = 0;
                return false;
            }
        }

        internal static bool TryGetIndexProperties(Stream stream, bool virt, out int timestamp, out int filesize)
        {
            try
            {
                using (PEFile pefile = new PEFile(stream, virt))
                {
                    var header = pefile.Header;
                    timestamp = header.TimeDateStampSec;
                    filesize = (int)header.SizeOfImage;
                    return true;
                }
            }
            catch
            {
                timestamp = 0;
                filesize = 0;
                return false;
            }
        }

        /// <summary>
        /// Whether this object has been disposed.
        /// </summary>
        public bool Disposed { get; private set; }

        /// <summary>
        /// Gets the File Version Information that is stored as a resource in the PE file.  (This is what the
        /// version tab a file's property page is populated with).  
        /// </summary>
        public FileVersionInfo GetFileVersionInfo()
        {
            var resources = GetResources();
            var versionNode = ResourceNode.GetChild(ResourceNode.GetChild(resources, "Version"), "1");
            if (versionNode == null)
                return null;
            if (!versionNode.IsLeaf && versionNode.Children.Count == 1)
                versionNode = versionNode.Children[0];


            var buff = AllocBuff();
            byte* bytes = versionNode.FetchData(0, versionNode.DataLength, buff);
            var ret = new FileVersionInfo(bytes, versionNode.DataLength);

            FreeBuff(buff);
            return ret;
        }
        /// <summary>
        /// For side by side dlls, the manifest that decribes the binding information is stored as the RT_MANIFEST resource, and it
        /// is an XML string.   This routine returns this.  
        /// </summary>
        /// <returns></returns>
        public string GetSxSManfest()
        {
            var resources = GetResources();
            var manifest = ResourceNode.GetChild(ResourceNode.GetChild(resources, "RT_MANIFEST"), "1");
            if (manifest == null)
                return null;
            if (!manifest.IsLeaf && manifest.Children.Count == 1)
                manifest = manifest.Children[0];

            var buff = AllocBuff();
            byte* bytes = manifest.FetchData(0, manifest.DataLength, buff);
            string ret = null;
            using (var stream = new UnmanagedMemoryStream(bytes, manifest.DataLength))
            using (var textReader = new StreamReader(stream))
                ret = textReader.ReadToEnd();
            FreeBuff(buff);
            return ret;
        }

        /// <summary>
        /// Closes any file handles and cleans up resources.  
        /// </summary>
        public void Dispose()
        {
            // This method can only be called once on a given object.  
            _stream.Close();
            _headerBuff.Dispose();
            if (_freeBuff != null)
                _freeBuff.Dispose();

            Disposed = true;
        }

        // TODO make public?
        internal ResourceNode GetResources()
        {
            if (Header.ResourceDirectory.VirtualAddress == 0 || Header.ResourceDirectory.Size < sizeof(IMAGE_RESOURCE_DIRECTORY))
                return null;
            var ret = new ResourceNode("", Header.FileOffsetOfResources, this, false, true);
            return ret;
        }

        #region private
        private PEBuffer _headerBuff;
        private PEBuffer _freeBuff;
        private Stream _stream;
        private bool _virt;

        internal byte* FetchRVA(int rva, int size, PEBuffer buffer)
        {
            int offset = Header.RvaToFileOffset(rva);
            return buffer.Fetch(offset, size);
        }

        internal IntPtr SafeFetchRVA(int rva, int size, PEBuffer buffer)
        {
            return new IntPtr(FetchRVA(rva, size, buffer));
        }


        internal PEBuffer AllocBuff()
        {
            var ret = _freeBuff;
            if (ret == null)
                return new PEBuffer(_stream);
            _freeBuff = null;
            return ret;
        }
        internal void FreeBuff(PEBuffer buffer)
        {
            _freeBuff = buffer;
        }
        #endregion
    };

    /// <summary>
    /// A PEHeader is a reader of the data at the begining of a PEFile.    If the header bytes of a 
    /// PEFile are read or mapped into memory, this class can parse it when given a pointer to it.
    /// It can read both 32 and 64 bit PE files.  
    /// </summary>
    public unsafe sealed class PEHeader
    {
        /// <summary>
        /// Parses the given buffer for the header of a PEFile. If it can be parsed correctly,
        /// a new PEHeader object is constructed from the buffer and returned. Otherwise, null
        /// is returned.
        /// </summary>
        internal static PEHeader FromBuffer(PEBuffer buffer, bool virt)
        {
            byte* ptr = buffer.Fetch(0, 0x300);
            var tmpDos = (IMAGE_DOS_HEADER*)ptr;
            int needed = tmpDos->e_lfanew + sizeof(IMAGE_NT_HEADERS);
            if (buffer.Length < needed)
            {
                ptr = buffer.Fetch(0, needed);

                if (buffer.Length < needed)
                    return null;

                tmpDos = (IMAGE_DOS_HEADER*)ptr;
            }

            IMAGE_NT_HEADERS* tmpNt = (IMAGE_NT_HEADERS*)((byte*)tmpDos + tmpDos->e_lfanew);
            needed += tmpNt->FileHeader.SizeOfOptionalHeader + sizeof(IMAGE_SECTION_HEADER) * tmpNt->FileHeader.NumberOfSections;

            if (buffer.Length < needed)
            {
                ptr = buffer.Fetch(0, needed);
                if (buffer.Length < needed)
                    return null;
            }

            return new PEHeader(buffer, virt);
        }


        private PEHeader(PEBuffer buffer, bool virt)
        {
            _virt = virt;

            byte* ptr = buffer.Fetch(0, 0x300);
            _dosHeader = (IMAGE_DOS_HEADER*)ptr;
            _ntHeader = (IMAGE_NT_HEADERS*)((byte*)ptr + _dosHeader->e_lfanew);
            _sections = (IMAGE_SECTION_HEADER*)(((byte*)_ntHeader) + sizeof(IMAGE_NT_HEADERS) + _ntHeader->FileHeader.SizeOfOptionalHeader);

            if (buffer.Length < PEHeaderSize)
                throw new BadImageFormatException();
        }

        /// <summary>
        /// The total s,ize of the header,  including section array of the the PE header.  
        /// </summary>
        public int PEHeaderSize
        {
            get
            {
                return VirtualAddressToRva(_sections) + sizeof(IMAGE_SECTION_HEADER) * _ntHeader->FileHeader.NumberOfSections;
            }
        }

        /// <summary>
        /// Given a virtual address to data in a mapped PE file, return the relative virtual address (displacement from start of the image)
        /// </summary>
        public int VirtualAddressToRva(void* ptr)
        {
            return (int)((byte*)ptr - (byte*)_dosHeader);
        }

        /// <summary>
        /// Given a relative virtual address (displacement from start of the image) return the virtual address to data in a mapped PE file
        /// </summary>
        public void* RvaToVirtualAddress(int rva)
        {
            return ((byte*)_dosHeader) + rva;
        }
        /// <summary>
        /// Given a relative virtual address (displacement from start of the image) return a offset in the file data for that data.  
        /// </summary>
        public int RvaToFileOffset(int rva)
        {
            if (_virt)
                return rva;

            for (int i = 0; i < _ntHeader->FileHeader.NumberOfSections; i++)
            {
                if (_sections[i].VirtualAddress <= rva && rva < _sections[i].VirtualAddress + _sections[i].VirtualSize)
                    return (int)_sections[i].PointerToRawData + (rva - (int)_sections[i].VirtualAddress);
            }
            throw new InvalidOperationException("Illegal RVA 0x" + rva.ToString("x"));
        }

        /// <summary>
        /// Given a relative virtual address (displacement from start of the image) return a offset in the file data for that data, if
        /// the RVA is valid. If the RVA is invalid, the method will return false. Otherwise, the method will return true and store the
        /// offset in the result parameter.
        /// </summary>
        public bool TryGetFileOffsetFromRva(int rva, out int result)
        {
            if (_virt)
            {
                result = rva;
                return true;
            }

            if (rva < (int)((byte*)_sections - (byte*)_dosHeader))
            {
                result = rva;
                return true;
            }

            for (int i = 0; i < _ntHeader->FileHeader.NumberOfSections; i++)
            {
                if (_sections[i].VirtualAddress <= rva && rva < _sections[i].VirtualAddress + _sections[i].VirtualSize)
                {
                    result = (int)_sections[i].PointerToRawData + (rva - (int)_sections[i].VirtualAddress);
                    return true;
                }
            }

            result = 0;
            return false;
        }

        /// <summary>
        /// Returns true if this is PE file for a 64 bit architecture.  
        /// </summary>
        public bool IsPE64 { get { return OptionalHeader32->Magic == 0x20b; } }
        /// <summary>
        /// Returns true if this file contains managed code (might also contain native code). 
        /// </summary>
        public bool IsManaged { get { return ComDescriptorDirectory.VirtualAddress != 0; } }

        // fields of code:IMAGE_NT_HEADERS
        /// <summary>   
        /// Returns the 'Signture' of the PE HEader PE\0\0 = 0x4550, used for sanity checking.  
        /// </summary>
        public uint Signature { get { return _ntHeader->Signature; } }

        // fields of code:IMAGE_FILE_HEADER
        /// <summary>
        /// The machine this PE file is intended to run on 
        /// </summary>
        public MachineType Machine { get { return (MachineType)_ntHeader->FileHeader.Machine; } }
        /// <summary>
        /// PE files have a number of sections that represent regions of memory with the access permisions.  This is the nubmer of such sections.  
        /// </summary>
        public ushort NumberOfSections { get { return _ntHeader->FileHeader.NumberOfSections; } }
        /// <summary>
        /// The the PE file was created represented as the number of seconds since Jan 1 1970 
        /// </summary>
        public int TimeDateStampSec { get { return (int)_ntHeader->FileHeader.TimeDateStamp; } }
        /// <summary>
        /// The the PE file was created represented as a DateTime object
        /// </summary>
        public DateTime TimeDateStamp
        {
            get
            {
                return TimeDateStampToDate(TimeDateStampSec);
            }
        }

        /// <summary>
        /// PointerToSymbolTable (see IMAGE_FILE_HEADER in PE File spec)
        /// </summary>
        public ulong PointerToSymbolTable { get { return _ntHeader->FileHeader.PointerToSymbolTable; } }
        /// <summary>
        /// NumberOfSymbols (see IMAGE_FILE_HEADER PE File spec)
        /// </summary>
        public ulong NumberOfSymbols { get { return _ntHeader->FileHeader.NumberOfSymbols; } }
        /// <summary>
        /// SizeOfOptionalHeader (see IMAGE_FILE_HEADER PE File spec)
        /// </summary>
        public ushort SizeOfOptionalHeader { get { return _ntHeader->FileHeader.SizeOfOptionalHeader; } }
        /// <summary>
        /// Characteristics (see IMAGE_FILE_HEADER PE File spec)
        /// </summary>
        public ushort Characteristics { get { return _ntHeader->FileHeader.Characteristics; } }

        // fields of code:IMAGE_OPTIONAL_HEADER32 (or code:IMAGE_OPTIONAL_HEADER64)
        /// <summary>
        /// Magic (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public ushort Magic { get { return OptionalHeader32->Magic; } }
        /// <summary>
        /// MajorLinkerVersion (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public byte MajorLinkerVersion { get { return OptionalHeader32->MajorLinkerVersion; } }
        /// <summary>
        /// MinorLinkerVersion (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public byte MinorLinkerVersion { get { return OptionalHeader32->MinorLinkerVersion; } }
        /// <summary>
        /// SizeOfCode (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public uint SizeOfCode { get { return OptionalHeader32->SizeOfCode; } }
        /// <summary>
        /// SizeOfInitializedData (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public uint SizeOfInitializedData { get { return OptionalHeader32->SizeOfInitializedData; } }
        /// <summary>
        /// SizeOfUninitializedData (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public uint SizeOfUninitializedData { get { return OptionalHeader32->SizeOfUninitializedData; } }
        /// <summary>
        /// AddressOfEntryPoint (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public uint AddressOfEntryPoint { get { return OptionalHeader32->AddressOfEntryPoint; } }
        /// <summary>
        /// BaseOfCode (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public uint BaseOfCode { get { return OptionalHeader32->BaseOfCode; } }

        // These depend on the whether you are PE32 or PE64
        /// <summary>
        /// ImageBase (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public ulong ImageBase { get { if (IsPE64) return OptionalHeader64->ImageBase; else return OptionalHeader32->ImageBase; } }
        /// <summary>
        /// SectionAlignment (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public uint SectionAlignment { get { if (IsPE64) return OptionalHeader64->SectionAlignment; else return OptionalHeader32->SectionAlignment; } }
        /// <summary>
        /// FileAlignment (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public uint FileAlignment { get { if (IsPE64) return OptionalHeader64->FileAlignment; else return OptionalHeader32->FileAlignment; } }
        /// <summary>
        /// MajorOperatingSystemVersion (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public ushort MajorOperatingSystemVersion { get { if (IsPE64) return OptionalHeader64->MajorOperatingSystemVersion; else return OptionalHeader32->MajorOperatingSystemVersion; } }
        /// <summary>
        /// MinorOperatingSystemVersion (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public ushort MinorOperatingSystemVersion { get { if (IsPE64) return OptionalHeader64->MinorOperatingSystemVersion; else return OptionalHeader32->MinorOperatingSystemVersion; } }
        /// <summary>
        /// MajorImageVersion (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public ushort MajorImageVersion { get { if (IsPE64) return OptionalHeader64->MajorImageVersion; else return OptionalHeader32->MajorImageVersion; } }
        /// <summary>
        /// MinorImageVersion (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public ushort MinorImageVersion { get { if (IsPE64) return OptionalHeader64->MinorImageVersion; else return OptionalHeader32->MinorImageVersion; } }
        /// <summary>
        /// MajorSubsystemVersion (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public ushort MajorSubsystemVersion { get { if (IsPE64) return OptionalHeader64->MajorSubsystemVersion; else return OptionalHeader32->MajorSubsystemVersion; } }
        /// <summary>
        /// MinorSubsystemVersion (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public ushort MinorSubsystemVersion { get { if (IsPE64) return OptionalHeader64->MinorSubsystemVersion; else return OptionalHeader32->MinorSubsystemVersion; } }
        /// <summary>
        /// Win32VersionValue (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public uint Win32VersionValue { get { if (IsPE64) return OptionalHeader64->Win32VersionValue; else return OptionalHeader32->Win32VersionValue; } }
        /// <summary>
        /// SizeOfImage (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public uint SizeOfImage { get { if (IsPE64) return OptionalHeader64->SizeOfImage; else return OptionalHeader32->SizeOfImage; } }
        /// <summary>
        /// SizeOfHeaders (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public uint SizeOfHeaders { get { if (IsPE64) return OptionalHeader64->SizeOfHeaders; else return OptionalHeader32->SizeOfHeaders; } }
        /// <summary>
        /// CheckSum (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public uint CheckSum { get { if (IsPE64) return OptionalHeader64->CheckSum; else return OptionalHeader32->CheckSum; } }
        /// <summary>
        /// Subsystem (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public ushort Subsystem { get { if (IsPE64) return OptionalHeader64->Subsystem; else return OptionalHeader32->Subsystem; } }
        /// <summary>
        /// DllCharacteristics (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public ushort DllCharacteristics { get { if (IsPE64) return OptionalHeader64->DllCharacteristics; else return OptionalHeader32->DllCharacteristics; } }
        /// <summary>
        /// SizeOfStackReserve (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public ulong SizeOfStackReserve { get { if (IsPE64) return OptionalHeader64->SizeOfStackReserve; else return OptionalHeader32->SizeOfStackReserve; } }
        /// <summary>
        /// SizeOfStackCommit (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public ulong SizeOfStackCommit { get { if (IsPE64) return OptionalHeader64->SizeOfStackCommit; else return OptionalHeader32->SizeOfStackCommit; } }
        /// <summary>
        /// SizeOfHeapReserve (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public ulong SizeOfHeapReserve { get { if (IsPE64) return OptionalHeader64->SizeOfHeapReserve; else return OptionalHeader32->SizeOfHeapReserve; } }
        /// <summary>
        /// SizeOfHeapCommit (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public ulong SizeOfHeapCommit { get { if (IsPE64) return OptionalHeader64->SizeOfHeapCommit; else return OptionalHeader32->SizeOfHeapCommit; } }
        /// <summary>
        /// LoaderFlags (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public uint LoaderFlags { get { if (IsPE64) return OptionalHeader64->LoaderFlags; else return OptionalHeader32->LoaderFlags; } }
        /// <summary>
        /// NumberOfRvaAndSizes (see IMAGE_OPTIONAL_HEADER32 or IMAGE_OPTIONAL_HEADER64 in PE File spec)
        /// </summary>
        public uint NumberOfRvaAndSizes { get { if (IsPE64) return OptionalHeader64->NumberOfRvaAndSizes; else return OptionalHeader32->NumberOfRvaAndSizes; } }

        // Well known data blobs (directories)  
        /// <summary>
        /// returns the data directory (virtual address an blob, of a data directory with index 'idx'.   14 are currently defined.
        /// </summary>
        public IMAGE_DATA_DIRECTORY Directory(int idx)
        {
            if (idx >= NumberOfRvaAndSizes)
                return new IMAGE_DATA_DIRECTORY();
            return ntDirectories[idx];
        }
        /// <summary>
        /// Return the data directory for DLL Exports see PE file spec for more
        /// </summary>
        public IMAGE_DATA_DIRECTORY ExportDirectory { get { return Directory(0); } }
        /// <summary>
        /// Return the data directory for DLL Imports see PE file spec for more
        /// </summary>
        public IMAGE_DATA_DIRECTORY ImportDirectory { get { return Directory(1); } }
        /// <summary>
        /// Return the data directory for DLL Resources see PE file spec for more
        /// </summary>
        public IMAGE_DATA_DIRECTORY ResourceDirectory { get { return Directory(2); } }
        /// <summary>
        /// Return the data directory for DLL Exceptions see PE file spec for more
        /// </summary>
        public IMAGE_DATA_DIRECTORY ExceptionDirectory { get { return Directory(3); } }
        /// <summary>
        /// Return the data directory for DLL securiy certificates (Authenticode) see PE file spec for more
        /// </summary>
        public IMAGE_DATA_DIRECTORY CertificatesDirectory { get { return Directory(4); } }
        /// <summary>
        /// Return the data directory Image Base Relocations (RELOCS) see PE file spec for more
        /// </summary>
        public IMAGE_DATA_DIRECTORY BaseRelocationDirectory { get { return Directory(5); } }
        /// <summary>
        /// Return the data directory for Debug information see PE file spec for more
        /// </summary>
        public IMAGE_DATA_DIRECTORY DebugDirectory { get { return Directory(6); } }
        /// <summary>
        /// Return the data directory for DLL Exports see PE file spec for more
        /// </summary>
        public IMAGE_DATA_DIRECTORY ArchitectureDirectory { get { return Directory(7); } }
        /// <summary>
        /// Return the data directory for GlobalPointer (IA64) see PE file spec for more
        /// </summary>
        public IMAGE_DATA_DIRECTORY GlobalPointerDirectory { get { return Directory(8); } }
        /// <summary>
        /// Return the data directory for THread local storage see PE file spec for more
        /// </summary>
        public IMAGE_DATA_DIRECTORY ThreadStorageDirectory { get { return Directory(9); } }
        /// <summary>
        /// Return the data directory for Load Configuration see PE file spec for more
        /// </summary>
        public IMAGE_DATA_DIRECTORY LoadConfigurationDirectory { get { return Directory(10); } }
        /// <summary>
        /// Return the data directory for Bound Imports see PE file spec for more
        /// </summary>
        public IMAGE_DATA_DIRECTORY BoundImportDirectory { get { return Directory(11); } }
        /// <summary>
        /// Return the data directory for the DLL Import Address Table (IAT) see PE file spec for more
        /// </summary>
        public IMAGE_DATA_DIRECTORY ImportAddressTableDirectory { get { return Directory(12); } }
        /// <summary>
        /// Return the data directory for Delayed Imports see PE file spec for more
        /// </summary>
        public IMAGE_DATA_DIRECTORY DelayImportDirectory { get { return Directory(13); } }
        /// <summary>
        ///  see PE file spec for more .NET Runtime infomration.  
        /// </summary>
        public IMAGE_DATA_DIRECTORY ComDescriptorDirectory { get { return Directory(14); } }

        #region private
        internal static DateTime TimeDateStampToDate(int timeDateStampSec)
        {
            // Convert seconds from Jan 1 1970 to DateTime ticks.  
            // The 621356004000000000L represents Jan 1 1970 as DateTime 100ns ticks.  
            DateTime ret = new DateTime((long)timeDateStampSec * 10000000 + 621356004000000000L, DateTimeKind.Utc).ToLocalTime();

            // From what I can tell TimeDateSec does not take into account daylight savings time when
            // computing the UTC time. Because of this we adjust here to get the proper local time.  
            if (ret.IsDaylightSavingTime())
                ret = ret.AddHours(-1.0);
            return ret;
        }

        internal int FileOffsetOfResources
        {
            get
            {
                if (ResourceDirectory.VirtualAddress == 0)
                    return 0;
                return RvaToFileOffset(ResourceDirectory.VirtualAddress);
            }
        }

        private IMAGE_OPTIONAL_HEADER32* OptionalHeader32 { get { return (IMAGE_OPTIONAL_HEADER32*)(((byte*)_ntHeader) + sizeof(IMAGE_NT_HEADERS)); } }
        private IMAGE_OPTIONAL_HEADER64* OptionalHeader64 { get { return (IMAGE_OPTIONAL_HEADER64*)(((byte*)_ntHeader) + sizeof(IMAGE_NT_HEADERS)); } }
        private IMAGE_DATA_DIRECTORY* ntDirectories
        {
            get
            {
                if (IsPE64)
                    return (IMAGE_DATA_DIRECTORY*)(((byte*)_ntHeader) + sizeof(IMAGE_NT_HEADERS) + sizeof(IMAGE_OPTIONAL_HEADER64));
                else
                    return (IMAGE_DATA_DIRECTORY*)(((byte*)_ntHeader) + sizeof(IMAGE_NT_HEADERS) + sizeof(IMAGE_OPTIONAL_HEADER32));
            }
        }

        private IMAGE_DOS_HEADER* _dosHeader;
        private IMAGE_NT_HEADERS* _ntHeader;
        private IMAGE_SECTION_HEADER* _sections;
        private bool _virt;
        #endregion
    }

    /// <summary>
    /// The Machine types supporte by the portable executable (PE) File format
    /// </summary>
    public enum MachineType : ushort
    {
        /// <summary>
        /// Unknown machine type
        /// </summary>
        Native = 0,
        /// <summary>
        /// Intel X86 CPU 
        /// </summary>
        X86 = 0x014c,
        /// <summary>
        /// Intel IA64 
        /// </summary>
        ia64 = 0x0200,
        /// <summary>
        /// ARM 32 bit 
        /// </summary>
        ARM = 0x01c0,
        /// <summary>
        /// Arm 64 bit 
        /// </summary>
        Amd64 = 0x8664,
    };

    /// <summary>
    /// Represents a Portable Executable (PE) Data directory.  This is just a well known optional 'Blob' of memory (has a starting point and size)
    /// </summary>
    public struct IMAGE_DATA_DIRECTORY
    {
        /// <summary>
        /// The start of the data blob when the file is mapped into memory
        /// </summary>
        public int VirtualAddress;
        /// <summary>
        /// The length of the data blob.  
        /// </summary>
        public int Size;
    }

    /// <summary>
    /// FileVersionInfo reprents the extended version formation that is optionally placed in the PE file resource area. 
    /// </summary>
    public unsafe sealed class FileVersionInfo
    {
        // TODO incomplete, but this is all I need.  
        /// <summary>
        /// The verison string 
        /// </summary>
        public string FileVersion { get; private set; }

        /// <summary>
        /// Comments to supplement the file version
        /// </summary>
        public string Comments { get; private set; }

        #region private
        internal FileVersionInfo(byte* data, int dataLen)
        {
            FileVersion = "";
            if (dataLen <= 0x5c)
                return;

            // See http://msdn.microsoft.com/en-us/library/ms647001(v=VS.85).aspx
            byte* stringInfoPtr = data + 0x5c;   // Gets to first StringInfo

            // TODO search for FileVersion string ... 
            string dataAsString = new string((char*)stringInfoPtr, 0, (dataLen - 0x5c) / 2);

            FileVersion = GetDataString(dataAsString, "FileVersion");
            Comments = GetDataString(dataAsString, "Comments");
        }

        private static string GetDataString(string dataAsString, string fileVersionKey)
        {
            int fileVersionIdx = dataAsString.IndexOf(fileVersionKey);
            if (fileVersionIdx >= 0)
            {
                int valIdx = fileVersionIdx + fileVersionKey.Length;
                for (; ;)
                {
                    valIdx++;
                    if (valIdx >= dataAsString.Length)
                        return null;
                    if (dataAsString[valIdx] != (char)0)
                        break;
                }
                int varEndIdx = dataAsString.IndexOf((char)0, valIdx);
                if (varEndIdx < 0)
                    return null;

                return dataAsString.Substring(valIdx, varEndIdx - valIdx);
            }

            return null;
        }

        #endregion
    }

    #region private classes we may want to expose

    /// <summary>
    /// A PEBuffer represents 
    /// </summary>
    internal unsafe sealed class PEBuffer : IDisposable
    {
        public PEBuffer(Stream stream, int buffSize = 512)
        {
            _stream = stream;
            GetBuffer(buffSize);
        }
        public byte* Fetch(int filePos, int size)
        {
            if (size > _buff.Length)
                GetBuffer(size);
            if (!(_buffPos <= filePos && filePos + size <= _buffPos + _buffLen))
            {
                // Read in the block of 'size' bytes at filePos
                _buffPos = filePos;
                _stream.Seek(_buffPos, SeekOrigin.Begin);
                _buffLen = 0;
                while (_buffLen < _buff.Length)
                {
                    var count = _stream.Read(_buff, _buffLen, size - _buffLen);
                    if (count == 0)
                        break;
                    _buffLen += count;
                }
            }
            return &_buffPtr[filePos - _buffPos];
        }
        public byte* Buffer { get { return _buffPtr; } }
        public int Length { get { return _buffLen; } }
        public void Dispose()
        {
            _pinningHandle.Free();
        }
        #region private
        private void GetBuffer(int buffSize)
        {
            _buff = new byte[buffSize];
            _pinningHandle = GCHandle.Alloc(_buff, GCHandleType.Pinned);
            fixed (byte* ptr = _buff)
                _buffPtr = ptr;
            _buffLen = 0;
        }

        private int _buffPos;
        private int _buffLen;      // Number of valid bytes in _buff
        private byte[] _buff;
        private byte* _buffPtr;
        private GCHandle _pinningHandle;
        private Stream _stream;
        #endregion
    }

    internal unsafe sealed class ResourceNode
    {
        public string Name { get; private set; }
        public bool IsLeaf { get; private set; }

        // If IsLeaf is true
        public int DataLength { get { return _dataLen; } }
        public byte* FetchData(int offsetInResourceData, int size, PEBuffer buff)
        {
            return buff.Fetch(_dataFileOffset + offsetInResourceData, size);
        }
        public FileVersionInfo GetFileVersionInfo()
        {
            var buff = _file.AllocBuff();
            byte* bytes = FetchData(0, DataLength, buff);
            var ret = new FileVersionInfo(bytes, DataLength);
            _file.FreeBuff(buff);
            return ret;
        }

        public override string ToString()
        {
            StringWriter sw = new StringWriter();
            ToString(sw, "");
            return sw.ToString();
        }

        static public ResourceNode GetChild(ResourceNode node, string name)
        {
            if (node == null)
                return null;
            foreach (var child in node.Children)
                if (child.Name == name)
                    return child;
            return null;
        }

        // If IsLeaf is false
        public List<ResourceNode> Children
        {
            get
            {
                if (_children == null && !IsLeaf)
                {
                    var buff = _file.AllocBuff();
                    var resourceStartFileOffset = _file.Header.FileOffsetOfResources;

                    IMAGE_RESOURCE_DIRECTORY* resourceHeader = (IMAGE_RESOURCE_DIRECTORY*)buff.Fetch(
                        _nodeFileOffset, sizeof(IMAGE_RESOURCE_DIRECTORY));

                    int totalCount = resourceHeader->NumberOfNamedEntries + resourceHeader->NumberOfIdEntries;
                    int totalSize = totalCount * sizeof(IMAGE_RESOURCE_DIRECTORY_ENTRY);

                    IMAGE_RESOURCE_DIRECTORY_ENTRY* entries = (IMAGE_RESOURCE_DIRECTORY_ENTRY*)buff.Fetch(
                        _nodeFileOffset + sizeof(IMAGE_RESOURCE_DIRECTORY), totalSize);

                    var nameBuff = _file.AllocBuff();
                    _children = new List<ResourceNode>();
                    for (int i = 0; i < totalCount; i++)
                    {
                        var entry = &entries[i];
                        string entryName = null;
                        if (_isTop)
                            entryName = IMAGE_RESOURCE_DIRECTORY_ENTRY.GetTypeNameForTypeId(entry->Id);
                        else
                            entryName = entry->GetName(nameBuff, resourceStartFileOffset);
                        Children.Add(new ResourceNode(entryName, resourceStartFileOffset + entry->DataOffset, _file, entry->IsLeaf));
                    }
                    _file.FreeBuff(nameBuff);
                    _file.FreeBuff(buff);
                }
                return _children;
            }
        }

        #region private
        private void ToString(StringWriter sw, string indent)
        {
            sw.Write("{0}<ResourceNode", indent);
            sw.Write(" Name=\"{0}\"", Name);
            sw.Write(" IsLeaf=\"{0}\"", IsLeaf);

            if (IsLeaf)
            {
                sw.Write("DataLength=\"{0}\"", DataLength);
                sw.WriteLine("/>");
            }
            else
            {
                sw.Write("ChildCount=\"{0}\"", Children.Count);
                sw.WriteLine(">");
                foreach (var child in Children)
                    child.ToString(sw, indent + "  ");
                sw.WriteLine("{0}</ResourceNode>", indent);
            }
        }

        internal ResourceNode(string name, int nodeFileOffset, PEFile file, bool isLeaf, bool isTop = false)
        {
            _file = file;
            _nodeFileOffset = nodeFileOffset;
            _isTop = isTop;
            IsLeaf = isLeaf;
            Name = name;

            if (isLeaf)
            {
                var buff = _file.AllocBuff();
                IMAGE_RESOURCE_DATA_ENTRY* dataDescr = (IMAGE_RESOURCE_DATA_ENTRY*)buff.Fetch(nodeFileOffset, sizeof(IMAGE_RESOURCE_DATA_ENTRY));

                _dataLen = dataDescr->Size;
                _dataFileOffset = file.Header.RvaToFileOffset(dataDescr->RvaToData);
                var data = FetchData(0, _dataLen, buff);
                _file.FreeBuff(buff);
            }
        }

        private PEFile _file;
        private int _nodeFileOffset;
        private List<ResourceNode> _children;
        private bool _isTop;
        private int _dataLen;
        private int _dataFileOffset;
        #endregion
    }
    #endregion


    #region private classes
    [StructLayout(LayoutKind.Explicit, Size = 64)]
    internal struct IMAGE_DOS_HEADER
    {
        public const short IMAGE_DOS_SIGNATURE = 0x5A4D;       // MZ.  
        [FieldOffset(0)]
        public short e_magic;
        [FieldOffset(60)]
        public int e_lfanew;            // Offset to the IMAGE_FILE_HEADER
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct IMAGE_NT_HEADERS
    {
        public uint Signature;
        public IMAGE_FILE_HEADER FileHeader;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct IMAGE_FILE_HEADER
    {
        public ushort Machine;
        public ushort NumberOfSections;
        public uint TimeDateStamp;
        public uint PointerToSymbolTable;
        public uint NumberOfSymbols;
        public ushort SizeOfOptionalHeader;
        public ushort Characteristics;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct IMAGE_OPTIONAL_HEADER32
    {
        public ushort Magic;
        public byte MajorLinkerVersion;
        public byte MinorLinkerVersion;
        public uint SizeOfCode;
        public uint SizeOfInitializedData;
        public uint SizeOfUninitializedData;
        public uint AddressOfEntryPoint;
        public uint BaseOfCode;
        public uint BaseOfData;
        public uint ImageBase;
        public uint SectionAlignment;
        public uint FileAlignment;
        public ushort MajorOperatingSystemVersion;
        public ushort MinorOperatingSystemVersion;
        public ushort MajorImageVersion;
        public ushort MinorImageVersion;
        public ushort MajorSubsystemVersion;
        public ushort MinorSubsystemVersion;
        public uint Win32VersionValue;
        public uint SizeOfImage;
        public uint SizeOfHeaders;
        public uint CheckSum;
        public ushort Subsystem;
        public ushort DllCharacteristics;
        public uint SizeOfStackReserve;
        public uint SizeOfStackCommit;
        public uint SizeOfHeapReserve;
        public uint SizeOfHeapCommit;
        public uint LoaderFlags;
        public uint NumberOfRvaAndSizes;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct IMAGE_OPTIONAL_HEADER64
    {
        public ushort Magic;
        public byte MajorLinkerVersion;
        public byte MinorLinkerVersion;
        public uint SizeOfCode;
        public uint SizeOfInitializedData;
        public uint SizeOfUninitializedData;
        public uint AddressOfEntryPoint;
        public uint BaseOfCode;
        public ulong ImageBase;
        public uint SectionAlignment;
        public uint FileAlignment;
        public ushort MajorOperatingSystemVersion;
        public ushort MinorOperatingSystemVersion;
        public ushort MajorImageVersion;
        public ushort MinorImageVersion;
        public ushort MajorSubsystemVersion;
        public ushort MinorSubsystemVersion;
        public uint Win32VersionValue;
        public uint SizeOfImage;
        public uint SizeOfHeaders;
        public uint CheckSum;
        public ushort Subsystem;
        public ushort DllCharacteristics;
        public ulong SizeOfStackReserve;
        public ulong SizeOfStackCommit;
        public ulong SizeOfHeapReserve;
        public ulong SizeOfHeapCommit;
        public uint LoaderFlags;
        public uint NumberOfRvaAndSizes;
    }

    [StructLayout(LayoutKind.Sequential)]
    unsafe internal struct IMAGE_SECTION_HEADER
    {
        public string Name
        {
            get
            {
                fixed (byte* ptr = NameBytes)
                {
                    if (ptr[7] == 0)
                        return System.Runtime.InteropServices.Marshal.PtrToStringAnsi((IntPtr)ptr);
                    else
                        return System.Runtime.InteropServices.Marshal.PtrToStringAnsi((IntPtr)ptr, 8);
                }
            }
        }
        public fixed byte NameBytes[8];
        public uint VirtualSize;
        public uint VirtualAddress;
        public uint SizeOfRawData;
        public uint PointerToRawData;
        public uint PointerToRelocations;
        public uint PointerToLinenumbers;
        public ushort NumberOfRelocations;
        public ushort NumberOfLinenumbers;
        public uint Characteristics;
    };

    internal struct IMAGE_DEBUG_DIRECTORY
    {
        public int Characteristics;
        public int TimeDateStamp;
        public short MajorVersion;
        public short MinorVersion;
        public IMAGE_DEBUG_TYPE Type;
        public int SizeOfData;
        public int AddressOfRawData;
        public int PointerToRawData;
    };

    internal enum IMAGE_DEBUG_TYPE
    {
        UNKNOWN = 0,
        COFF = 1,
        CODEVIEW = 2,
        FPO = 3,
        MISC = 4,
        BBT = 10,
    };

    internal unsafe struct CV_INFO_PDB70
    {
        public const int PDB70CvSignature = 0x53445352; // RSDS in ascii

        public int CvSignature;
        public Guid Signature;
        public int Age;
        public fixed byte bytePdbFileName[1];   // Actually variable sized. 
        public string PdbFileName
        {
            get
            {
                fixed (byte* ptr = bytePdbFileName)
                    return System.Runtime.InteropServices.Marshal.PtrToStringAnsi((IntPtr)ptr);
            }
        }
    };


    /* Resource information */
    // Resource directory consists of two counts, following by a variable length
    // array of directory entries.  The first count is the number of entries at
    // beginning of the array that have actual names associated with each entry.
    // The entries are in ascending order, case insensitive strings.  The second
    // count is the number of entries that immediately follow the named entries.
    // This second count identifies the number of entries that have 16-bit integer
    // Ids as their name.  These entries are also sorted in ascending order.
    //
    // This structure allows fast lookup by either name or number, but for any
    // given resource entry only one form of lookup is supported, not both.
    internal unsafe struct IMAGE_RESOURCE_DIRECTORY
    {
        public int Characteristics;
        public int TimeDateStamp;
        public short MajorVersion;
        public short MinorVersion;
        public ushort NumberOfNamedEntries;
        public ushort NumberOfIdEntries;
        //  IMAGE_RESOURCE_DIRECTORY_ENTRY DirectoryEntries[];
    };

    //
    // Each directory contains the 32-bit Name of the entry and an offset,
    // relative to the beginning of the resource directory of the data associated
    // with this directory entry.  If the name of the entry is an actual text
    // string instead of an integer Id, then the high order bit of the name field
    // is set to one and the low order 31-bits are an offset, relative to the
    // beginning of the resource directory of the string, which is of type
    // IMAGE_RESOURCE_DIRECTORY_STRING.  Otherwise the high bit is clear and the
    // low-order 16-bits are the integer Id that identify this resource directory
    // entry. If the directory entry is yet another resource directory (i.e. a
    // subdirectory), then the high order bit of the offset field will be
    // set to indicate this.  Otherwise the high bit is clear and the offset
    // field points to a resource data entry.
    internal unsafe struct IMAGE_RESOURCE_DIRECTORY_ENTRY
    {
        public bool IsStringName { get { return _nameOffsetAndFlag < 0; } }
        public int NameOffset { get { return _nameOffsetAndFlag & 0x7FFFFFFF; } }

        public bool IsLeaf { get { return (0x80000000 & _dataOffsetAndFlag) == 0; } }
        public int DataOffset { get { return _dataOffsetAndFlag & 0x7FFFFFFF; } }
        public int Id { get { return 0xFFFF & _nameOffsetAndFlag; } }

        private int _nameOffsetAndFlag;
        private int _dataOffsetAndFlag;

        internal unsafe string GetName(PEBuffer buff, int resourceStartFileOffset)
        {
            if (IsStringName)
            {
                int nameLen = *((ushort*)buff.Fetch(NameOffset + resourceStartFileOffset, 2));
                char* namePtr = (char*)buff.Fetch(NameOffset + resourceStartFileOffset + 2, nameLen);
                return new string(namePtr);
            }
            else
                return Id.ToString();
        }

        internal static string GetTypeNameForTypeId(int typeId)
        {
            switch (typeId)
            {
                case 1:
                    return "Cursor";
                case 2:
                    return "BitMap";
                case 3:
                    return "Icon";
                case 4:
                    return "Menu";
                case 5:
                    return "Dialog";
                case 6:
                    return "String";
                case 7:
                    return "FontDir";
                case 8:
                    return "Font";
                case 9:
                    return "Accelerator";
                case 10:
                    return "RCData";
                case 11:
                    return "MessageTable";
                case 12:
                    return "GroupCursor";
                case 14:
                    return "GroupIcon";
                case 16:
                    return "Version";
                case 19:
                    return "PlugPlay";
                case 20:
                    return "Vxd";
                case 21:
                    return "Aniicursor";
                case 22:
                    return "Aniicon";
                case 23:
                    return "Html";
                case 24:
                    return "RT_MANIFEST";
            }
            return typeId.ToString();
        }
    }

    // Each resource data entry describes a leaf node in the resource directory
    // tree.  It contains an offset, relative to the beginning of the resource
    // directory of the data for the resource, a size field that gives the number
    // of bytes of data at that offset, a CodePage that should be used when
    // decoding code point values within the resource data.  Typically for new
    // applications the code page would be the unicode code page.
    internal unsafe struct IMAGE_RESOURCE_DATA_ENTRY
    {
        public int RvaToData;
        public int Size;
        public int CodePage;
        public int Reserved;
    };

    #endregion
}
