using System.Net.Mime;
using System.Text;
using Naak.HtmlRules.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Naak.UnitTests
{
	[TestFixture]
	public class ContextOfLinkTextMustMakeSenseTester : HtmlRuleTester
	{
		[Test]
		public void Identifies_link_with_same_URL_same_TEXT_as_No_Error()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<a href=""/home"">TestLink</a>");
			bodyHtml.Append(@"<a href=""/home"">TestLink</a>");
			bodyHtml.Append(@"<P/>");

			ExecuteTest(new ContextOfLinkTextMustMakeSense(), bodyHtml.ToString());
			Assert.That(ErrorCount, Is.EqualTo(0));
		}

		[Test]
		public void Identifies_link_with_different_URL_same_TEXT_as_Error()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<a href=""/home2"">TestLink</a>");
			bodyHtml.Append(@"<a href=""/home"">TestLink</a>");
			bodyHtml.Append(@"<P/>");

			ExecuteTest(new ContextOfLinkTextMustMakeSense(), bodyHtml.ToString());
			Assert.That(ErrorCount, Is.EqualTo(1));
		}

		[Test]
		public void Identifies_link_with_different_URL_same_TEXT_same_TITLE_as_Error()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<a href=""/home2"" title=""title"">TestLink</a>");
			bodyHtml.Append(@"<a href=""/home"" title=""title"">TestLink</a>");
			bodyHtml.Append(@"<P/>");

			ExecuteTest(new ContextOfLinkTextMustMakeSense(), bodyHtml.ToString());
			Assert.That(ErrorCount, Is.EqualTo(1));
		}

		[Test]
		public void Identifies_link_with_different_URL_same_TEXT_different_TITLE_as_No_Error()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<a href=""/home2"" title=""title1"">TestLink</a>");
			bodyHtml.Append(@"<a href=""/home"" title=""title"">TestLink</a>");
			bodyHtml.Append(@"<P/>");

			ExecuteTest(new ContextOfLinkTextMustMakeSense(), bodyHtml.ToString());
			Assert.That(ErrorCount, Is.EqualTo(0));
		}

		[Test]
		public void Identifies_link_with_different_URL_same_TEXT_one_of_them_missing_TITLE_as_No_Error()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<a href=""/home2"" >TestLink</a>");
			bodyHtml.Append(@"<a href=""/home"" title=""title"">TestLink</a>");
			bodyHtml.Append(@"<P/>");

			ExecuteTest(new ContextOfLinkTextMustMakeSense(), bodyHtml.ToString());
			Assert.That(ErrorCount, Is.EqualTo(0));
		}

		[Test]
		public void Identifies_link_with_different_URL_same_TEXT_same_NAME_as_Error()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<a href=""/home2"" name=""name"">TestLink</a>");
			bodyHtml.Append(@"<a href=""/home"" name=""name"">TestLink</a>");
			bodyHtml.Append(@"<P/>");

			ExecuteTest(new ContextOfLinkTextMustMakeSense(), bodyHtml.ToString());
			Assert.That(ErrorCount, Is.EqualTo(1));
		}

		[Test]
		public void Identifies_link_with_missing_URL_same_TEXT_same_NAME_as_No_Error()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<a name=""name"">TestLink</a>");
			bodyHtml.Append(@"<a name=""name"">TestLink</a>");
			bodyHtml.Append(@"<P/>");

			ExecuteTest(new ContextOfLinkTextMustMakeSense(), bodyHtml.ToString());
			Assert.That(ErrorCount, Is.EqualTo(0));
		}

		[Test]
		public void Identifies_link_with_different_URL_same_TEXT_different_NAME_as_No_Error()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<a href=""/home2"" name=""name1"">TestLink</a>");
			bodyHtml.Append(@"<a href=""/home"" name=""name"">TestLink</a>");
			bodyHtml.Append(@"<P/>");

			ExecuteTest(new ContextOfLinkTextMustMakeSense(), bodyHtml.ToString());
			Assert.That(ErrorCount, Is.EqualTo(0));
		}

		[Test]
		public void Identifies_link_with_different_URL_same_TEXT_one_of_them_missing_NAME_as_No_Error()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<a href=""/home2"" >TestLink</a>");
			bodyHtml.Append(@"<a href=""/home"" name=""name"">TestLink</a>");
			bodyHtml.Append(@"<P/>");

			ExecuteTest(new ContextOfLinkTextMustMakeSense(), bodyHtml.ToString());
			Assert.That(ErrorCount, Is.EqualTo(0));
		}

		[Test]
		public void Identifies_link_with_missing_URL_same_TEXT_one_of_them_missing_NAME_as_No_Error()
		{
			var bodyHtml = new StringBuilder();
			bodyHtml.Append(@"<a >TestLink</a>");
			bodyHtml.Append(@"<a name=""name"">TestLink</a>");
			bodyHtml.Append(@"<P/>");

			ExecuteTest(new ContextOfLinkTextMustMakeSense(), bodyHtml.ToString());
			Assert.That(ErrorCount, Is.EqualTo(0));
		}
	}
}