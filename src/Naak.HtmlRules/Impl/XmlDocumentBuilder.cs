using System.Xml;
using Naak.HtmlRules;
using Tjoc.Web.Validator;

namespace Naak.HtmlRules.Impl
{
	public class XmlDocumentBuilder : IXmlDocumentBuilder
	{
		public XmlDocument Build(string xml)
		{
			var document = new XmlDocument();
			document.XmlResolver = new LocalDTDUrlResolver();
			document.LoadXml(xml);
			return document;
		}
	}
}