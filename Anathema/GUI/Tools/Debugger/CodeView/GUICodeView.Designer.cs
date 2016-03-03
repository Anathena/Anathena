namespace Anathema.GUI
{
    partial class GUICodeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUICodeView));
            this.DisassemblerListView = new System.Windows.Forms.ListView();
            this.AddressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BytesHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.InstructionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CommentHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GUIToolStrip = new System.Windows.Forms.ToolStrip();
            this.ViewLabel = new System.Windows.Forms.ToolStripLabel();
            this.ViewStyleComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.QuickNavComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.RefreshNavigationButton = new System.Windows.Forms.ToolStripButton();
            this.GUIToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // DisassemblerListView
            // 
            this.DisassemblerListView.BackColor = System.Drawing.SystemColors.Control;
            this.DisassemblerListView.CheckBoxes = true;
            this.DisassemblerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.AddressHeader,
            this.BytesHeader,
            this.InstructionHeader,
            this.CommentHeader});
            this.DisassemblerListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisassemblerListView.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisassemblerListView.FullRowSelect = true;
            this.DisassemblerListView.Location = new System.Drawing.Point(0, 25);
            this.DisassemblerListView.Name = "DisassemblerListView";
            this.DisassemblerListView.ShowGroups = false;
            this.DisassemblerListView.Size = new System.Drawing.Size(530, 213);
            this.DisassemblerListView.TabIndex = 117;
            this.DisassemblerListView.UseCompatibleStateImageBehavior = false;
            this.DisassemblerListView.View = System.Windows.Forms.View.Details;
            // 
            // AddressHeader
            // 
            this.AddressHeader.Text = "Address";
            this.AddressHeader.Width = 137;
            // 
            // BytesHeader
            // 
            this.BytesHeader.Text = "Bytes";
            this.BytesHeader.Width = 93;
            // 
            // InstructionHeader
            // 
            this.InstructionHeader.Text = "Instruction";
            this.InstructionHeader.Width = 143;
            // 
            // CommentHeader
            // 
            this.CommentHeader.Text = "Comment";
            this.CommentHeader.Width = 279;
            // 
            // GUIToolStrip
            // 
            this.GUIToolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.GUIToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewLabel,
            this.ViewStyleComboBox,
            this.QuickNavComboBox,
            this.RefreshNavigationButton});
            this.GUIToolStrip.Location = new System.Drawing.Point(0, 0);
            this.GUIToolStrip.Name = "GUIToolStrip";
            this.GUIToolStrip.Size = new System.Drawing.Size(530, 25);
            this.GUIToolStrip.TabIndex = 150;
            this.GUIToolStrip.Text = "Main Tool Strip";
            // 
            // ViewLabel
            // 
            this.ViewLabel.Name = "ViewLabel";
            this.ViewLabel.Size = new System.Drawing.Size(35, 22);
            this.ViewLabel.Text = "View:";
            // 
            // ViewStyleComboBox
            // 
            this.ViewStyleComboBox.Name = "ViewStyleComboBox";
            this.ViewStyleComboBox.Size = new System.Drawing.Size(121, 25);
            // 
            // QuickNavComboBox
            // 
            this.QuickNavComboBox.Name = "QuickNavComboBox";
            this.QuickNavComboBox.Size = new System.Drawing.Size(121, 25);
            // 
            // RefreshNavigationButton
            // 
            this.RefreshNavigationButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RefreshNavigationButton.Image = global::Anathema.Properties.Resources.Undo;
            this.RefreshNavigationButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefreshNavigationButton.Name = "RefreshNavigationButton";
            this.RefreshNavigationButton.Size = new System.Drawing.Size(23, 22);
            this.RefreshNavigationButton.Text = "Refresh";
            // 
            // GUICodeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 238);
            this.Controls.Add(this.DisassemblerListView);
            this.Controls.Add(this.GUIToolStrip);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUICodeView";
            this.Text = "Code View";
            this.GUIToolStrip.ResumeLayout(false);
            this.GUIToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView DisassemblerListView;
        private System.Windows.Forms.ColumnHeader AddressHeader;
        private System.Windows.Forms.ColumnHeader BytesHeader;
        private System.Windows.Forms.ColumnHeader InstructionHeader;
        private System.Windows.Forms.ColumnHeader CommentHeader;
        private System.Windows.Forms.ToolStrip GUIToolStrip;
        private System.Windows.Forms.ToolStripLabel ViewLabel;
        private System.Windows.Forms.ToolStripComboBox ViewStyleComboBox;
        private System.Windows.Forms.ToolStripComboBox QuickNavComboBox;
        private System.Windows.Forms.ToolStripButton RefreshNavigationButton;
    }
}