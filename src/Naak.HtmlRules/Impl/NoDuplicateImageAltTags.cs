using System.Collections.Generic;
using HtmlAgilityPack;

namespace Naak.HtmlRules.Impl
{
    [HtmlRule]
	public class NoDuplicateImageAltTags : IHtmlRule
	{
		public ValidationError[] ValidateHtml(HtmlDocument document)
		{
			var records = new List<ValidationError>();

			//XmlNodeList nodes = document.SelectNodes("//img", namespaceManager);
			//var nodeList = new List<XmlNode>();

			//foreach (XmlNode node in nodes)
			//{
			//  nodeList.Add(node);
			//}

			//var distinctAltTexts = new List<string>();

			//foreach (var node in nodeList)
			//{
			//  XmlAttribute attribute = node.Attributes["alt"];
			//  if (attribute != null && !distinctAltTexts.Contains(attribute.InnerText))
			//  {
			//    distinctAltTexts.Add(attribute.InnerText);
			//  }
			//}

			//foreach (var altText in distinctAltTexts)
			//{
			//  IEnumerable<XmlNode> matchingNodes = nodeList.FindAll(node => node.Attributes["alt"].InnerText == altText);

			//  if (matchingNodes.Count() > 1)
			//  {
			//    var message = new StringBuilder();
			//    message.Append("");

			//    new ValidationError("There must be at least one H1 tag on the page")
			//  }
			//}

			//if (nodes.Count == 0)
			//{
			//  records.Add(new ValidationError("There must be at least one H1 tag on the page"));
			//}

			return records.ToArray();
		}
	}
}