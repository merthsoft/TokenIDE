using System.Windows.Forms;
namespace Merthsoft.TokenIDE {
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
			if (disposing) {
				if ((components != null)) {
					components.Dispose();
				}
				BrushList.ForEach(b => b.Dispose());
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
			this.colorCheckBox = new System.Windows.Forms.CheckBox();
			this.ResizeFromHexButton = new System.Windows.Forms.Button();
			this.MaintainDim = new System.Windows.Forms.CheckBox();
			this.DrawGrid = new System.Windows.Forms.CheckBox();
			this.pixelSizeBox = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.ActiveHex = new System.Windows.Forms.CheckBox();
			this.InsertButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.spriteHeightBox = new System.Windows.Forms.NumericUpDown();
			this.spriteWidthBox = new System.Windows.Forms.NumericUpDown();
			this.bottomPanel = new System.Windows.Forms.SplitContainer();
			this.spriteBox = new System.Windows.Forms.PictureBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.hexBox = new System.Windows.Forms.TextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.binBox = new System.Windows.Forms.TextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.hasGBox = new System.Windows.Forms.CheckBox();
			this.mainToolStrip = new System.Windows.Forms.ToolStrip();
			this.undoButton = new System.Windows.Forms.ToolStripButton();
			this.redoButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.topPanel = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.penWidthBox = new System.Windows.Forms.NumericUpDown();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.paletteBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pixelSizeBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spriteHeightBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spriteWidthBox)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bottomPanel)).BeginInit();
			this.bottomPanel.Panel1.SuspendLayout();
			this.bottomPanel.Panel2.SuspendLayout();
			this.bottomPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spriteBox)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.mainToolStrip.SuspendLayout();
			this.topPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.penWidthBox)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// paletteBox
			// 
			this.paletteBox.Location = new System.Drawing.Point(266, 9);
			this.paletteBox.Name = "paletteBox";
			this.paletteBox.Size = new System.Drawing.Size(216, 70);
			this.paletteBox.TabIndex = 21;
			this.paletteBox.TabStop = false;
			this.paletteBox.Visible = false;
			this.paletteBox.Paint += new System.Windows.Forms.PaintEventHandler(this.paletteBox_Paint);
			this.paletteBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.paletteBox_MouseClick);
			// 
			// colorCheckBox
			// 
			this.colorCheckBox.AutoSize = true;
			this.colorCheckBox.Location = new System.Drawing.Point(95, 67);
			this.colorCheckBox.Name = "colorCheckBox";
			this.colorCheckBox.Size = new System.Drawing.Size(50, 17);
			this.colorCheckBox.TabIndex = 20;
			this.colorCheckBox.Text = "Color";
			this.colorCheckBox.UseVisualStyleBackColor = true;
			this.colorCheckBox.CheckedChanged += new System.EventHandler(this.colorCheckBox_CheckedChanged);
			// 
			// ResizeFromHexButton
			// 
			this.ResizeFromHexButton.Location = new System.Drawing.Point(188, 38);
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
			// pixelSizeBox
			// 
			this.pixelSizeBox.Location = new System.Drawing.Point(49, 41);
			this.pixelSizeBox.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
			this.pixelSizeBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.pixelSizeBox.Name = "pixelSizeBox";
			this.pixelSizeBox.Size = new System.Drawing.Size(40, 20);
			this.pixelSizeBox.TabIndex = 16;
			this.pixelSizeBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.pixelSizeBox.ValueChanged += new System.EventHandler(this.PixelSize_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(37, 13);
			this.label2.TabIndex = 15;
			this.label2.Text = "Zoom:";
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
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Width x Height";
			// 
			// spriteHeightBox
			// 
			this.spriteHeightBox.Location = new System.Drawing.Point(49, 16);
			this.spriteHeightBox.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
			this.spriteHeightBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.spriteHeightBox.Name = "spriteHeightBox";
			this.spriteHeightBox.Size = new System.Drawing.Size(40, 20);
			this.spriteHeightBox.TabIndex = 8;
			this.spriteHeightBox.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.spriteHeightBox.ValueChanged += new System.EventHandler(this.Height_ValueChanged);
			// 
			// spriteWidthBox
			// 
			this.spriteWidthBox.Location = new System.Drawing.Point(4, 16);
			this.spriteWidthBox.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
			this.spriteWidthBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.spriteWidthBox.Name = "spriteWidthBox";
			this.spriteWidthBox.Size = new System.Drawing.Size(40, 20);
			this.spriteWidthBox.TabIndex = 7;
			this.spriteWidthBox.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.spriteWidthBox.ValueChanged += new System.EventHandler(this.Width_ValueChanged);
			// 
			// bottomPanel
			// 
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.bottomPanel.Location = new System.Drawing.Point(0, 145);
			this.bottomPanel.Name = "bottomPanel";
			// 
			// bottomPanel.Panel1
			// 
			this.bottomPanel.Panel1.AutoScroll = true;
			this.bottomPanel.Panel1.Controls.Add(this.spriteBox);
			// 
			// bottomPanel.Panel2
			// 
			this.bottomPanel.Panel2.Controls.Add(this.tabControl1);
			this.bottomPanel.Panel2.Controls.Add(this.statusStrip1);
			this.bottomPanel.Size = new System.Drawing.Size(706, 445);
			this.bottomPanel.SplitterDistance = 469;
			this.bottomPanel.TabIndex = 1;
			// 
			// spriteBox
			// 
			this.spriteBox.BackColor = System.Drawing.Color.White;
			this.spriteBox.Location = new System.Drawing.Point(3, 3);
			this.spriteBox.Name = "spriteBox";
			this.spriteBox.Size = new System.Drawing.Size(81, 81);
			this.spriteBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.spriteBox.TabIndex = 0;
			this.spriteBox.TabStop = false;
			this.spriteBox.Paint += new System.Windows.Forms.PaintEventHandler(this.spriteBox_Paint);
			this.spriteBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.spriteBox_MouseDown);
			this.spriteBox.MouseLeave += new System.EventHandler(this.spriteBox_MouseLeave);
			this.spriteBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.spriteBox_MouseMove);
			this.spriteBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.spriteBox_MouseUp);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(233, 423);
			this.tabControl1.TabIndex = 16;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.hexBox);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(225, 397);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Hex";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// hexBox
			// 
			this.hexBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.hexBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.hexBox.Location = new System.Drawing.Point(3, 3);
			this.hexBox.Multiline = true;
			this.hexBox.Name = "hexBox";
			this.hexBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.hexBox.Size = new System.Drawing.Size(219, 391);
			this.hexBox.TabIndex = 14;
			this.hexBox.TextChanged += new System.EventHandler(this.hexBox_TextChanged);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.binBox);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(225, 397);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Binary";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// binBox
			// 
			this.binBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.binBox.Location = new System.Drawing.Point(3, 3);
			this.binBox.Multiline = true;
			this.binBox.Name = "binBox";
			this.binBox.ReadOnly = true;
			this.binBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.binBox.Size = new System.Drawing.Size(219, 391);
			this.binBox.TabIndex = 15;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 423);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(233, 22);
			this.statusStrip1.TabIndex = 13;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
			this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
			// 
			// hasGBox
			// 
			this.hasGBox.AutoSize = true;
			this.hasGBox.Location = new System.Drawing.Point(188, 62);
			this.hasGBox.Name = "hasGBox";
			this.hasGBox.Size = new System.Drawing.Size(66, 17);
			this.hasGBox.TabIndex = 22;
			this.hasGBox.Text = "Use \"G\"";
			this.hasGBox.UseVisualStyleBackColor = true;
			this.hasGBox.CheckedChanged += new System.EventHandler(this.hasGBox_CheckedChanged);
			// 
			// mainToolStrip
			// 
			this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoButton,
            this.redoButton,
            this.toolStripSeparator1});
			this.mainToolStrip.Location = new System.Drawing.Point(0, 24);
			this.mainToolStrip.Name = "mainToolStrip";
			this.mainToolStrip.Size = new System.Drawing.Size(706, 27);
			this.mainToolStrip.TabIndex = 23;
			this.mainToolStrip.Text = "toolStrip1";
			// 
			// undoButton
			// 
			this.undoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.undoButton.Enabled = false;
			this.undoButton.Image = global::Merthsoft.TokenIDE.Properties.Resources.icon_undo;
			this.undoButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.undoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.undoButton.Name = "undoButton";
			this.undoButton.Size = new System.Drawing.Size(24, 24);
			this.undoButton.Text = "Undo (Ctrl+Z)";
			this.undoButton.Click += new System.EventHandler(this.undoButton_Click);
			// 
			// redoButton
			// 
			this.redoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.redoButton.Enabled = false;
			this.redoButton.Image = global::Merthsoft.TokenIDE.Properties.Resources.icon_redo;
			this.redoButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.redoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.redoButton.Name = "redoButton";
			this.redoButton.Size = new System.Drawing.Size(24, 24);
			this.redoButton.Text = "Redo (Ctrl+Y)";
			this.redoButton.Click += new System.EventHandler(this.redoButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
			// 
			// topPanel
			// 
			this.topPanel.Controls.Add(this.label3);
			this.topPanel.Controls.Add(this.penWidthBox);
			this.topPanel.Controls.Add(this.spriteWidthBox);
			this.topPanel.Controls.Add(this.spriteHeightBox);
			this.topPanel.Controls.Add(this.label1);
			this.topPanel.Controls.Add(this.InsertButton);
			this.topPanel.Controls.Add(this.ActiveHex);
			this.topPanel.Controls.Add(this.label2);
			this.topPanel.Controls.Add(this.pixelSizeBox);
			this.topPanel.Controls.Add(this.DrawGrid);
			this.topPanel.Controls.Add(this.MaintainDim);
			this.topPanel.Controls.Add(this.ResizeFromHexButton);
			this.topPanel.Controls.Add(this.colorCheckBox);
			this.topPanel.Controls.Add(this.paletteBox);
			this.topPanel.Controls.Add(this.hasGBox);
			this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.topPanel.Location = new System.Drawing.Point(0, 51);
			this.topPanel.Name = "topPanel";
			this.topPanel.Size = new System.Drawing.Size(706, 94);
			this.topPanel.TabIndex = 23;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(15, 69);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 13);
			this.label3.TabIndex = 23;
			this.label3.Text = "Pen:";
			// 
			// penWidthBox
			// 
			this.penWidthBox.Location = new System.Drawing.Point(49, 67);
			this.penWidthBox.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
			this.penWidthBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.penWidthBox.Name = "penWidthBox";
			this.penWidthBox.Size = new System.Drawing.Size(40, 20);
			this.penWidthBox.TabIndex = 24;
			this.penWidthBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 23);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(706, 24);
			this.menuStrip1.TabIndex = 24;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = global::Merthsoft.TokenIDE.Properties.Resources.icon_open;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripButton_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// undoToolStripMenuItem
			// 
			this.undoToolStripMenuItem.Image = global::Merthsoft.TokenIDE.Properties.Resources.icon_undo;
			this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
			this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
			this.undoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.undoToolStripMenuItem.Text = "Undo";
			this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoButton_Click);
			// 
			// redoToolStripMenuItem
			// 
			this.redoToolStripMenuItem.Image = global::Merthsoft.TokenIDE.Properties.Resources.icon_redo;
			this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
			this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
			this.redoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
			this.redoToolStripMenuItem.Text = "Redo";
			this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoButton_Click);
			// 
			// HexSprite
			// 
			this.AcceptButton = this.InsertButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(706, 590);
			this.Controls.Add(this.bottomPanel);
			this.Controls.Add(this.topPanel);
			this.Controls.Add(this.mainToolStrip);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "HexSprite";
			this.Text = "Hex Sprite Editor";
			((System.ComponentModel.ISupportInitialize)(this.paletteBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pixelSizeBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spriteHeightBox)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spriteWidthBox)).EndInit();
			this.bottomPanel.Panel1.ResumeLayout(false);
			this.bottomPanel.Panel1.PerformLayout();
			this.bottomPanel.Panel2.ResumeLayout(false);
			this.bottomPanel.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.bottomPanel)).EndInit();
			this.bottomPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spriteBox)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.mainToolStrip.ResumeLayout(false);
			this.mainToolStrip.PerformLayout();
			this.topPanel.ResumeLayout(false);
			this.topPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.penWidthBox)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button InsertButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown spriteHeightBox;
		private System.Windows.Forms.NumericUpDown spriteWidthBox;
		private System.Windows.Forms.PictureBox spriteBox;
		private System.Windows.Forms.SplitContainer bottomPanel;
		private System.Windows.Forms.CheckBox ActiveHex;
		private System.Windows.Forms.NumericUpDown pixelSizeBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox DrawGrid;
		private System.Windows.Forms.TextBox hexBox;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.CheckBox MaintainDim;
		private System.Windows.Forms.Button ResizeFromHexButton;
		private System.Windows.Forms.TextBox binBox;
		private System.Windows.Forms.PictureBox paletteBox;
		private System.Windows.Forms.CheckBox colorCheckBox;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.CheckBox hasGBox;
		private System.Windows.Forms.ToolStrip mainToolStrip;
		private System.Windows.Forms.Panel topPanel;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton undoButton;
		private System.Windows.Forms.ToolStripButton redoButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown penWidthBox;
		private ToolStripStatusLabel toolStripStatusLabel2;
		private ToolStripStatusLabel toolStripStatusLabel3;

	}
}