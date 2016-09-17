namespace Ana.Source.Mvvm.Command
{
    using Helpers;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Input;

    /// <summary>
    /// A generic command whose sole purpose is to relay its functionality to other
    /// objects by invoking delegates. The default return value for the CanExecute
    /// method is 'true'. This class allows you to accept command parameters in the
    /// Execute and CanExecute callback methods.
    /// </summary>
    /// <typeparam name="T">The type of the command parameter.</typeparam>
    /// <remarks>If you are using this class in WPF4.5 or above, you need to use the 
    /// GalaSoft.MvvmLight.CommandWpf namespace (instead of GalaSoft.MvvmLight.Command).
    /// This will enable (or restore) the CommandManager class which handles
    /// automatic enabling/disabling of controls based on the CanExecute delegate.</remarks>
    ////[ClassInfo(typeof(RelayCommand))]
    public class RelayCommand<T> : ICommand
    {
        private readonly WeakAction<T> _execute;

        private readonly WeakFunc<T, bool> _canExecute;

        /// <summary>
        /// Initializes a new instance of the RelayCommand class that 
        /// can always execute.
        /// </summary>
        /// <param name="execute">The execution logic. IMPORTANT: Note that closures are not supported at the moment
        /// due to the use of WeakActions (see http://stackoverflow.com/questions/25730530/). </param>
        /// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RelayCommand class.
        /// </summary>
        /// <param name="execute">The execution logic. IMPORTANT: Note that closures are not supported at the moment
        /// due to the use of WeakActions (see http://stackoverflow.com/questions/25730530/). </param>
        /// <param name="canExecute">The execution status logic. IMPORTANT: Note that closures are not supported at the moment
        /// due to the use of WeakActions (see http://stackoverflow.com/questions/25730530/). </param>
        /// <exception cref="ArgumentNullException">If the execute argument is null.</exception>
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = new WeakAction<T>(execute);

            if (canExecute != null)
            {
                _canExecute = new WeakFunc<T, bool>(canExecute);
            }
        }

        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged" /> event.
        /// </summary>
        [SuppressMessage(
            "Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "The this keyword is used in the Silverlight version")]
        [SuppressMessage(
            "Microsoft.Design",
            "CA1030:UseEventsWhereAppropriate",
            Justification = "This cannot be an event")]
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data 
        /// to be passed, this object can be set to a null reference</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            if (_canExecute.IsStatic || _canExecute.IsAlive)
            {
                if (parameter == null && typeof(T).IsValueType)
                {
                    return _canExecute.Execute(default(T));
                }

                if (parameter == null || parameter is T)
                {
                    return (_canExecute.Execute((T)parameter));
                }
            }

            return false;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked. 
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data 
        /// to be passed, this object can be set to a null reference</param>
        public virtual void Execute(object parameter)
        {
            var val = parameter;

            if (parameter != null
                && parameter.GetType() != typeof(T))
            {
                if (parameter is IConvertible)
                {
                    val = Convert.ChangeType(parameter, typeof(T), null);
                }
            }

            if (CanExecute(val)
                && _execute != null
                && (_execute.IsStatic || _execute.IsAlive))
            {
                if (val == null)
                {
                    if (typeof(T).IsValueType)
                    {
                        _execute.Execute(default(T));
                    }
                    else
                    {
                        // ReSharper disable ExpressionIsAlwaysNull
                        _execute.Execute((T)val);
                        // ReSharper restore ExpressionIsAlwaysNull
                    }
                }
                else
                {
                    _execute.Execute((T)val);
                }
            }
        }
    }
}