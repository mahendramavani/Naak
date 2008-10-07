
using System;
using System.Xml;
using System.Xml.Schema;

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
