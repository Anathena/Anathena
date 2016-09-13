namespace Anna.Source.Engine.OperatingSystems.Windows.Enumerations

open System
open System.Runtime.InteropServices

/// <summary>
/// Contains native platform invoke definitions for Windows
/// </summary>
module Enumerations = 
    [<DllImport("shell32.dll", SetLastError = true)>]
    extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex)