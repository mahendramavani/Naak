using System.Collections.Generic;
using System.Xml;

namespace Naak.HtmlRules.Impl
{
	public class LabelsRelateToFormElements : IHtmlRule
	{
		public ValidationError[] ValidateHtml(XmlDocument document, XmlNamespaceManager namespaceManager)
		{
			var records = new List<ValidationError>();

			XmlNodeList labels = document.SelectNodes("//x:label", namespaceManager);

			foreach (XmlNode label in labels)
			{
				XmlAttribute forAttribute = label.Attributes["for"];
				XmlNode relatedElement = null;

				if (forAttribute != null)
				{
					string labelId = forAttribute.Value;
					string xpath = string.Format("//x:input[@id='{0}']", labelId);

					relatedElement = document.SelectSingleNode(xpath, namespaceManager);

					if (relatedElement == null)
					{
						xpath = string.Format("//x:select[@id='{0}']", labelId);
						relatedElement = document.SelectSingleNode(xpath, namespaceManager);
					}

					if (relatedElement == null)
					{
						xpath = string.Format("//x:textarea[@id='{0}']", labelId);
						relatedElement = document.SelectSingleNode(xpath, namespaceManager);
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