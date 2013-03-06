using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merthsoft.CalcData {
	public class RealData8x : IData8x {
		[Flags]
		public enum RealFlags : byte {
			Undefined = 0x02,
			Complex1 = 0x04,
			Complex2 = 0x08,
			ModifiedSinceLastGraph = 0x40,
			Sign = 0x80
		}

		private RealFlags _flags;
		private decimal _value;

		public decimal Value {
			get { return _value; }
			set { _value = value; }
		}

		public bool Defined {
			get {
				return (_flags & RealFlags.Undefined) == 0;
			}
			set {
				_flags = _flags | RealFlags.Undefined;
			}
		}

		public int Sign {
			get {
				return (_flags & RealFlags.Sign) > 0 ? -1 : 1;
			}
			set {
				if (value < 0) {
					_flags = _flags | RealFlags.Sign;
				} else if (value > 0) {
					_flags &= ~RealFlags.Sign;
				} else {
					throw new ArgumentException("Sign must not be zero.");
				}
			}
		}

		public byte[] Data {
			get { return FullData; }
		}

		public byte[] FullData {
			get {
				byte[] Result = new byte[9];

				// Object type:
				Result[0] = (byte)(Sign < 0 ? 0x80 : 0x00);

				// Calculate the mantissa and exponent:
				decimal Mantissa = Math.Abs(Value);
				int Exponent = -1;

				while (Math.Truncate(Mantissa) != 0m) {
					Mantissa /= 10m;
					++Exponent;
				}
				while (Math.Truncate(Mantissa * 10m) == 0m) {
					Mantissa *= 10m;
					--Exponent;
				}


				if (Exponent < -128 || Exponent > 127) throw new ArgumentOutOfRangeException();

				// Exponent:
				Result[1] = (byte)(0x80 + Exponent);

				// Mantissa:
				byte[] AsAscii = Array.ConvertAll<byte, byte>(Encoding.ASCII.GetBytes(Mantissa.ToString(System.Globalization.CultureInfo.InvariantCulture).Substring(2).PadRight(14, '0')), delegate(byte b) { return (byte)((b >= '0' && b <= '9') ? (b - '0') : 0); });
				for (int i = 0; i < 7; ++i) {
					Result[i + 2] = (byte)(((AsAscii[(i * 2)] & 0x0F) << 4) | (AsAscii[(i * 2) + 1] & 0x0F));
				}

				return Result;
			}
		}

		public void ReadData(System.IO.BinaryReader b, int len) {
			if (len != 9) {
				throw new ArgumentException("Length must be equal to nine.");
			}
			byte[] data = b.ReadBytes(9);
			_flags = (RealFlags)data[0];
			byte exponent = data[1];// b.ReadByte();
			byte[] mantissa = data.SubArray(2, 7);// b.ReadBytes(7);

			decimal result = 0;
			for (int i = 0; i < 7; ++i) {
				result += (decimal)(Math.Pow(10, i * 2 + 0) * (mantissa[6 - i] & 0xF));
				result += (decimal)(Math.Pow(10, i * 2 + 1) * (mantissa[6 - i] >> 04));
			}
			result *= (decimal)Math.Pow(10, exponent - 0x80 - 14 + 1);

			if (Sign < 0) result *= -1;

			Value = result == 0m ? 0m : Decimal.Parse(result.ToString().TrimEnd('0', '.'));
		}

		public short SetData(object[] data) {
			throw new NotImplementedException();
		}
	}
}
