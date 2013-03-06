using System;
using System.Drawing;

namespace Merthsoft.Tokens.DCSGUI {
	public class GUIRSmallWin : GUIContainer {
		public string Title { get; set; }
		public string Icon { get; set; }

		public GUIRSmallWin(int x, int y, string title, string icon) {
			_GUINum = GUINums.GUIRSmallWin;
			this.X = x;
			this.Y = y;
			Width = 81;
			Height = 49;
			this.Title = title;
			this.Icon = icon;
			Container = null;
			XOff = 1 + x;
			YOff = 10 + y;
		}

		public override string GetOutString() {
			return base.GetOutString() + "," + X + "," + Y + ",\"" + Icon + "\",\"" + Title;
		}

		public override void Draw(System.Drawing.Graphics g) {
			Pen p = new Pen(Selected ? Color.Red : Color.Black);
			g.DrawRectangle(p, DrawRect);
			g.DrawRectangle(p, X, Y, Width-1, 10);
			g.DrawString(Title, DCSFont, p.Brush, X, Y);
			if (WinButtonClose) {
				g.DrawRectangle(p, X + Width - 9, Y + 1, 7, 7); 
			}
			if (WinButtonMaximize) {
				g.DrawRectangle(p, X + Width - 17, Y + 1, 7, 7);
			}
			if (WinButtonMinimize) {
				g.DrawRectangle(p, X + Width - 25, Y + 1, 7, 7);
			}
		}
	}
}
