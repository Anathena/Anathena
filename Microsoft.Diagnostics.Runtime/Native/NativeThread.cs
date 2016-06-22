using System;
using System.Collections.Generic;
using Microsoft.Diagnostics.Runtime.Desktop;

namespace Microsoft.Diagnostics.Runtime.Native
{
    class NativeThread : ThreadBase
    {
        private NativeRuntime _runtime;

        public NativeThread(NativeRuntime runtime, IThreadData thread, ulong address, bool finalizer) : base(thread, address, finalizer)
        {
            _runtime = runtime;
        }

        public override IList<BlockingObject> BlockingObjects
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override ClrException CurrentException
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override ClrRuntime Runtime
        {
            get
            {
                return _runtime;
            }
        }

        public override ulong StackBase
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override ulong StackLimit
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override IList<ClrStackFrame> StackTrace
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override IEnumerable<ClrRoot> EnumerateStackObjects()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<ClrRoot> EnumerateStackObjects(bool includePossiblyDead)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<ClrStackFrame> EnumerateStackTrace()
        {
            throw new NotImplementedException();
        }
    }
}
