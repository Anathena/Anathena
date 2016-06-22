using System;

namespace Microsoft.Diagnostics.Runtime
{
    /// <summary>
    /// While ClrMD provides a managed PDB reader and PDB locator, it would be inefficient to load our own PDB
    /// reader into memory if the user already has one available.  For ClrMD operations which require reading data
    /// from PDBs, you will need to provide this implementation.  (This is currently only required for debugging
    /// .Net Native applications).
    /// </summary>
    public interface ISymbolProvider
    {
        /// <summary>
        /// Loads a PDB by its given guid/age and provides an ISymbolResolver for that PDB.
        /// </summary>
        /// <param name="pdbName">The name of the pdb.  This may be a full path and not just a simple name.</param>
        /// <param name="guid">The guid of the pdb to locate.</param>
        /// <param name="age">The age of the pdb to locate.</param>
        /// <returns>A symbol resolver for the given pdb.  Null if none was found.</returns>
        ISymbolResolver GetSymbolResolver(string pdbName, Guid guid, int age);
    }

    /// <summary>
    /// ISymbolResolver represents a single symbol module (PDB) loaded into the process.
    /// </summary>
    public interface ISymbolResolver
    {
        /// <summary>
        /// Retrieves the given symbol's name based on its RVA.
        /// </summary>
        /// <param name="rva">A relative virtual address in the module.</param>
        /// <returns>The symbol corresponding to RVA.</returns>
        string GetSymbolNameByRVA(uint rva);
    }
}
