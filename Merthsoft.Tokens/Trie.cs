using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Merthsoft.Tokens {
	public class Trie<TKey, TData> {
		public int LongestPath { get; private set; }
		private TrieNode<TKey, TData> head = new TrieNode<TKey, TData>();

		public void AddData(IEnumerable<TKey> key, TData data) {
			TrieNode <TKey, TData> currentNode = head;

			int length = 0;
			foreach (TKey k in key) {
				if (!currentNode.Children.ContainsKey(k)) {
					currentNode.Children[k] = new TrieNode<TKey, TData>();
				}
				currentNode = currentNode.Children[k];
				length++;
			}

			if (length > LongestPath) { LongestPath = length; }

			//if (currentNode == head) { throw new ArgumentException("key must contain a non-zero number of elements."); }

			currentNode.Data = data;
			currentNode.TerminalNode = true;
		}

		public bool GetData(IEnumerable<TKey> key, out TData data) {
			TrieNode<TKey, TData> currentNode = head;

			foreach (TKey k in key) {
				if (!currentNode.Children.ContainsKey(k)) {
					throw new KeyNotFoundException(string.Format("Key {0} not found.", key));
				}
				currentNode = currentNode.Children[k];
			}

			if (!currentNode.TerminalNode) {
				throw new KeyNotFoundException(string.Format("Key {0} not found.", key));
			}

			data = currentNode.Data;

			return currentNode != head;
		}

		public bool LongestSubstringMatch(TKey[] key, int start, out TData data, out TKey[] matchingKey) {
			TrieNode<TKey, TData> currentNode = head;
			TrieNode<TKey, TData> lastMatch = head;
			List<TKey> lastMatchingKeyList = new List<TKey>();
			List<TKey> matchingKeyList = new List<TKey>();

			for (int i = start; i < key.Length; i++) {
				var k = key[i];
				if (!currentNode.Children.ContainsKey(k)) {
					currentNode = lastMatch;
					matchingKeyList = lastMatchingKeyList;
					break;
				}
				matchingKeyList.Add(k);
				currentNode = currentNode.Children[k];
				if (currentNode.TerminalNode) {
					lastMatch = currentNode;
					lastMatchingKeyList = matchingKeyList.ToList();
				}
			}

			data = lastMatch.Data;
			matchingKey = lastMatchingKeyList.ToArray();
			return lastMatch != head;
		}
	}
}
