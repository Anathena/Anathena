namespace Anathema
{
    partial class GUILabeler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUILabeler));
            this.LabelerToolStrip = new System.Windows.Forms.ToolStrip();
            this.InputCorrelatorButton = new System.Windows.Forms.ToolStripButton();
            this.LabelerToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelerToolStrip
            // 
            this.LabelerToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.LabelerToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InputCorrelatorButton});
            this.LabelerToolStrip.Location = new System.Drawing.Point(0, 0);
            this.LabelerToolStrip.Name = "LabelerToolStrip";
            this.LabelerToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.LabelerToolStrip.Size = new System.Drawing.Size(239, 25);
            this.LabelerToolStrip.TabIndex = 144;
            this.LabelerToolStrip.Text = "toolStrip1";
            // 
            // InputCorrelatorButton
            // 
            this.InputCorrelatorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.InputCorrelatorButton.Image = ((System.Drawing.Image)(resources.GetObject("InputCorrelatorButton.Image")));
            this.InputCorrelatorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InputCorrelatorButton.Name = "InputCorrelatorButton";
            this.InputCorrelatorButton.Size = new System.Drawing.Size(23, 22);
            this.InputCorrelatorButton.Text = "Input Correlator";
            this.InputCorrelatorButton.ToolTipText = "Hash Trees";
            this.InputCorrelatorButton.Click += new System.EventHandler(this.InputCorrelatorButton_Click);
            // 
            // GUILabeler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LabelerToolStrip);
            this.Name = "GUILabeler";
            this.Size = new System.Drawing.Size(239, 234);
            this.LabelerToolStrip.ResumeLayout(false);
            this.LabelerToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip LabelerToolStrip;
        private System.Windows.Forms.ToolStripButton InputCorrelatorButton;
    }
}
