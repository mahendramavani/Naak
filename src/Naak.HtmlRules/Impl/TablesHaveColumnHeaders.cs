using System.Collections.Generic;
using HtmlAgilityPack;

namespace Naak.HtmlRules.Impl
{
	public class TablesHaveColumnHeaders : IHtmlRule
	{
		public ValidationError[] ValidateHtml(HtmlDocument document)
		{
			var records = new List<ValidationError>();

			var nodes = document.SelectNodes("//table");

			if (nodes != null)
				foreach (var node in nodes)
				{
					var tableHeaders = node.SelectNodes("tr/th");
					var tableHeadersWithThead = node.SelectNodes("thead/tr/th");
                    if (tableHeaders.Count < 1 && tableHeadersWithThead.Count < 1)
					{
						string message =
							string.Format(
								"Layout table detected - if the table is a data table, use TH for the column or row headers. Otherwise, use CSS for layout: {0}",
								node.OuterHtml);
						records.Add(new ValidationError(message));
					}
				}
			return records.ToArray();
		}
	}
}