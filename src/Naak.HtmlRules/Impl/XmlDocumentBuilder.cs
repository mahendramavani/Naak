using System.Xml;
using Naak.HtmlRules;

namespace Naak.HtmlRules.Impl
{
	public class XmlDocumentBuilder : IXmlDocumentBuilder
	{
		public XmlDocument Build(string xml)
		{
			var document = new XmlDocument();
			document.LoadXml(xml);
			return document;
		}
	}
}