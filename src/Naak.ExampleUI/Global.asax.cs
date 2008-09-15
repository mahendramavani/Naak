using System;
using System.Web;
using Naak.HtmlRules;

namespace Naak.ExampleUI
{
	public class Global : HttpApplication
	{
		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			DependencyRegistrar.EnsureDependenciesRegistered();
		}
	}
}