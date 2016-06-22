// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Diagnostics.Runtime.Desktop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Address = System.UInt64;

namespace Microsoft.Diagnostics.Runtime.Native
{
    internal class NativeException : ClrException
    {
        private Address _ccwPtr;
        private ClrType _type;
        private ulong _hResult;
        private ulong _threadId;
        private ulong _exceptionId;
        private ulong _innerExceptionId;
        private ulong _nestingLevel;
        private IList<ClrStackFrame> _stackFrames;

        private ClrException _innerException;

        public NativeException(
                        ClrType clrType,
                        Address ccwPtr,
                        ulong hResult,
                        ulong threadId,
                        ulong exceptionId,
                        ulong innerExceptionId,
                        ulong nestingLevel,
                        IList<ClrStackFrame> stackFrames)
        {
            _type = clrType;
            _ccwPtr = ccwPtr;
            _hResult = hResult;
            _threadId = threadId;
            _exceptionId = exceptionId;
            _innerExceptionId = innerExceptionId;
            _nestingLevel = nestingLevel;
            _stackFrames = stackFrames;
        }

        public override Address Address
        {
            get { return _ccwPtr; }
        }
        public override int HResult
        {
            get
            {
                return (int)_hResult;
            }
        }

        public override ClrType Type
        {
            get { return _type; }
        }

        public override string Message
        {
            get
            {
                return null;
            }
        }

        public override ClrException Inner
        {
            get
            {
                return _innerException;
            }
        }

        internal void setInnerException(ClrException inner)
        {
            _innerException = inner;
        }

        public override IList<ClrStackFrame> StackTrace
        {
            get
            {
                return _stackFrames;
            }
        }

        public ulong InnerExceptionId
        {
            get { return _innerExceptionId; }
        }

        public ulong ThreadId
        {
            get { return _threadId; }
        }

        public ulong NestingLevel
        {
            get { return _nestingLevel; }
        }

        public ulong ExceptionId
        {
            get { return _exceptionId; }
        }
    }
}
