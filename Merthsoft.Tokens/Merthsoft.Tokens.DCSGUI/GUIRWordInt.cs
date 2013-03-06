using System;
using System.Drawing;

namespace Merthsoft.Tokens.DCSGUI {
	public class GUIRWordInt : GUIItem {
		public int InitialValue { get; set; }
		public int Min { get; set; }
		public int Max { get; set; }

		public override int Width {
			get {
				return base.Width;
			}
			set {
				base.Width = 27;
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

		public GUIRWordInt(int x, int y, int w, int initialValue, int min, int max, GUIContainer container) {
			_GUINum = GUINums.GUIRWordInt;
			this.X = x;
			this.Y = y;
			Width = 27;
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
