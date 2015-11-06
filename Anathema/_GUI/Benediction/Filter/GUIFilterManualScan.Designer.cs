namespace Anathema
{
    partial class GUIFilterManualScan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIFilterManualScan));
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.SearchSpaceAnalysisButton = new System.Windows.Forms.ToolStripButton();
            this.FiniteStateMachineButton = new System.Windows.Forms.ToolStripButton();
            this.InputCorrelatorButton = new System.Windows.Forms.ToolStripButton();
            this.ManualScanButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ScanOptionsButton = new System.Windows.Forms.ToolStripButton();
            this.DataTypeCBB = new System.Windows.Forms.ComboBox();
            this.ScanTypeWorldStrip = new System.Windows.Forms.ToolStrip();
            this.NotEqualValButton = new System.Windows.Forms.ToolStripButton();
            this.IncreasedValButton = new System.Windows.Forms.ToolStripButton();
            this.DecreasedValButton = new System.Windows.Forms.ToolStripButton();
            this.EqualValButton = new System.Windows.Forms.ToolStripButton();
            this.GreaterThanValButton = new System.Windows.Forms.ToolStripButton();
            this.LessThanValButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.AddConstraintButton = new System.Windows.Forms.ToolStripButton();
            this.ClearConstraintsButton = new System.Windows.Forms.ToolStripButton();
            this.ENotationButton = new System.Windows.Forms.ToolStripButton();
            this.CompareTypeLabel = new System.Windows.Forms.Label();
            this.ScanValueTB = new System.Windows.Forms.TextBox();
            this.ScanValueUpperTB = new System.Windows.Forms.TextBox();
            this.ScanToolStrip.SuspendLayout();
            this.ScanTypeWorldStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SearchSpaceAnalysisButton,
            this.FiniteStateMachineButton,
            this.InputCorrelatorButton,
            this.ManualScanButton,
            this.toolStripSeparator4,
            this.ScanOptionsButton});
            this.ScanToolStrip.Location = new System.Drawing.Point(0, 244);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(231, 25);
            this.ScanToolStrip.TabIndex = 122;
            this.ScanToolStrip.Text = "toolStrip1";
            // 
            // SearchSpaceAnalysisButton
            // 
            this.SearchSpaceAnalysisButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SearchSpaceAnalysisButton.Image = ((System.Drawing.Image)(resources.GetObject("SearchSpaceAnalysisButton.Image")));
            this.SearchSpaceAnalysisButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SearchSpaceAnalysisButton.Name = "SearchSpaceAnalysisButton";
            this.SearchSpaceAnalysisButton.Size = new System.Drawing.Size(23, 22);
            this.SearchSpaceAnalysisButton.Text = "Start Scan";
            // 
            // FiniteStateMachineButton
            // 
            this.FiniteStateMachineButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FiniteStateMachineButton.Image = ((System.Drawing.Image)(resources.GetObject("FiniteStateMachineButton.Image")));
            this.FiniteStateMachineButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FiniteStateMachineButton.Name = "FiniteStateMachineButton";
            this.FiniteStateMachineButton.Size = new System.Drawing.Size(23, 22);
            this.FiniteStateMachineButton.Text = "Next Scan";
            // 
            // InputCorrelatorButton
            // 
            this.InputCorrelatorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.InputCorrelatorButton.Image = ((System.Drawing.Image)(resources.GetObject("InputCorrelatorButton.Image")));
            this.InputCorrelatorButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InputCorrelatorButton.Name = "InputCorrelatorButton";
            this.InputCorrelatorButton.Size = new System.Drawing.Size(23, 22);
            this.InputCorrelatorButton.Text = "Undo Scan";
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
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // ScanOptionsButton
            // 
            this.ScanOptionsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ScanOptionsButton.Image = ((System.Drawing.Image)(resources.GetObject("ScanOptionsButton.Image")));
            this.ScanOptionsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ScanOptionsButton.Name = "ScanOptionsButton";
            this.ScanOptionsButton.Size = new System.Drawing.Size(23, 22);
            this.ScanOptionsButton.Text = "Scan Options";
            // 
            // DataTypeCBB
            // 
            this.DataTypeCBB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.DataTypeCBB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DataTypeCBB.FormattingEnabled = true;
            this.DataTypeCBB.Items.AddRange(new object[] {
            "All",
            "Binary",
            "Byte",
            "Int 16",
            "Int 32",
            "Int 64",
            "Single",
            "Double",
            "Text",
            "Array of Bytes"});
            this.DataTypeCBB.Location = new System.Drawing.Point(6, 81);
            this.DataTypeCBB.Name = "DataTypeCBB";
            this.DataTypeCBB.Size = new System.Drawing.Size(101, 21);
            this.DataTypeCBB.TabIndex = 136;
            // 
            // ScanTypeWorldStrip
            // 
            this.ScanTypeWorldStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ScanTypeWorldStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanTypeWorldStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NotEqualValButton,
            this.IncreasedValButton,
            this.DecreasedValButton,
            this.EqualValButton,
            this.GreaterThanValButton,
            this.LessThanValButton,
            this.toolStripSeparator7,
            this.AddConstraintButton,
            this.ClearConstraintsButton,
            this.ENotationButton});
            this.ScanTypeWorldStrip.Location = new System.Drawing.Point(0, 219);
            this.ScanTypeWorldStrip.Name = "ScanTypeWorldStrip";
            this.ScanTypeWorldStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanTypeWorldStrip.Size = new System.Drawing.Size(231, 25);
            this.ScanTypeWorldStrip.TabIndex = 138;
            this.ScanTypeWorldStrip.Text = "toolStrip1";
            // 
            // NotEqualValButton
            // 
            this.NotEqualValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NotEqualValButton.Image = ((System.Drawing.Image)(resources.GetObject("NotEqualValButton.Image")));
            this.NotEqualValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NotEqualValButton.Name = "NotEqualValButton";
            this.NotEqualValButton.Size = new System.Drawing.Size(23, 22);
            this.NotEqualValButton.Text = "Negate Property";
            // 
            // IncreasedValButton
            // 
            this.IncreasedValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.IncreasedValButton.Image = ((System.Drawing.Image)(resources.GetObject("IncreasedValButton.Image")));
            this.IncreasedValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.IncreasedValButton.Name = "IncreasedValButton";
            this.IncreasedValButton.Size = new System.Drawing.Size(23, 22);
            this.IncreasedValButton.Text = "Increased Value";
            // 
            // DecreasedValButton
            // 
            this.DecreasedValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DecreasedValButton.Image = ((System.Drawing.Image)(resources.GetObject("DecreasedValButton.Image")));
            this.DecreasedValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DecreasedValButton.Name = "DecreasedValButton";
            this.DecreasedValButton.Size = new System.Drawing.Size(23, 22);
            this.DecreasedValButton.Text = "Decreased Value";
            // 
            // EqualValButton
            // 
            this.EqualValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.EqualValButton.Image = ((System.Drawing.Image)(resources.GetObject("EqualValButton.Image")));
            this.EqualValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.EqualValButton.Name = "EqualValButton";
            this.EqualValButton.Size = new System.Drawing.Size(23, 22);
            this.EqualValButton.Text = "Exact Value";
            // 
            // GreaterThanValButton
            // 
            this.GreaterThanValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.GreaterThanValButton.Image = ((System.Drawing.Image)(resources.GetObject("GreaterThanValButton.Image")));
            this.GreaterThanValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.GreaterThanValButton.Name = "GreaterThanValButton";
            this.GreaterThanValButton.Size = new System.Drawing.Size(23, 22);
            this.GreaterThanValButton.Text = "Greater Than Value";
            // 
            // LessThanValButton
            // 
            this.LessThanValButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LessThanValButton.Image = ((System.Drawing.Image)(resources.GetObject("LessThanValButton.Image")));
            this.LessThanValButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LessThanValButton.Name = "LessThanValButton";
            this.LessThanValButton.Size = new System.Drawing.Size(23, 22);
            this.LessThanValButton.Text = "Less Than Value";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // AddConstraintButton
            // 
            this.AddConstraintButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddConstraintButton.Image = ((System.Drawing.Image)(resources.GetObject("AddConstraintButton.Image")));
            this.AddConstraintButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddConstraintButton.Name = "AddConstraintButton";
            this.AddConstraintButton.Size = new System.Drawing.Size(23, 22);
            this.AddConstraintButton.Text = "Add to Filter";
            // 
            // ClearConstraintsButton
            // 
            this.ClearConstraintsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearConstraintsButton.Image = ((System.Drawing.Image)(resources.GetObject("ClearConstraintsButton.Image")));
            this.ClearConstraintsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearConstraintsButton.Name = "ClearConstraintsButton";
            this.ClearConstraintsButton.Size = new System.Drawing.Size(23, 22);
            this.ClearConstraintsButton.Text = "Clear Selections";
            // 
            // ENotationButton
            // 
            this.ENotationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ENotationButton.Image = ((System.Drawing.Image)(resources.GetObject("ENotationButton.Image")));
            this.ENotationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ENotationButton.Name = "ENotationButton";
            this.ENotationButton.Size = new System.Drawing.Size(23, 22);
            this.ENotationButton.Text = "toolStripButton1";
            // 
            // CompareTypeLabel
            // 
            this.CompareTypeLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CompareTypeLabel.Location = new System.Drawing.Point(3, 39);
            this.CompareTypeLabel.Name = "CompareTypeLabel";
            this.CompareTypeLabel.Size = new System.Drawing.Size(160, 13);
            this.CompareTypeLabel.TabIndex = 137;
            this.CompareTypeLabel.Text = "Not Between or Equal to Values";
            this.CompareTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ScanValueTB
            // 
            this.ScanValueTB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ScanValueTB.Location = new System.Drawing.Point(6, 55);
            this.ScanValueTB.Name = "ScanValueTB";
            this.ScanValueTB.Size = new System.Drawing.Size(101, 20);
            this.ScanValueTB.TabIndex = 134;
            // 
            // ScanValueUpperTB
            // 
            this.ScanValueUpperTB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ScanValueUpperTB.Location = new System.Drawing.Point(113, 55);
            this.ScanValueUpperTB.Name = "ScanValueUpperTB";
            this.ScanValueUpperTB.Size = new System.Drawing.Size(101, 20);
            this.ScanValueUpperTB.TabIndex = 135;
            // 
            // GUIFilterManualScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DataTypeCBB);
            this.Controls.Add(this.CompareTypeLabel);
            this.Controls.Add(this.ScanValueTB);
            this.Controls.Add(this.ScanValueUpperTB);
            this.Controls.Add(this.ScanTypeWorldStrip);
            this.Controls.Add(this.ScanToolStrip);
            this.Name = "GUIFilterManualScan";
            this.Size = new System.Drawing.Size(231, 269);
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.ScanTypeWorldStrip.ResumeLayout(false);
            this.ScanTypeWorldStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton SearchSpaceAnalysisButton;
        private System.Windows.Forms.ToolStripButton FiniteStateMachineButton;
        private System.Windows.Forms.ToolStripButton InputCorrelatorButton;
        private System.Windows.Forms.ToolStripButton ManualScanButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton ScanOptionsButton;
        private System.Windows.Forms.ComboBox DataTypeCBB;
        private System.Windows.Forms.ToolStrip ScanTypeWorldStrip;
        private System.Windows.Forms.ToolStripButton NotEqualValButton;
        private System.Windows.Forms.ToolStripButton IncreasedValButton;
        private System.Windows.Forms.ToolStripButton DecreasedValButton;
        private System.Windows.Forms.ToolStripButton EqualValButton;
        private System.Windows.Forms.ToolStripButton GreaterThanValButton;
        private System.Windows.Forms.ToolStripButton LessThanValButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton AddConstraintButton;
        private System.Windows.Forms.ToolStripButton ClearConstraintsButton;
        private System.Windows.Forms.ToolStripButton ENotationButton;
        private System.Windows.Forms.Label CompareTypeLabel;
        private System.Windows.Forms.TextBox ScanValueTB;
        private System.Windows.Forms.TextBox ScanValueUpperTB;
    }
}
