namespace TokenIDE {
	partial class GroupStats {
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.indentBox = new System.Windows.Forms.CheckBox();
			//((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.treeView1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.indentBox);
			this.splitContainer1.Size = new System.Drawing.Size(839, 552);
			this.splitContainer1.SplitterDistance = 179;
			this.splitContainer1.TabIndex = 0;
			// 
			// treeView1
			// 
			this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(179, 552);
			this.treeView1.TabIndex = 0;
			// 
			// indentBox
			// 
			this.indentBox.AutoSize = true;
			this.indentBox.Checked = true;
			this.indentBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.indentBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.indentBox.Location = new System.Drawing.Point(0, 535);
			this.indentBox.Name = "indentBox";
			this.indentBox.Size = new System.Drawing.Size(656, 17);
			this.indentBox.TabIndex = 0;
			this.indentBox.Text = "Indent";
			this.indentBox.UseVisualStyleBackColor = true;
			this.indentBox.CheckedChanged += new System.EventHandler(this.indentBox_CheckedChanged);
			// 
			// GroupStats
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(839, 552);
			this.Controls.Add(this.splitContainer1);
			this.Name = "GroupStats";
			this.Text = "Group Stats";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			//((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.CheckBox indentBox;
	}
}