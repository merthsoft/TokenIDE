using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Merthsoft.CalcData {
	public class String8x : TokenizeableVar8x {
		public String8x(byte strNumber = 0)
			: base(VarType.String, ((char)VarPrefix.String) + strNumber.ToString()) {
		}

		public String8x(BinaryReader b)
			: base(b) {
				if (ID != VarType.String) {
				throw new Exception(string.Format("Type {0} ({1}) is not a valid String.", (int)ID, ID.ToString()));
			}
		}
	}
}
