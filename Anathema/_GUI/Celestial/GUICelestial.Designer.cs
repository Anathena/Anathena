namespace Anathema
{
    partial class GUICelestial
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
            this.DisassemblerListView = new System.Windows.Forms.ListView();
            this.AddressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BytesHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.InstructionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CommentHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.HexEditorBox = new Be.Windows.Forms.HexBox();
            this.SuspendLayout();
            // 
            // DisassemblerListView
            // 
            this.DisassemblerListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DisassemblerListView.BackColor = System.Drawing.SystemColors.Control;
            this.DisassemblerListView.CheckBoxes = true;
            this.DisassemblerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.AddressHeader,
            this.BytesHeader,
            this.InstructionHeader,
            this.CommentHeader});
            this.DisassemblerListView.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisassemblerListView.FullRowSelect = true;
            this.DisassemblerListView.Location = new System.Drawing.Point(3, 3);
            this.DisassemblerListView.Name = "DisassemblerListView";
            this.DisassemblerListView.ShowGroups = false;
            this.DisassemblerListView.Size = new System.Drawing.Size(571, 177);
            this.DisassemblerListView.TabIndex = 114;
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
            // HexEditorBox
            // 
            this.HexEditorBox.AllowDrop = true;
            this.HexEditorBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HexEditorBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            // 
            // 
            // 
            this.HexEditorBox.BuiltInContextMenu.CopyMenuItemText = "Copy ";
            this.HexEditorBox.BuiltInContextMenu.CutMenuItemText = "Cut ";
            this.HexEditorBox.BuiltInContextMenu.PasteMenuItemText = "Paste ";
            this.HexEditorBox.BuiltInContextMenu.SelectAllMenuItemText = "Select All ";
            this.HexEditorBox.ColumnInfoVisible = true;
            this.HexEditorBox.Font = new System.Drawing.Font("Consolas", 9F);
            this.HexEditorBox.HexCasing = Be.Windows.Forms.HexCasing.Lower;
            this.HexEditorBox.LineInfoVisible = true;
            this.HexEditorBox.Location = new System.Drawing.Point(3, 186);
            this.HexEditorBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.HexEditorBox.Name = "HexEditorBox";
            this.HexEditorBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.HexEditorBox.Size = new System.Drawing.Size(570, 154);
            this.HexEditorBox.StringViewVisible = true;
            this.HexEditorBox.TabIndex = 116;
            this.HexEditorBox.VScrollBarVisible = true;
            // 
            // GUICelestial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.HexEditorBox);
            this.Controls.Add(this.DisassemblerListView);
            this.Name = "GUICelestial";
            this.Size = new System.Drawing.Size(577, 343);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView DisassemblerListView;
        private System.Windows.Forms.ColumnHeader AddressHeader;
        private System.Windows.Forms.ColumnHeader BytesHeader;
        private System.Windows.Forms.ColumnHeader InstructionHeader;
        private System.Windows.Forms.ColumnHeader CommentHeader;
        private Be.Windows.Forms.HexBox HexEditorBox;
    }
}
