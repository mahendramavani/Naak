using System.Collections.Generic;
using HtmlAgilityPack;

namespace Naak.HtmlRules.Impl
{
    [HtmlRule]
	public class ImagesHaveAltText : IHtmlRule
	{
		public ValidationError[] ValidateHtml(HtmlDocument document)
		{
			var records = new List<ValidationError>();

			const string formElementXPath = "//img[not(@alt) or @alt='']";

			var imagesWithoutAlt = document.SelectNodes(formElementXPath);

			if (imagesWithoutAlt != null)
			{
                foreach (var imageButton in imagesWithoutAlt)
                {
                    string message = string.Format("Image missing alt text: {0}", imageButton.OuterHtml);
                    records.Add(new ValidationError(message, imageButton.Line, imageButton.LinePosition));
                }
			}
			return records.ToArray();
		}
	}
}