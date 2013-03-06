using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Merthsoft.CalcData {
	public class Prog8x : TokenizeableVar8x {
		public bool Locked {
			get {
				return varID == VarType.ProgramLocked;
			}
			set {
				varID = value ? VarType.ProgramLocked : VarType.Program;
			}
		}

		public bool IsAsm {
			get {
				if (Data == null || Data.Length < 2) { return false; }
				return Data[0] == 0xBB && Data[1] == 0x6D;
			}
		}

		public Prog8x(string name = "", CalcType calcType = CalcType.Calc8x)
			: base(VarType.Program, name, calcType) {
			varID = VarType.Program;
		}

		public Prog8x(BinaryReader b)
			: base(b) {
			if (ID != VarType.Program && ID != VarType.ProgramLocked) {
			    throw new Exception(string.Format("Type {0} ({1}) is not a valid program.", (int)ID, ID.ToString()));
			}
		}

	}
}
