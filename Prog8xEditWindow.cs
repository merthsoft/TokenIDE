using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Merthsoft.Tokens;
using System.IO;
using Merthsoft.CalcData;
using System.Threading;
using FastColoredTextBoxNS;

namespace TokenIDE {
	public partial class Prog8xEditWindow : UserControl, IEditWindow {
		Dictionary<string, TokenStyle> styles;
		
		public TabPage ParentTabPage { get; set; }

		public bool FirstFileFlag { get; set; }

		public bool ReadOnly {
			get {
				return readOnlyCheckBox.Checked;
			}
			set {
				readOnlyCheckBox.Checked = value;
			}
		}

		public bool HasSaved {
			get;
			set;
		}

		public bool New { get; set; }

		private bool dirty;
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

		public bool Opening { get; set; }

		public bool LiveUpdate { get; set; }

		private int NumberOfLines { get; set; }

		public Var8x CalcVar { get { return Program; } set { Program = (TokenizeableVar8x)value; } }

		private TokenizeableVar8x _program;
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
					//BytesBox.Text = BitConverter.ToString(data).Replace("-", "").Replace("3F", Environment.NewLine);
					List<List<TokenData.TokenDictionaryEntry>> tokens;
					ProgramText = TokenData.Detokenize(data, out tokens);
					//UpdateTokensBox(tokens);
				}
				archivedCheckBox.Checked = _program.Archived;
				if (_program is Prog8x) {
					lockedBox.Checked = ((Prog8x)_program).Locked;
				} else {
					HideLocked();
				}
			}
		}

		public string SaveDirectory {
			get;
			set;
		}

		//private string _FileName;
		public string FileName {
			get ;
			//{
			//    return _FileName;
			//}
			set; 
			//{
			//    if (string.IsNullOrWhiteSpace(value)) {
			//        _FileName = "";
			//    } else {
			//        FileInfo fileInfo = new FileInfo(value);
			//        string ext = fileInfo.Extension;
			//        string fileInfoName = fileInfo.Name;
			//        if (!string.IsNullOrWhiteSpace(ext)) {
			//            _FileName = fileInfoName.Substring(0, fileInfoName.Length - ext.Length);
			//        } else {
			//            _FileName = fileInfoName;
			//        }
			//        //if (ParentTabPage != null) {
			//        //    ParentTabPage.Text = _FileName;
			//        //}
			//        if (!string.IsNullOrWhiteSpace(fileInfo.DirectoryName)) {
			//            SaveDirectory = fileInfo.DirectoryName;
			//        }
			//    }
			//}
		}

		private string _programName;
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

		private void UpdateBytesLabel(int length) {
			int nameLength = OnCalcName.StartsWith("new file") ? 0 : OnCalcName.Length;
			bytesLabels.Text = string.Format("Bytes: {0}", length + 9 + nameLength);
		}

		private void UpdateSelectionLabel(int selectionLength, int selectionTokens, int selectionBytes) {
			selectionLabel.Text = string.Format("Selection: {0} characters, {1} tokens, {2} bytes", selectionLength, selectionTokens, selectionBytes);
		}

		private Font font { get; set; }

		private TokenData _TokenData;
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
				
				//Font newFont = new Font(TokenData.FontFamily, TokenData.FontSize);
				
				//if (font != null && !font.Equals(newFont)) { font.Dispose(); }
				//font = newFont;

				//ProgramTextBox.Font = font;
				//TokensBox.Font = font;
			}
		}

		private string CommentString { get { return TokenData.CommentString; } }
		private string DirectiveString { get { return TokenData.DirectiveString; } }

		private int _numTokens;
		public int NumTokens { get { return _numTokens; } }

		public string ProgramText {
			get { return ProgramTextBox.Text; }
			set { ProgramTextBox.Text = value; LastLength = value.Length; }
		}

		public string[] ProgramLines {
			get { return ProgramTextBox.Lines.ToArray(); }
		}

		//public string TokensText {
		//    get { return TokensBox.Text; }
		//}

		//public string[] TokensLines {
		//    get { return TokensBox.Lines.ToArray(); }
		//}

		public string SelectedText {
			get { return ProgramTextBox.SelectedText; }
			set { ProgramTextBox.SelectedText = value; }
		}

		private int LastLength { get; set; }

		private bool StringFlag { get; set; }

		//public string ByteText {
		//    get { return BytesBox.Text; }
		//}

		public byte[] ByteData { get { return GenerateByteData(false, true); } }

		public byte[] GenerateByteData(bool newLinesForComments, bool breakOnError) {
			List<List<TokenData.TokenDictionaryEntry>> toss;
			return GenerateByteData(newLinesForComments, breakOnError, out toss);
		}

		public byte[] GenerateByteData(bool newLinesForComments, bool breakOnError, out List<List<TokenData.TokenDictionaryEntry>> tokens) {
			StringBuilder sb = new StringBuilder();
			//using (StringReader sr = new StringReader(ProgramText)) {
			//string line;
			int lineNumber = 0;
			Dictionary<string, string> directives = new Dictionary<string, string>();
			Dictionary<string, string> backward = new Dictionary<string, string>();
			int ifCount = 0;
			Stack<bool> ifFlag = new Stack<bool>();
			tokens = null;
			try {
				//while ((line = sr.ReadLine()) != null) {
				for (int i = 0; i < ProgramTextBox.Lines.Count; i++) {
					string line = ProgramTextBox.Lines[i];
					lineNumber++;
					if (!line.StartsWith(CommentString.ToString())) {
						if (!line.StartsWith(DirectiveString.ToString())) {
							foreach (string key in directives.Keys) {
								if (directives[key] != null) {
									line = line.Replace(key, directives[key]);
								}
							}
							if (ifFlag.Count == 0 || ifFlag.Peek()) {
								if (i != ProgramTextBox.Lines.Count - 1) {
									sb.AppendLine(line);
								} else {
									sb.Append(line);
								}
							} else if (newLinesForComments) {
								if (i != ProgramTextBox.Lines.Count - 1) {
									sb.AppendLine();
								}
							}
						} else {
							HandlePreproc(line, directives, null, ref ifCount, ifFlag, breakOnError);
							if (newLinesForComments) sb.AppendLine();
						}
					} else {
						if (newLinesForComments) sb.AppendLine();
					}
				}
				if (ifCount != 0) {
					throw new PreprocessorException(string.Format("Reached end of program with {0} open {1}if directives.", ifCount, DirectiveString));
				}
			} catch (Exception ex) {
				if (breakOnError) {
					MessageBox.Show(string.Format("Unable to parse line {0}.\nError: {1}", lineNumber, ex.Message), "Building file failed.");
					return null;
				}
			}
			//}
			try {
				string programText = sb.ToString();
				return TokenData.Tokenize(programText, out _numTokens, out tokens, breakOnError);
			} catch (TokenizationException ex) {
				MessageBox.Show(ex.Message, "Building file failed.");
				//ProgramTextBox.Selection = new Range(ProgramTextBox
				ProgramTextBox.Selection = new Range(ProgramTextBox, ex.Location, ex.InvalidString.Length);
				ProgramTextBox.DoSelectionVisible();
				return null;
			} catch (Exception ex) {
				MessageBox.Show(ex.Message, "Building file failed.");
				return null;
			}
		}

		private void HandlePreproc(string line, Dictionary<string, string> directives, Dictionary<string, string> reverseLookup, ref int ifCount, Stack<bool> ifFlag, bool breakOnError) {
			if (string.IsNullOrWhiteSpace(line)) {
				return;
			}

			string[] directive = line.Split(new[] {' '});
			string replaceString = null;
			if (directive.Length >= 3) {
				replaceString = string.Join(" ", directive.SubArray(2, directive.Length - 2));
			} else if (directive.Length == 1 && directive[0] == DirectiveString + "define") {
				return;
			}

			switch (directive[0].Remove(0, DirectiveString.Length)) {
				case "define":
					directives.Add(directive[1], replaceString);
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
		}

		private void ProgramText_TextChanged(object sender, TextChangedEventArgs e) {
			if (LiveUpdate) {
				List<List<TokenData.TokenDictionaryEntry>> tokens;
				byte[] byteData = GenerateByteData(true, false, out tokens);
				//TokenData.Detokenize(byteData, out tokens);
				UpdateHighlight(tokens, e.ChangedRange);
				UpdateBytesLabel(byteData.Length);
				RefreshBytes(false, byteData);
			}
			Dirty = true;
		}

		public void FullHighlightRefresh() {
			List<List<TokenData.TokenDictionaryEntry>> tokens;
			byte[] byteData = GenerateByteData(true, false, out tokens);
			//TokenData.Detokenize(byteData, out tokens);
			UpdateHighlight(tokens, ProgramTextBox.Range);
			//ProgramTextBox.Invalidate();
		}

		private void UpdateHighlight(List<List<TokenData.TokenDictionaryEntry>> tokens, Range range) {
			Dictionary<string, string> PreProcForward = new Dictionary<string, string>();
			Dictionary<string, string> PreProcBackward = new Dictionary<string, string>();
			int ifCount = 0;
			var ifFlag = new Stack<bool>();

			// Do all the preproc up to this point?
			for (int i = 0; i < ProgramTextBox.LinesCount; i++) {
			    HandlePreproc(ProgramTextBox.Lines[i].TrimStart(), PreProcForward, PreProcBackward, ref ifCount, ifFlag, false);
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
					foreach (var entry in line) {
						if (entry.StringTerminator && StringFlag) { StringFlag = false; }
						else if (entry.StringStarter) { StringFlag = true; }
						string token = entry.Name;
						if (place.iChar < programTextBoxLine.Length && programTextBoxLine[place.iChar] == '\\') {
							if (place.iChar != programTextBoxLine.Length) { place = place + 1; }
						}
						string lineText = ProgramTextBox.Lines[i].ClippedSubstring(place.iChar, token.Length);

						if (PreProcBackward.ContainsKey(token) && token != lineText) {
							//lineText = ProgramTextBox.Lines[i].ClippedSubstring(place.iChar, alt.Length);
							token = PreProcBackward[token];
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

//☃☃☃☃☃☃
//Disp "☃☃☃☃☃☃☃☃"
//Disp "\Disp "
//Text(9,16,sub(" \|>=>|>Sigma\|<=<<|3",F,1

		//private void UpdateTokensBox(List<List<TokenData.TokenDictionaryEntry>> tokens) {
		//    StringBuilder sb = new StringBuilder(LastLength + 1024);
			
		//    foreach (var line in tokens) {
		//        foreach (var entry in line) {
		//            sb.Append("[");
		//            sb.Append(entry.Name);
		//            sb.Append("]");
		//        };
		//        sb.AppendLine();
		//    }
		//    sb.Length -= Environment.NewLine.Length;
		//    LastLength = sb.Length;
		//    TokensBox.Text = sb.ToString();
		//}

		public void RefreshBytes(bool setProgramText = true, byte[] data = null) {
			//byte[] data = TokenData.Tokenize(ProgramTextBox.Text, out _numTokens);
			List<List<TokenData.TokenDictionaryEntry>> tokens;
			if (data == null) {
				data = GenerateByteData(true, false, out tokens);
				UpdateBytesLabel(data.Length);
			}
			if (setProgramText) {
				ProgramText = TokenData.Detokenize(data, out tokens);
			} else {
				TokenData.Detokenize(data, out tokens);
			}
			//UpdateTokensBox(tokens);
		}

		private void readOnlyCheckBox_CheckedChanged(object sender, EventArgs e) {
			ProgramTextBox.ReadOnly = readOnlyCheckBox.Checked;
			ProgramTextBox.BackColor = ProgramTextBox.ReadOnly ? SystemColors.Control : SystemColors.Window;
			ProgramTextBox.Update();
		}

		private void lockedBox_CheckedChanged(object sender, EventArgs e) {
			((Prog8x)_program).Locked = lockedBox.Checked;
		}

		private void archivedCheckBox_CheckedChanged(object sender, EventArgs e) {
			_program.Archived = archivedCheckBox.Checked;
		}

		public void HideLocked() {
			lockedBox.Visible = false;
		}

		private void liveUpdateCheckBox_CheckedChanged(object sender, EventArgs e) {
			LiveUpdate = liveUpdateCheckBox.Checked;
			if (LiveUpdate) {
				RefreshBytes(false);
				FullHighlightRefresh();
			}
		}

		public void Save() {
			string fileName = FileName;
			if (!FileName.EndsWith(".txt")) { fileName = FileName + ".txt"; }
			using (StreamWriter sw = new StreamWriter(fileName, false)) {
				sw.Write(ProgramText.Replace("\r", "").Replace("\n", Environment.NewLine));
			}
			Dirty = false;
		}

		private void ProgramTextBox_Scroll(object sender, ScrollEventArgs e) {
			if (e.ScrollOrientation == ScrollOrientation.VerticalScroll) {
				//TokensBox.VerticalScroll.Value = e.NewValue;
				//TokensBox.Invalidate();
			}
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

		private void ProgramTextBox_DragEnter(object sender, DragEventArgs e) {
		}

		private void Prog8xEditWindow_MouseHover(object sender, EventArgs e) {

		}
	}
}
