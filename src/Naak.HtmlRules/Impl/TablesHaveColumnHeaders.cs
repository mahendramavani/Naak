using System.Collections.Generic;
using System.Xml;

namespace Naak.HtmlRules.Impl
{
	public class TablesHaveColumnHeaders : IHtmlRule
	{
		public ValidationError[] ValidateHtml(XmlDocument document)
		{
			var records = new List<ValidationError>();

			XmlNodeList nodes = document.SelectNodes("//table");

			if (nodes != null)
				foreach (XmlNode node in nodes)
				{
					XmlNodeList tableHeaders = node.SelectNodes("tr/th");
					XmlNodeList tableHeadersWithThead = node.SelectNodes("thead/tr/th");
                    if (tableHeaders.Count < 1 && tableHeadersWithThead.Count < 1)
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