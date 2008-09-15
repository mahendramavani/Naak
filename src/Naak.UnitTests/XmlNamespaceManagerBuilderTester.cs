using System.Xml;
using Naak.HtmlRules;
using Naak.HtmlRules.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Naak.UnitTests
{
	[TestFixture]
	public class XmlNamespaceManagerBuilderTester
	{
		[Test]
		public void Builds_namespace_manager()
		{
			IXmlNamespaceManagerBuilder builder = new XmlNamespaceManagerBuilder();
			var document = new XmlDocument();
			XmlNamespaceManager manager = builder.Build(document);

			Assert.That(manager.LookupNamespace("x"), Is.EqualTo("http://www.w3.org/1999/xhtml"));
		}
	}
}