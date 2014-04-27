using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Merthsoft.TokenIDE {
	public class TabCloseEventArgs : EventArgs {
		public TabPage TabPage { get; set; }
		public bool Cancel { get; set; }

		public TabCloseEventArgs(TabPage tabPage) {
			TabPage = tabPage;
			Cancel = false;
		}
	}
}
