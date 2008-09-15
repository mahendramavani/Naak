using System.Text;
using Naak.HtmlRules.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Naak.UnitTests
{
	[TestFixture]
	public class FormElementsHaveLabelsTester : HtmlRuleTester
	{
		[Test]
		public void Correctly_identifies_form_fields_missing_labels()
		{
			var html = new StringBuilder();

			html.Append(@"<form>");
			html.Append(@"  <label for=""txtFirstName"">First Name</label>");
			html.Append(@"  <input type=""text"" id=""txtFirstName"" />");

			html.Append(@"  <label for=""txtMiddle"">First Name</label>");
			html.Append(@"  <input type=""text"" id=""txtMiddleName"" />");

			html.Append(@"  <label>Last Name</label>");
			html.Append(@"  <input type=""text"" id=""txtLastName"" />");

			html.Append(@"  <label for=""txtPassword"">Password</label>");
			html.Append(@"  <input type=""text"" id=""txtPassword"" />");

			html.Append(@"  <label for=""txtRetype"">Retype Password</label>");
			html.Append(@"  <input type=""password"" id=""txtRetypePassword"" />");

			html.Append(@"  <label for=""chkOption1"">Option 1</label>");
			html.Append(@"  <input type=""checkbox"" id=""chkOption1"" />");

			html.Append(@"  <label for=""chkOption"">Option 2</label>");
			html.Append(@"  <input type=""checkbox"" id=""chkOption2"" />");

			html.Append(@"  <label for=""ddlStatus1"">Status 1</label>");
			html.Append(@"  <select id=""ddlStatus1"" />");

			html.Append(@"  <label for=""ddlStatus"">Status 2</label>");
			html.Append(@"  <select id=""ddlStatus2"" />");

			html.Append(@"  <label for=""txtArea1"">Area 1</label>");
			html.Append(@"  <textarea id=""txtArea1"" />");

			html.Append(@"  <label for=""txtArea"">Area 2</label>");
			html.Append(@"  <textarea id=""txtArea2"" />");
			html.Append(@"</form>");

			ExecuteTest(new FormElementsHaveLabels(), html.ToString());

			Assert.That(ErrorCount, Is.EqualTo(6));

			Assert.That(
				ContainsError(
					@"Textbox missing correpsonding label: <input type=""text"" id=""txtMiddleName"" xmlns=""http://www.w3.org/1999/xhtml"" />"));
			Assert.That(
				ContainsError(
					@"Textbox missing correpsonding label: <input type=""text"" id=""txtLastName"" xmlns=""http://www.w3.org/1999/xhtml"" />"));
			Assert.That(
				ContainsError(
					@"Password textbox missing correpsonding label: <input type=""password"" id=""txtRetypePassword"" xmlns=""http://www.w3.org/1999/xhtml"" />"));
			Assert.That(
				ContainsError(
					@"Checkbox missing correpsonding label: <input type=""checkbox"" id=""chkOption2"" xmlns=""http://www.w3.org/1999/xhtml"" />"));
			Assert.That(
				ContainsError(
					@"Select list missing correpsonding label: <select id=""ddlStatus2"" xmlns=""http://www.w3.org/1999/xhtml"" />"));
			Assert.That(
				ContainsError(
					@"Text area missing correpsonding label: <textarea id=""txtArea2"" xmlns=""http://www.w3.org/1999/xhtml"" />"));
		}
	}
}