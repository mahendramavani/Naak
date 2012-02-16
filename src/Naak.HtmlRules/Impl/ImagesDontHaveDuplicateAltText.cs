using System.Collections.Generic;
using HtmlAgilityPack;

namespace Naak.HtmlRules.Impl
{
    [HtmlRule]
	public class ImagesDontHaveDuplicateAltText : IHtmlRule
	{

		public ValidationError[] ValidateHtml(HtmlDocument document)
		{
			var records = new List<ValidationError>();
			var previousAltText = new List<string>();

			const string formElementXPath = "//img";

			var images = document.SelectNodes(formElementXPath);

			foreach (var currentImage in images)
			{
				var altTextAttribute = currentImage.Attributes["alt"];
				if (altTextAttribute != null)
				{
					if (previousAltText.Contains(altTextAttribute.Value))
					{
						string message = string.Format("Image has duplicate alt text: {0}", currentImage.OuterHtml);
						records.Add(new ValidationError(message));
					}
					previousAltText.Add(altTextAttribute.Value);
				}
			}
			return records.ToArray();
		}
	}
}