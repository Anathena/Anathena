using Ana.GUI.CustomControls.ListViews;
using Ana.GUI.CustomControls.TextBoxes;

namespace Ana.GUI.Tools.TypeEditors
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
            this.AddHotKeyButton = new System.Windows.Forms.Button();
            this.RemoveHotKeyButton = new System.Windows.Forms.Button();
            this.OkayButton = new System.Windows.Forms.Button();
            this.CancelHotKeyButton = new System.Windows.Forms.Button();
            this.hotKeyListView = new Ana.GUI.CustomControls.ListViews.FlickerFreeListView();
            this.HotKeyHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HotKeyContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HotKeyTextBox = new Ana.GUI.CustomControls.TextBoxes.WatermarkTextBox();
            this.ClearHotKeyButton = new System.Windows.Forms.Button();
            this.HotKeyContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddHotKeyButton
            // 
            this.AddHotKeyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddHotKeyButton.Location = new System.Drawing.Point(148, 9);
            this.AddHotKeyButton.Name = "AddHotKeyButton";
            this.AddHotKeyButton.Size = new System.Drawing.Size(75, 23);
            this.AddHotKeyButton.TabIndex = 1;
            this.AddHotKeyButton.Text = "Add";
            this.AddHotKeyButton.UseVisualStyleBackColor = true;
            this.AddHotKeyButton.Click += new System.EventHandler(this.AddHotKeyButton_Click);
            // 
            // RemoveHotKeyButton
            // 
            this.RemoveHotKeyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RemoveHotKeyButton.Location = new System.Drawing.Point(12, 150);
            this.RemoveHotKeyButton.Name = "RemoveHotKeyButton";
            this.RemoveHotKeyButton.Size = new System.Drawing.Size(75, 23);
            this.RemoveHotKeyButton.TabIndex = 3;
            this.RemoveHotKeyButton.Text = "Remove";
            this.RemoveHotKeyButton.UseVisualStyleBackColor = true;
            this.RemoveHotKeyButton.Click += new System.EventHandler(this.RemoveHotKeyButton_Click);
            // 
            // OkayButton
            // 
            this.OkayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkayButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkayButton.Location = new System.Drawing.Point(148, 150);
            this.OkayButton.Name = "OkayButton";
            this.OkayButton.Size = new System.Drawing.Size(75, 23);
            this.OkayButton.TabIndex = 4;
            this.OkayButton.Text = "Okay";
            this.OkayButton.UseVisualStyleBackColor = true;
            this.OkayButton.Click += new System.EventHandler(this.OkayButton_Click);
            // 
            // CancelButton
            // 
            this.CancelHotKeyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelHotKeyButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelHotKeyButton.Location = new System.Drawing.Point(229, 150);
            this.CancelHotKeyButton.Name = "CancelButton";
            this.CancelHotKeyButton.Size = new System.Drawing.Size(75, 23);
            this.CancelHotKeyButton.TabIndex = 5;
            this.CancelHotKeyButton.Text = "Cancel";
            this.CancelHotKeyButton.UseVisualStyleBackColor = true;
            this.CancelHotKeyButton.Click += new System.EventHandler(this.CancelHotKeyButton_Click);
            // 
            // HotKeyListView
            // 
            this.hotKeyListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hotKeyListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HotKeyHeader});
            this.hotKeyListView.ContextMenuStrip = this.HotKeyContextMenuStrip;
            this.hotKeyListView.FullRowSelect = true;
            this.hotKeyListView.Location = new System.Drawing.Point(12, 38);
            this.hotKeyListView.Name = "HotKeyListView";
            this.hotKeyListView.Size = new System.Drawing.Size(292, 106);
            this.hotKeyListView.TabIndex = 6;
            this.hotKeyListView.UseCompatibleStateImageBehavior = false;
            this.hotKeyListView.View = System.Windows.Forms.View.Details;
            // 
            // HotKeyHeader
            // 
            this.HotKeyHeader.Text = "Hot Key";
            this.HotKeyHeader.Width = 256;
            // 
            // HotKeyContextMenuStrip
            // 
            this.HotKeyContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteSelectionToolStripMenuItem});
            this.HotKeyContextMenuStrip.Name = "OffsetsContextMenuStrip";
            this.HotKeyContextMenuStrip.Size = new System.Drawing.Size(159, 26);
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
            this.HotKeyTextBox.Size = new System.Drawing.Size(130, 20);
            this.HotKeyTextBox.TabIndex = 7;
            this.HotKeyTextBox.WatermarkColor = System.Drawing.Color.LightGray;
            this.HotKeyTextBox.WaterMarkFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HotKeyTextBox.WaterMarkText = "(Press any key)";
            // 
            // ClearHotKeyButton
            // 
            this.ClearHotKeyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearHotKeyButton.Location = new System.Drawing.Point(229, 9);
            this.ClearHotKeyButton.Name = "ClearHotKeyButton";
            this.ClearHotKeyButton.Size = new System.Drawing.Size(75, 23);
            this.ClearHotKeyButton.TabIndex = 8;
            this.ClearHotKeyButton.Text = "Clear";
            this.ClearHotKeyButton.UseVisualStyleBackColor = true;
            this.ClearHotKeyButton.Click += new System.EventHandler(this.ClearHotKeyButton_Click);
            // 
            // GUIHotKeyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 185);
            this.Controls.Add(this.ClearHotKeyButton);
            this.Controls.Add(this.HotKeyTextBox);
            this.Controls.Add(this.hotKeyListView);
            this.Controls.Add(this.CancelHotKeyButton);
            this.Controls.Add(this.OkayButton);
            this.Controls.Add(this.RemoveHotKeyButton);
            this.Controls.Add(this.AddHotKeyButton);
            this.Name = "GUIHotKeyEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HotKey Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GUIHotKeyEditor_FormClosed);
            this.HotKeyContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button AddHotKeyButton;
        private System.Windows.Forms.Button RemoveHotKeyButton;
        private System.Windows.Forms.Button OkayButton;
        private System.Windows.Forms.Button CancelHotKeyButton;
        private FlickerFreeListView hotKeyListView;
        private System.Windows.Forms.ColumnHeader HotKeyHeader;
        private System.Windows.Forms.ContextMenuStrip HotKeyContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem DeleteSelectionToolStripMenuItem;
        private WatermarkTextBox HotKeyTextBox;
        private System.Windows.Forms.Button ClearHotKeyButton;
    }
}