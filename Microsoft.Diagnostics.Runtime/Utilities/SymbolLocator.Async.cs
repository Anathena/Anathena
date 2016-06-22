// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Diagnostics.Runtime.Utilities
{
    public abstract partial class SymbolLocator
    {
        /// <summary>
        /// Attempts to locate a binary via the symbol server.  This function will then copy the file
        /// locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="fileName">The filename that the binary is indexed under.</param>
        /// <param name="buildTimeStamp">The build timestamp the binary is indexed under.</param>
        /// <param name="imageSize">The image size the binary is indexed under.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        public async Task<string> FindBinaryAsync(string fileName, uint buildTimeStamp, uint imageSize, bool checkProperties = true)
        {
            return await FindBinaryAsync(fileName, (int)buildTimeStamp, (int)imageSize, checkProperties);
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
        public abstract Task<string> FindBinaryAsync(string fileName, int buildTimeStamp, int imageSize, bool checkProperties = true);

        /// <summary>
        /// Attempts to locate a binary via the symbol server.  This function will then copy the file
        /// locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="module">The module to locate.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        public async Task<string> FindBinaryAsync(ModuleInfo module, bool checkProperties = true)
        {
            return await FindBinaryAsync(module.FileName, module.TimeStamp, module.FileSize, checkProperties);
        }

        /// <summary>
        /// Attempts to locate a dac via the symbol server.  This function will then copy the file
        /// locally to the symbol cache and return the location of the local file on disk.  Note that
        /// the dac should not validate if the properties of the file match the one it was indexed under.
        /// </summary>
        /// <param name="dac">The dac to locate.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        public async Task<string> FindBinaryAsync(DacInfo dac)
        {
            return await FindBinaryAsync(dac, false);
        }

        /// <summary>
        /// Attempts to locate the pdb for a given module.
        /// </summary>
        /// <param name="module">The module to locate the pdb for.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        public async Task<string> FindPdbAsync(ModuleInfo module)
        {
            if (module == null)
                throw new ArgumentNullException("module");

            PdbInfo pdb = module.Pdb;
            if (pdb == null)
                return null;

            return await FindPdbAsync(pdb);
        }

        /// <summary>
        /// Attempts to locate the pdb for a given module.
        /// </summary>
        /// <param name="pdb">The pdb to locate.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        public async Task<string> FindPdbAsync(PdbInfo pdb)
        {
            if (pdb == null)
                throw new ArgumentNullException("pdb");

            return await FindPdbAsync(pdb.FileName, pdb.Guid, pdb.Revision);
        }

        /// <summary>
        /// Attempts to locate a pdb based on its name, guid, and revision number.
        /// </summary>
        /// <param name="pdbName">The name the pdb is indexed under.</param>
        /// <param name="pdbIndexGuid">The guid the pdb is indexed under.</param>
        /// <param name="pdbIndexAge">The age of the pdb.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        public abstract Task<string> FindPdbAsync(string pdbName, Guid pdbIndexGuid, int pdbIndexAge);

        /// <summary>
        /// Copies the given file from the input stream into fullDestPath.
        /// </summary>
        /// <param name="input">The input stream to copy the file from.</param>
        /// <param name="fullSrcPath">The source of this file.  This is for informational/logging purposes and shouldn't be opened directly.</param>
        /// <param name="fullDestPath">The destination path of where the file should go on disk.</param>
        /// <param name="size">The length of the given file.  (Also for informational purposes, do not use this as part of a copy loop.</param>
        /// <returns>A task indicating when the copy is completed.</returns>
        protected abstract Task CopyStreamToFileAsync(Stream input, string fullSrcPath, string fullDestPath, long size);
    }


    /// <summary>
    /// In v4.5, this class supports multithreading.
    /// </summary>
    public partial class DefaultSymbolLocator : SymbolLocator
    {
        private static Dictionary<PdbEntry, Task<string>> s_pdbs = new Dictionary<PdbEntry, Task<string>>();
        private static Dictionary<FileEntry, Task<string>> s_files = new Dictionary<FileEntry, Task<string>>();
        private static Dictionary<string, Task> s_copy = new Dictionary<string, Task>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Attempts to locate a binary via the symbol server.  This function will then copy the file
        /// locally to the symbol cache and return the location of the local file on disk.
        /// </summary>
        /// <param name="fileName">The filename that the binary is indexed under.</param>
        /// <param name="buildTimeStamp">The build timestamp the binary is indexed under.</param>
        /// <param name="imageSize">The image size the binary is indexed under.</param>
        /// <param name="checkProperties">Whether or not to validate the properties of the binary after download.</param>
        /// <returns>A full path on disk (local) of where the binary was copied to, null if it was not found.</returns>
        public override async Task<string> FindBinaryAsync(string fileName, int buildTimeStamp, int imageSize, bool checkProperties = true)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException(nameof(fileName));

            string simpleFilename = Path.GetFileName(fileName);
            FileEntry fileEntry = new FileEntry(simpleFilename, buildTimeStamp, imageSize);

            var missingFiles = _missingFiles;

            Task<string> task = null;
            lock (s_files)
            {
                if (IsMissing(missingFiles, fileEntry))
                    return null;

                if (!s_files.TryGetValue(fileEntry, out task))
                    task = s_files[fileEntry] = DownloadFileWorker(fileName, simpleFilename, buildTimeStamp, imageSize, checkProperties);
            }

            // If we failed to find the file, we need to clear out the empty task, since the user could
            // change symbol paths and we need s_files to only contain positive results.
            string result = await task;
            if (result == null)
                ClearFailedTask(s_files, task, missingFiles, fileEntry);

            return result;
        }

        /// <summary>
        /// Attempts to locate a pdb based on its name, guid, and revision number.
        /// </summary>
        /// <param name="pdbName">The name the pdb is indexed under.</param>
        /// <param name="pdbIndexGuid">The guid the pdb is indexed under.</param>
        /// <param name="pdbIndexAge">The age of the pdb.</param>
        /// <returns>A full path on disk (local) of where the pdb was copied to.</returns>
        public async override Task<string> FindPdbAsync(string pdbName, Guid pdbIndexGuid, int pdbIndexAge)
        {
            if (string.IsNullOrWhiteSpace(pdbName))
                throw new ArgumentNullException(nameof(pdbName));

            // Ensure we don't attempt to download the same pdb multiple times.
            string pdbSimpleName = Path.GetFileName(pdbName);
            PdbEntry pdbEntry = new PdbEntry(pdbSimpleName, pdbIndexGuid, pdbIndexAge);

            var missingPdbs = _missingPdbs;

            Task<string> task = null;
            lock (s_pdbs)
            {
                if (IsMissing(missingPdbs, pdbEntry))
                    return null;

                if (!s_pdbs.TryGetValue(pdbEntry, out task))
                    task = s_pdbs[pdbEntry] = DownloadPdbWorker(pdbName, pdbSimpleName, pdbIndexGuid, pdbIndexAge);
            }

            // If we failed to find the file, we need to clear out the empty task, since the user could
            // change symbol paths and we need s_files to only contain positive results.
            string result = await task;
            if (result == null)
                ClearFailedTask(s_pdbs, task, missingPdbs, pdbEntry);

            return result;
        }

        private static void ClearFailedTask<T>(Dictionary<T, Task<string>> tasks, Task<string> task, HashSet<T> missingFiles, T fileEntry)
        {
            lock (tasks)
            {
                Task<string> tmp;
                if (tasks.TryGetValue(fileEntry, out tmp) && tmp == task)
                    tasks.Remove(fileEntry);

                lock (missingFiles)
                    missingFiles.Add(fileEntry);
            }
        }

        private async Task<string> DownloadPdbWorker(string pdbFullPath, string pdbSimpleName, Guid pdbIndexGuid, int pdbIndexAge)
        {
            string pdbIndexPath = GetIndexPath(pdbSimpleName, pdbIndexGuid, pdbIndexAge);
            string cachePath = Path.Combine(SymbolCache, pdbIndexPath);

            Func<string, bool> match = (file) => ValidatePdb(file, pdbIndexGuid, pdbIndexAge);
            string result = CheckLocalPaths(pdbFullPath, pdbSimpleName, cachePath, match);
            if (result != null)
                return result;

            result = await SearchSymbolServerForFile(pdbSimpleName, pdbIndexPath, match);
            return result;
        }

        private async Task<string> DownloadFileWorker(string fileFullPath, string fileSimpleName, int buildTimeStamp, int imageSize, bool checkProperties)
        {
            string fileIndexPath = GetIndexPath(fileSimpleName, buildTimeStamp, imageSize);
            string cachePath = Path.Combine(SymbolCache, fileIndexPath);

            Func<string, bool> match = (file) => ValidateBinary(file, buildTimeStamp, imageSize, checkProperties);
            string result = CheckLocalPaths(fileFullPath, fileSimpleName, cachePath, match);
            if (result != null)
            {
                Trace("Found '{0}' locally on path '{1}'.", fileSimpleName, result);
                return result;
            }

            result = await SearchSymbolServerForFile(fileSimpleName, fileIndexPath, match);
            return result;
        }

        private string CheckLocalPaths(string fullName, string simpleName, string fullDestPath, Func<string, bool> matches)
        {
            // We were given a full path instead of simply "foo.bar".
            if (fullName != simpleName)
            {
                if (matches(fullName))
                    return fullName;
            }

            // Check the target path too.
            if (File.Exists(fullDestPath))
            {
                if (matches(fullDestPath))
                    return fullDestPath;

                // We somehow got a bad file here...this shouldn't have happened.
                File.Delete(fullDestPath);
            }

            return null;
        }

        private async Task<string> SearchSymbolServerForFile(string fileSimpleName, string fileIndexPath, Func<string, bool> match)
        {
            var tasks = new List<Task<string>>();
            foreach (var element in SymPathElement.GetElements(SymbolPath))
            {
                if (element.IsSymServer)
                {
                    tasks.Add(TryGetFileFromServerAsync(element.Target, fileIndexPath, element.Cache ?? SymbolCache));
                }
                else
                {
                    string fullDestPath = Path.Combine(element.Cache ?? SymbolCache, fileIndexPath);
                    string sourcePath = Path.Combine(element.Target, fileSimpleName);
                    tasks.Add(CheckAndCopyRemoteFile(sourcePath, fullDestPath, match));
                }
            }

            string result = await tasks.GetFirstNonNullResult();
            return result;
        }


        private async Task<string> CheckAndCopyRemoteFile(string sourcePath, string fullDestPath, Func<string, bool> matches)
        {
            if (!matches(sourcePath))
                return null;

            try
            {
                using (Stream stream = File.OpenRead(sourcePath))
                    await CopyStreamToFileAsync(stream, sourcePath, fullDestPath, stream.Length);

                return fullDestPath;
            }
            catch (Exception e)
            {
                Trace("Error copying file '{0}' to '{1}': {2}", sourcePath, fullDestPath, e);
            }

            return null;
        }

        private async Task<string> TryGetFileFromServerAsync(string urlForServer, string fileIndexPath, string cache)
        {
            string fullDestPath = Path.Combine(cache, fileIndexPath);
            Debug.Assert(!string.IsNullOrWhiteSpace(cache));
            if (String.IsNullOrWhiteSpace(urlForServer))
                return null;

            // There are three ways symbol files can be indexed.  Start looking for each one.

            // First, check for the compressed location.  This is the one we really want to download.
            string compressedFilePath = fileIndexPath.Substring(0, fileIndexPath.Length - 1) + "_";
            string compressedFileTarget = Path.Combine(cache, compressedFilePath);

            TryDeleteFile(compressedFileTarget);
            Task<string> compressedFilePathDownload = GetPhysicalFileFromServerAsync(urlForServer, compressedFilePath, compressedFileTarget);

            // Second, check if the raw file itself is indexed, uncompressed.
            Task<string> rawFileDownload = GetPhysicalFileFromServerAsync(urlForServer, fileIndexPath, fullDestPath);

            // Last, check for a redirection link.
            string filePtrSigPath = Path.Combine(Path.GetDirectoryName(fileIndexPath), "file.ptr");
            Task<string> filePtrDownload = GetPhysicalFileFromServerAsync(urlForServer, filePtrSigPath, fullDestPath, returnContents: true);


            // Handle compressed download.
            string result = await compressedFilePathDownload;
            if (result != null)
            {
                try
                {
                    // Decompress it
                    Command.Run("Expand " + Command.Quote(result) + " " + Command.Quote(fullDestPath));
                    Trace($"Found '{Path.GetFileName(fileIndexPath)}' on server '{urlForServer}'.  Copied to '{fullDestPath}'.");
                    return fullDestPath;
                }
                catch (Exception e)
                {
                    Trace("Exception encountered while expanding file '{0}': {1}", result, e.Message);
                }
                finally
                {
                    if (File.Exists(result))
                        File.Delete(result);
                }
            }

            // Handle uncompressed download.
            result = await rawFileDownload;
            if (result != null)
            {
                Trace($"Found '{Path.GetFileName(fileIndexPath)}' on server '{urlForServer}'.  Copied to '{result}'.");
                return result;
            }

            // Handle redirection case.
            var filePtrData = (await filePtrDownload ?? "").Trim();
            if (filePtrData.StartsWith("PATH:"))
                filePtrData = filePtrData.Substring(5);

            if (!filePtrData.StartsWith("MSG:") && File.Exists(filePtrData))
            {
                try
                {
                    using (FileStream input = File.OpenRead(filePtrData))
                        await CopyStreamToFileAsync(input, filePtrSigPath, fullDestPath, input.Length);

                    Trace($"Found '{Path.GetFileName(fileIndexPath)}' on server '{urlForServer}'.  Copied to '{fullDestPath}'.");
                    return fullDestPath;
                }
                catch (Exception)
                {
                    Trace("Error copying from file.ptr: content '{0}' from '{1}' to '{2}'.", filePtrData, filePtrSigPath, fullDestPath);
                }
            }
            else if (!string.IsNullOrWhiteSpace(filePtrData))
            {
                Trace("Error resolving file.ptr: content '{0}' from '{1}'.", filePtrData, filePtrSigPath);
            }

            Trace($"No file matching '{Path.GetFileName(fileIndexPath)}' found on server '{urlForServer}'.");
            return null;
        }

        private void TryDeleteFile(string file)
        {
            if (File.Exists(file))
            {
                try
                {
                    File.Delete(file);
                }
                catch
                {
                    // Ignore failure here.
                }
            }
        }

        private async Task<string> GetPhysicalFileFromServerAsync(string serverPath, string fileIndexPath, string fullDestPath, bool returnContents = false)
        {
            if (string.IsNullOrEmpty(serverPath))
                return null;

            if (File.Exists(fullDestPath))
            {
                if (returnContents)
                    return File.ReadAllText(fullDestPath);

                return fullDestPath;
            }

            if (IsHttp(serverPath))
            {
                string fullUri = serverPath + "/" + fileIndexPath.Replace('\\', '/');
                try
                {
                    var req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(fullUri);
                    req.UserAgent = "Microsoft-Symbol-Server/6.13.0009.1140";
                    req.Timeout = Timeout;
                    var response = await req.GetResponseAsync();
                    using (var fromStream = response.GetResponseStream())
                    {
                        if (returnContents)
                            return await new StreamReader(fromStream).ReadToEndAsync();

                        Directory.CreateDirectory(Path.GetDirectoryName(fullDestPath));
                        await CopyStreamToFileAsync(fromStream, fullUri, fullDestPath, response.ContentLength);
                        Trace("Found '{0}' at '{1}'.  Copied to '{2}'.", Path.GetFileName(fileIndexPath), fullUri, fullDestPath);
                        return fullDestPath;
                    }
                }
                catch (System.Net.WebException)
                {
                    // Is probably just a 404, which happens normally.
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
                var fullSrcPath = Path.Combine(serverPath, fileIndexPath);
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
                    await CopyStreamToFileAsync(fs, fullSrcPath, fullDestPath, fs.Length);

                return fullDestPath;
            }
        }

        private static bool IsHttp(string server)
        {
            return server.StartsWith("http:", StringComparison.CurrentCultureIgnoreCase) || server.StartsWith("https:", StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Clear missing file/pdb cache
        /// </summary>
        protected override void SymbolPathOrCacheChanged()
        {
            _missingFiles = new HashSet<FileEntry>();
            _missingPdbs = new HashSet<PdbEntry>();
        }

        private static bool IsMissing<T>(HashSet<T> entries, T entry)
        {
            lock (entries)
                return entries.Contains(entry);
        }


        /// <summary>
        /// Copies the given file from the input stream into fullDestPath.
        /// </summary>
        /// <param name="stream">The input stream to copy the file from.</param>
        /// <param name="fullSrcPath">The source of this file.  This is for informational/logging purposes and shouldn't be opened directly.</param>
        /// <param name="fullDestPath">The destination path of where the file should go on disk.</param>
        /// <param name="size">The length of the given file.  (Also for informational purposes, do not use this as part of a copy loop.</param>
        /// <returns>A task indicating when the copy is completed.</returns>
        protected override void CopyStreamToFile(Stream stream, string fullSrcPath, string fullDestPath, long size)
        {
            CopyStreamToFileAsync(stream, fullSrcPath, fullDestPath, size).Wait();
        }

        /// <summary>
        /// Copies the given file from the input stream into fullDestPath.
        /// </summary>
        /// <param name="input">The input stream to copy the file from.</param>
        /// <param name="fullSrcPath">The source of this file.  This is for informational/logging purposes and shouldn't be opened directly.</param>
        /// <param name="fullDestPath">The destination path of where the file should go on disk.</param>
        /// <param name="size">The length of the given file.  (Also for informational purposes, do not use this as part of a copy loop.</param>
        /// <returns>A task indicating when the copy is completed.</returns>
        protected override async Task CopyStreamToFileAsync(Stream input, string fullSrcPath, string fullDestPath, long size)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullDestPath));

            Task result;
            FileStream output = null;
            try
            {
                lock (s_copy)
                {
                    if (!s_copy.TryGetValue(fullDestPath, out result))
                    {
                        if (File.Exists(fullDestPath))
                            return;

                        try
                        {
                            Trace("Copying '{0}' from '{1}' to '{2}'.", Path.GetFileName(fullDestPath), fullSrcPath, fullDestPath);

                            output = new FileStream(fullDestPath, FileMode.CreateNew);
                            s_copy[fullDestPath] = result = input.CopyToAsync(output);
                        }
                        catch (Exception e)
                        {
                            Trace("Encountered an error while attempting to copy '{0} to '{1}': {2}", fullSrcPath, fullDestPath, e.Message);
                        }
                    }
                }

                await result;
            }
            finally
            {
                if (output != null)
                    output.Dispose();
            }
        }

        private string GetFileEntry(FileEntry entry)
        {
            lock (s_files)
            {
                Task<string> task;
                if (s_files.TryGetValue(entry, out task))
                    return task.Result;
            }

            return null;
        }

        private void SetFileEntry(HashSet<FileEntry> missingFiles, FileEntry entry, string value)
        {
            if (value != null)
            {
                lock (s_files)
                {
                    if (!s_files.ContainsKey(entry))
                    {
                        Task<string> task = new Task<string>(() => value);
                        s_files[entry] = task;
                        task.Start();
                    }
                }
            }
            else
            {
                lock (missingFiles)
                    missingFiles.Add(entry);
            }
        }


        private string GetPdbEntry(PdbEntry entry)
        {
            lock (s_pdbs)
            {
                Task<string> task;
                if (s_pdbs.TryGetValue(entry, out task))
                    return task.Result;
            }

            return null;
        }

        private void SetPdbEntry(HashSet<PdbEntry> missing, PdbEntry entry, string value)
        {
            if (value != null)
            {
                lock (s_pdbs)
                {
                    if (!s_pdbs.ContainsKey(entry))
                    {
                        Task<string> task = new Task<string>(() => value);
                        s_pdbs[entry] = task;
                        task.Start();
                    }
                }
            }
            else
            {
                lock (missing)
                    missing.Add(entry);
            }
        }

        internal override void PrefetchBinary(string name, int timestamp, int imagesize)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(name))
                    new Task(async () => await FindBinaryAsync(name, timestamp, imagesize, true)).Start();
            }
            catch (Exception e)
            {
                // Background fetching binaries should never cause an exception that will tear down the process
                // (which would be the case since this is done on a background worker thread with no one around
                // to handle it).  We will swallow all exceptions here, but fail in debug builds.
                Debug.Fail(e.ToString());
            }
        }
    }


    internal static class AsyncHelpers
    {
        public static async Task<T> GetFirstNonNullResult<T>(this List<Task<T>> tasks) where T : class
        {
            while (tasks.Count > 0)
            {
                Task<T> task = await Task.WhenAny(tasks);

                T result = task.Result;
                if (result != null)
                    return result;

                if (tasks.Count == 1)
                    break;

                tasks.Remove(task);
            }

            return null;
        }
    }
}