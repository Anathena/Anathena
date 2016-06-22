// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Diagnostics.Runtime.Utilities.Pdb;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Runtime.InteropServices;

namespace Microsoft.Diagnostics.Runtime.Utilities
{
    /// <summary>
    /// This class is a general purpose symbol locator and binary locator.
    /// </summary>
    public abstract partial class SymbolLocator
    {
        private static string[] s_microsoftSymbolServers = { "http://msdl.microsoft.com/download/symbols", "http://referencesource.microsoft.com/symbols" };

        /// <summary>
        /// The raw symbol path.  You should probably use the SymbolPath property instead.
        /// </summary>
        protected volatile string _symbolPath;
        /// <summary>
        /// The raw symbol cache.  You should probably use the SymbolCache property instead.
        /// </summary>
        /// 
        protected volatile string _symbolCache;

        /// <summary>
        /// The timeout (in milliseconds) used when contacting each individual server.  This is not a total timeout for the entire
        /// symbol server operation.
        /// </summary>
        public int Timeout { get; set; } = 60000;

        /// <summary>
        /// A set of pdbs that we did not find when requested.  This set is SymbolLocator specific (not global
        /// like successful downloads) and is cleared when we change the symbol path or cache.
        /// </summary>
        internal volatile HashSet<PdbEntry> _missingPdbs = new HashSet<PdbEntry>();

        /// <summary>
        /// A set of files that we did not find when requested.  This set is SymbolLocator specific (not global
        /// like successful downloads) and is cleared when we change the symbol path or cache.
        /// </summary>
        internal volatile HashSet<FileEntry> _missingFiles = new HashSet<FileEntry>();

        /// <summary>
        /// Constructor.
        /// </summary>
        public SymbolLocator()
        {
            var sympath = _NT_SYMBOL_PATH;
            if (string.IsNullOrEmpty(sympath))
                sympath = MicrosoftSymbolServerPath;

            SymbolPath = sympath;
        }

        /// <summary>
        /// Return the string representing a symbol path for the 'standard' microsoft symbol servers.   
        /// This returns the public msdl.microsoft.com server if outside Microsoft.  
        /// </summary>
        public static string MicrosoftSymbolServerPath
        {
            get
            {
                bool first = true;
                StringBuilder result = new StringBuilder();

                foreach (var path in s_microsoftSymbolServers)
                {
                    if (!first)
                        result.Append(';');

                    result.Append("SRV*");
                    result.Append(path);
                    first = false;
                }

                return result.ToString();
            }
        }

        /// <summary>
        /// Retrieves a list of the default Microsoft symbol servers.
        /// </summary>
        public static string[] MicrosoftSymbolServers
        {
            get
            {
                return s_microsoftSymbolServers;
            }
        }

        /// <summary>
        /// This property gets and sets the global _NT_SYMBOL_PATH environment variable.
        /// This is the global setting for symbol paths on a computer.
        /// </summary>
        public static string _NT_SYMBOL_PATH
        {
            get
            {
                var ret = Environment.GetEnvironmentVariable("_NT_SYMBOL_PATH");
                return ret ?? "";
            }
            set
            {
                Environment.SetEnvironmentVariable("_NT_SYMBOL_PATH", value);
            }
        }

        /// <summary>
        /// Gets or sets the local symbol file cache.  This is the location that
        /// all symbol files are downloaded to on your computer.
        /// </summary>
        public string SymbolCache
        {
            get
            {
                var cache = _symbolCache;
                if (!string.IsNullOrEmpty(cache))
                    return cache;

                string tmp = Path.GetTempPath();
                if (string.IsNullOrEmpty(tmp))
                    tmp = ".";

                return Path.Combine(tmp, "symbols");
            }
            set
            {
                _symbolCache = value;
                if (!string.IsNullOrEmpty(value))
                    Directory.CreateDirectory(value);

                SymbolPathOrCacheChanged();
            }
        }

        /// <summary>
        /// Gets or sets the SymbolPath this object uses to attempt to find PDBs and binaries.
        /// </summary>
        public string SymbolPath
        {
            get
            {
                return _symbolPath ?? "";
            }

            set
            {
                _symbolPath = (value ?? "").Trim();

                SymbolPathOrCacheChanged();
            }
        }

        /// <summary>
        /// Attempts to locate a binary via the symbol server.  This function will then copy the file
        /// locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="fileName">The filename that the binary is indexed under.</param>
        /// <param name="buildTimeStamp">The build timestamp the binary is indexed under.</param>
        /// <param name="imageSize">The image size the binary is indexed under.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        public string FindBinary(string fileName, uint buildTimeStamp, uint imageSize, bool checkProperties = true)
        {
            return FindBinary(fileName, (int)buildTimeStamp, (int)imageSize, checkProperties);
        }

        /// <summary>
        /// Attempts to locate a binary via the symbol server.  This function will then copy the file
        /// locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="fileName">The filename that the binary is indexed under.</param>
        /// <param name="buildTimeStamp">The build timestamp the binary is indexed under.</param>
        /// <param name="imageSize">The image size the binary is indexed under.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        public abstract string FindBinary(string fileName, int buildTimeStamp, int imageSize, bool checkProperties = true);

        /// <summary>
        /// Attempts to locate a binary via the symbol server.  This function will then copy the file
        /// locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="module">The module to locate.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        public string FindBinary(ModuleInfo module, bool checkProperties = true)
        {
            return FindBinary(module.FileName, module.TimeStamp, module.FileSize, checkProperties);
        }

        /// <summary>
        /// Attempts to locate a dac via the symbol server.  This function will then copy the file
        /// locally to the symbol cache and return the location of the local file on disk.  Note that
        /// the dac should not validate if the properties of the file match the one it was indexed under.
        /// </summary>
        /// <param name="dac">The dac to locate.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        public string FindBinary(DacInfo dac)
        {
            return FindBinary(dac, false);
        }

        /// <summary>
        /// Attempts to locate the pdb for a given module.
        /// </summary>
        /// <param name="module">The module to locate the pdb for.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        public string FindPdb(ModuleInfo module)
        {
            if (module == null)
                throw new ArgumentNullException("module");

            PdbInfo pdb = module.Pdb;
            if (pdb == null)
                return null;

            return FindPdb(pdb);
        }

        /// <summary>
        /// Attempts to locate the pdb for a given module.
        /// </summary>
        /// <param name="pdb">The pdb to locate.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        public string FindPdb(PdbInfo pdb)
        {
            if (pdb == null)
                throw new ArgumentNullException("pdb");

            return FindPdb(pdb.FileName, pdb.Guid, pdb.Revision);
        }

        /// <summary>
        /// Attempts to locate a pdb based on its name, guid, and revision number.
        /// </summary>
        /// <param name="pdbName">The name the pdb is indexed under.</param>
        /// <param name="pdbIndexGuid">The guid the pdb is indexed under.</param>
        /// <param name="pdbIndexAge">The age of the pdb.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        public abstract string FindPdb(string pdbName, Guid pdbIndexGuid, int pdbIndexAge);

        /// <summary>
        /// Validates whether a pdb on disk matches the given Guid/revision.
        /// </summary>
        /// <param name="pdbName"></param>
        /// <param name="guid"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        protected virtual bool ValidatePdb(string pdbName, Guid guid, int age)
        {
            try
            {
                Guid fileGuid;
                int fileAge;
                PdbReader.GetPdbProperties(pdbName, out fileGuid, out fileAge);

                return guid == fileGuid && age == fileAge;
            }
            catch (IOException)
            {
                return false;
            }
        }

        /// <summary>
        /// Validates whether a file on disk matches the properties we expect.
        /// </summary>
        /// <param name="fullPath">The full path on disk of a PEImage to inspect.</param>
        /// <param name="buildTimeStamp">The build timestamp we expect to match.</param>
        /// <param name="imageSize">The build image size we expect to match.</param>
        /// <param name="checkProperties">Whether we should actually validate the imagesize/timestamp or not.</param>
        /// <returns></returns>
        protected virtual bool ValidateBinary(string fullPath, int buildTimeStamp, int imageSize, bool checkProperties)
        {
            if (string.IsNullOrEmpty(fullPath))
                return false;

            if (File.Exists(fullPath))
            {
                if (!checkProperties)
                {
                    return true;
                }

                try
                {
                    using (PEFile pefile = new PEFile(fullPath))
                    {
                        var header = pefile.Header;
                        if (!checkProperties || (header.TimeDateStampSec == buildTimeStamp && header.SizeOfImage == imageSize))
                        {
                            return true;
                        }
                        else
                        {
                            Trace("Rejected file '{0}' because file size and time stamp did not match.", fullPath);
                        }
                    }
                }
                catch (Exception e)
                {
                    Trace("Encountered exception {0} while attempting to inspect file '{1}'.", e.GetType().Name, fullPath);
                }
            }

            return false;
        }

        /// <summary>
        /// Copies a given stream to a file.
        /// </summary>
        /// <param name="input">The stream of data to copy.</param>
        /// <param name="fullSrcPath">The original source location of "stream".  This may be a URL or null.</param>
        /// <param name="fullDestPath">The full destination path to copy the file to.</param>
        /// <param name="size">A hint as to the length of the stream.  This may be 0 or negative if the length is unknown.</param>
        /// <returns>True if the method successfully copied the file, false otherwise.</returns>
        protected virtual void CopyStreamToFile(Stream input, string fullSrcPath, string fullDestPath, long size)
        {
            Debug.Assert(input != null);

            try
            {
                FileInfo fi = new FileInfo(fullDestPath);
                if (fi.Exists && fi.Length == size)
                    return;

                string folder = Path.GetDirectoryName(fullDestPath);
                Directory.CreateDirectory(folder);

                FileStream file = null;
                try
                {
                    file = new FileStream(fullDestPath, FileMode.OpenOrCreate);
                    byte[] buffer = new byte[2048];
                    int read;
                    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                        file.Write(buffer, 0, read);
                }
                finally
                {
                    if (file != null)
                        file.Dispose();
                }
            }
            catch (Exception e)
            {
                try
                {
                    if (File.Exists(fullDestPath))
                        File.Delete(fullDestPath);
                }
                catch
                {
                    // We ignore errors of this nature.
                }

                Trace("Encountered an error while attempting to copy '{0} to '{1}': {2}", fullSrcPath, fullDestPath, e.Message);
            }
        }

        /// <summary>
        /// Writes diagnostic messages about symbol loading to System.Diagnostics.Trace.  Figuring out symbol issues can be tricky,
        /// so if you override methods in SymbolLocator, be sure to trace the information here.
        /// </summary>
        /// <param name="fmt"></param>
        /// <param name="args"></param>
        protected static void Trace(string fmt, params object[] args)
        {
            if (args != null && args.Length > 0)
                fmt = string.Format(fmt, args);

            System.Diagnostics.Trace.WriteLine(fmt, "Microsoft.Diagnostics.Runtime.SymbolLocator");
        }

        /// <summary>
        /// Called when changing the symbol file path or cache.
        /// </summary>
        protected virtual void SymbolPathOrCacheChanged()
        {
            _missingPdbs.Clear();
            _missingFiles.Clear();
        }

        internal virtual void PrefetchBinary(string name, int timestamp, int imagesize)
        {
        }
    }


    /// <summary>
    /// Default implementation of a symbol locator.
    /// </summary>
    public partial class DefaultSymbolLocator : SymbolLocator
    {
        /// <summary>
        /// Default implementation of finding a pdb.
        /// </summary>
        /// <param name="pdbName">The name the pdb is indexed under.</param>
        /// <param name="pdbIndexGuid">The guid the pdb is indexed under.</param>
        /// <param name="pdbIndexAge">The age of the pdb.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        public override string FindPdb(string pdbName, Guid pdbIndexGuid, int pdbIndexAge)
        {
            if (string.IsNullOrEmpty(pdbName))
                return null;

            string pdbSimpleName = Path.GetFileName(pdbName);
            if (pdbName != pdbSimpleName)
            {
                if (ValidatePdb(pdbName, pdbIndexGuid, pdbIndexAge))
                    return pdbName;
            }

            // Check to see if it's already cached.
            PdbEntry entry = new PdbEntry(pdbSimpleName, pdbIndexGuid, pdbIndexAge);
            string result = GetPdbEntry(entry);
            if (result != null)
                return result;

            var missingPdbs = _missingPdbs;
            if (IsMissing(missingPdbs, entry))
                return null;

            string pdbIndexPath = GetIndexPath(pdbSimpleName, pdbIndexGuid, pdbIndexAge);
            foreach (SymPathElement element in SymPathElement.GetElements(SymbolPath))
            {
                if (element.IsSymServer)
                {
                    string targetPath = TryGetFileFromServer(element.Target, pdbIndexPath, element.Cache ?? SymbolCache);
                    if (targetPath != null)
                    {
                        Trace("Found pdb {0} from server '{1}' on path '{2}'.  Copied to '{3}'.", pdbSimpleName, element.Target, pdbIndexPath, targetPath);
                        SetPdbEntry(missingPdbs, entry, targetPath);
                        return targetPath;
                    }
                    else
                    {
                        Trace("No matching pdb found on server '{0}' on path '{1}'.", element.Target, pdbIndexPath);
                    }
                }
                else
                {
                    string fullPath = Path.Combine(element.Target, pdbSimpleName);
                    if (ValidatePdb(fullPath, pdbIndexGuid, pdbIndexAge))
                    {
                        Trace($"Found pdb '{pdbSimpleName}' at '{fullPath}'.");
                        SetPdbEntry(missingPdbs, entry, fullPath);
                        return fullPath;
                    }
                    else
                    {
                        Trace($"Mismatched pdb found at '{fullPath}'.");
                    }
                }
            }

            SetPdbEntry(missingPdbs, entry, null);
            return null;
        }

        /// <summary>
        /// Attempts to locate a binary via the symbol server.  This function will then copy the file
        /// locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="fileName">The filename that the binary is indexed under.</param>
        /// <param name="buildTimeStamp">The build timestamp the binary is indexed under.</param>
        /// <param name="imageSize">The image size the binary is indexed under.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        public override string FindBinary(string fileName, int buildTimeStamp, int imageSize, bool checkProperties = true)
        {
            string fullPath = fileName;
            fileName = Path.GetFileName(fullPath).ToLower();

            // First see if we already have the result cached.
            FileEntry entry = new FileEntry(fileName, buildTimeStamp, imageSize);
            string result = GetFileEntry(entry);
            if (result != null)
                return result;

            var missingFiles = _missingFiles;
            if (IsMissing(missingFiles, entry))
                return null;

            // Test to see if the file is on disk.
            if (ValidateBinary(fullPath, buildTimeStamp, imageSize, checkProperties))
            {
                SetFileEntry(missingFiles, entry, fullPath);
                return fullPath;
            }

            // Finally, check the symbol paths.
            string exeIndexPath = null;
            foreach (SymPathElement element in SymPathElement.GetElements(SymbolPath))
            {
                if (element.IsSymServer)
                {
                    if (exeIndexPath == null)
                        exeIndexPath = GetIndexPath(fileName, buildTimeStamp, imageSize);

                    string target = TryGetFileFromServer(element.Target, exeIndexPath, element.Cache ?? SymbolCache);
                    if (target == null)
                    {
                        Trace($"Server '{element.Target}' did not have file '{Path.GetFileName(fileName)}' with timestamp={buildTimeStamp:x} and filesize={imageSize:x}.");
                    }
                    else if (ValidateBinary(target, buildTimeStamp, imageSize, checkProperties))
                    {
                        Trace($"Found '{fileName}' on server '{element.Target}'.  Copied to '{target}'.");
                        SetFileEntry(missingFiles, entry, target);
                        return target;
                    }
                }
                else
                {
                    string filePath = Path.Combine(element.Target, fileName);
                    if (ValidateBinary(filePath, buildTimeStamp, imageSize, checkProperties))
                    {
                        Trace($"Found '{fileName}' at '{filePath}'.");
                        SetFileEntry(missingFiles, entry, filePath);
                        return filePath;
                    }
                }
            }

            SetFileEntry(missingFiles, entry, null);
            return null;
        }

        private static string GetIndexPath(string fileName, int buildTimeStamp, int imageSize)
        {
            return fileName + @"\" + buildTimeStamp.ToString("x") + imageSize.ToString("x") + @"\" + fileName;
        }

        private static string GetIndexPath(string pdbSimpleName, Guid pdbIndexGuid, int pdbIndexAge)
        {
            return pdbSimpleName + @"\" + pdbIndexGuid.ToString().Replace("-", "") + pdbIndexAge.ToString("x") + @"\" + pdbSimpleName;
        }

        private string TryGetFileFromServer(string urlForServer, string fileIndexPath, string cache)
        {
            Debug.Assert(!string.IsNullOrEmpty(cache));
            if (String.IsNullOrEmpty(urlForServer))
                return null;

            var targetPath = Path.Combine(cache, fileIndexPath);

            // See if it is a compressed file by replacing the last character of the name with an _
            var compressedSigPath = fileIndexPath.Substring(0, fileIndexPath.Length - 1) + "_";
            var compressedFilePath = GetPhysicalFileFromServer(urlForServer, compressedSigPath, cache);
            if (compressedFilePath != null)
            {
                try
                {
                    // Decompress it
                    Command.Run("Expand " + Command.Quote(compressedFilePath) + " " + Command.Quote(targetPath));
                    return targetPath;
                }
                catch (Exception e)
                {
                    Trace("Exception encountered while expanding file '{0}': {1}", compressedFilePath, e.Message);
                }
                finally
                {
                    if (File.Exists(compressedFilePath))
                        File.Delete(compressedFilePath);
                }
            }

            // Just try to fetch the file directly
            var ret = GetPhysicalFileFromServer(urlForServer, fileIndexPath, cache);
            if (ret != null)
                return ret;

            // See if we have a file that tells us to redirect elsewhere. 
            var filePtrSigPath = Path.Combine(Path.GetDirectoryName(fileIndexPath), "file.ptr");
            var filePtrData = GetPhysicalFileFromServer(urlForServer, filePtrSigPath, cache, true);
            if (filePtrData == null)
            {
                return null;
            }

            filePtrData = filePtrData.Trim();
            if (filePtrData.StartsWith("PATH:"))
                filePtrData = filePtrData.Substring(5);

            if (!filePtrData.StartsWith("MSG:") && File.Exists(filePtrData))
            {
                using (FileStream fs = File.OpenRead(filePtrData))
                {
                    CopyStreamToFile(fs, filePtrData, targetPath, fs.Length);
                    return targetPath;
                }
            }
            else
            {
                Trace("Error resolving file.ptr: content '{0}' from '{1}.", filePtrData, filePtrSigPath);
            }

            return null;
        }

        private string GetPhysicalFileFromServer(string serverPath, string pdbIndexPath, string symbolCacheDir, bool returnContents = false)
        {
            if (string.IsNullOrEmpty(serverPath))
                return null;

            var fullDestPath = Path.Combine(symbolCacheDir, pdbIndexPath);
            if (File.Exists(fullDestPath))
                return fullDestPath;

            if (serverPath.StartsWith("http:"))
            {
                var fullUri = serverPath + "/" + pdbIndexPath.Replace('\\', '/');
                try
                {
                    var req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(fullUri);
                    req.UserAgent = "Microsoft-Symbol-Server/6.13.0009.1140";
                    req.Timeout = Timeout;
                    var response = req.GetResponse();
                    using (var fromStream = response.GetResponseStream())
                    {
                        if (returnContents)
                        {
                            TextReader reader = new StreamReader(fromStream);
                            return reader.ReadToEnd();
                        }

                        CopyStreamToFile(fromStream, fullUri, fullDestPath, response.ContentLength);
                        return fullDestPath;
                    }
                }
                catch (WebException)
                {
                    // A timeout or 404.
                    return null;
                }
                catch (Exception e)
                {
                    Trace("Probe of {0} failed: {1}", fullUri, e.Message);
                    return null;
                }
            }
            else
            {
                var fullSrcPath = Path.Combine(serverPath, pdbIndexPath);
                if (!File.Exists(fullSrcPath))
                    return null;

                if (returnContents)
                {
                    try
                    {
                        return File.ReadAllText(fullSrcPath);
                    }
                    catch
                    {
                        return "";
                    }
                }

                using (FileStream fs = File.OpenRead(fullSrcPath))
                    CopyStreamToFile(fs, fullSrcPath, fullDestPath, fs.Length);

                return fullDestPath;
            }
        }

#if V2_SUPPORT
        private Dictionary<FileEntry, string> _binCache = new Dictionary<FileEntry, string>();
        private Dictionary<PdbEntry, string> _pdbCache = new Dictionary<PdbEntry, string>();
        

        private bool IsMissing<T>(HashSet<T> entries, T entry)
        {
            return entries.Contains(entry);
        }
        
        private string GetFileEntry(FileEntry entry)
        {
            string result;
            if (!_binCache.TryGetValue(entry, out result))
                return null;

            Debug.Assert(result != null);
            if (File.Exists(result))
                return result;

            _binCache.Remove(entry);
            return null;
        }

        private void SetFileEntry(HashSet<FileEntry> missing, FileEntry entry, string value)
        {
            if (value == null)
                missing.Add(entry);
            else
                _binCache[entry] = value;
        }


        private string GetPdbEntry(PdbEntry entry)
        {
            string result;
            if (!_pdbCache.TryGetValue(entry, out result))
                return null;

            Debug.Assert(result != null);
            if (File.Exists(result))
                return result;

            _pdbCache.Remove(entry);
            return null;
        }

        private void SetPdbEntry(HashSet<PdbEntry> missing, PdbEntry entry, string value)
        {
            if (value == null)
                missing.Add(entry);
            else
                _pdbCache[entry] = value;
        }
#endif
    }

    internal struct FileEntry : IEquatable<FileEntry>
    {
        public string FileName;
        public int TimeStamp;
        public int FileSize;

        public FileEntry(string filename, int timestamp, int filesize)
        {
            FileName = filename;
            TimeStamp = timestamp;
            FileSize = filesize;
        }

        public override int GetHashCode()
        {
            return FileName.ToLower().GetHashCode() ^ TimeStamp ^ FileSize;
        }

        public override bool Equals(object obj)
        {
            return obj is FileEntry && Equals((FileEntry)obj);
        }

        public bool Equals(FileEntry other)
        {
            return FileName.Equals(other.FileName, StringComparison.OrdinalIgnoreCase) && TimeStamp == other.TimeStamp && FileSize == other.FileSize;
        }
    }

    internal struct PdbEntry : IEquatable<PdbEntry>
    {
        public string FileName;
        public Guid Guid;
        public int Revision;

        public PdbEntry(string filename, Guid guid, int revision)
        {
            FileName = filename;
            Guid = guid;
            Revision = revision;
        }

        public override int GetHashCode()
        {
            return FileName.ToLower().GetHashCode() ^ Guid.GetHashCode() ^ Revision;
        }

        public override bool Equals(object obj)
        {
            return obj is PdbEntry && Equals((PdbEntry)obj);
        }

        public bool Equals(PdbEntry other)
        {
            return Revision == other.Revision && FileName.Equals(other.FileName, StringComparison.OrdinalIgnoreCase) && Guid == other.Guid;
        }
    }

    internal class FileLoader : ICorDebug.ICLRDebuggingLibraryProvider
    {
        private Dictionary<string, PEFile> _pefileCache = new Dictionary<string, PEFile>(StringComparer.OrdinalIgnoreCase);
        private DataTarget _dataTarget;
        
        public FileLoader(DataTarget dt)
        {
            _dataTarget = dt;
        }

        public PEFile LoadPEFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;

            PEFile result;
            if (_pefileCache.TryGetValue(fileName, out result))
            {
                if (!result.Disposed)
                    return result;

                _pefileCache.Remove(fileName);
            }

            try
            {
                result = new PEFile(fileName);
                _pefileCache[fileName] = result;
            }
            catch
            {
                result = null;
            }

            return result;
        }

        public int ProvideLibrary([In, MarshalAs(UnmanagedType.LPWStr)] string fileName, int timestamp, int sizeOfImage, out IntPtr hModule)
        {
            string result = _dataTarget.SymbolLocator.FindBinary(fileName, timestamp, sizeOfImage, false);
            if (result == null)
            {
                hModule = IntPtr.Zero;
                return -1;
            }

            hModule = NativeMethods.LoadLibrary(result);
            return 0;
        }
    }
}
