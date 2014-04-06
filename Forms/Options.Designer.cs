namespace Merthsoft.TokenIDE {
	partial class Options {
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
			this.components = new System.ComponentModel.Container();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.defaultTokenFileLabel = new System.Windows.Forms.Label();
			this.fontLabel = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.prettyPrintBox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// linkLabel1
			// 
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new System.Drawing.Point(12, 9);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(94, 13);
			this.linkLabel1.TabIndex = 2;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Default Token file:";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// linkLabel2
			// 
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.Location = new System.Drawing.Point(12, 35);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(31, 13);
			this.linkLabel2.TabIndex = 3;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "Font:";
			this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			// 
			// label1
			// 
			this.defaultTokenFileLabel.AutoSize = true;
			this.defaultTokenFileLabel.Location = new System.Drawing.Point(12, 22);
			this.defaultTokenFileLabel.Name = "label1";
			this.defaultTokenFileLabel.Size = new System.Drawing.Size(35, 13);
			this.defaultTokenFileLabel.TabIndex = 4;
			this.defaultTokenFileLabel.Text = "label1";
			// 
			// label2
			// 
			this.fontLabel.AutoSize = true;
			this.fontLabel.Location = new System.Drawing.Point(49, 35);
			this.fontLabel.Name = "label2";
			this.fontLabel.Size = new System.Drawing.Size(35, 13);
			this.fontLabel.TabIndex = 5;
			this.fontLabel.Text = "label2";
			// 
			// panel1
			// 
			this.panel1.Location = new System.Drawing.Point(12, 51);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(345, 199);
			this.panel1.TabIndex = 6;
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.Location = new System.Drawing.Point(282, 254);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 7;
			this.button1.Text = "Save";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(201, 254);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 8;
			this.button2.Text = "Cancel";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// prettyPrintBox
			// 
			this.prettyPrintBox.AutoSize = true;
			this.prettyPrintBox.Location = new System.Drawing.Point(12, 258);
			this.prettyPrintBox.Name = "prettyPrintBox";
			this.prettyPrintBox.Size = new System.Drawing.Size(179, 17);
			this.prettyPrintBox.TabIndex = 9;
			this.prettyPrintBox.Text = "Auto-replace tokens while typing";
			this.prettyPrintBox.UseVisualStyleBackColor = true;
			// 
			// Options
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(369, 281);
			this.Controls.Add(this.prettyPrintBox);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.fontLabel);
			this.Controls.Add(this.defaultTokenFileLabel);
			this.Controls.Add(this.linkLabel2);
			this.Controls.Add(this.linkLabel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "Options";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Options";
			this.Load += new System.EventHandler(this.Options_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.Label defaultTokenFileLabel;
		private System.Windows.Forms.Label fontLabel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.CheckBox prettyPrintBox;
	}
}