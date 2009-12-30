using System;
using System.Linq;
using System.Text;
using System.Xml;
using Naak.HtmlRules;
using Naak.HtmlRules.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Tjoc.Web.Validator;

namespace Naak.UnitTests
{
	public class HtmlRuleTester
	{
		private ValidationError[] _errors = new ValidationError[0];

		protected static void AssertHtmlRulePasses(IHtmlRule htmlRule, string bodyHtml)
		{
			var document = GetDocument(bodyHtml);
			var records = htmlRule.ValidateHtml(document, GetNamespaceManager(document));

			Assert.That(records.Length, Is.EqualTo(0));
		}

		protected void ExecuteTest(IHtmlRule rule, string bodyHtml)
		{
			XmlDocument document = GetDocument(bodyHtml);
			XmlNamespaceManager namespaceManager = GetNamespaceManager(document);

			_errors = rule.ValidateHtml(document, namespaceManager);
		}

		protected int ErrorCount
		{
			get { return _errors.Length; }
		}

		protected bool ContainsError(string message)
		{
			var containsError = _errors.Any(record => record.Message == message);
			return containsError;
		}

		protected static void AssertHtmlRuleFails(IHtmlRule htmlRule, string bodyHtml, int lineNumber, int linePosition, string message)
		{
			XmlDocument document = GetDocument(bodyHtml);
			XmlNamespaceManager namespaceManager = GetNamespaceManager(document);

			IHtmlRule rule = htmlRule;
			var records = rule.ValidateHtml(document, namespaceManager);

			Assert.That(records.Length, Is.EqualTo(1));
			Assert.That(records[0].Message, Is.EqualTo(message));
		}

		private static XmlNamespaceManager GetNamespaceManager(XmlDocument document)
		{
			var namespaceManager = new XmlNamespaceManager(document.NameTable);
			namespaceManager.AddNamespace("x", "http://www.w3.org/1999/xhtml");
			return namespaceManager;
		}

		private static XmlDocument GetDocument(string body)
		{
			var builder = new StringBuilder();
			builder.AppendLine("<!DOCTYPE html PUBLIC \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
			builder.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
			builder.AppendLine("  <head>");
			builder.AppendLine("    <title>Application</title>");
			builder.AppendLine("  </head>");
			builder.AppendLine("  <body>");
			builder.AppendLine("    <div id=\"container\">");
			builder.AppendLine("      ||BODY||");
			builder.AppendLine("    </div>");
			builder.AppendLine("  </body>");
			builder.AppendLine("</html>");
			var htmlTemplate = builder.ToString();

			var replacer = new TokenReplacer {Text = htmlTemplate};
			replacer.Replace("BODY", body);
			var html = replacer.Text;

			var document = new XmlDocument();
			document.XmlResolver = new LocalDTDUrlResolver();
			document.LoadXml(html);
			return document;
		}
	}

	public class TokenReplacer
	{
		private string _text;

		public string Text
		{
			get { return _text; }
			set { _text = value; }
		}

		public void Replace(string token, string tokenValue)
		{
			if (_text == null)
				throw new ApplicationException("The Text property must be set before tokens may be replaced");

			string tokenWithDelimiters = string.Format("||{0}||", token);
			_text = _text.Replace(tokenWithDelimiters, tokenValue);
		}
	}

}