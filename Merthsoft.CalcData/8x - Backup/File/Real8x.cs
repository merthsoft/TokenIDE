using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Merthsoft.CalcData {
	public class Real8x : Var8x {
		public Decimal Value {
			get { return ((RealData8x)_data).Value; }
		}

		public Real8x(string name = "")
			: base(VarType.RealNumber, name) {
				_data = new RealData8x();
		}

		public Real8x(BinaryReader b)
			: base(b) {
				if (ID != VarType.RealNumber) {
				throw new Exception(string.Format("Type {0} ({1}) is not a valid real number.", (int)ID, ID.ToString()));
			}
		}
	}
}
