using System.Text;
using Naak.HtmlRules.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Naak.UnitTests
{
	[TestFixture]
	public class FieldsetsHaveLegendsTester : HtmlRuleTester
	{
		[Test]
		public void Identifies_fieldsets_missing_legends()
		{
			var html = new StringBuilder();
			html.Append(@"<form>");
			html.Append(@"  <fieldset>");
			html.Append(@"    <legend>Search Fields</legend>");
			html.Append(@"  </fieldset>");
			html.Append(@"</form>");

			html.Append(@"<form>");
			html.Append(@"  <fieldset>");
			html.Append(@"    <legend />");
			html.Append(@"  </fieldset>");
			html.Append(@"</form>");

			html.Append(@"<form>");
			html.Append(@"  <fieldset />");
			html.Append(@"</form>");

			ExecuteTest(new FieldsetsHaveLegends(), html.ToString());

			Assert.That(ErrorCount, Is.EqualTo(2));
		}

		[Test]
		public void Correctly_validates_document_with_h1_tag()
		{
			const string bodyHtml = "<h1>Sample Heading</h1>";

			ExecuteTest(new AtLeastOneH1(), bodyHtml);

			Assert.That(ErrorCount, Is.EqualTo(0));
		}
	}
}