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
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.LabelFrequencyChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.MaxValueTrackBar = new System.Windows.Forms.TrackBar();
            this.MinValueTrackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.LabelFrequencyChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxValueTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinValueTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelFrequencyChart
            // 
            chartArea1.Name = "ChartArea1";
            this.LabelFrequencyChart.ChartAreas.Add(chartArea1);
            this.LabelFrequencyChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.LabelFrequencyChart.Legends.Add(legend1);
            this.LabelFrequencyChart.Location = new System.Drawing.Point(0, 0);
            this.LabelFrequencyChart.Name = "LabelFrequencyChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Frequency";
            this.LabelFrequencyChart.Series.Add(series1);
            this.LabelFrequencyChart.Size = new System.Drawing.Size(469, 241);
            this.LabelFrequencyChart.TabIndex = 0;
            this.LabelFrequencyChart.Text = "Label Thresholder";
            // 
            // MaxValueTrackBar
            // 
            this.MaxValueTrackBar.AutoSize = false;
            this.MaxValueTrackBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.MaxValueTrackBar.Location = new System.Drawing.Point(0, 265);
            this.MaxValueTrackBar.Name = "MaxValueTrackBar";
            this.MaxValueTrackBar.Size = new System.Drawing.Size(469, 24);
            this.MaxValueTrackBar.TabIndex = 1;
            this.MaxValueTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.MaxValueTrackBar.Value = 10;
            // 
            // MinValueTrackBar
            // 
            this.MinValueTrackBar.AutoSize = false;
            this.MinValueTrackBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.MinValueTrackBar.Location = new System.Drawing.Point(0, 241);
            this.MinValueTrackBar.Name = "MinValueTrackBar";
            this.MinValueTrackBar.Size = new System.Drawing.Size(469, 24);
            this.MinValueTrackBar.TabIndex = 2;
            this.MinValueTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // GUILabelThresholder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 289);
            this.Controls.Add(this.LabelFrequencyChart);
            this.Controls.Add(this.MinValueTrackBar);
            this.Controls.Add(this.MaxValueTrackBar);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GUILabelThresholder";
            this.Text = "Label Thresholder";
            ((System.ComponentModel.ISupportInitialize)(this.LabelFrequencyChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxValueTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinValueTrackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart LabelFrequencyChart;
        private System.Windows.Forms.TrackBar MaxValueTrackBar;
        private System.Windows.Forms.TrackBar MinValueTrackBar;
    }
}