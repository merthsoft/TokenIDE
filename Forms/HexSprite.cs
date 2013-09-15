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
using Merthsoft.TokenIDE.Properties;
using System.Threading.Tasks;

namespace Merthsoft.TokenIDE {
	public partial class HexSprite : Form {
		enum Tool { Pencil, Pen, Flood, Line, Rectangle, RectangleFill, Ellipse, EllipseFill, Circle, CircleFill, EyeDropper, _max, }

		Tool currentTool = Tool.Pencil;
		ToolStripButton currentButton = null;

		int mouseX, mouseY;
		int mouseXOld, mouseYOld;
		MouseButtons button;
		bool drawing;
		int shapeX, shapeY;
		int penWidth {
			get { return (int)penWidthBox.Value; }
			set { penWidthBox.Value = value; }
		}

		//List<int[,]> history;
		List<Sprite> history;
		int historyPosition;

		//int[,] sprite;
		//int[,] previewSprite = null;
		Sprite sprite;
		Sprite previewSprite = null;

		int spriteWidth, spriteHeight;
		int pixelSize;
		bool performResizeFlag = true;
		public string outString = "";

		int leftPixel = 1;
		int rightPixel = 0;

		List<Color> Colors = new List<Color>() { 
			Color.Cyan, Color.Blue, Color.Red, Color.Black, Color.Magenta, Color.Green, Color.Orange, Color.Brown, 
			Color.Navy, Color.LightBlue, Color.Yellow, Color.White, Color.LightGray, Color.DarkGray, Color.Gray, Color.FromArgb(0x60, 0x60, 0x60),
		};

		List<Brush> BrushList = new List<Brush>();
		List<Pen> PenList = new List<Pen>();

		public bool IsColor {
			get { return colorCheckBox.Checked; }
			set { colorCheckBox.Checked = value; }
		}

		public bool HasGCharacter {
			get { return hasGBox.Checked; }
			set { hasGBox.Checked = value; }
		}

		public HexSprite() {
			InitializeComponent();

			//history = new List<int[,]>();
			history = new List<Sprite>();
			historyPosition = 0;

			IntPtr iconPtr = TokenIDE.Properties.Resources.icon_hexsprite.GetHicon();
			using (Icon icon = Icon.FromHandle(iconPtr)) {
				this.Icon = icon;
			}

			Colors.ForEach(c => {
				SolidBrush brush = new SolidBrush(c);
				BrushList.Add(brush);
				PenList.Add(new Pen(brush));
			});

			for (int i = 0; i < (int)Tool._max; i++) {
				ToolStripButton toolButton = new ToolStripButton();
				string toolName = ((Tool)i).ToString();
				toolButton.Text = toolName;
				toolButton.Image = (Image)Resources.ResourceManager.GetObject("icon_" + toolName.ToLower());
				toolButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
				if (toolButton.Image == null) {
					toolButton.Image = (Image)Resources.ResourceManager.GetObject("icon_blank");
					toolButton.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
				}
				toolButton.CheckOnClick = true;
				toolButton.Click += new EventHandler(toolButton_Click);
				toolButton.ImageScaling = ToolStripItemImageScaling.None;
				toolButton.Tag = (Tool)i;

				mainToolStrip.Items.Add(toolButton);

				if ((Tool)i == Tool.Pencil) {
					toolButton.Checked = true;
					currentButton = toolButton;
				}
			}
		}

		void toolButton_Click(object sender, EventArgs e) {
			ToolStripButton button = (ToolStripButton)sender;
			currentButton.Checked = false;
			button.Checked = true;
			currentButton = button;
			currentTool = (Tool)button.Tag;
		}

		public HexSprite(bool color)
			: this() {
			IsColor = color;

			spriteWidth = 8;
			spriteHeight = 8;
			//sprite = new int[spriteWidth, spriteHeight];
			sprite = new Sprite(spriteWidth, spriteHeight);
			pixelSize = 10;
			updateHex();

			if (IsColor) {
				spriteWidthBox.Value = 32;
				spriteHeightBox.Value = 32;
				pixelSizeBox.Value = 8;
			}
		}

		public HexSprite(string hex, bool color) : this() {
			IsColor = color;

			HasGCharacter = hex.Contains('G');

			try {
				performResizeFlag = false;
				resizeFromHex(hex, out spriteWidth, out spriteHeight);
				spriteWidthBox.Value = spriteWidth;
				spriteHeightBox.Value = spriteHeight;
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
				int delta = spriteWidth - (int)spriteWidthBox.Value;
				performResizeFlag = false;
				spriteHeightBox.Value = spriteHeight + delta;
				performResizeFlag = true;
			}
			resizeSprite((int)spriteWidthBox.Value, (int)spriteHeightBox.Value);
		}

		private void Height_ValueChanged(object sender, EventArgs e) {
			if (!performResizeFlag)
				return;
			if (MaintainDim.Checked) {
				int delta = spriteHeight - (int)spriteHeightBox.Value;
				performResizeFlag = false;
				spriteWidthBox.Value = spriteWidth + delta;
				performResizeFlag = true;
			}
			
			resizeSprite((int)spriteWidthBox.Value, (int)spriteHeightBox.Value);
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
			//int[,] newSprite = new int[newW, newH];
			Sprite newSprite = new Sprite(newW, newH);
			for (int i = 0; i < Math.Min(spriteWidth, newW); i++) {
				for (int j = 0; j < Math.Min(spriteHeight, newH); j++) {
					newSprite[i, j] = sprite[i, j];
				}
			}
			spriteWidth = newW;
			spriteHeight = newH;
			if (ActiveHex.Checked) {
				if (IsColor) {
					//sprite = HexHelper.HexToArr(hexBox.Text, spriteWidth, spriteHeight);
					sprite = new Sprite(hexBox.Text, spriteWidth, spriteHeight, Colors.Count / 4);
				} else {
					//sprite = HexHelper.HexBinToArr(hexBox.Text, spriteWidth, spriteHeight);
					sprite = new Sprite(hexBox.Text, spriteWidth, spriteHeight, 1);
				}
			} else {
				sprite = newSprite;
			}
			spriteBox.Invalidate();
		}

		private void spriteBox_Paint(object sender, PaintEventArgs e) {
			Graphics g = e.Graphics;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
			spriteBox.Width = spriteWidth * pixelSize;
			spriteBox.Height = spriteHeight * pixelSize;

			//int[,] spriteToUse = previewSprite ?? ;
			drawSprite(g, sprite);
			if (previewSprite != null) { drawSprite(g, previewSprite); }

			//g.FillRectangle(Brushes.PaleVioletRed, new Rectangle(mouseX, mouseY, pixelSize, pixelSize));
		}

		//private void drawSprite(Graphics g, int[,] spriteToUse) {
		private void drawSprite(Graphics g, Sprite spriteToUse) {
#if !DEBUG
			try {
#endif
			for (int j = 0; j < spriteHeight; j++) {
				for (int i = 0; i < spriteWidth; i++) {
					//if (sprite[i, j] == 1) {
					Rectangle box = new Rectangle(i * pixelSize, j * pixelSize, pixelSize, pixelSize);
#if !DEBUG
						try {
#endif
					int paletteIndex = spriteToUse[i, j];
					if (paletteIndex == -1) { continue; }
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

		private void handleMouse(MouseEventArgs e) {
			button = e.Button;
			mouseXOld = mouseX;
			mouseYOld = mouseY;
			mouseX = e.X / pixelSize;
			mouseY = e.Y / pixelSize;

			int pixelColor = -1;

			switch (button) {
				case MouseButtons.Left:
					pixelColor = leftPixel;
					break;
				case MouseButtons.Right:
					pixelColor = rightPixel;
					break;
			}

			switch (currentTool) {
				case Tool.Pencil:
					if (button != System.Windows.Forms.MouseButtons.None) {
						sprite.Plot(mouseX, mouseY, pixelColor, penWidth);
					}
					break;
				case Tool.Pen:
					if (button != System.Windows.Forms.MouseButtons.None) {
						sprite.DrawLine(mouseXOld, mouseYOld, mouseX, mouseY, pixelColor, penWidth);
					}
					break;
				case Tool.Flood:
					if (button != System.Windows.Forms.MouseButtons.None) {
						sprite.FloodFill(mouseX, mouseY, pixelColor);
					}
					break;
				case Tool.Line:
					if (!drawing) {
						shapeX = mouseX;
						shapeY = mouseY;
						drawing = true;
					}
					if (button == System.Windows.Forms.MouseButtons.None) {
						copyPreviewSprite();
						previewSprite = null;
						drawing = false;
					} else {
						createPreviewSprite();
						previewSprite.DrawLine(shapeX, shapeY, mouseX, mouseY, pixelColor, penWidth);
					}
					break;
				case Tool.Rectangle:
				case Tool.RectangleFill:
					if (!drawing) {
						shapeX = mouseX;
						shapeY = mouseY;
						drawing = true;
					}
					if (button == System.Windows.Forms.MouseButtons.None) {
						copyPreviewSprite();
						previewSprite = null;
						drawing = false;
					} else {
						createPreviewSprite();
						previewSprite.DrawRectangle(shapeX, shapeY, mouseX, mouseY, pixelColor, penWidth, currentTool == Tool.RectangleFill);
					}
					break;
				case Tool.Ellipse:
				case Tool.EllipseFill:
					if (!drawing) {
						shapeX = mouseX;
						shapeY = mouseY;
						drawing = true;
					}
					if (button == System.Windows.Forms.MouseButtons.None) {
						copyPreviewSprite();
						previewSprite = null;
						drawing = false;
					} else {
						createPreviewSprite();
						previewSprite.DrawEllipse(shapeX, shapeY, mouseX, mouseY, pixelColor, penWidth, currentTool == Tool.EllipseFill);
					}
					break;
				case Tool.Circle:
				case Tool.CircleFill:
					if (!drawing) {
						shapeX = mouseX;
						shapeY = mouseY;
						drawing = true;
					}
					if (button == System.Windows.Forms.MouseButtons.None) {
						copyPreviewSprite();
						previewSprite = null;
						drawing = false;
					} else {
						createPreviewSprite();
						int radius = (int)Math.Sqrt((shapeX - mouseX) * (shapeX - mouseX) + (shapeY - mouseY) * (shapeY - mouseY));
						previewSprite.DrawCircle(shapeX, shapeY, radius, pixelColor, penWidth, currentTool == Tool.CircleFill);
					}
					break;
				case Tool.EyeDropper:
					if (IsColor) {
						if (button == System.Windows.Forms.MouseButtons.Left) {
							leftPixel = sprite[mouseX, mouseY];
						} else if (button == System.Windows.Forms.MouseButtons.Right) {
							rightPixel = sprite[mouseX, mouseY];
						}
						paletteBox.Invalidate();
					}
					break;
				default:
					break;
			}

			spriteBox.Invalidate();

			if (ActiveHex.Checked) {
				updateHex();
			}
		}

		private void createPreviewSprite() {
			//previewSprite = new int[spriteWidth, spriteHeight];
			previewSprite = new Sprite(spriteWidth, spriteHeight);
			for (int j = 0; j < spriteHeight; j++) {
				for (int i = 0; i < spriteWidth; i++) {
					previewSprite[i, j] = -1;
				}
			}
		}

		private void copyPreviewSprite() {
			for (int j = 0; j < spriteHeight; j++) {
				for (int i = 0; i < spriteWidth; i++) {
					if (previewSprite[i, j] != -1) {
						sprite[i, j] = previewSprite[i, j];
					}
				}
			}
		}

		private void spriteBox_MouseMove(object sender, MouseEventArgs e) {
			if (e.Button != MouseButtons.None)
				handleMouse(e);
		}

		private void spriteBox_MouseLeave(object sender, EventArgs e) {
			spriteBox.Invalidate();
		}

		private void spriteBox_MouseDown(object sender, MouseEventArgs e) {
			pushHistory();

			mouseX = e.X / pixelSize;
			mouseY = e.Y / pixelSize;
			handleMouse(e);
		}

		private void pushHistory() {
			if (historyPosition != history.Count) {
				history.RemoveRange(historyPosition, history.Count - historyPosition);
			}
			history.Add(sprite.Copy());
			historyPosition = history.Count;
			toggleRedo(false);
			toggleUndo(true);
		}

		//private int[,] copySpriteArray() {
		//    int[,] newSprite = new int[spriteWidth, spriteHeight];
		//    Array.Copy(sprite, newSprite, sprite.Length);

		//    return newSprite;
		//}

		private void spriteBox_MouseUp(object sender, MouseEventArgs e) {
			//if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right) {
			//    mouseDown = false;
			//    //pixelMode = -1;
			//}

			MouseEventArgs ne = new MouseEventArgs(MouseButtons.None, e.Clicks, e.X, e.Y, e.Delta);
			handleMouse(ne);
		}

		private void PixelSize_ValueChanged(object sender, EventArgs e) {
			pixelSize = (int)pixelSizeBox.Value;
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
					//sprite = HexHelper.HexToArr(hexBox.Text, spriteWidth, spriteHeight);
					sprite = new Sprite(hexBox.Text, spriteWidth, spriteHeight, Colors.Count / 4);
				} else {
					//sprite = HexHelper.HexBinToArr(hexBox.Text, spriteWidth, spriteHeight);
					sprite = new Sprite(hexBox.Text, spriteWidth, spriteHeight, 1);
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
				spriteWidthBox.Value = spriteWidth;
				spriteHeightBox.Value = spriteHeight;
				performResizeFlag = true;
			} catch (Exception ex) {
				MessageBox.Show(ex.ToString());
			}
			spriteBox.Invalidate();
		}

		private void InsertButton_Click(object sender, EventArgs e) {
			outString = hexBox.Text;
			Close();
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
						g.DrawString("L", newFont, c.GetBrightness() < 0.5f ? Brushes.White : Brushes.Black, paletteX + 5, paletteY + 20);
					}
				} 
				if (colorIndex == rightPixel) {
					using (Font newFont = new Font(FontFamily.GenericSansSerif, 7)) {
						g.DrawString("R", newFont, c.GetBrightness() < 0.5f ? Brushes.White : Brushes.Black, paletteX + 12, paletteY + 20);
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
		
		private void Open(string fileName) {
			pushHistory();
			using (Bitmap image = new Bitmap(fileName)) {
				spriteWidthBox.Value = image.Width;
				spriteHeightBox.Value = image.Height;
				sprite = new Sprite(image.PalettizeImage(Colors, 0));
			}

			updateHex();
			spriteBox.Invalidate();
		}

		private void hasGBox_CheckedChanged(object sender, EventArgs e) {
			updateHex();
		}

		private void openToolStripButton_Click(object sender, EventArgs e) {
			OpenFileDialog f = new OpenFileDialog();
			if (f.ShowDialog() != System.Windows.Forms.DialogResult.OK) { return; }
			Open(f.FileName);
		}

		private void undoButton_Click(object sender, EventArgs e) {
			undo();
		}

		private void redoButton_Click(object sender, EventArgs e) {
			redo();
		}

		private void undo() {
			if (historyPosition == history.Count) {
				//history.Add(copySpriteArray());
				history.Add(sprite.Copy());
			}

			sprite = history[--historyPosition];
			if (historyPosition == 0) {
				toggleUndo(false);
			}
			toggleRedo(true);

			spriteBox.Invalidate();
			if (ActiveHex.Checked) {
				updateHex();
			}
		}

		private void redo() {
			sprite = history[++historyPosition];
			if (historyPosition + 1 == history.Count) {
				toggleRedo(false);
			}
			toggleUndo(true);
			
			spriteBox.Invalidate();
			if (ActiveHex.Checked) {
				updateHex();
			}
		}

		private void toggleUndo(bool enabled) {
			undoButton.Enabled = enabled;
			undoToolStripMenuItem.Enabled = enabled;
		}

		private void toggleRedo(bool enabled) {
			redoButton.Enabled = enabled;
			redoToolStripMenuItem.Enabled = enabled;
		}
	}
}
