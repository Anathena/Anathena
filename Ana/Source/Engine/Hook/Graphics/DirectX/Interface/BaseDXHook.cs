namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Threading;

    /// <summary>
    /// Base class for all DirectX versions to be hooked in a target process
    /// </summary>
    internal abstract class BaseDXHook
    {
        public BaseDXHook(DirextXGraphicsInterface graphicsInterface)
        {
            this.Hooks = new List<Hook>();
            this.GraphicsInterface = graphicsInterface;
        }

        ~BaseDXHook()
        {
            this.Dispose(false);
        }

        public DirextXGraphicsInterface GraphicsInterface { get; set; }

        protected List<Hook> Hooks { get; set; }

        public abstract void Hook();

        public abstract void Cleanup();

        protected void Frame()
        {
        }

        protected IntPtr[] GetVirtualTableAddresses(IntPtr pointer, Int32 numberOfMethods)
        {
            return this.GetVirtualTableAddresses(pointer, 0, numberOfMethods);
        }

        protected IntPtr[] GetVirtualTableAddresses(IntPtr pointer, Int32 startIndex, Int32 numberOfMethods)
        {
            List<IntPtr> virtualTableAddresses = new List<IntPtr>();
            IntPtr virtualTablePtr = Marshal.ReadIntPtr(pointer);

            for (Int32 index = startIndex; index < startIndex + numberOfMethods; index++)
            {
                virtualTableAddresses.Add(Marshal.ReadIntPtr(virtualTablePtr, index * IntPtr.Size));
            }

            return virtualTableAddresses.ToArray();
        }

        protected void Dispose(Boolean disposeManagedResources)
        {
            // Only clean up managed objects if disposing (i.e. not called from destructor)
            if (disposeManagedResources)
            {
                try
                {
                    this.Cleanup();
                }
                catch
                {
                }

                try
                {
                    // Uninstall Hooks
                    if (this.Hooks.Count > 0)
                    {
                        // First disable the hook (by excluding all threads) and wait long enough to ensure that all hooks are not active
                        foreach (Hook hook in this.Hooks)
                        {
                            hook.Deactivate();
                        }

                        Thread.Sleep(100);

                        // Now we can dispose of the hooks (which triggers the removal of the hook)
                        foreach (Hook hook in this.Hooks)
                        {
                            hook.Dispose();
                        }

                        this.Hooks.Clear();
                    }
                }
                catch
                {
                }
            }
        }
    }
    //// End class
}
//// End namespace