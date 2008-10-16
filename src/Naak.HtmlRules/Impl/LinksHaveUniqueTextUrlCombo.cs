using System.Collections.Generic;
using System.Xml;

namespace Naak.HtmlRules.Impl
{
	public class LinksHaveUniqueTextUrlCombo : IHtmlRule
	{
		public class Link
		{
			public Link(string text, string url)
			{
				Text = text;
				Url = url;
			}
			public string Url { get; private set; }
			public string Text { get; private set; }
		}

		public ValidationError[] ValidateHtml(XmlDocument document, XmlNamespaceManager namespaceManager)
		{
			var records = new List<ValidationError>();

			var existingLinks = new Dictionary<string, Link>(); 
			const string formElementXPath = "//x:a";
			
			

			XmlNodeList linksNodeList = document.SelectNodes(formElementXPath, namespaceManager);
			
			foreach (XmlNode currentNode in linksNodeList)
			{
				
				var linkUrl = currentNode.Attributes["href"];
				var link = new Link(currentNode.InnerText, linkUrl.Value);
				if(existingLinks.ContainsKey(link.Text))
				{
					if (link.Url != existingLinks[link.Text].Url)
					{
						string message = string.Format("Link text does not make sense out of context,link is the same as another link on the page, but links to a different location: {0}", currentNode.OuterXml);
						records.Add(new ValidationError(message));
					}
				}
				else
				{
					existingLinks.Add(link.Text,link);
				}

			}

			return records.ToArray();
		}
	}
}