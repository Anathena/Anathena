namespace Anathema
{
    partial class GUIResults
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIResults));
            this.AddressListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LabelHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MainToolStrip = new System.Windows.Forms.ToolStrip();
            this.OpenATButton = new System.Windows.Forms.ToolStripButton();
            this.MergeATButton = new System.Windows.Forms.ToolStripButton();
            this.SaveATButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.AddSelectedButton = new System.Windows.Forms.ToolStripButton();
            this.AddSpecificButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ClearTableButton = new System.Windows.Forms.ToolStripButton();
            this.UndoTableDeleteButton = new System.Windows.Forms.ToolStripButton();
            this.AddressCount = new System.Windows.Forms.Label();
            this.MainToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddressListView
            // 
            this.AddressListView.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.AddressListView.BackColor = System.Drawing.SystemColors.Control;
            this.AddressListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.LabelHeader,
            this.columnHeader2});
            this.AddressListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AddressListView.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressListView.FullRowSelect = true;
            this.AddressListView.Location = new System.Drawing.Point(0, 17);
            this.AddressListView.Name = "AddressListView";
            this.AddressListView.Size = new System.Drawing.Size(284, 219);
            this.AddressListView.TabIndex = 151;
            this.AddressListView.UseCompatibleStateImageBehavior = false;
            this.AddressListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Address";
            this.columnHeader1.Width = 86;
            // 
            // LabelHeader
            // 
            this.LabelHeader.Text = "Label";
            this.LabelHeader.Width = 86;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            // 
            // MainToolStrip
            // 
            this.MainToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.MainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenATButton,
            this.MergeATButton,
            this.SaveATButton,
            this.toolStripSeparator3,
            this.AddSelectedButton,
            this.AddSpecificButton,
            this.toolStripSeparator5,
            this.ClearTableButton,
            this.UndoTableDeleteButton});
            this.MainToolStrip.Location = new System.Drawing.Point(0, 236);
            this.MainToolStrip.Name = "MainToolStrip";
            this.MainToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.MainToolStrip.Size = new System.Drawing.Size(284, 25);
            this.MainToolStrip.TabIndex = 153;
            // 
            // OpenATButton
            // 
            this.OpenATButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenATButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenATButton.Image")));
            this.OpenATButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenATButton.Name = "OpenATButton";
            this.OpenATButton.Size = new System.Drawing.Size(23, 22);
            // 
            // MergeATButton
            // 
            this.MergeATButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MergeATButton.Image = ((System.Drawing.Image)(resources.GetObject("MergeATButton.Image")));
            this.MergeATButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MergeATButton.Name = "MergeATButton";
            this.MergeATButton.Size = new System.Drawing.Size(23, 22);
            // 
            // SaveATButton
            // 
            this.SaveATButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveATButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveATButton.Image")));
            this.SaveATButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveATButton.Name = "SaveATButton";
            this.SaveATButton.Size = new System.Drawing.Size(23, 22);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // AddSelectedButton
            // 
            this.AddSelectedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddSelectedButton.Image = ((System.Drawing.Image)(resources.GetObject("AddSelectedButton.Image")));
            this.AddSelectedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddSelectedButton.Name = "AddSelectedButton";
            this.AddSelectedButton.Size = new System.Drawing.Size(23, 22);
            // 
            // AddSpecificButton
            // 
            this.AddSpecificButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddSpecificButton.Image = ((System.Drawing.Image)(resources.GetObject("AddSpecificButton.Image")));
            this.AddSpecificButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddSpecificButton.Name = "AddSpecificButton";
            this.AddSpecificButton.Size = new System.Drawing.Size(23, 22);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // ClearTableButton
            // 
            this.ClearTableButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ClearTableButton.Enabled = false;
            this.ClearTableButton.Image = ((System.Drawing.Image)(resources.GetObject("ClearTableButton.Image")));
            this.ClearTableButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearTableButton.Name = "ClearTableButton";
            this.ClearTableButton.Size = new System.Drawing.Size(23, 22);
            // 
            // UndoTableDeleteButton
            // 
            this.UndoTableDeleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UndoTableDeleteButton.Enabled = false;
            this.UndoTableDeleteButton.Image = ((System.Drawing.Image)(resources.GetObject("UndoTableDeleteButton.Image")));
            this.UndoTableDeleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UndoTableDeleteButton.Name = "UndoTableDeleteButton";
            this.UndoTableDeleteButton.Size = new System.Drawing.Size(23, 22);
            // 
            // AddressCount
            // 
            this.AddressCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AddressCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.AddressCount.Location = new System.Drawing.Point(0, 0);
            this.AddressCount.Name = "AddressCount";
            this.AddressCount.Size = new System.Drawing.Size(284, 17);
            this.AddressCount.TabIndex = 152;
            this.AddressCount.Text = "Items: 0";
            // 
            // DockableWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.AddressListView);
            this.Controls.Add(this.MainToolStrip);
            this.Controls.Add(this.AddressCount);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DockableWindow";
            this.Text = "Results";
            this.MainToolStrip.ResumeLayout(false);
            this.MainToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView AddressListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader LabelHeader;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStrip MainToolStrip;
        private System.Windows.Forms.ToolStripButton OpenATButton;
        private System.Windows.Forms.ToolStripButton MergeATButton;
        private System.Windows.Forms.ToolStripButton SaveATButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton AddSelectedButton;
        private System.Windows.Forms.ToolStripButton AddSpecificButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton ClearTableButton;
        private System.Windows.Forms.ToolStripButton UndoTableDeleteButton;
        private System.Windows.Forms.Label AddressCount;
    }
}