using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merthsoft.CalcData {
	public class TokenData8x : IData8x {
		short _numTokens;
		byte[] _data;

		public byte[] Data {
			get { return _data; }
		}

		public byte[] FullData {
			get {
				List<byte> b = new List<byte>();
				b.AddRange(((short)_data.Length).GetBytes());
				b.AddRange(_data);
				return b.ToArray();
			}
		}

		public void ReadData(System.IO.BinaryReader b, int len) {
			_numTokens = b.ReadInt16();
			_data = b.ReadBytes(len - 2);
		}

		public short SetData(object[] data) {
			_numTokens = short.Parse((string)data[0]);
			_data = (byte[])(data[1]);
			return (short)(2 + _data.Length);
		}
	}
}
