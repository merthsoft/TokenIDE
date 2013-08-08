using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Merthsoft.TokenIDE.Properties;

namespace XmlValidator {
	/// <summary>
	/// Handles setting up the XML libraries and validating documents
	/// </summary>
	public class XmlValidator {
		List<ValidationError> errors = new List<ValidationError>();

		int currentLine = -1;
		int currentColumn = -1;

		public IList<ValidationError> Errors {
			get { return errors.AsReadOnly(); }
		}

		private void ValidationHandler(object sender, ValidationEventArgs args) {
			errors.Add(new ValidationError(
					currentLine + 1,    // XmlReader numbers from line 0
					currentColumn,
					args.Message
				));
		}

		/// <summary>
		/// Validates an XML file using the given schema.
		/// </summary>
		/// <param name="schema">URI of the schema.</param>
		/// <param name="xml">URI of the XML document.</param>
		/// <returns><c>true</c> if validation is successful</returns>
		public bool Validate(string schemaURI, string xmlURI) {
			if (String.IsNullOrWhiteSpace(schemaURI)) {
				errors.Add(new ValidationError(-1, -1, "Could not find schema"));
				return false;
			}

			XmlReaderSettings s = new XmlReaderSettings();
			s.ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings | XmlSchemaValidationFlags.ProcessIdentityConstraints;
			s.ValidationType = ValidationType.Schema;
			s.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);
			s.Schemas.Add(null, schemaURI);

			XmlReader reader = XmlReader.Create(xmlURI, s);

			try {
				while (reader.Read()) {
					IXmlLineInfo lineInfo = reader as IXmlLineInfo;
					currentLine = lineInfo.LineNumber;
					currentColumn = lineInfo.LinePosition;
				}
			} catch (XmlException e) {
				errors.Add(new ValidationError(e.LineNumber, e.LinePosition, e.Message));
			}

			reader.Close();
			return errors.Count == 0;
		}

		public bool Validate(XmlSchema xmlSchema, string xmlURI) {
			XmlReaderSettings s = new XmlReaderSettings();
			s.ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings | XmlSchemaValidationFlags.ProcessIdentityConstraints;
			s.ValidationType = ValidationType.Schema;
			s.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);
			//s.Schemas.Add(null, schemaURI);
			s.Schemas.Add(xmlSchema);

			XmlReader reader = XmlReader.Create(xmlURI, s);

			try {
				while (reader.Read()) {
					IXmlLineInfo lineInfo = reader as IXmlLineInfo;
					currentLine = lineInfo.LineNumber;
					currentColumn = lineInfo.LinePosition;
				}
			} catch (XmlException e) {
				errors.Add(new ValidationError(e.LineNumber, e.LinePosition, e.Message));
			}

			reader.Close();
			return errors.Count == 0;
		}
	}

	public class ValidationError {
		int line;
		int column;
		string message;

		public string Line { get { return line > 0 ? line.ToString() : String.Empty; } }

		public string Column { get { return column > 0 ? column.ToString() : String.Empty; } }

		public string Message { get { return message; } }

		public ValidationError(int line, int column, string message) {
			this.line = line;
			this.column = column;
			this.message = message;
		}

		public string[] ToArray() {
			return new string[] { Line, Column, Message };
		}
	}
}
