using System.Xml;

namespace Naak.HtmlRules
{
	public interface IXmlNamespaceManagerBuilder
	{
		XmlNamespaceManager Build(XmlDocument document);
	}
}