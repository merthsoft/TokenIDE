using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Merthsoft.CalcData {
	public class Matrix8x : Var8x, IEnumerable {
		public List<List<decimal>> MatrixContents {
			get {
				List<List<decimal>> a = new List<List<decimal>>();
				foreach (var col in ((MatrixData8x)_data).MatrixContents) {
					var b = new List<decimal>();
					foreach (var rd in col) {
						b.Add(rd);
					}
					a.Add(b);
				}

				return a;
			}
		}

		public int NumCols {
			get {
				return ((MatrixData8x)_data).NumCols;
			}
		}

		public int NumRows {
			get {
				return ((MatrixData8x)_data).NumRows;
			}
		}

		public decimal this[int col, int row] {
			get { return MatrixContents[col][row]; }
			set { MatrixContents[col][row] = value; }
		}

		public Matrix8x(char matrix = 'A')
			: base(VarType.Matrix, ((char)VarPrefix.Matrix) + (matrix.ToString().ToUpper()[0] - 'A').ToString()) {
				_data = new MatrixData8x();
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
	}
}
