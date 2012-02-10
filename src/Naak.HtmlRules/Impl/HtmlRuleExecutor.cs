using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace Naak.HtmlRules.Impl
{
	public class HtmlRuleExecutor : IHtmlRuleExecutor
	{
	    private readonly IRuleRepository _repository;

		public HtmlRuleExecutor(IRuleRepository repository)
		{
			_repository = repository;
		}

		public ValidationError[] GetAccessibilityErrors(string html)
		{
			var records = new List<ValidationError>();

			try
			{
				var document = BuildDocument(html);

				foreach (var htmlRule in _repository.GetNaakRulesToExecute())
				{
					ValidationError[] currentRecords = htmlRule.ValidateHtml(document);

					foreach (var record in currentRecords)
					{
						records.Add(record);
					}
				}
			}
			catch (Exception exc)
			{
				records.Add(new ValidationError(exc.Message));
			}

			return records.ToArray();
		}

	    private static HtmlDocument BuildDocument(string html)
	    {
            var document = new HtmlDocument();
            document.LoadHtml(html);
            return document;
        }
	}
}