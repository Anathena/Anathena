using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace Anathema.Source.SystemInternals.Graphics.DirectX.Interface
{
    internal abstract class BaseDXHook
    {
        protected List<Hook> Hooks = new List<Hook>();
        public DirextXGraphicsInterface GraphicsInterface { get; set; }
        protected virtual String HookName { get { return "BaseDXHook"; } }

        public BaseDXHook(DirextXGraphicsInterface GraphicsInterface)
        {
            this.GraphicsInterface = GraphicsInterface;
        }

        ~BaseDXHook()
        {
            Dispose(false);
        }

        protected void Frame() { }

        protected IntPtr[] GetVirtualTableAddresses(IntPtr Pointer, Int32 NumberOfMethods)
        {
            return GetVirtualTableAddresses(Pointer, 0, NumberOfMethods);
        }

        protected IntPtr[] GetVirtualTableAddresses(IntPtr Pointer, Int32 StartIndex, Int32 NumberOfMethods)
        {
            List<IntPtr> VirtualTableAddresses = new List<IntPtr>();
            IntPtr VirtualTablePtr = Marshal.ReadIntPtr(Pointer);

            for (Int32 Index = StartIndex; Index < StartIndex + NumberOfMethods; Index++)
                VirtualTableAddresses.Add(Marshal.ReadIntPtr(VirtualTablePtr, Index * IntPtr.Size)); // using IntPtr.Size allows us to support both 32 and 64-bit processes

            return VirtualTableAddresses.ToArray();
        }

        public abstract void Hook();

        public abstract void Cleanup();

        protected void Dispose(Boolean DisposeManagedResources)
        {
            // Only clean up managed objects if disposing (i.e. not called from destructor)
            if (DisposeManagedResources)
            {
                try
                {
                    Cleanup();
                }
                catch { }

                try
                {
                    // Uninstall Hooks
                    if (Hooks.Count > 0)
                    {
                        // First disable the hook (by excluding all threads) and wait long enough to ensure that all hooks are not active
                        foreach (Hook Hook in Hooks)
                            Hook.Deactivate();

                        Thread.Sleep(100);

                        // Now we can dispose of the hooks (which triggers the removal of the hook)
                        foreach (Hook Hook in Hooks)
                            Hook.Dispose();

                        Hooks.Clear();
                    }
                }
                catch
                {

                }
            }

            // base.Dispose(DisposeManagedResources);
        }

    } // End class

} // End namespace