using System;
using System.Drawing;
using System.ComponentModel;

namespace Merthsoft.Tokens.DCSGUI {
	public abstract class GUIScroll : GUIItem {
		public abstract int Size { get; set; }
		public int MoveAmount { get; set; }
		public int Min { get; set; }
		public int Max { get; set; }
		public int InitialPosition { get; set; }
		public byte ID { get; set; }

		public GUIScroll(int x, int y, int size, byte id, int moveAmount, int min, int max, int initialPosition) {
			X = x;
			Y = y;
			Size = size;
			ID = id;
			MoveAmount = moveAmount;
			Min = min;
			Max = max;
			InitialPosition = initialPosition;
		}

		[Browsable(false)]
		public override int Height {
			get {
				return base.Height;
			}
			set {
				base.Height = value;
			}
		}

		[Browsable(false)]
		public override int Width {
			get {
				return base.Width;
			}
			set {
				base.Width = value;
			}
		}
	}
}
