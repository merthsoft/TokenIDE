using System;
using System.Drawing;

namespace Merthsoft.Tokens.DCSGUI {
	public class GUIRFullScreenImg:GUIItem {
		public int PicNum { get; set; }

		public GUIRFullScreenImg(int picNum) {
			_GUINum = GUINums.GUIRFullScreenImg;
			Width = 96;
			Height = 64;
			this.PicNum = picNum;
			Container = null;
		}

		public override string GetOutString() {
			return string.Format("{0},{1},{2},{3}", base.GetOutString(), X, Y, PicNum);
		}

		public override void Draw(System.Drawing.Graphics g) {
			Pen p = new Pen(Selected ? Color.Red : Color.Black);
			p.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
			g.DrawRectangle(p, DrawRect);
		}
	}
}
