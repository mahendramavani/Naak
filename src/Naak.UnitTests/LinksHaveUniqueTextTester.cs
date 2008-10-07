using System.Net.Mime;
using System.Text;
using Naak.HtmlRules.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Naak.UnitTests
{
	[TestFixture]
	public class LinksHaveUniqueTextTester : HtmlRuleTester
	{
		[Test]
		public void Identifies_images_tag_wtih_duplicate_text()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<a href=""/home"">Test</a>"); 
			bodyHtml.Append(@"<a href=""/home"">TestLink</a>"); 
			bodyHtml.Append(@"<a href=""/home"">TestLink</a>");
			bodyHtml.Append(@"<P/>");

			ExecuteTest(new LinksHaveUniqueText(), bodyHtml.ToString());
			Assert.That(ErrorCount, Is.EqualTo(1));
			Assert.That(ContainsError(@"Link has duplicate text: <a href=""/home"" xmlns=""http://www.w3.org/1999/xhtml"">TestLink</a>"));
		}


	}
}