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
		int spriteWidth, spriteHeight;
		bool mouseDown;
		int mX, mY;
		int pixelSize;
		bool performResizeFlag = true;
		public string outString = "";

		MouseButtons button;
		int leftPixel = 1;
		int rightPixel = 0;

		List<Color> Colors = new List<Color>() { 
			Color.Cyan, Color.Blue, Color.Red, Color.Black, Color.Magenta, Color.Green, Color.Orange, 
			Color.Brown, Color.Navy, Color.LightBlue, Color.Yellow, Color.White, Color.LightGray, Color.DarkGray, Color.Gray, Color.FromArgb(0x60, 0x60, 0x60),
		};

		List<Brush> BrushList = new List<Brush>();

		public bool IsColor {
			get { return colorCheckBox.Checked; }
			set { colorCheckBox.Checked = value; }
		}

		public bool HasGCharacter {
			get { return hasGBox.Checked; }
			set { hasGBox.Checked = value; }
		}

		public HexSprite(bool color) {
			InitializeComponent();

			IntPtr iconPtr = TokenIDE.Properties.Resources.icon_hexsprite.GetHicon();
			using (Icon icon = Icon.FromHandle(iconPtr)) {
				this.Icon = icon;
			}

			Colors.ForEach(c => BrushList.Add(new SolidBrush(c)));
			IsColor = color;

			spriteWidth = 8;
			spriteHeight = 8;
			sprite = new int[spriteWidth, spriteHeight];
			pixelSize = 10;
			updateHex();
		}

		public HexSprite(string hex, bool color) {
			InitializeComponent();

			IsColor = color;

			HasGCharacter = hex.Contains('G');

			IntPtr iconPtr = TokenIDE.Properties.Resources.icon_hexsprite.GetHicon();
			using (Icon icon = Icon.FromHandle(iconPtr)) {
				this.Icon = icon;
			}

			Colors.ForEach(c => BrushList.Add(new SolidBrush(c)));
			try {
				performResizeFlag = false;
				resizeFromHex(hex, out spriteWidth, out spriteHeight);
				SpriteWidth.Value = spriteWidth;
				SpriteHeight.Value = spriteHeight;
				performResizeFlag = true;
			} catch {
				spriteWidth = spriteHeight = 8;
			}
			hexBox.Text = hex;
			pixelSize = 10;
			MaintainDim.Checked = true;
		}

		private void Width_ValueChanged(object sender, EventArgs e) {
			if (!performResizeFlag)
				return;
			if (MaintainDim.Checked) {
				int delta = spriteWidth - (int)SpriteWidth.Value;
				performResizeFlag = false;
				SpriteHeight.Value = spriteHeight + delta;
				performResizeFlag = true;
			}
			resizeSprite((int)SpriteWidth.Value, (int)SpriteHeight.Value);
		}

		private void Height_ValueChanged(object sender, EventArgs e) {
			if (!performResizeFlag)
				return;
			if (MaintainDim.Checked) {
				int delta = spriteHeight - (int)SpriteHeight.Value;
				performResizeFlag = false;
				SpriteWidth.Value = spriteWidth + delta;
				performResizeFlag = true;
			}
			
			resizeSprite((int)SpriteWidth.Value, (int)SpriteHeight.Value);
		}

		private void resizeFromHex(string hex, out int newW, out int newH) {
			if (IsColor) {
				newW = newH = (int)Math.Ceiling(Math.Sqrt(hex.Length));
				return;
			}
			newW = spriteWidth;
			newH = spriteHeight;
			// Nearest square
			newW = newH = (int)Math.Ceiling(Math.Sqrt(hex.Length * 4));
			// Byte align
			int wAlign = (int)Math.Round(spriteWidth / 8.0) * 8;
			if (wAlign != 0) {
				spriteHeight += newW - wAlign;
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
			for (int i = 0; i < Math.Min(spriteWidth, newW); i++) {
				for (int j = 0; j < Math.Min(spriteHeight, newH); j++) {
					newSprite[i, j] = sprite[i, j];
				}
			}
			spriteWidth = newW;
			spriteHeight = newH;
			if (ActiveHex.Checked) {
				if (IsColor) {
					sprite = HexHelper.HexToArr(hexBox.Text, spriteWidth, spriteHeight);
				} else {
					sprite = HexHelper.HexBinToArr(hexBox.Text, spriteWidth, spriteHeight);
				}
			} else {
				sprite = newSprite;
			}
			spriteBox.Invalidate();
		}

		private void spriteBox_Paint(object sender, PaintEventArgs e) {
#if !DEBUG
			try {
#endif
				Graphics g = e.Graphics;
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
				spriteBox.Width = spriteWidth * pixelSize;
				spriteBox.Height = spriteHeight * pixelSize;
				for (int j = 0; j < spriteHeight; j++) {
					for (int i = 0; i < spriteWidth; i++) {
						//if (sprite[i, j] == 1) {
						Rectangle box = new Rectangle(i * pixelSize, j * pixelSize, pixelSize, pixelSize);
#if !DEBUG
						try {
#endif
							int paletteIndex = sprite[i, j];
							if (IsColor) {
								g.FillRectangle(BrushList[paletteIndex], box);
							} else {
								g.FillRectangle(paletteIndex == 0 ? Brushes.White : Brushes.Black, box);
							}
#if !DEBUG
						} catch (Exception ex) {
							MessageBox.Show(ex.ToString(), ex.GetType().ToString());
						}
#endif
						//}
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
#if !DEBUG
			} catch (Exception ex) {
				MessageBox.Show(ex.ToString(), ex.GetType().ToString());
			}
#endif
		}

		private void updateHex() {
			binBox.Text = "";
			StringBuilder bin = new StringBuilder();
			for (int j = 0; j < spriteHeight; j++) {
				string t = "";
				StringBuilder line = new StringBuilder();
				for (int i = 0; i < spriteWidth; i++) {
					if (i % 8 == 0) {
						t += ",%";
					}
					t += sprite[i, j].ToString();
					if (IsColor) {
						line.Append(sprite[i, j].ToString("X1"));
					} else {
						line.Append(sprite[i, j].ToString());
					}
				}

				if (!IsColor) {
					line = new StringBuilder(HexHelper.BinToHex(line.ToString()));
				}
				// Try to backtrack and add G
				if (HasGCharacter) {
					int lineWidth = line.Length;
					if (line[lineWidth - 1] == '0') {
						int zeroCount = 1;
						for (int k = lineWidth - 2; k >= 0; k--) {
							if (line[k] == '0') {
								zeroCount++;
							} else {
								break;
							}
						}

						if (zeroCount > 1) {
							line.Remove(lineWidth - zeroCount, zeroCount);
							line.Append('G');
						}
					}
				}

				bin.Append(line);
				binBox.Text = string.Concat(binBox.Text, ".db ", t.Substring(1), Environment.NewLine);
			}

			//if (IsColor) {
			hexBox.Text = bin.ToString();
			//} else {
			//	hexBox.Text = HexHelper.BinToHex(bin.ToString());
			//}
		}

		private void spriteBox_MouseMove(object sender, MouseEventArgs e) {
			mX = (int)(e.X / pixelSize) * pixelSize;
			mY = (int)(e.Y / pixelSize) * pixelSize;
			if (mouseDown) {
				//if (pixelMode == -1) {
				//    try { pixelMode = sprite[mX / pixelSize, mY / pixelSize] == 0 ? 1 : 0; } catch { }
				//} else {
				//    try { sprite[mX / pixelSize, mY / pixelSize] = pixelMode; } catch { }
				//}
				try { sprite[mX / pixelSize, mY / pixelSize] = button == System.Windows.Forms.MouseButtons.Left ? leftPixel : rightPixel; } catch { }
			}
			spriteBox.Invalidate();

			if (mouseDown && ActiveHex.Checked) {
				updateHex();
			}
		}

		private void spriteBox_MouseLeave(object sender, EventArgs e) {
			mX = -100;
			mY = -100;
			spriteBox.Invalidate();
		}

		private void spriteBox_Click(object sender, EventArgs e) {
			if (mouseDown) {
				//if (pixelMode == -1) {
				//    try { pixelMode = sprite[mX / pixelSize, mY / pixelSize] == 0 ? 1 : 0; } catch { }
				//}
				//if (pixelMode != -1) {
				//    try { sprite[mX / pixelSize, mY / pixelSize] = pixelMode; } catch { }
				//}
				try { sprite[mX / pixelSize, mY / pixelSize] = button == System.Windows.Forms.MouseButtons.Left ? leftPixel : rightPixel; } catch { }
			}

			if (mouseDown && ActiveHex.Checked) {
				updateHex();
			}
		}

		private void spriteBox_MouseDown(object sender, MouseEventArgs e) {
			if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right) {
				mouseDown = true;
				button = e.Button;
				//try { pixelMode = sprite[mX / pixelSize, mY / pixelSize] == 0 ? 1 : 0; } catch { }
			}
		}

		private void spriteBox_MouseUp(object sender, MouseEventArgs e) {
			if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right) {
				mouseDown = false;
				//pixelMode = -1;
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
#if !DEBUG
			try {
#endif
				if (IsColor) {
					sprite = HexHelper.HexToArr(hexBox.Text, spriteWidth, spriteHeight);
				} else {
					sprite = HexHelper.HexBinToArr(hexBox.Text, spriteWidth, spriteHeight);
				}
#if !DEBUG
			} catch (Exception ex) {
				MessageBox.Show(ex.ToString());
			}
#endif
			toolStripStatusLabel1.Text = "Len: " + hexBox.Text.Length.ToString();
			if (ActiveHex.Checked) {
				spriteBox.Invalidate();
			}
		}

		private void ResizeFromHexButton_Click(object sender, EventArgs e) {
			if (hexBox.Text == "")
				return;
			try {
				performResizeFlag = false;
				resizeFromHex(hexBox.Text, out spriteWidth, out spriteHeight);
				SpriteWidth.Value = spriteWidth;
				SpriteHeight.Value = spriteHeight;
				performResizeFlag = true;
			} catch (Exception ex) {
				MessageBox.Show(ex.ToString());
			}
			spriteBox.Invalidate();
		}

		private void InsertButton_Click(object sender, EventArgs e) {
			outString = hexBox.Text;
		}

		private void paletteBox_Paint(object sender, PaintEventArgs e) {
			Graphics g = e.Graphics;
			const int boxWidth = 27;
			const int boxHeight = 34;

			int paletteX = 0;
			int paletteY = 0;
			for (int colorIndex = 0; colorIndex < Colors.Count; colorIndex++) {
				Color c = Colors[colorIndex];
				g.FillRect(BrushList[colorIndex], paletteX, paletteY, paletteX + boxWidth, paletteY + boxHeight);
				g.DrawRect(Pens.Black, paletteX, paletteY, paletteX + boxWidth, paletteY + boxHeight);

				if (colorIndex == leftPixel) {
					using (Font newFont = new Font(FontFamily.GenericSansSerif, 7)) {
						g.DrawString("L", newFont, c.GetBrightness() <= 0.5f ? Brushes.White : Brushes.Black, paletteX + 5, paletteY + 20);
					}
				} 
				if (colorIndex == rightPixel) {
					using (Font newFont = new Font(FontFamily.GenericSansSerif, 7)) {
						g.DrawString("R", newFont, c.GetBrightness() <= 0.5f ? Brushes.White : Brushes.Black, paletteX + 12, paletteY + 20);
					}
				}

				paletteX += boxWidth;
				if (paletteX >= paletteBox.Width) {
					paletteX = 0;
					paletteY += boxHeight;
				}
			}
		}

		private void colorCheckBox_CheckedChanged(object sender, EventArgs e) {
			paletteBox.Visible = colorCheckBox.Checked;

			// Make all numbers 1 if it's black and white
			if (!IsColor) {
				for (int j = 0; j < spriteHeight; j++) {
					for (int i = 0; i < spriteWidth; i++) {
						if (sprite[i, j] == rightPixel) {
							sprite[i, j] = 0;
						} else {
							sprite[i, j] = 1;
						}
					}
				}
			}

			leftPixel = 1;
			rightPixel = 0;

			spriteBox.Invalidate();
			updateHex();
		}

		private void paletteBox_MouseClick(object sender, MouseEventArgs e) {
			int paletteIndex = (e.X / 27) + 8*(e.Y / 34);
			
			if (e.Button == System.Windows.Forms.MouseButtons.Left) {
				leftPixel = paletteIndex;
			} else if (e.Button == System.Windows.Forms.MouseButtons.Right) {
				rightPixel = paletteIndex;
			}

			paletteBox.Invalidate();
		}

		private void openImageToolStripMenuItem_Click(object sender, EventArgs e) {
			OpenFileDialog f = new OpenFileDialog();
			if (f.ShowDialog() != System.Windows.Forms.DialogResult.OK) { return; }
			Open(f.FileName);
		}

		private void Open(string fileName) {
			using (Bitmap image = new Bitmap(fileName)) {
				SpriteWidth.Value = image.Width;
				SpriteHeight.Value = image.Height;
				sprite = image.PalettizeImage(Colors, 0);
			}

			updateHex();
			spriteBox.Invalidate();
		}

		private void hasGBox_CheckedChanged(object sender, EventArgs e) {
			updateHex();
		}
	}
}
