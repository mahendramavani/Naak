using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Tjoc.Web.Validator
{
	public class XhtmlValidator
	{
		private readonly Collection<ValidationRecord> _records = new Collection<ValidationRecord>();
		private readonly string _document;

		public XhtmlValidator(string input)
		{
			_document = input;
		}

		public Collection<ValidationRecord> Validate()
		{
			var xrs = new XmlReaderSettings {ProhibitDtd = false, ValidationType = ValidationType.DTD};
			xrs.ValidationEventHandler += xrs_ValidationEventHandler;
			// TODO - need caching resolver!
			xrs.XmlResolver = new CachingXmlResolver();
			using (var sr = new StringReader(_document))
			using (XmlReader xr = XmlReader.Create(sr, xrs))
			{
				try
				{
					while (xr.Read()) ;
				}
				catch (XmlException xmlExc)
				{
					_records.Add(new ValidationRecord(xmlExc));
				}
			}

			return _records;
		}

		private void xrs_ValidationEventHandler(object sender, ValidationEventArgs e)
		{
			_records.Add(new ValidationRecord(e));
		}
	}

	public class ValidationRecord
	{
		public ValidationRecord(ValidationEventArgs args)
		{
			LineNumber = args.Exception.LineNumber;
			LinePosition = args.Exception.LinePosition;
			Message = args.Message;
			Severity = args.Severity;
		}

		public ValidationRecord(XmlException xmlExc)
		{
			LineNumber = xmlExc.LineNumber;
			LinePosition = xmlExc.LinePosition;
			Message = xmlExc.Message;
			Severity = XmlSeverityType.Error;
		}

		public int LineNumber;
		public int LinePosition;
		public string Message;
		public XmlSeverityType Severity;

		public override string ToString()
		{
			return String.Format("{0}: {1} (Line:{2}, Position:{3})"
			                     , Severity, Message, LineNumber, LinePosition);
		}
	}
}