using System.Xml;
using Naak.HtmlRules.Impl;

namespace Naak.HtmlRules
{
	public interface IHtmlRule
	{
		ValidationError[] ValidateHtml(XmlDocument document, XmlNamespaceManager namespaceManager);
	}
}