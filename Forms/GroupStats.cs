using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Merthsoft.Tokens;

namespace Merthsoft.TokenIDE {
	public partial class GroupStats : Form {
		string indentedText;
		string unindentedText;

		private Prog8xEditWindow editWindow;

		public GroupStats(Prog8xEditWindow parentWindow) {
			InitializeComponent();

			Dictionary<string, Dictionary<string, int>> groupCounts = parentWindow.GetGroupCounts(ref indentedText);
			unindentedText = parentWindow.ProgramText;

			foreach (var group in groupCounts) {
				TreeNode t = new TreeNode();
				int total = 0;
				foreach (var count in group.Value) {
					TreeNode child = new TreeNode(string.Format("{0} ({1})", count.Key, Math.Abs(count.Value)));
					t.Nodes.Add(child);
					total += count.Value;
				}
				t.Text = string.Format("{0} ({1})", group.Key, total);
				treeView1.Nodes.Add(t);
				t.Expand();
			}

			editWindow = new Prog8xEditWindow(parentWindow.TokenData, "");
			editWindow.Font = parentWindow.Font;
			editWindow.readOnlyPanel.Visible = false;
			editWindow.ReadOnly = true;
			editWindow.Dock = DockStyle.Fill;
			editWindow.ProgramText = indentedText;
			editWindow.FullHighlightRefresh();

			splitContainer1.Panel2.Controls.Add(editWindow);
			//splitContainer1.Panel2.Controls.
			splitContainer1.Panel2.Controls.SetChildIndex(editWindow, 0);
		}

		private void indentBox_CheckedChanged(object sender, EventArgs e) {
			editWindow.ProgramText = indentBox.Checked ? indentedText : unindentedText;
		}
	}
}
