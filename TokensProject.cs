using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Runtime.Serialization;

namespace TokenIDE.Project {
	public class TokensProject {
		public string Name { get; set; }
		public string RootDir { get; set; }
		public string CompileDir { get; set; }

		public List<ProjectProgram> Programs { get; set; }
		public List<ProjectAppVar> AppVars { get; set; }

		public TokensProject(string fileName = "") {
			Programs = new List<ProjectProgram>();
			AppVars = new List<ProjectAppVar>();

			if (fileName == "") { return; }

			XmlDocument doc = new XmlDocument();
			doc.Load(fileName);
			XmlElement root = doc.DocumentElement;

			Name = root.Attributes["name"].Value;
			RootDir = root.Attributes["rootDir"].Value;
			CompileDir = root.Attributes["compileDir"].Value;

			foreach (XmlNode progNode in root.SelectNodes("/Project/Files/Programs/Program")) {
				Programs.Add(new ProjectProgram(progNode));
			}

			foreach (XmlNode appVarNode in root.SelectNodes("/Project/Files/AppVars/AppVar")) {
				AppVars.Add(new ProjectAppVar(appVarNode));
			}
		}

	}
}
