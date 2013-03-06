using System;
using System.Drawing;

namespace Merthsoft.Tokens.DCSGUI {
	public class GUIRHotspot : GUIItem {
		public GUIRHotspot(int x, int y, int w, int h, GUIContainer container) {
			_GUINum = GUINums.GUIRHotspot;
			this.X = x;
			this.Y = y;
			Width = w;
			Height = h;
			Container = container;
		}

		public override string GetOutString() {
			return base.GetOutString() + "," + X + "," + Y + "," + Width + "," + Height;
		}

		public override void Draw(System.Drawing.Graphics g) {
			Pen p = new Pen(Selected ? System.Drawing.Color.Red : System.Drawing.Color.Black);
			p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
			g.DrawRectangle(p, DrawRect);
		}
	}
}
