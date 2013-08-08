using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Merthsoft.Tokens;
using Merthsoft.Extensions;

namespace Merthsoft.TokenIDE {
	public partial class HexSprite : Form {
		int[,] sprite;
		int w, h;
		bool mouseDown;
		int pixelMode = -1;
		int mX, mY;
		int pixelSize;
		bool performResizeFlag = true;
		public string outString = "";

		public HexSprite() {
			InitializeComponent();

			IntPtr iconPtr = TokenIDE.Properties.Resources.icon_hexsprite.GetHicon();
			using (Icon icon = Icon.FromHandle(iconPtr)) {
				this.Icon = icon;
			}

			w = 8;
			h = 8;
			sprite = new int[w, h];
			pixelSize = 10;
			updateHex();
		}

		public HexSprite(string hex) {
			InitializeComponent();

			IntPtr iconPtr = TokenIDE.Properties.Resources.icon_hexsprite.GetHicon();
			using (Icon icon = Icon.FromHandle(iconPtr)) {
				this.Icon = icon;
			}

			try {
				performResizeFlag = false;
				resizeFromHex(hex, out w, out h);
				SpriteWidth.Value = w;
				SpriteHeight.Value = h;
				performResizeFlag = true;
			} catch {
				w = h = 8;
			}
			hexBox.Text = hex;
			pixelSize = 10;
			MaintainDim.Checked = true;
		}

		private void Width_ValueChanged(object sender, EventArgs e) {
			if (!performResizeFlag)
				return;
			if (MaintainDim.Checked) {
				int delta = w - (int)SpriteWidth.Value;
				performResizeFlag = false;
				SpriteHeight.Value = h + delta;
				performResizeFlag = true;
			}
			resizeSprite((int)SpriteWidth.Value, (int)SpriteHeight.Value);
		}

		private void Height_ValueChanged(object sender, EventArgs e) {
			if (!performResizeFlag)
				return;
			if (MaintainDim.Checked) {
				int delta = h - (int)SpriteHeight.Value;
				performResizeFlag = false;
				SpriteWidth.Value = w + delta;
				performResizeFlag = true;
			}
				resizeSprite((int)SpriteWidth.Value, (int)SpriteHeight.Value);
		}

		private void resizeFromHex(string hex, out int newW, out int newH) {
			newW = w;
			newH = h;
			// Nearest square
			newW = newH = (int)Math.Ceiling(Math.Sqrt(hex.Length * 4));
			// Byte align
			int wAlign = (int)Math.Round(w / 8.0) * 8;
			if (wAlign != 0) {
				h += newW - wAlign;
				newW = wAlign;
			} else {
				newW = 8;
			}
			// Adjust H to fit
			while (newW * newH / 4 > hex.Length) {
				newH--;
			} 
			while (newW * newH / 4 < hex.Length) {
				newH++;
			}
		}

		private void resizeSprite(int newW, int newH) {
			if (sprite == null || !performResizeFlag)
				return;
			int[,] newSprite = new int[newW, newH];
			for (int i = 0; i < Math.Min(w, newW); i++) {
				for (int j = 0; j < Math.Min(h, newH); j++) {
					newSprite[i, j] = sprite[i, j];
				}
			}
			w = newW;
			h = newH;
			if (ActiveHex.Checked)
				sprite = HexHelper.HexToArr(hexBox.Text, w, h);
			else
				sprite = newSprite;
			spriteBox.Invalidate();
		}

		private void spriteBox_Paint(object sender, PaintEventArgs e) {
			try {
				Graphics g = e.Graphics;
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
				spriteBox.Width = w * pixelSize;
				spriteBox.Height = h * pixelSize;
				for (int j = 0; j < h; j++) {
					for (int i = 0; i < w; i++) {
						if (sprite[i, j] == 1) {
							Rectangle box = new Rectangle(i * pixelSize, j * pixelSize, pixelSize, pixelSize);
							try {
								g.FillRectangle(Brushes.Black, box);
							} catch (Exception ex) {
								MessageBox.Show(ex.ToString(), ex.GetType().ToString());
							}
						}
						if (DrawGrid.Checked) {
							Rectangle grid = new Rectangle(i * pixelSize, j * pixelSize, pixelSize, pixelSize);
							try {
								g.DrawRectangle(Pens.DarkGray, grid);
							} catch (Exception ex) {
								MessageBox.Show(ex.ToString(), ex.GetType().ToString());
							}
						}
					}
				}
				g.FillRectangle(Brushes.PaleVioletRed, new Rectangle(mX, mY, pixelSize, pixelSize));
			} catch (Exception ex) {
				MessageBox.Show(ex.ToString(), ex.GetType().ToString());
			}
		}

		private void updateHex() {
			binBox.Text = "";
			string bin = "";
			for (int j = 0; j < h; j++) {
				string t = "";
				for (int i = 0; i < w; i++) {
					if (i % 8 == 0) {
						t += ",%";
					}
					t += sprite[i, j].ToString();
					bin += sprite[i, j].ToString();
				}
				binBox.Text = string.Concat(binBox.Text, ".db ", t.Substring(1), Environment.NewLine);
			}
			hexBox.Text = HexHelper.BinToHex(bin);
			
		}

		private void spriteBox_MouseMove(object sender, MouseEventArgs e) {
			mX = (int)(e.X / pixelSize) * pixelSize;
			mY = (int)(e.Y / pixelSize) * pixelSize;
			if (mouseDown) {
				if (pixelMode == -1) {
					try { pixelMode = sprite[mX / pixelSize, mY / pixelSize] == 0 ? 1 : 0; } catch { }
				}
				if (pixelMode != -1) {
					try { sprite[mX / pixelSize, mY / pixelSize] = pixelMode; } catch { }
				}
			}
			spriteBox.Invalidate();

			if (mouseDown && ActiveHex.Checked)
				updateHex();
		}

		private void spriteBox_MouseLeave(object sender, EventArgs e) {
			mX = -100;
			mY = -100;
			spriteBox.Invalidate();
		}

		private void spriteBox_Click(object sender, EventArgs e) {
			if (mouseDown) {
				if (pixelMode == -1) {
					try { pixelMode = sprite[mX / pixelSize, mY / pixelSize] == 0 ? 1 : 0; } catch { }
				}
				if (pixelMode != -1) {
					try { sprite[mX / pixelSize, mY / pixelSize] = pixelMode; } catch { }
				}
			}

			if (mouseDown && ActiveHex.Checked) {
				updateHex();
			}
		}

		private void spriteBox_MouseDown(object sender, MouseEventArgs e) {
			if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right) {
				mouseDown = true;
				try { pixelMode = sprite[mX / pixelSize, mY / pixelSize] == 0 ? 1 : 0; } catch { }
			}
		}

		private void spriteBox_MouseUp(object sender, MouseEventArgs e) {
			if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right) {
				mouseDown = false;
				pixelMode = -1;
			}
		}

		private void PixelSize_ValueChanged(object sender, EventArgs e) {
			pixelSize = (int)PixelSize.Value;
			spriteBox.Invalidate();
		}

		private void DrawGrid_CheckedChanged(object sender, EventArgs e) {
			spriteBox.Invalidate();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e) {
			if (ActiveHex.Checked)
				updateHex();
			spriteBox.Invalidate();
		}

		private void hexBox_TextChanged(object sender, EventArgs e) {
			try {
				sprite = HexHelper.HexToArr(hexBox.Text, w, h);
			} catch (Exception ex) {
				MessageBox.Show(ex.ToString());
			}
			toolStripStatusLabel1.Text = "Len: " + hexBox.Text.Length.ToString();
			if (ActiveHex.Checked)
				spriteBox.Invalidate();
		}

		private void ResizeFromHexButton_Click(object sender, EventArgs e) {
			if (hexBox.Text == "")
				return;
			try {
				performResizeFlag = false;
				resizeFromHex(hexBox.Text, out w, out h);
				SpriteWidth.Value = w;
				SpriteHeight.Value = h;
				performResizeFlag = true;
			} catch (Exception ex) {
				MessageBox.Show(ex.ToString());
			}
			spriteBox.Invalidate();
		}

		private void InsertButton_Click(object sender, EventArgs e) {
			outString = hexBox.Text;
		}
	}
}
