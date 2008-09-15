using System.Text;
using Naak.HtmlRules.Impl;
using Naak.UnitTests;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Naak.UnitTests
{
	[TestFixture]
	public class ImagesHaveAltTextTester : HtmlRuleTester
	{
		[Test]
		public void Identifies_images_missing_alt_text()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<img alt=""""/>");
			bodyHtml.Append(@"<img/>");
			bodyHtml.Append(@"<img alt=""Description""/>");

			ExecuteTest(new ImagesHaveAltText(), bodyHtml.ToString());

			Assert.That(ErrorCount, Is.EqualTo(2));
			Assert.That(ContainsError(@"Image missing alt text: <img alt="""" xmlns=""http://www.w3.org/1999/xhtml"" />"));
			Assert.That(ContainsError(@"Image missing alt text: <img xmlns=""http://www.w3.org/1999/xhtml"" />"));
		}
	}
}