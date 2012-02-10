using Naak.HtmlRules.Impl;
using HtmlAgilityPack;

namespace Naak.HtmlRules
{
	public interface IHtmlRule
	{
		ValidationError[] ValidateHtml(HtmlDocument document);
	}
}