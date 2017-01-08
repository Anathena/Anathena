namespace Ana.Source.Mvvm.Command
{
    using Helpers;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Input;

    /// <summary>
    /// A generic command whose sole purpose is to relay its functionality to other objects by invoking delegates. The default return value for the CanExecute
    /// method is 'true'. This class allows you to accept command parameters in the Execute and CanExecute callback methods.
    /// </summary>
    /// <typeparam name="T">The type of the command parameter.</typeparam>
    internal class RelayCommand<T> : ICommand
    {
        /// <summary>
        /// TODO TODO.
        /// </summary>
        private readonly WeakAction<T> execute;

        /// <summary>
        /// TODO TODO.
        /// </summary>
        private readonly WeakFunc<T, Boolean> canExecute;

        /// <summary>
        /// Initializes a new instance of the RelayCommand class that 
        /// can always execute.
        /// </summary>
        /// <param name="execute">
        /// The execution logic. IMPORTANT: Note that closures are not supported at the moment due to the use of WeakActions (see http://stackoverflow.com/questions/25730530/).
        /// </param>
        /// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
        public RelayCommand(Action<T> execute) : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute">
        /// The execution logic. IMPORTANT: Note that closures are not supported at the moment due to the use of WeakActions (see http://stackoverflow.com/questions/25730530/).
        /// </param>
        /// <param name="canExecute">
        /// The execution status logic. IMPORTANT: Note that closures are not supported at the moment due to the use of WeakActions (see http://stackoverflow.com/questions/25730530/).
        /// </param>
        /// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
        public RelayCommand(Action<T> execute, Func<T, Boolean> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.execute = new WeakAction<T>(execute);

            if (canExecute != null)
            {
                this.canExecute = new WeakFunc<T, Boolean>(canExecute);
            }
        }

        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (this.canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (this.canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged" /> event.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "The this keyword is used in the Silverlight version")]
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Justification = "This cannot be an event")]
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data  to be passed, this object can be set to a null reference</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public Boolean CanExecute(Object parameter)
        {
            if (this.canExecute == null)
            {
                return true;
            }

            if (this.canExecute.IsStatic || this.canExecute.IsAlive)
            {
                if (parameter == null && typeof(T).IsValueType)
                {
                    return this.canExecute.Execute(default(T));
                }

                if (parameter == null || parameter is T)
                {
                    return this.canExecute.Execute((T)parameter);
                }
            }

            return false;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked. 
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to a null reference</param>
        public virtual void Execute(Object parameter)
        {
            Object val = parameter;

            if (parameter != null && parameter.GetType() != typeof(T))
            {
                if (parameter is IConvertible)
                {
                    val = Convert.ChangeType(parameter, typeof(T), null);
                }
            }

            if (this.CanExecute(val) && this.execute != null && (this.execute.IsStatic || this.execute.IsAlive))
            {
                if (val == null)
                {
                    if (typeof(T).IsValueType)
                    {
                        this.execute.Execute(default(T));
                    }
                    else
                    {
                        this.execute.Execute((T)val);
                    }
                }
                else
                {
                    this.execute.Execute((T)val);
                }
            }
        }
    }
    //// End class
}
//// End namespace