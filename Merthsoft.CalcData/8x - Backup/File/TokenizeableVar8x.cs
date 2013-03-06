using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Merthsoft.CalcData {
	public class TokenizeableVar8x : Var8x {
		public TokenizeableVar8x(VarType type, string name = "")
			: base(type, name) {
			_data = new TokenData8x();
		}

		public TokenizeableVar8x(BinaryReader b) : base(b) { }
	}
}
