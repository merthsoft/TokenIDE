namespace Merthsoft.TokenIDE.Forms {
	partial class ColorPicker {
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
			this.paletteBox = new System.Windows.Forms.PictureBox();
			this.pixelBox = new System.Windows.Forms.PictureBox();
			this.numberLabel = new System.Windows.Forms.Label();
			this.insertButton = new System.Windows.Forms.Button();
			this.copyButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.paletteBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pixelBox)).BeginInit();
			this.SuspendLayout();
			// 
			// paletteBox
			// 
			this.paletteBox.Location = new System.Drawing.Point(12, 12);
			this.paletteBox.Name = "paletteBox";
			this.paletteBox.Size = new System.Drawing.Size(704, 176);
			this.paletteBox.TabIndex = 22;
			this.paletteBox.TabStop = false;
			this.paletteBox.Paint += new System.Windows.Forms.PaintEventHandler(this.paletteBox_Paint);
			this.paletteBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.paletteBox_MouseClick);
			this.paletteBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.paletteBox_MouseDoubleClick);
			this.paletteBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.paletteBox_MouseMove);
			// 
			// pixelBox
			// 
			this.pixelBox.Location = new System.Drawing.Point(641, 194);
			this.pixelBox.Name = "pixelBox";
			this.pixelBox.Size = new System.Drawing.Size(75, 75);
			this.pixelBox.TabIndex = 27;
			this.pixelBox.TabStop = false;
			this.pixelBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pixelBox_Paint);
			// 
			// numberLabel
			// 
			this.numberLabel.AutoSize = true;
			this.numberLabel.Location = new System.Drawing.Point(671, 272);
			this.numberLabel.Name = "numberLabel";
			this.numberLabel.Size = new System.Drawing.Size(45, 13);
			this.numberLabel.TabIndex = 28;
			this.numberLabel.Text = "0 (0x00)";
			// 
			// insertButton
			// 
			this.insertButton.Location = new System.Drawing.Point(644, 288);
			this.insertButton.Name = "insertButton";
			this.insertButton.Size = new System.Drawing.Size(75, 23);
			this.insertButton.TabIndex = 29;
			this.insertButton.Text = "Insert";
			this.insertButton.UseVisualStyleBackColor = true;
			this.insertButton.Click += new System.EventHandler(this.insertButton_Click);
			// 
			// copyButton
			// 
			this.copyButton.Location = new System.Drawing.Point(563, 288);
			this.copyButton.Name = "copyButton";
			this.copyButton.Size = new System.Drawing.Size(75, 23);
			this.copyButton.TabIndex = 30;
			this.copyButton.Text = "Copy";
			this.copyButton.UseVisualStyleBackColor = true;
			this.copyButton.Click += new System.EventHandler(this.button2_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(482, 288);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 31;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.button3_Click);
			// 
			// ColorPicker
			// 
			this.AcceptButton = this.insertButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(731, 323);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.copyButton);
			this.Controls.Add(this.insertButton);
			this.Controls.Add(this.numberLabel);
			this.Controls.Add(this.pixelBox);
			this.Controls.Add(this.paletteBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ColorPicker";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Color Picker";
			((System.ComponentModel.ISupportInitialize)(this.paletteBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pixelBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox paletteBox;
		private System.Windows.Forms.PictureBox pixelBox;
		private System.Windows.Forms.Label numberLabel;
		private System.Windows.Forms.Button insertButton;
		private System.Windows.Forms.Button copyButton;
		private System.Windows.Forms.Button cancelButton;
	}
}