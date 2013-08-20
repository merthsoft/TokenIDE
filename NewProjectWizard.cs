using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FolderSelect;

namespace Merthsoft.TokenIDE {
	public partial class NewProjectWizard : Form {
		private string name;
		public string ProjectName {
			get { return name; }
			set {
				name = value;
			}
		}

		private string baseDirectory;
		public string BaseDirectory {
			get { return baseDirectory; }
			set {
				baseDirectory = value;
			}
		}

		private string outDirectory;
		public string OutDirectory {
			get { return outDirectory; }
			set {
				outDirectory = value;
			}
		}

		bool editedOutDirectory = false;
		bool editedBaseDirectory = false;

		public NewProjectWizard() {
			InitializeComponent();
		}

		public new DialogResult ShowDialog() {
			SetDefaultValues();
			return base.ShowDialog();
		}

		public void SetDefaultValues() {
			if (baseDirectory == null || name == null) {
				return;
			}

			DirectoryInfo baseInfo = new DirectoryInfo(BaseDirectory);
			if (baseInfo.Name != ProjectName) {
				BaseDirectory = Path.Combine(baseInfo.Parent.FullName, ProjectName);
			}
			OutDirectory = Path.Combine(BaseDirectory, "out");

			nameBox.Text = ProjectName;
			baseBox.Text = BaseDirectory;
			outBox.Text = OutDirectory;

			editedBaseDirectory = false;
			editedOutDirectory = false;
		}

		private void cancelButton_Click(object sender, EventArgs e) {
			DialogResult = System.Windows.Forms.DialogResult.Cancel;
			Close();
		}

		private void acceptButton_Click(object sender, EventArgs e) {
			DialogResult = System.Windows.Forms.DialogResult.OK;
			Close();
		}

		private void baseDirectoryLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			FolderSelectDialog fsd = new FolderSelectDialog() {
				Title = "Base Directory",
				InitialDirectory = BaseDirectory,
			};
			if (!fsd.ShowDialog()) { return; }
			BaseDirectory = fsd.FileName;
			baseBox.Text = fsd.FileName;
			editedBaseDirectory = true;
		}

		private void outputDirectoryLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			FolderSelectDialog fsd = new FolderSelectDialog() {
				Title = "Output Directory",
				InitialDirectory = OutDirectory,
			};
			if (!fsd.ShowDialog()) { return; }
			OutDirectory = fsd.FileName;
			outBox.Text = fsd.FileName;
			editedOutDirectory = true;
		}

		private void baseBox_TextChanged(object sender, EventArgs e) {
			editedBaseDirectory = true;
		}

		private void outBox_TextChanged(object sender, EventArgs e) {
			editedOutDirectory = true;
		}

		private void nameBox_TextChanged(object sender, EventArgs e) {
			ProjectName = nameBox.Text;
			if (!editedOutDirectory && !editedBaseDirectory) {
				SetDefaultValues();
			}
		}

		private void fromProjectButton_Click(object sender, EventArgs e) {
			SetDefaultValues();
		}
	}
}
