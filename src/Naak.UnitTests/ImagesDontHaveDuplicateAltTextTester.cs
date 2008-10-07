using System.Net.Mime;
using System.Text;
using Naak.HtmlRules.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Naak.UnitTests
{
	[TestFixture]
	public class ImagesDontHaveDuplicateAltTextTester : HtmlRuleTester
	{
		[Test]
		public void Identifies_images_tag_wtih_duplicate_alt_text()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<img id=""i1"" alt=""Unique Test"" />");
			bodyHtml.Append(@"<P/>");
			bodyHtml.Append(@"<img  alt=""Description"" id=""i2"" />");
			bodyHtml.Append(@"<img id=""i3""  alt=""Description""/>");
			bodyHtml.Append(@"<img id=""i4""  alt=""Description""/>");

			ExecuteTest(new ImagesDontHaveDuplicateAltText(), bodyHtml.ToString());

			Assert.That(ErrorCount, Is.EqualTo(2));
			Assert.That(ContainsError(@"Image has duplicate alt text: <img id=""i3"" alt=""Description"" xmlns=""http://www.w3.org/1999/xhtml"" />"));
			Assert.That(ContainsError(@"Image has duplicate alt text: <img id=""i4"" alt=""Description"" xmlns=""http://www.w3.org/1999/xhtml"" />"));
		}


	}
}