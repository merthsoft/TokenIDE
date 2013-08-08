namespace Merthsoft.TokenIDE {
	partial class CropImageDialog {
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.DoneButton = new System.Windows.Forms.Button();
			this.ToleranceUpDown = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ToleranceUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(566, 384);
			this.panel1.TabIndex = 1;
			// 
			// panel3
			// 
			this.panel3.AutoScroll = true;
			this.panel3.Controls.Add(this.pictureBox1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 28);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(566, 356);
			this.panel3.TabIndex = 1;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(12, 6);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(100, 50);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 16;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
			this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
			this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.ToleranceUpDown);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.DoneButton);
			this.panel2.Controls.Add(this.numericUpDown1);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(566, 28);
			this.panel2.TabIndex = 0;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(127, 5);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(53, 20);
			this.numericUpDown1.TabIndex = 17;
			this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(84, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(37, 13);
			this.label1.TabIndex = 16;
			this.label1.Text = "Zoom:";
			// 
			// DoneButton
			// 
			this.DoneButton.Location = new System.Drawing.Point(3, 3);
			this.DoneButton.Name = "DoneButton";
			this.DoneButton.Size = new System.Drawing.Size(75, 23);
			this.DoneButton.TabIndex = 18;
			this.DoneButton.Text = "Done";
			this.DoneButton.UseVisualStyleBackColor = true;
			this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
			// 
			// ToleranceUpDown
			// 
			this.ToleranceUpDown.Location = new System.Drawing.Point(249, 4);
			this.ToleranceUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.ToleranceUpDown.Name = "ToleranceUpDown";
			this.ToleranceUpDown.Size = new System.Drawing.Size(53, 20);
			this.ToleranceUpDown.TabIndex = 20;
			this.ToleranceUpDown.Value = new decimal(new int[] {
            127,
            0,
            0,
            0});
			this.ToleranceUpDown.ValueChanged += new System.EventHandler(this.ToleranceUpDown_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(186, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 19;
			this.label2.Text = "Tolerance:";
			// 
			// CropImageDialog
			// 
			this.AcceptButton = this.DoneButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(566, 384);
			this.Controls.Add(this.panel1);
			this.Name = "CropImageDialog";
			this.Text = "CropImageDialog";
			this.Load += new System.EventHandler(this.CropImageDialog_Load);
			this.panel1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ToleranceUpDown)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button DoneButton;
		private System.Windows.Forms.NumericUpDown ToleranceUpDown;
		private System.Windows.Forms.Label label2;
	}
}