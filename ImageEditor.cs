using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Merthsoft.Tokens;
using System.Drawing.Drawing2D;
using Merthsoft.CalcData;
using Merthsoft.Extensions;

namespace Merthsoft.TokenIDE {
	public partial class ImageEditor:Form {
		enum Tools { Pencil, Pen, Flood, Line, Rectangle, RectangleFill, Ellipse, EllipseFill, Circle, CircleFill, _max }

		Tools t = Tools.Pencil;
		Bitmap p = new Bitmap(96, 64);
		int screenWidth = 96;
		int screenHeight = 64;
		int zoom = 4;
		int penWidth = 1;
		int mouseX, mouseY;
		int mouseXOld, mouseYOld;
		MouseButtons mouseButton;
		bool drawing;
		int shapeX, shapeY;
		Pen shapePen;

		List<Bitmap> history;
		int historyPosition;

		public string[] OutStrings;

		public ImageEditor() {
			InitializeComponent();

			IntPtr iconPtr = TokenIDE.Properties.Resources.icon_pic.GetHicon();
			using (Icon icon = Icon.FromHandle(iconPtr)) {
				this.Icon = icon;
			}

			canvas.Width = screenWidth * zoom;
			canvas.Height = screenHeight * zoom;
			history = new List<Bitmap>();
			historyPosition = 0;
			for (Tools i = 0; i < Tools._max; i++) {
				toolBox.Items.Add(i.ToString());
			}
			for (int i = 1; i < 256; i++) {
				picNumberBox.Items.Add("Pic" + i);
			}
			picNumberBox.SelectedIndex = 0;

			HexOrBinBox.SelectedIndex = 0;
		}

		private void canvas_Paint(object sender, PaintEventArgs e) {
			Bitmap b = new Bitmap(screenWidth, screenHeight);
			Graphics g = Graphics.FromImage(b);
			g.DrawImage(p, 0, 0, screenWidth, screenHeight);
			switch (t) {
				case Tools.Line:
					g.DrawLine(shapePen, shapeX, shapeY, mouseX, mouseY);
					break;
				case Tools.Rectangle:
					g.DrawRect(shapePen, shapeX, shapeY, mouseX, mouseY);
					break;
				case Tools.RectangleFill:
					g.FillRect(shapePen.Brush, shapeX, shapeY, mouseX, mouseY);
					break;
				case Tools.Ellipse:
					g.DrawEllipse(shapePen, shapeX, shapeY, mouseX - shapeX, mouseY - shapeY);
					break;
				case Tools.EllipseFill:
					g.FillEllipse(shapePen.Brush, shapeX, shapeY, mouseX - shapeX, mouseY - shapeY);
					break;
				case Tools.Circle:
					g.DrawCircle(shapePen, shapeX, shapeY, (int)Math.Sqrt((shapeX - mouseX) * (shapeX - mouseX) + (shapeY - mouseY) * (shapeY - mouseY)));
					break;
				case Tools.CircleFill:
					g.FillCircle(shapePen.Brush, shapeX, shapeY, (int)Math.Sqrt((shapeX - mouseX) * (shapeX - mouseX) + (shapeY - mouseY) * (shapeY - mouseY)));
					break;
			}
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			e.Graphics.DrawImage(b, 0, 0, screenWidth * zoom, screenHeight * zoom);
		}

		private void loadToolStripMenuItem_Click(object sender, EventArgs e) {
			OpenFileDialog f = new OpenFileDialog();
			f.ShowDialog();
			Open(f.FileName);
		}

		private void Open(string fileName) {
			if (File.Exists(fileName)) {
				FileInfo fi = new FileInfo(fileName);
				if (fi.Extension.ToLower() == ".8xi") {
					using (FileStream fs = new FileStream(fileName, FileMode.Open)) {
						using (BinaryReader b = new BinaryReader(fs)) {
							//Pic8x pic = (Pic8x)Var8x.FromBinaryReader(b);
							Pic8x pic = new Pic8x(b);
							p = pic.GetBitmap() ?? p;
						}
					}
				} else if (fi.Extension.ToLower() == ".bmp" || fi.Extension.ToLower() == ".png" ||
						   fi.Extension.ToLower() == ".jpg" || fi.Extension.ToLower() == ".jpeg" ||
						   fi.Extension.ToLower() == ".gif") {
					using (Bitmap a = new Bitmap(fileName)) {
						CropImageDialog cid = new CropImageDialog(a);
						cid.ShowDialog();
						p = cid.outMap ?? p;
						canvas.Invalidate();
					}
				}
			}
			canvas.Invalidate();
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
			zoom = (int)numericUpDown1.Value;
			canvas.Width = screenWidth * zoom;
			canvas.Height = screenHeight * zoom;
		}

		private void handleMouse(MouseEventArgs e) {
			mouseButton = e.Button;
			mouseXOld = mouseX;
			mouseYOld = mouseY;
			mouseX = e.X / zoom;
			mouseY = e.Y / zoom;
			Pen pen;
			switch (mouseButton) {
				case MouseButtons.Left:
					pen = new Pen(Color.Black, penWidth);
					break;
				case MouseButtons.Right:
					pen = new Pen(Color.White, penWidth);
					break;
				case MouseButtons.None:
				default:
					pen = Pens.Transparent;
					break;
			}
			Graphics g = Graphics.FromImage(p);
			switch (t) {
				case Tools.Pencil:
					g.FillRectangle(pen.Brush, mouseX, mouseY, penWidth, penWidth);
					break;
				case Tools.Pen:
					g.DrawLine(pen, mouseXOld, mouseYOld, mouseX, mouseY);
					break;
				case Tools.Flood:
					if (mouseButton == System.Windows.Forms.MouseButtons.None)
						return;
					p.FloodFill(pen, mouseX, mouseY);
					break;
				case Tools.Line:
					if (!drawing) {
						shapeX = mouseX;
						shapeY = mouseY;
						drawing = true;
						shapePen = pen;
					}
					if (mouseButton == System.Windows.Forms.MouseButtons.None) {
						g.DrawLine(shapePen, shapeX, shapeY, mouseX, mouseY);
						drawing = false;
					}
					break;
				case Tools.Rectangle:
				case Tools.RectangleFill:
					if (!drawing) {
						shapeX = mouseX;
						shapeY = mouseY;
						drawing = true;
						shapePen = pen;
					}
					if (mouseButton == System.Windows.Forms.MouseButtons.None) {
						if (t == Tools.RectangleFill) {
							g.FillRect(shapePen.Brush, shapeX, shapeY, mouseX, mouseY);
						} else {
							g.DrawRect(shapePen, shapeX, shapeY, mouseX, mouseY);
						}
						drawing = false;
					}
					break;
				case Tools.Ellipse:
				case Tools.EllipseFill:
					if (!drawing) {
						shapeX = mouseX;
						shapeY = mouseY;
						drawing = true;
						shapePen = pen;
					}
					if (mouseButton == System.Windows.Forms.MouseButtons.None) {
						if (t == Tools.EllipseFill) {
							g.FillEllipse(shapePen.Brush, shapeX, shapeY, mouseX - shapeX, mouseY - shapeY);
						} else {
							g.DrawEllipse(shapePen, shapeX, shapeY, mouseX - shapeX, mouseY - shapeY);
						}
						drawing = false;
					}
					break;
				case Tools.Circle:
				case Tools.CircleFill:
					if (!drawing) {
						shapeX = mouseX;
						shapeY = mouseY;
						drawing = true;
						shapePen = pen;
					}
					if (mouseButton == System.Windows.Forms.MouseButtons.None) {
						if (t == Tools.CircleFill) {
							g.FillCircle(shapePen.Brush, shapeX, shapeY, (int)Math.Sqrt((shapeX - mouseX) * (shapeX - mouseX) + (shapeY - mouseY) * (shapeY - mouseY)));
						} else {
							g.DrawCircle(shapePen, shapeX, shapeY, (int)Math.Sqrt((shapeX - mouseX) * (shapeX - mouseX) + (shapeY - mouseY) * (shapeY - mouseY)));
						}
						drawing = false;
					}
					break;
				default:
					break;
			}

			canvas.Invalidate();
		}

		private void canvas_MouseDown(object sender, MouseEventArgs e) {
			if (historyPosition != history.Count) {
				history.RemoveRange(historyPosition, history.Count - historyPosition);
			}
			history.Add(new Bitmap(p));
			historyPosition = history.Count;
			redoToolStripMenuItem.Enabled = false;
			undoToolStripMenuItem.Enabled = true;

			mouseX = e.X / zoom;
			mouseY = e.Y / zoom;
			handleMouse(e);
		}

		private void canvas_MouseUp(object sender, MouseEventArgs e) {
			MouseEventArgs ne = new MouseEventArgs(MouseButtons.None, e.Clicks, e.X, e.Y, e.Delta);
			handleMouse(ne);
		}

		private void canvas_MouseMove(object sender, MouseEventArgs e) {
			if (e.Button != MouseButtons.None)
				handleMouse(e);
			mouseCoordLabel.Text = string.Format("Mouse: ({0}, {1})", e.X / zoom, e.Y / zoom);
		}

		private void toolBox_SelectedIndexChanged(object sender, EventArgs e) {
			if (toolBox.SelectedIndices.Count != 0)
				t = (Tools)toolBox.SelectedIndices[0];
		}

		private void undoToolStripMenuItem_Click(object sender, EventArgs e) {
			if (historyPosition == history.Count)
				history.Add(new Bitmap(p));
			Bitmap a = new Bitmap(history[--historyPosition]);
			if (historyPosition == 0) {
				undoToolStripMenuItem.Enabled = false;
			}
			redoToolStripMenuItem.Enabled = true;
			p = a;

			canvas.Invalidate();
		}

		private void numericUpDown2_ValueChanged(object sender, EventArgs e) {
			penWidth = (int)numericUpDown2.Value;
		}

		private void redoToolStripMenuItem_Click(object sender, EventArgs e) {
			Bitmap a = new Bitmap(history[++historyPosition]);
			//history[historyPosition] = new Bitmap(p);
			if (historyPosition + 1 == history.Count) {
				redoToolStripMenuItem.Enabled = false;
			}
			undoToolStripMenuItem.Enabled = true;
			p = a;

			canvas.Invalidate();
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
			byte picNum = byte.Parse(picNumberBox.SelectedItem.ToString().Remove(0, 3));//picNumberBox.SelectedItem.ToString().Replace;
			Pic8x v = new Pic8x((byte)(picNum - 1));
			byte[] data = new byte[768];
			byte b = 0;
			int index = 0;
			int bit = 7;
			for (int j = 0; j < 64; j++) {
				for (int i = 0; i < 96; i++) {
					int val = p.GetPixel(i, j).ToArgb().Equals(Color.Black.ToArgb()) ? 1 : 0;
					b |= (byte)(val << bit--);
					if (bit == -1) {
						bit = 7;
						data[index++] = b;
						b = 0;
					}
				}
			}
			v.SetData(new object[] { "768", data });
			StreamWriter s = new StreamWriter(picNumberBox.SelectedItem.ToString() + ".8xi");
			v.Save(new BinaryWriter(s.BaseStream));
			s.Close();
		}

		private void toolStripTextBox1_Click(object sender, EventArgs e) {
			p = new Bitmap(96, 64);
			canvas.Invalidate();
		}

		private void oneStringToolStripMenuItem_Click(object sender, EventArgs e) {
			OutStrings = ToList(96, 64);
			Close();
		}

		private void x8ToolStripMenuItem_Click(object sender, EventArgs e) {
			OutStrings = ToList(8, 8);
			Close();
		}

		private void x16ToolStripMenuItem_Click(object sender, EventArgs e) {
			OutStrings = ToList(16, 16);
			Close();
		}

		private void DoCustomStripMenuItem_Click(object sender, EventArgs e) {
			try {
				OutStrings = ToList(int.Parse(HexWidthBox.Text), int.Parse(HexHeightBox.Text));
				Close();
			} catch { }
		}

		private string[] ToList(int w, int h) {
			string[] l = ToBinList(w, h).ToArray();

			if (HexOrBinBox.SelectedItem.Equals("Hex")) {
				for (int i = 0; i < l.Length; i++) {
					l[i] = HexHelper.BinToHex(l[i]);
				}
			} else {
				for (int i = 0; i < l.Length; i++) {
					string asmString = "";
					string s = l[i];
					for (int j = 0; j < s.Length; j++) {
						if (j % w == 0) {
							asmString += "\n.db %";
						} else if (j % 8 == 0) {
							asmString += ",%";
						}
						asmString += s[j];
					}
					l[i] = asmString.Remove(0, 1) + "\n";
				}
			}

			return l;
		}

		private List<string> ToBinList(int w, int h) {
			List<string> l = new List<string>();
			string s = "";
			for (int j = 0; j < 64; j += h) {
				for (int i = 0; i < 96; i += w) {
					for (int y = j; y < j + h; y++) {
						for (int x = i; x < i + w; x++) {
							int val = p.GetPixel(x, y).ToArgb().Equals(Color.Black.ToArgb()) ? 1 : 0;
							s += val.ToString();
						}
					}
					l.Add(s);
					s = "";
				}
			}

			return l;
		}
	}
}
