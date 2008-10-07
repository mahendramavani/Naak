using System.Collections.Generic;
using System.Xml;

namespace Naak.HtmlRules.Impl
{
	public class LinksHaveUniqueText : IHtmlRule
	{
		public ValidationError[] ValidateHtml(XmlDocument document, XmlNamespaceManager namespaceManager)
		{
			var records = new List<ValidationError>();
			var linkTextLibrary = new List<string>();
			const string formElementXPath = "//x:a";
			
			

			XmlNodeList linksNodeList = document.SelectNodes(formElementXPath, namespaceManager);
			foreach (XmlNode currentNode in linksNodeList)
			{
				var linkText = currentNode.InnerText;
				if (linkTextLibrary.Contains(linkText))
				{
					string message = string.Format("Link has duplicate text: {0}", currentNode.OuterXml);
					records.Add(new ValidationError(message));
				}
				linkTextLibrary.Add(linkText);
			}

			return records.ToArray();
		}
	}
}