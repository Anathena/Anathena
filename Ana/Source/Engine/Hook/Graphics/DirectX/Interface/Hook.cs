namespace Ana.Source.Engine.Hook.Graphics.DirectX.Interface
{
    using EasyHook;
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Extends <see cref="Hook"/> with support for accessing the Original method from within a hook delegate
    /// </summary>
    /// <typeparam name="T">A delegate type</typeparam>
    internal class Hook<T> : Hook where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Hook{T}" /> class
        /// Creates a new hook at <paramref name="funcToHook"/> redirecting to <paramref name="newFunc"/>. The hook starts inactive so a call to <see cref="Activate"/> is required to enable the hook
        /// </summary>
        /// <param name="funcToHook">A pointer to the location to insert the hook</param>
        /// <param name="newFunc">The delegate to call from the hooked location</param>
        /// <param name="owner">The object to assign as the "callback" object within the <see cref="EasyHook.LocalHook"/> instance</param>
        public Hook(IntPtr funcToHook, Delegate newFunc, Object owner) : base(funcToHook, newFunc, owner)
        {
            Debug.Assert(typeof(Delegate).IsAssignableFrom(typeof(T)), "T must be Delegate type");
            this.Original = (T)(object)Marshal.GetDelegateForFunctionPointer(funcToHook, typeof(T));
        }

        /// <summary>
        /// Gets a value that when called from within the <see cref="Hook.NewFunc"/> delegate this will call the original function at <see cref="Hook.FuncToHook"/>
        /// </summary>
        public T Original { get; private set; }
    }
    //// End class

    /// <summary>
    /// Wraps the <see cref="EasyHook.LocalHook"/> class with a simplified active/inactive state
    /// </summary>
    internal class Hook : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Hook" /> class
        /// Creates a new hook at <paramref name="funcToHook"/> redirecting to <paramref name="newFunc"/>. The hook starts inactive so a call to <see cref="Activate"/> is required to enable the hook
        /// </summary>
        /// <param name="funcToHook">A pointer to the location to insert the hook</param>
        /// <param name="newFunc">The delegate to call from the hooked location</param>
        /// <param name="owner">The object to assign as the "callback" object within the <see cref="EasyHook.LocalHook"/> instance</param>
        public Hook(IntPtr funcToHook, Delegate newFunc, Object owner)
        {
            this.FuncToHook = funcToHook;
            this.NewFunc = newFunc;
            this.Owner = owner;

            this.CreateHook();
        }

        ~Hook()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the hooked function location
        /// </summary>
        public IntPtr FuncToHook { get; private set; }

        /// <summary>
        /// Gets the replacement delegate
        /// </summary>
        public Delegate NewFunc { get; private set; }

        /// <summary>
        /// Gets the callback object passed to LocalHook constructor
        /// </summary>
        public Object Owner { get; private set; }

        /// <summary>
        /// Gets the <see cref="EasyHook.LocalHook"/> instance
        /// </summary>
        public LocalHook LocalHook { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the hook is currently active
        /// </summary>
        public Boolean IsActive { get; private set; }

        /// <summary>
        /// Activates the hook
        /// </summary>
        public void Activate()
        {
            if (this.LocalHook == null)
            {
                this.CreateHook();
            }

            if (this.IsActive)
            {
                return;
            }

            this.IsActive = true;
            this.LocalHook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });
        }

        /// <summary>
        /// Deactivates the hook
        /// </summary>
        public void Deactivate()
        {
            if (!this.IsActive)
            {
                return;
            }

            this.IsActive = false;
            this.LocalHook.ThreadACL.SetInclusiveACL(new Int32[] { 0 });
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected void CreateHook()
        {
            if (this.LocalHook != null)
            {
                return;
            }

            this.LocalHook = LocalHook.Create(this.FuncToHook, this.NewFunc, this.Owner);
        }

        protected void UnHook()
        {
            if (this.IsActive)
            {
                this.Deactivate();
            }

            if (this.LocalHook != null)
            {
                this.LocalHook.Dispose();
                this.LocalHook = null;
            }
        }

        protected virtual void Dispose(Boolean disposeManagedObjects)
        {
            // Only clean up managed objects if disposing (i.e. not called from destructor)
            if (disposeManagedObjects)
            {
                this.UnHook();
            }
        }
    }
    //// End class
}
//// End namespace