using EasyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Capture.Hook
{
    // Thanks to remcoros for the initial version of the following helper classes

    /// <summary>
    /// Extends <see cref="Hook"/> with support for accessing the Original method from within a hook delegate
    /// </summary>
    /// <typeparam name="T">A delegate type</typeparam>
    public class Hook<T> : Hook
        where T: class
    {
        /// <summary>
        /// When called from within the <see cref="Hook.NewFunc"/> delegate this will call the original function at <see cref="Hook.FuncToHook"/>.
        /// </summary>
        public T Original { get; private set; }

        /// <summary>
        /// Creates a new hook at <paramref name="funcToHook"/> redirecting to <paramref name="newFunc"/>. The hook starts inactive so a call to <see cref="Activate"/> is required to enable the hook.
        /// </summary>
        /// <param name="funcToHook">A pointer to the location to insert the hook</param>
        /// <param name="newFunc">The delegate to call from the hooked location</param>
        /// <param name="owner">The object to assign as the "callback" object within the <see cref="EasyHook.LocalHook"/> instance.</param>
        public Hook(IntPtr funcToHook, Delegate newFunc, object owner)
            : base(funcToHook, newFunc, owner)
        {
            // Debug assertion that T is a Delegate type
            System.Diagnostics.Debug.Assert(typeof(Delegate).IsAssignableFrom(typeof(T)));

            Original = (T)(object)Marshal.GetDelegateForFunctionPointer(funcToHook, typeof(T));
        }
    }

    /// <summary>
    /// Wraps the <see cref="EasyHook.LocalHook"/> class with a simplified active/inactive state
    /// </summary>
    public class Hook: IDisposable
    {
        /// <summary>
        /// The hooked function location
        /// </summary>
        public IntPtr FuncToHook { get; private set; }
        
        /// <summary>
        /// The replacement delegate
        /// </summary>
        public Delegate NewFunc { get; private set; }
        
        /// <summary>
        /// The callback object passed to LocalHook constructor
        /// </summary>
        public object Owner { get; private set; }
        
        /// <summary>
        /// The <see cref="EasyHook.LocalHook"/> instance
        /// </summary>
        public LocalHook LocalHook { get; private set; }
        
        /// <summary>
        /// Indicates whether the hook is currently active
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Creates a new hook at <paramref name="funcToHook"/> redirecting to <paramref name="newFunc"/>. The hook starts inactive so a call to <see cref="Activate"/> is required to enable the hook.
        /// </summary>
        /// <param name="funcToHook">A pointer to the location to insert the hook</param>
        /// <param name="newFunc">The delegate to call from the hooked location</param>
        /// <param name="owner">The object to assign as the "callback" object within the <see cref="EasyHook.LocalHook"/> instance.</param>
        public Hook(IntPtr funcToHook, Delegate newFunc, object owner)
        {
            this.FuncToHook = funcToHook;
            this.NewFunc = newFunc;
            this.Owner = owner;
            
            CreateHook();
        }

        ~Hook()
        {
            Dispose(false);
        }

        protected void CreateHook()
        {
            if (LocalHook != null) return;

            this.LocalHook = LocalHook.Create(FuncToHook, NewFunc, Owner);
        }

        protected void UnHook()
        {
            if (this.IsActive)
                Deactivate();

            if (this.LocalHook != null)
            {
                this.LocalHook.Dispose();
                this.LocalHook = null;
            }
        }

        /// <summary>
        /// Activates the hook
        /// </summary>
        public void Activate()
        {
            if (this.LocalHook == null)
                CreateHook();

            if (this.IsActive) return;
            
            this.IsActive = true;
            this.LocalHook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });
        }

        /// <summary>
        /// Deactivates the hook
        /// </summary>
        public void Deactivate()
        {
            if (!this.IsActive) return;

            this.IsActive = false;
            this.LocalHook.ThreadACL.SetInclusiveACL(new Int32[] { 0 });
        }


        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposeManagedObjects)
        {
            // Only clean up managed objects if disposing (i.e. not called from destructor)
            if (disposeManagedObjects)
            {
                UnHook();
            }
        }
    }
}
