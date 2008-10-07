using System.Collections.Generic;
using System.Xml;

namespace Naak.HtmlRules.Impl
{
	public class ImagesHaveAltText : IHtmlRule
	{
		public ValidationError[] ValidateHtml(XmlDocument document, XmlNamespaceManager namespaceManager)
		{
			var records = new List<ValidationError>();

			string formElementXPath = "//x:img[not(@alt) or @alt='']";

			XmlNodeList imageButtonsWithoutAlt = document.SelectNodes(formElementXPath, namespaceManager);

			if (imageButtonsWithoutAlt != null)
				foreach (XmlNode imageButton in imageButtonsWithoutAlt)
				{
					string message = string.Format("Image missing alt text: {0}", imageButton.OuterXml);
					records.Add(new ValidationError(message));
				}

			return records.ToArray();
		}
	}
}