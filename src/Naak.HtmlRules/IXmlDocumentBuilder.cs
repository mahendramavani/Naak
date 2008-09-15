using System.Xml;

namespace Naak.HtmlRules
{
	public interface IXmlDocumentBuilder
	{
		XmlDocument Build(string xml);
	}
}