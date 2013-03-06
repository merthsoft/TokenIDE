using System;
using System.Drawing;
using System.ComponentModel;

namespace Merthsoft.Tokens.DCSGUI {
	public class GUIRWrappedText : GUIContainer {
		private string _text;
		public string Text {
			get { return _text; }
			set {
				_text = value;
				//_height = 
			}
		}
		public int Font { get; set; }

		[Browsable(false)]
		public override int Height {
			get {
				return base.Height;
			}
			set {
				base.Height = value;
			}
		}

		public GUIRWrappedText(int x, int y, int width, string text, int font, GUIContainer container) {
			_GUINum = GUINums.GUIRWrappedText;
			X = x;
			Y = y;
			Width = width;
			Container = container;
			Text = text;
			Font = font;
		}

		public override string GetOutString() {
			return string.Format("{0},{1},{2},{5},{6},{3},\"{4}", X, Y, Font, Text, Height, Width);
		}

		public override void Draw(Graphics g) {
			Pen p = new Pen(Selected ? Color.Red : Color.Black);
			p.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
			g.DrawRectangle(p, X, Y, Width, Height * 6);
			g.DrawString(Text, DCSFont, p.Brush, new RectangleF(X, Y, Width, 6 * Height));
		}
	}
}
