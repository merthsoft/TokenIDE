using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Merthsoft.TokenIDE {
	public partial class ShiftTiles : Form {
		public int Start {
			get { return (int)startBox.Value; }
			set { startBox.Value = value;}
		}

		public int End {
			get { return (int)endBox.Value; }
			set { endBox.Value = value; }
		}

		public int Amount {
			get { return (int)amountBox.Value; }
			set { amountBox.Value = value; }
		}

		public ShiftTiles() {
			InitializeComponent();
		}

		private void okButton_Click(object sender, EventArgs e) {
			Close();
		}

		private void cancelButton_Click(object sender, EventArgs e) {
			Close();
		}

		private void ShiftTiles_FormClosing(object sender, FormClosingEventArgs e) {
			if (DialogResult == System.Windows.Forms.DialogResult.OK) {
				if (End < Start) {
					MessageBox.Show("End value must be greater than or equal to start value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					e.Cancel = true;
					return;
				}
			}
		}
	}
}
