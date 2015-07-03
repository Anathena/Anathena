namespace Anathema
{
    partial class PageVisualizer
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.PageChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.CheckBoxHideConstant = new System.Windows.Forms.CheckBox();
            this.CheckBoxHideDynamic = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.PageChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // PageChart
            // 
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisY.LabelStyle.Enabled = false;
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.Transparent;
            chartArea2.AxisY.LineWidth = 0;
            chartArea2.AxisY.MajorGrid.Enabled = false;
            chartArea2.AxisY.MajorTickMark.Enabled = false;
            chartArea2.Name = "ChartArea1";
            this.PageChart.ChartAreas.Add(chartArea2);
            this.PageChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.PageChart.Legends.Add(legend2);
            this.PageChart.Location = new System.Drawing.Point(0, 0);
            this.PageChart.Name = "PageChart";
            series2.BorderWidth = 4;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StepLine;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.PageChart.Series.Add(series2);
            this.PageChart.Size = new System.Drawing.Size(522, 392);
            this.PageChart.TabIndex = 0;
            this.PageChart.Text = "chart1";
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Name = "SplitContainer";
            this.SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.CheckBoxHideConstant);
            this.SplitContainer.Panel1.Controls.Add(this.CheckBoxHideDynamic);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.PageChart);
            this.SplitContainer.Size = new System.Drawing.Size(522, 429);
            this.SplitContainer.SplitterDistance = 33;
            this.SplitContainer.TabIndex = 2;
            // 
            // CheckBoxHideConstant
            // 
            this.CheckBoxHideConstant.AutoSize = true;
            this.CheckBoxHideConstant.Checked = true;
            this.CheckBoxHideConstant.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxHideConstant.Location = new System.Drawing.Point(12, 12);
            this.CheckBoxHideConstant.Name = "CheckBoxHideConstant";
            this.CheckBoxHideConstant.Size = new System.Drawing.Size(126, 17);
            this.CheckBoxHideConstant.TabIndex = 0;
            this.CheckBoxHideConstant.Text = "Hide Constant Pages";
            this.CheckBoxHideConstant.UseVisualStyleBackColor = true;
            this.CheckBoxHideConstant.CheckedChanged += new System.EventHandler(this.CheckBoxHideConstant_CheckedChanged);
            // 
            // CheckBoxHideDynamic
            // 
            this.CheckBoxHideDynamic.AutoSize = true;
            this.CheckBoxHideDynamic.Location = new System.Drawing.Point(144, 12);
            this.CheckBoxHideDynamic.Name = "CheckBoxHideDynamic";
            this.CheckBoxHideDynamic.Size = new System.Drawing.Size(125, 17);
            this.CheckBoxHideDynamic.TabIndex = 0;
            this.CheckBoxHideDynamic.Text = "Hide Dynamic Pages";
            this.CheckBoxHideDynamic.UseVisualStyleBackColor = true;
            this.CheckBoxHideDynamic.CheckedChanged += new System.EventHandler(this.CheckBoxHideDynamic_CheckedChanged);
            // 
            // PageVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 429);
            this.Controls.Add(this.SplitContainer);
            this.Name = "PageVisualizer";
            this.Text = "PageVisualizer";
            this.Load += new System.EventHandler(this.PageVisualizer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PageChart)).EndInit();
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel1.PerformLayout();
            this.SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart PageChart;
        private System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.CheckBox CheckBoxHideConstant;
        private System.Windows.Forms.CheckBox CheckBoxHideDynamic;
    }
}