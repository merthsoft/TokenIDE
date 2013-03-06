using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Merthsoft.CalcData {
	public class Matrix8x : Var8x, IEnumerable {
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

		public override short DataLength {
			get { return (short)(2 + NumCols + NumRows); }
		}

		public override byte[] Data {
			get { throw new NotImplementedException(); }
		}

		public override byte[] FullData {
			get { throw new NotImplementedException(); }
		}

		public decimal this[int col, int row] {
			get { return MatrixContents[col][row]; }
			set { MatrixContents[col][row] = value; }
		}

		public Matrix8x(char matrix = 'A')
			: base(VarType.Matrix, ((char)VarPrefix.Matrix) + (matrix.ToString().ToUpper()[0] - 'A').ToString()) {
		}

		public Matrix8x(BinaryReader b)
			: base(b) {
				if (ID != VarType.Matrix) {
				throw new Exception(string.Format("Type {0} ({1}) is not a valid matrix.", (int)ID, ID.ToString()));
			}
		}

		public IEnumerator GetEnumerator() {
			return MatrixContents.GetEnumerator();
		}

		public override void ReadData(System.IO.BinaryReader b, int len) {
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

		public override short SetData(object[] data) {
			throw new NotImplementedException();
		}
	}
}
