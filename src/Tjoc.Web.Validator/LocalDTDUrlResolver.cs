using System;
using System.IO;
using System.Xml;

namespace Tjoc.Web.Validator
{
	public class LocalDTDUrlResolver : XmlUrlResolver
	{
		public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
		{
			string dtdFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xhtml1-transitional.dtd");
			return new MemoryStream(File.ReadAllBytes(dtdFile));
		}

		public override Uri  ResolveUri(Uri baseUri, string relativeUri)
		{
			string dtdFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "xhtml1-transitional.dtd");
			return new Uri(dtdFile);
		}
	}
}