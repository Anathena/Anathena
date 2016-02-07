namespace Anathema
{
    partial class GUIAddressTable
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
            this.AddressTableListView = new Anathema.CheckableListView();
            this.FrozenHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressDescriptionHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AddressHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TypeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ValueHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // AddressTableListView
            // 
            this.AddressTableListView.BackColor = System.Drawing.SystemColors.Control;
            this.AddressTableListView.CheckBoxes = true;
            this.AddressTableListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FrozenHeader,
            this.AddressDescriptionHeader,
            this.AddressHeader,
            this.TypeHeader,
            this.ValueHeader});
            this.AddressTableListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AddressTableListView.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressTableListView.FullRowSelect = true;
            this.AddressTableListView.Location = new System.Drawing.Point(0, 0);
            this.AddressTableListView.Name = "AddressTableListView";
            this.AddressTableListView.OwnerDraw = true;
            this.AddressTableListView.ShowGroups = false;
            this.AddressTableListView.Size = new System.Drawing.Size(473, 200);
            this.AddressTableListView.TabIndex = 144;
            this.AddressTableListView.UseCompatibleStateImageBehavior = false;
            this.AddressTableListView.View = System.Windows.Forms.View.Details;
            this.AddressTableListView.VirtualMode = true;
            // 
            // FrozenHeader
            // 
            this.FrozenHeader.Text = "Frozen";
            this.FrozenHeader.Width = 54;
            // 
            // AddressDescriptionHeader
            // 
            this.AddressDescriptionHeader.Text = "Description";
            this.AddressDescriptionHeader.Width = 146;
            // 
            // AddressHeader
            // 
            this.AddressHeader.Text = "Address";
            this.AddressHeader.Width = 87;
            // 
            // TypeHeader
            // 
            this.TypeHeader.Text = "Type";
            this.TypeHeader.Width = 70;
            // 
            // ValueHeader
            // 
            this.ValueHeader.Text = "Value";
            this.ValueHeader.Width = 106;
            // 
            // GUIAddressTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AddressTableListView);
            this.Name = "GUIAddressTable";
            this.Size = new System.Drawing.Size(473, 200);
            this.ResumeLayout(false);

        }

        #endregion

        private CheckableListView AddressTableListView;
        private System.Windows.Forms.ColumnHeader FrozenHeader;
        private System.Windows.Forms.ColumnHeader AddressDescriptionHeader;
        private System.Windows.Forms.ColumnHeader AddressHeader;
        private System.Windows.Forms.ColumnHeader TypeHeader;
        private System.Windows.Forms.ColumnHeader ValueHeader;
    }
}
