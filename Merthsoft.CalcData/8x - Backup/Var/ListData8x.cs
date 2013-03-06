using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merthsoft.CalcData{
	class RealListData8x : IData8x {
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

		public byte[] Data {
			get {
				List<byte> data = new List<byte>();
				foreach (RealData8x rd in listContents) {
					data.AddRange(rd.Data);
				}
				return data.ToArray();
			}
		}

		public byte[] FullData {
			get {
				List<byte> data = new List<byte>();
				data.AddRange(((short)(listContents.Count)).ToByteArray());
				data.AddRange(Data);
				return data.ToArray();
			}
		}

		public void ReadData(System.IO.BinaryReader b, int len) {
			short numItems = b.ReadInt16();
			listContents = new List<RealData8x>(numItems);
			for (int i = 0; i < numItems; i++) {
				RealData8x rd = new RealData8x();
				rd.ReadData(b, 9);
				listContents.Add(rd);
			}
		}

		public short SetData(object[] data) {
			throw new NotImplementedException();
		}
	}
}
