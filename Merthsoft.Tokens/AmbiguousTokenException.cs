using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merthsoft.Tokens {
	public class AmbiguousTokenException : Exception {
		public string Token { get; set; }

		public AmbiguousTokenException(string token)
			: base(string.Format("Your XML file has ambiguous data: {0}", token)) {
				Token = token;
		}
	}
}
