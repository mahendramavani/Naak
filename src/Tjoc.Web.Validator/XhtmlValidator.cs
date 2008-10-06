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
			xrs.XmlResolver = new LocalDTDUrlResolver();

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
}