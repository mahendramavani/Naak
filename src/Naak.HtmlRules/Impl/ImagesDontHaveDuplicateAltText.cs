using System.Collections.Generic;
using System.Xml;

namespace Naak.HtmlRules.Impl
{
	public class ImagesDontHaveDuplicateAltText : IHtmlRule
	{

		public ValidationError[] ValidateHtml(XmlDocument document, XmlNamespaceManager namespaceManager)
		{
			var records = new List<ValidationError>();
			var previousAltText = new List<string>();

			const string formElementXPath = "//x:img";

			XmlNodeList images = document.SelectNodes(formElementXPath, namespaceManager);

			foreach (XmlNode currentImage in images)
			{
				var altTextAttribute = currentImage.Attributes["alt"];
				if (altTextAttribute != null)
				{
					if (previousAltText.Contains(altTextAttribute.Value))
					{
						string message = string.Format("Image has duplicate alt text: {0}", currentImage.OuterXml);
						records.Add(new ValidationError(message));
					}
					previousAltText.Add(altTextAttribute.Value);
				}
			}
			return records.ToArray();
		}
	}
}