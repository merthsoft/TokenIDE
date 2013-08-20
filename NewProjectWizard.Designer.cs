namespace Merthsoft.TokenIDE {
	partial class NewProjectWizard {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.projectNameLabel = new System.Windows.Forms.Label();
			this.nameBox = new System.Windows.Forms.TextBox();
			this.baseBox = new System.Windows.Forms.TextBox();
			this.baseDirectoryLabel = new System.Windows.Forms.LinkLabel();
			this.outBox = new System.Windows.Forms.TextBox();
			this.outputDirectoryLabel = new System.Windows.Forms.LinkLabel();
			this.acceptButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.fromProjectButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// projectNameLabel
			// 
			this.projectNameLabel.AutoSize = true;
			this.projectNameLabel.Location = new System.Drawing.Point(12, 9);
			this.projectNameLabel.Name = "projectNameLabel";
			this.projectNameLabel.Size = new System.Drawing.Size(74, 13);
			this.projectNameLabel.TabIndex = 0;
			this.projectNameLabel.Text = "Project Name:";
			// 
			// nameBox
			// 
			this.nameBox.Location = new System.Drawing.Point(105, 6);
			this.nameBox.Name = "nameBox";
			this.nameBox.Size = new System.Drawing.Size(468, 20);
			this.nameBox.TabIndex = 1;
			this.nameBox.TextChanged += new System.EventHandler(this.nameBox_TextChanged);
			// 
			// baseBox
			// 
			this.baseBox.Location = new System.Drawing.Point(105, 32);
			this.baseBox.Name = "baseBox";
			this.baseBox.Size = new System.Drawing.Size(468, 20);
			this.baseBox.TabIndex = 3;
			this.baseBox.TextChanged += new System.EventHandler(this.baseBox_TextChanged);
			// 
			// baseDirectoryLabel
			// 
			this.baseDirectoryLabel.AutoSize = true;
			this.baseDirectoryLabel.Location = new System.Drawing.Point(12, 35);
			this.baseDirectoryLabel.Name = "baseDirectoryLabel";
			this.baseDirectoryLabel.Size = new System.Drawing.Size(79, 13);
			this.baseDirectoryLabel.TabIndex = 2;
			this.baseDirectoryLabel.TabStop = true;
			this.baseDirectoryLabel.Text = "Base Directory:";
			this.baseDirectoryLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.baseDirectoryLabel_LinkClicked);
			// 
			// outBox
			// 
			this.outBox.Location = new System.Drawing.Point(105, 58);
			this.outBox.Name = "outBox";
			this.outBox.Size = new System.Drawing.Size(468, 20);
			this.outBox.TabIndex = 5;
			this.outBox.TextChanged += new System.EventHandler(this.outBox_TextChanged);
			// 
			// outputDirectoryLabel
			// 
			this.outputDirectoryLabel.AutoSize = true;
			this.outputDirectoryLabel.Location = new System.Drawing.Point(12, 61);
			this.outputDirectoryLabel.Name = "outputDirectoryLabel";
			this.outputDirectoryLabel.Size = new System.Drawing.Size(87, 13);
			this.outputDirectoryLabel.TabIndex = 4;
			this.outputDirectoryLabel.TabStop = true;
			this.outputDirectoryLabel.Text = "Output Directory:";
			this.outputDirectoryLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.outputDirectoryLabel_LinkClicked);
			// 
			// acceptButton
			// 
			this.acceptButton.Location = new System.Drawing.Point(498, 84);
			this.acceptButton.Name = "acceptButton";
			this.acceptButton.Size = new System.Drawing.Size(75, 23);
			this.acceptButton.TabIndex = 8;
			this.acceptButton.Text = "OK";
			this.acceptButton.UseVisualStyleBackColor = true;
			this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(417, 84);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 9;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// fromProjectButton
			// 
			this.fromProjectButton.Location = new System.Drawing.Point(105, 84);
			this.fromProjectButton.Name = "fromProjectButton";
			this.fromProjectButton.Size = new System.Drawing.Size(108, 23);
			this.fromProjectButton.TabIndex = 10;
			this.fromProjectButton.Text = "From Project Name";
			this.fromProjectButton.UseVisualStyleBackColor = true;
			this.fromProjectButton.Click += new System.EventHandler(this.fromProjectButton_Click);
			// 
			// NewProjectWizard
			// 
			this.AcceptButton = this.acceptButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(585, 113);
			this.Controls.Add(this.fromProjectButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.acceptButton);
			this.Controls.Add(this.outBox);
			this.Controls.Add(this.outputDirectoryLabel);
			this.Controls.Add(this.baseBox);
			this.Controls.Add(this.baseDirectoryLabel);
			this.Controls.Add(this.nameBox);
			this.Controls.Add(this.projectNameLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "NewProjectWizard";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "New Project Wizard";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label projectNameLabel;
		private System.Windows.Forms.TextBox nameBox;
		private System.Windows.Forms.TextBox baseBox;
		private System.Windows.Forms.LinkLabel baseDirectoryLabel;
		private System.Windows.Forms.TextBox outBox;
		private System.Windows.Forms.LinkLabel outputDirectoryLabel;
		private System.Windows.Forms.Button acceptButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button fromProjectButton;
	}
}