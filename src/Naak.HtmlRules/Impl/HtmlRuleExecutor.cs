using System;
using System.Collections.Generic;
using System.Xml;

namespace Naak.HtmlRules.Impl
{
	public class HtmlRuleExecutor : IHtmlRuleExecutor
	{
		private readonly IXmlDocumentBuilder _documentBuilder;
	    private readonly IRuleRepository _repository;

		public HtmlRuleExecutor(IRuleRepository repository, IXmlDocumentBuilder documentBuilder)
		{
			_repository = repository;
			_documentBuilder = documentBuilder;
		}

		public ValidationError[] GetAccessibilityErrors(string html)
		{
			var records = new List<ValidationError>();

			try
			{
				XmlDocument document = _documentBuilder.Build(html);

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
	}
}