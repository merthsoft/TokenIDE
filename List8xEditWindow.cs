using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Merthsoft.CalcData;

namespace TokenIDE {
	public partial class List8xEditWindow : UserControl, IEditWindow {
		public List8xEditWindow(string fileName) {
			InitializeComponent();
			FirstFileFlag = false;
			Dock = DockStyle.Fill;
			FileName = fileName;
		}

		public TabPage ParentTabPage { get; set; }

		public string SaveDirectory { get; set; }

		public bool HasSaved { get; set; }

		public bool New { get; set; }

		public bool Dirty { get; set; }

		public string OnCalcName { get; set; }

		public string FileName { get; set; }

		private RealList8x list;
		public RealList8x List {
			get {
				return list;
			}
			set {
				list = value;
				foreach (decimal d in list) {
					grid.Rows.Add(d);
				}
				string listName = list.Name.Substring(1);
				if (listName[0] < 10) {
					listName = "L" + (listName[0] + 1);
				} else {
					listName = "ʟ" + listName;
				}
				OnCalcName = Col1.HeaderText = listName;
			}
		}

		public Var8x CalcVar { get { return List; } set { List = (RealList8x)value; } }

		public bool FirstFileFlag { get; set; }

		public int NumTokens {
			get { return List.NumElements; }
		}

		public byte[] ByteData {
			get { return List.Data; }
		}

		public void Save() {
			throw new NotImplementedException();
		}

		private void grid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e) {
			string rowNumber = (e.RowIndex + 1).ToString().PadLeft(grid.RowCount.ToString().Length, '0');

			// get the size of the row number string
			SizeF Sz = e.Graphics.MeasureString(rowNumber.ToString(), this.Font);

			// adjust the width of the column that contains the row header cells 
			if (grid.RowHeadersWidth < (int)(Sz.Width + 20)) {
				grid.RowHeadersWidth = (int)(Sz.Width + 20);
			}

			// draw the row number
			e.Graphics.DrawString(rowNumber, this.Font, SystemBrushes.ControlText, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - Sz.Height) / 2));

		}

		private void grid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
			decimal toss;
			string val = e.FormattedValue.ToString();
			if (!(decimal.TryParse(val, out toss)) && val != "") {
				e.Cancel = true;
			}
		}
	}
}
