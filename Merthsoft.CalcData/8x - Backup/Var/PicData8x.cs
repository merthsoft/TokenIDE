using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Merthsoft.CalcData {
	public class PicData8x : IData8x {
		short _size = 0x2F4;
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
			_size = b.ReadInt16();
			_data = b.ReadBytes(_size);
		}

		public short SetData(object[] data) {
			_size = short.Parse((string)data[0]);
			_data = (byte[])(data[1]);
			return (short)(2 + _size);
		}

		public Bitmap GetBitmap() {
			Bitmap bitmap = new Bitmap(96, 64);
			int x = 0, y = 0;
			for (int i = 0; i < _size; i++) {
				byte b = _data[i];
				for (int j = 7; j >= 0; j--) {
					if (((b & (1 << j)) >> j) == 1) {
						bitmap.SetPixel(x, y, Color.Black);
					} else {
						bitmap.SetPixel(x, y, Color.White);
					}
					x++;
					if (x == 96) {
						x = 0;
						y++;
					}
				}
			}

			return bitmap;
		}
	}
}
