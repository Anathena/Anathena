// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

#pragma warning disable 1591

namespace Microsoft.Diagnostics.Runtime.Interop
{
    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("61a4905b-23f9-4247-b3c5-53d087529ab7")]
    public unsafe interface IDebugEventContextCallbacks
    {
        [PreserveSig]
        int GetInterestMask(
            [Out] out DEBUG_EVENT Mask);

        [PreserveSig]
        int Breakpoint(
            [In, MarshalAs(UnmanagedType.Interface)] IDebugBreakpoint2 Bp,
            [In] DEBUG_EVENT_CONTEXT* Context,
            [In] uint ContextSize);

        [PreserveSig]
        int Exception(
            [In] ref EXCEPTION_RECORD64 Exception,
            [In] UInt32 FirstChance,
            [In] DEBUG_EVENT_CONTEXT* Context,
            [In] uint ContextSize);

        [PreserveSig]
        int CreateThread(
            [In] UInt64 Handle,
            [In] UInt64 DataOffset,
            [In] UInt64 StartOffset,
            [In] DEBUG_EVENT_CONTEXT* Context,
            [In] uint ContextSize);

        [PreserveSig]
        int ExitThread(
            [In] UInt32 ExitCode,
            [In] DEBUG_EVENT_CONTEXT* Context,
            [In] uint ContextSize);

        [PreserveSig]
        int CreateProcess(
            [In] UInt64 ImageFileHandle,
            [In] UInt64 Handle,
            [In] UInt64 BaseOffset,
            [In] UInt32 ModuleSize,
            [In, MarshalAs(UnmanagedType.LPWStr)] string ModuleName,
            [In, MarshalAs(UnmanagedType.LPWStr)] string ImageName,
            [In] UInt32 CheckSum,
            [In] UInt32 TimeDateStamp,
            [In] UInt64 InitialThreadHandle,
            [In] UInt64 ThreadDataOffset,
            [In] UInt64 StartOffset,
            [In] DEBUG_EVENT_CONTEXT* Context,
            [In] uint ContextSize);

        [PreserveSig]
        int ExitProcess(
            [In] UInt32 ExitCode,
            [In] DEBUG_EVENT_CONTEXT* Context,
            [In] uint ContextSize);

        [PreserveSig]
        int LoadModule(
            [In] UInt64 ImageFileHandle,
            [In] UInt64 BaseOffset,
            [In] UInt32 ModuleSize,
            [In, MarshalAs(UnmanagedType.LPWStr)] string ModuleName,
            [In, MarshalAs(UnmanagedType.LPWStr)] string ImageName,
            [In] UInt32 CheckSum,
            [In] UInt32 TimeDateStamp,
            [In] DEBUG_EVENT_CONTEXT* Context,
            [In] uint ContextSize);

        [PreserveSig]
        int UnloadModule(
            [In, MarshalAs(UnmanagedType.LPWStr)] string ImageBaseName,
            [In] UInt64 BaseOffset,
            [In] DEBUG_EVENT_CONTEXT* Context,
            [In] uint ContextSize);

        [PreserveSig]
        int SystemError(
            [In] UInt32 Error,
            [In] UInt32 Level,
            [In] DEBUG_EVENT_CONTEXT* Context,
            [In] uint ContextSize);

        [PreserveSig]
        int SessionStatus(
            [In] DEBUG_SESSION Status);

        [PreserveSig]
        int ChangeDebuggeeState(
            [In] DEBUG_CDS Flags,
            [In] UInt64 Argument,
            [In] DEBUG_EVENT_CONTEXT* Context,
            [In] uint ContextSize);

        [PreserveSig]
        int ChangeEngineState(
            [In] DEBUG_CES Flags,
            [In] UInt64 Argument,
            [In] DEBUG_EVENT_CONTEXT* Context,
            [In] uint ContextSize);

        [PreserveSig]
        int ChangeSymbolState(
            [In] DEBUG_CSS Flags,
            [In] UInt64 Argument);
    }
}
