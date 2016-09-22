namespace Ana.Source.ProcessSelector
{
    using Docking;
    using Engine.Processes;
    using Main;
    using Mvvm.Command;
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Utils;

    /// <summary>
    /// View model for the Process Selector
    /// </summary>
    internal class ProcessSelectorViewModel : ToolViewModel
    {
        public const String ProcessSelectorContentId = "ProcessSelector";

        public ProcessSelectorViewModel() : base("Process Selector")
        {
            this.ContentId = ProcessSelectorContentId;
            this.IconSource = ImageLoader.LoadImage("pack://application:,,/Content/Icons/SelectProcess.png");

            this.SelectProcessCommand = new RelayCommand<NormalizedProcess>((process) => this.SelectProcess(process), (process) => true);

            MainViewModel.GetInstance().Subscribe(this);
        }

        public ICommand SelectProcessCommand { get; private set; }

        public IEnumerable<NormalizedProcess> ProcessObjects
        {
            get
            {
                return ProcessCollector.GetProcesses();
            }

            set
            {
            }
        }

        private void SelectProcess(NormalizedProcess process)
        {
            if (process == null)
            {
                return;
            }

            this.IsVisible = false;
        }
    }
    //// End class
}
//// End namespace