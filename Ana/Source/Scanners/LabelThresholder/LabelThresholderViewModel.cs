namespace Ana.Source.LabelThresholder
{
    using Docking;
    using Main;
    using System;
    using System.Threading;

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

        private Double lowerValue;

        private Double higherValue;

        private Double minimumValue;

        private Double maximumValue;

        /// <summary>
        /// Prevents a default instance of the <see cref="LabelThresholderViewModel" /> class from being created
        /// </summary>
        private LabelThresholderViewModel() : base("Label Thresholder")
        {
            this.ContentId = LabelThresholderViewModel.ToolContentId;
            this.LowerValue = 0;
            this.MinimumValue = 0;
            this.HigherValue = 100;
            this.MaximumValue = 100;

            MainViewModel.GetInstance().Subscribe(this);
        }

        public Double LowerValue
        {
            get
            {
                return this.lowerValue;
            }

            set
            {
                this.lowerValue = value;
                this.RaisePropertyChanged(nameof(this.LowerValue));
            }
        }

        public Double HigherValue
        {
            get
            {
                return this.higherValue;
            }

            set
            {
                this.higherValue = value;
                this.RaisePropertyChanged(nameof(this.HigherValue));
            }
        }

        public Double MinimumValue
        {
            get
            {
                return this.minimumValue;
            }

            set
            {
                this.minimumValue = value;
                this.RaisePropertyChanged(nameof(this.MinimumValue));
            }
        }

        public Double MaximumValue
        {
            get
            {
                return this.maximumValue;
            }

            set
            {
                this.maximumValue = value;
                this.RaisePropertyChanged(nameof(this.MaximumValue));
            }
        }

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
    }
    //// End class
}
//// End namespace