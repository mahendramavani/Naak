namespace Naak.HtmlRules
{
	public interface IRuleConfiguration
	{
		bool ShouldRuleBeIncludedBasedOnConfiguration(IHtmlRule rule);
	}
}