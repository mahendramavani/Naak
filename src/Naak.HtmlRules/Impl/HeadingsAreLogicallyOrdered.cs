using System;
using System.Collections.Generic;
using System.Xml;

namespace Naak.HtmlRules.Impl
{
	public class HeadingsAreLogicallyOrdered : IHtmlRule
	{
		public ValidationError[] ValidateHtml(XmlDocument document)
		{
			var records = new List<ValidationError>();
			var headingNodes = new List<XmlNode>();

			PopulateHeadingNodeList(document, headingNodes);

			int currentLevel = 0;

			foreach (XmlNode headingNode in headingNodes)
			{
				var expectedLevel = currentLevel + 1;
				var foundLevel = int.Parse(headingNode.Name.Replace("h", string.Empty));

				if (foundLevel > expectedLevel)
				{
					var message = string.Format("Illogical heading order: Expected to find <h{0}> but found {1} instead", expectedLevel, headingNode.OuterXml);
					records.Add(new ValidationError(message));
					break;
				}

				currentLevel = foundLevel;
			}

			return records.ToArray();
		}

		private static void PopulateHeadingNodeList(XmlNode parentNode, ICollection<XmlNode> nodes)
		{
			foreach (XmlNode childNode in parentNode.ChildNodes)
			{
				var name = childNode.Name;
				if (name == "h1" || name == "h2" || name == "h3" || name == "h4" || name == "h5" || name == "h6" )
				{
					nodes.Add(childNode);
				}

				PopulateHeadingNodeList(childNode, nodes);
			}
		}
	}
}