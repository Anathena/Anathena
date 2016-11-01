namespace Ana.Source.Scanners.LabelThresholder
{
    using Docking;
    using LiveCharts;
    using Main;
    using Mvvm.Command;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// View model for the Label Thresholder
    /// </summary>
    internal class LabelThresholderViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(LabelThresholderViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="LabelThresholderViewModel" /> class
        /// </summary>
        private static Lazy<LabelThresholderViewModel> labelThresholderViewModelInstance = new Lazy<LabelThresholderViewModel>(
                () => { return new LabelThresholderViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        private IList<String> labels;

        private IChartValues values;

        /// <summary>
        /// Prevents a default instance of the <see cref="LabelThresholderViewModel" /> class from being created
        /// </summary>
        private LabelThresholderViewModel() : base("Label Thresholder")
        {
            this.ContentId = LabelThresholderViewModel.ToolContentId;
            this.LabelThresholderModel = new LabelThresholderModel(this.OnUpdateHistogram);
            this.LowerThreshold = this.MinimumValue;
            this.UpperThreshold = this.MaximumValue;
            this.ApplyThresholdCommand = new RelayCommand(() => Task.Run(() => this.ApplyThreshold()), () => true);
            this.InvertSelectionCommand = new RelayCommand(() => Task.Run(() => this.InvertSelection()), () => true);

            MainViewModel.GetInstance().Subscribe(this);
        }

        public ICommand ApplyThresholdCommand { get; private set; }

        public ICommand InvertSelectionCommand { get; private set; }

        public IList<String> Labels
        {
            get
            {
                return this.labels;
            }

            set
            {
                this.labels = value;
                this.RaisePropertyChanged(nameof(this.Labels));
            }
        }

        public IChartValues Values
        {
            get
            {
                return this.values;
            }

            set
            {
                this.values = value;
                this.RaisePropertyChanged(nameof(this.Values));
            }
        }

        public Double LowerThreshold
        {
            get
            {
                return this.LabelThresholderModel.LowerThreshold;
            }

            set
            {
                this.LabelThresholderModel.LowerThreshold = value;
                this.RaisePropertyChanged(nameof(this.LowerThreshold));
            }
        }

        public Double UpperThreshold
        {
            get
            {
                return this.LabelThresholderModel.UpperThreshold;
            }

            set
            {
                this.LabelThresholderModel.UpperThreshold = value;
                this.RaisePropertyChanged(nameof(this.UpperThreshold));
            }
        }

        public Double MinimumValue
        {
            get
            {
                return 0;
            }
        }

        public Double MaximumValue
        {
            get
            {
                return 100;
            }
        }

        private LabelThresholderModel LabelThresholderModel { get; set; }

        /// <summary>
        /// Gets a singleton instance of the <see cref="LabelThresholderViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static LabelThresholderViewModel GetInstance()
        {
            return labelThresholderViewModelInstance.Value;
        }

        public void OpenLabelThresholder()
        {
            this.IsVisible = true;
        }

        private void ApplyThreshold()
        {
            this.LabelThresholderModel.ApplyThreshold();
        }

        private void InvertSelection()
        {
            this.LabelThresholderModel.ToggleInverted();
        }

        private void OnUpdateHistogram()
        {
            SortedList<dynamic, Int64> histogram = LabelThresholderModel.Histogram;
            // this.Labels = histogram.Values.Select(x => x.ToString()).ToList();
            // this.Values = new ChartValues<Int32>(histogram.Keys.Select(x => (Int32)x));
            this.Labels = histogram.Keys.Select(x => (String)x.ToString()).ToList();
            this.Values = new ChartValues<Int64>(histogram.Values);
        }
    }
    //// End class
}
//// End namespace