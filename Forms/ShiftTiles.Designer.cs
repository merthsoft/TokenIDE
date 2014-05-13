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
			((System.ComponentModel.ISupportInitialize)(this.startBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.endBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.amountBox)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(117, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Shift tiles starting at tile:";
			// 
			// startBox
			// 
			this.startBox.Location = new System.Drawing.Point(15, 25);
			this.startBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.startBox.Name = "startBox";
			this.startBox.Size = new System.Drawing.Size(156, 20);
			this.startBox.TabIndex = 1;
			// 
			// endBox
			// 
			this.endBox.Location = new System.Drawing.Point(15, 64);
			this.endBox.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.endBox.Name = "endBox";
			this.endBox.Size = new System.Drawing.Size(156, 20);
			this.endBox.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(91, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "and ending at tile:";
			// 
			// amountBox
			// 
			this.amountBox.Location = new System.Drawing.Point(15, 103);
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
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 87);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(78, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "by this amount:";
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(96, 129);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 6;
			this.okButton.Text = "&Ok";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(15, 129);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 7;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// ShiftTiles
			// 
			this.AcceptButton = this.cancelButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(179, 163);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.amountBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.endBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.startBox);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ShiftTiles";
			this.Text = "Shift Tiles";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShiftTiles_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.startBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.endBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.amountBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

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
	}
}