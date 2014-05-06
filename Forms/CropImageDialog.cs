using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Merthsoft.TokenIDE {
	public partial class CropImageDialog : Form {
		Bitmap b;
		Rectangle cropRect = new Rectangle(0, 0, 96, 64);
		bool mouseDown;
		int zoom = 1;
		int tol = 127;

		Bitmap _outMap;
		public Bitmap outMap { get { return _outMap; } }

		public CropImageDialog(Bitmap b) {
			InitializeComponent();
			this.b = b;
			//pictureBox1.Image = b;
			pictureBox1.Size = b.Size;
			this.Icon = TokenIDE.Properties.Resources.Tokens_Icon;
		}

		private void CropImageDialog_Load(object sender, EventArgs e) {
		}

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
			mouseDown = true;
			cropRect.X = e.X;
			cropRect.Y = e.Y;
		}

		private void pictureBox1_MouseUp(object sender, MouseEventArgs e) {
			mouseDown = false;
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e) {
			if (mouseDown) {
				cropRect.X = e.X;
				cropRect.Y = e.Y;
				pictureBox1.Invalidate();
			}
		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e) {
			e.Graphics.DrawImage(b, 0, 0, b.Width * zoom, b.Height * zoom);

			Bitmap resized = new Bitmap(b.Width + 200, b.Height + 200);
			using (Graphics g = Graphics.FromImage((Image)resized)) {
				g.FillRectangle(Brushes.White, 0, 0, b.Width + 200, b.Height + 200);
				g.DrawImage(b, 100, 100, b.Width, b.Height);
			}
			var prev = resized.Clone(new Rectangle(cropRect.X / zoom + 100, cropRect.Y / zoom + 100, cropRect.Width, cropRect.Height), resized.PixelFormat);
			for (int i = 0; i < cropRect.Width; i++) {
				for (int j = 0; j < cropRect.Height; j++) {
					Color c = prev.GetPixel(i, j);
					if ((c.R + c.G + c.B) / 3 > tol) {
						prev.SetPixel(i, j, Color.White);
					} else {
						prev.SetPixel(i, j, Color.Black);
					}
				}
			}
			e.Graphics.DrawImage(prev, cropRect.X, cropRect.Y, cropRect.Width * zoom, cropRect.Height * zoom);
			e.Graphics.DrawRectangle(Pens.Black, cropRect.X, cropRect.Y, cropRect.Width * zoom, cropRect.Height * zoom);
		}

		private void DoneButton_Click(object sender, EventArgs e) {
			Bitmap resized = new Bitmap(b.Width + 200, b.Height + 200);
			using (Graphics g = Graphics.FromImage((Image)resized)) {
				g.FillRectangle(Brushes.White, 0, 0, b.Width + 200, b.Height + 200);
				g.DrawImage(b, 100, 100, b.Width, b.Height);
			}

			_outMap = resized.Clone(new Rectangle(cropRect.X / zoom + 100, cropRect.Y / zoom + 100, cropRect.Width, cropRect.Height), resized.PixelFormat);
			_outMap.PosterizeImage(cropRect, tol);

			this.Close();
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
			zoom = (int)numericUpDown1.Value;
			pictureBox1.Width = b.Width * zoom;
			pictureBox1.Height = b.Height * zoom;
		}

		private void ToleranceUpDown_ValueChanged(object sender, EventArgs e) {
			tol = (int)ToleranceUpDown.Value;
			pictureBox1.Invalidate();
		}
	}
}
