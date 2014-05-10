using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merthsoft.TokenIDE {
	class BitStream {
		private List<byte> data;
		private int position;

		public bool AtEnd { get { return position >= data.Count * 8; } }

		public byte[] Data { get { return data.ToArray(); } }

		public int Length { get { return data.Count * 8; } }

		public BitStream() {
			data = new List<byte>();
			data.Add(0);
			position = 0;
		}

		public BitStream(byte[] data, int position) {
			this.data = data.ToList();
			this.position = position;
		}

		public UInt64 Read(int numBits) {
			UInt64 ret = 0;
			for (int i = 0; i < numBits; i++) {
				ret <<= 1;
				ret += Read();
			}

			return ret;
		}

		public byte Read() {
			int dataPosition = position / 8;
			int bitPosition = 7-position % 8;
			position++;
			return (byte)((data[dataPosition] >> bitPosition) & 1);
		}

		public void Write(UInt64 value, int numBits) {
			for (int i = numBits-1; i >= 0; i--) {
				Write(((value >> i) & 1) == 1);
			}
		}

		public void Write(bool bit) {
			int dataPosition = position / 8;
			int bitPosition = 7 - position % 8;
			position++;

			while (dataPosition >= data.Count) {
				data.Add(0);
			}

			int val = bit ? 1 : 0;
			byte mask = ((byte)(~(1 << bitPosition)));
			byte b = (byte)(val << bitPosition);
			data[dataPosition] &= mask;
			data[dataPosition] += b;
		}
	}
}
