namespace TokenIDE {
	partial class ImageEditor {
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBox1 = new System.Windows.Forms.ToolStripMenuItem();
			this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.picNumberBox = new System.Windows.Forms.ToolStripComboBox();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.insertAsHexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.oneStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.brokenUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.x16ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.customToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.HexWidthBox = new System.Windows.Forms.ToolStripTextBox();
			this.HexHeightBox = new System.Windows.Forms.ToolStripTextBox();
			this.DoCustomHex = new System.Windows.Forms.ToolStripMenuItem();
			this.HexOrBinBox = new System.Windows.Forms.ToolStripComboBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.toolBox = new System.Windows.Forms.ListView();
			this.panel2 = new System.Windows.Forms.Panel();
			this.canvas = new System.Windows.Forms.PictureBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.mouseCoordLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip1.SuspendLayout();
			////((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.insertAsHexToolStripMenuItem,
            this.HexOrBinBox});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(642, 27);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.loadToolStripMenuItem,
            this.saveAsToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 23);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// toolStripTextBox1
			// 
			this.toolStripTextBox1.Name = "toolStripTextBox1";
			this.toolStripTextBox1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.toolStripTextBox1.Size = new System.Drawing.Size(152, 22);
			this.toolStripTextBox1.Text = "New";
			this.toolStripTextBox1.Click += new System.EventHandler(this.toolStripTextBox1_Click);
			// 
			// loadToolStripMenuItem
			// 
			this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
			this.loadToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.loadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.loadToolStripMenuItem.Text = "Open";
			this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.picNumberBox,
            this.saveToolStripMenuItem});
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveAsToolStripMenuItem.Text = "Save As";
			// 
			// picNumberBox
			// 
			this.picNumberBox.Name = "picNumberBox";
			this.picNumberBox.Size = new System.Drawing.Size(152, 23);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 23);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Enabled = false;
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.undoToolStripMenuItem.Text = "Undo";
			this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
			// 
			// redoToolStripMenuItem
			// 
			this.redoToolStripMenuItem.Enabled = false;
			this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
			this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.redoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.redoToolStripMenuItem.Text = "Redo";
			this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
			// 
			// insertAsHexToolStripMenuItem
			// 
			this.insertAsHexToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oneStringToolStripMenuItem,
            this.brokenUpToolStripMenuItem});
			this.insertAsHexToolStripMenuItem.Name = "insertAsHexToolStripMenuItem";
			this.insertAsHexToolStripMenuItem.Size = new System.Drawing.Size(48, 23);
			this.insertAsHexToolStripMenuItem.Text = "Insert";
			// 
			// oneStringToolStripMenuItem
			// 
			this.oneStringToolStripMenuItem.Name = "oneStringToolStripMenuItem";
			this.oneStringToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.oneStringToolStripMenuItem.Text = "One String";
			this.oneStringToolStripMenuItem.Click += new System.EventHandler(this.oneStringToolStripMenuItem_Click);
			// 
			// brokenUpToolStripMenuItem
			// 
			this.brokenUpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x8ToolStripMenuItem,
            this.x16ToolStripMenuItem,
            this.customToolStripMenuItem});
			this.brokenUpToolStripMenuItem.Name = "brokenUpToolStripMenuItem";
			this.brokenUpToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.brokenUpToolStripMenuItem.Text = "Broken Up";
			// 
			// x8ToolStripMenuItem
			// 
			this.x8ToolStripMenuItem.Name = "x8ToolStripMenuItem";
			this.x8ToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.x8ToolStripMenuItem.Text = "8x8";
			this.x8ToolStripMenuItem.Click += new System.EventHandler(this.x8ToolStripMenuItem_Click);
			// 
			// x16ToolStripMenuItem
			// 
			this.x16ToolStripMenuItem.Name = "x16ToolStripMenuItem";
			this.x16ToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.x16ToolStripMenuItem.Text = "16x16";
			this.x16ToolStripMenuItem.Click += new System.EventHandler(this.x16ToolStripMenuItem_Click);
			// 
			// customToolStripMenuItem
			// 
			this.customToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HexWidthBox,
            this.HexHeightBox,
            this.DoCustomHex});
			this.customToolStripMenuItem.Name = "customToolStripMenuItem";
			this.customToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
			this.customToolStripMenuItem.Text = "Custom";
			// 
			// HexWidthBox
			// 
			this.HexWidthBox.Name = "HexWidthBox";
			this.HexWidthBox.Size = new System.Drawing.Size(100, 23);
			this.HexWidthBox.Text = "Width";
			// 
			// HexHeightBox
			// 
			this.HexHeightBox.Name = "HexHeightBox";
			this.HexHeightBox.Size = new System.Drawing.Size(100, 23);
			this.HexHeightBox.Text = "Height";
			// 
			// DoCustomHex
			// 
			this.DoCustomHex.Name = "DoCustomHex";
			this.DoCustomHex.Size = new System.Drawing.Size(160, 22);
			this.DoCustomHex.Text = "Go";
			this.DoCustomHex.Click += new System.EventHandler(this.DoCustomStripMenuItem_Click);
			// 
			// HexOrBinBox
			// 
			this.HexOrBinBox.Items.AddRange(new object[] {
            "Hex",
            "Binary"});
			this.HexOrBinBox.Name = "HexOrBinBox";
			this.HexOrBinBox.Size = new System.Drawing.Size(121, 23);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 27);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.toolBox);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.panel2);
			this.splitContainer1.Panel2.Controls.Add(this.panel1);
			this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
			this.splitContainer1.Size = new System.Drawing.Size(642, 350);
			this.splitContainer1.SplitterDistance = 131;
			this.splitContainer1.TabIndex = 2;
			// 
			// toolBox
			// 
			this.toolBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolBox.HideSelection = false;
			this.toolBox.Location = new System.Drawing.Point(0, 0);
			this.toolBox.Name = "toolBox";
			this.toolBox.Size = new System.Drawing.Size(131, 350);
			this.toolBox.TabIndex = 0;
			this.toolBox.UseCompatibleStateImageBehavior = false;
			this.toolBox.View = System.Windows.Forms.View.List;
			this.toolBox.SelectedIndexChanged += new System.EventHandler(this.toolBox_SelectedIndexChanged);
			// 
			// panel2
			// 
			this.panel2.AutoScroll = true;
			this.panel2.Controls.Add(this.canvas);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 34);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(507, 294);
			this.panel2.TabIndex = 17;
			// 
			// canvas
			// 
			this.canvas.BackColor = System.Drawing.Color.White;
			this.canvas.Location = new System.Drawing.Point(3, 6);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(96, 64);
			this.canvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.canvas.TabIndex = 21;
			this.canvas.TabStop = false;
			this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
			this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
			this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
			this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseUp);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.numericUpDown2);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.numericUpDown1);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(507, 34);
			this.panel1.TabIndex = 16;
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Location = new System.Drawing.Point(171, 7);
			this.numericUpDown2.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(53, 20);
			this.numericUpDown2.TabIndex = 24;
			this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(105, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 13);
			this.label2.TabIndex = 23;
			this.label2.Text = "Pen Width:";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(46, 7);
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
			this.numericUpDown1.TabIndex = 22;
			this.numericUpDown1.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(37, 13);
			this.label1.TabIndex = 21;
			this.label1.Text = "Zoom:";
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mouseCoordLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 328);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(507, 22);
			this.statusStrip1.TabIndex = 15;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// mouseCoordLabel
			// 
			this.mouseCoordLabel.Name = "mouseCoordLabel";
			this.mouseCoordLabel.Size = new System.Drawing.Size(46, 17);
			this.mouseCoordLabel.Text = "Mouse:";
			// 
			// ImageEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(642, 377);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "ImageEditor";
			this.Text = "ImageEditor";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			////((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
		private System.Windows.Forms.ListView toolBox;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripComboBox picNumberBox;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripTextBox1;
		private System.Windows.Forms.ToolStripMenuItem insertAsHexToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem oneStringToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem brokenUpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x8ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem x16ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem customToolStripMenuItem;
		private System.Windows.Forms.ToolStripTextBox HexWidthBox;
		private System.Windows.Forms.ToolStripTextBox HexHeightBox;
		private System.Windows.Forms.ToolStripMenuItem DoCustomHex;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel mouseCoordLabel;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.NumericUpDown numericUpDown2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolStripComboBox HexOrBinBox;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.PictureBox canvas;
	}
}