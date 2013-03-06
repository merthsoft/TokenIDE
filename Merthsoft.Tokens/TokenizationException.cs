using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merthsoft.Tokens {
	public class TokenizationException : Exception {
		public string InvalidString { get; private set; }
		public int Location { get; private set; }

		public TokenizationException(string invalidString, int location)
			: base(string.Format("Could not tokenize string \"{0}\" at {1}.", invalidString, location)) {
				InvalidString = invalidString;
				Location = location;
		}
	}
}
