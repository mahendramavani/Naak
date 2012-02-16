using System.Collections.Generic;
using HtmlAgilityPack;

namespace Naak.HtmlRules.Impl
{
    [HtmlRule]
	public class FieldsetsHaveLegends : IHtmlRule
	{
		public ValidationError[] ValidateHtml(HtmlDocument document)
		{
			var records = new List<ValidationError>();

			var nodes = document.SelectNodes("//fieldset[not(legend) or legend[not(text())]]");

			foreach (var node in nodes)
			{
				string message = string.Format("Fieldset must have a legend: {0}", node.OuterHtml);
				records.Add(new ValidationError(message, node.Line, node.LinePosition));
			}

			return records.ToArray();
		}
	}
}