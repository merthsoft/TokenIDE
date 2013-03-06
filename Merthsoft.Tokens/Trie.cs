using System;
using System.Collections;

namespace Merthsoft.Tokens {
	class TrieNode<T> {
		public string s;
		public Hashtable outEdges;
		public T data;

		public TrieNode(string s) {
			this.s = s;
			outEdges = new Hashtable();
		}
	}

	/// <summary>
	/// Represents a Trie data structure.
	/// </summary>
	public class Trie<T> {
		TrieNode<T> head;

		/// <summary>
		/// Create an empty trie
		/// </summary>
		public Trie() {
			head = new TrieNode<T>(string.Empty);
		}

		/// <summary>
		/// Add a word to the tire
		/// </summary>
		/// <param name="word">The word to add</param>
		/// <param name="data">The data to associate with the word</param>
		public void AddWord(string word, T data) {
			TrieNode<T> n = head;
			for (int i = 0; i < word.Length; i++) {
				char c = word[i];
				if (!n.outEdges.ContainsKey(c)) {
					TrieNode<T> newNode = new TrieNode<T>(word.Substring(0, i + 1));
					n.outEdges.Add(c, newNode);
				}
				n = (TrieNode<T>)n.outEdges[c];
			}

			n.data = data;
		}

		/// <summary>
		/// Searches the Trie for the passed in word.
		/// </summary>
		/// <param name="word">The word to find.</param>
		/// <param name="data">The data associated with this word.</param>
		/// <param name="atLeaf">Gets set to true if the node is a leaf node.</param>
		/// <returns>true - word found
		/// false - word not found
		/// This could return true even if the data is null (i.e. if the word was not actually added),
		/// if the word you are searching for is a prefix of an existing word. If you allow nulls but not
		/// prefixes, you could use the "atLeaf" parameter to ensure that you are at a leaf. If you don't
		/// allow nulls, then the returned data would be null if this word wasn't actually added.</returns>
		public bool FindWord(string word, ref T data, out bool atLeaf) {
			TrieNode<T> n = head;
			for (int i = 0; i < word.Length; i++) {
				char c = word[i];
				if (!n.outEdges.ContainsKey(c)) {
					atLeaf = n.outEdges.Count == 0;
					return false;
				}
				n = (TrieNode<T>)n.outEdges[c];
			}
			atLeaf = (n.outEdges.Count == 0);
			data = n.data;
			return n.s == word;
		}
	}
}
