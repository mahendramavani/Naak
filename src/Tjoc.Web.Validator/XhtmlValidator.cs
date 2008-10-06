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
			XmlValidatingReader reader;
			var myschema = new XmlSchemaCollection();

			try
			{
				//Create the XmlParserContext.
				var context = new XmlParserContext(null, null, "", XmlSpace.None);

				reader = new XmlValidatingReader(_document, XmlNodeType.Element, context);
				reader.ValidationEventHandler += xrs_ValidationEventHandler;
				//Add the schema.
				string schemaFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xhtml1-transitional.xsd");
				myschema.Add("http://www.w3.org/1999/xhtml", schemaFile);

				//Set the schema type and add the schema to the reader.
				reader.ValidationType = ValidationType.Schema;
				reader.Schemas.Add(myschema);

				while (reader.Read())
				{
				}
			}
			catch (XmlException xmlExp)
			{
				_records.Add(new ValidationRecord(xmlExp));
			}
			catch (Exception exc)
			{
				_records.Add(new ValidationRecord(exc.Message));
			}

			return _records;
		}

		private void xrs_ValidationEventHandler(object sender, ValidationEventArgs e)
		{
			_records.Add(new ValidationRecord(e));
		}
	}
}