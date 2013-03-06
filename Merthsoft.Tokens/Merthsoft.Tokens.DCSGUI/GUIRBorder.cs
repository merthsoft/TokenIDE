using System;
using System.Drawing;

namespace Merthsoft.Tokens.DCSGUI {
	public class GUIRBorder : GUIItem{
		public enum RectColor { White, Black, Invert }

		public RectColor Color { get; set; }

		public GUIRBorder(int x, int y, int w, int h, RectColor c, GUIContainer container) {
			_GUINum = GUINums.GUIRBorder;
			this.X = x;
			this.Y = y;
			Width = w;
			Height = h;
			Container = container;
			Color = c;
		}

		public override string GetOutString() {
			return base.GetOutString() + "," + X + "," + Y + "," + Width + "," + Height + "," + (int)Color;
		}

		public override void Draw(System.Drawing.Graphics g) {
			Pen p = new Pen(Selected ? System.Drawing.Color.Red : System.Drawing.Color.Black);
			g.DrawRectangle(p, DrawRect);
		}
	}
}
