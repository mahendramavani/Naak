using System.Xml;
using Naak.HtmlRules;

namespace Naak.HtmlRules.Impl
{
	public class XmlNamespaceManagerBuilder : IXmlNamespaceManagerBuilder
	{
		public XmlNamespaceManager Build(XmlDocument document)
		{
			var namespaceManager = new XmlNamespaceManager(document.NameTable);
			namespaceManager.AddNamespace("x", "http://www.w3.org/1999/xhtml");
			return namespaceManager;
		}
	}
}