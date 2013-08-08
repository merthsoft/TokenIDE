using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Merthsoft.TokenIDE.Project {
	public class ProjectFile {
		[XmlAttribute("path")]
		public string Path { get; set; }

		[XmlAttribute("output")]
		public string Output { get; set; }

		public override string ToString() {
			return new FileInfo(Path).Name;
		}
	}

	public class MemorySection {
		[XmlArray("Programs"), XmlArrayItem("File")]
		public List<ProjectFile> Programs { get; set; }

		[XmlArray("AppVars"), XmlArrayItem("File")]
		public List<ProjectFile> AppVars { get; set; }

		public MemorySection() {
			Programs = new List<ProjectFile>();
			AppVars = new List<ProjectFile>();
		}
	}

	[XmlRoot("TokensProject", Namespace=TokensProject.SCHEMA)]
	public class TokensProject {
		public const string SCHEMA = "http://merthsoft.com/TokensProject";

		[XmlIgnore]
		public string FileName { get; set; }

		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("baseDirectory")]
		public string BaseDirectory { get; set; }

		[XmlAttribute("outDirectory")]
		public string OutDirectory { get; set; }
		
		[XmlElement("RAM")]
		public MemorySection Ram { get; set; }

		[XmlElement("Archive")]
		public MemorySection Archive { get; set; }

		public TokensProject() {
			Ram = new MemorySection();
			Archive = new MemorySection();
		}

		public void Save(string fileName = null) {
			if (fileName != null) {
				FileName = fileName;
			}

			XmlSerializer serializer = new XmlSerializer(typeof(TokensProject));
			using (StreamWriter fs = new StreamWriter(FileName, false, Encoding.Unicode)) {
				serializer.Serialize(fs, this);
			}
		}

		public static TokensProject Open(string fileName) {
			XmlSerializer serializer = new XmlSerializer(typeof(TokensProject));
			using (FileStream fs = new FileStream(fileName, FileMode.Open)) {
				TokensProject project = (TokensProject)serializer.Deserialize(fs);
				project.FileName = fileName;

				return project;
			}
		}
	}
}
