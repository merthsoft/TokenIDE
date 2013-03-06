using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Merthsoft.CalcData {
	public class Real8x : Var8x, IData8x {
		RealData8x realData;

		public override byte[] Data {
			get { return realData.Data; }
		}

		public override byte[] FullData {
			get { return realData.FullData; }
		}

		public override short DataLength {
			get { return realData.DataLength; }
		}

		public Real8x(string name = "")
			: base(VarType.RealNumber, name) {
				realData = new RealData8x();
		}

		public Real8x(BinaryReader b)
			: base(b) {
				if (ID != VarType.RealNumber) {
				throw new Exception(string.Format("Type {0} ({1}) is not a valid real number.", (int)ID, ID.ToString()));
			}
		}

		public override void ReadData(System.IO.BinaryReader b, int len) {
			realData.ReadData(b, len);
		}

		public override short SetData(object[] data) {
			return realData.SetData(data);
		}
	}
}
