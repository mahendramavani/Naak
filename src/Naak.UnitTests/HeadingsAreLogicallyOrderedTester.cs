using System.Text;
using Naak.HtmlRules.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Naak.UnitTests
{
	[TestFixture]
	public class HeadingsAreLogicallyOrderedTester : HtmlRuleTester
	{
		[Test]
		public void Identifies_missing_h1_tag()
		{
			ExecuteTest(new HeadingsAreLogicallyOrdered(), @"<div><h2>Second-Level Heading</h2></div>");

			Assert.That(ErrorCount, Is.EqualTo(1));

			Assert.That(
				ContainsError(
					@"Illogical heading order: Expected to find <h1> but found <h2 xmlns=""http://www.w3.org/1999/xhtml"">Second-Level Heading</h2> instead"));
		}

		[Test]
		public void Identifies_missing_h2_tag()
		{
			var html = new StringBuilder();
			html.Append("<h1 />");
			html.Append("<div><h3 /></div>");

			ExecuteTest(new HeadingsAreLogicallyOrdered(), html.ToString());

			Assert.That(ErrorCount, Is.EqualTo(1));

			Assert.That(
				ContainsError(
					@"Illogical heading order: Expected to find <h2> but found <h3 xmlns=""http://www.w3.org/1999/xhtml"" /> instead"));
		}

		[Test]
		public void Identifies_missing_header_in_complex_document()
		{
			var html = new StringBuilder();
			html.Append("<h1 />");
			html.Append("<div>");
			html.Append("  <h2 />");
			html.Append("  <div>");
			html.Append("    <h3 />");
			html.Append("  </div>");
			html.Append("  <h2 />");
			html.Append("  <div />");
			html.Append("  <div>");
			html.Append("    <div>");
			html.Append("      <h4 />");
			html.Append("    </div>");
			html.Append("    <h2 />");
			html.Append("    <div>");
			html.Append("      <h3 />");
			html.Append("    </div>");
			html.Append("  </div>");
			html.Append("</div>");

			ExecuteTest(new HeadingsAreLogicallyOrdered(), html.ToString());

			Assert.That(ErrorCount, Is.EqualTo(1));

			Assert.That(
				ContainsError(
					@"Illogical heading order: Expected to find <h3> but found <h4 xmlns=""http://www.w3.org/1999/xhtml"" /> instead"));
		}
	}
}