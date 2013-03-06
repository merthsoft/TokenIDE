using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Merthsoft.CalcData {
	public class Prog8x : TokenizeableVar8x {
		public bool Locked {
			get {
				return _ID == VarType.ProgramLocked;
			}
			set {
				_ID = value ? VarType.ProgramLocked : VarType.Program;
			}
		}

		public Prog8x(string name = "")
			: base(VarType.Program, name) {
			_data = new TokenData8x();
			_ID = VarType.Program;
		}

		public Prog8x(BinaryReader b)
			: base(b) {
			if (ID != VarType.Program && ID != VarType.ProgramLocked) {
			    throw new Exception(string.Format("Type {0} ({1}) is not a valid program.", (int)ID, ID.ToString()));
			}
		}
	}
}
