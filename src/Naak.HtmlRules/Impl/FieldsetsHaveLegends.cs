using System;
using System.Collections.Generic;
using System.Xml;

namespace Naak.HtmlRules.Impl
{
	public class FieldsetsHaveLegends : IHtmlRule
	{
		public ValidationError[] ValidateHtml(XmlDocument document)
		{
			var records = new List<ValidationError>();

			XmlNodeList nodes = document.SelectNodes("//fieldset[not(legend) or legend[not(text())]]");

			foreach (XmlNode node in nodes)
			{
				string message = string.Format("Fieldset must have a legend: {0}", node.OuterXml);
				records.Add(new ValidationError(message));
			}

			return records.ToArray();
		}
	}
}