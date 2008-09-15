using System;
using System.Collections.ObjectModel;
using System.Web;
using System.Xml;
using StructureMap;
using Tjoc.Web.Validator;

namespace Naak.HtmlRules.Impl
{
	public class AccessibilityRenderer : IValidationRenderer
	{
		public void Render(HttpResponse response, Collection<ValidationRecord> errors, TimeSpan validationDuration, string html)
		{
			var htmlRenderer = new InlineHtmlRenderer();

			if (errors.Count == 0)
			{
				var executor = ObjectFactory.GetInstance<IHtmlRuleExecutor>();

				var accessibilityErrors = executor.GetAccessibilityErrors(html);

				foreach (ValidationError error in accessibilityErrors)
				{
					errors.Add(new ValidationRecord(new XmlException(error.Message)));
				}
			}

			htmlRenderer.Render(response, errors, validationDuration, html);
		}
	}
}