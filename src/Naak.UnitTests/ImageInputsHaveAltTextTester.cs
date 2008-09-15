using System.Text;
using Naak.HtmlRules.Impl;
using Naak.UnitTests;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Naak.UnitTests
{
	[TestFixture]
	public class ImageInputsHaveAltTextTester : HtmlRuleTester
	{
		[Test]
		public void Correctly_identifies_image_inputs_missing_alt_tag()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<form>");
			bodyHtml.Append(@"  <input type=""image"" id=""imgFirst"" alt=""""/>");
			bodyHtml.Append(@"  <input type=""image"" id=""imgSecond""/>");
			bodyHtml.Append(@"  <input type=""image"" id=""imgSecond"" alt=""Image Description""/>");
			bodyHtml.Append(@"</form>");

			ExecuteTest(new ImageInputsHaveAltText(), bodyHtml.ToString());

			Assert.That(ErrorCount, Is.EqualTo(2));
			Assert.That(ContainsError(@"Image input missing alt text: <input type=""image"" id=""imgFirst"" alt="""" xmlns=""http://www.w3.org/1999/xhtml"" />"));
			Assert.That(ContainsError(@"Image input missing alt text: <input type=""image"" id=""imgSecond"" xmlns=""http://www.w3.org/1999/xhtml"" />"));
		}
	}
}