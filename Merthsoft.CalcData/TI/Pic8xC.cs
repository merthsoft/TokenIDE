using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Merthsoft.CalcData {
	public class Pic8xC : Var8x {
		public static List<Color> Palette = new List<Color>() {
			Color.Cyan,
			MerthsoftExtensions.ColorFrom565(0,0,31), MerthsoftExtensions.ColorFrom565(31,0,0), MerthsoftExtensions.ColorFrom565(0,0,0),
			MerthsoftExtensions.ColorFrom565(31,0,31), MerthsoftExtensions.ColorFrom565(0,39,0), MerthsoftExtensions.ColorFrom565(31,35,4),
			MerthsoftExtensions.ColorFrom565(22,8,0), MerthsoftExtensions.ColorFrom565(0,0,15), MerthsoftExtensions.ColorFrom565(0,36,31),
			MerthsoftExtensions.ColorFrom565(31,63,0), MerthsoftExtensions.ColorFrom565(31,63,31), MerthsoftExtensions.ColorFrom565(28,56,28),
			MerthsoftExtensions.ColorFrom565(24,48,24), MerthsoftExtensions.ColorFrom565(17,34,17), MerthsoftExtensions.ColorFrom565(10,21,10),
		};

		short _size = 21945;
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

		public override ushort DataLength {
			get { return (ushort)(_data.Length+2); }
		}

		public Pic8xC(byte picNumber = 0)
			: base(VarType.Picture, new string(new char[] {(char)VarPrefix.Picture, (char)picNumber})){
				Archived = true;
				version = 0x0A;
		}

		public Pic8xC(BinaryReader b)
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
			int width = 265;
			int height = 165;
			Bitmap bitmap = new Bitmap(width, height);
			int x = 0, y = 0;
			for (int i = 0; i < _size; i++) {
				byte b = _data[i];
				byte b1 = (byte)((b & 0xF0) >> 4);
				byte b2 = (byte)(b & 0x0F);

				bitmap.SetPixel(x, y, Palette[b1]);

				if (x+1 < width) {
					bitmap.SetPixel(x + 1, y, Palette[b2]);
				}

				x += 2;
				if (x > width) {
					x = 0;
					y++;
				}
			}

			return bitmap;
		}
	}
}
