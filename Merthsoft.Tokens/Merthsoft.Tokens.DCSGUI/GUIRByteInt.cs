using System;
using System.Drawing;

namespace Merthsoft.Tokens.DCSGUI {
	public class GUIRByteInt : GUIItem {
		public byte InitialValue { get; set; }
		public byte Min { get; set; }
		public byte Max { get; set; }

		public override int Width {
			get {
				return base.Width;
			}
			set {
				base.Width = 20;
			}
		}

		public override int Height {
			get {
				return base.Height;
			}
			set {
				base.Height = TextBoxHeight;
			}
		}

		public GUIRByteInt(int x, int y, int w, byte initialValue, byte min, byte max, GUIContainer container) {
			_GUINum = GUINums.GUIRByteInt;
			this.X = x;
			this.Y = y;
			Width = 20;
			Height = TextBoxHeight;
			InitialValue = initialValue;
			Min = min;
			Max = max;
			Container = container;
		}

		public override string GetOutString() {
			return string.Format("{0},{1},{2},{3},{4},{5}", base.GetOutString(), X, Y, InitialValue, Min, Max);
		}

		public override void Draw(System.Drawing.Graphics g) {
			Pen p = new Pen(Selected ? System.Drawing.Color.Red : System.Drawing.Color.Black);
			g.DrawRectangle(p, DrawRect);
			g.DrawString(InitialValue.ToString(), DCSFont, p.Brush, X, Y);
		}
	}
}
