using System;
using System.Drawing;
using Merthsoft.Extensions;

namespace Merthsoft.Tokens.DCSGUI {
	public class GUIRRect : GUIItem {
		public byte Fill { get; set; }

		public GUIRRect(int x, int y, int w, int h, byte c, GUIContainer container) {
			_GUINum = GUINums.GUIRRect;
			this.X = x;
			this.Y = y;
			Width = w;
			Height = h;
			Fill = c;
			Container = container;
		}

		public override string GetOutString() {
			return base.GetOutString() + "," + X + "," + Y + "," + Width + "," + Height + "," + (int)Fill;
		}

		public override void Draw(System.Drawing.Graphics g) {
			if (Fill == 254) {
				g.FillRectangle(Selected ? Brushes.Red : Brushes.Black, DrawRect);
			} else if (Fill == 0) {
				Pen p = new Pen(Selected ? System.Drawing.Color.Red : System.Drawing.Color.Black);
				p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
				g.DrawRectangle(p, DrawRect);
			} else {
				// bitmap texture
				Bitmap textureBitmap = new Bitmap(8, 2);
				Graphics textureGraphics = Graphics.FromImage(textureBitmap);
				string s = HexHelper.HexToBin(Fill.ToString("X").PadLeft(2, '0'));
				for (int i = 0; i < 8; i++) {
					if (s[i] == '0') {
						textureBitmap.SetPixel(i, 0, Color.White);
						textureBitmap.SetPixel(i, 1, Selected ? Color.Red : Color.Black);
					} else {
						textureBitmap.SetPixel(i, 1, Color.White);
						textureBitmap.SetPixel(i, 0, Selected ? Color.Red : Color.Black);
					}
				}

				TextureBrush texturedBrush = new TextureBrush(textureBitmap);
				g.FillRectangle(texturedBrush, DrawRect);
			}

		}
	}
}
