﻿namespace Ana.Source.ChangeCounter
{
    using Docking;
    using Main;
    using System;
    using System.Threading;

    /// <summary>
    /// View model for the Change Counter
    /// </summary>
    internal class ChangeCounterViewModel : ToolViewModel
    {
        /// <summary>
        /// The content id for the docking library associated with this view model
        /// </summary>
        public const String ToolContentId = nameof(ChangeCounterViewModel);

        /// <summary>
        /// Singleton instance of the <see cref="ChangeCounterViewModel" /> class
        /// </summary>
        private static Lazy<ChangeCounterViewModel> changeCounterViewModelInstance = new Lazy<ChangeCounterViewModel>(
                () => { return new ChangeCounterViewModel(); },
                LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Prevents a default instance of the <see cref="ChangeCounterViewModel" /> class from being created
        /// </summary>
        private ChangeCounterViewModel() : base("Change Counter")
        {
            this.ContentId = ToolContentId;

            MainViewModel.GetInstance().Subscribe(this);
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="ChangeCounterViewModel"/> class
        /// </summary>
        /// <returns>A singleton instance of the class</returns>
        public static ChangeCounterViewModel GetInstance()
        {
            return changeCounterViewModelInstance.Value;
        }
    }
    //// End class
}
//// End namespace