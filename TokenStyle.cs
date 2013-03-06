using System.Drawing;
using FastColoredTextBoxNS;
using Merthsoft.Extensions;

namespace Merthsoft.Tokens {
	public class ErrorStyle : TokenStyle {
		public static new TokenStyle Default;

		static ErrorStyle() {
			Default = new ErrorStyle((Brush)Brushes.Black.Clone(), null, FontStyle.Regular, Pens.Red);
		}

		public ErrorStyle(Brush foreBrush, Brush backgroundBrush, FontStyle fontStyle, Pen errorUnderlinePen)
			: base(foreBrush, backgroundBrush, fontStyle, errorUnderlinePen, -1) {
		}

		public ErrorStyle(TokenStyle tokenStyle)
			: base(tokenStyle.ForeBrush, tokenStyle.BackgroundBrush, tokenStyle.FontStyle, Pens.Red, -1) {
		}

		public override void Draw(System.Drawing.Graphics gr, System.Drawing.Point position, Range range) {
			base.Draw(gr, position, range);

			int width = (range.End.iChar - range.Start.iChar) * range.tb.CharWidth;
			int height = range.tb.CharHeight;
			int dx = range.tb.CharWidth;

			for (int i = 0; i < width; i += dx) {
				gr.DrawLine(TokenUnderlinePen, position.X + i, position.Y + height-2, position.X + i + dx / 2, position.Y + height);
				gr.DrawLine(TokenUnderlinePen, position.X + i + dx / 2, position.Y + height, position.X + i + dx, position.Y + height-2);
			}
		}
	}

	public class TokenStyle : TextStyle {
		public int MinTokenLengthToUnderline { get; set; }
		public Pen TokenUnderlinePen { get; set; }

		public static TokenStyle Default;

		static TokenStyle() {
			Default = new TokenStyle((Brush)Brushes.Black.Clone(), null, FontStyle.Regular, null, 2);
		}

		public TokenStyle(Brush foreBrush, Brush backgroundBrush, FontStyle fontStyle, Pen tokenUnderlinePen, int minTokenLengthToUnderline)
			: base(foreBrush, backgroundBrush, fontStyle) {
			MinTokenLengthToUnderline = minTokenLengthToUnderline == -1 ? int.MaxValue : minTokenLengthToUnderline;
			TokenUnderlinePen = tokenUnderlinePen ?? Pens.DarkGray;
		}

		public static TokenStyle FromTokenDataStyle(TokenData.Style s) {
			Brush foreBrush = BrushFromString(s.Foreground);
			Brush backBrush = BrushFromString(s.Background);
			Pen underPen = PenFromString(s.TokenUnderlineColor);
			int minTok = s.MinTokenLength;
			FontStyle fs = FontStyle.Regular;
			if (s.Bold) { fs |= FontStyle.Bold; }
			if (s.Italic) { fs |= FontStyle.Italic; }
			if (s.Underline) { fs |= FontStyle.Underline; }
			if (s.Strike) { fs |= FontStyle.Strikeout; }

			return new TokenStyle(foreBrush, backBrush, fs, underPen, minTok);
		}

		private static Brush BrushFromString(string s) {
			if (s == null) { return null; }

			Brush b;
			if (HexHelper.IsHexString(s)) {
				b = new SolidBrush(Color.FromArgb(HexHelper.GetInt(s)));
			} else {
				b = new SolidBrush(Color.FromName(s));
			}

			return b;
		}

		private static Pen PenFromString(string s) {
			if (s == null) { return null; }

			Pen p;
			if (HexHelper.IsHexString(s)) {
				p = new Pen(Color.FromArgb(HexHelper.GetInt(s)));
			} else {
				p = new Pen(Color.FromName(s));
			}

			return p;
		}

		public override void Draw(System.Drawing.Graphics gr, System.Drawing.Point position, Range range) {
			base.Draw(gr, position, range);
			if (range.End.iChar - range.Start.iChar < MinTokenLengthToUnderline) {
				return;
			}

			int width = (range.End.iChar - range.Start.iChar) * range.tb.CharWidth;
			int height = range.tb.CharHeight;
			// Bottom line
			gr.DrawLine(TokenUnderlinePen, position.X, position.Y + height, position.X + width - 2, position.Y + height);
			// Left line
			gr.DrawLine(TokenUnderlinePen, position.X, position.Y + height - 3, position.X, position.Y + height);
			// Right line
			gr.DrawLine(TokenUnderlinePen, position.X + width - 2, position.Y + height - 3, position.X + width - 2, position.Y + height);
		}
	}
}
