using System.Collections.Generic;
using System.Xml;
using Naak.HtmlRules;
using Naak.HtmlRules.Impl;

namespace Naak.HtmlRules.Impl
{
	public class TablesHaveColumnHeaders : IHtmlRule
	{
		public ValidationError[] ValidateHtml(XmlDocument document, XmlNamespaceManager namespaceManager)
		{
			var records = new List<ValidationError>();

			XmlNodeList nodes = document.SelectNodes("//x:table", namespaceManager);

			if (nodes != null)
				foreach (XmlNode node in nodes)
				{
					XmlNodeList tableHeaders = node.SelectNodes("x:tr/x:th", namespaceManager);
					if (tableHeaders.Count < 1)
					{
						string message =
							string.Format(
								"Layout table detected - if the table is a data table, use TH for the column or row headers. Otherwise, use CSS for layout: {0}",
								node.OuterXml);
						records.Add(new ValidationError(message));
					}
				}
			return records.ToArray();
		}
	}
}