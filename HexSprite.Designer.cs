namespace TokenIDE {
	partial class HexSprite {
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
			this.ResizeFromHexButton = new System.Windows.Forms.Button();
			this.MaintainDim = new System.Windows.Forms.CheckBox();
			this.DrawGrid = new System.Windows.Forms.CheckBox();
			this.PixelSize = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.ActiveHex = new System.Windows.Forms.CheckBox();
			this.InsertButton = new System.Windows.Forms.Button();
			this.CloseButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SpriteHeight = new System.Windows.Forms.NumericUpDown();
			this.SpriteWidth = new System.Windows.Forms.NumericUpDown();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.spriteBox = new System.Windows.Forms.PictureBox();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.hexBox = new System.Windows.Forms.TextBox();
			this.binBox = new System.Windows.Forms.TextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			////((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PixelSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SpriteHeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SpriteWidth)).BeginInit();
			////((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spriteBox)).BeginInit();
			////((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.ResizeFromHexButton);
			this.splitContainer1.Panel1.Controls.Add(this.MaintainDim);
			this.splitContainer1.Panel1.Controls.Add(this.DrawGrid);
			this.splitContainer1.Panel1.Controls.Add(this.PixelSize);
			this.splitContainer1.Panel1.Controls.Add(this.label2);
			this.splitContainer1.Panel1.Controls.Add(this.ActiveHex);
			this.splitContainer1.Panel1.Controls.Add(this.InsertButton);
			this.splitContainer1.Panel1.Controls.Add(this.CloseButton);
			this.splitContainer1.Panel1.Controls.Add(this.label1);
			this.splitContainer1.Panel1.Controls.Add(this.SpriteHeight);
			this.splitContainer1.Panel1.Controls.Add(this.SpriteWidth);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.AutoScroll = true;
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(490, 393);
			this.splitContainer1.SplitterDistance = 87;
			this.splitContainer1.TabIndex = 0;
			// 
			// ResizeFromHexButton
			// 
			this.ResizeFromHexButton.Location = new System.Drawing.Point(188, 55);
			this.ResizeFromHexButton.Name = "ResizeFromHexButton";
			this.ResizeFromHexButton.Size = new System.Drawing.Size(75, 23);
			this.ResizeFromHexButton.TabIndex = 19;
			this.ResizeFromHexButton.Text = "Hex Resize";
			this.ResizeFromHexButton.UseVisualStyleBackColor = true;
			this.ResizeFromHexButton.Click += new System.EventHandler(this.ResizeFromHexButton_Click);
			// 
			// MaintainDim
			// 
			this.MaintainDim.AutoSize = true;
			this.MaintainDim.Location = new System.Drawing.Point(95, 15);
			this.MaintainDim.Name = "MaintainDim";
			this.MaintainDim.Size = new System.Drawing.Size(87, 17);
			this.MaintainDim.TabIndex = 18;
			this.MaintainDim.Text = "Maintain Dim";
			this.MaintainDim.UseVisualStyleBackColor = true;
			// 
			// DrawGrid
			// 
			this.DrawGrid.AutoSize = true;
			this.DrawGrid.Checked = true;
			this.DrawGrid.CheckState = System.Windows.Forms.CheckState.Checked;
			this.DrawGrid.Location = new System.Drawing.Point(95, 50);
			this.DrawGrid.Name = "DrawGrid";
			this.DrawGrid.Size = new System.Drawing.Size(73, 17);
			this.DrawGrid.TabIndex = 17;
			this.DrawGrid.Text = "Draw Grid";
			this.DrawGrid.UseVisualStyleBackColor = true;
			this.DrawGrid.CheckedChanged += new System.EventHandler(this.DrawGrid_CheckedChanged);
			// 
			// PixelSize
			// 
			this.PixelSize.Location = new System.Drawing.Point(49, 49);
			this.PixelSize.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
			this.PixelSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.PixelSize.Name = "PixelSize";
			this.PixelSize.Size = new System.Drawing.Size(40, 20);
			this.PixelSize.TabIndex = 16;
			this.PixelSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.PixelSize.ValueChanged += new System.EventHandler(this.PixelSize_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 13);
			this.label2.TabIndex = 15;
			this.label2.Text = "Pixel:";
			// 
			// ActiveHex
			// 
			this.ActiveHex.AutoSize = true;
			this.ActiveHex.Checked = true;
			this.ActiveHex.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ActiveHex.Location = new System.Drawing.Point(95, 32);
			this.ActiveHex.Name = "ActiveHex";
			this.ActiveHex.Size = new System.Drawing.Size(78, 17);
			this.ActiveHex.TabIndex = 14;
			this.ActiveHex.Text = "Active Hex";
			this.ActiveHex.UseVisualStyleBackColor = true;
			this.ActiveHex.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// InsertButton
			// 
			this.InsertButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.InsertButton.Location = new System.Drawing.Point(188, 9);
			this.InsertButton.Name = "InsertButton";
			this.InsertButton.Size = new System.Drawing.Size(75, 23);
			this.InsertButton.TabIndex = 13;
			this.InsertButton.Text = "Insert";
			this.InsertButton.UseVisualStyleBackColor = true;
			this.InsertButton.Click += new System.EventHandler(this.InsertButton_Click);
			// 
			// CloseButton
			// 
			this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CloseButton.Location = new System.Drawing.Point(188, 32);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(75, 23);
			this.CloseButton.TabIndex = 12;
			this.CloseButton.Text = "Close";
			this.CloseButton.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Width x Height";
			// 
			// SpriteHeight
			// 
			this.SpriteHeight.Location = new System.Drawing.Point(49, 26);
			this.SpriteHeight.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
			this.SpriteHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.SpriteHeight.Name = "SpriteHeight";
			this.SpriteHeight.Size = new System.Drawing.Size(40, 20);
			this.SpriteHeight.TabIndex = 8;
			this.SpriteHeight.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.SpriteHeight.ValueChanged += new System.EventHandler(this.Height_ValueChanged);
			// 
			// SpriteWidth
			// 
			this.SpriteWidth.Location = new System.Drawing.Point(3, 26);
			this.SpriteWidth.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
			this.SpriteWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.SpriteWidth.Name = "SpriteWidth";
			this.SpriteWidth.Size = new System.Drawing.Size(40, 20);
			this.SpriteWidth.TabIndex = 7;
			this.SpriteWidth.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.SpriteWidth.ValueChanged += new System.EventHandler(this.Width_ValueChanged);
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.AutoScroll = true;
			this.splitContainer2.Panel1.Controls.Add(this.spriteBox);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
			this.splitContainer2.Panel2.Controls.Add(this.statusStrip1);
			this.splitContainer2.Size = new System.Drawing.Size(490, 302);
			this.splitContainer2.SplitterDistance = 326;
			this.splitContainer2.TabIndex = 1;
			// 
			// spriteBox
			// 
			this.spriteBox.BackColor = System.Drawing.Color.White;
			this.spriteBox.Location = new System.Drawing.Point(0, 3);
			this.spriteBox.Name = "spriteBox";
			this.spriteBox.Size = new System.Drawing.Size(81, 81);
			this.spriteBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.spriteBox.TabIndex = 0;
			this.spriteBox.TabStop = false;
			this.spriteBox.Click += new System.EventHandler(this.spriteBox_Click);
			this.spriteBox.Paint += new System.Windows.Forms.PaintEventHandler(this.spriteBox_Paint);
			this.spriteBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.spriteBox_MouseDown);
			this.spriteBox.MouseLeave += new System.EventHandler(this.spriteBox_MouseLeave);
			this.spriteBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.spriteBox_MouseMove);
			this.spriteBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.spriteBox_MouseUp);
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.hexBox);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.binBox);
			this.splitContainer3.Size = new System.Drawing.Size(160, 280);
			this.splitContainer3.SplitterDistance = 135;
			this.splitContainer3.TabIndex = 15;
			// 
			// hexBox
			// 
			this.hexBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.hexBox.Location = new System.Drawing.Point(0, 0);
			this.hexBox.Multiline = true;
			this.hexBox.Name = "hexBox";
			this.hexBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.hexBox.Size = new System.Drawing.Size(160, 135);
			this.hexBox.TabIndex = 14;
			this.hexBox.TextChanged += new System.EventHandler(this.hexBox_TextChanged);
			// 
			// binBox
			// 
			this.binBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.binBox.Location = new System.Drawing.Point(0, 0);
			this.binBox.Multiline = true;
			this.binBox.Name = "binBox";
			this.binBox.ReadOnly = true;
			this.binBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.binBox.Size = new System.Drawing.Size(160, 141);
			this.binBox.TabIndex = 15;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 280);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(160, 22);
			this.statusStrip1.TabIndex = 13;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
			this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
			// 
			// HexSprite
			// 
			this.AcceptButton = this.InsertButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.CloseButton;
			this.ClientSize = new System.Drawing.Size(490, 393);
			this.Controls.Add(this.splitContainer1);
			this.Name = "HexSprite";
			this.Text = "HexSprite";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel1.PerformLayout();
			this.splitContainer1.Panel2.ResumeLayout(false);
			////((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PixelSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SpriteHeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SpriteWidth)).EndInit();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.Panel2.PerformLayout();
			////((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spriteBox)).EndInit();
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel1.PerformLayout();
			this.splitContainer3.Panel2.ResumeLayout(false);
			this.splitContainer3.Panel2.PerformLayout();
			////((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Button InsertButton;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown SpriteHeight;
		private System.Windows.Forms.NumericUpDown SpriteWidth;
		private System.Windows.Forms.PictureBox spriteBox;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.CheckBox ActiveHex;
		private System.Windows.Forms.NumericUpDown PixelSize;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox DrawGrid;
		private System.Windows.Forms.TextBox hexBox;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.CheckBox MaintainDim;
		private System.Windows.Forms.Button ResizeFromHexButton;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.TextBox binBox;

	}
}