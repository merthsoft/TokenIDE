using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Merthsoft.TokenIDE {
	public partial class ShiftTiles : Form {
		public int Start {
			get { return (int)startBox.Value; }
			set { startBox.Value = value;}
		}

		public int End {
			get { return (int)endBox.Value; }
			set { endBox.Value = value; }
		}

		public int Amount {
			get { return (int)amountBox.Value; }
			set { amountBox.Value = value; }
		}

		public HexSprite ParentEditor { get; set; }

		public Sprite Sprite {
			get {
				int modAmount = 0;
				switch (ParentEditor.SelectedPalette) {
					case Merthsoft.TokenIDE.HexSprite.Palette.BlackAndWhite:
						modAmount = 2;
						break;
					case Merthsoft.TokenIDE.HexSprite.Palette.BasicColors:
						modAmount = ParentEditor.CelticPalette.Count;
						break;
					case Merthsoft.TokenIDE.HexSprite.Palette.xLIBC:
						modAmount = ParentEditor.XLibPalette.Count;
						break;
					case Merthsoft.TokenIDE.HexSprite.Palette.Full565:
						modAmount = ushort.MaxValue;
						break;
					default:
						break;
				}

				Sprite s = new Sprite(ParentEditor.Sprite);
				for (int i = 0; i < s.Width; i++) {
					for (int j = 0; j < s.Height; j++) {
						int tile = s[i, j];
						if (tile >= Start && tile <= End) {
							s[i, j] = (s[i, j] + Amount) % modAmount;
						}
					}
				}

				return s;
			}
		}

		private int width;
		private int height;
		private int zoom { get { return (int)zoomBox.Value; } }

		public ShiftTiles(HexSprite parent) {
			InitializeComponent();

			ParentEditor = parent;

			setWidthAndHeight();
		}

		private void setWidthAndHeight() {
			width = ParentEditor.SpriteWidth;
			height = ParentEditor.SpriteHeight;
			if (ParentEditor.MapMode) {
				width *= 8;
				height *= 8;
			}
			width *= zoom;
			height *= zoom;

			spriteBox.Width = width;
			spriteBox.Height = height;
		}

		private void okButton_Click(object sender, EventArgs e) {
			Close();
		}

		private void cancelButton_Click(object sender, EventArgs e) {
			Close();
		}

		private void ShiftTiles_FormClosing(object sender, FormClosingEventArgs e) {
			if (DialogResult == System.Windows.Forms.DialogResult.OK) {
				if (End < Start) {
					MessageBox.Show("End value must be greater than or equal to start value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					e.Cancel = true;
					return;
				}
			}
		}

		private void spriteBox_Paint(object sender, PaintEventArgs e) {
			using (Bitmap b = new Bitmap(width/zoom, height/zoom)) {
				ParentEditor.Sprite.Invalidate();
				ParentEditor.DrawSprite(b, Sprite);
				e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				e.Graphics.DrawImage(b, 0, 0, width, height);
			}
		}

		private void startBox_ValueChanged(object sender, EventArgs e) {
			spriteBox.Invalidate();
		}

		private void endBox_ValueChanged(object sender, EventArgs e) {
			spriteBox.Invalidate();
		}

		private void amountBox_ValueChanged(object sender, EventArgs e) {
			spriteBox.Invalidate();
		}

		private void pixelSizeBox_ValueChanged(object sender, EventArgs e) {
			setWidthAndHeight();
			spriteBox.Invalidate();
		}
	}
}
