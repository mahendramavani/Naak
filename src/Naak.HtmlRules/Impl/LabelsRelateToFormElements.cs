using System.Collections.Generic;
using System.Xml;

namespace Naak.HtmlRules.Impl
{
	public class LabelsRelateToFormElements : IHtmlRule
	{
		public ValidationError[] ValidateHtml(XmlDocument document)
		{
			var records = new List<ValidationError>();

			XmlNodeList labels = document.SelectNodes("//label");

			foreach (XmlNode label in labels)
			{
				XmlAttribute forAttribute = label.Attributes["for"];
				XmlNode relatedElement = null;

				if (forAttribute != null)
				{
					string labelId = forAttribute.Value;
					string xpath = string.Format("//input[@id='{0}']", labelId);

					relatedElement = document.SelectSingleNode(xpath);

					if (relatedElement == null)
					{
						xpath = string.Format("//select[@id='{0}']", labelId);
						relatedElement = document.SelectSingleNode(xpath);
					}

					if (relatedElement == null)
					{
						xpath = string.Format("//textarea[@id='{0}']", labelId);
						relatedElement = document.SelectSingleNode(xpath);
					}
				}

				if (relatedElement == null)
				{
					string message = string.Format("Label does not relate to a form control: {0}", label.OuterXml);
					records.Add(new ValidationError(message));
				}
			}

			return records.ToArray();
		}
	}
}