using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace Merthsoft.CalcData {
	public class RealList8x : Var8x, IEnumerable<decimal> {
		private List<RealData8x> listContents;
		public List<decimal> ListContents {
			get {
				List<decimal> a = new List<decimal>();
				foreach (var rd in listContents) {
					a.Add(rd.Value);
				}

				return a;
			}
		}

		public override byte[] Data {
			get {
				List<byte> data = new List<byte>();
				foreach (RealData8x rd in listContents) {
					data.AddRange(rd.Data);
				}
				return data.ToArray();
			}
		}

		public override byte[] FullData {
			get {
				List<byte> data = new List<byte>();
				data.AddRange(((short)(listContents.Count)).ToByteArray());
				data.AddRange(Data);
				return data.ToArray();
			}
		}

		public override short DataLength {
			get { return (short)(2 + listContents.Count); }
		}

		public int NumElements {
			get { return listContents.Count; }
		}

		public decimal this[int index] {
			get { return ListContents[index]; }
			set { ListContents[index] = value; }
		}

		public RealList8x(string name = "")
			: base(VarType.RealNumber, (char)VarPrefix.List + name) {
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

		public override void ReadData(System.IO.BinaryReader b, int len) {
			short numItems = b.ReadInt16();
			listContents = new List<RealData8x>(numItems);
			for (int i = 0; i < numItems; i++) {
				RealData8x rd = new RealData8x();
				rd.ReadData(b, 9);
				listContents.Add(rd);
			}
		}

		public override short SetData(object[] data) {
			throw new NotImplementedException();
		}
	}
}
