namespace Merthsoft.TokenIDE {
	partial class DCSGUIDesigner {
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
			this.doneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.panel2 = new System.Windows.Forms.Panel();
			this.toolBox = new System.Windows.Forms.ListView();
			this.activeControlsList = new System.Windows.Forms.ListBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.canvas = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.mouseCoordLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.menuStrip1.SuspendLayout();
			//////((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			////((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			////((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.doneToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(918, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// doneToolStripMenuItem
			// 
			this.doneToolStripMenuItem.Name = "doneToolStripMenuItem";
			this.doneToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
			this.doneToolStripMenuItem.Text = "Done";
			this.doneToolStripMenuItem.Click += new System.EventHandler(this.doneToolStripMenuItem_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 24);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.panel1);
			this.splitContainer1.Size = new System.Drawing.Size(918, 460);
			this.splitContainer1.SplitterDistance = 225;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 1;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.panel2);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.activeControlsList);
			this.splitContainer2.Size = new System.Drawing.Size(225, 460);
			this.splitContainer2.SplitterDistance = 197;
			this.splitContainer2.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.toolBox);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(225, 197);
			this.panel2.TabIndex = 0;
			// 
			// toolBox
			// 
			this.toolBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolBox.Location = new System.Drawing.Point(0, 0);
			this.toolBox.MultiSelect = false;
			this.toolBox.Name = "toolBox";
			this.toolBox.Size = new System.Drawing.Size(225, 197);
			this.toolBox.TabIndex = 0;
			this.toolBox.UseCompatibleStateImageBehavior = false;
			this.toolBox.View = System.Windows.Forms.View.List;
			this.toolBox.SelectedIndexChanged += new System.EventHandler(this.toolbox_SelectedIndexChanged);
			// 
			// activeControlsList
			// 
			this.activeControlsList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.activeControlsList.FormattingEnabled = true;
			this.activeControlsList.Location = new System.Drawing.Point(0, 0);
			this.activeControlsList.Name = "activeControlsList";
			this.activeControlsList.Size = new System.Drawing.Size(225, 259);
			this.activeControlsList.TabIndex = 5;
			this.activeControlsList.SelectedIndexChanged += new System.EventHandler(this.activeControlsList_SelectedIndexChanged);
			this.activeControlsList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.activeControlsList_KeyDown);
			this.activeControlsList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.activeControlsList_KeyPress);
			this.activeControlsList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.activeControlsList_KeyUp);
			this.activeControlsList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.activeControlsList_MouseDoubleClick);
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.splitContainer3);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(688, 460);
			this.panel1.TabIndex = 0;
			this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.canvas);
			this.splitContainer3.Panel1.Controls.Add(this.label1);
			this.splitContainer3.Panel1.Controls.Add(this.numericUpDown1);
			this.splitContainer3.Panel1.Controls.Add(this.statusStrip1);
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.propertyGrid1);
			this.splitContainer3.Size = new System.Drawing.Size(688, 460);
			this.splitContainer3.SplitterDistance = 507;
			this.splitContainer3.TabIndex = 17;
			// 
			// canvas
			// 
			this.canvas.BackColor = System.Drawing.Color.White;
			this.canvas.Location = new System.Drawing.Point(3, 31);
			this.canvas.Name = "canvas";
			this.canvas.Size = new System.Drawing.Size(96, 64);
			this.canvas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.canvas.TabIndex = 8;
			this.canvas.TabStop = false;
			this.canvas.Click += new System.EventHandler(this.canvas_Click);
			this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
			this.canvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseDown);
			this.canvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.canvas_MouseMove);
			this.canvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(37, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Zoom:";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(46, 2);
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
			this.numericUpDown1.TabIndex = 10;
			this.numericUpDown1.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mouseCoordLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 438);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(507, 22);
			this.statusStrip1.TabIndex = 16;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// mouseCoordLabel
			// 
			this.mouseCoordLabel.Name = "mouseCoordLabel";
			this.mouseCoordLabel.Size = new System.Drawing.Size(46, 17);
			this.mouseCoordLabel.Text = "Mouse:";
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(177, 460);
			this.propertyGrid1.TabIndex = 0;
			this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
			// 
			// DCSGUIDesigner
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(918, 484);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "DCSGUIDesigner";
			this.Text = "DCSGUIDesigner";
			this.Load += new System.EventHandler(this.DCSGUIDesigner_Load);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DCSGUIDesigner_KeyUp);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			////((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			////((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.Panel1.PerformLayout();
			this.splitContainer3.Panel2.ResumeLayout(false);
			////((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
			this.splitContainer3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.ListBox activeControlsList;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox canvas;
		private System.Windows.Forms.ListView toolBox;
		private System.Windows.Forms.ToolStripMenuItem doneToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel mouseCoordLabel;
		private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
	}
}