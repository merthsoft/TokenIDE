using System;
using System.Drawing;

namespace Merthsoft.Tokens.DCSGUI {
	public class GUIRText : GUIContainer {
		public string Text { get; set; }
		public int Font { get; set; }

		public GUIRText(int x, int y, string text, int font, GUIContainer container) {
			_GUINum = GUINums.GUIRText;
			this.X = x;
			this.Y = y;
			Container = container;
			Text = text;
			Font = font;
		}

		public override string GetOutString() {
			return string.Format("{0},{1},{2},{3},\"{4}", base.GetOutString(), X, Y, Font, Text);
		}

		public override void Draw(Graphics g) {
			Pen p = new Pen(Selected ? Color.Red : Color.Black);
			g.DrawString(Text, DCSFont, p.Brush, X, Y);
		}
	}
}
