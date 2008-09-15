using System.Collections.Generic;
using StructureMap;
using Tarantino.Core.Commons.Services.Configuration;
using Tarantino.Infrastructure;

namespace Naak.HtmlRules.Impl
{
	public class HtmlRuleExecutor : IHtmlRuleExecutor
	{
		private readonly IServiceLocator _serviceLocator;
		private readonly IXmlDocumentBuilder _documentBuilder;
		private readonly IXmlNamespaceManagerBuilder _namespaceManagerBuilder;

		public HtmlRuleExecutor(IServiceLocator serviceLocator, IXmlDocumentBuilder documentBuilder, IXmlNamespaceManagerBuilder namespaceManagerBuilder)
		{
			_serviceLocator = serviceLocator;
			_documentBuilder = documentBuilder;
			_namespaceManagerBuilder = namespaceManagerBuilder;
		}

		public ValidationError[] GetAccessibilityErrors(string html)
		{
			var records = new List<ValidationError>();

			var document = _documentBuilder.Build(html);
			var namespaceManager = _namespaceManagerBuilder.Build(document);

			foreach (var htmlRule in _serviceLocator.CreateAllInstances<IHtmlRule>())
			{
				var currentRecords = htmlRule.ValidateHtml(document, namespaceManager);

				foreach (var record in currentRecords)
				{
					records.Add(record);
				}
			}

			return records.ToArray();
		}
	}
}