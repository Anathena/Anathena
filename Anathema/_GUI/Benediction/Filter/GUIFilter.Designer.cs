namespace Anathema
{
    partial class GUIFilter
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIFilter));
            this.FilterToolStrip = new System.Windows.Forms.ToolStrip();
            this.SearchSpaceAnalysisButton = new System.Windows.Forms.ToolStripButton();
            this.FiniteStateMachineButton = new System.Windows.Forms.ToolStripButton();
            this.ManualScanButton = new System.Windows.Forms.ToolStripButton();
            this.FilterToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // FilterToolStrip
            // 
            this.FilterToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.FilterToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SearchSpaceAnalysisButton,
            this.FiniteStateMachineButton,
            this.ManualScanButton});
            this.FilterToolStrip.Location = new System.Drawing.Point(0, 0);
            this.FilterToolStrip.Name = "FilterToolStrip";
            this.FilterToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.FilterToolStrip.Size = new System.Drawing.Size(287, 25);
            this.FilterToolStrip.TabIndex = 143;
            this.FilterToolStrip.Text = "toolStrip1";
            // 
            // SearchSpaceAnalysisButton
            // 
            this.SearchSpaceAnalysisButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SearchSpaceAnalysisButton.Image = ((System.Drawing.Image)(resources.GetObject("SearchSpaceAnalysisButton.Image")));
            this.SearchSpaceAnalysisButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SearchSpaceAnalysisButton.Name = "SearchSpaceAnalysisButton";
            this.SearchSpaceAnalysisButton.Size = new System.Drawing.Size(23, 22);
            this.SearchSpaceAnalysisButton.Text = "Search Space Analysis";
            this.SearchSpaceAnalysisButton.ToolTipText = "Hash Trees";
            this.SearchSpaceAnalysisButton.Click += new System.EventHandler(this.SearchSpaceAnalysisButton_Click);
            // 
            // FiniteStateMachineButton
            // 
            this.FiniteStateMachineButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FiniteStateMachineButton.Image = ((System.Drawing.Image)(resources.GetObject("FiniteStateMachineButton.Image")));
            this.FiniteStateMachineButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FiniteStateMachineButton.Name = "FiniteStateMachineButton";
            this.FiniteStateMachineButton.Size = new System.Drawing.Size(23, 22);
            this.FiniteStateMachineButton.Text = "Finite State Scanner";
            this.FiniteStateMachineButton.ToolTipText = "Finite State Scanner";
            this.FiniteStateMachineButton.Click += new System.EventHandler(this.FiniteStateMachineButton_Click);
            // 
            // ManualScanButton
            // 
            this.ManualScanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ManualScanButton.Image = ((System.Drawing.Image)(resources.GetObject("ManualScanButton.Image")));
            this.ManualScanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ManualScanButton.Name = "ManualScanButton";
            this.ManualScanButton.Size = new System.Drawing.Size(23, 22);
            this.ManualScanButton.Text = "Manual Scan";
            this.ManualScanButton.ToolTipText = "Manual Scan";
            this.ManualScanButton.Click += new System.EventHandler(this.ManualScanButton_Click);
            // 
            // GUIFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FilterToolStrip);
            this.Name = "GUIFilter";
            this.Size = new System.Drawing.Size(287, 282);
            this.FilterToolStrip.ResumeLayout(false);
            this.FilterToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip FilterToolStrip;
        private System.Windows.Forms.ToolStripButton SearchSpaceAnalysisButton;
        private System.Windows.Forms.ToolStripButton FiniteStateMachineButton;
        private System.Windows.Forms.ToolStripButton ManualScanButton;
    }
}
