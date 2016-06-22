// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace Microsoft.Diagnostics.Runtime
{

    /// <summary>
    /// Exception thrown by Microsoft.Diagnostics.Runtime unless there is a more appropriate
    /// exception subclass.
    /// </summary>
    public class ClrDiagnosticsException : Exception
    {
        /// <summary>
        /// Specific HRESULTS for errors.
        /// </summary>
        public enum HR
        {
            /// <summary>
            /// Unknown error occured.
            /// </summary>
            UnknownError = unchecked((int)(((ulong)(0x3) << 31) | ((ulong)(0x125) << 16) | ((ulong)(0x0)))),

            /// <summary>
            /// The dll of the specified runtime (mscorwks.dll or clr.dll) is loaded into the process, but
            /// has not actually been initialized and thus cannot be debugged.
            /// </summary>
            RuntimeUninitialized = UnknownError + 1,

            /// <summary>
            /// Something unexpected went wrong with the debugger we used to attach to the process or load
            /// the crash dump.
            /// </summary>
            DebuggerError,

            /// <summary>
            /// Something unexpected went wrong when requesting data from the target process.
            /// </summary>
            DataRequestError,

            /// <summary>
            /// Hit an unexpected (non-recoverable) dac error.
            /// </summary>
            DacError,

            /// <summary>
            /// The caller attempted to re-use an object after calling ClrRuntime.Flush.  See the
            /// documentation for ClrRuntime.Flush for more details.
            /// </summary>
            RevisionError,

            /// <summary>
            /// An error occurred while processing the given crash dump.
            /// </summary>
            CrashDumpError,

            /// <summary>
            /// There is an issue with the configuration of this application.
            /// </summary>
            ApplicationError,
        }

        /// <summary>
        /// The HRESULT of this exception.
        /// </summary>
        public new int HResult { get { return base.HResult; } }

        #region Functions
        internal ClrDiagnosticsException(string message)
            : base(message)
        {
            base.HResult = (int)HR.UnknownError;
        }

        internal ClrDiagnosticsException(string message, HR hr)
            : base(message)
        {
            base.HResult = (int)hr;
        }
        #endregion

        internal static void ThrowRevisionError(int revision, int runtimeRevision)
        {
            throw new ClrDiagnosticsException(string.Format("You must not reuse any object other than ClrRuntime after calling flush!\nClrModule revision ({0}) != ClrRuntime revision ({1}).", revision, runtimeRevision), ClrDiagnosticsException.HR.RevisionError);
        }
    }
}
