using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Merthsoft.Tokens.DCSGUI {
	public class GUIRScrollVert : GUIScroll {
		public override int Size {
			get {
				return Height;
			}
			set {
				Height = value;
			}
		}

		public GUIRScrollVert(int x, int y, int size, byte id, int moveAmount, int min, int max, int initialPosition)
			: base(x, y, size, id, moveAmount, min, max, initialPosition) {
				Width = 7;
				_GUINum = GUINums.GUIRScrollVert;
		}

		public override void Draw(System.Drawing.Graphics g) {
			Pen p = new Pen(Selected ? Color.Red : Color.Black);
			g.DrawRectangle(p, X, Y, Width, Height);
			g.DrawRectangle(p, X, Y, Width, 6);
			g.DrawRectangle(p, X, Y + Height - 6, Width, 6);
		}
	}
}
