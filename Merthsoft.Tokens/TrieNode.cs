using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merthsoft.Tokens {
	class TrieNode<TKey, TData> {
		public TData Data { get; set; }
		public bool TerminalNode { get; set; }

		public Dictionary<TKey, TrieNode<TKey, TData>> Children { get; set; }

		public TrieNode() {
			Children = new Dictionary<TKey, TrieNode<TKey, TData>>();
		}

		public TrieNode(TData data) : this() {
			Data = data;
		}
	}
}
