using System.Xml;
using Naak.HtmlRules;
using Naak.HtmlRules.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Naak.UnitTests
{
	[TestFixture]
	public class XmlDocumentBuilderTester
	{
		[Test]
		public void Builds_xml_document()
		{
			IXmlDocumentBuilder builder = new XmlDocumentBuilder();
			XmlDocument document = builder.Build("<test />");

			Assert.That(document.Name, Is.EqualTo("#document"));
			Assert.That(document.ChildNodes.Count, Is.EqualTo(1));
			Assert.That(document.ChildNodes[0].Name, Is.EqualTo("test"));
		}
	}
}