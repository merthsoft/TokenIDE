using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Merthsoft.Tokens;
using System.IO;

namespace Merthsoft.TokenIDE {
	public partial class Options : Form {
		private Prog8xEditWindow window;

		public string TokenFile { 
			get { return defaultTokenFileLabel.Text; }
			set { 
				defaultTokenFileLabel.Text = value;
				toolTip1.SetToolTip(defaultTokenFileLabel, value);
			}
		}

		private Font selectedFont;
		public Font SelectedFont {
			get { return selectedFont; }
			set {
				if (value == null) { return; }
				selectedFont = value;
				fontLabel.Text = string.Format("{0}, {1}pt", value.Name, value.SizeInPoints);
				if (window != null) {
					window.ProgramTextBox.Font = value;
					window.Invalidate();
					window.FullHighlightRefresh();
				}
			}
		}

		public TokenData TokenData {
			set {
				window = new Prog8xEditWindow(value, "");
				window.Dock = DockStyle.Fill;
				panel1.Controls.Add(window);
				window.readOnlyPanel.Visible = false;
				window.ProgramText = string.Concat(value.CommentString, " Type in here to test the font.\nDisp \"Hello, world.");
				if (SelectedFont != null) {
					window.ProgramTextBox.Font = SelectedFont;
				}
				window.FullHighlightRefresh();
			}
		}

		public bool PrettyPrint { get { return prettyPrintBox.Checked; } set { prettyPrintBox.Checked = value; } }

		public Options() {
			InitializeComponent();
		}

		private void Options_Load(object sender, EventArgs e) {

		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			using (OpenFileDialog ofd = new OpenFileDialog()) {
				ofd.AddFilter("Token File", "*.xml");
				if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					FileInfo fi = new FileInfo(ofd.FileName);
					if (fi.DirectoryName == System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)) {
						TokenFile = fi.Name;
					} else {
						TokenFile = ofd.FileName;
					}
				}
			}
		}

		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			using (FontDialog fd = new FontDialog()) {
				fd.Font = selectedFont;
				if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
					SelectedFont = fd.Font;
				}
			}
		}

		private void button1_Click(object sender, EventArgs e) {
			DialogResult = System.Windows.Forms.DialogResult.OK;
		}
	}
}
