using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merthsoft.TokenIDE {
	class History<T> {
		List<T> history = new List<T>();

		public int HistoryPosition { get; private set; }
		public int HistorySize { get { return history.Count; } }

		public History() {
			HistoryPosition = 0;
		}

		public History(IEnumerable<T> history) : this() {
			this.history = history.ToList();
		}

		public void Push(T item) {
			if (HistoryPosition != history.Count) {
				history.RemoveRange(HistoryPosition, history.Count - HistoryPosition);
			}
			history.Add(item);
			HistoryPosition = history.Count;
		}

		public void Clear() {
			history.Clear();
			HistoryPosition = 0;
		}

		public T Undo() {
			if (HistoryPosition == 0) {
				throw new IndexOutOfRangeException("Cannot Undo when there's no history.");
			}

			return history[--HistoryPosition];
		}

		public T Redo() {
			if (HistoryPosition == HistorySize) {
				throw new IndexOutOfRangeException("Cannot Redo when at the end of the history.");
			}

			return history[++HistoryPosition];
		}
	}
}
