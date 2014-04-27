using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace Merthsoft.CalcData {
	public class Pic8x : Var8x {
		short _size = 0x2F4;
		byte[] _data;

		public override byte[] Data {
			get { return _data; }
		}

		public byte PicNumber {
			get { return (byte)Name[1]; }
		}

		public override byte[] FullData {
			get {
				List<byte> b = new List<byte>();
				b.AddRange(((short)_data.Length).GetBytes());
				b.AddRange(_data);
				return b.ToArray();
			}
		}

		// [TODO] figure this shit out.
		public override ushort DataLength {
			get { return (ushort)_data.Length; }
		}

		public Pic8x(byte picNumber = 0)
			: base(VarType.Picture, new String(new char[] {(char)VarPrefix.Picture, (char)picNumber})){
		}

		public Pic8x(BinaryReader b)
			: base(b) {
			if (ID != VarType.Picture) {
				throw new Exception(string.Format("Type {0} ({1}) is not a valid picture.", (int)ID, ID.ToString()));
			}
		}

		public override void ReadData(System.IO.BinaryReader b, int len) {
			_size = b.ReadInt16();
			_data = b.ReadBytes(_size);
		}

		public override ushort SetData(object[] data) {
			_size = short.Parse((string)data[0]);
			_data = (byte[])(data[1]);
			return (ushort)(2 + _size);
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
