using Ana.Source.Controller;
using Ana.Source.Utils.Extensions;
using System;

namespace Ana.Source.Utils
{
    class ProgressItem
    {
        private String ProgressLabel;
        private Double ActionProgress;
        private Boolean RestictProgressUpdates;

        public ProgressItem() : this(String.Empty, 0) { }
        public ProgressItem(String ProgressLabel) : this(ProgressLabel, 0) { }
        public ProgressItem(String ProgressLabel, Int32 Progress)
        {
            this.ProgressLabel = ProgressLabel;
            this.ActionProgress = Progress;

            RestrictProgress();
        }

        /// <summary>
        /// Only allows progress to update when it increases
        /// </summary>
        public void RestrictProgress()
        {
            this.RestictProgressUpdates = true;
        }

        /// <summary>
        /// Allows progress to go up or down
        /// </summary>
        public void RelaxProgress()
        {
            this.RestictProgressUpdates = false;
        }

        /// <summary>
        /// Update the label associated with this progress item
        /// </summary>
        /// <param name="ProgressLabel"></param>
        public void SetProgressLabel(String ProgressLabel)
        {
            this.ProgressLabel = ProgressLabel;
        }

        /// <summary>
        /// Return the label associated with this progress item
        /// </summary>
        /// <returns></returns>
        public String GetProgressLabel()
        {
            return ProgressLabel;
        }

        /// <summary>
        /// Return the progress as an Int32 between 0 and 100
        /// </summary>
        /// <returns></returns>
        public Int32 GetProgress()
        {
            return (Int32)(ActionProgress * 100.0).Clamp(0, 100);
        }

        /// <summary>
        /// Marks the progress as 100% complete
        /// </summary>
        public void FinishProgress()
        {
            ActionProgress = 0.0;
            Main.GetInstance().FinishActionProgress(this);
        }

        /// <summary>
        /// Updates the progress of an item, given a specified amount out of a given maximum
        /// </summary>
        /// <param name="Amount"></param>
        /// <param name="Max"></param>
        public void UpdateProgress(Int32 Amount, Int32 Max)
        {
            UpdateProgress(Max == 0 ? 0 : ((Double)Amount / (Double)Max).Clamp(0.0, 1.0));
        }

        /// <summary>
        /// Updates the progress of an item. Value ranges from 0 to 1.0
        /// </summary>
        /// <param name="ActionProgress"></param>
        public void UpdateProgress(Double ActionProgress)
        {
            // Do nothing if progress has not changed
            if (ActionProgress == this.ActionProgress)
                return;

            // If update restriction is enabled, ensure the new progress is greater than the old
            if (RestictProgressUpdates && this.ActionProgress > ActionProgress)
                return;

            this.ActionProgress = ActionProgress;

            Main.GetInstance().UpdateActionProgress(this);
        }

    } // End class

} // End namespace