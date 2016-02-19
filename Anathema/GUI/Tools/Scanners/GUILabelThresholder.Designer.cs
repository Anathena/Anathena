namespace Anathema
{
    partial class GUILabelThresholder
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUILabelThresholder));
            this.LabelFrequencyChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.MaxValueTrackBar = new System.Windows.Forms.TrackBar();
            this.MinValueTrackBar = new System.Windows.Forms.TrackBar();
            this.ScanToolStrip = new System.Windows.Forms.ToolStrip();
            this.ApplyThresholdButton = new System.Windows.Forms.ToolStripButton();
            this.RefreshButton = new System.Windows.Forms.ToolStripButton();
            this.InvertSelectionButton = new System.Windows.Forms.ToolStripButton();
            this.ReductionLabel = new System.Windows.Forms.ToolStripLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MaxLabelLabel = new System.Windows.Forms.Label();
            this.MinLabelLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.LabelFrequencyChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxValueTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinValueTrackBar)).BeginInit();
            this.ScanToolStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelFrequencyChart
            // 
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisX.Title = "Label";
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.AxisY.Title = "Frequency";
            chartArea1.Name = "LabelFrequencyChartArea";
            this.LabelFrequencyChart.ChartAreas.Add(chartArea1);
            this.LabelFrequencyChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LabelFrequencyChart.Location = new System.Drawing.Point(0, 25);
            this.LabelFrequencyChart.Name = "LabelFrequencyChart";
            series1.ChartArea = "LabelFrequencyChartArea";
            series1.Name = "Frequency";
            this.LabelFrequencyChart.Series.Add(series1);
            this.LabelFrequencyChart.Size = new System.Drawing.Size(486, 186);
            this.LabelFrequencyChart.TabIndex = 0;
            this.LabelFrequencyChart.Text = "Label Thresholder";
            // 
            // MaxValueTrackBar
            // 
            this.MaxValueTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxValueTrackBar.AutoSize = false;
            this.MaxValueTrackBar.Location = new System.Drawing.Point(94, 36);
            this.MaxValueTrackBar.Name = "MaxValueTrackBar";
            this.MaxValueTrackBar.Size = new System.Drawing.Size(366, 24);
            this.MaxValueTrackBar.TabIndex = 1;
            this.MaxValueTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.MaxValueTrackBar.Value = 10;
            this.MaxValueTrackBar.Scroll += new System.EventHandler(this.MaxValueTrackBar_Scroll);
            // 
            // MinValueTrackBar
            // 
            this.MinValueTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MinValueTrackBar.AutoSize = false;
            this.MinValueTrackBar.Location = new System.Drawing.Point(94, 6);
            this.MinValueTrackBar.Name = "MinValueTrackBar";
            this.MinValueTrackBar.Size = new System.Drawing.Size(366, 24);
            this.MinValueTrackBar.TabIndex = 2;
            this.MinValueTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.MinValueTrackBar.Scroll += new System.EventHandler(this.MinValueTrackBar_Scroll);
            // 
            // ScanToolStrip
            // 
            this.ScanToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ScanToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ApplyThresholdButton,
            this.RefreshButton,
            this.InvertSelectionButton,
            this.ReductionLabel});
            this.ScanToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ScanToolStrip.Name = "ScanToolStrip";
            this.ScanToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ScanToolStrip.Size = new System.Drawing.Size(486, 25);
            this.ScanToolStrip.TabIndex = 141;
            this.ScanToolStrip.Text = "toolStrip1";
            // 
            // ApplyThresholdButton
            // 
            this.ApplyThresholdButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ApplyThresholdButton.Image = global::Anathema.Properties.Resources.RightArrow;
            this.ApplyThresholdButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ApplyThresholdButton.Name = "ApplyThresholdButton";
            this.ApplyThresholdButton.Size = new System.Drawing.Size(23, 22);
            this.ApplyThresholdButton.Text = "Apply Threshold";
            this.ApplyThresholdButton.Click += new System.EventHandler(this.ApplyThresholdButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RefreshButton.Image = global::Anathema.Properties.Resources.Undo;
            this.RefreshButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(23, 22);
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // InvertSelectionButton
            // 
            this.InvertSelectionButton.CheckOnClick = true;
            this.InvertSelectionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.InvertSelectionButton.Image = global::Anathema.Properties.Resources.Invert;
            this.InvertSelectionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.InvertSelectionButton.Name = "InvertSelectionButton";
            this.InvertSelectionButton.Size = new System.Drawing.Size(23, 22);
            this.InvertSelectionButton.Text = "Invert Selection";
            this.InvertSelectionButton.Click += new System.EventHandler(this.InvertSelectionButton_Click);
            // 
            // ReductionLabel
            // 
            this.ReductionLabel.Name = "ReductionLabel";
            this.ReductionLabel.Size = new System.Drawing.Size(73, 22);
            this.ReductionLabel.Text = "Reduction: 0";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.MaxLabelLabel);
            this.panel1.Controls.Add(this.MinLabelLabel);
            this.panel1.Controls.Add(this.MinValueTrackBar);
            this.panel1.Controls.Add(this.MaxValueTrackBar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 211);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(486, 70);
            this.panel1.TabIndex = 142;
            // 
            // MaxLabelLabel
            // 
            this.MaxLabelLabel.AutoSize = true;
            this.MaxLabelLabel.Location = new System.Drawing.Point(3, 40);
            this.MaxLabelLabel.Name = "MaxLabelLabel";
            this.MaxLabelLabel.Size = new System.Drawing.Size(30, 13);
            this.MaxLabelLabel.TabIndex = 4;
            this.MaxLabelLabel.Text = "Max:";
            // 
            // MinLabelLabel
            // 
            this.MinLabelLabel.AutoSize = true;
            this.MinLabelLabel.Location = new System.Drawing.Point(3, 10);
            this.MinLabelLabel.Name = "MinLabelLabel";
            this.MinLabelLabel.Size = new System.Drawing.Size(27, 13);
            this.MinLabelLabel.TabIndex = 3;
            this.MinLabelLabel.Text = "Min:";
            // 
            // GUILabelThresholder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 281);
            this.Controls.Add(this.LabelFrequencyChart);
            this.Controls.Add(this.ScanToolStrip);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUILabelThresholder";
            this.Text = "Label Thresholder";
            ((System.ComponentModel.ISupportInitialize)(this.LabelFrequencyChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxValueTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinValueTrackBar)).EndInit();
            this.ScanToolStrip.ResumeLayout(false);
            this.ScanToolStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart LabelFrequencyChart;
        private System.Windows.Forms.TrackBar MaxValueTrackBar;
        private System.Windows.Forms.TrackBar MinValueTrackBar;
        private System.Windows.Forms.ToolStrip ScanToolStrip;
        private System.Windows.Forms.ToolStripButton ApplyThresholdButton;
        private System.Windows.Forms.ToolStripButton RefreshButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label MaxLabelLabel;
        private System.Windows.Forms.Label MinLabelLabel;
        private System.Windows.Forms.ToolStripLabel ReductionLabel;
        private System.Windows.Forms.ToolStripButton InvertSelectionButton;
    }
}