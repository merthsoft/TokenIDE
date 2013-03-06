using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Merthsoft.CalcData {
	public abstract class Var8x {
		public enum VarType {
			RealNumber		= 0x00,
			RealList		= 0x01,
			Matrix			= 0x02,
			YVar			= 0x03,
			String			= 0x04,
			Program			= 0x05,
			ProgramLocked	= 0x06,
			Picture			= 0x07,
			GDB				= 0x08,
			WindowSettings1	= 0x0B,
			ComplexNumber	= 0x0C,
			ComplexList		= 0x0D,
			WindowSettings2	= 0x0F,
			SavedWinSettings= 0x10,
			TableSetup		= 0x11,
			Backup			= 0x13,
			DelFlashApp		= 0x14,
			AppVar			= 0x15,
			GroupVar		= 0x17,
			Directory		= 0x19,
			FashOS			= 0x23,
			FlashAPP		= 0x24,
			IDList			= 0x26,
			GetCertificate	= 0x27,
			Clock			= 0x29
		}

		protected enum VarPrefix : byte {
			Matrix	= 0x5C,
			List	= 0x5D,
			Picture = 0x60,
			String	= 0xAA
		}

		protected byte FlagUnArchived = 0x00;
		protected byte FlagArchived = 0x80;

		protected static string SIGSTRING = "**TI83F*";

		// Header data
		protected byte[] _sigArray;
		protected byte[] _fsigArray;
		protected byte[] _commentArray;
		protected byte[] _lenArray;
		// Data section
		protected byte[] _dataLenArray;
		protected byte[] _flag1;
		protected byte[] _varID;
		protected byte[] _varNameArray;
		protected byte[] _version;
		protected byte _archFlag;
		protected IData8x _data;
		// Checksum
		protected byte[] _checksumArray;

		protected string _name;
		protected VarType _ID;
		protected short _length;
		protected short _dataLength;
		protected string _comment;

		public byte[] Data { get { return _data.Data; } }
		public short Length { get { return _length; } }
		public short DataLength { get { return _dataLength; } }

		public string Comment { get { return new string(_comment.ToCharArray()); } }
		public VarType ID { get { return _ID; } set { _ID = value; } }
		public string Name { get { return _name; } set { _name = value; } }

		public IData8x DataObject { get { return _data; } }

		public bool Archived {
			get {
				return _archFlag == FlagArchived;
			}
			set {
				_archFlag = (byte)(value ? FlagArchived : FlagUnArchived);
			}
		}

		public Var8x(VarType typeID, string name) {
			_sigArray = SIGSTRING.ToByteArray();
			_fsigArray = new byte[3] { 0x1A, 0x0A, 0x00 };
			_flag1 = new byte[2] { 0x0D, 0x00 };
			_comment = "Merthsoft TokenIDE";
			_commentArray = new byte[42];
			_name = name;
			_varNameArray = new byte[8];
			_ID = typeID;
			_archFlag = 0;
			//if (ID == VarType.Program)
			//    _data = new ProgData8x();
			//if (ID == VarType.Picture) {
			//    _data = new PicData8x();
			//    _name = ((char)0x60) + name.ToLower().Replace("pic", "");
			//}
		}

		public static Var8x FromBinaryReader(BinaryReader b) {
			Var8x ret = null;

			#region Header
			// Header section
			var sigArray = b.ReadBytes(8);
			var fsigArray = b.ReadBytes(3);
			var commentArray = b.ReadBytes(42);
			var comment = ASCIIEncoding.ASCII.GetString(commentArray);
			var lenArray = b.ReadBytes(2);
			var length = BitConverter.ToInt16(lenArray, 0);
			#endregion
			#region Data
			// Data section
			var flag1 = b.ReadBytes(2);
			var dataLenArray = b.ReadBytes(2);
			var dataLength = BitConverter.ToInt16(dataLenArray, 0);
			var varID = b.ReadBytes(1);
			var ID = (VarType)varID[0];
			var varNameArray = b.ReadBytes(8);
			var name = ASCIIEncoding.ASCII.GetString(varNameArray);
			var version = b.ReadBytes(1);
			var archFlag = b.ReadByte();
			// Skip next two bytes, they are a repeat of dataLength
			b.ReadBytes(2);
			switch (ID) {
				case VarType.Program:
				case VarType.ProgramLocked:
					ret = new Prog8x();
					break;
				case VarType.Picture:
					ret = new Pic8x();
					break;
			}
			ret._data.ReadData(b, dataLength);
			#endregion
			// Checksum
			var checksumArray = b.ReadBytes(2);
			ret._sigArray = sigArray;
			ret._fsigArray = fsigArray;
			ret._commentArray = commentArray;
			ret._comment = comment;
			ret._lenArray = lenArray;
			ret._length = length;
			ret._flag1 = flag1;
			ret._dataLenArray = dataLenArray;
			ret._dataLength = dataLength;
			ret._version = version;
			ret._archFlag = archFlag;
			ret._checksumArray = checksumArray;

			return ret;
		}

		public Var8x(BinaryReader b) {
			#region Header
			// Header section
			_sigArray = b.ReadBytes(8);
			_fsigArray = b.ReadBytes(3);
			_commentArray = b.ReadBytes(42);
			_comment = ASCIIEncoding.ASCII.GetString(_commentArray);
			_lenArray = b.ReadBytes(2);
			_length = BitConverter.ToInt16(_lenArray, 0);
			#endregion
			#region Data
			// Data section
			_flag1 = b.ReadBytes(2);
			_dataLenArray = b.ReadBytes(2);
			_dataLength = BitConverter.ToInt16(_dataLenArray, 0);
			_varID = b.ReadBytes(1);
			_ID = (VarType)_varID[0];
			_varNameArray = b.ReadBytes(8);
			_name = ASCIIEncoding.ASCII.GetString(_varNameArray);
			_version = b.ReadBytes(1);
			_archFlag = b.ReadByte();
			// Skip next two bytes, they are a repeat of dataLength
			b.ReadBytes(2);
			switch (ID) {
				case VarType.Program:
				case VarType.ProgramLocked:
				case VarType.AppVar:
					_data = new TokenData8x();
					break;
				case VarType.Picture:
					_data = new PicData8x();
					break;
				case VarType.RealNumber:
					_data = new RealData8x();
					break;
				case VarType.RealList:
					_data = new RealListData8x();
					break;
				case VarType.Matrix:
					_data = new MatrixData8x();
					break;
				default:
					throw new Exception(string.Format("Type {0} not found.", (int)ID));
			}
			_data.ReadData(b, _dataLength);
			#endregion
			// Checksum
			_checksumArray = b.ReadBytes(2);
		}

		public void SetData(object[] data) {
			_dataLength = _data.SetData(data);
			_length = (short)(74 + _dataLength);
		}

		public void Save(BinaryWriter b) {
			_comment = "Merthsoft Token IDE";
			List<byte> buffer = new List<byte>();
			#region Gen buffer
			#region Header
			List<byte> headerBuffer = new List<byte>();
			// Header section
			headerBuffer.AddRange(_sigArray);
			headerBuffer.AddRange(_fsigArray);
			headerBuffer.AddRange(Comment.ToByteArray(42));
			headerBuffer.AddRange(((short)(Length - 57)).GetBytes());
			#endregion

			#region Data
			List<byte> dataBuffer = new List<byte>();
			// Data section
			dataBuffer.AddRange(_flag1);
			dataBuffer.AddRange(DataLength.GetBytes());
			dataBuffer.Add((byte)ID);
			dataBuffer.AddRange(Name.ToByteArray(8));
			dataBuffer.Add(0);
			dataBuffer.Add(_archFlag);
			dataBuffer.AddRange(DataLength.GetBytes());
			dataBuffer.AddRange(_data.FullData);
			#endregion
			buffer.AddRange(headerBuffer);
			buffer.AddRange(dataBuffer);
			//buffer.AddRange(BitConverter.GetBytes(dataBuffer.ToArray().Checksum()));
			buffer.AddRange(dataBuffer.ToArray().Checksum().GetBytes());
			#endregion
			b.Write(buffer.ToArray());
		}
	}
}
