using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Merthsoft.CalcData {
	public class AppVar8x : TokenizeableVar8x {
		public AppVar8x(string name = "")
			: base(VarType.AppVar, name) {
		}

		public AppVar8x(BinaryReader b)
			: base(b) {
			if (ID != VarType.AppVar) {
				throw new Exception(string.Format("Type {0} ({1}) is not a valid AppVar.", (int)ID, ID.ToString()));
			}
		}
	}
}
