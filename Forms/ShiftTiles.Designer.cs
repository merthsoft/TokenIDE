namespace Merthsoft.TokenIDE {
	partial class ShiftTiles {
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
			this.label1 = new System.Windows.Forms.Label();
			this.startBox = new System.Windows.Forms.NumericUpDown();
			this.endBox = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.amountBox = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label4 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.spriteBox = new System.Windows.Forms.PictureBox();
			this.label5 = new System.Windows.Forms.Label();
			this.zoomBox = new System.Windows.Forms.NumericUpDown();
			this.panel3 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.startBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.endBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.amountBox)).BeginInit();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spriteBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.zoomBox)).BeginInit();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(143, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Shift values starting at value:";
			// 
			// startBox
			// 
			this.startBox.Location = new System.Drawing.Point(6, 16);
			this.startBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.startBox.Name = "startBox";
			this.startBox.Size = new System.Drawing.Size(156, 20);
			this.startBox.TabIndex = 1;
			this.startBox.ValueChanged += new System.EventHandler(this.startBox_ValueChanged);
			// 
			// endBox
			// 
			this.endBox.Location = new System.Drawing.Point(168, 16);
			this.endBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.endBox.Name = "endBox";
			this.endBox.Size = new System.Drawing.Size(156, 20);
			this.endBox.TabIndex = 3;
			this.endBox.ValueChanged += new System.EventHandler(this.endBox_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(165, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "and ending at value:";
			// 
			// amountBox
			// 
			this.amountBox.Location = new System.Drawing.Point(330, 16);
			this.amountBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.amountBox.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
			this.amountBox.Name = "amountBox";
			this.amountBox.Size = new System.Drawing.Size(156, 20);
			this.amountBox.TabIndex = 5;
			this.amountBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.amountBox.ValueChanged += new System.EventHandler(this.amountBox_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(327, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(78, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "by this amount:";
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Dock = System.Windows.Forms.DockStyle.Right;
			this.okButton.Location = new System.Drawing.Point(424, 0);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 32);
			this.okButton.TabIndex = 6;
			this.okButton.Text = "&Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Dock = System.Windows.Forms.DockStyle.Right;
			this.cancelButton.Location = new System.Drawing.Point(349, 0);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 32);
			this.cancelButton.TabIndex = 7;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.startBox);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.amountBox);
			this.panel1.Controls.Add(this.endBox);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(499, 55);
			this.panel1.TabIndex = 8;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 39);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Preview:";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.label5);
			this.panel2.Controls.Add(this.zoomBox);
			this.panel2.Controls.Add(this.cancelButton);
			this.panel2.Controls.Add(this.okButton);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 444);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(499, 32);
			this.panel2.TabIndex = 9;
			// 
			// spriteBox
			// 
			this.spriteBox.Location = new System.Drawing.Point(3, 3);
			this.spriteBox.Name = "spriteBox";
			this.spriteBox.Size = new System.Drawing.Size(100, 50);
			this.spriteBox.TabIndex = 10;
			this.spriteBox.TabStop = false;
			this.spriteBox.Paint += new System.Windows.Forms.PaintEventHandler(this.spriteBox_Paint);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 10);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(37, 13);
			this.label5.TabIndex = 17;
			this.label5.Text = "Zoom:";
			// 
			// zoomBox
			// 
			this.zoomBox.Location = new System.Drawing.Point(40, 8);
			this.zoomBox.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
			this.zoomBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.zoomBox.Name = "zoomBox";
			this.zoomBox.Size = new System.Drawing.Size(40, 20);
			this.zoomBox.TabIndex = 18;
			this.zoomBox.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.zoomBox.ValueChanged += new System.EventHandler(this.pixelSizeBox_ValueChanged);
			// 
			// panel3
			// 
			this.panel3.AutoScroll = true;
			this.panel3.Controls.Add(this.spriteBox);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(0, 55);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(499, 389);
			this.panel3.TabIndex = 11;
			// 
			// ShiftTiles
			// 
			this.AcceptButton = this.cancelButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(499, 476);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "ShiftTiles";
			this.Text = "Shift Values";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShiftTiles_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.startBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.endBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.amountBox)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.spriteBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.zoomBox)).EndInit();
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown startBox;
		private System.Windows.Forms.NumericUpDown endBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown amountBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.PictureBox spriteBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown zoomBox;
		private System.Windows.Forms.Panel panel3;
	}
}