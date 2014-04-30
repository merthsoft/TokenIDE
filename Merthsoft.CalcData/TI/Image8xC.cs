using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Merthsoft.CalcData{
	public class Image8xC: Var8x {
		short _size = 21976;
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
			get { return (ushort)_data.Length; }
		}

		public Image8xC(byte picNumber = 0)
			: base(VarType.Image, new string(new char[] {(char)VarPrefix.Image, (char)picNumber})){
				Archived = true;
		}

		public Image8xC(BinaryReader b)
			: base(b) {
			if (ID != VarType.Image) {
				throw new Exception(string.Format("Type {0} ({1}) is not a valid image.", (int)ID, ID.ToString()));
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
			int width = 133;
			int height = 83;
			Bitmap bitmap = new Bitmap(width, height);
			int x = 0, y = height-1;
			for (int i = 2; i < (width + 1)*height*2; i+=2) {
				byte b1 = _data[i];
				byte b2 = _data[i+1];

				bitmap.SetPixel(x, y, MerthsoftExtensions.ColorFrom565(b1, b2));

				x += 1;
				if (x >= width) {
					i += 2;
					x = 0;
					y--;
				}
			}

			return bitmap;
		}
	}
}
