namespace Be.HexEditor
{
    partial class FormHexEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHexEditor));
            this.menuStrip = new Be.HexEditor.Core.MenuStripEx();
            this.fileToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.openToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.recentFilesToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.editToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.cutToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.copyToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.pasteToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.copyHexStringToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.pasteHexToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.findToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.findNextToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.goToToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.viewToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.encodingToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.bitsToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.toolsToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.optionsToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.helpToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.aboutToolStripMenuItem = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.toolStrip = new Be.HexEditor.Core.ToolStripEx();
            this.openToolStripButton = new Be.HexEditor.Core.ToolStripButtonEx();
            this.saveToolStripButton = new Be.HexEditor.Core.ToolStripButtonEx();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripButton = new Be.HexEditor.Core.ToolStripButtonEx();
            this.copyToolStripSplitButton = new Be.HexEditor.Core.ToolStripSplitButtonEx();
            this.copyToolStripMenuItem1 = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.copyHexToolStripMenuItem1 = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.pasteToolStripSplitButton = new Be.HexEditor.Core.ToolStripSplitButtonEx();
            this.pasteToolStripMenuItem1 = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.pasteHexToolStripMenuItem1 = new Be.HexEditor.Core.ToolStripMenuItemEx();
            this.encodingToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.fileSizeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.bitToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.bodyPanel = new System.Windows.Forms.Panel();
            this.hexBox = new Be.Windows.Forms.HexBox();
            this.bitControl1 = new Be.HexEditor.BitControl();
            this.RecentFileHandler = new Be.HexEditor.RecentFileHandler(this.components);
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.bodyPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.toolStripSeparator2,
            this.recentFilesToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.FolderOpen_16;
            this.openToolStripMenuItem.Image16 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.FolderOpen_16;
            this.openToolStripMenuItem.Image24 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.FolderOpen_24;
            this.openToolStripMenuItem.Image32 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.FolderOpen_32;
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.open_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            resources.ApplyResources(this.toolStripSeparator, "toolStripSeparator");
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.save16;
            this.saveToolStripMenuItem.Image16 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.save16;
            this.saveToolStripMenuItem.Image24 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Save24;
            this.saveToolStripMenuItem.Image32 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Save32;
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.save_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // recentFilesToolStripMenuItem
            // 
            resources.ApplyResources(this.recentFilesToolStripMenuItem, "recentFilesToolStripMenuItem");
            this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            this.recentFilesToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.recentFiles_DropDownItemClicked);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exit_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator3,
            this.copyHexStringToolStripMenuItem,
            this.pasteHexToolStripMenuItem,
            this.toolStripSeparator4,
            this.findToolStripMenuItem,
            this.findNextToolStripMenuItem,
            this.goToToolStripMenuItem,
            this.toolStripSeparator5,
            this.selectAllToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Cut16;
            this.cutToolStripMenuItem.Image16 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Copy16;
            this.cutToolStripMenuItem.Image24 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Copy24;
            this.cutToolStripMenuItem.Image32 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Copy32;
            resources.ApplyResources(this.cutToolStripMenuItem, "cutToolStripMenuItem");
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cut_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Copy16;
            this.copyToolStripMenuItem.Image16 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Copy16;
            this.copyToolStripMenuItem.Image24 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Copy24;
            this.copyToolStripMenuItem.Image32 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Copy32;
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copy_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Paste16;
            this.pasteToolStripMenuItem.Image16 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Paste16;
            this.pasteToolStripMenuItem.Image24 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Paste24;
            this.pasteToolStripMenuItem.Image32 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Paste32;
            resources.ApplyResources(this.pasteToolStripMenuItem, "pasteToolStripMenuItem");
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.paste_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // copyHexStringToolStripMenuItem
            // 
            this.copyHexStringToolStripMenuItem.Name = "copyHexStringToolStripMenuItem";
            resources.ApplyResources(this.copyHexStringToolStripMenuItem, "copyHexStringToolStripMenuItem");
            this.copyHexStringToolStripMenuItem.Click += new System.EventHandler(this.copyHex_Click);
            // 
            // pasteHexToolStripMenuItem
            // 
            this.pasteHexToolStripMenuItem.Name = "pasteHexToolStripMenuItem";
            resources.ApplyResources(this.pasteHexToolStripMenuItem, "pasteHexToolStripMenuItem");
            this.pasteHexToolStripMenuItem.Click += new System.EventHandler(this.pasteHex_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Image = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Find16;
            this.findToolStripMenuItem.Image16 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Find16;
            this.findToolStripMenuItem.Image24 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Find24;
            this.findToolStripMenuItem.Image32 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Find32;
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            resources.ApplyResources(this.findToolStripMenuItem, "findToolStripMenuItem");
            this.findToolStripMenuItem.Click += new System.EventHandler(this.find_Click);
            // 
            // findNextToolStripMenuItem
            // 
            this.findNextToolStripMenuItem.Image = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.FindNext16;
            this.findNextToolStripMenuItem.Image16 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.FindNext16;
            this.findNextToolStripMenuItem.Image24 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.FindNext24;
            this.findNextToolStripMenuItem.Image32 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.FindNext32;
            this.findNextToolStripMenuItem.Name = "findNextToolStripMenuItem";
            resources.ApplyResources(this.findNextToolStripMenuItem, "findNextToolStripMenuItem");
            this.findNextToolStripMenuItem.Click += new System.EventHandler(this.findNext_Click);
            // 
            // goToToolStripMenuItem
            // 
            this.goToToolStripMenuItem.Name = "goToToolStripMenuItem";
            resources.ApplyResources(this.goToToolStripMenuItem, "goToToolStripMenuItem");
            this.goToToolStripMenuItem.Click += new System.EventHandler(this.goTo_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            resources.ApplyResources(this.selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.encodingToolStripMenuItem,
            this.bitsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            // 
            // encodingToolStripMenuItem
            // 
            this.encodingToolStripMenuItem.Name = "encodingToolStripMenuItem";
            resources.ApplyResources(this.encodingToolStripMenuItem, "encodingToolStripMenuItem");
            // 
            // bitsToolStripMenuItem
            // 
            this.bitsToolStripMenuItem.CheckOnClick = true;
            this.bitsToolStripMenuItem.Name = "bitsToolStripMenuItem";
            resources.ApplyResources(this.bitsToolStripMenuItem, "bitsToolStripMenuItem");
            this.bitsToolStripMenuItem.CheckedChanged += new System.EventHandler(this.bitsToolStripMenuItem_CheckedChanged);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.options_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.about_Click);
            // 
            // toolStrip
            // 
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator6,
            this.cutToolStripButton,
            this.copyToolStripSplitButton,
            this.pasteToolStripSplitButton,
            this.encodingToolStripComboBox});
            this.toolStrip.Name = "toolStrip";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = global::Anathena.GUI.CustomControls.HexEditor.images.openHS;
            this.openToolStripButton.Image16 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.FolderOpen_16;
            this.openToolStripButton.Image24 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.FolderOpen_24;
            this.openToolStripButton.Image32 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.FolderOpen_32;
            resources.ApplyResources(this.openToolStripButton, "openToolStripButton");
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Click += new System.EventHandler(this.open_Click);
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = global::Anathena.GUI.CustomControls.HexEditor.images.saveHS;
            this.saveToolStripButton.Image16 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.save16;
            this.saveToolStripButton.Image24 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Save24;
            this.saveToolStripButton.Image32 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Save32;
            resources.ApplyResources(this.saveToolStripButton, "saveToolStripButton");
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Click += new System.EventHandler(this.save_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolStripButton.Image = global::Anathena.GUI.CustomControls.HexEditor.images.CutHS;
            this.cutToolStripButton.Image16 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Cut16;
            this.cutToolStripButton.Image24 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Copy24;
            this.cutToolStripButton.Image32 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Copy32;
            resources.ApplyResources(this.cutToolStripButton, "cutToolStripButton");
            this.cutToolStripButton.Name = "cutToolStripButton";
            this.cutToolStripButton.Click += new System.EventHandler(this.cut_Click);
            // 
            // copyToolStripSplitButton
            // 
            this.copyToolStripSplitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolStripSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem1,
            this.copyHexToolStripMenuItem1});
            this.copyToolStripSplitButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.copyToolStripSplitButton.Image = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Copy16;
            this.copyToolStripSplitButton.Image16 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Copy16;
            this.copyToolStripSplitButton.Image24 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Copy24;
            this.copyToolStripSplitButton.Image32 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Copy32;
            resources.ApplyResources(this.copyToolStripSplitButton, "copyToolStripSplitButton");
            this.copyToolStripSplitButton.Name = "copyToolStripSplitButton";
            this.copyToolStripSplitButton.ButtonClick += new System.EventHandler(this.copy_Click);
            // 
            // copyToolStripMenuItem1
            // 
            this.copyToolStripMenuItem1.Image = global::Anathena.GUI.CustomControls.HexEditor.images.CopyHS;
            this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
            resources.ApplyResources(this.copyToolStripMenuItem1, "copyToolStripMenuItem1");
            this.copyToolStripMenuItem1.Click += new System.EventHandler(this.copy_Click);
            // 
            // copyHexToolStripMenuItem1
            // 
            this.copyHexToolStripMenuItem1.Image = global::Anathena.GUI.CustomControls.HexEditor.images.CopyHS;
            this.copyHexToolStripMenuItem1.Name = "copyHexToolStripMenuItem1";
            resources.ApplyResources(this.copyHexToolStripMenuItem1, "copyHexToolStripMenuItem1");
            this.copyHexToolStripMenuItem1.Click += new System.EventHandler(this.copyHex_Click);
            // 
            // pasteToolStripSplitButton
            // 
            this.pasteToolStripSplitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolStripSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pasteToolStripMenuItem1,
            this.pasteHexToolStripMenuItem1});
            this.pasteToolStripSplitButton.Image = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Paste16;
            this.pasteToolStripSplitButton.Image16 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Paste16;
            this.pasteToolStripSplitButton.Image24 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Paste24;
            this.pasteToolStripSplitButton.Image32 = global::Anathena.GUI.CustomControls.HexEditor.ScalingImages.Paste32;
            resources.ApplyResources(this.pasteToolStripSplitButton, "pasteToolStripSplitButton");
            this.pasteToolStripSplitButton.Name = "pasteToolStripSplitButton";
            this.pasteToolStripSplitButton.ButtonClick += new System.EventHandler(this.paste_Click);
            // 
            // pasteToolStripMenuItem1
            // 
            this.pasteToolStripMenuItem1.Name = "pasteToolStripMenuItem1";
            resources.ApplyResources(this.pasteToolStripMenuItem1, "pasteToolStripMenuItem1");
            this.pasteToolStripMenuItem1.Click += new System.EventHandler(this.paste_Click);
            // 
            // pasteHexToolStripMenuItem1
            // 
            this.pasteHexToolStripMenuItem1.Name = "pasteHexToolStripMenuItem1";
            resources.ApplyResources(this.pasteHexToolStripMenuItem1, "pasteHexToolStripMenuItem1");
            this.pasteHexToolStripMenuItem1.Click += new System.EventHandler(this.pasteHex_Click);
            // 
            // encodingToolStripComboBox
            // 
            this.encodingToolStripComboBox.BackColor = System.Drawing.SystemColors.Control;
            this.encodingToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.encodingToolStripComboBox.DropDownWidth = 180;
            resources.ApplyResources(this.encodingToolStripComboBox, "encodingToolStripComboBox");
            this.encodingToolStripComboBox.Name = "encodingToolStripComboBox";
            this.encodingToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.toolStripEncoding_SelectedIndexChanged);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.SystemColors.Control;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.fileSizeToolStripStatusLabel,
            this.bitToolStripStatusLabel});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip.SizingGrip = false;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Margin = new System.Windows.Forms.Padding(5, 3, 0, 2);
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            resources.ApplyResources(this.toolStripStatusLabel, "toolStripStatusLabel");
            // 
            // fileSizeToolStripStatusLabel
            // 
            this.fileSizeToolStripStatusLabel.Name = "fileSizeToolStripStatusLabel";
            this.fileSizeToolStripStatusLabel.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            resources.ApplyResources(this.fileSizeToolStripStatusLabel, "fileSizeToolStripStatusLabel");
            // 
            // bitToolStripStatusLabel
            // 
            this.bitToolStripStatusLabel.Name = "bitToolStripStatusLabel";
            resources.ApplyResources(this.bitToolStripStatusLabel, "bitToolStripStatusLabel");
            // 
            // bodyPanel
            // 
            resources.ApplyResources(this.bodyPanel, "bodyPanel");
            this.bodyPanel.Controls.Add(this.hexBox);
            this.bodyPanel.Controls.Add(this.bitControl1);
            this.bodyPanel.Name = "bodyPanel";
            // 
            // hexBox
            // 
            this.hexBox.AllowDrop = true;
            resources.ApplyResources(this.hexBox, "hexBox");
            this.hexBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            // 
            // 
            // 
            this.hexBox.BuiltInContextMenu.CopyMenuItemImage = global::Anathena.GUI.CustomControls.HexEditor.images.CopyHS;
            this.hexBox.BuiltInContextMenu.CopyMenuItemText = resources.GetString("hexBox.BuiltInContextMenu.CopyMenuItemText");
            this.hexBox.BuiltInContextMenu.CutMenuItemImage = global::Anathena.GUI.CustomControls.HexEditor.images.CutHS;
            this.hexBox.BuiltInContextMenu.CutMenuItemText = resources.GetString("hexBox.BuiltInContextMenu.CutMenuItemText");
            this.hexBox.BuiltInContextMenu.PasteMenuItemImage = global::Anathena.GUI.CustomControls.HexEditor.images.PasteHS;
            this.hexBox.BuiltInContextMenu.PasteMenuItemText = resources.GetString("hexBox.BuiltInContextMenu.PasteMenuItemText");
            this.hexBox.BuiltInContextMenu.SelectAllMenuItemText = resources.GetString("hexBox.BuiltInContextMenu.SelectAllMenuItemText");
            this.hexBox.ColumnInfoVisible = true;
            this.hexBox.HexCasing = Be.Windows.Forms.HexCasing.Lower;
            this.hexBox.LineInfoVisible = true;
            this.hexBox.Name = "hexBox";
            this.hexBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox.StringViewVisible = true;
            this.hexBox.UseFixedBytesPerLine = true;
            this.hexBox.VScrollBarVisible = true;
            this.hexBox.SelectionStartChanged += new System.EventHandler(this.hexBox_SelectionStartChanged);
            this.hexBox.SelectionLengthChanged += new System.EventHandler(this.hexBox_SelectionLengthChanged);
            this.hexBox.CurrentLineChanged += new System.EventHandler(this.Position_Changed);
            this.hexBox.CurrentPositionInLineChanged += new System.EventHandler(this.Position_Changed);
            this.hexBox.Copied += new System.EventHandler(this.hexBox_Copied);
            this.hexBox.CopiedHex += new System.EventHandler(this.hexBox_CopiedHex);
            this.hexBox.RequiredWidthChanged += new System.EventHandler(this.hexBox_RequiredWidthChanged);
            this.hexBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.hexBox_DragDrop);
            this.hexBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.hexBox_DragEnter);
            // 
            // bitControl1
            // 
            resources.ApplyResources(this.bitControl1, "bitControl1");
            this.bitControl1.Name = "bitControl1";
            this.bitControl1.BitChanged += new System.EventHandler(this.bitControl1_BitChanged);
            // 
            // RecentFileHandler
            // 
            this.RecentFileHandler.RecentFileToolStripItem = this.recentFilesToolStripMenuItem;
            // 
            // FormHexEditor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.bodyPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormHexEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormHexEditor_FormClosing);
            this.Load += new System.EventHandler(this.FormHexEditor_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.bodyPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Core.MenuStripEx menuStrip;
        private Core.ToolStripMenuItemEx fileToolStripMenuItem;
        private Core.ToolStripMenuItemEx openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private Core.ToolStripMenuItemEx saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private Core.ToolStripMenuItemEx exitToolStripMenuItem;
        private Core.ToolStripMenuItemEx editToolStripMenuItem;
        private Core.ToolStripMenuItemEx cutToolStripMenuItem;
        private Core.ToolStripMenuItemEx copyToolStripMenuItem;
        private Core.ToolStripMenuItemEx pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private Core.ToolStripMenuItemEx findToolStripMenuItem;
        private Core.ToolStripMenuItemEx toolsToolStripMenuItem;
        private Core.ToolStripMenuItemEx optionsToolStripMenuItem;
        private Core.ToolStripMenuItemEx helpToolStripMenuItem;
        private Core.ToolStripMenuItemEx aboutToolStripMenuItem;
        private Core.ToolStripEx toolStrip;
        private Core.ToolStripButtonEx openToolStripButton;
        private Core.ToolStripButtonEx saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private Core.ToolStripButtonEx cutToolStripButton;
        private Be.Windows.Forms.HexBox hexBox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private Core.ToolStripMenuItemEx findNextToolStripMenuItem;
        private Core.ToolStripMenuItemEx goToToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private Core.ToolStripMenuItemEx recentFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripStatusLabel fileSizeToolStripStatusLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private Core.ToolStripMenuItemEx copyHexStringToolStripMenuItem;
        private Core.ToolStripMenuItemEx pasteHexToolStripMenuItem;
        private Core.ToolStripSplitButtonEx copyToolStripSplitButton;
        private Core.ToolStripMenuItemEx copyToolStripMenuItem1;
        private Core.ToolStripMenuItemEx copyHexToolStripMenuItem1;
        private Core.ToolStripSplitButtonEx pasteToolStripSplitButton;
        private Core.ToolStripMenuItemEx pasteToolStripMenuItem1;
        private Core.ToolStripMenuItemEx pasteHexToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private Core.ToolStripMenuItemEx selectAllToolStripMenuItem;
        public RecentFileHandler RecentFileHandler;
		private System.Windows.Forms.ToolStripStatusLabel bitToolStripStatusLabel;
		private System.Windows.Forms.ToolStripComboBox encodingToolStripComboBox;
		private Core.ToolStripMenuItemEx viewToolStripMenuItem;
		private Core.ToolStripMenuItemEx encodingToolStripMenuItem;
		private Core.ToolStripMenuItemEx bitsToolStripMenuItem;
		private BitControl bitControl1;
		private System.Windows.Forms.Panel bodyPanel;
    }
}