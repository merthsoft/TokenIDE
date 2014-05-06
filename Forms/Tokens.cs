using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Schema;
using System.Linq;
using Merthsoft.CalcData;
using Merthsoft.DynamicConfig;
using Merthsoft.TokenIDE.Forms;
using Merthsoft.TokenIDE.Project;
using Merthsoft.TokenIDE.Properties;
using Merthsoft.Tokens;
using Merthsoft.Tokens.DCSGUI;

namespace Merthsoft.TokenIDE {
	public partial class Tokens : Form {
		private bool prettyPrint;
		private TokenData _tokenData;
		private dynamic config;
		private IEditWindow currWindow;
		//UserControl currWindow;
		private Dictionary<ToolStripMenuItem, Process> externalTools = new Dictionary<ToolStripMenuItem, Process>();

		private Font editorFont;
		private List<TokensProject> projects;

		private int NumWindows {
			get {
				return EditWindows.TabPages.Count;
			}
		}

		private TokenData TokenData {
			get { return _tokenData; }
			set {
				_tokenData = value;
				TokensTree.Nodes.Clear();
				commentBox.Clear();
				List<string> groups = _tokenData.GroupNames;
				groups.Sort();
				foreach (string group in groups) {
					TreeNode n = new TreeNode(group);
					TokensTree.Nodes.Add(n);
					List<Merthsoft.Tokens.TokenData.GroupEntry> inGroup = _tokenData.GetAllInGroup(group);
					inGroup.Sort();
					foreach (Merthsoft.Tokens.TokenData.GroupEntry token in inGroup) {
						TreeNode tNode = new TreeNode(token.ToString());
						tNode.Tag = token;
						n.Nodes.Add(tNode);
					}
				}
				if (TokensTree.Nodes.Count > 0) {
					TokensTree.SelectedNode = TokensTree.Nodes[0];
				}
			}
		}

		public Tokens(string[] files) {
			InitializeComponent();

			EditWindows.TabClose += EditWindows_TabClose;

			statusLabel.Text = "";
			//TokenData = new TokenData("Tokens.xml");
			//Environment.CurrentDirectory = Application.StartupPath;
			config = Config.ReadIni("TokenIDE.ini");

			if (!OpenTokensFile(Path.Combine(Application.StartupPath, config.TokenIDE.file.ToString()))) {
				MessageBox.Show(string.Format("Unable to load default file {0}, quitting.", config.TokenIDE.file.ToString()), "TokenIDE");
				return;
			}
			editorFont = new Font(config.Font.family, float.Parse(config.Font.size));

			try { prettyPrint = bool.Parse(config.TokenIDE.prettyPrint); } catch { }

			AddNewTab();

			foreach (string file in files) {
				try {
					OpenFile(file);
				} catch (Exception e) {
					MessageBox.Show(string.Format("Could not open file {0}: {1}.", file, e.Message));
				}
			}

			projects = new List<TokensProject>();

			IDictionary<string, object> tools = config.Tools;
			int keyNumber = 0;
			foreach (var tool in tools) {
				string value = tool.Value.ToString();
				char split = value[0];
				string[] vals = value.Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries);
				ToolStripMenuItem t = new ToolStripMenuItem(vals[0]);
				Keys key = (Keys)((int)Keys.D1 + (keyNumber++ % 10));
				if (keyNumber > 9) {
					key = key | Keys.Shift;
				}
				if (keyNumber > 19) {
					MessageBox.Show("You can't have more than 20 external tools.");
					break;
				}

				t.ShortcutKeys = Keys.Control | key;
				t.ShowShortcutKeys = true;
				t.Tag = vals;
				t.Click += new EventHandler(launchExternalTool);

				externalToolsToolStripMenuItem.DropDownItems.Add(t);
			}
		}

		void EditWindows_TabClose(object sender, TabCloseEventArgs e) {
			if (e.TabPage == null || e.TabPage.Controls.Count == 0) {
				return;
			}
			IEditWindow editWindow = e.TabPage.Controls[0] as IEditWindow;
			if (editWindow == null) {
				e.Cancel = true;
				return;
			}
			e.Cancel = !closeWindow(editWindow);
		}

		public void build(Var8x.VarType? varType, Var8x.CalcType? calcType, IEditWindow window, string dir, int numTokens, byte[] rawData, bool archived, bool locked = false) {
			string name = window.OnCalcName;
			Var8x var = null;
			string ext = getExtension(varType, calcType);

			FileInfo fi = new FileInfo(window.FileName);
			if (!string.IsNullOrWhiteSpace(fi.Extension)) {
				window.FileName = window.FileName.Remove(window.FileName.Length - fi.Extension.Length);
			}
			window.FileName += ext;

			using (StreamWriter s = new StreamWriter(Path.Combine(dir, name + ext))) {
				if (!(varType.HasValue && calcType.HasValue)) {
					s.BaseStream.Write(rawData, 0, rawData.Length);
				} else {
					if (varType == Var8x.VarType.AppVar) {
						var = new AppVar8x(name, calcType.Value);
					} else {
						var = new Prog8x(name, calcType.Value);
						((Prog8x)var).Locked = locked;
					}
					var.Archived = archived;

					var.SetData(new object[] { numTokens.ToString(), rawData });
					var.Save(new BinaryWriter(s.BaseStream));
				}
			}
		}

		public void dumpTokens(Dictionary<byte, TokenData.TokenDictionaryEntry> data, StringBuilder sb) {
			foreach (var token in data) {
				if (token.Value.Name != null) {
					sb.AppendFormat(string.Format("{0} - {1}", token.Value.Bytes, token.Value.Name.Replace("{", "{{").Replace("}", "}}").Replace("\\", "\\\\")));
				}
				sb.AppendLine();
				dumpTokens(token.Value.SubTokens, sb);
			}
		}

		public string getExtension(Var8x.VarType? varType, Var8x.CalcType? calcType) {
			if (varType == null && calcType == null) {
				return ".bin";
			}

			string prefix = null;
			string suffix = null;
			switch (calcType) {
				case Var8x.CalcType.Calc8x:
					prefix = "8x";
					break;

				case Var8x.CalcType.Calc83:
					prefix = "83";
					break;

				case Var8x.CalcType.Calc82:
					prefix = "82";
					break;

				case Var8x.CalcType.Calc73:
					prefix = "73";
					break;

				case Var8x.CalcType.Calc85:
					prefix = "85";
					break;

				default:
					break;
			}

			switch (varType) {
				case Var8x.VarType.AppVar:
					suffix = "v";
					break;

				case Var8x.VarType.Program:
				case Var8x.VarType.ProgramLocked:
					suffix = "p";
					break;
			}

			return "." + prefix + suffix;
		}

		private static void AddProjectItem(TreeNode treeNode, ProjectFile projItem) {
			TreeNode progNode = new TreeNode(projItem.ToString());
			progNode.Tag = projItem;
			progNode.ImageKey = progNode.SelectedImageKey = treeNode.Text == "Programs" ? "icon_prog.png" : "icon_project_appvars.png";
			treeNode.Nodes.Add(progNode);
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			AboutBox1 a = new AboutBox1();
			a.ShowDialog();
		}

		private void AddMemorySection(PojectSection mem, TreeNode treeNode) {
			TreeNode programsNode = new TreeNode("Programs");
			AddProjectItems(mem.Programs, programsNode);
			treeNode.Nodes.Add(programsNode);

			TreeNode appVarNodes = new TreeNode("AppVars");
			appVarNodes.Tag = mem.Programs;
			appVarNodes.ContextMenuStrip = addItemContextMenuStrip;
			appVarNodes.ImageKey = appVarNodes.SelectedImageKey = "icon_project_appvars.png";
			foreach (var appVarItem in mem.AppVars) {
				TreeNode appVarNode = new TreeNode(appVarItem.ToString());
				appVarNode.Tag = appVarItem;
				appVarNode.ImageKey = appVarNode.SelectedImageKey = "icon_appvar.png";
				appVarNodes.Nodes.Add(appVarNode);
			}
			treeNode.Nodes.Add(appVarNodes);
		}

		private void AddNewTab() {
			TabPage tp = new TabPage("new file");
			tp.AllowDrop = true;
			Prog8xEditWindow ew = new Prog8xEditWindow(TokenData, "new file");
			ew.FirstFileFlag = true;
			ew.Program = new Prog8x("");
			ew.ParentTabPage = tp;
			tp.Controls.Add(ew);
			EditWindows.TabPages.Add(tp);
			currWindow = ew;

			ew.ProgramTextBox.Font = editorFont;
			ew.PrettyPrint = prettyPrint;
			ew.DragEnter += TokenIDE_DragEnter;
			ew.DragDrop += TokenIDE_DragDrop;
		}

		private void AddProject(TokensProject proj) {
			leftTabControl.SelectedTab = ProjectTab;

			projects.Add(proj);

			TreeNode projectNode = new TreeNode(proj.Name);
			projectNode.ImageKey = "icon_project.png";
			projectNode.Tag = proj;

			TreeNode ramNode = new TreeNode("RAM");
			ramNode.ImageKey = "icon_project_blank.png";
			ramNode.Tag = proj.Ram;
			AddMemorySection(proj.Ram, ramNode);
			projectNode.Nodes.Add(ramNode);

			TreeNode archiveNode = new TreeNode("Archive");
			archiveNode.ImageKey = "icon_project_blank.png";
			archiveNode.Tag = proj.Archive;
			AddMemorySection(proj.Archive, archiveNode);
			projectNode.Nodes.Add(archiveNode);

			projectTree.Nodes.Add(projectNode);
			projectNode.Expand();
		}

		private void AddProjectItems(List<ProjectFile> section, TreeNode treeNode) {
			treeNode.Tag = section;
			treeNode.ContextMenuStrip = addItemContextMenuStrip;
			treeNode.ImageKey = treeNode.SelectedImageKey = treeNode.Text == "Programs" ? "icon_project_progs.png" : "icon_project_appvars.png";
			foreach (ProjectFile projItem in section) {
				AddProjectItem(treeNode, projItem);
			}
		}

		private void binaryFileToolStripMenuItem_Click(object sender, EventArgs e) {
			buildFile(null, null);
		}

		private void blackAndWhiteToolStripMenuItem_Click(object sender, EventArgs e) {
			hexSprite(false);
		}

		private void blockCountsToolStripMenuItem_Click(object sender, EventArgs e) {
			Prog8xEditWindow editWindow = currWindow as Prog8xEditWindow;
			if (currWindow == null) { return; }

			GroupStats gs = new GroupStats(editWindow);
			gs.Show();
		}

		private void buildAllToolStripMenuItem_Click(object sender, EventArgs e) {
		}

		private void buildFile(Var8x.VarType? varType, Var8x.CalcType? calcType) {
			var prevCursor = Cursor;
			Cursor = Cursors.WaitCursor;
			string dir;
			byte[] data = setUpSave(out dir);
			if (data == null) {
				return;
			}
			try {
				bool locked = false;
				if (currWindow is Prog8xEditWindow) {
					locked = ((Prog8xEditWindow)currWindow).Locked;
				}

				build(varType, calcType, currWindow, dir, currWindow.NumTokens, data, currWindow.Archived, locked);
				statusLabel.Text = "Build succeeded";
			} catch (Exception ex) {
				statusLabel.Text = string.Concat("Build failed: ", ex.Message);
			}
			Cursor = prevCursor;
		}

		private void changeSaveDirectoryToolStripMenuItem_Click(object sender, EventArgs e) {
			//var fbd = new FolderSelectDialog() {
			//    Title = "Save Directory",
			//    InitialDirectory = Environment.CurrentDirectory,
			//};
			//if (!fbd.ShowDialog()) {
			//    return;
			//}

			var fbd = new OpenFileDialog() {
				InitialDirectory = Environment.CurrentDirectory,
				ValidateNames = false,
				CheckFileExists = false,
				CheckPathExists = false,
				FileName = "Folder Selection.",
			};

			if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) {
				return;
			}

			currWindow.SaveDirectory = fbd.FileName;
		}

		private void changeTokenFileToolStripMenuItem_Click(object sender, EventArgs e) {
			OpenFileDialog f = new OpenFileDialog();
			f.AddFilter("Token Files", "*.xml");
			if (f.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) { return; }
			string xmlFileName = f.FileName;
			OpenTokensFile(xmlFileName);
		}

		private void CloseTab() {
			closeWindow(currWindow);
		}

		private bool closeWindow(IEditWindow window) {
			if (window.Dirty) {
				if (MessageBox.Show("File has not been saved, are you sure you want to exit?", "Exit?",
					MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No) {
					return false;
				}
			}
			EditWindows.TabPages.Remove(window.ParentTabPage);
			return true;
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e) {
			CloseTab();
		}

		private void colorSpritesToolStripMenuItem_Click(object sender, EventArgs e) {
			hexSprite(true);
		}

		private void CompileToolStripMenuItem_Click(object sender, EventArgs e) {
			Var8x.VarType varType = Var8x.VarType.Program;
			Var8x.CalcType calcType = Var8x.CalcType.Calc8x;
			buildFile(varType, calcType);
		}

		private void dCSGuiDesignerToolStripMenuItem_Click(object sender, EventArgs e) {
			if (!(currWindow is Prog8xEditWindow)) { return; }
			Prog8xEditWindow ew = (Prog8xEditWindow)currWindow;

			DCSGUIDesigner d = new DCSGUIDesigner();
			d.ShowDialog();
			if (d.outControls == null)
				return;
			string s = "OpenGUIStack(\n";
			foreach (GUIItem dgi in d.outControls) {
				s += dgi.GetOutString() + "\n";
			}
			s += "RenderGUI(";
			ew.SelectedText = s;
		}

		private void dumpTokensToolStripMenuItem_Click(object sender, EventArgs e) {
			Prog8xEditWindow window = currWindow as Prog8xEditWindow;
			if (window == null) { return; }
			TokenData data = window.TokenData;
			StringBuilder sb = new StringBuilder();
			dumpTokens(data.Tokens, sb);

			window.ProgramText = sb.ToString();
		}

		private void EditWindows_MouseClick(object sender, MouseEventArgs e) {
			//if (e.Button == System.Windows.Forms.MouseButtons.Middle) {
			//        CloseTab(EditWindows.Tab);
			//}
		}

		private void EditWindows_SelectedIndexChanged(object sender, EventArgs e) {
			if (EditWindows.TabPages.Count > 0) {
				currWindow = (IEditWindow)EditWindows.SelectedTab.Controls[0];
			} else {
				currWindow = null;
			}
		}

		private void existingItemToolStripMenuItem_Click(object sender, EventArgs e) {
			TokensProject project = GetProject(projectTree.SelectedNode.Parent.Parent);
			var fileDialog = new OpenFileDialog() {
				InitialDirectory = project.BaseDirectory,
				Filter = "Text Files (*.txt)|*.txt",
				CheckFileExists = true,
				Multiselect = true,
			};
			if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) { return; }

			List<ProjectFile> section = (List<ProjectFile>)projectTree.SelectedNode.Tag;
			foreach (string fileName in fileDialog.FileNames) {
				FileInfo fileInfo = new FileInfo(fileName);
				string projectFilePath = Path.Combine(project.BaseDirectory, fileInfo.Name);
				if (!File.Exists(projectFilePath)) {
					File.Copy(fileName, projectFilePath);
				}

				var newProjectFile = new ProjectFile() { Path = fileInfo.Name };

				//string outName = fileInfo.FileName() + projectTree.SelectedNode.Text == "Program" ? ".8xp" : ".8xk";
				//newProjectFile.Output = Path.Combine(project.OutDirectory, fileName);
				section.Add(newProjectFile);
				AddProjectItem(projectTree.SelectedNode, newProjectFile);
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void fileToolStripMenuItem2_Click(object sender, EventArgs e) {
			AddNewTab();
		}

		private void findToolStripMenuItem_Click(object sender, EventArgs e) {
			//Find f = new Find(currWindow);
			//f.Show();
			if (currWindow is Prog8xEditWindow) {
				var ew = (Prog8xEditWindow)currWindow;
				FastColoredTextBoxNS.FindForm f = new FastColoredTextBoxNS.FindForm(ew.ProgramTextBox);
				f.Show();
			}
		}

		private TokensProject GetProject(TreeNode node) {
			if (node.Parent == null) {
				return (TokensProject)node.Tag;
			}
			return GetProject(node.Parent);
		}

		private void hexSprite(bool color) {
			if (!(currWindow is Prog8xEditWindow)) { return; }
			Prog8xEditWindow ew = (Prog8xEditWindow)currWindow;

#if !DEBUG
			try {
#endif
			HexSprite s = new HexSprite();

			if (color) {
				s.SpriteHeight = s.SpriteWidth = 32;
				s.SelectedPalette = HexSprite.Palette.BasicColors;
			} else {
				s.SpriteHeight = s.SpriteWidth = 8;
				s.SelectedPalette = HexSprite.Palette.BlackAndWhite;
			}

			if (ew.SelectedText != "") {
				string hexString = ew.SelectedText.Trim().Replace("\"", "").Replace("(", "").Replace(")", "").Replace(",", "");
				try { s.Hex = hexString; } catch {
					MessageBox.Show(string.Format("Unable to create sprite from {0}.", hexString));
					return;
				}
			}

			s.PasteTextEvent += handlePasteEvent;
			s.Show();
#if !DEBUG
			} catch (Exception ex) {
				MessageBox.Show(ex.ToString());
			}
#endif
		}

		private void hexSpriteEditorToolStripMenuItem_Click(object sender, EventArgs e) {
		}

		private void hexViewToolStripMenuItem_Click(object sender, EventArgs e) {
			Prog8xEditWindow window = currWindow as Prog8xEditWindow;
			if (window == null) { return; }
			List<List<TokenData.TokenDictionaryEntry>> tokens;
			window.GenerateByteData(false, false, out tokens);
			StringBuilder sb = new StringBuilder();
			foreach (var line in tokens) {
				foreach (var token in line) {
					sb.AppendFormat("[{0}:{1}] ", token.Name, token.Bytes);
				}
				sb.AppendLine();
			}

			TextBox tb = new TextBox();
			tb.Dock = DockStyle.Fill;
			tb.Multiline = true;
			tb.ScrollBars = ScrollBars.Both;
			tb.Font = editorFont;
			tb.WordWrap = false;
			tb.Text = sb.ToString();

			Form f = new Form();
			f.Text = "Hex View";
			f.Controls.Add(tb);

			f.Show();
		}

		private void imageEditorToolStripMenuItem_Click(object sender, EventArgs e) {
			if (!(currWindow is Prog8xEditWindow)) { return; }
			Prog8xEditWindow ew = (Prog8xEditWindow)currWindow;

#if !DEBUG
			try {
#endif
			HexSprite s = new HexSprite();

			s.SpriteHeight = 64;
			s.SpriteWidth = 96;
			s.SelectedPalette = HexSprite.Palette.BlackAndWhite;

			s.PasteTextEvent += handlePasteEvent;
			s.Show();
#if !DEBUG
			} catch (Exception ex) {
				MessageBox.Show(ex.ToString());
			}
#endif
		}

		private void oldImageEditorToolStripMenuItem_Click(object sender, EventArgs e) {
			if (!(currWindow is Prog8xEditWindow)) { return; }
			Prog8xEditWindow ew = (Prog8xEditWindow)currWindow;

			ImageEditor i = new ImageEditor();
			i.ShowDialog();
			if (i.OutStrings != null) {
				string s = "";
				foreach (string str in i.OutStrings) {
					s += str + "\n";
				}
				ew.SelectedText = s;
			}
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			if (docLinkLabel.Text == "") { return; }
			System.Diagnostics.Process.Start(docLinkLabel.Text);
		}

		private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
		}

		private void navigateToDocsToolStripMenuItem_Click(object sender, EventArgs e) {
			TreeNode n = TokensTree.SelectedNode;
			while (!TokenData.Sites.ContainsKey(n.Text) && n.Parent != null) {
				n = n.Parent;
			}
			if (!TokenData.Sites.ContainsKey(n.Text)) {
				MessageBox.Show(string.Format("No online documentation available for {0}.", TokensTree.SelectedNode.Text));
				return;
			}
			System.Diagnostics.Process.Start(TokenData.Sites[n.Text]);
		}

		private void OpenFile(string fileName) {
			foreach (TabPage tab in EditWindows.TabPages) {
				if (tab.Controls[0] is IEditWindow && ((IEditWindow)tab.Controls[0]).FileName == fileName) {
					EditWindows.SelectedTab = tab;
					return;
				}
			}

			FileInfo fi = new FileInfo(fileName);
			TabPage tp = new TabPage();
			tp.AllowDrop = true;
			Prog8xEditWindow ewProg;
			List8xEditWindow ewList;
			tp.Text = fi.Name;
			IEditWindow prevWindow = currWindow;
			string ext = fi.Extension.ToLower();
			switch (ext) {
				case ".8xp":
				case ".83p":
				case ".82p":
				case ".73p":
				case ".85p":
					TokenData tokenData = new Merthsoft.Tokens.TokenData((string)((IDictionary<string, object>)(config.Extensions))[ext.Substring(1)]);
					currWindow = ewProg = new Prog8xEditWindow(tokenData, fileName);
					if (fi.Length > short.MaxValue / 4) {
						ewProg.LiveUpdate = false;
					}
					using (FileStream pstream = new FileStream(fileName, FileMode.Open))
					using (BinaryReader preader = new BinaryReader(pstream)) {
						Prog8x newProg8x = new Prog8x(preader);
						if (newProg8x.IsAsm) {
							if (MessageBox.Show(
									string.Format("{0} is an assembly program, are you sure you want to open it?", fileName),
									"ASM Program Detected", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation
								) == System.Windows.Forms.DialogResult.No) {
								currWindow = prevWindow;
								return;
							}
						}
						ewProg.Program = newProg8x;
					}

					ewProg.FullHighlightRefresh();
					tp.Controls.Add(ewProg);
					ewProg.ParentTabPage = tp;
					ewProg.ReadOnly = false;
					ewProg.ProgramTextBox.Font = editorFont;
					ewProg.PrettyPrint = prettyPrint;
					break;

				case ".txt":
					currWindow = ewProg = new Prog8xEditWindow(TokenData, fileName);
					if (fi.Length > short.MaxValue / 4) {
						ewProg.LiveUpdate = false;
					}
					ewProg.Program = new Prog8x(fi.Name.Split('.')[0]);
					using (StreamReader sr = new StreamReader(fileName)) {
						ewProg.ProgramText = sr.ReadToEnd();
					}
					ewProg.RefreshBytes(false);
					ewProg.FullHighlightRefresh();
					tp.Controls.Add(ewProg);
					ewProg.ParentTabPage = tp;
					ewProg.PrettyPrint = prettyPrint;
					ewProg.ProgramTextBox.Font = editorFont;
					break;

				case ".bin":
					currWindow = ewProg = new Prog8xEditWindow(TokenData, fileName);
					if (fi.Length > short.MaxValue / 4) {
						ewProg.LiveUpdate = false;
					}
					ewProg.Program = new Prog8x(fi.Name.Split('.')[0]);

					using (FileStream pstream = new FileStream(fileName, FileMode.Open))
					using (BinaryReader preader = new BinaryReader(pstream)) {
						ewProg.ProgramText = TokenData.Detokenize(preader.ReadBytes((int)preader.BaseStream.Length));
					}

					ewProg.RefreshBytes();
					ewProg.FullHighlightRefresh();
					tp.Controls.Add(ewProg);
					ewProg.ParentTabPage = tp;
					ewProg.ProgramTextBox.Font = editorFont;
					ewProg.PrettyPrint = prettyPrint;
					break;

				case ".8xv":
					currWindow = ewProg = new Prog8xEditWindow(TokenData, fileName);
					if (fi.Length > short.MaxValue / 4) {
						ewProg.LiveUpdate = false;
					}
					using (FileStream pstream = new FileStream(fileName, FileMode.Open)) {
						using (BinaryReader preader = new BinaryReader(pstream)) {
							ewProg.Program = new AppVar8x(preader);
						}
					}
					ewProg.FullHighlightRefresh();
					tp.Controls.Add(ewProg);
					ewProg.ParentTabPage = tp;
					ewProg.ReadOnly = false;
					ewProg.ProgramTextBox.Font = editorFont;
					ewProg.PrettyPrint = prettyPrint;
					break;

				case ".8xl":
					currWindow = ewList = new List8xEditWindow(fileName);
					using (FileStream pstream = new FileStream(fileName, FileMode.Open)) {
						using (BinaryReader preader = new BinaryReader(pstream)) {
							ewList.List = new RealList8x(preader);
						}
					}
					tp.Controls.Add(ewList);
					ewList.ParentTabPage = tp;
					break;

				default:
					throw new Exception(string.Format("File type not supported: {0}.", fi.Extension));
			}
			tp.Text = currWindow.OnCalcName;
			EditWindows.TabPages.Add(tp);
			EditWindows.SelectedTab = tp;
			if (prevWindow.OnCalcName == "" && prevWindow.NumTokens == 0 &&
				prevWindow.ParentTabPage.Text == "new file" && prevWindow.FirstFileFlag) {
				EditWindows.TabPages.Remove(prevWindow.ParentTabPage);
			}
			currWindow.SaveDirectory = fi.Directory.FullName;
			currWindow.New = false;
			//currWindow.DragEnter += TokenIDE_DragEnter;
			//currWindow.DragDrop += TokenIDE_DragDrop;
			//} catch (Exception ex) {
			//	MessageBox.Show(string.Format("Could not open file!\n{0}", ex.Message));
			//}
		}

		private void openFileToolStripMenuItem_Click(object sender, EventArgs e) {
			//try {
			FileDialog fd = new OpenFileDialog();
			if (fd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) { return; }
			if (fd.FileName == string.Empty || fd.FileName == null)
				return;
			OpenFile(fd.FileName);
		}

		private bool OpenTokensFile(string xmlFileName) {
			if (string.IsNullOrEmpty(xmlFileName) || !File.Exists(xmlFileName)) {
				return false;
			}

			XmlValidator.XmlValidator validator = new XmlValidator.XmlValidator();
			XmlSchema xmlSchema = XmlSchema.Read(new StringReader(Resources.Tokens_xsd), SchemaStringValidationHandler);
			if (!validator.Validate(xmlSchema, xmlFileName)) {
				using (StreamWriter sw = new StreamWriter("xml.err", false)) {
					sw.WriteLine("Line:Column Message");
					foreach (XmlValidator.ValidationError error in validator.Errors) {
						sw.WriteLine(string.Format("{0}:{1} {2}", error.Line, error.Column, error.Message));
					}
				}
				MessageBox.Show("Error: The XML file you attempted to load did not match the schema. Please select a different file. See xml.err for a detailed list of errors.");
				return false;
			} else {
				TokenData oldTokenData = TokenData;
				TokenData = new TokenData(xmlFileName);
				if (currWindow != null && currWindow is Prog8xEditWindow) {
					int a;
					Prog8xEditWindow ew = (Prog8xEditWindow)currWindow;
					ew.ProgramText = TokenData.Detokenize(oldTokenData.Tokenize(ew.ProgramText, out a));
					ew.TokenData = TokenData;
					ew.Dirty = false;
				}
			}

			return true;
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e) {
			Options o = new Options();
			o.SelectedFont = editorFont;
			o.TokenFile = config.TokenIDE.file;
			o.TokenData = TokenData;
			o.PrettyPrint = prettyPrint;
			DialogResult result = o.ShowDialog();
			if (result == System.Windows.Forms.DialogResult.OK) {
				config.TokenIDE.file = o.TokenFile;
				editorFont = o.SelectedFont;
				prettyPrint = o.PrettyPrint;
				config.Font.family = editorFont.FontFamily.Name;
				config.TokenIDE.prettyPrint = prettyPrint;
				config.Font.size = editorFont.SizeInPoints;
				Config.WriteIni(config, "TokenIDE.ini");
				foreach (TabPage window in EditWindows.Controls) {
					Prog8xEditWindow editWindow = window.Controls[0] as Prog8xEditWindow;
					if (editWindow == null) { continue; }
					editWindow.ProgramTextBox.Font = editorFont;
					editWindow.PrettyPrint = prettyPrint;
					editWindow.Invalidate();
				}
			}
		}

		private void projectToolStripMenuItem_Click(object sender, EventArgs e) {
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.AddFilter("Tokens Project", "*.xml");
			ofd.CheckFileExists = true;
			if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK || ofd.FileName == "") { return; }

			TokensProject proj = TokensProject.Open(ofd.FileName);
			AddProject(proj);

			buildAllToolStripMenuItem.Enabled = true;
		}

		private void projectToolStripMenuItem1_Click(object sender, EventArgs e) {
			NewProjectWizard wizard = new NewProjectWizard() {
				ProjectName = "New Project",
				BaseDirectory = Path.Combine(string.IsNullOrWhiteSpace(currWindow.SaveDirectory) ? Environment.CurrentDirectory : currWindow.SaveDirectory, "New Project"),
			};

			if (wizard.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) {
				return;
			}

			var project = new TokensProject() {
				Name = wizard.ProjectName,
				BaseDirectory = wizard.BaseDirectory,
				OutDirectory = wizard.OutDirectory,
				FileName = Path.Combine(wizard.BaseDirectory, wizard.ProjectName + ".xml"),
			};

			AddProject(project);

			if (!Directory.Exists(project.BaseDirectory)) {
				Directory.CreateDirectory(project.BaseDirectory);
			}

			if (!Directory.Exists(project.OutDirectory)) {
				Directory.CreateDirectory(project.OutDirectory);
			}

			project.Save();
		}

		private void projectTree_AfterExpand(object sender, TreeViewEventArgs e) {
			if (e.Node.Nodes.Count == 1) { e.Node.Nodes[0].Expand(); }
		}

		private void projectTree_AfterSelect(object sender, TreeViewEventArgs e) {
		}

		private void projectTree_DoubleClick(object sender, EventArgs e) {
			TreeNode selectedNode = projectTree.SelectedNode;
			if (selectedNode == null) { return; }

			TokensProject selectedProject = GetProject(projectTree.SelectedNode.Parent.Parent);

			if (selectedNode.Nodes == null || selectedNode.Nodes.Count == 0 &&
				(selectedNode.Text != "Programs" && selectedNode.Text != "AppVars")) {
				OpenFile(Path.Combine(selectedProject.BaseDirectory, ((ProjectFile)selectedNode.Tag).Path));
			}
			//if (selectedNode.Tag != null && selectedNode.Parent.Text == "Programs" || selectedNode.Parent.Text == "AppVars") {
			//OpenFile(((ProjectItem)selectedNode.Tag).File);
			//}
		}

		private void projectTree_MouseClick(object sender, MouseEventArgs e) {
			projectTree.SelectedNode = projectTree.GetNodeAt(e.Location);
		}

		private string replaceTokens(string val) {
			val = val.Replace("%file%", Path.Combine(currWindow.SaveDirectory, currWindow.FileName));
			val = val.Replace("%tokendir%", Application.StartupPath);
			if (val.Contains("%files%")) {
				StringBuilder sb = new StringBuilder();
				foreach (TabPage page in EditWindows.TabPages) {
					IEditWindow window = (IEditWindow)(page.Controls[0]);
					if (window.SaveDirectory == null) { continue; }
					sb.AppendFormat("\"{0}\" ", Path.Combine(window.SaveDirectory, window.FileName));
				}
				val = val.Replace("%files%", sb.ToString());
			}

			return val;
		}
		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) {
			if (EditWindows.SelectedTab.Controls[0] is Prog8xEditWindow) {
				SaveTextFile(true);
			}
		}

		private void saveProject() {
			foreach (TokensProject proj in projects) {
				proj.Save();
			}
		}

		private void SaveTextFile(bool saveAs) {
			if (currWindow.New || saveAs) {
				//string progName = InputBox.ShowInputBox("Program Name");
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.AddExtension = true;
				sfd.FileName = currWindow.OnCalcName + ".txt";
				sfd.DefaultExt = ".txt";
				sfd.AddFilter("Text File", ".txt");
				//sfd.CheckFileExists = true;
				if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) { return; }
				if (!string.IsNullOrWhiteSpace(sfd.FileName)) {
					//currWindow.ProgramName = new FileInfo(sfd.FileName).Name;
					currWindow.FileName = sfd.FileName;
				} else {
					statusLabel.Text = "Save failed";
					return;
				}
				currWindow.New = false;
			}
			//if (String.IsNullOrWhiteSpace(currWindow.OnCalcName)) {
			//    statusLabel.Text = "Save failed";
			//    return;
			//}
			//string programName = currWindow.ProgramName;
			//SaveProgram(currWindow.SaveDirectory, programName, !currWindow.HasSaved);
			try {
				currWindow.Save();
				statusLabel.Text = "Save succeeded.";
			} catch (Exception ex) {
				statusLabel.Text = "Save failed: " + ex.Message;
			}
			//currWindow.HasSaved = true;
			return;
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
			if (EditWindows.SelectedTab.Controls[0] is Prog8xEditWindow) {
				SaveTextFile(false);
			}
		}

		private void SchemaStringValidationHandler(object sender, ValidationEventArgs args) {
			throw new Exception("Shaun, what the fuck did you do?!");
		}

		private byte[] setUpSave(out string directory) {
			directory = "";
			if (currWindow == null) {
				return null;
			}
			if (string.IsNullOrWhiteSpace(currWindow.OnCalcName)) {
				currWindow.OnCalcName = InputBox.Show("Program Name");
				//currWindow.FileName = currWindow.ProgramName;
			}
			if (string.IsNullOrWhiteSpace(currWindow.OnCalcName)) {
				statusLabel.Text = "Build failed";
				return null;
			}
			byte[] data = currWindow.ByteData;
			if (data == null) {
				statusLabel.Text = "Build failed";
				return null;
			}

			if (string.IsNullOrWhiteSpace(currWindow.SaveDirectory)) {
				//var fbd = new FolderSelectDialog() {
				//    InitialDirectory = Environment.CurrentDirectory,
				//    Title = "Save Directory",
				//};
				//FileFolderDialog ffd = new FileFolderDialog() {
				var fbd = new OpenFileDialog() {
					InitialDirectory = Environment.CurrentDirectory,
					ValidateNames = false,
					CheckFileExists = false,
					CheckPathExists = false,
					FileName = "Folder Selection.",
				};
				if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) {
					return null;
				}

				FileInfo fi = new FileInfo(fbd.FileName);

				directory = fi.DirectoryName;
				currWindow.SaveDirectory = directory;
			} else {
				directory = currWindow.SaveDirectory;
			}

			currWindow.Save();

			return data;
		}

		private void statusLabel_TextChanged(object sender, EventArgs e) {
			if (statusLabel.Text == "") { return; }
			statusLabelClear.Start();
		}

		private void statusLabelClear_Tick(object sender, EventArgs e) {
			statusLabel.Text = "";
		}

		private void launchExternalTool(object sender, EventArgs e) {
			if (currWindow.SaveDirectory == null) {
				return;
			}

			ToolStripMenuItem t = (ToolStripMenuItem)sender;

			string[] vals = (string[])t.Tag;
			string program = replaceTokens(vals[1]);
			string args = replaceTokens(vals[2]);
			string buildOrSave = vals.ElementAtOrDefault(3, s => s.ToLowerInvariant());
			string kill = vals.ElementAtOrDefault(4, s => s.ToLowerInvariant());

			if (program == null) {
				//MessageBox.Show("Could not run program \"{0}\". Check that it exists.", "Error external tool", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			
			if (kill == "kill" && externalTools.ContainsKey(t)) {
				try {
					externalTools[t].Kill();
				} catch { }
			}

			switch (buildOrSave) {
				case "build":
					buildFile(Var8x.VarType.Program, Var8x.CalcType.Calc8x);
					break;
				case "save":
					SaveTextFile(false);
					break;
			}

			try {
				externalTools[t] = Process.Start(program, args);
			} catch (Exception ex) {
				MessageBox.Show(string.Format("Error running program {0}:{1}{2}", program, Environment.NewLine, ex.Message), program, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void TokenIDE_DragDrop(object sender, DragEventArgs e) {
			// Extract the data from the DataObject-Container into a string list
			string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

			foreach (string file in fileList) {
				OpenFile(file);
			}
		}

		private void TokenIDE_DragEnter(object sender, DragEventArgs e) {
			// Check if the Dataformat of the data can be accepted
			// (we only accept file drops from Explorer, etc.)
			e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
		}

		private void TokenIDE_FormClosing(object sender, FormClosingEventArgs e) {
			bool dirty = false;
			foreach (TabPage tp in EditWindows.TabPages) {
				if (((IEditWindow)tp.Controls[0]).Dirty) {
					dirty = true;
				}
			}
			if (dirty) {
				if (MessageBox.Show("File has not been saved, are you sure you want to exit?", "Exit?",
						MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No) {
					e.Cancel = true;
				}
			}
		}

		private void tokenize73pToolStripMenuItem_Click(object sender, EventArgs e) {
			buildFile(Var8x.VarType.Program, Var8x.CalcType.Calc73);
		}

		private void tokenize82pToolStripMenuItem_Click(object sender, EventArgs e) {
			buildFile(Var8x.VarType.Program, Var8x.CalcType.Calc82);
		}

		private void tokenize83pToolStripMenuItem_Click(object sender, EventArgs e) {
			Var8x.VarType varType = Var8x.VarType.Program;
			Var8x.CalcType calcType = Var8x.CalcType.Calc83;
			buildFile(varType, calcType);
		}

		private void tokenize85pToolStripMenuItem_Click(object sender, EventArgs e) {
			buildFile(Var8x.VarType.Program, Var8x.CalcType.Calc85);
		}

		private void tokenize8xvToolStripMenuItem_Click(object sender, EventArgs e) {
			buildFile(Var8x.VarType.AppVar, Var8x.CalcType.Calc8x);
		}

		private void Tokens_Load(object sender, EventArgs e) {
		}

		private void TokensTree_AfterSelect(object sender, TreeViewEventArgs e) {
			if (e.Node != null) {
				if (TokenData.Comments.ContainsKey(e.Node.Text)) {
					string comment = TokenData.Comments[e.Node.Text];
					if (comment != null) {
						commentBox.Text = comment.Replace("\n", Environment.NewLine);//.Replace("\\n", "\n");
					} else {
						commentBox.Text = "No data available for this command.";
					}

					TreeNode n = e.Node;
					while (!TokenData.Sites.ContainsKey(n.Text) && n.Parent != null) {
						n = n.Parent;
					}
					if (!TokenData.Sites.ContainsKey(n.Text)) {
						docLinkLabel.Text = "";
					} else {
						docLinkLabel.Text = TokenData.Sites[n.Text];
					}
					mainToolTip.SetToolTip(docLinkLabel, docLinkLabel.Text);
				} else {
					commentBox.Text = "";
				}
			}
		}

		private void TokensTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
			TokensTree.SelectedNode = e.Node;
			if (TokenData.Comments.ContainsKey(e.Node.Text)) {
				string comment = TokenData.Comments[e.Node.Text];
				if (comment != null) {
					commentBox.Text = comment.Replace("\n", Environment.NewLine);//.Replace("\\n", "\n");
				} else {
					commentBox.Text = "No data available for this command.";
				}
			} else {
				commentBox.Text = "";
			}
		}

		private void TokensTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
			if (!(currWindow is Prog8xEditWindow)) { return; }
			Prog8xEditWindow ew = (Prog8xEditWindow)currWindow;

			if (e.Node.Nodes.Count == 0 && e.Node.IsSelected) {
				ew.SelectedText = ((Merthsoft.Tokens.TokenData.GroupEntry)e.Node.Tag).Main;
				ew.ProgramTextBox.Focus();
			}
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e) {
			saveProject();
		}

		private void xLIBCColorPicerToolStripMenuItem_Click(object sender, EventArgs e) {
			if (!(currWindow is Prog8xEditWindow)) { return; }
			Prog8xEditWindow ew = (Prog8xEditWindow)currWindow;

			int color;
			ColorPicker c = new ColorPicker();
			if (ew.SelectedText != "" && int.TryParse(ew.SelectedText, out color) && color >= 0 && color < 256) {
				c.SelectedColor = color;
			}
			c.StartPosition = FormStartPosition.CenterParent;
			c.PasteTextEvent += handlePasteEvent;
			c.Show();
		}

		private void handlePasteEvent(object sender, PasteTextEventArgs e) {
			if (!(currWindow is Prog8xEditWindow)) {
				MessageBox.Show("Unable to paste into window.", "Paste", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			Prog8xEditWindow ew = (Prog8xEditWindow)currWindow;

			ew.SelectedText = e.TextToPaste;
		}

		private void collapsePaneButton_ButtonClick(object sender, EventArgs e) {
			if (mainContainer.Panel1Collapsed) {
				collapsePaneButton.Text = "<";
			} else {
				collapsePaneButton.Text = ">";
			}
			mainContainer.Panel1Collapsed = !mainContainer.Panel1Collapsed;
		}
	}
}