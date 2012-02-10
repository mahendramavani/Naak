using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Naak.HtmlRules.Impl
{
	public class ContextOfLinkTextMustMakeSense : IHtmlRule
	{
		public class Link
		{
			private readonly string _url;
			private readonly string _text;
			private readonly string _title;
			private readonly string _name;

			public Link(string url, string text, string title, string name)
			{
				_name = name;
				_url = url;
				_text = text;
				_title = title;
			}

			public string Url
			{
				get { return _url; }
			}

			public string Text
			{
				get { return _text; }
			}

			public string Title
			{
				get { return _title; }
			}

			public string Name
			{
				get { return _name; }
			}
		}

		public ValidationError[] ValidateHtml(HtmlDocument document)
		{
			var records = new List<ValidationError>();

			var existingLinks = new List<Link>();
			const string formElementXPath = "//a";

			var linksNodeList = document.SelectNodes(formElementXPath);

			if (linksNodeList != null)
				foreach (var currentNode in linksNodeList)
				{
					var urlAttribute = currentNode.Attributes["href"];
					var titleAttribute = currentNode.Attributes["title"];
					var nameAttribute = currentNode.Attributes["name"];
					var linkText = currentNode.InnerText;

					var url = urlAttribute == null ? string.Empty : urlAttribute.Value;
					var title = titleAttribute == null ? string.Empty : titleAttribute.Value;
					var name = nameAttribute == null ? string.Empty : nameAttribute.Value;

					var link = new Link(url, linkText, title, name);

					var matches = from c in existingLinks
					              where c.Url != link.Url && c.Text == link.Text && c.Title == link.Title && c.Name == link.Name
					              select c;

					if (matches.Count() == 0)
						existingLinks.Add(link);
					else
					{
						records.Add(
							new ValidationError(
								string.Format(
									"Link text does not make sense out of context,link is the same as another link on the page, but links to a different location: {0}",
									currentNode.OuterHtml)));
					}
				}

			return records.ToArray();
		}
	}
}