namespace MouseTest
{
    partial class MainForm
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

            if (disposing)
            {
                if (mouse != null)
                    mouse.Dispose();
                mouse = null;
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
            this.createButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.behaviorLabel = new System.Windows.Forms.Label();
            this.dataBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.backgroundRadio = new System.Windows.Forms.RadioButton();
            this.exclusiveRadio = new System.Windows.Forms.RadioButton();
            this.nonexclusiveRadio = new System.Windows.Forms.RadioButton();
            this.foregroundRadio = new System.Windows.Forms.RadioButton();
            this.bufferedRadio = new System.Windows.Forms.RadioButton();
            this.immediateRadio = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.helpLabel = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(12, 363);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(95, 23);
            this.createButton.TabIndex = 1;
            this.createButton.Text = "Create Device";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(470, 363);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(75, 23);
            this.exitButton.TabIndex = 13;
            this.exitButton.TabStop = false;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // behaviorLabel
            // 
            this.behaviorLabel.Location = new System.Drawing.Point(6, 16);
            this.behaviorLabel.Name = "behaviorLabel";
            this.behaviorLabel.Size = new System.Drawing.Size(521, 205);
            this.behaviorLabel.TabIndex = 0;
            this.behaviorLabel.Text = "label1";
            // 
            // dataBox
            // 
            this.dataBox.Location = new System.Drawing.Point(48, 337);
            this.dataBox.Name = "dataBox";
            this.dataBox.ReadOnly = true;
            this.dataBox.Size = new System.Drawing.Size(497, 20);
            this.dataBox.TabIndex = 12;
            this.dataBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 340);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Data:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.behaviorLabel);
            this.groupBox3.Location = new System.Drawing.Point(12, 107);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(533, 224);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Expected Behavior";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.backgroundRadio);
            this.groupBox1.Controls.Add(this.exclusiveRadio);
            this.groupBox1.Controls.Add(this.nonexclusiveRadio);
            this.groupBox1.Controls.Add(this.foregroundRadio);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 89);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cooperative Level";
            // 
            // backgroundRadio
            // 
            this.backgroundRadio.AutoCheck = false;
            this.backgroundRadio.AutoSize = true;
            this.backgroundRadio.Location = new System.Drawing.Point(125, 42);
            this.backgroundRadio.Name = "backgroundRadio";
            this.backgroundRadio.Size = new System.Drawing.Size(83, 17);
            this.backgroundRadio.TabIndex = 3;
            this.backgroundRadio.Text = "Background";
            this.backgroundRadio.UseVisualStyleBackColor = true;
            this.backgroundRadio.Click += new System.EventHandler(this.backgroundRadio_Click);
            // 
            // exclusiveRadio
            // 
            this.exclusiveRadio.AutoCheck = false;
            this.exclusiveRadio.AutoSize = true;
            this.exclusiveRadio.Checked = true;
            this.exclusiveRadio.Location = new System.Drawing.Point(6, 19);
            this.exclusiveRadio.Name = "exclusiveRadio";
            this.exclusiveRadio.Size = new System.Drawing.Size(70, 17);
            this.exclusiveRadio.TabIndex = 0;
            this.exclusiveRadio.Text = "Exclusive";
            this.exclusiveRadio.UseVisualStyleBackColor = true;
            this.exclusiveRadio.Click += new System.EventHandler(this.exclusiveRadio_Click);
            // 
            // nonexclusiveRadio
            // 
            this.nonexclusiveRadio.AutoCheck = false;
            this.nonexclusiveRadio.AutoSize = true;
            this.nonexclusiveRadio.Location = new System.Drawing.Point(6, 42);
            this.nonexclusiveRadio.Name = "nonexclusiveRadio";
            this.nonexclusiveRadio.Size = new System.Drawing.Size(89, 17);
            this.nonexclusiveRadio.TabIndex = 1;
            this.nonexclusiveRadio.Text = "Nonexclusive";
            this.nonexclusiveRadio.UseVisualStyleBackColor = true;
            this.nonexclusiveRadio.Click += new System.EventHandler(this.nonexclusiveRadio_Click);
            // 
            // foregroundRadio
            // 
            this.foregroundRadio.AutoCheck = false;
            this.foregroundRadio.AutoSize = true;
            this.foregroundRadio.Checked = true;
            this.foregroundRadio.Location = new System.Drawing.Point(125, 19);
            this.foregroundRadio.Name = "foregroundRadio";
            this.foregroundRadio.Size = new System.Drawing.Size(79, 17);
            this.foregroundRadio.TabIndex = 2;
            this.foregroundRadio.Text = "Foreground";
            this.foregroundRadio.UseVisualStyleBackColor = true;
            this.foregroundRadio.Click += new System.EventHandler(this.foregroundRadio_Click);
            // 
            // bufferedRadio
            // 
            this.bufferedRadio.AutoSize = true;
            this.bufferedRadio.Location = new System.Drawing.Point(6, 42);
            this.bufferedRadio.Name = "bufferedRadio";
            this.bufferedRadio.Size = new System.Drawing.Size(65, 17);
            this.bufferedRadio.TabIndex = 1;
            this.bufferedRadio.Text = "Buffered";
            this.bufferedRadio.UseVisualStyleBackColor = true;
            this.bufferedRadio.CheckedChanged += new System.EventHandler(this.bufferedRadio_CheckedChanged);
            // 
            // immediateRadio
            // 
            this.immediateRadio.AutoSize = true;
            this.immediateRadio.Checked = true;
            this.immediateRadio.Location = new System.Drawing.Point(6, 19);
            this.immediateRadio.Name = "immediateRadio";
            this.immediateRadio.Size = new System.Drawing.Size(73, 17);
            this.immediateRadio.TabIndex = 0;
            this.immediateRadio.TabStop = true;
            this.immediateRadio.Text = "Immediate";
            this.immediateRadio.UseVisualStyleBackColor = true;
            this.immediateRadio.CheckedChanged += new System.EventHandler(this.immediateRadio_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bufferedRadio);
            this.groupBox2.Controls.Add(this.immediateRadio);
            this.groupBox2.Location = new System.Drawing.Point(235, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(130, 89);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Style";
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // helpLabel
            // 
            this.helpLabel.Location = new System.Drawing.Point(371, 21);
            this.helpLabel.Name = "helpLabel";
            this.helpLabel.Size = new System.Drawing.Size(168, 80);
            this.helpLabel.TabIndex = 15;
            this.helpLabel.Text = "label2";
            // 
            // MainForm
            // 
            this.AcceptButton = this.createButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 399);
            this.Controls.Add(this.helpLabel);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.dataBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "SlimDX - Mouse Sample";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Label behaviorLabel;
        private System.Windows.Forms.TextBox dataBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton backgroundRadio;
        private System.Windows.Forms.RadioButton exclusiveRadio;
        private System.Windows.Forms.RadioButton nonexclusiveRadio;
        private System.Windows.Forms.RadioButton foregroundRadio;
        private System.Windows.Forms.RadioButton bufferedRadio;
        private System.Windows.Forms.RadioButton immediateRadio;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label helpLabel;
    }
}

