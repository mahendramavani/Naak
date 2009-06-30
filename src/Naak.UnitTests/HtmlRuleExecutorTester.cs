using System.Xml;
using Naak.HtmlRules;
using Naak.HtmlRules.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;

namespace Naak.UnitTests
{
	[TestFixture]
	public class HtmlRuleExecutorTester
	{
		[Test]
		public void Should_execute_all_rules_and_aggregate_results()
		{
			var record1 = new ValidationError(string.Empty);
			var record2 = new ValidationError(string.Empty);
			var record3 = new ValidationError(string.Empty);

			var xmlDocument = new XmlDocument();
			var namespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);

			var serviceLocator = MockRepository.GenerateStub<IServiceLocator>();
			var documentBuilder = MockRepository.GenerateStub<IXmlDocumentBuilder>();
			var namespaceManagerBuilder = MockRepository.GenerateStub<IXmlNamespaceManagerBuilder>();
			var rule1 = MockRepository.GenerateStub<IHtmlRule>();
			var rule2 = MockRepository.GenerateStub<IHtmlRule>();
			var rule3 = MockRepository.GenerateStub<IHtmlRule>();

			documentBuilder.Stub(db => db.Build("<test />")).Return(xmlDocument);
			namespaceManagerBuilder.Stub(nmb => nmb.Build(xmlDocument)).Return(namespaceManager);

			serviceLocator.Stub(sl => sl.CreateAllInstances<IHtmlRule>()).Return(new[] {rule1, rule2, rule3});

			rule1.Stub(rule => rule.ValidateHtml(xmlDocument, namespaceManager)).Return(new[] {record1});
			rule2.Stub(rule => rule.ValidateHtml(xmlDocument, namespaceManager)).Return(new ValidationError[0]);
			rule3.Stub(rule => rule.ValidateHtml(xmlDocument, namespaceManager)).Return(new[] {record2, record3});

			IHtmlRuleExecutor executor = new HtmlRuleExecutor(serviceLocator, documentBuilder, namespaceManagerBuilder);
			ValidationError[] errors = executor.GetAccessibilityErrors("<test />");

			Assert.That(errors, Is.EqualTo(new[] {record1, record2, record3}));
		}
	}
}