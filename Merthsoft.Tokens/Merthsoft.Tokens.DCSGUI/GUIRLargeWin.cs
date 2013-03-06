using System;
using System.Drawing;

namespace Merthsoft.Tokens.DCSGUI {
	public class GUIRLargeWin : GUIContainer {
		public string Title { get; set; }
		public string Icon { get; set; }

		public GUIRLargeWin(string title, string icon) {
			_GUINum = GUINums.GUIRLargeWin;
			this.X = 0;
			this.Y = 0;
			Width = 96;
			Height = 64;
			this.Title = title;
			this.Icon = icon;
			Container = null;
			XOff = 1;
			YOff = 10;
		}

		public override string GetOutString() {
			return base.GetOutString() + ",\"" + Icon + "\",\"" + Title;
		}

		public override void Draw(System.Drawing.Graphics g) {
			Pen p = new Pen(Selected ? Color.Red : Color.Black);
			g.DrawRectangle(p, DrawRect);
			g.DrawRectangle(p, X, Y, Width, 10);
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
