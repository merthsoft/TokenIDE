using System;
using System.Drawing;

namespace Merthsoft.Tokens.DCSGUI {
	public class GUIRCheckbox : GUIItem {
		public enum CheckedState { Unchecked = 0, Checked = 1 }

		public string GroupID { get; set; }
		public CheckedState InitialState { get; set; }
		public string Title { get; set; }

		public GUIRCheckbox(int x, int y, string groupID, CheckedState initialState, string title, GUIContainer container) {
			_GUINum = GUINums.GUIRCheckbox;
			X = x;
			Y = y;
			Width = 5;
			Height = 5;
			GroupID = groupID;
			InitialState = initialState;
			Title = title;
			Container = container;
		}

		public override string GetOutString() {
			return base.GetOutString() + "," + X.ToString() + "," + Y.ToString() + "," + Convert.ToInt32(InitialState) + @",""" + GroupID + @""",""" + Title;
		}

		public override void Draw(System.Drawing.Graphics g) {
			Pen p = new Pen(Selected ? Color.Red : Color.Black, 1);
			g.DrawRectangle(p, DrawRect);
			if (InitialState == CheckedState.Checked)
				g.FillRectangle(Selected ? Brushes.Red : Brushes.Black, DrawRect);
		}
	}
}
