using Naak.HtmlRules.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Naak.UnitTests
{
	[TestFixture]
	public class AtLeastOneH1Tester : HtmlRuleTester
	{
		[Test]
		public void Correctly_identifies_missing_h1_tag()
		{
			const string bodyHtml = "<span>Invalid Body</span>";

			ExecuteTest(new AtLeastOneH1(), bodyHtml);

			Assert.That(ErrorCount, Is.EqualTo(1));
			Assert.That(ContainsError("There must be at least one H1 tag on the page"));
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