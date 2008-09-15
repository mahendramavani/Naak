using System;
using Naak.HtmlRules;
using Naak.HtmlRules.Impl;
using StructureMap;
using Tarantino.Core.Commons.Services.Logging;
using Tarantino.Infrastructure;

namespace Naak.HtmlRules
{
	public class DependencyRegistrar
	{
		private static bool _dependenciesRegistered;

		public void RegisterDependencies()
		{
			Logger.Debug(this, "Registering types with StructureMap");
			StructureMapConfiguration.UseDefaultStructureMapConfigFile = false;

			InfrastructureDependencyRegistrar.RegisterInfrastructure();

			Register<IHtmlRuleExecutor, HtmlRuleExecutor>();
			Register<IXmlDocumentBuilder, XmlDocumentBuilder>();
			Register<IXmlNamespaceManagerBuilder, XmlNamespaceManagerBuilder>();

			Register<IHtmlRule, AtLeastOneH1>();
			Register<IHtmlRule, FormElementsHaveLabels>();
			Register<IHtmlRule, ImageInputsHaveAltText>();
			Register<IHtmlRule, TablesHaveColumnHeaders>();
			Register<IHtmlRule, FieldsetsHaveLegends>();
			Register<IHtmlRule, LabelsRelateToFormElements>();
			Register<IHtmlRule, ImagesHaveAltText>();
			Register<IHtmlRule, HeadingsAreLogicallyOrdered>();
		}

		public T Resolve<T>()
		{
			return ObjectFactory.GetInstance<T>();
		}

		public static bool Registered<T>()
		{
			EnsureDependenciesRegistered();
			return ObjectFactory.GetInstance<T>() != null;
		}

		public static bool Registered(Type type)
		{
			EnsureDependenciesRegistered();
			return ObjectFactory.GetInstance(type) != null;
		}

		private static void Register<I, C>() where C : I
		{
			StructureMapConfiguration.BuildInstancesOf<I>().TheDefaultIsConcreteType<C>();
			Register<C>();
		}

		private static void Register<C>()
		{
			StructureMapConfiguration.BuildInstancesOf<C>().TheDefaultIsConcreteType<C>();
		}

		public static void EnsureDependenciesRegistered()
		{
			if (!_dependenciesRegistered)
			{
				new DependencyRegistrar().RegisterDependencies();
				_dependenciesRegistered = true;
			}
		}
	}
}