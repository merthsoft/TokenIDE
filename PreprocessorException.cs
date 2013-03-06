using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenIDE {
	class PreprocessorException : Exception {
		public PreprocessorException(string message) : base(message) { }
	}
}
