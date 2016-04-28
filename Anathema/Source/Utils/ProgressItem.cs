using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anathema.Source.Utils
{
    class ProgressItem
    {
        private String ProgressLabel;
        private Double ActionProgress;
        private Double CompletionThreshold;
        private Boolean RestictProgressUpdates;

        public ProgressItem() : this(String.Empty, 0) { }
        public ProgressItem(String ProgressLabel) : this(ProgressLabel, 0) { }
        public ProgressItem(String ProgressLabel, Int32 Progress)
        {
            this.ProgressLabel = ProgressLabel;
            this.ActionProgress = Progress;

            SetCompletionThreshold(1.0);
            RestrictProgress();
        }

        /// <summary>
        /// Returns true if the progress item is complete, or passed the set threshold
        /// </summary>
        /// <returns></returns>
        public Boolean ActionComplete()
        {
            if (ActionProgress >= CompletionThreshold)
                return true;

            return false;
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
            return (Int32)(ActionProgress * 100.0);
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
        /// Marks the progress as 100% complete
        /// </summary>
        public void FinishProgress()
        {
            UpdateProgress(1.0);
        }

        /// <summary>
        /// Updates the progress of an item, given a specified amount out of a given maximum
        /// </summary>
        /// <param name="Amount"></param>
        /// <param name="Max"></param>
        public void UpdateProgress(Int32 Amount, Int32 Max)
        {
            UpdateProgress((Double)Amount / (Double)Max);
        }

        /// <summary>
        /// Updates the progress of an item. Value ranges from 0 to 1.0
        /// </summary>
        /// <param name="ActionProgress"></param>
        public void UpdateProgress(Double ActionProgress)
        {
            Boolean Finished = false;

            if (ActionProgress < 0.0 || ActionProgress > 1.0)
                throw new Exception("Invalid progress amount");

            if (ActionProgress == this.ActionProgress)
                return;

            // Once passed the completion threshold, we can consider this event finished
            if (ActionProgress >= CompletionThreshold)
            {
                ActionProgress = 1.0;
                Finished = true;
            }

            // If update restriction is enabled, ensure the new progress is greater than the old
            if (RestictProgressUpdates && this.ActionProgress > ActionProgress)
                return;

            this.ActionProgress = ActionProgress;

            Main.GetInstance().UpdateActionProgress(this);

            if (Finished)
                ActionProgress = 0.0;
        }

        /// <summary>
        /// Sets a threshold of % to mark the action as complete. This is useful when events are continuous
        /// and never actually end, such as snapshot prefiltering, but 
        /// </summary>
        /// <param name="CompletionThreshold"></param>
        public void SetCompletionThreshold(Double CompletionThreshold)
        {
            this.CompletionThreshold = CompletionThreshold;
        }

    } // End class

} // End namespace