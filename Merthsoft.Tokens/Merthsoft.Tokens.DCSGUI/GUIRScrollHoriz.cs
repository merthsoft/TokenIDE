using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Merthsoft.Tokens.DCSGUI {
	public class GUIRScrollHoriz : GUIScroll {
		public override int Size {
			get {
				return Width;
			}
			set {
				Width = value;
			}
		}

		public GUIRScrollHoriz(int x, int y, int size, byte id, int moveAmount, int min, int max, int initialPosition)
			: base(x, y, size, id, moveAmount, min, max, initialPosition) {
				Height = 7;
				_GUINum = GUINums.GUIRScrollHoriz;
		}

		public override void Draw(System.Drawing.Graphics g) {
			Pen p = new Pen(Selected ? Color.Red : Color.Black);
			g.DrawRectangle(p, X, Y, Width, Height);
			g.DrawRectangle(p, X, Y, 6, Height);
			g.DrawRectangle(p, X + Width - 6, Y, 6, Height);
		}
	}
}
