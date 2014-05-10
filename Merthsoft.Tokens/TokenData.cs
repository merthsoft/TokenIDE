using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using Merthsoft.Tokens;
using Merthsoft.Extensions;
using System.Text.RegularExpressions;
using System.IO;

namespace Merthsoft.Tokens {
	public class TokenData {
		public class TokenDictionaryEntry {
			public string Name { get; set; }
			public string Group { get; set; }
			public string Comment { get; set; }
			public string Site { get; set; }
			public string StyleType { get; set; }
			public bool StringTerminator { get; set; }
			public bool StringStarter { get; set; }
			public Dictionary<byte, TokenDictionaryEntry> SubTokens { get; set; }
			public List<string> Alts { get; set; }
			public string IndentGroup { get; set; }
			public bool IndentGroupTerminator { get; set; }
			public string Bytes { get; set; }
			public byte Byte { get; set; }
			public byte[] ByteArray {
				get {
					byte[] byteArray = new byte[Bytes.Length/2];
					for (int i = 0; i < Bytes.Length; i += 2) {
						byte b = byte.Parse(Bytes.Substring(i, 2));
						byteArray[i/2] = b;
					}

					return byteArray;
				}
			}

			public TokenDictionaryEntry() {
				SubTokens = new Dictionary<byte, TokenDictionaryEntry>();
				Alts = new List<string>();

				Group = "_default";
				StringTerminator = false;
				StringStarter = false;
				StyleType = null;
			}

			public override string ToString() {
				return Name;
			}
		}

		public class GroupEntry : IComparable<GroupEntry> {
			public string Main { get; set; }
			public List<string> Alts { get; set; }

			public GroupEntry(string main) {
				Main = main;
				Alts = new List<string>();
			}

			public override string ToString() {
				StringBuilder sb = new StringBuilder(Main);
				if (Alts.Count > 0) {
					sb.Append(" (");
					for (int i = 0; i < Alts.Count; i++) {
						sb.AppendFormat("{1}{0}", Alts[0], i != 0 ? ", " : "");
					}
					sb.Append(")");
				}

				return sb.ToString();
			}

			public int CompareTo(GroupEntry other) {
				return Main.CompareTo(other.Main);
			}
		}

		public class Style {
			public string Name { get; set; }
			public int MinTokenLength { get; set; }
			public string Foreground { get; set; }
			public string Background { get; set; }
			public string TokenUnderlineColor { get; set; }
			public bool Bold { get; set; }
			public bool Underline { get; set; }
			public bool Italic { get; set; }
			public bool Strike { get; set; }

			public Style(XmlNode node) {
				Name = node.Attributes["name"].Value;
				MinTokenLength = node.GetAttributeOrDefault("minTokenLength", -1);
				Foreground = node.GetAttributeOrDefault("foreground", "Black");
				Background = node.GetAttributeOrDefault("background", null);
				TokenUnderlineColor = node.GetAttributeOrDefault("tokenUnderlineColor", "DarkGray");
				Bold = node.GetAttributeOrDefault("bold", false);
				Underline = node.GetAttributeOrDefault("underline", false);
				Italic = node.GetAttributeOrDefault("italic", false);
				Strike = node.GetAttributeOrDefault("strike", false);
			}
		}

		Dictionary<byte, TokenDictionaryEntry> tokens;
		Trie<string> trie;

		public string CommentString { get; private set; }
		public string DirectiveString { get; private set; }
		public bool TrimStart { get; private set; }

		public Dictionary<byte, TokenDictionaryEntry> Tokens { get { return tokens; } private set { tokens = value; } }
		public Trie<string> Trie { get { return trie; } private set { trie = value; } }
		public List<string> GroupNames { get; private set; }
		public Dictionary<string, Style> Styles { get; private set; }
		public Dictionary<string, string> Comments { get; private set; }
		public Dictionary<string, string> Groups { get; private set; }
		public Dictionary<string, string> Sites { get; private set; }
		public Dictionary<string, TokenDictionaryEntry> FlatTokens { get; private set; }
		//public int FontSize { get; private set; }
		//public string FontFamily { get; private set; }

		/// <summary>
		/// Reads tokens in from an xml file.
		/// </summary>
		/// <param name="xmlFile">The xml file to read the tokens from.</param>
		/// <returns></returns>
		public TokenData(string xmlFile) {
			FileInfo fi = new FileInfo(xmlFile);

			XmlDocument doc = new XmlDocument();
			doc.Load(xmlFile);
			readTokenData(doc, fi.DirectoryName);
		}

		public TokenData(XmlDocument doc, string searchFolder = null) {
			readTokenData(doc, searchFolder);
		}

		public TokenData(string xml, string searchFolder) {
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			readTokenData(doc, searchFolder);
		}

		private void readTokenData(XmlDocument doc, string searchFolder) {
			XmlElement root = doc.DocumentElement;
			XmlNamespaceManager nsMan = new XmlNamespaceManager(root.OwnerDocument.NameTable);
			nsMan.AddNamespace("t", "http://merthsoft.com/Tokens");
			XmlNodeList tokenNodes = root.SelectNodes("/t:Tokens/t:Token", nsMan);
			XmlNodeList groupNodes = root.SelectNodes("/t:Tokens/t:Groups/t:Group", nsMan);
			XmlNodeList styleNodes = root.SelectNodes("/t:Tokens/t:Styles/t:Style", nsMan);
			trie = new Trie<string>();
			Groups = new Dictionary<string, string>();
			Comments = new Dictionary<string, string>();
			Sites = new Dictionary<string, string>();
			GroupNames = new List<string>();
			Styles = new Dictionary<string, Style>();
			FlatTokens = new Dictionary<string, TokenDictionaryEntry>();

			//FontSize = styleNodes[0].ParentNode.GetAttributeOrDefault("fontSize", 12);
			//FontFamily = styleNodes[0].ParentNode.GetAttributeOrDefault("fontFamily", "Consolas");

			string parentXml = root.GetAttributeOrDefault("parentXml", null);
			if (!string.IsNullOrWhiteSpace(parentXml)) {
				if (!File.Exists(parentXml) && searchFolder != null) {
					parentXml = Path.Combine(searchFolder, parentXml);
				}
				if (!File.Exists(parentXml)) {
					throw new FileNotFoundException(string.Format("Could not find parent xml file.", parentXml));
				}
				TokenData parentData = new TokenData(parentXml);

				Tokens = parentData.Tokens;
				Trie = parentData.Trie;
				Groups = parentData.Groups;
				Comments = parentData.Comments;
				Sites = parentData.Sites;
				GroupNames = parentData.GroupNames;
				Styles = parentData.Styles;
				FlatTokens = parentData.FlatTokens;
				CommentString = parentData.CommentString;
				DirectiveString = parentData.DirectiveString;
			}

			if (styleNodes.Count != 0) {
				CommentString = styleNodes[0].ParentNode.GetAttributeOrDefault("commentString", CommentString ?? "//");
				DirectiveString = styleNodes[0].ParentNode.GetAttributeOrDefault("directiveString", DirectiveString ?? "#");
				TrimStart = styleNodes[0].ParentNode.GetAttributeOrDefault("trimStart", true);
			} else {
				CommentString = "//";
				DirectiveString = "#";
				TrimStart = true;
			}

			foreach (XmlNode node in styleNodes) {
				Style s = new Style(node);
				Styles.Add(s.Name, s);
			}

			foreach (XmlNode node in groupNodes) {
				string name = node.Attributes["name"].Value;
				GroupNames.Add(name);
				if (node.Attributes["comment"] != null) {
					Comments.Add(name, Regex.Unescape(node.Attributes["comment"].Value));
				}
				if (node.Attributes["site"] != null) {
					Sites.Add(name, node.Attributes["site"].Value);
				}
			}

			List<string> alts;
			Dictionary<byte, TokenDictionaryEntry> newTokens = GetTokensFromNode(tokenNodes, out alts);

			if (tokens == null) {
				tokens = newTokens;
			} else {
				foreach (TokenDictionaryEntry token in newTokens.Values) {
					Combine(tokens, token);
				}
			}
		}

		private void Combine(Dictionary<byte, TokenDictionaryEntry> tokenDictionary, TokenDictionaryEntry newToken) {
			// Doesn't exist in current dictionary, so add it and be done
			if (!tokenDictionary.ContainsKey(newToken.Byte)) {
				tokenDictionary[newToken.Byte] = newToken;
				return;
			}

			var oldToken = tokenDictionary[newToken.Byte];

			// It does exist, so do we overwrite or not?
			if (!string.IsNullOrEmpty(newToken.Name)) {
				oldToken.Name = newToken.Name;
				oldToken.Alts = newToken.Alts;
				oldToken.Comment = newToken.Comment;
				oldToken.Group = newToken.Group;
				oldToken.IndentGroup = newToken.IndentGroup;
				oldToken.IndentGroupTerminator = newToken.IndentGroupTerminator;
				oldToken.StringStarter = newToken.StringStarter;
				oldToken.StringTerminator = newToken.StringTerminator;
				oldToken.StyleType = newToken.StyleType;
				oldToken.Site = newToken.Site;
			}

			foreach (var subToken in newToken.SubTokens.Values) {
				Combine(oldToken.SubTokens, subToken);
			}
		}

		public List<GroupEntry> GetAllInGroup(string group) {
			return GetAllInGroup(group, tokens);
		}

		private List<GroupEntry> GetAllInGroup(string group, Dictionary<byte, TokenDictionaryEntry> tokens) {
			if (tokens == null)
				return null;
			List<GroupEntry> ret = new List<GroupEntry>();
			foreach (byte b in tokens.Keys) {
				if (tokens[b].Group == group && tokens[b].Name != null) {
					GroupEntry ge = new GroupEntry(tokens[b].Name);
					ge.Alts = tokens[b].Alts;
					ret.Add(ge);
				}
				if (tokens[b].SubTokens != null) {
					ret.AddRange(GetAllInGroup(group, tokens[b].SubTokens));
				}
			}

			return ret;
		}

		private Dictionary<byte, TokenDictionaryEntry> GetTokensFromNode(XmlNodeList nodes, out List<string> alts, string prevBytes = "") {
			Dictionary<byte, TokenDictionaryEntry> currentTokens = new Dictionary<byte, TokenDictionaryEntry>();
			alts = new List<string>();
			foreach (XmlNode node in nodes) {
				//alts = new List<string>();
				if (node.NodeType != XmlNodeType.Comment) {
					if (node.Name == "Token") {
						byte key = getByteFromXml(node);

						TokenDictionaryEntry value = new TokenDictionaryEntry() { Byte = key };

						if (node.Attributes["string"] != null) {
							value.Name = node.Attributes["string"].Value;
							if (value.Name == "\\n") {
								value.Name = "\n";
							}

							if (node.Attributes["comment"] != null) {
								string comment = Regex.Unescape(node.Attributes["comment"].Value);
								value.Comment = comment;
								if (!Comments.ContainsKey(value.Name))
									Comments.Add(value.Name, comment);
							}

							if (node.Attributes["site"] != null) {
								string site = node.Attributes["site"].Value;
								value.Site = site;
								if (!Sites.ContainsKey(value.Name))
									Sites.Add(value.Name, site);
							}

							if (node.Attributes["group"] != null) {
								value.Group = node.Attributes["group"].Value;
								if (!GroupNames.Contains(value.Group)) {
									GroupNames.Add(value.Group);
								}
							} else {
								value.Group = "_default";
							}

							if (!Groups.ContainsKey(value.Name)) {
								Groups.Add(value.Name, value.Group);
							}

							if (node.Attributes["style"] != null) {
								value.StyleType = node.Attributes["style"].Value;
							}

							if (node.Attributes["stringTerminator"] != null) {
								bool term;
								value.StringTerminator = bool.TryParse(node.Attributes["stringTerminator"].Value, out term) ? term : false;
							}

							if (node.Attributes["stringStarter"] != null) {
								bool term;
								value.StringStarter = bool.TryParse(node.Attributes["stringStarter"].Value, out term) ? term : false;
							}

							if (node.Attributes["indentGroup"] != null) {
								value.IndentGroup = node.Attributes["indentGroup"].Value;
							}

							if (node.Attributes["indentGroupTerminator"] != null) {
								if (string.IsNullOrEmpty(value.IndentGroup)) {
									throw new ArgumentException(string.Format("Cannot have indentGroupTerminator without indentGroup. On token {0}.", value.Name));
								}

								bool term;
								value.IndentGroupTerminator = bool.TryParse(node.Attributes["indentGroupTerminator"].Value, out term) ? term : false;
							}

							string bytes = prevBytes + key.ToString("X2");
							value.Bytes = bytes;
							trie.AddWord(value.Name, bytes);
						}
						List<string> myAlts = new List<string>();
						if (node.HasChildNodes) {
							value.SubTokens = GetTokensFromNode(node.ChildNodes, out myAlts, prevBytes + key.ToString("X2"));
						}
						currentTokens.Add(key, value);
						if (value.Name != null) {
							if (FlatTokens.ContainsKey(value.Name)) {
								throw new AmbiguousTokenException(value.Name);
							}
							FlatTokens.Add(value.Name, value);
							foreach (string alt in myAlts) {
								if (FlatTokens.ContainsKey(alt)) {
									throw new AmbiguousTokenException(string.Format("{0} alt: ({1})", value.Name, alt));
								}
								value.Alts.Add(alt);
								FlatTokens.Add(alt, value);
							}
							myAlts.Clear();
						}
					} else if (node.Name == "Alt") {
						string alt = node.Attributes["string"].Value;
						trie.AddWord(alt, prevBytes);
						alts.Add(alt);
					}
				}
			}

			return currentTokens;
		}

		private byte getByteFromXml(XmlNode node) {
			string val = node.Attributes["byte"].Value;
			byte key;
			// starting with an "@" means it's a reference value
			if (val.StartsWith("@")) {
				key = FlatTokens[val.Substring(1)].Byte;
			} else if (!byte.TryParse(val, out key)) {
				key = (HexHelper.GetByteArray(val, 1))[0];
			}

			return key;
		}

		/// <summary>
		/// Tokenizes a given string.
		/// </summary>
		/// <param name="data">The strng to convert to tokes.</param>
		/// <param name="numTokens">The number of tokens in this string.</param>
		/// <returns>An array of tokens as bytes.</returns>
		public byte[] Tokenize(string data, out int numTokens) {
			List<List<TokenData.TokenDictionaryEntry>> toss;
			return Tokenize(data, out numTokens, out toss, false);
		}

		/// <summary>
		/// Tokenizes a given string.
		/// </summary>
		/// <param name="data">The string to convert to tokes.</param>
		/// <param name="numTokens">The number of tokens in this string.</param>
		/// <returns>An array of tokens as bytes.</returns>
		public byte[] Tokenize(string data, out int numTokens, out List<List<TokenData.TokenDictionaryEntry>> tokens, bool exceptionOnUnknownToken) {
			Trie<string> trie = this.Trie;
			string s = string.Empty;
			int i = 0;
			while (TrimStart && i < data.Length && (data[i] == ' ' || data[i] == '\t')) {
				i++;
			}
			numTokens = 0;
			int lineNumber = 0;
			tokens = new List<List<TokenDictionaryEntry>>();
			tokens.Add(new List<TokenDictionaryEntry>());
			while (i < data.Length) {
				string foundData = null;
				string lastGoodData = null;
				bool atLeaf = false;
				int back = 0;
				bool found = false;
				string sub = string.Empty;
				do {
					sub += data[i++].ToString();
					if (sub == "\\") {
						if (i != data.Length) {
							sub += data[i++].ToString();
							if (!trie.FindWord(sub, ref lastGoodData, out atLeaf)) {
								trie.FindWord(sub[1].ToString(), ref lastGoodData, out atLeaf);
							}
						}
						break;
					}
					//if (sub.EndsWith("\\")) {
					//	back++;
					//	break;
					//}
					found = trie.FindWord(sub, ref foundData, out atLeaf);
					if (foundData != null && foundData != lastGoodData) {
						lastGoodData = foundData;
						back = 0;
					} else if (sub != "\r") {
						back++;
					}
				} while (found && !atLeaf && i < data.Length);
				numTokens++;
				s += lastGoodData;
				if (lastGoodData == null && sub != "\r") {
					if (exceptionOnUnknownToken) {
						throw new TokenizationException(sub, i - 1);
					}
					tokens[lineNumber].Add(new TokenDictionaryEntry() { Name = sub, StyleType = "Error" });
					if (!sub.StartsWith("\\")) { i++; }
				} else if (sub == "\n") {
					lineNumber++;
					tokens.Add(new List<TokenDictionaryEntry>());
					while (TrimStart && i < data.Length && (data[i] == ' ' || data[i] == '\t')) {
					    i++;
					}
				} else if (sub != "\r") {
					if (sub.StartsWith("\\")) {
						tokens[lineNumber].Add(FlatTokens[sub.Substring(1, sub.Length - back - 1)]);
					} else {
						tokens[lineNumber].Add(FlatTokens[sub.Substring(0, sub.Length - back)]);
					}
				} else {
					numTokens--;
				}
				i -= back;
			}

			return HexHelper.GetByteArray(s);
		}

		/// <summary>
		/// Converts an array of bytes to a string of tokenizeable words.
		/// </summary>
		/// <param name="bytes">The bytes to convert.</param>
		/// <param name="lines">Holds the output of each line as line/token</param>
		/// <returns>The array as a string of tokenizeable words.</returns>
		public string Detokenize(byte[] bytes, out List<List<TokenDictionaryEntry>> lines) {
			lines = new List<List<TokenDictionaryEntry>>();
			lines.Add(new List<TokenDictionaryEntry>());
			if (bytes == null)
				return "";
			Dictionary<byte, TokenData.TokenDictionaryEntry> tokens = Tokens;
			string s = string.Empty;
			int lineNumber = 0;
			TokenDictionaryEntry entry;
			for (int i = 0; i < bytes.Length; i++) {
				string s1 = GetStringFromBytes(bytes, ref i, tokens, out entry);
				//s1 = Regex.Unescape(s1);
				if (s1 != "\n") {
					lines[lineNumber].Add(entry);
				} else {
					s1 = Environment.NewLine;
					lineNumber++;
					lines.Add(new List<TokenDictionaryEntry>());
				}
				s += s1;
			}
			return s;
		}

		/// <summary>
		/// Converts an array of bytes to a string of tokenizeable words.
		/// </summary>
		/// <param name="bytes">The bytes to convert.</param>
		/// <returns>The array as a string of tokenizeable words.</returns>
		public string Detokenize(byte[] bytes) {
			List<List<TokenDictionaryEntry>> discard;
			return Detokenize(bytes, out discard);
		}

		private static string GetStringFromBytes(byte[] bytes, ref int index, Dictionary<byte, TokenData.TokenDictionaryEntry> tokens, out TokenDictionaryEntry entry) {
			if (index == bytes.Length) {
				entry = null;
				return null;
			}
			byte b = bytes[index];
			string s1 = null;
			entry = null;
			if (!tokens.ContainsKey(b)) {
				s1 = string.Format("[NOT FOUND: {0:X2}]", b);
				entry = null;
			} else if (tokens[b].SubTokens == null) {
				entry = tokens[b];
				s1 = entry.Name;
			} else {
				index++;
				if (index != bytes.Length && tokens[b].SubTokens.ContainsKey(bytes[index])) {
					s1 = GetStringFromBytes(bytes, ref index, tokens[b].SubTokens, out entry);
				}
				if (s1 == null) {
					index--;
					entry = tokens[b];
					s1 = entry.Name;
				}
			}
			return s1;
		}
	}
}
