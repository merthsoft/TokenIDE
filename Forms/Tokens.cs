﻿using System;
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
using System.Web;

namespace Merthsoft.TokenIDE {
	public partial class Tokens : Form {
		private MarkdownSharp.Markdown markdown;
		private string referenceCommentStart;
		private string referenceCommentEnd;

		private bool prettyPrint;
        private bool IsMono;
		private TokenData _tokenData;
		private dynamic config;
		private IEditWindow currWindow;

		private Dictionary<ToolStripMenuItem, Process> externalTools = new Dictionary<ToolStripMenuItem, Process>();

		private Font editorFont;
		private List<TokensProject> projects;

		private TokenData TokenData {
			get { return _tokenData; }
			set {
				_tokenData = value;
				TokensTree.Nodes.Clear();
				setReferenceComment("");
				List<string> groups = _tokenData.GroupNames;
				groups.Sort();
				foreach (string group in groups) {
					TreeNode n = new TreeNode(group);
					TokensTree.Nodes.Add(n);
					List<TokenData.GroupEntry> inGroup = _tokenData.GetAllInGroup(group);
					inGroup.Sort();
					foreach (TokenData.GroupEntry token in inGroup) {
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

			markdown = new MarkdownSharp.Markdown() { AutoNewLines = true };
			referenceCommentStart = string.Format("<html><head><style>body {{ background-color:#{0:X2}{1:X2}{2:X2}; font-family:sans-serif; font-size: 9pt; margins: 0,0,0,0; padding: 0,0,0,0 }}</style></head><body><div>", SystemColors.Control.R, SystemColors.Control.G, SystemColors.Control.B);
			referenceCommentEnd = "</div></body></html>";
			
			EditWindows.TabClose += EditWindows_TabClose;

			statusLabel.Text = "";
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

                IsMono = Type.GetType("Mono.Runtime") != null;

                if (IsMono) {
                    splitContainer2.Panel2.Controls.Remove(commentText);
                    commentText = null;
                }
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

		public static void dumpTokens(Dictionary<byte, TokenData.TokenDictionaryEntry> data, StringBuilder sb) {
			foreach (var token in data) {
				if (token.Value.Name != null) {
					sb.AppendFormat(string.Format("{0} - {1}", token.Value.Bytes, token.Value.Name.Replace("{", "{{").Replace("}", "}}").Replace("\\", "\\\\")));
				}
				sb.AppendLine();
				dumpTokens(token.Value.SubTokens, sb);
			}
		}

		public static string getExtension(Var8x.VarType? varType, Var8x.CalcType? calcType) {
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

			return string.Format(".{0}{1}", prefix, suffix);
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

			TreeNode projectNode = new TreeNode(proj.Name) { ImageKey = "icon_project.png", Tag = proj };

			TreeNode ramNode = new TreeNode("RAM") { ImageKey = "icon_project_blank.png", Tag = proj.Ram };
			AddMemorySection(proj.Ram, ramNode);
			projectNode.Nodes.Add(ramNode);

			TreeNode archiveNode = new TreeNode("Archive") { ImageKey = "icon_project_blank.png", Tag = proj.Archive };
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

		private void colorSpritesToolStripMenuItem_Click(object sender, EventArgs e) 
			=> hexSprite(HexSprite.Palette.xLIBC);

        private void basicColorSpritesHexEditorMenuItem_Click(object sender, EventArgs e) 
			=> hexSprite(HexSprite.Palette.BasicColors);

        private void blackAndWhiteToolStripMenuItem_Click(object sender, EventArgs e)
			=> hexSprite(HexSprite.Palette.BlackAndWhite);

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
			currWindow.Find();
		}

		private static TokensProject GetProject(TreeNode node) {
			if (node.Parent == null) {
				return (TokensProject)node.Tag;
			}
			return GetProject(node.Parent);
		}

		private void hexSprite(HexSprite.Palette palette) {
			Prog8xEditWindow ew = currWindow as Prog8xEditWindow;

#if !DEBUG
			try {
#endif
			HexSprite hexSprite = new HexSprite();

			hexSprite.SelectedPalette = palette;
			switch (palette) {
				case HexSprite.Palette.BasicColors:
					hexSprite.SpriteHeight = hexSprite.SpriteWidth = 32;
					break;
				case HexSprite.Palette.BlackAndWhite:
					hexSprite.SpriteHeight = hexSprite.SpriteWidth = 8;
					break;
				case HexSprite.Palette.xLIBC:
					hexSprite.SpriteHeight = hexSprite.SpriteWidth = 8;
					break;
            }
			hexSprite.PixelSize = 4;

			if (ew != null && ew.SelectedText != "") {
				string hexString = ew.SelectedText.Trim().Replace("\"", "").Replace("(", "").Replace(")", "").Replace(",", "");
				try { hexSprite.Hex = hexString; } catch {
					MessageBox.Show(string.Format("Unable to create sprite from {0}.", hexString));
					return;
				}
			}

			hexSprite.PasteTextEvent += handlePasteEvent;
			hexSprite.Show();
#if !DEBUG
			} catch (Exception ex) {
				MessageBox.Show(ex.ToString());
			}
#endif
		}

		private void hexViewToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!(currWindow is Prog8xEditWindow window)) { return; }
            window.GenerateByteData(false, false, out var tokens);
			var mainBuilder = new StringBuilder();
            var bytesSb = new StringBuilder();
            var tokensSb = new StringBuilder();
            foreach (var line in tokens) {
				foreach (var token in line) {
                    bytesSb.Append($"{token.Bytes:X}|");
					tokensSb.Append($"{token.Name}|");
                }
				if (bytesSb.Length > 0)
					bytesSb.Length--;
				if (tokensSb.Length  > 0)
					tokensSb.Length--;
                mainBuilder.AppendLine($"{bytesSb} | {tokensSb}");
                bytesSb.Clear();
				tokensSb.Clear();
            }

            var tb = new TextBox() { 
				Text = mainBuilder.ToString(), 
				Dock = DockStyle.Fill, 
				Multiline = true, 
				ScrollBars = ScrollBars.Both, 
				Font = editorFont, 
				WordWrap = false, 
			};

			Form f = new Form()
			{
				Text = "Hex View",
				StartPosition = FormStartPosition.CenterParent
			};
            f.Controls.Add(tb);

			f.Show();
		}

		private void imageEditorToolStripMenuItem_Click(object sender, EventArgs e) {
			if (!(currWindow is Prog8xEditWindow)) { return; }
			Prog8xEditWindow ew = (Prog8xEditWindow)currWindow;

#if !DEBUG
			try {
#endif
			HexSprite sprite = new HexSprite() { SpriteHeight = 64, SpriteWidth = 96, SelectedPalette = HexSprite.Palette.BlackAndWhite };

			sprite.PasteTextEvent += handlePasteEvent;
			sprite.Show();
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
			string site = docLinkLabel.Text;
			launchSite(site);
		}

		private static void launchSite(string site) {
			try {
				System.Diagnostics.Process.Start(site);
			} catch {
				if (!site.StartsWith("http://") || !site.StartsWith("https://")) {
					launchSite("http://" + site);
				}
			}
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
			launchSite(TokenData.Sites[n.Text]);
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
				TokenData tokenData = new TokenData((string)((IDictionary<string, object>)(config.Extensions))[ext.Substring(1)]);
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
						AppVar8x newAppVar = new AppVar8x(preader);
						if (HexSprite.IsXLibCSprite(newAppVar)) {
							var prompt = string.Format("{0} appears to be an xLibC image type, do you want to open it in the image editor?", fileName);
							var openInSpriteEditorPrompt = MessageBox.Show(prompt, "xLIBC Var Detected", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
							if (openInSpriteEditorPrompt == System.Windows.Forms.DialogResult.Yes) {
								currWindow = prevWindow;
								pstream.Close();
								openSprite(fileName);
								return;
							}
						}
						ewProg.Program = newAppVar;
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

			case ".png":
			case ".bmp":
			case ".jpg":
			case ".jpeg":
			case ".8xi":
			case ".8ci":
			case ".8ca":
			case ".gif":
				currWindow = prevWindow;
				openSprite(fileName);
				return;

			default:
				throw new Exception(string.Format("File type not supported: {0}.", fi.Extension));
			}
			tp.Text = currWindow.OnCalcName;
			EditWindows.TabPages.Add(tp);
			EditWindows.SelectedTab = tp;
			if (prevWindow != null && prevWindow.OnCalcName == "" && prevWindow.NumTokens == 0 &&
				prevWindow.ParentTabPage.Text == "new file" && prevWindow.FirstFileFlag) {
				EditWindows.TabPages.Remove(prevWindow.ParentTabPage);
			}
			currWindow.SaveDirectory = fi.Directory.FullName;
			currWindow.New = false;
		}

		private void openSprite(string fileName) {
			HexSprite hs = new HexSprite();
			hs.Open(fileName);
			hs.PasteTextEvent += handlePasteEvent;
			hs.Show();
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
				SaveFileDialog sfd = new SaveFileDialog();
				sfd.AddExtension = true;
				sfd.FileName = currWindow.OnCalcName + ".txt";
				sfd.DefaultExt = ".txt";
				sfd.AddFilter("Text File", ".txt");
				//sfd.CheckFileExists = true;
				if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) { return; }
				if (!string.IsNullOrWhiteSpace(sfd.FileName)) {
					currWindow.FileName = sfd.FileName;
					currWindow.SaveDirectory = new FileInfo(sfd.FileName).Directory.FullName;
				} else {
					statusLabel.Text = "Save failed";
					return;
				}
				currWindow.New = false;
			}

			try {
				currWindow.Save();
				statusLabel.Text = "Save succeeded.";
			} catch (Exception ex) {
				statusLabel.Text = "Save failed: " + ex.Message;
			}

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
                var quitWithoutSavePrompt = MessageBox.Show("File has not been saved, are you sure you want to exit?", "Exit?", MessageBoxButtons.YesNo);
                if (quitWithoutSavePrompt == System.Windows.Forms.DialogResult.No) {
                    e.Cancel = true;
                }
                else
                {
                    EditWindows.TabPages.Clear();
                }
            }
            else
            {
                EditWindows.TabPages.Clear();
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

		private void TokensTree_AfterSelect(object sender, TreeViewEventArgs e) {
			TreeNode node = e.Node;
			setReferenceInfo(node);
		}

		private void setReferenceInfo(TreeNode node) {
			TokenData.GroupEntry entry = node.Tag as TokenData.GroupEntry;
            string mainText = entry == null ? node.Text : entry.Main;

            if (TokenData.Comments.ContainsKey(mainText)) {
				string comment = TokenData.Comments[mainText];
				if (comment != null) {
					setReferenceComment(comment);
				} else {
					setReferenceComment("No data available for this command.");
				}
			} else {
				setReferenceComment("");
			}

			var n = node;
			entry = n.Tag as TokenData.GroupEntry;
			mainText = entry != null ? entry.Main : n.Text;
			while (!TokenData.Sites.ContainsKey(mainText) && n.Parent != null) {
				n = n.Parent;
				entry = n.Tag as TokenData.GroupEntry;
				mainText = entry != null ? entry.Main : n.Text;
			}
			if (!TokenData.Sites.ContainsKey(mainText)) {
				docLinkLabel.Text = "";
			} else {
				docLinkLabel.Text = TokenData.Sites[mainText];
			}
			mainToolTip.SetToolTip(docLinkLabel, docLinkLabel.Text);
		}

		private void setReferenceComment(string comment) {
			comment = HttpUtility.HtmlEncode(comment);
			comment = markdown.Transform(comment);
            if (!IsMono && commentText != null)
            {
                commentText.DocumentText = string.Concat(referenceCommentStart, comment, referenceCommentEnd);
            }
		}

		private void TokensTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
			TokensTree.SelectedNode = e.Node;
		}

		private void TokensTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
			if (!(currWindow is Prog8xEditWindow)) { return; }
			Prog8xEditWindow ew = (Prog8xEditWindow)currWindow;

			if (e.Node.Nodes.Count == 0 && e.Node.IsSelected) {
				ew.SelectedText = ((TokenData.GroupEntry)e.Node.Tag).Main;
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
            collapsePaneButton.Text = mainContainer.Panel1Collapsed ? "<" : ">";
            mainContainer.Panel1Collapsed = !mainContainer.Panel1Collapsed;
		}

		private void xLIBCMapEditorToolStripMenuItem_Click(object sender, EventArgs e) {
			HexSprite hexSprite = new HexSprite(true);
			hexSprite.PasteTextEvent += handlePasteEvent;
			hexSprite.SpriteWidth = 20;
			hexSprite.SpriteHeight = 15;

			Prog8xEditWindow ew = currWindow as Prog8xEditWindow;
			if (ew != null && ew.SelectedText != "") {
				string hexString = ew.SelectedText.Trim().Replace("\"", "").Replace("(", "").Replace(")", "").Replace(",", "");
				try { hexSprite.Hex = hexString; } catch {
					MessageBox.Show(string.Format("Unable to create map from string.", hexString));
					return;
				}
			}

			hexSprite.Show();
		}

		private void howToTypeThingsToolStripMenuItem_Click(object sender, EventArgs e) {
			Form f = new Form() { Width = 750, Text = "How to Type Things in TokenIDE" };
			using (Font font = new Font(FontFamily.GenericMonospace, 10)) {
				TextBox tb = new TextBox() {
					Multiline = true,
					Dock = DockStyle.Fill,
					ScrollBars = ScrollBars.Vertical,
					ReadOnly = true,
					Text = Resources.howtotypethings,
					SelectionLength = 0,
					Font = font,
				};
				f.Controls.Add(tb);
				f.Show();
				tb.SelectionLength = 0;
			}
		}

		private void undoToolStripMenuItem_Click(object sender, EventArgs e) {
			currWindow.Undo();
		}

		private void redoToolStripMenuItem_Click(object sender, EventArgs e) {
			currWindow.Redo();
		}

		private void replaceToolStripMenuItem_Click(object sender, EventArgs e) {
			currWindow.Replace();
		}

		private void commentText_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
			if (!commentText.DocumentText.ToLowerInvariant().Contains("<style>")) {
				setReferenceInfo(TokensTree.SelectedNode);
			}
		}

		private void changeProgramNameToolStripMenuItem_Click(object sender, EventArgs e) {
			currWindow.OnCalcName = InputBox.Show("Program Name", currWindow.OnCalcName) ?? currWindow.OnCalcName;
		}
    }
}