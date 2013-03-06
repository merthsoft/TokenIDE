using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace Merthsoft.CalcData {
	public interface IData8x {
		byte[] Data { get; }
		byte[] FullData { get; }
		short DataLength { get; }

		void ReadData(BinaryReader b, int len);

		short SetData(object[] data);
	}
}
