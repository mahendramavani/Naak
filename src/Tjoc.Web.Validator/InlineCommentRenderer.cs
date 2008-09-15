using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;

namespace Tjoc.Web.Validator
{
	/// <summary>
	/// HTML comments implementation of the rendering engine
	/// </summary>
	public class InlineCommentRenderer : IValidationRenderer
	{
		/// <summary>
		/// Renders the report in HTML comments added to the source
		/// </summary>
		/// <param name="response">The response object which maybe used to write/output the report</param>
		/// <param name="errors">The source data for the report</param>
		/// <param name="validationDuration">The time taken to generate the validation data</param>
		public void Render(HttpResponse response, Collection<ValidationRecord> errors, TimeSpan validationDuration, string html)
		{
			var comments = new StringBuilder();

			comments.AppendLine();
			comments.AppendLine();

			comments.AppendLine("<!-- START OF VALIDATOR REPORT ************ ");

			comments.AppendLine();

			comments.AppendFormat("Validation took: {0}ms", validationDuration.TotalMilliseconds);

			comments.AppendLine();
			comments.AppendLine();

			if (errors.Count > 0)
			{
				foreach (ValidationRecord record in errors)
				{
					comments.Append(" - ");
					comments.AppendLine(record.ToString());
					comments.AppendLine();
				}
			}
			else
			{
				comments.AppendLine("Congratulations: No errors!");
			}

			comments.AppendLine();
			comments.AppendLine("************ END OF VALIDATOR REPORT -->");

			response.Write(comments.ToString());
		}
	}
}