using System;
using System.Collections.Generic;
using System.Xml;

namespace Naak.HtmlRules.Impl
{
	public class FormElementsHaveLabels : IHtmlRule
	{
		public ValidationError[] ValidateHtml(XmlDocument document)
		{
			var records = new List<ValidationError>();

			var textDefinition = new {TagName = "text", Description = "Textbox", IsInput = true};
			var passwordDefinition = new {TagName = "password", Description = "Password textbox", IsInput = true};
			var checkboxDefinition = new {TagName = "checkbox", Description = "Checkbox", IsInput = true};
			var selectDefinition = new { TagName = "select", Description = "Select list", IsInput = false };
			var textAreaDefinition = new { TagName = "textarea", Description = "Text area", IsInput = false };

			var definitions = new[] {textDefinition, passwordDefinition, checkboxDefinition, selectDefinition, textAreaDefinition};

			foreach (var definition in definitions)
			{
				var formElementXPath = definition.IsInput ? string.Format("//input[@type='{0}']", definition.TagName) : string.Format("//{0}", definition.TagName);

				var elements = document.SelectNodes(formElementXPath);

				foreach (XmlNode element in elements)
				{
					var idAttribute = element.Attributes["id"];

					XmlNode correspondingLabel = null;

					if (idAttribute != null)
					{
						var elementId = idAttribute.Value;
						var xpath = string.Format("//label[@for='{0}']", elementId);

						correspondingLabel = document.SelectSingleNode(xpath);
					}

					if (correspondingLabel == null)
					{
						var message = string.Format("{0} missing correpsonding label: {1}", definition.Description, element.OuterXml);
						records.Add(new ValidationError(message));
					}
				}
			}

			return records.ToArray();
		}
	}
}