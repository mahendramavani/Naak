using System.Text;
using Naak.HtmlRules.Impl;
using Naak.UnitTests;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Naak.UnitTests
{
	[TestFixture]
	public class LabelsRelateToFormElementsTester : HtmlRuleTester
	{
		[Test]
		public void Correctly_identifies_labels_that_do_not_relate_to_form_elements()
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

			html.Append(@"  <label for=""chkOption2"">Option 2</label>");

			html.Append(@"  <label for=""ddlStatus1"">Status 1</label>");
			html.Append(@"  <select id=""ddlStatus1"" />");

			html.Append(@"  <label for=""ddlStatus2"">Status 2</label>");

			html.Append(@"  <label for=""txtArea1"">Area 1</label>");
			html.Append(@"  <textarea id=""txtArea1"" />");

			html.Append(@"  <label for=""txtArea2"">Area 2</label>");
			html.Append(@"</form>");

			ExecuteTest(new LabelsRelateToFormElements(), html.ToString());

			Assert.That(ErrorCount, Is.EqualTo(6));

			Assert.That(ContainsError(@"Label does not relate to a form control: <label for=""txtMiddle"" xmlns=""http://www.w3.org/1999/xhtml"">First Name</label>"));
			Assert.That(ContainsError(@"Label does not relate to a form control: <label xmlns=""http://www.w3.org/1999/xhtml"">Last Name</label>"));
			Assert.That(ContainsError(@"Label does not relate to a form control: <label for=""txtRetype"" xmlns=""http://www.w3.org/1999/xhtml"">Retype Password</label>"));
			Assert.That(ContainsError(@"Label does not relate to a form control: <label for=""chkOption2"" xmlns=""http://www.w3.org/1999/xhtml"">Option 2</label>"));
			Assert.That(ContainsError(@"Label does not relate to a form control: <label for=""ddlStatus2"" xmlns=""http://www.w3.org/1999/xhtml"">Status 2</label>"));
			Assert.That(ContainsError(@"Label does not relate to a form control: <label for=""txtArea2"" xmlns=""http://www.w3.org/1999/xhtml"">Area 2</label>"));
		}
	}
}