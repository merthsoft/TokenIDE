using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Merthsoft.CalcData {
	public abstract class Var8x : IData8x {
		public enum CalcType {
			Calc8x = 0,
			Calc83 = 1,
			Calc82 = 2,
			Calc73 = 3,
			Calc85 = 4,
			Calc86 = 5,
		}

		protected static List<string> SIGSTRING = new List<string>() { 
			"**TI83F*", 
			"**TI83**",
			"**TI82**",
			"**TI73**",
			"**TI85**",
			"**TI86**",
		};

		public enum VarType {
			RealNumber = 0x00,
			RealList = 0x01,
			Matrix = 0x02,
			YVar = 0x03,
			String = 0x04,
			Program = 0x05,
			ProgramLocked = 0x06,
			Picture = 0x07,
			GDB = 0x08,
			WindowSettings1 = 0x0B,
			ComplexNumber = 0x0C,
			ComplexList = 0x0D,
			WindowSettings2 = 0x0F,
			SavedWinSettings = 0x10,
			TableSetup = 0x11,
			Backup = 0x13,
			DelFlashApp = 0x14,
			AppVar = 0x15,
			GroupVar = 0x17,
			Directory = 0x19,
			FashOS = 0x23,
			FlashAPP = 0x24,
			IDList = 0x26,
			GetCertificate = 0x27,
			Clock = 0x29
		}

		protected enum VarPrefix : byte {
			Matrix = 0x5C,
			List = 0x5D,
			Picture = 0x60,
			String = 0xAA
		}

		protected byte FlagUnArchived = 0x00;
		protected byte FlagArchived = 0x80;

		// Header data
		protected byte[] sigArray;
		protected byte[] fsigArray;
		protected byte[] commentArray;
		protected byte[] lenArray;
		// Data section
		protected byte[] dataLenArray;
		protected byte[] flag1;
		protected byte[] varIDArray;
		protected byte[] varNameArray;
		protected byte[] version;
		protected byte archFlag;
		//protected IData8x _data;
		// Checksum
		protected byte[] checksumArray;

		protected string name;
		protected VarType varID;
		protected short length;
		protected ushort dataLength;
		protected string comment;

		public abstract byte[] Data { get; }
		public short Length { get { return (short)(DataLength + 17); } }
		//public ushort DataLength { get { return dataLength; } }

		public string Comment { get { return new string(comment.ToCharArray()); } }
		public VarType ID { get { return Calc == CalcType.Calc85 ? (VarType)0x05 : varID; } set { varID = value; } }
		public string Name { get { return name; } set { name = value; } }

		public CalcType Calc { get; set; }

		//public IData8x DataObject { get { return _data; } }

		public bool Archived {
			get {
				return archFlag == FlagArchived;
			}
			set {
				archFlag = (byte)(value ? FlagArchived : FlagUnArchived);
			}
		}

		public Var8x(VarType typeID, string name, CalcType calcType = Var8x.CalcType.Calc8x) {
			sigArray = SIGSTRING[(int)calcType].ToByteArray();
			if (calcType != CalcType.Calc85) {
				fsigArray = new byte[3] { 0x1A, 0x0A, 0x00 };
			} else {
				fsigArray = new byte[3] { 0x1A, 0x0C, 0x00 };
			}
			flag1 = new byte[2] { 0x0D, 0x00 };
			comment = "Merthsoft TokenIDE";
			commentArray = new byte[42];
			this.name = name;
			varNameArray = new byte[8];
			varID = typeID;
			archFlag = 0;
			Calc = calcType;
		}

		public Var8x(BinaryReader b) {
			#region Header
			// Header section
			sigArray = b.ReadBytes(8);
			string sig = Encoding.ASCII.GetString(sigArray);
			int type = SIGSTRING.IndexOf(sig);
			if (type == -1) {
				throw new Exception(string.Format("File is not valid, {0} is not a valid signature string.", sig));
			}
			Calc = (CalcType)type;

			fsigArray = b.ReadBytes(3);
			commentArray = b.ReadBytes(42);
			comment = ASCIIEncoding.ASCII.GetString(commentArray);
			lenArray = b.ReadBytes(2);
			length = BitConverter.ToInt16(lenArray, 0);
			#endregion
			#region Data
			// Data section
			flag1 = b.ReadBytes(2);
			dataLenArray = b.ReadBytes(2);
			dataLength = (ushort)(BitConverter.ToInt16(dataLenArray, 0) - 2);
			varIDArray = b.ReadBytes(1);
			varID = (VarType)varIDArray[0];
			if (Calc != CalcType.Calc85) {
				varNameArray = b.ReadBytes(8);
			} else {
				if (varID == (VarType)0x12) {
					varID = VarType.Program;
				}
				varNameArray = b.ReadBytes(BitConverter.ToInt16(flag1, 0));
			}
			name = ASCIIEncoding.ASCII.GetString(varNameArray);
			if (Calc == CalcType.Calc8x) {
				version = b.ReadBytes(1);
				archFlag = b.ReadByte();
			}
			// Skip next two bytes, they are a repeat of dataLength
			b.ReadBytes(2);
			ReadData(b, dataLength);
			#endregion
			// Checksum
			checksumArray = b.ReadBytes(2);
		}

		public void Save(BinaryWriter b) {
			comment = "Merthsoft Token IDE";
			List<byte> buffer = new List<byte>();
			#region Gen buffer
			#region Header
			List<byte> headerBuffer = new List<byte>();
			// Header section
			headerBuffer.AddRange(sigArray);
			headerBuffer.AddRange(fsigArray);
			headerBuffer.AddRange(Comment.ToByteArray(42));
			headerBuffer.AddRange(((short)(Length)).GetBytes());
			#endregion

			#region Data
			List<byte> dataBuffer = new List<byte>();
			// Data section
			short nameLength = (short)Name.Length;
			if (Calc != CalcType.Calc85) {
				dataBuffer.AddRange(flag1);
			} else {
				dataBuffer.AddRange(((short)(nameLength + 4)).ToByteArray());
			}
			dataBuffer.AddRange(((short)(DataLength)).GetBytes());
			dataBuffer.Add((byte)ID);
			if (Calc != CalcType.Calc85) {
				dataBuffer.AddRange(Name.ToByteArray(8));
			} else {
				dataBuffer.Add((byte)nameLength);
				dataBuffer.AddRange(Name.ToByteArray());
			}
			if (Calc == CalcType.Calc8x) {
				dataBuffer.Add(0);
				dataBuffer.Add(archFlag);
			}
			dataBuffer.AddRange(((short)(DataLength)).GetBytes());
			dataBuffer.AddRange(FullData);
			#endregion
			buffer.AddRange(headerBuffer);
			buffer.AddRange(dataBuffer);
			buffer.AddRange(dataBuffer.Checksum().GetBytes());
			#endregion
			b.Write(buffer.ToArray());
		}


		public abstract byte[] FullData { get; }

		public abstract void ReadData(BinaryReader b, int len);

		public abstract ushort SetData(object[] data);

		public abstract ushort DataLength { get; }
	}
}
