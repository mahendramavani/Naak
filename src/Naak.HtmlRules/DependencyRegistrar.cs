using System;
using Naak.HtmlRules;
using Naak.HtmlRules.Impl;
using StructureMap;

namespace Naak.HtmlRules
{
	public class DependencyRegistrar
	{
		private static bool _dependenciesRegistered;

		public void RegisterDependencies()
		{
			Logger.Debug(this, "Registering types with StructureMap");

			ObjectFactory.Initialize(x => x.Scan(s =>
			{
				s.AssemblyContainingType<NaakRegistry>();
				s.LookForRegistries();
			}));
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