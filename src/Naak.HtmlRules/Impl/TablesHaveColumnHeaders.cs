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

			XmlNodeList nodes = document.SelectNodes("//x:table[x:tr[1]/x:td]", namespaceManager);

			foreach (XmlNode node in nodes)
			{
				string message =
					string.Format(
						"Layout table detected - if the table is a data table, use TH for the column headers. Otherwise, use CSS for layout: {0}",
						node.OuterXml);
				records.Add(new ValidationError(message));
			}

			return records.ToArray();
		}
	}
}