using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Merthsoft.CalcData {
	public class Pic8x : Var8x {
		public Pic8x(byte picNumber = 0)
			: base(VarType.Picture, new String(new char[] {(char)VarPrefix.Picture, (char)picNumber})){
			_data = new PicData8x();
		}

		public Pic8x(BinaryReader b)
			: base(b) {
			if (ID != VarType.Picture) {
				throw new Exception(string.Format("Type {0} ({1}) is not a valid picture.", (int)ID, ID.ToString()));
			}
		}
	}
}
