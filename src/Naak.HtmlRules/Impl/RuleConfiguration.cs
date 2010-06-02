using System.Configuration;
using System.Linq;

namespace Naak.HtmlRules.Impl
{
	public class RuleConfiguration : IRuleConfiguration
	{
		public bool ShouldRuleBeIncludedBasedOnConfiguration(IHtmlRule rule)
		{
			string ruleName = rule.GetType().Name;

			string appSetting = ConfigurationManager.AppSettings["NaakRules"];
			string[] configuredRules = appSetting == null ? new string[0] : appSetting.Split(',');

			bool ruleConfigurationNotSpecified = configuredRules.Length == 0;
			bool includeRule = ruleConfigurationNotSpecified || configuredRules.Any(s => s.Trim() == ruleName);

			return includeRule;
		}
	}
}