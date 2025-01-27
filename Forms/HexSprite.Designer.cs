﻿using System.Windows.Forms;
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
				CelticBrushes.ForEach(b => b.Dispose());
				if (errorImage != null) {
					errorImage.Dispose();
					errorImage = null;
				}
				if (currentButton != null) {
					currentButton.Dispose();
					currentButton = null;
				}
				if (drawCanvas != null) {
					drawCanvas.Dispose();
					drawCanvas = null;
				}
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HexSprite));
            this.paletteBox = new System.Windows.Forms.PictureBox();
            this.MaintainDim = new System.Windows.Forms.CheckBox();
            this.drawGridBox = new System.Windows.Forms.CheckBox();
            this.pixelSizeBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.spriteHeightBox = new System.Windows.Forms.NumericUpDown();
            this.spriteWidthBox = new System.Windows.Forms.NumericUpDown();
            this.spriteBox = new System.Windows.Forms.PictureBox();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.undoButton = new System.Windows.Forms.ToolStripButton();
            this.redoButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.topPanel = new System.Windows.Forms.Panel();
            this.mouseButtonsPanel = new System.Windows.Forms.Panel();
            this.rightMouseLabel = new System.Windows.Forms.Label();
            this.leftMouseLabel = new System.Windows.Forms.Label();
            this.rightMousePictureBox = new System.Windows.Forms.PictureBox();
            this.leftMousePictureBox = new System.Windows.Forms.PictureBox();
            this.useGBox = new System.Windows.Forms.CheckBox();
            this.paletteChoice = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.penWidthBox = new System.Windows.Forms.NumericUpDown();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearTilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.insertAndExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shiftTilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.loadTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monochromePicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xLIBCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xLIBCBackgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xLIBCToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dCSHeaderSpriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redrawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spritePanel = new System.Windows.Forms.Panel();
            this.mainContainer = new System.Windows.Forms.SplitContainer();
            this.tilesFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tilesPaneOrientationMenu = new System.Windows.Forms.ToolStripSplitButton();
            this.horizonalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spriteIndexLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.outputLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.clearTextTimer = new System.Windows.Forms.Timer(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.paletteBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixelSizeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spriteHeightBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spriteWidthBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spriteBox)).BeginInit();
            this.mainToolStrip.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.mouseButtonsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightMousePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftMousePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.penWidthBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.spritePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainContainer)).BeginInit();
            this.mainContainer.Panel1.SuspendLayout();
            this.mainContainer.Panel2.SuspendLayout();
            this.mainContainer.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // paletteBox
            // 
            this.paletteBox.Location = new System.Drawing.Point(443, 4);
            this.paletteBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.paletteBox.Name = "paletteBox";
            this.paletteBox.Size = new System.Drawing.Size(469, 108);
            this.paletteBox.TabIndex = 21;
            this.paletteBox.TabStop = false;
            this.paletteBox.Paint += new System.Windows.Forms.PaintEventHandler(this.paletteBox_Paint);
            this.paletteBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.paletteBox_MouseClick);
            this.paletteBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.paletteBox_MouseMove);
            // 
            // MaintainDim
            // 
            this.MaintainDim.AutoSize = true;
            this.MaintainDim.Location = new System.Drawing.Point(127, 18);
            this.MaintainDim.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaintainDim.Name = "MaintainDim";
            this.MaintainDim.Size = new System.Drawing.Size(106, 20);
            this.MaintainDim.TabIndex = 18;
            this.MaintainDim.Text = "Maintain Dim";
            this.MaintainDim.UseVisualStyleBackColor = true;
            // 
            // drawGridBox
            // 
            this.drawGridBox.AutoSize = true;
            this.drawGridBox.Location = new System.Drawing.Point(127, 38);
            this.drawGridBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.drawGridBox.Name = "drawGridBox";
            this.drawGridBox.Size = new System.Drawing.Size(88, 20);
            this.drawGridBox.TabIndex = 17;
            this.drawGridBox.Text = "Draw Grid";
            this.drawGridBox.UseVisualStyleBackColor = true;
            this.drawGridBox.CheckedChanged += new System.EventHandler(this.DrawGrid_CheckedChanged);
            // 
            // pixelSizeBox
            // 
            this.pixelSizeBox.Location = new System.Drawing.Point(65, 50);
            this.pixelSizeBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.pixelSizeBox.Size = new System.Drawing.Size(53, 22);
            this.pixelSizeBox.TabIndex = 16;
            this.pixelSizeBox.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.pixelSizeBox.ValueChanged += new System.EventHandler(this.PixelSize_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Zoom:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Width x Height";
            // 
            // spriteHeightBox
            // 
            this.spriteHeightBox.Location = new System.Drawing.Point(65, 20);
            this.spriteHeightBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.spriteHeightBox.Size = new System.Drawing.Size(53, 22);
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
            this.spriteWidthBox.Location = new System.Drawing.Point(5, 20);
            this.spriteWidthBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.spriteWidthBox.Size = new System.Drawing.Size(53, 22);
            this.spriteWidthBox.TabIndex = 7;
            this.spriteWidthBox.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.spriteWidthBox.ValueChanged += new System.EventHandler(this.Width_ValueChanged);
            // 
            // spriteBox
            // 
            this.spriteBox.BackColor = System.Drawing.Color.White;
            this.spriteBox.Location = new System.Drawing.Point(4, 4);
            this.spriteBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.spriteBox.Name = "spriteBox";
            this.spriteBox.Size = new System.Drawing.Size(81, 81);
            this.spriteBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.spriteBox.TabIndex = 0;
            this.spriteBox.TabStop = false;
            this.spriteBox.Paint += new System.Windows.Forms.PaintEventHandler(this.spriteBox_Paint);
            this.spriteBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.spriteBox_MouseDown);
            this.spriteBox.MouseEnter += new System.EventHandler(this.spriteBox_MouseEnter);
            this.spriteBox.MouseLeave += new System.EventHandler(this.spriteBox_MouseLeave);
            this.spriteBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.spriteBox_MouseMove);
            this.spriteBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.spriteBox_MouseUp);
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoButton,
            this.redoButton,
            this.toolStripSeparator1});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 30);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(941, 31);
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
            this.undoButton.Size = new System.Drawing.Size(29, 28);
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
            this.redoButton.Size = new System.Drawing.Size(29, 28);
            this.redoButton.Text = "Redo (Ctrl+Y)";
            this.redoButton.Click += new System.EventHandler(this.redoButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.mouseButtonsPanel);
            this.topPanel.Controls.Add(this.paletteBox);
            this.topPanel.Controls.Add(this.useGBox);
            this.topPanel.Controls.Add(this.paletteChoice);
            this.topPanel.Controls.Add(this.label3);
            this.topPanel.Controls.Add(this.penWidthBox);
            this.topPanel.Controls.Add(this.spriteWidthBox);
            this.topPanel.Controls.Add(this.spriteHeightBox);
            this.topPanel.Controls.Add(this.label1);
            this.topPanel.Controls.Add(this.label2);
            this.topPanel.Controls.Add(this.pixelSizeBox);
            this.topPanel.Controls.Add(this.drawGridBox);
            this.topPanel.Controls.Add(this.MaintainDim);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 61);
            this.topPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(941, 116);
            this.topPanel.TabIndex = 23;
            // 
            // mouseButtonsPanel
            // 
            this.mouseButtonsPanel.Controls.Add(this.rightMouseLabel);
            this.mouseButtonsPanel.Controls.Add(this.leftMouseLabel);
            this.mouseButtonsPanel.Controls.Add(this.rightMousePictureBox);
            this.mouseButtonsPanel.Controls.Add(this.leftMousePictureBox);
            this.mouseButtonsPanel.Location = new System.Drawing.Point(296, 4);
            this.mouseButtonsPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mouseButtonsPanel.Name = "mouseButtonsPanel";
            this.mouseButtonsPanel.Size = new System.Drawing.Size(144, 111);
            this.mouseButtonsPanel.TabIndex = 32;
            // 
            // rightMouseLabel
            // 
            this.rightMouseLabel.AutoSize = true;
            this.rightMouseLabel.Location = new System.Drawing.Point(76, 0);
            this.rightMouseLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rightMouseLabel.Name = "rightMouseLabel";
            this.rightMouseLabel.Size = new System.Drawing.Size(41, 16);
            this.rightMouseLabel.TabIndex = 29;
            this.rightMouseLabel.Text = "Right:";
            // 
            // leftMouseLabel
            // 
            this.leftMouseLabel.AutoSize = true;
            this.leftMouseLabel.Location = new System.Drawing.Point(3, 0);
            this.leftMouseLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.leftMouseLabel.Name = "leftMouseLabel";
            this.leftMouseLabel.Size = new System.Drawing.Size(31, 16);
            this.leftMouseLabel.TabIndex = 28;
            this.leftMouseLabel.Text = "Left:";
            // 
            // rightMousePictureBox
            // 
            this.rightMousePictureBox.Location = new System.Drawing.Point(76, 16);
            this.rightMousePictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rightMousePictureBox.Name = "rightMousePictureBox";
            this.rightMousePictureBox.Size = new System.Drawing.Size(65, 91);
            this.rightMousePictureBox.TabIndex = 27;
            this.rightMousePictureBox.TabStop = false;
            this.rightMousePictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.rightMousePictureBox_Paint);
            // 
            // leftMousePictureBox
            // 
            this.leftMousePictureBox.Location = new System.Drawing.Point(3, 16);
            this.leftMousePictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.leftMousePictureBox.Name = "leftMousePictureBox";
            this.leftMousePictureBox.Size = new System.Drawing.Size(65, 91);
            this.leftMousePictureBox.TabIndex = 26;
            this.leftMousePictureBox.TabStop = false;
            this.leftMousePictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.leftMousePictureBox_Paint);
            // 
            // useGBox
            // 
            this.useGBox.AutoSize = true;
            this.useGBox.Location = new System.Drawing.Point(127, 58);
            this.useGBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.useGBox.Name = "useGBox";
            this.useGBox.Size = new System.Drawing.Size(153, 20);
            this.useGBox.TabIndex = 31;
            this.useGBox.Text = "Use \"G\" Optimization";
            this.useGBox.UseVisualStyleBackColor = true;
            // 
            // paletteChoice
            // 
            this.paletteChoice.FormattingEnabled = true;
            this.paletteChoice.Location = new System.Drawing.Point(127, 81);
            this.paletteChoice.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.paletteChoice.Name = "paletteChoice";
            this.paletteChoice.Size = new System.Drawing.Size(160, 24);
            this.paletteChoice.TabIndex = 25;
            this.paletteChoice.SelectedIndexChanged += new System.EventHandler(this.paletteChoice_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 16);
            this.label3.TabIndex = 23;
            this.label3.Text = "Pen:";
            // 
            // penWidthBox
            // 
            this.penWidthBox.Location = new System.Drawing.Point(65, 82);
            this.penWidthBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.penWidthBox.Size = new System.Drawing.Size(53, 22);
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
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(941, 30);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.addTilesToolStripMenuItem,
            this.clearTilesToolStripMenuItem,
            this.importImageToolStripMenuItem,
            this.exportImageToolStripMenuItem,
            this.toolStripSeparator2,
            this.insertAndExitToolStripMenuItem,
            this.copyToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 26);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::Merthsoft.TokenIDE.Properties.Resources.icon_open;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = global::Merthsoft.TokenIDE.Properties.Resources.icon_save;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Image = global::Merthsoft.TokenIDE.Properties.Resources.icon_save;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // addTilesToolStripMenuItem
            // 
            this.addTilesToolStripMenuItem.Image = global::Merthsoft.TokenIDE.Properties.Resources.icon_open;
            this.addTilesToolStripMenuItem.Name = "addTilesToolStripMenuItem";
            this.addTilesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.addTilesToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.addTilesToolStripMenuItem.Text = "Add Tiles";
            this.addTilesToolStripMenuItem.Click += new System.EventHandler(this.addTilesToolStripMenuItem_Click);
            // 
            // clearTilesToolStripMenuItem
            // 
            this.clearTilesToolStripMenuItem.Name = "clearTilesToolStripMenuItem";
            this.clearTilesToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.clearTilesToolStripMenuItem.Text = "Clear Tiles";
            this.clearTilesToolStripMenuItem.Click += new System.EventHandler(this.clearTilesToolStripMenuItem_Click);
            // 
            // importImageToolStripMenuItem
            // 
            this.importImageToolStripMenuItem.Image = global::Merthsoft.TokenIDE.Properties.Resources.icon_pic;
            this.importImageToolStripMenuItem.Name = "importImageToolStripMenuItem";
            this.importImageToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.importImageToolStripMenuItem.Text = "Import Image";
            this.importImageToolStripMenuItem.Click += new System.EventHandler(this.importImageToolStripMenuItem_Click);
            // 
            // exportImageToolStripMenuItem
            // 
            this.exportImageToolStripMenuItem.Image = global::Merthsoft.TokenIDE.Properties.Resources.icon_save;
            this.exportImageToolStripMenuItem.Name = "exportImageToolStripMenuItem";
            this.exportImageToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.exportImageToolStripMenuItem.Text = "Export Image";
            this.exportImageToolStripMenuItem.Click += new System.EventHandler(this.exportImageToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(228, 6);
            // 
            // insertAndExitToolStripMenuItem
            // 
            this.insertAndExitToolStripMenuItem.Name = "insertAndExitToolStripMenuItem";
            this.insertAndExitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.insertAndExitToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.insertAndExitToolStripMenuItem.Text = "Insert and Exit";
            this.insertAndExitToolStripMenuItem.Click += new System.EventHandler(this.insertAndExitToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.copyToolStripMenuItem.Text = "Copy Hex";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.shiftTilesToolStripMenuItem,
            this.toolStripSeparator3,
            this.loadTemplateToolStripMenuItem,
            this.redrawToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(49, 26);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Enabled = false;
            this.undoToolStripMenuItem.Image = global::Merthsoft.TokenIDE.Properties.Resources.icon_undo;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoButton_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Enabled = false;
            this.redoToolStripMenuItem.Image = global::Merthsoft.TokenIDE.Properties.Resources.icon_redo;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoButton_Click);
            // 
            // shiftTilesToolStripMenuItem
            // 
            this.shiftTilesToolStripMenuItem.Name = "shiftTilesToolStripMenuItem";
            this.shiftTilesToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.shiftTilesToolStripMenuItem.Text = "Shift Values";
            this.shiftTilesToolStripMenuItem.Click += new System.EventHandler(this.shiftTilesToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(188, 6);
            // 
            // loadTemplateToolStripMenuItem
            // 
            this.loadTemplateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.monochromePicToolStripMenuItem,
            this.colorPicToolStripMenuItem,
            this.colorImageToolStripMenuItem,
            this.xLIBCToolStripMenuItem,
            this.xLIBCBackgroundToolStripMenuItem,
            this.xLIBCToolStripMenuItem1,
            this.dCSHeaderSpriteToolStripMenuItem});
            this.loadTemplateToolStripMenuItem.Name = "loadTemplateToolStripMenuItem";
            this.loadTemplateToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.loadTemplateToolStripMenuItem.Text = "Load Template";
            // 
            // monochromePicToolStripMenuItem
            // 
            this.monochromePicToolStripMenuItem.Name = "monochromePicToolStripMenuItem";
            this.monochromePicToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.monochromePicToolStripMenuItem.Text = "Monochrome Pic";
            this.monochromePicToolStripMenuItem.Click += new System.EventHandler(this.monochromePicToolStripMenuItem_Click);
            // 
            // colorPicToolStripMenuItem
            // 
            this.colorPicToolStripMenuItem.Name = "colorPicToolStripMenuItem";
            this.colorPicToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.colorPicToolStripMenuItem.Text = "Color Pic";
            this.colorPicToolStripMenuItem.Click += new System.EventHandler(this.colorPicToolStripMenuItem_Click);
            // 
            // colorImageToolStripMenuItem
            // 
            this.colorImageToolStripMenuItem.Name = "colorImageToolStripMenuItem";
            this.colorImageToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.colorImageToolStripMenuItem.Text = "Color Image";
            this.colorImageToolStripMenuItem.Click += new System.EventHandler(this.colorImageToolStripMenuItem_Click);
            // 
            // xLIBCToolStripMenuItem
            // 
            this.xLIBCToolStripMenuItem.Name = "xLIBCToolStripMenuItem";
            this.xLIBCToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.xLIBCToolStripMenuItem.Text = "xLIBC Tiles";
            this.xLIBCToolStripMenuItem.Click += new System.EventHandler(this.xLIBCToolStripMenuItem_Click);
            // 
            // xLIBCBackgroundToolStripMenuItem
            // 
            this.xLIBCBackgroundToolStripMenuItem.Name = "xLIBCBackgroundToolStripMenuItem";
            this.xLIBCBackgroundToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.xLIBCBackgroundToolStripMenuItem.Text = "xLIBC Background";
            this.xLIBCBackgroundToolStripMenuItem.Click += new System.EventHandler(this.xLIBCBackgroundToolStripMenuItem_Click);
            // 
            // xLIBCToolStripMenuItem1
            // 
            this.xLIBCToolStripMenuItem1.Name = "xLIBCToolStripMenuItem1";
            this.xLIBCToolStripMenuItem1.Size = new System.Drawing.Size(236, 26);
            this.xLIBCToolStripMenuItem1.Text = "xLIBC 32-Color Image";
            this.xLIBCToolStripMenuItem1.Click += new System.EventHandler(this.xLIBCToolStripMenuItem1_Click);
            // 
            // dCSHeaderSpriteToolStripMenuItem
            // 
            this.dCSHeaderSpriteToolStripMenuItem.Name = "dCSHeaderSpriteToolStripMenuItem";
            this.dCSHeaderSpriteToolStripMenuItem.Size = new System.Drawing.Size(236, 26);
            this.dCSHeaderSpriteToolStripMenuItem.Text = "DCS Header Sprite";
            this.dCSHeaderSpriteToolStripMenuItem.Click += new System.EventHandler(this.dCSHeaderSpriteToolStripMenuItem_Click);
            // 
            // redrawToolStripMenuItem
            // 
            this.redrawToolStripMenuItem.Name = "redrawToolStripMenuItem";
            this.redrawToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.redrawToolStripMenuItem.Text = "Redraw";
            this.redrawToolStripMenuItem.Click += new System.EventHandler(this.redrawToolStripMenuItem_Click);
            // 
            // spritePanel
            // 
            this.spritePanel.AutoScroll = true;
            this.spritePanel.Controls.Add(this.mainContainer);
            this.spritePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spritePanel.Location = new System.Drawing.Point(0, 177);
            this.spritePanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.spritePanel.Name = "spritePanel";
            this.spritePanel.Size = new System.Drawing.Size(941, 523);
            this.spritePanel.TabIndex = 25;
            // 
            // mainContainer
            // 
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainContainer.Name = "mainContainer";
            // 
            // mainContainer.Panel1
            // 
            this.mainContainer.Panel1.Controls.Add(this.tilesFlow);
            // 
            // mainContainer.Panel2
            // 
            this.mainContainer.Panel2.AutoScroll = true;
            this.mainContainer.Panel2.Controls.Add(this.spriteBox);
            this.mainContainer.Size = new System.Drawing.Size(941, 523);
            this.mainContainer.SplitterDistance = 169;
            this.mainContainer.SplitterWidth = 5;
            this.mainContainer.TabIndex = 1;
            // 
            // tilesFlow
            // 
            this.tilesFlow.AutoScroll = true;
            this.tilesFlow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tilesFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilesFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.tilesFlow.Location = new System.Drawing.Point(0, 0);
            this.tilesFlow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tilesFlow.Name = "tilesFlow";
            this.tilesFlow.Size = new System.Drawing.Size(169, 523);
            this.tilesFlow.TabIndex = 2;
            this.tilesFlow.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tilesFlow_MouseClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tilesPaneOrientationMenu,
            this.spriteIndexLabel,
            this.outputLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 700);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(941, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tilesPaneOrientationMenu
            // 
            this.tilesPaneOrientationMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tilesPaneOrientationMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.horizonalToolStripMenuItem,
            this.verticalToolStripMenuItem});
            this.tilesPaneOrientationMenu.Image = ((System.Drawing.Image)(resources.GetObject("tilesPaneOrientationMenu.Image")));
            this.tilesPaneOrientationMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tilesPaneOrientationMenu.Name = "tilesPaneOrientationMenu";
            this.tilesPaneOrientationMenu.Size = new System.Drawing.Size(177, 24);
            this.tilesPaneOrientationMenu.Text = "Tiles pane oritentation";
            // 
            // horizonalToolStripMenuItem
            // 
            this.horizonalToolStripMenuItem.Name = "horizonalToolStripMenuItem";
            this.horizonalToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.horizonalToolStripMenuItem.Text = "Horizonal";
            this.horizonalToolStripMenuItem.Click += new System.EventHandler(this.horizonalToolStripMenuItem_Click);
            // 
            // verticalToolStripMenuItem
            // 
            this.verticalToolStripMenuItem.Name = "verticalToolStripMenuItem";
            this.verticalToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.verticalToolStripMenuItem.Text = "Vertical";
            this.verticalToolStripMenuItem.Click += new System.EventHandler(this.verticalToolStripMenuItem_Click);
            // 
            // spriteIndexLabel
            // 
            this.spriteIndexLabel.Name = "spriteIndexLabel";
            this.spriteIndexLabel.Size = new System.Drawing.Size(118, 20);
            this.spriteIndexLabel.Text = "spriteIndexLabel";
            // 
            // outputLabel
            // 
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(89, 20);
            this.outputLabel.Text = "outputLabel";
            this.outputLabel.TextChanged += new System.EventHandler(this.outputLabel_TextChanged);
            // 
            // clearTextTimer
            // 
            this.clearTextTimer.Interval = 500;
            this.clearTextTimer.Tick += new System.EventHandler(this.clearTextTimer_Tick);
            // 
            // colorDialog1
            // 
            this.colorDialog1.FullOpen = true;
            // 
            // HexSprite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 726);
            this.Controls.Add(this.spritePanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "HexSprite";
            this.Text = "Hex Sprite Editor";
            ((System.ComponentModel.ISupportInitialize)(this.paletteBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pixelSizeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spriteHeightBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spriteWidthBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spriteBox)).EndInit();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.mouseButtonsPanel.ResumeLayout(false);
            this.mouseButtonsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightMousePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftMousePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.penWidthBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.spritePanel.ResumeLayout(false);
            this.mainContainer.Panel1.ResumeLayout(false);
            this.mainContainer.Panel2.ResumeLayout(false);
            this.mainContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainContainer)).EndInit();
            this.mainContainer.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown spriteHeightBox;
		private System.Windows.Forms.NumericUpDown spriteWidthBox;
		private System.Windows.Forms.PictureBox spriteBox;
		private System.Windows.Forms.NumericUpDown pixelSizeBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox drawGridBox;
		private System.Windows.Forms.CheckBox MaintainDim;
		private System.Windows.Forms.PictureBox paletteBox;
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
		private ComboBox paletteChoice;
		private Label rightMouseLabel;
		private Label leftMouseLabel;
		private PictureBox rightMousePictureBox;
		private PictureBox leftMousePictureBox;
		private Panel spritePanel;
		private ToolStripMenuItem saveToolStripMenuItem;
		private ToolStripMenuItem saveAsToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripMenuItem insertAndExitToolStripMenuItem;
		private ToolStripMenuItem copyToolStripMenuItem;
		private StatusStrip statusStrip1;
		private ToolStripStatusLabel outputLabel;
		private Timer clearTextTimer;
		private CheckBox useGBox;
		private ToolStripStatusLabel spriteIndexLabel;
		private ColorDialog colorDialog1;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripMenuItem loadTemplateToolStripMenuItem;
		private ToolStripMenuItem monochromePicToolStripMenuItem;
		private ToolStripMenuItem colorPicToolStripMenuItem;
		private ToolStripMenuItem colorImageToolStripMenuItem;
		private ToolStripMenuItem xLIBCToolStripMenuItem;
		private ToolStripMenuItem xLIBCBackgroundToolStripMenuItem;
		private SplitContainer mainContainer;
		private FlowLayoutPanel tilesFlow;
		private Panel mouseButtonsPanel;
		private ToolStripMenuItem redrawToolStripMenuItem;
		private ToolStripMenuItem addTilesToolStripMenuItem;
		private ToolStripMenuItem importImageToolStripMenuItem;
		private ToolStripMenuItem exportImageToolStripMenuItem;
		private ToolStripMenuItem shiftTilesToolStripMenuItem;
		private ToolStripMenuItem clearTilesToolStripMenuItem;
		private ToolStripSplitButton tilesPaneOrientationMenu;
		private ToolStripMenuItem horizonalToolStripMenuItem;
		private ToolStripMenuItem verticalToolStripMenuItem;
		private ToolStripMenuItem xLIBCToolStripMenuItem1;
		private ToolStripMenuItem dCSHeaderSpriteToolStripMenuItem;

	}
}