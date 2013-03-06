using System;
using System.Drawing;

namespace Merthsoft.Tokens.DCSGUI {
	public class GUIRPassIn : GUIItem {
		public string Text { get; set; }
		public int MaxChars { get; set; }

		public override int Height {
			get {
				return base.Height;
			}
			set {
				base.Height = TextBoxHeight;
			}
		}

		public GUIRPassIn(int x, int y, int w, int maxChars, string text, GUIContainer container) {
			_GUINum = GUINums.GUIRPassIn;
			this.X = x;
			this.Y = y;
			Width = w;
			Height = TextBoxHeight;
			MaxChars = maxChars;
			Text = text;
			Container = container;
		}

		public override string GetOutString() {
			return string.Format("{0},{1},{2},{3},{4},\"{5}", base.GetOutString(), X, Y, Width, MaxChars, Text);
		}

		public override void Draw(System.Drawing.Graphics g) {
			Pen p = new Pen(Selected ? System.Drawing.Color.Red : System.Drawing.Color.Black);
			g.DrawRectangle(p, DrawRect);
			g.DrawString("*****".Substring(0, Math.Min(Width / 4, 5)), DCSFont, p.Brush, X, Y);
		}
	}
}
