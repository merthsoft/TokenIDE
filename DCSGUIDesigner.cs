using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Merthsoft.Tokens;
using Merthsoft.Tokens.DCSGUI;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace TokenIDE {
	public partial class DCSGUIDesigner : Form {
		public List<GUIItem> outControls;

		PrivateFontCollection pfc = new PrivateFontCollection();

		int screenWidth = 96;
		int screenHeight = 64;
		int zoom = 4;
		int mouseX, mouseY;
		int mouseXOld, mouseYOld;
		MouseButtons mouseButton;
		GUIContainer container;

		GUIItem newItem = null;
		GUIItem selectedItem;

		byte scrollByte = 0;

		public DCSGUIDesigner() {
			InitializeComponent();

			IntPtr iconPtr = TokenIDE.Properties.Resources.icon_guiedit.GetHicon();
			using (Icon icon = Icon.FromHandle(iconPtr)) {
				this.Icon = icon;
			}

			for (GUINums i = 0; i < GUINums._end; i++) {
				int t;
				if (!int.TryParse(i.ToString(), out t)) {
					ListViewItem item = new ListViewItem(i.ToString());
					item.Name = ((int)i).ToString();
					toolBox.Items.Add(item);
				}
			}
			canvas.Width = screenWidth * zoom;
			canvas.Height = screenHeight * zoom;
			try {
				pfc.AddFontFile("TI-83P Font.ttf");
				GUIItem.DCSFont = new Font(pfc.Families[0], 7.5f, FontStyle.Regular);
			} catch {
				GUIItem.DCSFont = new Font(FontFamily.GenericMonospace, 7.5f, FontStyle.Regular);
			}
		}

		private void DCSGUIDesigner_Load(object sender, EventArgs e) {

		}

		private void pictureBox1_Paint(object sender, PaintEventArgs e) {
			Bitmap b = new Bitmap(96, 64);
			Graphics g = Graphics.FromImage(b);
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
			foreach (GUIItem gi in activeControlsList.Items)
				gi.Draw(g);
			if (newItem != null) {
				switch (newItem.GUINum) {
					case GUINums.GUIRCheckbox:
					case GUINums.GUIRRadio:
					case GUINums.GUIRWordInt:
					case GUINums.GUIRByteInt:
						newItem.X = mouseX;
						newItem.Y = mouseY;
						break;
					case GUINums.GUIRSmallWin:
						newItem.X = mouseX;
						newItem.Y = mouseY;
						break;
					case GUINums.GUIRText:
						newItem.X = mouseX + container.XOff;
						newItem.Y = mouseY + container.YOff;
						break;
					case GUINums.GUIRBorder:
					case GUINums.GUIRRect:
					case GUINums.GUIRHotspot:
					case GUINums.GUIRTextMultiline:
						newItem.Width = mouseX - newItem.X;
						newItem.Height = mouseY - newItem.Y;
						break;
					case GUINums.GUIRTextLineIn:
					case GUINums.GUIRPassIn:
						newItem.Width = mouseX - newItem.X;
						break;
					case GUINums.GUIRScrollHoriz:
						newItem.Width = mouseX - newItem.X;
						break;
					case GUINums.GUIRScrollVert:
						newItem.Height = mouseY - newItem.Y;
						break;
					default:
						break;
				}
				newItem.Draw(g);
			}
			e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
			e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			e.Graphics.DrawImage(b, 0, 0, screenWidth * zoom, screenHeight * zoom);
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e) {
			zoom = (int)numericUpDown1.Value;
			canvas.Width = screenWidth * zoom;
			canvas.Height = screenHeight * zoom;
		}

		private void canvas_MouseDown(object sender, MouseEventArgs e) {
			mouseX = e.X / zoom - (container != null ? container.XOff : 0);
			mouseY = e.Y / zoom - (container != null ? container.YOff : 0);
			handleMouse(e);
		}

		private void panel1_MouseUp(object sender, MouseEventArgs e) {
			MouseEventArgs ne = new MouseEventArgs(MouseButtons.None, e.Clicks, e.X, e.Y, e.Delta);
			handleMouse(ne);
		}

		private void canvas_MouseMove(object sender, MouseEventArgs e) {
			if (e.Button != MouseButtons.None)
				handleMouse(e);
			mouseCoordLabel.Text = string.Format("Mouse: ({0}, {1}) {2}", e.X / zoom, e.Y / zoom, (container != null ? String.Format("Container: ({0}, {1})", e.X / zoom - container.XOff, e.Y / zoom - container.YOff) : ""));
		}

		private void handleMouse(MouseEventArgs e) {
			mouseButton = e.Button;
			mouseXOld = mouseX;
			mouseYOld = mouseY;
			mouseX = e.X / zoom - (container != null ? container.XOff : 0);
			mouseY = e.Y / zoom - (container != null ? container.YOff : 0);
			switch (e.Button) {
				case System.Windows.Forms.MouseButtons.Left:
					LeftMouse();
					break;
				case System.Windows.Forms.MouseButtons.None:
					NoMouse();
					break;
				case System.Windows.Forms.MouseButtons.Right:
					RightMouse();
					break;
				default:
					break;
			}

			canvas.Invalidate();
		}

		private void NoMouse() {
			if (newItem != null) {
				if (newItem is GUIContainer) {
					container = (GUIContainer)newItem;
				}
				activeControlsList.Items.Add(newItem);
				newItem = null;
			}
		}

		private void LeftMouse() {
			if (selectedItem != null) {
				selectedItem.X = mouseX;
				selectedItem.Y = mouseY;
			} else {
				if (newItem == null && toolBox.SelectedItems.Count != 0) {
					switch ((GUINums)(int.Parse(toolBox.SelectedItems[0].Name))) {
						case GUINums.GUIRLargeWin:
							newItem = new GUIRLargeWin("LARGE", "0000000000");
							break;
						case GUINums.GUIRnull:
							newItem = new GUIRnull(false);
							break;
						case GUINums.GUIRSmallWin:
							newItem = new GUIRSmallWin(mouseX + (container != null ? container.XOff : 0), mouseY + (container != null ? container.YOff : 0), "SMALL", "0000000000");
							break;
						case GUINums.GUIRCheckbox:
							newItem = new GUIRCheckbox(mouseX, mouseY, "", GUIRCheckbox.CheckedState.Unchecked, "", container);
							break;
						case GUINums.GUIRRadio:
							newItem = new GUIRRadio(mouseX, mouseY, "", GUIRRadio.CheckedState.Unchecked, container);
							break;
						case GUINums.GUIRFullScreenImg:
							newItem = new GUIRFullScreenImg(0);
							break;
						case GUINums.GUIRHotspot:
							newItem = new GUIRHotspot(mouseX, mouseY, 0, 0, container);
							break;
						case GUINums.GUIRRect:
							newItem = new GUIRRect(mouseX, mouseY, 0, 0, 254, container);
							break;
						case GUINums.GUIRTextLineIn:
							newItem = new GUIRTextLineIn(mouseX, mouseY, 0, 30, "TEXT", container);
							break;
						case GUINums.GUIRPassIn:
							newItem = new GUIRPassIn(mouseX, mouseY, 0, 30, "TEXT", container);
							break;
						case GUINums.GUIRByteInt:
							newItem = new GUIRByteInt(mouseX, mouseY, 0, 255, 0, 255, container);
							break;
						case GUINums.GUIRWordInt:
							newItem = new GUIRWordInt(mouseX, mouseY, 0, 65535, 0, 65535, container);
							break;
						case GUINums.GUIRTextMultiline:
							newItem = new GUIRTextMultiline(mouseX, mouseY, 1, 0, "TEXTMULTI", 0, container);
							break;
						case GUINums.GUIRBorder:
							newItem = new GUIRBorder(mouseX, mouseY, 0, 0, GUIRBorder.RectColor.Black, container);
							break;
						case GUINums.GUIRText:
							newItem = new GUIRText(mouseX, mouseY, "TEXT", 0, container);
							break;
						case GUINums.GUIRScrollVert:
							newItem = new GUIRScrollVert(mouseX, mouseY, 0, scrollByte++, 1, 0, 100, 0);
							break;
						case GUINums.GUIRScrollHoriz:
							newItem = new GUIRScrollHoriz(mouseX, mouseY, 0, scrollByte++, 1, 0, 100, 0);
							break;
						default:
							break;
					}
					if (container == null && !(newItem is GUIContainer)) {
						MessageBox.Show("You must first push a container!");
						newItem = null;
					}
				}
			}
		}

		private void RightMouse() {
			if (selectedItem != null) {
				selectedItem.Selected = false;
				selectedItem = null;
			}
			for (int i = activeControlsList.Items.Count - 1; i >= 0; i--) {
				if (((GUIItem)activeControlsList.Items[i]).IsPointOnItem(mouseX, mouseY)) {
					selectedItem = ((GUIItem)activeControlsList.Items[i]);
					selectedItem.Selected = true;
					break;
				}
			}
		}

		private void doneToolStripMenuItem_Click(object sender, EventArgs e) {
			outControls = new List<GUIItem>();
			foreach (GUIItem d in activeControlsList.Items) {
				outControls.Add(d);
			}
			Close();
		}

		private void canvas_Click(object sender, EventArgs e) {
			canvas.Invalidate();
		}

		private void toolbox_SelectedIndexChanged(object sender, EventArgs e) {
			if (selectedItem != null) {
				selectedItem.Selected = false;
				selectedItem = null;
			}
		}

		private void activeControlsList_MouseDoubleClick(object sender, MouseEventArgs e) {
			if (activeControlsList.SelectedItem == null)
				return;
		}

		private void activeControlsList_SelectedIndexChanged(object sender, EventArgs e) {
			if (activeControlsList.SelectedItem == null)
				return;
			if (selectedItem != null) {
				selectedItem.Selected = false;
				selectedItem = null;
			}
			propertyGrid1.SelectedObject = selectedItem = ((GUIItem)activeControlsList.SelectedItem);
			selectedItem.Selected = true;
			canvas.Invalidate();
		}

		private void activeControlsList_KeyPress(object sender, KeyPressEventArgs e) {

		}

		private void activeControlsList_KeyDown(object sender, KeyEventArgs e) {

		}

		private void activeControlsList_KeyUp(object sender, KeyEventArgs e) {
			switch (e.KeyCode) {
				case Keys.Delete:
					if (activeControlsList.SelectedItem != null)
						activeControlsList.Items.Remove(activeControlsList.SelectedItem);
					break;
				case Keys.Escape:
					if (selectedItem != null) {
						selectedItem.Selected = false;
						selectedItem = null;
						activeControlsList.SelectedItem = null;
					}
					break;
			}

			canvas.Invalidate();
		}

		private void DCSGUIDesigner_KeyUp(object sender, KeyEventArgs e) {

		}

		private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) {
			canvas.Invalidate();
		}
	}
}
