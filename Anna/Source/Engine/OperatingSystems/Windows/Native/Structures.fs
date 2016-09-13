namespace Anna.Source.Engine.OperatingSystems.Windows.Structures

open System
open System.Runtime.InteropServices

/// <summary>
/// Contains native platform invoke definitions for Windows
/// </summary>
module Structures = 
    [<DllImport("shell32.dll", SetLastError = true)>]
    extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex)