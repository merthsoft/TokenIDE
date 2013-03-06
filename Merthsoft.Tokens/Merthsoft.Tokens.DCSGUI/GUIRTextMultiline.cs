using System;
using System.Drawing;

namespace Merthsoft.Tokens.DCSGUI {
	public class GUIRTextMultiline : GUIContainer {
		public string Text { get; set; }
		public int Font { get; set; }

		/// <summary>
		/// The number of rows
		/// </summary>
		public override int Height {
			get {
				return base.Height;
			}
			set {
				base.Height = value / 6;
			}
		}

		public GUIRTextMultiline(int x, int y, int rows, int width, string text, int font, GUIContainer container) {
			_GUINum = GUINums.GUIRTextMultiline;
			X = x;
			Y = y;
			Height = rows;
			Width = width;
			Container = container;
			Text = text;
			Font = font;
		}

		public override string GetOutString() {
			return string.Format("{0},{1},{2},{5},{6},{3},\"{4}", base.GetOutString(), X, Y, Font, Text, Height, Width);
		}

		public override void Draw(Graphics g) {
			Pen p = new Pen(Selected ? Color.Red : Color.Black);
			g.DrawRectangle(p, X, Y, Width, Height * 6);
			g.DrawString(Text, DCSFont, p.Brush, new RectangleF(X, Y, Width, 6 * Height));
		}
	}
}
