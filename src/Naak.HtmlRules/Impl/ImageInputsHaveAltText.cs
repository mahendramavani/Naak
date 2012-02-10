using System.Collections.Generic;
using System.Xml;

namespace Naak.HtmlRules.Impl
{
	public class ImageInputsHaveAltText : IHtmlRule
	{
		public ValidationError[] ValidateHtml(XmlDocument document)
		{
			var records = new List<ValidationError>();

			string formElementXPath = "//input[@type='image'][not(@alt) or @alt='']";

			XmlNodeList imageButtonsWithoutAlt = document.SelectNodes(formElementXPath);

			foreach (XmlNode imageButton in imageButtonsWithoutAlt)
			{
				string message = string.Format("Image input missing alt text: {0}", imageButton.OuterXml);
				records.Add(new ValidationError(message));
			}

			return records.ToArray();
		}
	}
}