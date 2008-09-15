using Naak.HtmlRules.Impl;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Naak.UnitTests
{
	[TestFixture]
	public class ValidationErrorTester
	{
		[Test]
		public void Correctly_serializes_message()
		{
			var record = new ValidationError("error message");
			Assert.That(record.ToString(), Is.EqualTo("error message"));
		}
	}
}