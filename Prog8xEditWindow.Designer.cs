namespace TokenIDE {
	partial class Prog8xEditWindow {
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.ProgramTextBox = new FastColoredTextBoxNS.FastColoredTextBox();
			this.readOnlyPanel = new System.Windows.Forms.Panel();
			this.selectionLabel = new System.Windows.Forms.Label();
			this.bytesLabels = new System.Windows.Forms.Label();
			this.IndentCheckBox = new System.Windows.Forms.CheckBox();
			this.archivedCheckBox = new System.Windows.Forms.CheckBox();
			this.lockedBox = new System.Windows.Forms.CheckBox();
			this.readOnlyCheckBox = new System.Windows.Forms.CheckBox();
			this.liveUpdateCheckBox = new System.Windows.Forms.CheckBox();
			this.readOnlyPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// ProgramTextBox
			// 
			this.ProgramTextBox.AllowDrop = true;
			this.ProgramTextBox.AutoIndent = false;
			this.ProgramTextBox.AutoScrollMinSize = new System.Drawing.Size(27, 15);
			this.ProgramTextBox.BackBrush = null;
			this.ProgramTextBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ProgramTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.ProgramTextBox.CursorOn = false;
			this.ProgramTextBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
			this.ProgramTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ProgramTextBox.Font = new System.Drawing.Font("Consolas", 10F);
			this.ProgramTextBox.Location = new System.Drawing.Point(0, 0);
			this.ProgramTextBox.Name = "ProgramTextBox";
			this.ProgramTextBox.Paddings = new System.Windows.Forms.Padding(0);
			this.ProgramTextBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.ProgramTextBox.Size = new System.Drawing.Size(614, 430);
			this.ProgramTextBox.TabIndex = 0;
			this.ProgramTextBox.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.ProgramText_TextChanged);
			this.ProgramTextBox.SelectionChangedDelayed += new System.EventHandler(this.ProgramTextBox_SelectionChangedDelayed);
			this.ProgramTextBox.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ProgramTextBox_Scroll);
			this.ProgramTextBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.ProgramTextBox_DragEnter);
			// 
			// readOnlyPanel
			// 
			this.readOnlyPanel.Controls.Add(this.selectionLabel);
			this.readOnlyPanel.Controls.Add(this.bytesLabels);
			this.readOnlyPanel.Controls.Add(this.IndentCheckBox);
			this.readOnlyPanel.Controls.Add(this.archivedCheckBox);
			this.readOnlyPanel.Controls.Add(this.lockedBox);
			this.readOnlyPanel.Controls.Add(this.readOnlyCheckBox);
			this.readOnlyPanel.Controls.Add(this.liveUpdateCheckBox);
			this.readOnlyPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.readOnlyPanel.Location = new System.Drawing.Point(0, 430);
			this.readOnlyPanel.Name = "readOnlyPanel";
			this.readOnlyPanel.Size = new System.Drawing.Size(614, 21);
			this.readOnlyPanel.TabIndex = 1;
			// 
			// selectionLabel
			// 
			this.selectionLabel.AutoSize = true;
			this.selectionLabel.Location = new System.Drawing.Point(381, 4);
			this.selectionLabel.Name = "selectionLabel";
			this.selectionLabel.Size = new System.Drawing.Size(0, 13);
			this.selectionLabel.TabIndex = 4;
			this.selectionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// bytesLabels
			// 
			this.bytesLabels.AutoSize = true;
			this.bytesLabels.Location = new System.Drawing.Point(381, 4);
			this.bytesLabels.Name = "bytesLabels";
			this.bytesLabels.Size = new System.Drawing.Size(45, 13);
			this.bytesLabels.TabIndex = 1;
			this.bytesLabels.Text = "Bytes: 0";
			this.bytesLabels.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// IndentCheckBox
			// 
			this.IndentCheckBox.AutoSize = true;
			this.IndentCheckBox.Location = new System.Drawing.Point(229, 3);
			this.IndentCheckBox.Name = "IndentCheckBox";
			this.IndentCheckBox.Size = new System.Drawing.Size(56, 17);
			this.IndentCheckBox.TabIndex = 3;
			this.IndentCheckBox.Text = "Indent";
			this.IndentCheckBox.UseVisualStyleBackColor = true;
			this.IndentCheckBox.Visible = false;
			// 
			// archivedCheckBox
			// 
			this.archivedCheckBox.AutoSize = true;
			this.archivedCheckBox.Location = new System.Drawing.Point(86, 3);
			this.archivedCheckBox.Name = "archivedCheckBox";
			this.archivedCheckBox.Size = new System.Drawing.Size(68, 17);
			this.archivedCheckBox.TabIndex = 2;
			this.archivedCheckBox.Text = "Archived";
			this.archivedCheckBox.UseVisualStyleBackColor = true;
			this.archivedCheckBox.CheckedChanged += new System.EventHandler(this.archivedCheckBox_CheckedChanged);
			// 
			// lockedBox
			// 
			this.lockedBox.AutoSize = true;
			this.lockedBox.Location = new System.Drawing.Point(160, 3);
			this.lockedBox.Name = "lockedBox";
			this.lockedBox.Size = new System.Drawing.Size(62, 17);
			this.lockedBox.TabIndex = 1;
			this.lockedBox.Text = "Locked";
			this.lockedBox.UseVisualStyleBackColor = true;
			this.lockedBox.CheckedChanged += new System.EventHandler(this.lockedBox_CheckedChanged);
			// 
			// readOnlyCheckBox
			// 
			this.readOnlyCheckBox.AutoSize = true;
			this.readOnlyCheckBox.Location = new System.Drawing.Point(4, 3);
			this.readOnlyCheckBox.Name = "readOnlyCheckBox";
			this.readOnlyCheckBox.Size = new System.Drawing.Size(76, 17);
			this.readOnlyCheckBox.TabIndex = 0;
			this.readOnlyCheckBox.Text = "Read Only";
			this.readOnlyCheckBox.UseVisualStyleBackColor = true;
			this.readOnlyCheckBox.CheckedChanged += new System.EventHandler(this.readOnlyCheckBox_CheckedChanged);
			// 
			// liveUpdateCheckBox
			// 
			this.liveUpdateCheckBox.AutoSize = true;
			this.liveUpdateCheckBox.Checked = true;
			this.liveUpdateCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.liveUpdateCheckBox.Location = new System.Drawing.Point(291, 3);
			this.liveUpdateCheckBox.Name = "liveUpdateCheckBox";
			this.liveUpdateCheckBox.Size = new System.Drawing.Size(84, 17);
			this.liveUpdateCheckBox.TabIndex = 0;
			this.liveUpdateCheckBox.Text = "Live Update";
			this.liveUpdateCheckBox.UseVisualStyleBackColor = true;
			this.liveUpdateCheckBox.CheckedChanged += new System.EventHandler(this.liveUpdateCheckBox_CheckedChanged);
			// 
			// Prog8xEditWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ProgramTextBox);
			this.Controls.Add(this.readOnlyPanel);
			this.Name = "Prog8xEditWindow";
			this.Size = new System.Drawing.Size(614, 451);
			this.MouseHover += new System.EventHandler(this.Prog8xEditWindow_MouseHover);
			this.readOnlyPanel.ResumeLayout(false);
			this.readOnlyPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		//public System.Windows.Forms.RichTextBox ProgramTextBox;
		public FastColoredTextBoxNS.FastColoredTextBox ProgramTextBox;
		private System.Windows.Forms.CheckBox readOnlyCheckBox;
		private System.Windows.Forms.CheckBox archivedCheckBox;
		private System.Windows.Forms.CheckBox lockedBox;
		//private System.Windows.Forms.RichTextBox TokensBox;
		private System.Windows.Forms.CheckBox liveUpdateCheckBox;
		private System.Windows.Forms.Label bytesLabels;
		private System.Windows.Forms.CheckBox IndentCheckBox;
		private System.Windows.Forms.Label selectionLabel;
		public System.Windows.Forms.Panel readOnlyPanel;

	}
}
