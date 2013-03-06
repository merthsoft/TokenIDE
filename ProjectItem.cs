using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TokenIDE.Project {
	public enum FileType { data, raw }

	public class ProjectItem {
		public string Name { get; set; }
		public string File { get; set; }
		public FileType FileType { get; set; }
		public string OutFile { get; set; }
		public bool Archived { get; set; }

		protected string itemType;

		public ProjectItem(XmlNode xmlNode) {
			Name = xmlNode.Attributes["name"].Value;
			File = xmlNode.Attributes["file"].Value;
			FileType = (FileType)Enum.Parse(FileType.GetType(), xmlNode.Attributes["type"].Value);
			OutFile = xmlNode.Attributes["outFile"].Value;
			Archived = bool.Parse(xmlNode.Attributes["archived"].Value);
		}

		public virtual void WriteAttributes(XmlWriter xw) {
			xw.WriteAttributeString("name", Name);
			xw.WriteAttributeString("file", File);
			xw.WriteAttributeString("type", FileType.ToString());
			xw.WriteAttributeString("outFile", OutFile);
			xw.WriteAttributeString("archived", Archived.ToString());
		}

		public virtual void WriteNode(XmlWriter xw) {
			xw.WriteStartElement(itemType);
			WriteAttributes(xw);
			xw.WriteEndElement();
		}

		public override string ToString() {
			if (Archived) { return "*" + Name; }
			return Name;
		}
	}

	public class ProjectAppVar : ProjectItem {
		public ProjectAppVar(XmlNode xmlNode) : base(xmlNode) { itemType = "AppVar"; }
	}

	public class ProjectProgram : ProjectItem {
		public bool Locked { get; set; }

		public ProjectProgram(XmlNode xmlNode)
			: base(xmlNode) {
				Locked = bool.Parse(xmlNode.Attributes["locked"].Value);
				itemType = "Program";
		}

		public override void WriteAttributes(XmlWriter xw) {
			base.WriteAttributes(xw);
			xw.WriteAttributeString("locked", Locked.ToString());
		}
	}
}
