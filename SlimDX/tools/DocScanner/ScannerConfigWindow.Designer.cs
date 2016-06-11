namespace DocScanner
{
    partial class ScannerConfigWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScannerConfigWindow));
			this.openDialog = new System.Windows.Forms.OpenFileDialog();
			this.MemberTypesGroupBox = new System.Windows.Forms.GroupBox();
			this.EventsCheckBox = new System.Windows.Forms.CheckBox();
			this.FieldsCheckBox = new System.Windows.Forms.CheckBox();
			this.PropertiesCheckBox = new System.Windows.Forms.CheckBox();
			this.MethodsCheckBox = new System.Windows.Forms.CheckBox();
			this.TypesCheckBox = new System.Windows.Forms.CheckBox();
			this.NamespacesCheckBox = new System.Windows.Forms.CheckBox();
			this.TagsAvailableLabel = new System.Windows.Forms.Label();
			this.AvailableTagsList = new System.Windows.Forms.ListBox();
			this.AddTagToPresentButton = new System.Windows.Forms.Button();
			this.RemoveTagFromPresentButton = new System.Windows.Forms.Button();
			this.TagsPresentList = new System.Windows.Forms.ListBox();
			this.TagsPresentLabel = new System.Windows.Forms.Label();
			this.TagsMissingLabel = new System.Windows.Forms.Label();
			this.TagsMissingList = new System.Windows.Forms.ListBox();
			this.RemoveTagFromMissingButton = new System.Windows.Forms.Button();
			this.AddTagToMissingButton = new System.Windows.Forms.Button();
			this.AssemblyPathTextBox = new System.Windows.Forms.TextBox();
			this.BrowseButton = new System.Windows.Forms.Button();
			this.SearchButton = new System.Windows.Forms.Button();
			this.MemberTypesGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// openDialog
			// 
			this.openDialog.Filter = "Assemblies|*.dll";
			this.openDialog.Title = "Open Assembly";
			// 
			// MemberTypesGroupBox
			// 
			this.MemberTypesGroupBox.Controls.Add(this.EventsCheckBox);
			this.MemberTypesGroupBox.Controls.Add(this.FieldsCheckBox);
			this.MemberTypesGroupBox.Controls.Add(this.PropertiesCheckBox);
			this.MemberTypesGroupBox.Controls.Add(this.MethodsCheckBox);
			this.MemberTypesGroupBox.Controls.Add(this.TypesCheckBox);
			this.MemberTypesGroupBox.Controls.Add(this.NamespacesCheckBox);
			this.MemberTypesGroupBox.Location = new System.Drawing.Point(6, 42);
			this.MemberTypesGroupBox.Name = "MemberTypesGroupBox";
			this.MemberTypesGroupBox.Size = new System.Drawing.Size(445, 52);
			this.MemberTypesGroupBox.TabIndex = 1;
			this.MemberTypesGroupBox.TabStop = false;
			this.MemberTypesGroupBox.Text = "Member Types";
			// 
			// EventsCheckBox
			// 
			this.EventsCheckBox.AutoSize = true;
			this.EventsCheckBox.Location = new System.Drawing.Point(377, 20);
			this.EventsCheckBox.Name = "EventsCheckBox";
			this.EventsCheckBox.Size = new System.Drawing.Size(59, 17);
			this.EventsCheckBox.TabIndex = 5;
			this.EventsCheckBox.Text = "Events";
			this.EventsCheckBox.UseVisualStyleBackColor = true;
			// 
			// FieldsCheckBox
			// 
			this.FieldsCheckBox.AutoSize = true;
			this.FieldsCheckBox.Location = new System.Drawing.Point(317, 20);
			this.FieldsCheckBox.Name = "FieldsCheckBox";
			this.FieldsCheckBox.Size = new System.Drawing.Size(53, 17);
			this.FieldsCheckBox.TabIndex = 4;
			this.FieldsCheckBox.Text = "Fields";
			this.FieldsCheckBox.UseVisualStyleBackColor = true;
			// 
			// PropertiesCheckBox
			// 
			this.PropertiesCheckBox.AutoSize = true;
			this.PropertiesCheckBox.Location = new System.Drawing.Point(237, 20);
			this.PropertiesCheckBox.Name = "PropertiesCheckBox";
			this.PropertiesCheckBox.Size = new System.Drawing.Size(73, 17);
			this.PropertiesCheckBox.TabIndex = 3;
			this.PropertiesCheckBox.Text = "Properties";
			this.PropertiesCheckBox.UseVisualStyleBackColor = true;
			// 
			// MethodsCheckBox
			// 
			this.MethodsCheckBox.AutoSize = true;
			this.MethodsCheckBox.Location = new System.Drawing.Point(164, 20);
			this.MethodsCheckBox.Name = "MethodsCheckBox";
			this.MethodsCheckBox.Size = new System.Drawing.Size(67, 17);
			this.MethodsCheckBox.TabIndex = 2;
			this.MethodsCheckBox.Text = "Methods";
			this.MethodsCheckBox.UseVisualStyleBackColor = true;
			// 
			// TypesCheckBox
			// 
			this.TypesCheckBox.AutoSize = true;
			this.TypesCheckBox.Location = new System.Drawing.Point(102, 20);
			this.TypesCheckBox.Name = "TypesCheckBox";
			this.TypesCheckBox.Size = new System.Drawing.Size(55, 17);
			this.TypesCheckBox.TabIndex = 1;
			this.TypesCheckBox.Text = "Types";
			this.TypesCheckBox.UseVisualStyleBackColor = true;
			// 
			// NamespacesCheckBox
			// 
			this.NamespacesCheckBox.AutoSize = true;
			this.NamespacesCheckBox.Location = new System.Drawing.Point(7, 20);
			this.NamespacesCheckBox.Name = "NamespacesCheckBox";
			this.NamespacesCheckBox.Size = new System.Drawing.Size(88, 17);
			this.NamespacesCheckBox.TabIndex = 0;
			this.NamespacesCheckBox.Text = "Namespaces";
			this.NamespacesCheckBox.UseVisualStyleBackColor = true;
			// 
			// TagsAvailableLabel
			// 
			this.TagsAvailableLabel.AutoSize = true;
			this.TagsAvailableLabel.Location = new System.Drawing.Point(6, 101);
			this.TagsAvailableLabel.Name = "TagsAvailableLabel";
			this.TagsAvailableLabel.Size = new System.Drawing.Size(80, 13);
			this.TagsAvailableLabel.TabIndex = 2;
			this.TagsAvailableLabel.Text = "Available Tags:";
			// 
			// AvailableTagsList
			// 
			this.AvailableTagsList.FormattingEnabled = true;
			this.AvailableTagsList.Items.AddRange(new object[] {
            "exception",
            "param",
            "returns",
            "summary",
            "unmanaged"});
			this.AvailableTagsList.Location = new System.Drawing.Point(6, 118);
			this.AvailableTagsList.Name = "AvailableTagsList";
			this.AvailableTagsList.Size = new System.Drawing.Size(203, 238);
			this.AvailableTagsList.Sorted = true;
			this.AvailableTagsList.TabIndex = 3;
			// 
			// AddTagToPresentButton
			// 
			this.AddTagToPresentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.AddTagToPresentButton.Location = new System.Drawing.Point(215, 143);
			this.AddTagToPresentButton.Name = "AddTagToPresentButton";
			this.AddTagToPresentButton.Size = new System.Drawing.Size(27, 23);
			this.AddTagToPresentButton.TabIndex = 4;
			this.AddTagToPresentButton.Text = ">";
			this.AddTagToPresentButton.UseVisualStyleBackColor = true;
			this.AddTagToPresentButton.Click += new System.EventHandler(this.AddTagToPresentButton_Click);
			// 
			// RemoveTagFromPresentButton
			// 
			this.RemoveTagFromPresentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.RemoveTagFromPresentButton.Location = new System.Drawing.Point(215, 172);
			this.RemoveTagFromPresentButton.Name = "RemoveTagFromPresentButton";
			this.RemoveTagFromPresentButton.Size = new System.Drawing.Size(27, 23);
			this.RemoveTagFromPresentButton.TabIndex = 5;
			this.RemoveTagFromPresentButton.Text = "<";
			this.RemoveTagFromPresentButton.UseVisualStyleBackColor = true;
			this.RemoveTagFromPresentButton.Click += new System.EventHandler(this.RemoveTagFromPresentButton_Click);
			// 
			// TagsPresentList
			// 
			this.TagsPresentList.FormattingEnabled = true;
			this.TagsPresentList.Location = new System.Drawing.Point(248, 118);
			this.TagsPresentList.Name = "TagsPresentList";
			this.TagsPresentList.Size = new System.Drawing.Size(203, 108);
			this.TagsPresentList.Sorted = true;
			this.TagsPresentList.TabIndex = 6;
			// 
			// TagsPresentLabel
			// 
			this.TagsPresentLabel.AutoSize = true;
			this.TagsPresentLabel.Location = new System.Drawing.Point(245, 102);
			this.TagsPresentLabel.Name = "TagsPresentLabel";
			this.TagsPresentLabel.Size = new System.Drawing.Size(73, 13);
			this.TagsPresentLabel.TabIndex = 7;
			this.TagsPresentLabel.Text = "Tags Present:";
			// 
			// TagsMissingLabel
			// 
			this.TagsMissingLabel.AutoSize = true;
			this.TagsMissingLabel.Location = new System.Drawing.Point(245, 229);
			this.TagsMissingLabel.Name = "TagsMissingLabel";
			this.TagsMissingLabel.Size = new System.Drawing.Size(72, 13);
			this.TagsMissingLabel.TabIndex = 8;
			this.TagsMissingLabel.Text = "Tags Missing:";
			// 
			// TagsMissingList
			// 
			this.TagsMissingList.FormattingEnabled = true;
			this.TagsMissingList.Location = new System.Drawing.Point(248, 248);
			this.TagsMissingList.Name = "TagsMissingList";
			this.TagsMissingList.Size = new System.Drawing.Size(203, 108);
			this.TagsMissingList.Sorted = true;
			this.TagsMissingList.TabIndex = 9;
			// 
			// RemoveTagFromMissingButton
			// 
			this.RemoveTagFromMissingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.RemoveTagFromMissingButton.Location = new System.Drawing.Point(215, 300);
			this.RemoveTagFromMissingButton.Name = "RemoveTagFromMissingButton";
			this.RemoveTagFromMissingButton.Size = new System.Drawing.Size(27, 23);
			this.RemoveTagFromMissingButton.TabIndex = 11;
			this.RemoveTagFromMissingButton.Text = "<";
			this.RemoveTagFromMissingButton.UseVisualStyleBackColor = true;
			this.RemoveTagFromMissingButton.Click += new System.EventHandler(this.RemoveTagFromMissingButton_Click);
			// 
			// AddTagToMissingButton
			// 
			this.AddTagToMissingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.AddTagToMissingButton.Location = new System.Drawing.Point(215, 271);
			this.AddTagToMissingButton.Name = "AddTagToMissingButton";
			this.AddTagToMissingButton.Size = new System.Drawing.Size(27, 23);
			this.AddTagToMissingButton.TabIndex = 10;
			this.AddTagToMissingButton.Text = ">";
			this.AddTagToMissingButton.UseVisualStyleBackColor = true;
			this.AddTagToMissingButton.Click += new System.EventHandler(this.AddTagToMissingButton_Click);
			// 
			// AssemblyPathTextBox
			// 
			this.AssemblyPathTextBox.Location = new System.Drawing.Point(6, 15);
			this.AssemblyPathTextBox.Name = "AssemblyPathTextBox";
			this.AssemblyPathTextBox.Size = new System.Drawing.Size(405, 20);
			this.AssemblyPathTextBox.TabIndex = 12;
			// 
			// BrowseButton
			// 
			this.BrowseButton.Location = new System.Drawing.Point(417, 15);
			this.BrowseButton.Name = "BrowseButton";
			this.BrowseButton.Size = new System.Drawing.Size(29, 23);
			this.BrowseButton.TabIndex = 13;
			this.BrowseButton.Text = "...";
			this.BrowseButton.UseVisualStyleBackColor = true;
			this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
			// 
			// SearchButton
			// 
			this.SearchButton.Location = new System.Drawing.Point(191, 362);
			this.SearchButton.Name = "SearchButton";
			this.SearchButton.Size = new System.Drawing.Size(75, 23);
			this.SearchButton.TabIndex = 14;
			this.SearchButton.Text = "Search";
			this.SearchButton.UseVisualStyleBackColor = true;
			this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
			// 
			// ScannerConfigWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(457, 393);
			this.Controls.Add(this.SearchButton);
			this.Controls.Add(this.BrowseButton);
			this.Controls.Add(this.AssemblyPathTextBox);
			this.Controls.Add(this.RemoveTagFromMissingButton);
			this.Controls.Add(this.AddTagToMissingButton);
			this.Controls.Add(this.TagsMissingList);
			this.Controls.Add(this.TagsMissingLabel);
			this.Controls.Add(this.TagsPresentLabel);
			this.Controls.Add(this.TagsPresentList);
			this.Controls.Add(this.RemoveTagFromPresentButton);
			this.Controls.Add(this.AddTagToPresentButton);
			this.Controls.Add(this.AvailableTagsList);
			this.Controls.Add(this.TagsAvailableLabel);
			this.Controls.Add(this.MemberTypesGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "ScannerConfigWindow";
			this.Text = "Documentation Scanner";
			this.MemberTypesGroupBox.ResumeLayout(false);
			this.MemberTypesGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.OpenFileDialog openDialog;
		private System.Windows.Forms.GroupBox MemberTypesGroupBox;
		private System.Windows.Forms.CheckBox TypesCheckBox;
		private System.Windows.Forms.CheckBox NamespacesCheckBox;
		private System.Windows.Forms.CheckBox FieldsCheckBox;
		private System.Windows.Forms.CheckBox PropertiesCheckBox;
		private System.Windows.Forms.CheckBox MethodsCheckBox;
		private System.Windows.Forms.Label TagsAvailableLabel;
		private System.Windows.Forms.ListBox AvailableTagsList;
		private System.Windows.Forms.Button AddTagToPresentButton;
		private System.Windows.Forms.Button RemoveTagFromPresentButton;
		private System.Windows.Forms.ListBox TagsPresentList;
		private System.Windows.Forms.Label TagsPresentLabel;
		private System.Windows.Forms.Label TagsMissingLabel;
		private System.Windows.Forms.ListBox TagsMissingList;
		private System.Windows.Forms.Button RemoveTagFromMissingButton;
		private System.Windows.Forms.Button AddTagToMissingButton;
		private System.Windows.Forms.TextBox AssemblyPathTextBox;
		private System.Windows.Forms.Button BrowseButton;
		private System.Windows.Forms.Button SearchButton;
		private System.Windows.Forms.CheckBox EventsCheckBox;
    }
}

