namespace Ana.Source.Scanners.LabelThresholder
{
    using Docking;
    using LiveCharts;
    using LiveCharts.Wpf;
    using Main;
    using Mvvm.Command;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// View model for the Label Thresholder.
    /// </summary>
    internal class LabelThresholderViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model.
        /// </summary>
        public const String ToolContentId = nameof(LabelThresholderViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="LabelThresholderViewModel" /> class.
        /// </summary>
        private static Lazy<LabelThresholderViewModel> labelThresholderViewModelInstance = new Lazy<LabelThresholderViewModel>(
                () => { return new LabelThresholderViewModel(); },
                LazyThreadSafetyMode.ExecutionAndPublication);

        private SeriesCollection seriesCollection;

        private IList<String> labels;

        private IChartValues filteredValues;

        private IChartValues keptValues;

        /// <summary>
        /// Prevents a default instance of the <see cref="LabelThresholderViewModel" /> class from being created.
        /// </summary>
        private LabelThresholderViewModel() : base("Label Thresholder")
        {
            this.ContentId = LabelThresholderViewModel.ToolContentId;
            this.LabelThresholderModel = new LabelThresholderModel(this.OnUpdateHistogram);
            this.LowerThreshold = this.MinimumValue;
            this.UpperThreshold = this.MaximumValue;
            this.ApplyThresholdCommand = new RelayCommand(() => Task.Run(() => this.ApplyThreshold()), () => true);
            this.InvertSelectionCommand = new RelayCommand(() => Task.Run(() => this.InvertSelection()), () => true);

            Task.Run(() => MainViewModel.GetInstance().Subscribe(this));
        }

        public ICommand ApplyThresholdCommand { get; private set; }

        public ICommand InvertSelectionCommand { get; private set; }

        public SeriesCollection SeriesCollection
        {
            get
            {
                return this.seriesCollection;
            }

            set
            {
                this.seriesCollection = value;
                this.RaisePropertyChanged(nameof(this.SeriesCollection));
            }
        }

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

        public IChartValues FilteredValues
        {
            get
            {
                return this.filteredValues;
            }

            set
            {
                this.filteredValues = value;
                this.RaisePropertyChanged(nameof(this.FilteredValues));
            }
        }

        public IChartValues KeptValues
        {
            get
            {
                return this.keptValues;
            }

            set
            {
                this.keptValues = value;
                this.RaisePropertyChanged(nameof(this.KeptValues));
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
        /// Gets a singleton instance of the <see cref="LabelThresholderViewModel"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static LabelThresholderViewModel GetInstance()
        {
            return labelThresholderViewModelInstance.Value;
        }

        public void OpenLabelThresholder()
        {
            this.IsVisible = true;
            this.IsActive = true;
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
            SortedList<dynamic, Int64> histogramKept = LabelThresholderModel.HistogramKept;
            SortedList<dynamic, Int64> histogramFiltered = LabelThresholderModel.HistogramFiltered;

            this.labels = histogram.Keys.Select(x => (String)x.ToString()).ToList();
            this.KeptValues = new ChartValues<Int64>(histogramKept.Values.Select(x => (Int64)Math.Log(x)));
            this.FilteredValues = new ChartValues<Int64>(histogramFiltered.Values.Select(x => (Int64)Math.Log(x)));

            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                if (this.SeriesCollection == null)
                {
                    this.SeriesCollection = new SeriesCollection()
                    {
                        new ColumnSeries
                        {
                            Values = this.KeptValues,
                            Fill = Brushes.Blue,
                            DataLabels = false
                        },
                        new ColumnSeries
                        {
                            Values = this.FilteredValues,
                            Fill = Brushes.Red,
                            DataLabels = false
                        }
                    };
                }
                else
                {
                    this.SeriesCollection[0].Values = this.KeptValues;
                    this.SeriesCollection[1].Values = this.FilteredValues;
                }
            });
        }
    }
    //// End class
}
//// End namespace