using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Merthsoft.CalcData {
	public class TokenizeableVar8x : Var8x, IData8x {
		short numTokens;
		byte[] data;

		public override byte[] Data {
			get { return data; }
		}

		public override byte[] FullData {
			get {
				List<byte> b = new List<byte>();
				b.AddRange(((short)data.Length).GetBytes());
				b.AddRange(data);
				return b.ToArray();
			}
		}

		public override short DataLength { get { return (short)(2 + data.Length); } }

		public TokenizeableVar8x(VarType type, string name = "", CalcType calcType = CalcType.Calc8x)
			: base(type, name, calcType) {
		}

		public TokenizeableVar8x(BinaryReader b) : base(b) { }

		public override void ReadData(System.IO.BinaryReader b, int len) {
			numTokens = b.ReadInt16();
			data = b.ReadBytes(len);
		}

		public override short SetData(object[] data) {
			numTokens = short.Parse((string)data[0]);
			this.data = (byte[])(data[1]);
			return (short)(2 + data.Length);
		}
	}
}
