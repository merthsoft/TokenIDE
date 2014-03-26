using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using Merthsoft.CalcData;
using Merthsoft.Tokens;

namespace Merthsoft.TokenIDE {
	public partial class Prog8xEditWindow : UserControl, IEditWindow {
		public class Replacement {
			public int Location { get; set; }
			public string OldValue { get; set; }
			public string NewValue { get; set; }

			public Replacement(int location, string oldValue, string newValue) {
				Location = location;
				OldValue = oldValue;
				NewValue = newValue;
			}
		}

		private int _numTokens;
		private TokenizeableVar8x _program;
		private string _programName;
		private TokenData _TokenData;
		private bool dirty;
		private bool liveUpdate;
		private Dictionary<string, TokenStyle> styles;

		public bool Archived {
			get { return _program == null ? false : _program.Archived; }
			set {
				if (_program != null) {
					_program.Archived = value;
					archivedCheckBox.Checked = value;
				}
			}
		}

		public byte[] ByteData { get { return GenerateByteData(false, true); } }

		public Var8x CalcVar { get { return Program; } set { Program = (TokenizeableVar8x)value; } }

		public bool Dirty {
			get {
				return dirty;
			}
			set {
				if (ParentTabPage == null) return;
				dirty = value;
				ParentTabPage.Text = string.Format("{0}{1}", OnCalcName, dirty ? "*" : "");
			}
		}

		public string FileName {
			get;
			set;
		}

		public bool FirstFileFlag { get; set; }

		public bool HasSaved {
			get;
			set;
		}

		public bool LiveUpdate {
			get {
				return liveUpdate;
			}
			set {
				liveUpdate = value;
				liveUpdateCheckBox.Checked = value;
			}
		}

		public bool Locked { get { return ((Prog8x)_program).Locked; } }

		public bool New { get; set; }

		public int NumTokens { get { return _numTokens; } }

		public string OnCalcName {
			get {
				return _programName ?? "";
			}
			set {
				_programName = value;
				_program.Name = _programName;
				byte[] data = TokenData.Tokenize(ProgramTextBox.Text, out _numTokens);
				UpdateBytesLabel(data.Length);
				if (ParentTabPage != null) {
					ParentTabPage.Text = _programName;
				}
			}
		}

		public bool Opening { get; set; }

		public TabPage ParentTabPage { get; set; }
		public TokenizeableVar8x Program {
			get {
				return _program;
			}
			set {
				Opening = true;
				_program = value;
				OnCalcName = _program.Name.Replace("\0", "");
				byte[] data = _program.Data;
				if (data != null) {
					UpdateBytesLabel(data.Length);
					List<List<TokenData.TokenDictionaryEntry>> tokens;
					ProgramText = TokenData.Detokenize(data, out tokens);
				}
				archivedCheckBox.Checked = _program.Archived;
				if (_program is Prog8x) {
					lockedBox.Checked = ((Prog8x)_program).Locked;
				} else {
					HideLocked();
				}
			}
		}

		public string[] ProgramLines {
			get { return ProgramTextBox.Lines.ToArray(); }
		}

		public string ProgramText {
			get { return ProgramTextBox.Text; }
			set { ProgramTextBox.Text = value; LastLength = value.Length; }
		}

		public bool ReadOnly {
			get {
				return readOnlyCheckBox.Checked;
			}
			set {
				readOnlyCheckBox.Checked = value;
			}
		}
		public string SaveDirectory {
			get;
			set;
		}

		public string SelectedText {
			get { return ProgramTextBox.SelectedText; }
			set { ProgramTextBox.SelectedText = value; }
		}

		public TokenData TokenData {
			get {
				return _TokenData;
			}
			set {
				_TokenData = value;
				if (styles != null) {
					foreach (var s in styles.Values) {
						s.Dispose();
					}
				}
				styles = new Dictionary<string, TokenStyle>();
				foreach (TokenData.Style s in TokenData.Styles.Values) {
					styles.Add(s.Name, TokenStyle.FromTokenDataStyle(s));
				}

				if (styles.Count == 0) {
					styles.Add("Default", TokenStyle.Default);
					styles.Add("String", null);
					styles.Add("Comment", null);
					styles.Add("Disabled", null);
				}

				styles.Add("Error", ErrorStyle.Default);
				styles.Add("ErrorString", new ErrorStyle(styles["String"] ?? ErrorStyle.Default));
			}
		}

		private string CommentString { get { return TokenData.CommentString; } }

		private string DirectiveString { get { return TokenData.DirectiveString; } }

		private Font font { get; set; }

		private int LastLength { get; set; }

		private int NumberOfLines { get; set; }
		private bool StringFlag { get; set; }

		public Prog8xEditWindow(TokenData td, string fileName) {
			InitializeComponent();
			FirstFileFlag = false;
			TokenData = td;
			Dock = DockStyle.Fill;
			FileName = fileName;
			//ProgramName = new FileInfo(fileName).Name;
			LiveUpdate = true;
			//readOnlyPanel.Height = splitContainer1.Panel1.Height - BytesBox.Height;
			StringFlag = false;
			New = true;

			ProgramTextBox.HotkeysMapping[Keys.Control | Keys.Y] = FCTBAction.Redo;
		}

		public byte[] GenerateByteData(bool newLinesForComments, bool breakOnError) {
			List<List<TokenData.TokenDictionaryEntry>> toss;
			return GenerateByteData(newLinesForComments, breakOnError, out toss);
		}

		public byte[] GenerateByteData(bool newLinesForComments, bool breakOnError, out List<List<TokenData.TokenDictionaryEntry>> tokens, out int lengthWithoutComments) {
			List<List<Replacement>> replacements;
			return GenerateByteData(newLinesForComments, breakOnError, out tokens, out lengthWithoutComments, out replacements);
		}

		public byte[] GenerateByteData(bool newLinesForComments, bool breakOnError, out List<List<TokenData.TokenDictionaryEntry>> tokens, out int lengthWithoutComments, out List<List<Replacement>> replacements) {
			StringBuilder sb = new StringBuilder();
			replacements = new List<List<Replacement>>();
			int lineNumber = 0;
			Dictionary<string, string> directives = new Dictionary<string, string>();
			Dictionary<string, string> backward = new Dictionary<string, string>();
			int ifCount = 0;
			Stack<bool> ifFlag = new Stack<bool>();
			tokens = null;
			int numCommentLines = 0;
			try {
				for (int i = 0; i < ProgramTextBox.Lines.Count; i++) {
					string line = ProgramTextBox.Lines[i];
					lineNumber++;
					replacements.Add(new List<Replacement>());
					if (line.StartsWith(CommentString.ToString())) {
						if (newLinesForComments) {
							sb.AppendLine();
							numCommentLines++;
						}
						continue;
					}

					if (line.StartsWith(DirectiveString.ToString())) {
						HandlePreproc(line, directives, ref ifCount, ifFlag, breakOnError);
						if (newLinesForComments) {
							sb.AppendLine();
							numCommentLines++;
						}
						continue;
					}

					List<Replacement> lineReplacements = new List<Replacement>();
					//foreach (string key in directives.Keys) {
					//    if (directives[key] != null) {
					//        List<int> reps = line.Replace(key, directives[key], out line);
					//        lineReplacements.AddRange(reps.ConvertAll<Replacement>(replacement => new Replacement(replacement, key, directives[key])));
					//    }
					//}

					var reps = line.Replace(directives, out line);					
					lineReplacements.AddRange(reps.ConvertAll<Replacement>(replacement => new Replacement(replacement.Item1, replacement.Item2, directives[replacement.Item2])));
					
					replacements[lineNumber - 1] = lineReplacements;

					if (ifFlag.Count == 0 || ifFlag.Peek()) {
						if (i == ProgramTextBox.Lines.Count - 1) {
							sb.Append(line);
						} else {
							sb.AppendLine(line);
						}
					} else if (newLinesForComments && i != ProgramTextBox.Lines.Count - 1) {
						sb.AppendLine();
						numCommentLines++;
					}
				}
				if (ifCount != 0) {
					throw new PreprocessorException(string.Format("Reached end of program with {0} open {1}if directives.", ifCount, DirectiveString));
				}
			} catch (Exception ex) {
				if (breakOnError) {
					MessageBox.Show(string.Format("Unable to parse line {0}.\nError: {1}", lineNumber, ex.Message), "Building file failed.");
					lengthWithoutComments = 0;
					return null;
				}
			}
			//}
			try {
				string programText = sb.ToString();
				byte[] data = TokenData.Tokenize(programText, out _numTokens, out tokens, breakOnError);
				lengthWithoutComments = data.Length - numCommentLines;
				return data;
			} catch (TokenizationException ex) {
				MessageBox.Show(ex.Message, "Building file failed.");
				ProgramTextBox.Selection = new Range(ProgramTextBox, ex.Location, ex.InvalidString.Length);
				ProgramTextBox.DoSelectionVisible();
				lengthWithoutComments = 0;
				return null;
			} catch (Exception ex) {
				MessageBox.Show(ex.Message, "Building file failed.");
				lengthWithoutComments = 0;
				return null;
			}
		}

		public byte[] GenerateByteData(bool newLinesForComments, bool breakOnError, out List<List<TokenData.TokenDictionaryEntry>> tokens) {
			int toss;
			
			return GenerateByteData(newLinesForComments, breakOnError, out tokens, out toss);
		}

		public void HideLocked() {
			lockedBox.Visible = false;
		}

		public void RefreshBytes(bool setProgramText = true, byte[] data = null) {
			List<List<TokenData.TokenDictionaryEntry>> tokens;
			if (data == null) {
				int length;
				data = GenerateByteData(true, false, out tokens, out length);
				UpdateBytesLabel(length);
			}
			if (setProgramText) {
				ProgramText = TokenData.Detokenize(data, out tokens);
			} else {
				TokenData.Detokenize(data, out tokens);
			}
			//UpdateTokensBox(tokens);
		}

		public void Save() {
			string fileName = FileName;
			if (!FileName.EndsWith(".txt")) {
				int extensionLocation = fileName.LastIndexOf('.');
				if (extensionLocation != -1) {
					fileName = fileName.Remove(extensionLocation);
				}
				fileName = fileName + ".txt";
			}
			using (StreamWriter sw = new StreamWriter(fileName, false)) {
				sw.Write(ProgramText.Replace("\r", "").Replace("\n", Environment.NewLine));
			}
			Dirty = false;
		}

		private void archivedCheckBox_CheckedChanged(object sender, EventArgs e) {
			_program.Archived = archivedCheckBox.Checked;
		}

		private void HandlePreproc(string line, Dictionary<string, string> directives, ref int ifCount, Stack<bool> ifFlag, bool breakOnError) {
			if (string.IsNullOrWhiteSpace(line)) {
				return;
			}

			Dictionary<string, string> reverseLookup = null;
			string[] directive = line.Split(new[] { ' ' });
			string replaceString = null;
			if (directive.Length >= 3) {
				replaceString = string.Join(" ", directive.SubArray(2, directive.Length - 2)).Trim();
			} else if (directive.Length == 1 && directive[0] == DirectiveString + "define") {
				return;
			}

			switch (directive[0].Remove(0, DirectiveString.Length)) {
				case "define":
					if (directives.ContainsKey(directive[1])) {
						directives[directive[1]] = replaceString ?? directives[directive[1]];
					} else {
						directives.Add(directive[1], replaceString);
					}
					if (reverseLookup != null && !string.IsNullOrWhiteSpace(replaceString)) { reverseLookup.Add(replaceString, directive[1]); }
					break;

				case "undefine":
					directives.Remove(directive[1]);
					if (reverseLookup != null && !string.IsNullOrWhiteSpace(replaceString)) { reverseLookup.Remove(replaceString); }
					break;

				case "ifdef":
					if (directive.Length < 2) { return; }
					ifCount++;
					ifFlag.Push(directives.Keys.Contains(directive[1]));
					break;

				case "ifndef":
					if (directive.Length < 2) { return; }
					ifCount++;
					ifFlag.Push(!directives.Keys.Contains(directive[1]));
					break;

				case "else":
					if (ifCount == 0 && breakOnError) {
						throw new PreprocessorException(string.Format("Mismatched {0}if/{0}else directives.", DirectiveString));
					}
					ifFlag.Push(!ifFlag.Pop());
					break;

				case "elseifdef":
					if (ifCount == 0 && breakOnError) {
						throw new PreprocessorException(string.Format("Mismatched {0}if/{0}else directives.", DirectiveString));
					}
					if (directive.Length < 2) { return; }
					ifFlag.Push(directives.Keys.Contains(directive[1]));
					break;

				case "elseifndef":
					if (ifCount == 0 && breakOnError) {
						throw new PreprocessorException(string.Format("Mismatched {0}if/{0}else directives.", DirectiveString));
					}
					if (directive.Length < 2) { return; }
					ifFlag.Push(!directives.Keys.Contains(directive[1]));
					break;

				case "endif":
					if (ifCount == 0 && breakOnError) {
						throw new PreprocessorException(string.Format("Mismatched {0}if/{0}end directives.", DirectiveString));
					} else {
						ifCount--;
						ifFlag.Pop();
					}
					break;
			}
		}

		private void liveUpdateCheckBox_CheckedChanged(object sender, EventArgs e) {
			LiveUpdate = liveUpdateCheckBox.Checked;
			if (LiveUpdate) {
				RefreshBytes(false);
				FullHighlightRefresh();
			}
		}

		private void lockedBox_CheckedChanged(object sender, EventArgs e) {
			((Prog8x)_program).Locked = lockedBox.Checked;
		}

		private void Prog8xEditWindow_MouseHover(object sender, EventArgs e) {
		}

		public void FullHighlightRefresh() {
			UpdateHighlight(ProgramTextBox.Range);
		}

		private void ProgramText_TextChanged(object sender, TextChangedEventArgs e) {
			if (LiveUpdate) {
				RefreshBytes(false, UpdateHighlight(e.ChangedRange));
			}
			Dirty = true;
		}

		private byte[] UpdateHighlight(Range range) {
			List<List<TokenData.TokenDictionaryEntry>> tokens;
			int length;
			var replacements = new List<List<Replacement>>();
			byte[] byteData = GenerateByteData(true, false, out tokens, out length, out replacements);
			UpdateHighlight(tokens, range, replacements);
			UpdateBytesLabel(length);
			return byteData;
		}

		private void ProgramTextBox_DragEnter(object sender, DragEventArgs e) {
		}

		private void ProgramTextBox_Scroll(object sender, ScrollEventArgs e) {
		}

		private void ProgramTextBox_SelectionChangedDelayed(object sender, EventArgs e) {
			if (ProgramTextBox.SelectionLength == 0) {
				selectionLabel.Visible = false;
				bytesLabels.Visible = true;
				return;
			}

			selectionLabel.Visible = true;
			bytesLabels.Visible = false;

			int length;
			int numTokens;
			int bytes;

			string selectedText = ProgramTextBox.Selection.Text;
			bytes = TokenData.Tokenize(selectedText, out numTokens).Length;
			length = selectedText.Length;

			UpdateSelectionLabel(length, numTokens, bytes);
		}

		private void readOnlyCheckBox_CheckedChanged(object sender, EventArgs e) {
			ProgramTextBox.ReadOnly = readOnlyCheckBox.Checked;
			ProgramTextBox.BackColor = ProgramTextBox.ReadOnly ? SystemColors.Control : SystemColors.Window;
			ProgramTextBox.Update();
		}

		private void UpdateBytesLabel(int length) {
			int nameLength = OnCalcName.StartsWith("new file") ? 0 : OnCalcName.Length;
			bytesLabels.Text = string.Format("Bytes: {0}", length + 9 + nameLength);
		}

		private void UpdateHighlight(List<List<TokenData.TokenDictionaryEntry>> tokens, Range range, List<List<Replacement>> replacements) {
			Dictionary<string, string> PreProcForward = new Dictionary<string, string>();
			int ifCount = 0;
			var ifFlag = new Stack<bool>();

			// Do all the preproc up to this point?
			for (int i = 0; i < ProgramTextBox.LinesCount; i++) {
				HandlePreproc(ProgramTextBox.Lines[i].TrimStart(), PreProcForward, ref ifCount, ifFlag, false);
				PreProcForward = PreProcForward.OrderByDescending(v => v.Key.Length).ToDictionary(v => v.Key, v => v.Value);
			}

			Place place = new Place(0, range.Start.iLine);
			for (int i = range.Start.iLine; i <= range.End.iLine; i++) {
				if (i < 0 || i > ProgramTextBox.Lines.Count - 1) { continue; }
				var line = tokens[i];
				string programTextBoxLine = ProgramTextBox.Lines[place.iLine];
				//string tokenizedLine = string.Join("", tokens[i].Select(t => t.Name));

				if (ifFlag.Count > 0 && !ifFlag.Peek()) {
					Range curRange = ProgramTextBox.GetRange(place, place + ProgramTextBox.Lines[i].Length);
					curRange.ClearStyle(styles.Values.ToArray());
					curRange.SetStyle(styles["Disabled"] ?? styles["Default"]);
				} else if (programTextBoxLine.StartsWith(CommentString)) {
					Range curRange = ProgramTextBox.GetRange(place, place + ProgramTextBox.Lines[i].Length);
					curRange.ClearStyle(styles.Values.ToArray());
					curRange.SetStyle(styles["Comment"] ?? styles["Default"]);
				} else {
					StringFlag = false;
					int lastPrepocCount = 0;
					foreach (var entry in line) {
						if (lastPrepocCount > 0) {
							lastPrepocCount--;
							continue;
						}
						if (entry.StringTerminator && StringFlag) { StringFlag = false; } else if (entry.StringStarter) { StringFlag = true; }
						string token = entry.Name;
						if (place.iChar < programTextBoxLine.Length && programTextBoxLine[place.iChar] == '\\') {
							if (place.iChar != programTextBoxLine.Length) { place = place + 1; }
						}
						string lineText = ProgramTextBox.Lines[i].ClippedSubstring(place.iChar, token.Length);

						Replacement replacement = null;
						if (i < replacements.Count) {
							List<Replacement> lineReplacements = replacements[i];
							replacement = lineReplacements.FirstOrDefault(r => r.Location == place.iChar);
						}
						if (replacement != null) {
							token = replacement.OldValue;
							TokenData.Tokenize(replacement.NewValue, out lastPrepocCount);
							lastPrepocCount--;
						} else if (lineText != token) {
							foreach (string alt in entry.Alts) {
								lineText = ProgramTextBox.Lines[i].ClippedSubstring(place.iChar, alt.Length);
								if (lineText == alt) {
									token = alt;
									break;
								}
							}
						}
						int offset;
						if (place.iChar != 0 && place.iChar < programTextBoxLine.Length && programTextBoxLine[place.iChar - 1] == '\\') {
							offset = -1;
						} else {
							offset = 0;
						}

						Range curRange = ProgramTextBox.GetRange(place + offset, place + token.Length);
						//if (entry.Name == "For(" || entry.Name == "End") {
						//    curRange.ClearFoldingMarkers();
						//    curRange.SetFoldingMarkers("For", "End");
						//    curRange.SetFoldingMarkers("Then", "End");
						//    curRange.SetFoldingMarkers("While ", "End");
						//    curRange.SetFoldingMarkers("Repeat", "End");
						//}
						curRange.ClearStyle(styles.Values.ToArray());
						string styleKey;
						if (StringFlag || entry.StringStarter) {
							styleKey = "String";
							if (entry.StyleType == "Error") {
								styleKey = "Error" + styleKey;
							}
						} else {
							styleKey = entry.StyleType ?? "Default";
						}
						TokenStyle style = styles[styleKey];
						curRange.SetStyle(style ?? styles["Default"]);
						place += token.Length;
					}
				}
				place.iLine++;
				place.iChar = 0;
			}

			//ProgramTextBox.Invalidate();
		}

		private void UpdateSelectionLabel(int selectionLength, int selectionTokens, int selectionBytes) {
			selectionLabel.Text = string.Format("Selection: {0} characters, {1} tokens, {2} bytes", selectionLength, selectionTokens, selectionBytes);
		}
	}
}