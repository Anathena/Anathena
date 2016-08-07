using Anathema.GUI.CustomControls.ListViews;
using Anathema.GUI.CustomControls.TextBoxes;

namespace Anathema.GUI.Tools.TypeEditors
{
    partial class GUIHotKeyEditor
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
            this.HotKeyListView = new Anathema.GUI.CustomControls.ListViews.FlickerFreeListView();
            this.HotKeyHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ActionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OffsetsContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HotKeyTextBox = new Anathema.GUI.CustomControls.TextBoxes.WatermarkTextBox();
            this.OffsetsContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddOffsetButton
            // 
            this.AddOffsetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddOffsetButton.Location = new System.Drawing.Point(136, 9);
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
            this.RemoveOffsetButton.Location = new System.Drawing.Point(217, 9);
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
            this.OkayButton.Location = new System.Drawing.Point(217, 150);
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
            this.CancelButton.Location = new System.Drawing.Point(12, 150);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // HotKeyListView
            // 
            this.HotKeyListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HotKeyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HotKeyHeader,
            this.ActionHeader});
            this.HotKeyListView.ContextMenuStrip = this.OffsetsContextMenuStrip;
            this.HotKeyListView.FullRowSelect = true;
            this.HotKeyListView.Location = new System.Drawing.Point(12, 38);
            this.HotKeyListView.Name = "HotKeyListView";
            this.HotKeyListView.Size = new System.Drawing.Size(280, 106);
            this.HotKeyListView.TabIndex = 6;
            this.HotKeyListView.UseCompatibleStateImageBehavior = false;
            this.HotKeyListView.View = System.Windows.Forms.View.Details;
            // 
            // HotKeyHeader
            // 
            this.HotKeyHeader.Text = "Hot Key";
            this.HotKeyHeader.Width = 128;
            // 
            // ActionHeader
            // 
            this.ActionHeader.Text = "Action";
            this.ActionHeader.Width = 128;
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
            // HotKeyTextBox
            // 
            this.HotKeyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HotKeyTextBox.Location = new System.Drawing.Point(12, 11);
            this.HotKeyTextBox.Name = "HotKeyTextBox";
            this.HotKeyTextBox.Size = new System.Drawing.Size(118, 20);
            this.HotKeyTextBox.TabIndex = 7;
            this.HotKeyTextBox.WatermarkColor = System.Drawing.Color.LightGray;
            this.HotKeyTextBox.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HotKeyTextBox.WaterMarkText = null;
            // 
            // GUIHotKeyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 185);
            this.Controls.Add(this.HotKeyTextBox);
            this.Controls.Add(this.HotKeyListView);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkayButton);
            this.Controls.Add(this.RemoveOffsetButton);
            this.Controls.Add(this.AddOffsetButton);
            this.Name = "GUIHotKeyEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HotKey Editor";
            this.OffsetsContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button AddOffsetButton;
        private System.Windows.Forms.Button RemoveOffsetButton;
        private System.Windows.Forms.Button OkayButton;
        private System.Windows.Forms.Button CancelButton;
        private FlickerFreeListView HotKeyListView;
        private System.Windows.Forms.ColumnHeader ActionHeader;
        private System.Windows.Forms.ColumnHeader HotKeyHeader;
        private System.Windows.Forms.ContextMenuStrip OffsetsContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DeleteSelectionToolStripMenuItem;
        private WatermarkTextBox HotKeyTextBox;
    }
}