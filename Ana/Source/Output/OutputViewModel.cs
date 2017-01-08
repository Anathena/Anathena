namespace Ana.Source.Output
{
    using Docking;
    using Main;
    using Mvvm.Command;
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Output.
    /// </summary>
    internal class OutputViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model.
        /// </summary>
        public const String ToolContentId = nameof(OutputViewModel);

        /// <summary>
        /// The rough total capacity in bytes of our log.
        /// </summary>
        private const Int32 LogCapacity = Int16.MaxValue;

        /// <summary>
        /// The minimum number of bytes to clear when going over capacity.
        /// </summary>
        private const Int32 MinimumClearSize = 4096;

        /// <summary>
        /// Singleton instance of the <see cref="OutputViewModel" /> class.
        /// </summary>
        private static Lazy<OutputViewModel> outputViewModelInstance = new Lazy<OutputViewModel>(
                () => { return new OutputViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// The log text builder.
        /// </summary>
        private StringBuilder logText;

        /// <summary>
        /// Prevents a default instance of the <see cref="OutputViewModel" /> class from being created.
        /// </summary>
        private OutputViewModel() : base("Output")
        {
            this.ContentId = OutputViewModel.ToolContentId;
            this.logText = new StringBuilder(OutputViewModel.LogCapacity);
            this.ClearOutputCommand = new RelayCommand(() => this.ClearOutput(), () => true);

            Task.Run(() => MainViewModel.GetInstance().Subscribe(this));
        }

        /// <summary>
        /// The possible channels to which we can log messages.
        /// </summary>
        public enum LogLevel
        {
            /// <summary>
            /// Debugging information.
            /// </summary>
            Debug,

            /// <summary>
            /// Standard information.
            /// </summary>
            Info,

            /// <summary>
            /// Warning messages.
            /// </summary>
            Warn,

            /// <summary>
            /// Error messages.
            /// </summary>
            Error,

            /// <summary>
            /// Severe error messages.
            /// </summary>
            Fatal,
        }

        /// <summary>
        /// Gets the command to clear the output text.
        /// </summary>
        public ICommand ClearOutputCommand { get; private set; }

        /// <summary>
        /// Gets the log text and builds it from the current string builder.
        /// </summary>
        public String LogText
        {
            get
            {
                return this.logText.ToString();
            }
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="OutputViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static OutputViewModel GetInstance()
        {
            return OutputViewModel.outputViewModelInstance.Value;
        }

        /// <summary>
        /// Logs a message to output.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="message">The log message.</param>
        public void Log(LogLevel logLevel, String message)
        {
            // Over capacity, remove some of the first lines based on the minimum clear size
            if (this.logText.Length > OutputViewModel.LogCapacity)
            {
                String currentText = this.LogText.Substring(Math.Min(this.LogText.Length, OutputViewModel.MinimumClearSize));
                this.logText = new StringBuilder(currentText.Substring(currentText.IndexOf(Environment.NewLine) + 1));
            }

            // Write log message. We do this after the capacity check to avoid any chance of accidentally clearing out this message.
            message = DateTime.Now.ToString("mm:ss.fff") + String.Concat(Enumerable.Repeat(" ", 4)) + "[" + logLevel.ToString() + "] - " + message;
            Console.WriteLine(message);
            this.logText.AppendLine(message);

            this.RaisePropertyChanged(nameof(this.LogText));
        }

        /// <summary>
        /// Clears all output text.
        /// </summary>
        private void ClearOutput()
        {
            this.logText.Clear();
            this.RaisePropertyChanged(nameof(this.LogText));
        }
    }
    //// End class
}
//// End namespace