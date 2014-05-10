using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Merthsoft.CalcData;

namespace Merthsoft.TokenIDE {
	public interface IEditWindow {
		event DragEventHandler DragDrop;
		event DragEventHandler DragEnter;

		TabPage ParentTabPage { get; set; }
		string SaveDirectory { get; set; }
		bool HasSaved { get; set; }
		bool New { get; set; }
		bool Dirty { get; set; }
		string OnCalcName { get; set; }
		string FileName { get; set; }
		Var8x CalcVar { get; set;  }
		bool FirstFileFlag { get; set; }
		int NumTokens { get; }
		byte[] ByteData { get; }
		bool Archived { get; set; }

		void Save();

		void Undo();
		void Redo();
		void Find();
		void Replace();
	}
}
