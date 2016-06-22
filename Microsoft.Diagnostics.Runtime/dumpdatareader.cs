// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Diagnostics.Runtime.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Microsoft.Diagnostics.Runtime
{
    internal unsafe class DumpDataReader : IDataReader, IDisposable
    {
        private string _fileName;
        private DumpReader _dumpReader;
        private List<ModuleInfo> _modules;
        private string _generatedPath;

        public DumpDataReader(string file)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException(file);

            if (Path.GetExtension(file).ToLower() == ".cab")
                file = ExtractCab(file);

            _fileName = file;
            _dumpReader = new DumpReader(file);
        }

        ~DumpDataReader()
        {
            Dispose();
        }

        private string ExtractCab(string file)
        {
            _generatedPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            while (Directory.Exists(_generatedPath))
                _generatedPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            Directory.CreateDirectory(_generatedPath);

            CommandOptions options = new CommandOptions();
            options.NoThrow = true;
            options.NoWindow = true;
            Command cmd = Command.Run(string.Format("expand -F:*dmp {0} {1}", file, _generatedPath), options);

            bool error = false;
            if (cmd.ExitCode != 0)
            {
                error = true;
            }
            else
            {
                file = null;
                foreach (var item in Directory.GetFiles(_generatedPath))
                {
                    string ext = Path.GetExtension(item).ToLower();
                    if (ext == ".dll" || ext == ".pdb" || ext == ".exe")
                        continue;

                    file = item;
                    break;
                }


                error |= file == null;
            }

            if (error)
            {
                Dispose();
                throw new IOException("Failed to extract a crash dump from " + file);
            }

            return file;
        }


        public bool IsMinidump
        {
            get
            {
                return _dumpReader.IsMinidump;
            }
        }

        public override string ToString()
        {
            return _fileName;
        }

        public void Close()
        {
            _dumpReader.Dispose();
            Dispose();
        }

        public void Dispose()
        {
            if (_generatedPath != null)
            {
                try
                {
                    foreach (var file in Directory.GetFiles(_generatedPath))
                        File.Delete(file);

                    Directory.Delete(_generatedPath, false);
                }
                catch
                {
                }

                _generatedPath = null;
            }
        }

        public void Flush()
        {
            _modules = null;
        }

        public Architecture GetArchitecture()
        {
            switch (_dumpReader.ProcessorArchitecture)
            {
                case ProcessorArchitecture.PROCESSOR_ARCHITECTURE_ARM:
                    return Architecture.Arm;

                case ProcessorArchitecture.PROCESSOR_ARCHITECTURE_AMD64:
                    return Architecture.Amd64;

                case ProcessorArchitecture.PROCESSOR_ARCHITECTURE_INTEL:
                    return Architecture.X86;
            }

            return Architecture.Unknown;
        }

        public uint GetPointerSize()
        {
            switch (GetArchitecture())
            {
                case Architecture.Amd64:
                    return 8;

                default:
                    return 4;
            }
        }

        public IList<ModuleInfo> EnumerateModules()
        {
            if (_modules != null)
                return _modules;

            List<ModuleInfo> modules = new List<ModuleInfo>();

            foreach (var mod in _dumpReader.EnumerateModules())
            {
                var raw = mod.Raw;

                ModuleInfo module = new ModuleInfo(this);
                module.FileName = mod.FullName;
                module.ImageBase = raw.BaseOfImage;
                module.FileSize = raw.SizeOfImage;
                module.TimeStamp = raw.TimeDateStamp;

                module.Version = GetVersionInfo(mod);
                modules.Add(module);
            }

            _modules = modules;
            return modules;
        }

        public void GetVersionInfo(ulong baseAddress, out VersionInfo version)
        {
            DumpModule module = _dumpReader.TryLookupModuleByAddress(baseAddress);
            version = (module != null) ? GetVersionInfo(module) : new VersionInfo();
        }

        private static VersionInfo GetVersionInfo(DumpModule module)
        {
            var raw = module.Raw;
            var version = raw.VersionInfo;
            int minor = (ushort)version.dwFileVersionMS;
            int major = (ushort)(version.dwFileVersionMS >> 16);
            int patch = (ushort)version.dwFileVersionLS;
            int rev = (ushort)(version.dwFileVersionLS >> 16);

            var versionInfo = new VersionInfo(major, minor, rev, patch);
            return versionInfo;
        }


        private byte[] _ptrBuffer = new byte[IntPtr.Size];
        public ulong ReadPointerUnsafe(ulong addr)
        {
            return _dumpReader.ReadPointerUnsafe(addr);
        }

        public uint ReadDwordUnsafe(ulong addr)
        {
            return _dumpReader.ReadDwordUnsafe(addr);
        }


        public bool ReadMemory(ulong address, byte[] buffer, int bytesRequested, out int bytesRead)
        {
            bytesRead = _dumpReader.ReadPartialMemory(address, buffer, bytesRequested);
            return bytesRead > 0;
        }

        public bool ReadMemory(ulong address, IntPtr buffer, int bytesRequested, out int bytesRead)
        {
            bytesRead = (int)_dumpReader.ReadPartialMemory(address, buffer, (uint)bytesRequested);
            return bytesRead > 0;
        }


        public ulong GetThreadTeb(uint id)
        {
            var thread = _dumpReader.GetThread((int)id);
            if (thread == null)
                return 0;

            return thread.Teb;
        }

        public IEnumerable<uint> EnumerateAllThreads()
        {
            foreach (var dumpThread in _dumpReader.EnumerateThreads())
                yield return (uint)dumpThread.ThreadId;
        }

        public bool VirtualQuery(ulong addr, out VirtualQueryData vq)
        {
            return _dumpReader.VirtualQuery(addr, out vq);
        }

        public bool GetThreadContext(uint id, uint contextFlags, uint contextSize, IntPtr context)
        {
            var thread = _dumpReader.GetThread((int)id);
            if (thread == null)
                return false;

            thread.GetThreadContext(context, (int)contextSize);
            return true;
        }

        public bool GetThreadContext(uint threadID, uint contextFlags, uint contextSize, byte[] context)
        {
            throw new NotImplementedException();
        }
    }
}
