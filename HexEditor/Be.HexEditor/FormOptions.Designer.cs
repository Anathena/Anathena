namespace Be.HexEditor
{
    partial class FormOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOptions));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.generalTabPage = new System.Windows.Forms.TabPage();
            this.languageSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.selectLanguageLabel = new System.Windows.Forms.Label();
            this.languageComboBox = new System.Windows.Forms.ComboBox();
            this.useSystemLanguageCheckBox = new System.Windows.Forms.CheckBox();
            this.recentFilesGroupBox = new System.Windows.Forms.GroupBox();
            this.clearRecentFilesButton = new System.Windows.Forms.Button();
            this.recentFilesMaxlabel = new System.Windows.Forms.Label();
            this.recentFilesMaxTextBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl.SuspendLayout();
            this.generalTabPage.SuspendLayout();
            this.languageSettingsGroupBox.SuspendLayout();
            this.recentFilesGroupBox.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.generalTabPage);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // generalTabPage
            // 
            this.generalTabPage.Controls.Add(this.languageSettingsGroupBox);
            this.generalTabPage.Controls.Add(this.recentFilesGroupBox);
            resources.ApplyResources(this.generalTabPage, "generalTabPage");
            this.generalTabPage.Name = "generalTabPage";
            this.generalTabPage.UseVisualStyleBackColor = true;
            // 
            // languageSettingsGroupBox
            // 
            resources.ApplyResources(this.languageSettingsGroupBox, "languageSettingsGroupBox");
            this.languageSettingsGroupBox.Controls.Add(this.selectLanguageLabel);
            this.languageSettingsGroupBox.Controls.Add(this.languageComboBox);
            this.languageSettingsGroupBox.Controls.Add(this.useSystemLanguageCheckBox);
            this.languageSettingsGroupBox.Name = "languageSettingsGroupBox";
            this.languageSettingsGroupBox.TabStop = false;
            // 
            // selectLanguageLabel
            // 
            resources.ApplyResources(this.selectLanguageLabel, "selectLanguageLabel");
            this.selectLanguageLabel.Name = "selectLanguageLabel";
            // 
            // languageComboBox
            // 
            this.languageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languageComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.languageComboBox, "languageComboBox");
            this.languageComboBox.Name = "languageComboBox";
            // 
            // useSystemLanguageCheckBox
            // 
            resources.ApplyResources(this.useSystemLanguageCheckBox, "useSystemLanguageCheckBox");
            this.useSystemLanguageCheckBox.Name = "useSystemLanguageCheckBox";
            this.useSystemLanguageCheckBox.UseVisualStyleBackColor = true;
            this.useSystemLanguageCheckBox.CheckedChanged += new System.EventHandler(this.useSystemLanguageCheckBox_CheckedChanged);
            // 
            // recentFilesGroupBox
            // 
            resources.ApplyResources(this.recentFilesGroupBox, "recentFilesGroupBox");
            this.recentFilesGroupBox.Controls.Add(this.clearRecentFilesButton);
            this.recentFilesGroupBox.Controls.Add(this.recentFilesMaxlabel);
            this.recentFilesGroupBox.Controls.Add(this.recentFilesMaxTextBox);
            this.recentFilesGroupBox.Name = "recentFilesGroupBox";
            this.recentFilesGroupBox.TabStop = false;
            // 
            // clearRecentFilesButton
            // 
            resources.ApplyResources(this.clearRecentFilesButton, "clearRecentFilesButton");
            this.clearRecentFilesButton.Name = "clearRecentFilesButton";
            this.clearRecentFilesButton.UseVisualStyleBackColor = true;
            this.clearRecentFilesButton.Click += new System.EventHandler(this.clearRecentFilesButton_Click);
            // 
            // recentFilesMaxlabel
            // 
            resources.ApplyResources(this.recentFilesMaxlabel, "recentFilesMaxlabel");
            this.recentFilesMaxlabel.Name = "recentFilesMaxlabel";
            // 
            // recentFilesMaxTextBox
            // 
            resources.ApplyResources(this.recentFilesMaxTextBox, "recentFilesMaxTextBox");
            this.recentFilesMaxTextBox.Name = "recentFilesMaxTextBox";
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel
            // 
            resources.ApplyResources(this.flowLayoutPanel, "flowLayoutPanel");
            this.flowLayoutPanel.Controls.Add(this.cancelButton);
            this.flowLayoutPanel.Controls.Add(this.okButton);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            // 
            // FormOptions
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.tabControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.tabControl.ResumeLayout(false);
            this.generalTabPage.ResumeLayout(false);
            this.languageSettingsGroupBox.ResumeLayout(false);
            this.languageSettingsGroupBox.PerformLayout();
            this.recentFilesGroupBox.ResumeLayout(false);
            this.recentFilesGroupBox.PerformLayout();
            this.flowLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage generalTabPage;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox recentFilesGroupBox;
        private System.Windows.Forms.Button clearRecentFilesButton;
        private System.Windows.Forms.Label recentFilesMaxlabel;
        private System.Windows.Forms.TextBox recentFilesMaxTextBox;
        private System.Windows.Forms.GroupBox languageSettingsGroupBox;
        private System.Windows.Forms.Label selectLanguageLabel;
        private System.Windows.Forms.ComboBox languageComboBox;
        private System.Windows.Forms.CheckBox useSystemLanguageCheckBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
    }
}