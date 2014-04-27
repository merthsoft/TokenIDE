using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Merthsoft.TokenIDE {
	// From http://stackoverflow.com/questions/4352781/is-it-possible-to-make-the-winforms-tab-control-be-able-to-do-tab-reordering-lik
	public class DraggableTabControl : TabControl {
		public delegate void TabCloseEventHandler(object sender, TabCloseEventArgs e);
		public event TabCloseEventHandler TabClose;

		private TabPage m_DraggedTab;

		public DraggableTabControl() {
			MouseDown += OnMouseDown;
			MouseMove += OnMouseMove;
		}

		private void OnMouseDown(object sender, MouseEventArgs e) {
			TabPage clickedTab = TabAt(e.Location);
			switch (e.Button) {
				case MouseButtons.Left:
					if (MerthsoftExtensions.IsRunningOnMono()) { return; }
					m_DraggedTab = clickedTab;
					break;
				case MouseButtons.Middle:
					TabCloseEventHandler temp = TabClose;
					if (temp != null) {
						TabCloseEventArgs args = new TabCloseEventArgs(clickedTab);
						temp(this, args);
						if (!args.Cancel) {
							TabPages.Remove(clickedTab);
						}
					}
					break;
				case MouseButtons.None:
					break;
				case MouseButtons.Right:
					break;
				case MouseButtons.XButton1:
					break;
				case MouseButtons.XButton2:
					break;
				default:
					break;
			}
		}

		private void OnMouseMove(object sender, MouseEventArgs e) {
			if (e.Button != MouseButtons.Left || m_DraggedTab == null || MerthsoftExtensions.IsRunningOnMono()) {
				return;
			}

			TabPage tab = TabAt(e.Location);

			if (tab == null || tab == m_DraggedTab) {
				return;
			}

			Swap(m_DraggedTab, tab);
			SelectedTab = m_DraggedTab;
		}

		private TabPage TabAt(Point position) {
			int count = TabCount;

			for (int i = 0; i < count; i++) {
				if (GetTabRect(i).Contains(position)) {
					return TabPages[i];
				}
			}

			return null;
		}

		private void Swap(TabPage a, TabPage b) {
			int i = TabPages.IndexOf(a);
			int j = TabPages.IndexOf(b);
			TabPages[i] = b;
			TabPages[j] = a;
		}
	}
}
