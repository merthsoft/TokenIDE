using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merthsoft.TokenIDE {
	public delegate void PasteTextEventHandler(object sender, PasteTextEventArgs e);

	public class PasteTextEventArgs : EventArgs {
		public string TextToPaste { get; private set; }

		public PasteTextEventArgs(string textToPaste) {
			TextToPaste = textToPaste;
		}

	}
}
