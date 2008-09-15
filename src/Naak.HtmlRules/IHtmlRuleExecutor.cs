using Naak.HtmlRules.Impl;

namespace Naak.HtmlRules
{
	public interface IHtmlRuleExecutor
	{
		ValidationError[] GetAccessibilityErrors(string html);
	}
}