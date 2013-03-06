using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merthsoft.CalcData {
	class MatrixData8x : IData8x {
		private List<List<RealData8x>> matrixContents;
		public List<List<decimal>> MatrixContents {
			get {
				List<List<decimal>> a = new List<List<decimal>>();
				foreach (var col in matrixContents) {
					var b = new List<decimal>();
					foreach (var rd in col) {
						b.Add(rd.Value);
					}
					a.Add(b);
				}

				return a;
			}
		}

		public int NumCols {
			get {
				return matrixContents.Count;
			}
		}

		public int NumRows {
			get {
				return matrixContents[0].Count;
			}
		}

		public byte[] Data {
			get { throw new NotImplementedException(); }
		}

		public byte[] FullData {
			get { throw new NotImplementedException(); }
		}

		public void ReadData(System.IO.BinaryReader b, int len) {
			byte numCols = b.ReadByte();
			byte numRows = b.ReadByte();

			matrixContents = new List<List<RealData8x>>(numCols);

			for (int i = 0; i < numRows; i++) {
				List<RealData8x> col = new List<RealData8x>();
				for (int j = 0; j < numCols; j++) {
					RealData8x rd = new RealData8x();
					rd.ReadData(b, 9);
					col.Add(rd);
				}
				matrixContents.Add(col);
			}
		}

		public short SetData(object[] data) {
			throw new NotImplementedException();
		}
	}
}
