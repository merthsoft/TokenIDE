using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace Merthsoft.CalcData {
	public class RealList8x : Var8x, IEnumerable<decimal> {
		public List<decimal> ListContents {
			get {
				List<decimal> a = new List<decimal>();
				foreach (var rd in ((RealListData8x)_data).ListContents) {
					a.Add(rd);
				}

				return a;
			}
		}

		public int NumElements {
			get { return ((RealListData8x)_data).ListContents.Count; }
		}

		public decimal this[int index] {
			get { return ListContents[index]; }
			set { ListContents[index] = value; }
		}

		public RealList8x(string name = "")
			: base(VarType.RealNumber, (char)VarPrefix.List + name) {
				_data = new RealListData8x();
		}

		public RealList8x(BinaryReader b)
			: base(b) {
				if (ID != VarType.RealList) {
				throw new Exception(string.Format("Type {0} ({1}) is not a valid real list.", (int)ID, ID.ToString()));
			}
		}

		IEnumerator<decimal> IEnumerable<decimal>.GetEnumerator() {
			return ListContents.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return ListContents.GetEnumerator();
		}
	}
}
