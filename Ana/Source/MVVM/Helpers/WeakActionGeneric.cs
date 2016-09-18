namespace Ana.Source.Mvvm.Helpers
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Stores an Action without causing a hard reference
    /// to be created to the Action's owner. The owner can be garbage collected at any time.
    /// </summary>
    /// <typeparam name="T">The type of the Action's parameter.</typeparam>
    public class WeakAction<T> : WeakAction, IExecuteWithObject
    {
        /// <summary>
        /// TODO TODO
        /// </summary>
        private Action<T> staticAction;

        /// <summary>
        /// Initializes a new instance of the WeakAction class.
        /// </summary>
        /// <param name="action">The action that will be associated to this instance.</param>
        public WeakAction(Action<T> action) : this(action == null ? null : action.Target, action)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WeakAction class.
        /// </summary>
        /// <param name="target">The action's owner.</param>
        /// <param name="action">The action that will be associated to this instance.</param>
        [SuppressMessage(
            "Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            MessageId = "1",
            Justification = "Method should fail with an exception if action is null.")]
        public WeakAction(Object target, Action<T> action)
        {
            if (action.Method.IsStatic)
            {
                this.staticAction = action;

                if (target != null)
                {
                    // Keep a reference to the target to control the WeakAction's lifetime.
                    this.Reference = new WeakReference(target);
                }

                return;
            }

            this.Method = action.Method;
            this.ActionReference = new WeakReference(action.Target);

            this.Reference = new WeakReference(target);
        }

        /// <summary>
        /// Gets the name of the method that this WeakAction represents.
        /// </summary>
        public override String MethodName
        {
            get
            {
                if (this.staticAction != null)
                {
                    return this.staticAction.Method.Name;
                }

                return this.Method.Name;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the Action's owner is still alive, or if it was collected
        /// by the Garbage Collector already.
        /// </summary>
        public override Boolean IsAlive
        {
            get
            {
                if (this.staticAction == null && this.Reference == null)
                {
                    return false;
                }

                if (this.staticAction != null)
                {
                    if (this.Reference != null)
                    {
                        return this.Reference.IsAlive;
                    }

                    return true;
                }

                return this.Reference.IsAlive;
            }
        }

        /// <summary>
        /// Executes the action. This only happens if the action's owner
        /// is still alive. The action's parameter is set to default(T).
        /// </summary>
        public new void Execute()
        {
            this.Execute(default(T));
        }

        /// <summary>
        /// Executes the action. This only happens if the action's owner
        /// is still alive.
        /// </summary>
        /// <param name="parameter">A parameter to be passed to the action.</param>
        public void Execute(T parameter)
        {
            if (this.staticAction != null)
            {
                this.staticAction(parameter);
                return;
            }

            var actionTarget = this.ActionTarget;

            if (this.IsAlive)
            {
                if (this.Method != null && this.ActionReference != null && actionTarget != null)
                {
                    Method.Invoke(actionTarget, new Object[] { parameter });
                }
            }
        }

        /// <summary>
        /// Executes the action with a parameter of type object. This parameter
        /// will be casted to T. This method implements <see cref="IExecuteWithObject.ExecuteWithObject" />
        /// and can be useful if you store multiple WeakAction{T} instances but don't know in advance
        /// what type T represents.
        /// </summary>
        /// <param name="parameter">The parameter that will be passed to the action after
        /// being casted to T.</param>
        public void ExecuteWithObject(Object parameter)
        {
            this.Execute((T)parameter);
        }

        /// <summary>
        /// Sets all the actions that this WeakAction contains to null,
        /// which is a signal for containing objects that this WeakAction
        /// should be deleted.
        /// </summary>
        public new void MarkForDeletion()
        {
            this.staticAction = null;
            base.MarkForDeletion();
        }
    }
    //// End class
}
//// End namespace