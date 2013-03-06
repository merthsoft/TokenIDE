using System;
using System.Drawing;

namespace Merthsoft.Tokens.DCSGUI {
	public class GUIRnull:GUIContainer {
		public bool OverwriteScreen { get; set; }

		public GUIRnull(bool overwriteScreen) {
			_GUINum = GUINums.GUIRnull;
			X = 0;
			Y = 0;
			Width = 96;
			Height = 64;
			OverwriteScreen = overwriteScreen;
			Container = null;
			XOff = 1;
			YOff = 1;
		}

		public override string GetOutString() {
			return base.GetOutString() + "," + (OverwriteScreen ? "255" : "254");
		}

		public override void Draw(System.Drawing.Graphics g) {
			Pen p = new Pen(Selected ? Color.Red : Color.Black);
			p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
			g.DrawRectangle(p, DrawRect);
		}
	}
}
