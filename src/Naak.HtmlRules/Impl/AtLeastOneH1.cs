using System.Collections.Generic;
using HtmlAgilityPack;

namespace Naak.HtmlRules.Impl
{
    [HtmlRule]
	public class AtLeastOneH1 : IHtmlRule
	{
		public ValidationError[] ValidateHtml(HtmlDocument document)
		{
			var records = new List<ValidationError>();

			var nodes = document.SelectNodes("//h1");

			if (nodes.Count == 0)
			{
				records.Add(new ValidationError("There must be at least one H1 tag on the page"));
			}

			return records.ToArray();
		}
	}
}