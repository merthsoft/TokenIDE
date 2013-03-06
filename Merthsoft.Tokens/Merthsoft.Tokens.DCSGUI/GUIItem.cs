using System;
using System.Drawing;
using System.Drawing.Text;

namespace Merthsoft.Tokens.DCSGUI {
	public enum GUINums {
		GUIRnull = 0,			// Added
		GUIRLargeWin,			// Added
		GUIRSmallWin,			// Added
		GUIRFullScreenImg,		// Added
		GUIRText,				// Added
		// GUIRWinButtons, Put in Container Class
		GUIRWrappedText = 6,	// Added
		GUIRButtonText,
		GUIRButtonImg,
		GUIRTextLineIn,			// Added
		GUIRRadio,				// Added
		GUIRCheckbox,			// Added
		GUIRByteInt,			// Added
		GUIRWordInt,			// Added
		GUIRHotspot,			// Added
		GUIRTextMultiline,		// Added
		GUIRSprite,
		GUIRLargeSprite,
		GUIRPassIn,				// Added
		GUIRScrollVert,			// Added
		GUIRScrollHoriz,		// Added
		GUIRBorder,				// Added
		GUIRRect,				// Added
		GUIRMouseCursor = 24,
		_end
	};

	public abstract class GUIItem {
		public static Font DCSFont;
		public static int TextBoxHeight = 8;

		protected GUINums _GUINum;
		protected static string baseArg = "PushGUIStack(";
		protected Rectangle DrawRect { 
			get {
				if (Container != null)
					return new Rectangle(X + Container.XOff, Y + Container.YOff, Width - 1, Height - 1);
				else
					return new Rectangle(X, Y, Width - 1, Height - 1);
			} 
		}

		public virtual int X { get; set; }
		public virtual int Y { get; set; }
		private int _width;
		public virtual int Width { get { return _width; } set { _width = Math.Max(0, value); } }
		private int _height;
		public virtual int Height { get { return _height; } set { _height = Math.Max(0, value); } }
		public GUIContainer Container { get; set; }

		public bool Selected { get; set; }

		public GUINums GUINum { get { return _GUINum; } }

		public bool IsPointOnItem(int x, int y) {
			return new Rectangle(X, Y, Width, Height).IntersectsWith(new Rectangle(x, y, 1, 1));
		}

		public virtual string GetOutString() {
			return string.Format("{0}{1}", baseArg, (int)_GUINum);
		}

		public override string ToString() {
			return string.Format("{0} ({1})", _GUINum.ToString(), DrawRect.ToString());
		}

		public abstract void Draw(Graphics g);
	}
}
