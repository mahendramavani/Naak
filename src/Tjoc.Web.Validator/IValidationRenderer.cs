using System;
using System.Collections.ObjectModel;
using System.Web;

namespace Tjoc.Web.Validator
{
	public interface IValidationRenderer
	{
		void Render(HttpResponse response, Collection<ValidationRecord> errors, TimeSpan validationDuration, string html);
	}
}