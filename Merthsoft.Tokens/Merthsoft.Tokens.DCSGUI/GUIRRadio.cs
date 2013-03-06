using System;
using System.Drawing;

namespace Merthsoft.Tokens.DCSGUI {
	public class GUIRRadio : GUIItem {
		public enum CheckedState { Unchecked = 0, Checked = 1 }

		public string GroupID { get; set; }
		public CheckedState InitialState { get; set; }

		public GUIRRadio(int x, int y, string groupID, CheckedState initialState, GUIContainer container) {
			_GUINum = GUINums.GUIRRadio;
			X = x;
			Y = y;
			Width = 5;
			Height = 5;
			GroupID = groupID;
			InitialState = initialState;
			Container = container;
		}

		public override string GetOutString() {
			return string.Format("{0},{1},{2},{3},\"{4}", base.GetOutString(), X, Y, Convert.ToInt32(InitialState), GroupID);
		}

		public override void Draw(System.Drawing.Graphics g) {
			Pen p = new Pen(Selected ? Color.Red : Color.Black, 1);
			g.DrawEllipse(p, DrawRect);
			if (InitialState == CheckedState.Checked)
				g.FillEllipse(Selected ? Brushes.Red : Brushes.Black, DrawRect);
		}
	}
}
