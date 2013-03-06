using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merthsoft.Tokens.DCSGUI {
	public abstract class GUIContainer : GUIItem {
		protected List<GUIItem> controls;
		public int XOff { get; set; }
		public int YOff { get; set; }
		public bool WinButtonClose { get; set; }
		public bool WinButtonMaximize { get; set; }
		public bool WinButtonMinimize { get; set; }
	}
}
