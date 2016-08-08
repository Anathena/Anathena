using Anathena.GUI.CustomControls.ListViews;
using Anathena.GUI.CustomControls.TextBoxes;

namespace Anathena.GUI.Tools.TypeEditors
{
    partial class GUIOffsetEditor
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
            this.components = new System.ComponentModel.Container();
            this.AddOffsetButton = new System.Windows.Forms.Button();
            this.RemoveOffsetButton = new System.Windows.Forms.Button();
            this.OkayButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OffsetsListView = new Anathena.GUI.CustomControls.ListViews.FlickerFreeListView();
            this.HexHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DecimalHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OffsetsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OffsetHexDecTextBox = new Anathena.GUI.CustomControls.TextBoxes.HexDecTextBox();
            this.OffsetsContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddOffsetButton
            // 
            this.AddOffsetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddOffsetButton.Location = new System.Drawing.Point(89, 9);
            this.AddOffsetButton.Name = "AddOffsetButton";
            this.AddOffsetButton.Size = new System.Drawing.Size(75, 23);
            this.AddOffsetButton.TabIndex = 1;
            this.AddOffsetButton.Text = "Add";
            this.AddOffsetButton.UseVisualStyleBackColor = true;
            this.AddOffsetButton.Click += new System.EventHandler(this.AddOffsetButton_Click);
            // 
            // RemoveOffsetButton
            // 
            this.RemoveOffsetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveOffsetButton.Location = new System.Drawing.Point(170, 9);
            this.RemoveOffsetButton.Name = "RemoveOffsetButton";
            this.RemoveOffsetButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveOffsetButton.TabIndex = 3;
            this.RemoveOffsetButton.Text = "Remove";
            this.RemoveOffsetButton.UseVisualStyleBackColor = true;
            this.RemoveOffsetButton.Click += new System.EventHandler(this.RemoveOffsetButton_Click);
            // 
            // OkayButton
            // 
            this.OkayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkayButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkayButton.Location = new System.Drawing.Point(170, 163);
            this.OkayButton.Name = "OkayButton";
            this.OkayButton.Size = new System.Drawing.Size(75, 23);
            this.OkayButton.TabIndex = 4;
            this.OkayButton.Text = "Okay";
            this.OkayButton.UseVisualStyleBackColor = true;
            this.OkayButton.Click += new System.EventHandler(this.OkayButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(12, 163);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OffsetsListView
            // 
            this.OffsetsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OffsetsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HexHeader,
            this.DecimalHeader});
            this.OffsetsListView.ContextMenuStrip = this.OffsetsContextMenuStrip;
            this.OffsetsListView.FullRowSelect = true;
            this.OffsetsListView.Location = new System.Drawing.Point(12, 38);
            this.OffsetsListView.Name = "OffsetsListView";
            this.OffsetsListView.Size = new System.Drawing.Size(233, 119);
            this.OffsetsListView.TabIndex = 6;
            this.OffsetsListView.UseCompatibleStateImageBehavior = false;
            this.OffsetsListView.View = System.Windows.Forms.View.Details;
            // 
            // HexHeader
            // 
            this.HexHeader.Text = "Hex";
            this.HexHeader.Width = 96;
            // 
            // DecimalHeader
            // 
            this.DecimalHeader.Text = "Decimal";
            this.DecimalHeader.Width = 96;
            // 
            // OffsetsContextMenuStrip
            // 
            this.OffsetsContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteSelectionToolStripMenuItem});
            this.OffsetsContextMenuStrip.Name = "OffsetsContextMenuStrip";
            this.OffsetsContextMenuStrip.Size = new System.Drawing.Size(159, 26);
            // 
            // DeleteSelectionToolStripMenuItem
            // 
            this.DeleteSelectionToolStripMenuItem.Name = "DeleteSelectionToolStripMenuItem";
            this.DeleteSelectionToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.DeleteSelectionToolStripMenuItem.Text = "Delete Selection";
            this.DeleteSelectionToolStripMenuItem.Click += new System.EventHandler(this.DeleteSelectionToolStripMenuItem_Click);
            // 
            // OffsetHexDecTextBox
            // 
            this.OffsetHexDecTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OffsetHexDecTextBox.ForeColor = System.Drawing.Color.Red;
            this.OffsetHexDecTextBox.IsHex = true;
            this.OffsetHexDecTextBox.Location = new System.Drawing.Point(12, 11);
            this.OffsetHexDecTextBox.Name = "OffsetHexDecTextBox";
            this.OffsetHexDecTextBox.Size = new System.Drawing.Size(71, 20);
            this.OffsetHexDecTextBox.TabIndex = 2;
            this.OffsetHexDecTextBox.WatermarkColor = System.Drawing.Color.LightGray;
            this.OffsetHexDecTextBox.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OffsetHexDecTextBox.WaterMarkText = null;
            // 
            // GUIOffsetEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 198);
            this.Controls.Add(this.OffsetsListView);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkayButton);
            this.Controls.Add(this.RemoveOffsetButton);
            this.Controls.Add(this.OffsetHexDecTextBox);
            this.Controls.Add(this.AddOffsetButton);
            this.Name = "GUIOffsetEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Offset Editor";
            this.OffsetsContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button AddOffsetButton;
        private HexDecTextBox OffsetHexDecTextBox;
        private System.Windows.Forms.Button RemoveOffsetButton;
        private System.Windows.Forms.Button OkayButton;
        private System.Windows.Forms.Button CancelButton;
        private FlickerFreeListView OffsetsListView;
        private System.Windows.Forms.ColumnHeader DecimalHeader;
        private System.Windows.Forms.ColumnHeader HexHeader;
        private System.Windows.Forms.ContextMenuStrip OffsetsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DeleteSelectionToolStripMenuItem;
    }
}