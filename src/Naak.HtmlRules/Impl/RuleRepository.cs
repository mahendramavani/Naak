using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Naak.HtmlRules.Impl
{
	public class RuleRepository : IRuleRepository
	{
		private readonly IServiceLocator _serviceLocator;
		private readonly IRuleConfiguration _configuration;

		public RuleRepository(IServiceLocator serviceLocator, IRuleConfiguration configuration)
		{
			_serviceLocator = serviceLocator;
			_configuration = configuration;
		}

		public IHtmlRule[] GetNaakRulesToExecute()
		{
			var rules = new List<IHtmlRule>();

			IHtmlRule[] htmlRules = _serviceLocator.CreateAllInstances<IHtmlRule>();

			foreach (IHtmlRule rule in htmlRules)
			{
				if (_configuration.ShouldRuleBeIncludedBasedOnConfiguration(rule))
				{
					rules.Add(rule);
				}
			}

			return rules.ToArray();
		}
	}
}