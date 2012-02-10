using System.Collections.Generic;
using System.Xml;
using HtmlAgilityPack;

namespace Naak.HtmlRules.Impl
{
	public class ImagesHaveAltText : IHtmlRule
	{
		public ValidationError[] ValidateHtml(HtmlDocument document)
		{
			var records = new List<ValidationError>();

			string formElementXPath = "//img[not(@alt) or @alt='']";

			var imageButtonsWithoutAlt = document.SelectNodes(formElementXPath);

			if (imageButtonsWithoutAlt != null)
				foreach (var imageButton in imageButtonsWithoutAlt)
				{
					string message = string.Format("Image missing alt text: {0}", imageButton.OuterHtml);
					records.Add(new ValidationError(message));
				}

			return records.ToArray();
		}
	}
}