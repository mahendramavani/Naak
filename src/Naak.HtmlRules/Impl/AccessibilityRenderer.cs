using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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
			if (errors.Count == 0)
			{
				var executor = ObjectFactory.GetInstance<IHtmlRuleExecutor>();

				var accessibilityErrors = executor.GetAccessibilityErrors(html);

				foreach (ValidationError error in accessibilityErrors)
				{
					errors.Add(new ValidationRecord(new XmlException(error.Message)));
				}
			}

			RenderHtml(response, errors, validationDuration);
		}

		private static void RenderHtml(HttpResponse response, ICollection<ValidationRecord> errors, TimeSpan validationDuration)
		{
			var body = new StringBuilder();

			body.AppendLine();
			body.AppendLine();

			body.AppendLine("<!-- START OF VALIDATOR REPORT || this html would not appear in the original document and, unfortunately, this paradoxically invalidates your source :\\ -->");

			body.AppendLine();
			body.AppendLine();

			body.Append("<div id='tjocWebValidatorModuleReport' style='");
			body.Append("clear:both;padding:10px;font-size:16pt;");

			if (errors.Count > 0)
			{
				body.Append("background-color:pink;border:2px solid red;'>");
			}
			else
			{
				body.Append("background-color:#cfc;border:2px solid green;'>");
			}

			body.Append("<strong>US Rehabilitation Act, Section 508 Validation ");

			if (errors.Count > 0)
			{
				body.Append("Failed");
			}
			else
			{
				body.Append("Passed");
			}

			body.Append("</strong>");

			body.AppendFormat(@"<p style=""margin-top: 12px; margin-bottom: 0px; font-size: 12pt;"">Validation took: {0}ms</p>", validationDuration.TotalMilliseconds);

			if (errors.Count > 0)
			{
				body.Append(@"<ul id=""naakErrors"" style=""font-size: 10pt;"">");
				foreach (ValidationRecord record in errors)
				{
					body.Append(@"<li style=""margin-top: 5px;"">");
					body.Append(HttpUtility.HtmlEncode(record.ToString()));
					body.Append("</li>");
				}
				body.Append("</ul>");
			}

			body.Append(@"<p style=""margin-top: 24px; margin-bottom: 0px; font-size: 12pt;"">XHTML validation performed by <a href=""http://www.thejoyofcode.com/Validator_Module.aspx"">Validator Module</a>, shared generously by <a href=""mailto://josh@thejoyofcode.com"">Josh Twist</a>, <a href=""http://www.thejoyofcode.com/"">http://www.thejoyofcode.com/</a></p>");
			body.Append(@"<p style=""margin-top: 9px; margin-bottom: 0px; font-size: 12pt;"">Accessibility validation performed by <a href=""http://code.google.com/p/naak"">.NET Accessibility Analysis Kit (NAAK)</a></p>");
			body.Append("</div>");

			response.Write(body.ToString());
		}
	}
}